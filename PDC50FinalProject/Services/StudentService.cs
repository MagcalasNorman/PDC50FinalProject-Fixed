using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDC50FinalProject.Model;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

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

        public async Task<List<Student>>GetStudentAsync()
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

        //public async Task<List<AcademicHistory>> GetAcademicHistoryAsync(int studentId)
        //{
        //    var url = $"{BaseUrl}/get_academic_history.php"; // Ensure BaseUrl is defined in your service
        //    var payload = JsonConvert.SerializeObject(new { studentId });

        //    var response = await _httpClient.PostAsync(url, new StringContent(payload, Encoding.UTF8, "application/json"));

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<List<AcademicHistory>>(json) ?? new List<AcademicHistory>();
        //    }
        //    else
        //    {
        //        throw new Exception("Failed to fetch academic history.");
        //    }
        //}

    }
}
