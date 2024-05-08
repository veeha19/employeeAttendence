using BLL.LogicServices;
using BOL.CommonEntities;
using BOL.DatabaseEntities;

using Microsoft.AspNetCore.Mvc;

namespace employeeAttendence.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeLogic _employeelogic;
        public EmployeeController(IEmployeeLogic employeelogic)
        {
            _employeelogic = employeelogic;
        }

        [HttpGet]
        public IActionResult EmployeeList()
        {
            EmployeeModule model = new();
            model.employeeList = _employeelogic.GetEmployeeList().ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewEmployee(Employee employeeData)
        {
            string result = _employeelogic.CreateEmployeeLogic(employeeData);
            if (result == "Saved Successfully") 
            {
                return RedirectToAction("EmployeeList", "Employee");
            }
            else
            {
                TempData["ErrorTemp"] = result;
                return RedirectToAction("CreateEmployee", "Employee");
            }

        }

        [HttpGet]
        public async Task<IActionResult> UpdateEmployee(Guid EMPId)
        {
            Employee employee = await _employeelogic.GetEmployeeById(EMPId);
            if (employee == null)
            {
                return NotFound();
            }
            await Task.Run(() => View("View", employee));
            return View();
        }

        [HttpPost]
        public IActionResult UpdateEmployeeRecord(Employee employee)
        {

            string result = _employeelogic.EmployeeUpdateRecordLogic(employee);
            if (result == "Updated Successfully")
            {
                return RedirectToAction("EmployeeList", "Employee");
            }
            else
            {
                TempData["ErrorTemp"] = result;
                return RedirectToAction("UpdateEmployee", "Employee");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Employee()
        {
            EmployeeModule model = new();
            model.StatusList = await _employeelogic.StatusEMPId();
            if (model.StatusList == null)
            {
                return NotFound();
            }
            await Task.Run(() => View("View", model));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeStatus(EmployeeStatus employee)
        {
            string result = await _employeelogic.EmployeeAttendence(employee);
            if (result == "Updated Successfully")
            {
                return RedirectToAction("Employee");
            }
            else
            {
                TempData["ErrorTemp"] = result;
                return RedirectToAction("EmployeeList", "Employee");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EMPDetail()
        {
            EmployeeModule model = new();

            model.DetailList = await _employeelogic.GetNewEmployeeList();
            if (model.DetailList == null)
            {
                return NotFound();
            }
            await Task.Run(() => View("View", model));
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EMPProfileGet()
        {
            EmployeeModule model = new();
            
            model.employeeList = await _employeelogic.EmployeeProfile();
            if (model.employeeList == null )
            {
                return NotFound();
            }
            await Task.Run(()=> View("View",model));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EMPProfile(EmployeeSearchFilter filter)
        {
            EmployeeModule model = new();
            
            model.EMPInformation = await _employeelogic.NewEmployeeList(filter);
            if (model.EMPInformation == null)
            { 
               return NotFound();
            }
            //await Task.Run(() => View("EMPProfileGet", model));
            return View("_EMPProfileGet", model);
        }

        [HttpGet]
        public async Task<IActionResult> EMPLeave()
        {
            EmployeeModule model = new();

            model.employeeList = await _employeelogic.EmployeeLeave();
            
            if(model.employeeList == null)
            {
                return BadRequest();
            }
            await Task.Run(() => View("View", model));
            return View();
        } 

        [HttpPost]
        public async Task <IActionResult> LeaveAllocation(EmployeeSearchFilter filter)
        {
            EmployeeModule model = new();

            model.EMPInformation = await _employeelogic.EMPAllocation(filter);
            if(model.EMPInformation == null)
            {
                return BadRequest();
            }
            return View("_LeaveAllocation", model);
        }
    }
}