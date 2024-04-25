using LeaveApiClient.Models;
using LeaveApiClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveApiClient.Controllers
{
    public class LeavesController : Controller
    {
        private readonly ApiService _apiService;
        public LeavesController(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            var leaves = await _apiService.GetAllLeavesAsync();
            if (leaves == null || !leaves.Any())
            {
                return View(new List<Leave>());
            }

            return View(leaves);
        }
        // Get: Leave/create hämtar f rtt tomt formulär
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var employees = await _apiService.GetEmployeesAsync();
            if (!employees.Any())
            {
                employees = new List<Employee>();
            }
            ViewBag.Employees = new SelectList(employees, "EmployeeId", "EmployeeName");
            ViewBag.LeaveTypes = new SelectList(Enum.GetValues(typeof(LeaveType))
                .Cast<LeaveType>()
                .Select(x => new { Id = (int)x, Name = x.ToString() }),
                "Id", "Name");
            return View();
        }
        // Post: Leaves/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Leave leave)
        {
            if (!ModelState.IsValid)
            {
                return View(leave);
            }
            await _apiService.AddLeaveAsync(leave);

            return View(leave);
        }
        // hämta detaljer för en view
        public async Task<IActionResult> Details(int id)
        {
            var leave = await _apiService.GetLeaveByIdAsync(id);
            if (leave == null)
            {
                return NotFound();
            }
            return View(leave);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var leave = await _apiService.GetLeaveByIdAsync(id);
            if (leave == null)
                return NotFound();

            var employees = await _apiService.GetEmployeesAsync();
            if (employees == null || !employees.Any())
            {
                // Log the error or handle the situation appropriately
                employees = new List<Employee>(); // Ensure it's never null to avoid SelectList issues
            }

            ViewBag.Employees = new SelectList(employees, "EmployeeId", "EmployeeName", leave.FkEmployeeId);
            return View(leave);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Leave leave)
        {
            if (id != leave.LeaveId)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _apiService.UpdateLeaveAsync(id, leave);
                return RedirectToAction(nameof(Index));
            }
            return View(leave);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var leave = await _apiService.GetLeaveByIdAsync(id);
            if (leave == null)
                return NotFound();

            return View(leave);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeleteLeaveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
