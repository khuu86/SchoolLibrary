using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary
{
    public class Student : Person
    {
        public int Semester { get; set; }

        // Override ToString() method
        public override string ToString()
        {
            return $"{Id}, {Name}, Semester: {Semester}";
        }

        // General validator method that calls both validators

        public void ValidateSemester()
        {
            if (Semester < 1 || Semester > 8)
            {
                throw new ArgumentOutOfRangeException($"Semester must be between 1 and 8 {Semester}");
            }
        }
        public override void Validate()
        {
            ValidateSemester();
            base.Validate();
        }

    }
}
