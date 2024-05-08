using BOL.CommonEntities;
using BOL.DatabaseEntities;
using DAL.DataContent;
using Dapper;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;


namespace DAL.DataServices
{
    public class EmployeeDataDAL : IEmployeeDataDAL
    {
        private readonly IDapperOrmHelper _dapperOrmHelper;

        public EmployeeDataDAL(IDapperOrmHelper dapperOrmHelper)
        {
            _dapperOrmHelper = dapperOrmHelper;
        }

        public List<Employee> GetEmployeeListDAL()
        {
            List<Employee> employee = [];

            try
            {
                using IDbConnection dbConnection = _dapperOrmHelper.GetDapperContextHelper();
                {
                    string SqlQuery = "SELECT * FROM Employee ORDER BY FristName";
                    employee = dbConnection.Query<Employee>(SqlQuery, commandType: CommandType.Text).ToList();

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return employee;
        }

        public string SaveEmployeeRecord(Employee employeeData)
        {
            string result = string.Empty;

            try
            {
                using IDbConnection dbConnection = _dapperOrmHelper.GetDapperContextHelper();
                {
                    Guid Id = Guid.NewGuid();
                    employeeData.EMPId = Id;
                    dbConnection.Execute(@"INSERT INTO 
                    Employee(EMPId,FristName, 
                    LastName, MobileNumber,UserCreateTime)VALUES
                    (@EMPId,@FristName,@LastName,@MobileNumber,@UserCreateTime)",

                    new
                    {
                        employeeData.EMPId,
                        employeeData.FristName,
                        employeeData.LastName,
                        employeeData.MobileNumber,
                        UserCreateTime = DateTime.Now,
                    },

                    commandType: CommandType.Text);
                    RegisterEmployeeRecord(Id);

                    result = "Saved SuccessFully";
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }

            return result;
        }
        public string RegisterEmployeeRecord(Guid employeeId)
        {
            string result = string.Empty;

            try
            {
                using IDbConnection dbConnection = _dapperOrmHelper.GetDapperContextHelper();
                {

                    dbConnection.Execute(@"INSERT INTO 
                    EmployeeRegistration(EMPId)VALUES
                    (@EMPId)",

                    new
                    {
                        EMPId = employeeId
                    },
                    commandType: CommandType.Text);

                    result = "Saved SuccessFully";
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }

            return result;
        }
        public string UpdateEmployeeRecordDAL(Employee employee)
        {
            string result = string.Empty;

            try
            {

                using IDbConnection dbConnection = _dapperOrmHelper.GetDapperContextHelper();
                {
                    dbConnection.Execute(
                        @"UPDATE Employee 
                         SET FristName = @FristName, 
                         LastName = @LastName, 
                         MobileNumber = @MobileNumber,
                         UpdateValueTime = @UpdateValueTime
                         WHERE EMPId = @EMPId ORDER BY FristName",

                        new
                        {
                            employee.FristName,
                            employee.LastName,
                            employee.MobileNumber,
                            employee.EMPId,
                            UpdateValueTime = DateTime.Now,
                        },
                        commandType: CommandType.Text);

                    result = "Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return result;
        }

        public async Task<Employee> GetEmployeeById(Guid EMPId)
           {
            try
            {
                using IDbConnection dbConnection = _dapperOrmHelper.GetDapperContextHelper();
                {
                    string query = @"SELECT * FROM Employee WHERE EMPId = @EMPId";
                    Employee result = await dbConnection.QueryFirstOrDefaultAsync<Employee>(query, new { EMPId });
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employee with ID {EMPId}: {ex.Message}");
                return null;
            }
        }
        public async Task<List<EmployeeStatus>> GetStatusEMPId()
        {
            try
            {

                using IDbConnection dbConnection = _dapperOrmHelper.GetDapperContextHelper();
                {
                    string query = @"SELECT Employee.EMPId, CONCAT(FristName,' ', LastName) 
                    AS EMPName, EntryDateTime AS EntryTime,ExitDateTime AS ExitTime,Status FROM Employee 
                    INNER JOIN EmployeeRegistration ON Employee.EMPId = EmployeeRegistration.EMPId ORDER BY EMPName;";

                    var employee = await dbConnection.QueryAsync<EmployeeStatus>(query, commandType: CommandType.Text);
                    return employee.AsList();
                }
            }
            catch (Exception)
            {
                Console.Write("Not Found");
                return null;
            }
        }
        public async Task<string> EmployeeUpdateStatus(EmployeeStatus employee)
        {
            string result = string.Empty;

            try
            {
                using IDbConnection dbConnection = _dapperOrmHelper.GetDapperContextHelper();
                {

                    if (employee.Status == false)
                    {
                        dbConnection.Execute(
                        @"UPDATE EmployeeRegistration 
                         SET EntryDateTime = @EntryDateTime,
                         Status = @Status
                         WHERE EMPId = @EMPId",

                        new
                        {
                            employee.EMPId,
                            EntryDateTime = employee.EntryTime == null ? DateTime.Now : employee.EntryTime,
                            Status = !employee.Status
                        },
                        commandType: CommandType.Text);

                        result = "Updated Successfully";
                    }
                    else
                    {
                        dbConnection.Execute(
                        @"UPDATE EmployeeRegistration 
                         SET ExitDateTime = @ExitDateTime,
                         Status = @Status
                         WHERE EMPId = @EMPId",

                        new
                        {
                            employee.EMPId,
                            ExitDateTime = employee.ExitTime == null ? DateTime.Now : employee.ExitTime,
                            Status = !employee.Status
                        },
                        commandType: CommandType.Text);

                        result = "Updated Successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return result;
        }
        public async Task<List<EmployeeDetail>> NewEmployeeListDAL()
        {
            try
            {
                using IDbConnection dbConnection = _dapperOrmHelper.GetDapperContextHelper();
                {

                    string query = @"SELECT EmployeeRegistration.EMPId, 
                    CONVERT(DATE, EmployeeRegistration.EntryDateTime) AS Date,
                    CONVERT(TIME, EmployeeRegistration.EntryDateTime) AS EntryTime,
                    CONVERT(TIME, EmployeeRegistration.ExitDateTime) AS ExitTime
                    FROM EmployeeRegistration;";

                    return (await dbConnection.QueryAsync<EmployeeDetail>(query, commandType: CommandType.Text)).AsList();

                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return null;
        }
        public async Task<List<Employee>> EMPProfileDAL()
          {
            try 
            { 

                using IDbConnection dbConnection = _dapperOrmHelper.GetDapperContextHelper();

                {
                    string query = @"SELECT DISTINCT FristName, LastName FROM Employee;";

                    return (await dbConnection.QueryAsync<Employee>(query, commandType: CommandType.Text)).AsList();

                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return null;
        }
        public async Task<List<EMPProfileInformation>> EmpDATA(EmployeeSearchFilter filter)
        {
            string result = string.Empty;

            try
            {
                using IDbConnection dbConnection = _dapperOrmHelper.GetDapperContextHelper();
                {
                     string query = @"SELECT Employee.EMPId,  
                     CONCAT(Employee.FristName, ' ', Employee.LastName) AS EMPName,
                     CONVERT(DATE, EmployeeRegistration.EntryDateTime) AS Date,
                     CONVERT(DATETIME, EmployeeRegistration.EntryDateTime) AS EntryTime,
                     CONVERT(DATETIME, EmployeeRegistration.ExitDateTime) AS ExitTime,
                     DATEDIFF(MINUTE, EmployeeRegistration.EntryDateTime, EmployeeRegistration.ExitDateTime) / 60.0 AS Hours,
                     SUM(DATEDIFF(MINUTE, EmployeeRegistration.EntryDateTime, EmployeeRegistration.ExitDateTime))
                     OVER (PARTITION BY Employee.EMPId) / 60.0 AS MonthlyHours
                     FROM Employee
                     JOIN EmployeeRegistration ON Employee.EMPId = EmployeeRegistration.EMPId
                     WHERE CONCAT(Employee.FristName, ' ',Employee.LastName) LIKE @EMPName
                     AND YEAR(EmployeeRegistration.EntryDateTime) = @Year
                     AND MONTH(EmployeeRegistration.EntryDateTime) = @Month
                     ORDER BY EMPName;";

                    return (await dbConnection.QueryAsync<EMPProfileInformation>(query, new
                    {   
                        EMPName = filter.EMPName,
                        Year = filter.Year,
                        Month = filter.Month
                    },
                      
                    commandType: CommandType.Text)).AsList();
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return null;
        }

        public async Task<List<Employee>> EMPLeaveDAL()
        {
            string result = string.Empty;

            try
            {
                using IDbConnection dbConnection = _dapperOrmHelper.GetDapperContextHelper();
                {
                    string query = @"SELECT DISTINCT FristName, LastName FROM Employee;";

                    return (await dbConnection.QueryAsync<Employee>(query,commandType: CommandType.Text)).AsList();
                }
            }
            catch(Exception ex)
            {
                _ = ex.Message;
            }
            return null;
        }
        public async Task<List<EMPProfileInformation>> LeaveDAL(EmployeeSearchFilter filter)
        {
            string result = string.Empty;

            try
            {
                using IDbConnection dbConnection = _dapperOrmHelper.GetDapperContextHelper();
                {
                    string query = @"  
                    SELECT e.EMPId,
                    CONCAT(e.FristName, ' ', e.LastName) AS EMPName, 
                    182 - COUNT(DISTINCT CONVERT(DATE, a.EntryDateTime)) AS AbsentDays,
                    COUNT(DISTINCT CONVERT(DATE, a.EntryDateTime)) AS PresentDays,
                    CONVERT(DATE, a.EntryDateTime) AS Date,
                    CONVERT(DATETIME, a.EntryDateTime) AS EntryTime,
                    CONVERT(DATETIME, a.ExitDateTime) AS ExitTime,
                    DATEDIFF(MINUTE, a.EntryDateTime, a.ExitDateTime) / 60.0 AS Hours
                    FROM 
                    employee e LEFT JOIN employeeRegistration a ON e.EMPId = a.EMPId
                    WHERE CONCAT(e.FristName, ' ', e.LastName) LIKE @EMPName
                    AND YEAR(a.EntryDateTime) = @YEAR 
                    GROUP BY e.EMPId,e.FristName, e.EMPId ,e.LastName,a.EntryDateTime,a.ExitDateTime;";

                    return (await dbConnection.QueryAsync<EMPProfileInformation>(query,new
                    {
                    
                    EMPName = filter.EMPName,
                    Year = filter.Year
                    
                    },
                    commandType: CommandType.Text)).AsList();
                }     
            }
            catch ( Exception ex)
            {
                _= ex.Message;
            }
            return null;
        }
    }
}