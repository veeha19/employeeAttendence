using DAL.DataServices;
using BOL.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL.DatabaseEntities;
using Microsoft.SqlServer.Server;

namespace BLL.LogicServices
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private readonly IEmployeeDataDAL _employeeDataDAL;

        public EmployeeLogic(IEmployeeDataDAL employeeDataDAL)
        {
            this._employeeDataDAL = employeeDataDAL;
        }

        public List<Employee> GetEmployeeList()
        {
            List<Employee> result = [];
            result = _employeeDataDAL.GetEmployeeListDAL();
            return result;
        }

        public string CreateEmployeeLogic(Employee employeeData)
        {
            string result = string.Empty;

            if (String.IsNullOrWhiteSpace(employeeData.FristName) || String.IsNullOrWhiteSpace(employeeData.LastName) || String.IsNullOrWhiteSpace(employeeData.MobileNumber))
            {
                result = "Please fill all form field";
                return result;
            }

            result = _employeeDataDAL.SaveEmployeeRecord(employeeData);
            if (result == "Saved SuccessFully")
            {
                return result;
            }
            else
            {
                result = " An error occured. Please try again";
                return result;
            }

        }
        public string EmployeeUpdateRecordLogic(Employee employee)
        {
            string result;

            if (employee == null || employee.EMPId == Guid.Empty)
            {
                result = "Invalid student data";
                return result;
            }

            if (String.IsNullOrWhiteSpace(employee.FristName) || String.IsNullOrWhiteSpace(employee.LastName) || String.IsNullOrWhiteSpace(employee.MobileNumber))
            {
                result = "Please fill all form field";
                return result;
            }

            result = _employeeDataDAL.UpdateEmployeeRecordDAL(employee);
            if (result == "Updated Successfully")
            {
                return result;
            }
            else
            {
                result = " An error occured. Please try again";
                return result;
            }
        }
        public async Task<Employee> GetEmployeeById(Guid EMPId)
        {
            return await Task.Run(() =>
            {
                return _employeeDataDAL.GetEmployeeById(EMPId);
            });
        }
        public async Task<List<EmployeeStatus>> StatusEMPId()
        {
            return await Task.Run(() =>
            {
                return _employeeDataDAL.GetStatusEMPId();
            });

        }
        public async Task<string> EmployeeAttendence(EmployeeStatus employee)
        {
            string result;

            result = await _employeeDataDAL.EmployeeUpdateStatus(employee);
            if (result == "Updated Successfully")
            {
                return result;
            }
            else
            {
                result = " An error occured. Please try again";
                return result;
            }
        }
        public async Task<List<EmployeeDetail>> GetNewEmployeeList()
        {
            return await Task.Run(() =>
            {
                return _employeeDataDAL.NewEmployeeListDAL();
            });
        }
        public async Task<List<Employee>> EmployeeProfile( )
        {
            return await Task.Run(() =>
            {
                return _employeeDataDAL.EMPProfileDAL();
            });
        }
        public async Task<List<EMPProfileInformation>> NewEmployeeList(EmployeeSearchFilter filter)
        {
            return await Task.Run(() =>
            {
                    
                return  _employeeDataDAL.EmpDATA(filter);

            });

        }
        public async Task<List<Employee>> EmployeeLeave()
        {
            return await Task.Run(() =>
            {
                return _employeeDataDAL.EMPLeaveDAL();
            });

        }
        public async Task<List<EMPProfileInformation>> EMPAllocation(EmployeeSearchFilter filter)
        {
            return await Task.Run(() =>
            {
                return _employeeDataDAL.LeaveDAL(filter);

            });
        }

    }
}