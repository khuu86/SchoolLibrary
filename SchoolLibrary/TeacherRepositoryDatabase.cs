using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary
{
    public class TeacherRepositoryDatabase : ITeacherRepository
    {

        private readonly TeacherDbContext _context;

        public TeacherRepositoryDatabase(TeacherDbContext dbContext)
        {
            _context = dbContext;
        }

        public Teacher AddTeacher(Teacher teacher)
        {
            teacher.Validate();
            teacher.Id = 0;
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return teacher;
        }

        public IEnumerable<Teacher> Get(int? minSalary = null, string? name = null, string? sortBy = null)
        {
            //List<Movie> result = _context.Movies.ToList();
            IQueryable<Teacher> query = _context.Teachers.ToList().AsQueryable();
            // Copy ToList()
            if (minSalary != null)
            {
                query = query.Where(m => m.Salary > minSalary);
            }
            if (name != null)
            {
                query = query.Where(m => m.Name.Contains(name));
            }
            if (sortBy != null)
            {
                sortBy = sortBy.ToLower();
                switch (sortBy)
                {
                    case "name":
                    case "name_asc":
                        query = query.OrderBy(m => m.Name);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(m => m.Name);
                        break;
                    case "salary":
                        query = query.OrderBy(m => m.Salary);
                        break;
                    case "salary_desc":
                        query = query.OrderByDescending(m => m.Salary);
                        break;
                    default:
                        break; // do nothing
                        //throw new ArgumentException("Unknown sort order: " + orderBy);
                }
            }
            return query;
        }

        public Teacher? GetById(int id)
        {
            return _context.Teachers.FirstOrDefault(m => m.Id == id);
        }

        public Teacher? Remove(int id)
        {
            Teacher? teacher = GetById(id);
            if (teacher == null)
            {
                return null;
            }
            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
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
            _context.SaveChanges();
            return existingTeacher;
        }



    }
}

