using Newtonsoft.Json;
using PDC50FinalProject.Model;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;

namespace PDC50FinalProject.Services
{
    public class StudentService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost/pdc50/";

        public StudentService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Student>> GetStudentAsync()
        {
            var response =
                await _httpClient.GetFromJsonAsync<List<Student>>($"{BaseUrl}get_user.php");
            return response ?? new List<Student>();
        }

        //Add user
        public async Task<string> AddStudentsAsync(Student student)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}add_user.php", student);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        //Update User
        public async Task<string> UpdateStudentsAsync(Student student)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}update_user.php", student);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        //Delete User
        public async Task<string> DeleteUsersAsync(int studentID)
        {
            var response =
                await _httpClient.PostAsJsonAsync($"{BaseUrl}delete_user.php", new { id = studentID });
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<List<AcademicHistory>> GetAcademicHistoryAsync(int studentId)
        {
            var url = $"{BaseUrl}/get_academic_history.php";
            var payload = JsonConvert.SerializeObject(new { studentId });

            var response = await _httpClient.PostAsync(url, new StringContent(payload, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var history = JsonConvert.DeserializeObject<List<AcademicHistory>>(json);

                // Debugging the response
                Debug.WriteLine("Received academic history: " + json);

                if (history != null)
                {
                    return history;
                }
                else
                {
                    Debug.WriteLine("No academic history found for the student.");
                    return new List<AcademicHistory>(); // Return an empty list if no data
                }
            }
            else
            {
                throw new Exception("Failed to fetch academic history.");
            }
        }

    }
}
