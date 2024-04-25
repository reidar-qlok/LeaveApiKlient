using LeaveApiClient.Models;
using Newtonsoft.Json;
using System.Text;

namespace LeaveApiClient.Services
{
    public class ApiService
    {
        private readonly HttpClient _client;
        public ApiService(IHttpClientFactory clientfactory)
        {
            _client = clientfactory.CreateClient("API Client");
        }
        // Get all employees
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            try
            {
                var response = await _client.GetAsync("employees");
                if (!response.IsSuccessStatusCode)
                {
                    return new List<Employee>();
                }
                var jsonstring = await response.Content.ReadAsStringAsync();
                var employees = JsonConvert.DeserializeObject<List<Employee>>(jsonstring);
                return employees;

            }
            catch (Exception ex)
            {
                return new List<Employee>();
            }
        }
        /// ///////Create a new employee ////////////////////

        public async Task AddEmployeeAsync(Employee employee)
        {
            var json = JsonConvert.SerializeObject(employee);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("employees", data);
            response.EnsureSuccessStatusCode();
        }


        // Update Employees
        public async Task UpdateEmployeeAsync(int id, Employee employee)
        {
            var json = JsonConvert.SerializeObject(employee);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"employees/{id}", data);
            response.EnsureSuccessStatusCode();
        }
        // Delet an employee
        public async Task DeleteEmployeeAsync(int id)
        {
            var response = await _client.DeleteAsync($"employees/{id}");
            response.EnsureSuccessStatusCode();
        }

        // Returns an employee by id
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var response = await _client.GetAsync($"employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Employee>();
            }
            else
            {
                throw new InvalidOperationException($"API failed with statuscode {response.StatusCode}");
            }
        }
        ////////////////////////////////
        ///         Leaves           ///
        ////////////////////////////////
        // Get all employees
        public async Task<List<Leave>> GetAllLeavesAsync()
        {
            try
            {
                var response = await _client.GetAsync("leaves");
                if (!response.IsSuccessStatusCode)
                {
                    return new List<Leave>();
                }
                var jsonstring = await response.Content.ReadAsStringAsync();
                var leaves = JsonConvert.DeserializeObject<List<Leave>>(jsonstring);
                return leaves;

            }
            catch (Exception ex)
            {
                return new List<Leave>();
            }
        }
        /// ///////Create a new leave ////////////////////

        public async Task AddLeaveAsync(Leave leave)
        {
            var json = JsonConvert.SerializeObject(leave);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("leaves", data);
            response.EnsureSuccessStatusCode();
        }


        // Update Leave
        public async Task UpdateLeaveAsync(int id, Leave leave)
        {
            var json = JsonConvert.SerializeObject(leave);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"leaves/{id}", data);
            response.EnsureSuccessStatusCode();
        }
        // Delet an employee
        public async Task<bool> DeleteLeaveAsync(int id)
        {
            var response = await _client.DeleteAsync($"leaves/{id}");
            response.EnsureSuccessStatusCode();
            return true;
        }

        // Returns a leave by id
        public async Task<Leave?> GetLeaveByIdAsync(int leaveId)
        {
            var response = await _client.GetAsync($"leaves/{leaveId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Leave?>(content);
            }
            else
            {
                return null;
            }
        }
    }
}
