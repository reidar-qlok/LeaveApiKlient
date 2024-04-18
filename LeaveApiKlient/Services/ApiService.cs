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
    }
}
