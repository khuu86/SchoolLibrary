
namespace SchoolLibrary
{
    public interface ITeacherRepository
    {
        Teacher AddTeacher(Teacher teacher);
        IEnumerable<Teacher> Get(int? minSalary = null, string? name = null, string? sortBy = null);
        Teacher? GetById(int id);
        Teacher? Remove(int id);
        Teacher? Update(int id, Teacher data);
    }
}