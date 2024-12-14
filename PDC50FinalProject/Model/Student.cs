using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDC50FinalProject.Model
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string AttendanceRecords { get; set; }
        public string Course { get; set; }
        public string YearLevel { get; set; }

        public ObservableCollection<AcademicHistory> AcademicHistory { get; set; } = new ObservableCollection<AcademicHistory>();

    }


}
