using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary
{
    public class TeacherRepository : ITeacherRepository
    {
        private int nextId = 1;
        private List<Teacher> teachers = new List<Teacher>();

        public Teacher AddTeacher(Teacher teacher)
        {
            teacher.Validate();
            teacher.Id = nextId++;
            teachers.Add(teacher);
            return teacher;
        }

        // Laver kopi af listen 
        public IEnumerable<Teacher> Get(int? minSalary = null,
            string? name = null,
            string? sortBy = null)
        {
            // Copy constructor)
            List<Teacher> result = new List<Teacher>(teachers);
            if (minSalary != null)
            {
                //result = result.Where(t => t.Salary >= minSalary);
                result = result.FindAll(t => t.Salary >= minSalary);
            }
            if (name != null)
            {
                //result = result.Where(t => t.Name == name);
                result = result.FindAll(t => t.Name == name);
            }
            if (sortBy != null)
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        result.Sort((t1, t2) => t1.Name.CompareTo(t2.Name));
                        break;
                    case "namedesc":
                        result.Sort((t1, t2) => t2.Name.CompareTo(t1.Name));
                        break;
                    case "salary":
                        result.Sort((t1, t2) => t1.Salary - t2.Salary);
                        break;
                    default:
                        throw new ArgumentException($"Unknown sort field: {sortBy}");
                }
            }
            return result;


        }

        public Teacher? GetById(int id)
        {
            return teachers.FirstOrDefault(t => t.Id == id);
        }

        public Teacher? Remove(int id)
        {
            Teacher? teacher = GetById(id);
            if (teacher == null)
            {
                return null;
            }
            teachers.Remove(teacher);
            return teacher;
        }

        public Teacher? Update(int id, Teacher data)
        {
            data.Validate();
            Teacher? existingTeacher = GetById(id);
            if (existingTeacher == null)
            {
                return null;
            }
            existingTeacher.Name = data.Name;
            existingTeacher.Salary = data.Salary;
            return existingTeacher;
        }
    }
}
