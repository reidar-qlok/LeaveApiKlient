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
    }
}
