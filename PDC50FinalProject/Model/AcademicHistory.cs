using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDC50FinalProject.Model
{
   public class AcademicHistory
    {
        public int ID { get; set; }
        public int StudentID {  get; set; } //foreign key
        public string Course { get; set; }
        public string yearLevel { get; set; }

    }
}
