using BOL.CommonEntities;
using BOL.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataServices
{
    public interface IEmployeeDataDAL
    {
        string SaveEmployeeRecord(Employee employee);
        string RegisterEmployeeRecord(Guid employeeId);
        string UpdateEmployeeRecordDAL(Employee employee);
        List<Employee> GetEmployeeListDAL();
        Task<Employee> GetEmployeeById(Guid EMPId);
        Task<List<EmployeeStatus>> GetStatusEMPId();
        Task<string> EmployeeUpdateStatus (EmployeeStatus employee );

        Task<List<EmployeeDetail>> NewEmployeeListDAL();

        Task<List<Employee>> EMPProfileDAL();

        Task<List<EMPProfileInformation>> EmpDATA(EmployeeSearchFilter filter);

        Task<List<Employee>> EMPLeaveDAL();

        Task<List<EMPProfileInformation>> LeaveDAL(EmployeeSearchFilter filter);
    }
}
