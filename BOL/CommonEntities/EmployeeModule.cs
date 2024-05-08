using BOL.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.CommonEntities
{
    public class EmployeeModule
    {   
        public List<Employee>? employeeList {  get; set; }
        public List<EmployeeStatus> StatusList { get; set; }
        public List<EmployeeDetail> DetailList { get; set; }
        public List<EMPProfileInformation> EMPInformation {  get; set; }
    }
}
