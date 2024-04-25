using LeaveApiClient.Models;
using LeaveApiClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeaveApiClient.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApiService _apiService;
        public EmployeesController(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _apiService.GetEmployeesAsync();
            if (employees == null || !employees.Any())
            {
                return View(new List<Employee>());
            }

            return View(employees);
        }
        // Get: Employees/create
        public IActionResult Create()
        {
            return View();
        }
        // Post: Emploees/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _apiService.AddEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        // Show a form to get an employee
        ////// GET: Employees/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _apiService.GetEmployeeByIdAsync(id);
            return View(employee);
        }
        ////// Uppdate an employee
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _apiService.UpdateEmployeeAsync(id, employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        // Get details for employees
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _apiService.GetEmployeeByIdAsync(id);
            return View(employee);
        }

        //[HttpGet]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var employee = await _apiService.GetEmployeeByIdAsync(id);
        //    if (employee == null)
        //        return NotFound();
        //    return View(employee);
        //}
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
