using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDC50FinalProject.Model;
using PDC50FinalProject.Services;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Newtonsoft.Json;

namespace PDC50FinalProject.ViewModel
{
    public class StudentViewModel : BindableObject
    {
        private readonly StudentService _studentService;
        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<Student> FilteredStudents { get; set; }
        public ObservableCollection<string> Courses { get; } = new ObservableCollection<string>
        {
            "BSIT", "BSCS", "BMMA"
        };

        public ObservableCollection<string> YearLevels { get; } = new ObservableCollection<string>
        {
            "1st Year", "2nd Year", "3rd Year", "4th Year"
        };

        private ObservableCollection<AcademicHistory> _academicHistory;
        public ObservableCollection<AcademicHistory> AcademicHistory
        {
            get => _academicHistory;
            set
            {
                _academicHistory = value;
                OnPropertyChanged();
            }
        }

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                if (_selectedStudent != null && _selectedStudent.AcademicHistory == null)
                {
                    _selectedStudent.AcademicHistory = new ObservableCollection<AcademicHistory>();
                }
                OnPropertyChanged();
                UpdateEntryField();
            }
        }

        private string _selectedCourse;
        public string SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                CourseInput = value;
                OnPropertyChanged();
            }
        }

        private string _selectedYearLevel;
        public string SelectedYearLevel
        {
            get => _selectedYearLevel;
            set
            {
                _selectedYearLevel = value;
                YearInput = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCourseFilter;
        public string SelectedCourseFilter
        {
            get => _selectedCourseFilter;
            set
            {
                _selectedCourseFilter = value;
                OnPropertyChanged();
            }
        }

        private string _selectedYearLevelFilter;
        public string SelectedYearLevelFilter
        {
            get => _selectedYearLevelFilter;
            set
            {
                _selectedYearLevelFilter = value;
                OnPropertyChanged();
            }
        }

        private string _nameInput;
        public string NameInput
        {
            get => _nameInput;
            set
            {
                _nameInput = value;
                OnPropertyChanged();
            }
        }

        private string _genderInput;
        public string GenderInput
        {
            get => _genderInput;
            set
            {
                _genderInput = value;
                OnPropertyChanged();
            }
        }

        private string _contactNoInput;
        public string ContactNoInput
        {
            get => _contactNoInput;
            set
            {
                _contactNoInput = value;
                OnPropertyChanged();
            }
        }

        private string _emailInput;
        public string EmailInput
        {
            get => _emailInput;
            set
            {
                _emailInput = value;
                OnPropertyChanged();
            }
        }

        private string _courseInput;
        public string CourseInput
        {
            get => _courseInput;
            set
            {
                _courseInput = value;
                OnPropertyChanged();
            }
        }

        private string _yearInput;
        public string YearInput
        {
            get => _yearInput;
            set
            {
                _yearInput = value;
                OnPropertyChanged();
            }
        }

        private string _attendanceInput;
        public string AttendanceInput
        {
            get => _attendanceInput;
            set
            {
                _attendanceInput = value;
                OnPropertyChanged();
            }
        }

        private void ClearInput()
        {
            NameInput = string.Empty;
            GenderInput = string.Empty;
            ContactNoInput = string.Empty;
            EmailInput = string.Empty;
            AttendanceInput = string.Empty;
            CourseInput = string.Empty;
            YearInput = string.Empty;
        }

        private void UpdateEntryField()
        {
            if (SelectedStudent == null)
            {
                ClearInput();
                return;
            }

            NameInput = SelectedStudent.Name;
            GenderInput = SelectedStudent.Gender;
            ContactNoInput = SelectedStudent.ContactNo;
            EmailInput = SelectedStudent.Email;
            CourseInput = SelectedStudent.Course;
            YearInput = SelectedStudent.YearLevel;
        }

        public StudentViewModel()
        {
            _studentService = new StudentService();
            Students = new ObservableCollection<Student>();
            FilteredStudents = new ObservableCollection<Student>(Students);
            LoadStudentCommand = new Command(async () => await LoadStudents());
            AddStudentCommand = new Command(async () => await AddStudents());
            UpdateStudentCommand = new Command(async () => await UpdateStudents());
            DeleteStudentCommand = new Command(async () => await DeleteStudents());
            LoadAcademicHistoryCommand = new Command(async () => await LoadAcademicHistoryAsync());
            FilterStudentsCommand = new Command(FilterStudents);
            ClearFilterCommand = new Command(ClearFilters);
        }

        public ICommand LoadStudentCommand { get; }
        public ICommand AddStudentCommand { get; }
        public ICommand UpdateStudentCommand { get; }
        public ICommand DeleteStudentCommand { get; }
        public ICommand FilterStudentsCommand { get; }
        public ICommand ClearFilterCommand { get; }
        public ICommand LoadAcademicHistoryCommand { get; }

        public async Task LoadStudents()
        {
            Students.Clear();
            FilteredStudents.Clear();

            var students = await _studentService.GetStudentAsync();
            foreach (var student in students)
            {
                Students.Add(student);
                FilteredStudents.Add(student);
            }
            ClearInput();
        }

        private async Task AddStudents()
        {
            if (!ValidateInputs()) return;

            var newStudent = new Student
            {
                Name = NameInput,
                Gender = GenderInput,
                ContactNo = ContactNoInput,
                Email = EmailInput,
                AttendanceRecords = AttendanceInput,
                Course = CourseInput,
                YearLevel = YearInput
            };

            var result = await _studentService.AddStudentsAsync(newStudent);
            if (result.Equals("Success", StringComparison.OrdinalIgnoreCase))
            {
                await LoadStudents();
                ClearInput();
            }
        }

        private async Task UpdateStudents()
        {
            if (SelectedStudent == null || !ValidateInputs()) return;

            // Ensure AcademicHistory is initialized as ObservableCollection
            if (SelectedStudent.AcademicHistory == null)
            {
                SelectedStudent.AcademicHistory = new ObservableCollection<AcademicHistory>();
            }

            var previousCourse = SelectedStudent.Course;
            var previousYearLevel = SelectedStudent.YearLevel;

            SelectedStudent.Name = NameInput;
            SelectedStudent.Gender = GenderInput;
            SelectedStudent.ContactNo = ContactNoInput;
            SelectedStudent.Email = EmailInput;
            SelectedStudent.Course = CourseInput;
            SelectedStudent.YearLevel = YearInput;

            // Check if the course or year level has changed
            if (previousCourse != CourseInput || previousYearLevel != YearInput)
            {
                // Add old course and year to AcademicHistory
                SelectedStudent.AcademicHistory.Add(new AcademicHistory
                {
                    Course = previousCourse,
                    yearLevel = previousYearLevel
                });
            }

            var result = await _studentService.UpdateStudentsAsync(SelectedStudent);
            if (result.Equals("Success", StringComparison.OrdinalIgnoreCase))
            {
                await LoadStudents();
                ClearInput();
            }
        }


        private async Task DeleteStudents()
        {
            if (SelectedStudent == null) return;

            var result = await _studentService.DeleteUsersAsync(SelectedStudent.ID);
            if (result.Equals("Success", StringComparison.OrdinalIgnoreCase))
            {
                await LoadStudents();
                ClearInput();
            }
        }

        private void FilterStudents()
        {
            var filtered = Students.AsEnumerable();

            FilteredStudents.Clear();
            foreach (var student in filtered)
            {
                FilteredStudents.Add(student);
            }
        }

        private void ClearFilters()
        {
            SelectedCourseFilter = null;
            SelectedYearLevelFilter = null;

            FilteredStudents.Clear();
            foreach (var student in Students)
            {
                FilteredStudents.Add(student);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(NameInput) || string.IsNullOrWhiteSpace(GenderInput) ||
                string.IsNullOrWhiteSpace(ContactNoInput) || string.IsNullOrWhiteSpace(EmailInput) ||
                string.IsNullOrWhiteSpace(CourseInput) || string.IsNullOrWhiteSpace(YearInput))
            {
                App.Current.MainPage.DisplayAlert("Error", "Please fill in all fields.", "OK");
                return false;
            }
            return true;
        }

        public async Task LoadAcademicHistoryAsync()
        {
            if (SelectedStudent == null) return;

            try
            {
                Debug.WriteLine($"Loading academic history for student ID: {SelectedStudent.ID}");

                var history = await _studentService.GetAcademicHistoryAsync(SelectedStudent.ID);

                // Verify if history is coming in properly
                Debug.WriteLine($"Received academic history: {JsonConvert.SerializeObject(history)}");

                // Ensure the AcademicHistory collection is initialized and populated
                if (SelectedStudent.AcademicHistory == null)
                {
                    SelectedStudent.AcademicHistory = new ObservableCollection<AcademicHistory>();
                }

                // Clear the collection and add new items
                SelectedStudent.AcademicHistory.Clear();
                foreach (var academic in history)
                {
                    SelectedStudent.AcademicHistory.Add(academic);
                }

                // Notify that the AcademicHistory has been updated
                OnPropertyChanged(nameof(SelectedStudent.AcademicHistory));

                // Optionally, check count
                Debug.WriteLine($"AcademicHistory count after update: {SelectedStudent.AcademicHistory.Count}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading academic history: {ex.Message}");
            }
        }

    }
}
