using BOL.CommonEntities;
using BOL.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.LogicServices
{
    public interface IEmployeeLogic
    {
        string CreateEmployeeLogic(Employee employeeData);
        
        List<Employee> GetEmployeeList();
         string EmployeeUpdateRecordLogic(Employee employeeData);
        
        Task<Employee> GetEmployeeById(Guid EMPId);
        Task<List<EmployeeStatus>> StatusEMPId();

        Task<string> EmployeeAttendence(EmployeeStatus employee );
        Task<List<EmployeeDetail>> GetNewEmployeeList();
        Task<List<Employee>> EmployeeProfile();

        Task<List<EMPProfileInformation>> NewEmployeeList(EmployeeSearchFilter filter);

        Task<List<Employee>> EmployeeLeave();
        Task<List<EMPProfileInformation>> EMPAllocation(EmployeeSearchFilter filter);
    }
}
