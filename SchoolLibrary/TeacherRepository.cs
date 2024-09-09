using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary
{
    public class TeacherRepository
    {
        private int nextId = 1;
        private List<Teacher> teachers = new List<Teacher>();

        public void AddTeacher(Teacher teacher)
        {
            teacher.Id = nextId++;
            teachers.Add(teacher);
        }
        public List<Teacher> Get()
        {
            // Copy constructor)
            return new List<Teacher>(teachers);
        }

        public Teacher? Get(int id)
        {
            return teachers.FirstOrDefault(t => t.Id == id);
        }

        public Teacher? Remove(int id)
        {
            Teacher? teacher = Get(id);
            if (teacher == null)
            {
                return null;
            }
            teachers.Remove(teacher);
            return teacher;
        }

        public Teacher? Update(int id, Teacher data)
        {
            Teacher? existingTeacher = Get(id);
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
