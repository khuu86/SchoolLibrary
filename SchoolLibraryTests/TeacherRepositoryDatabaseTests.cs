using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.Tests
{
    [TestClass()]
    public class TeacherRepositoryDatabaseTests
    {
        private const bool useDatabase = true;
        private static TeacherDbContext _context;
        private static ITeacherRepository _teacherRepository;
        // https://learn.microsoft.com/en-us/dotnet/core/testing/order-unit-tests?pivots=mstest
        private readonly Teacher _teacher1 = new() { Name = "John Doe", Salary = 1000 };


        [ClassInitialize]
        public static void InitOnce(TestContext context)
        {
            if (useDatabase)
            {
                var optionsBuilder = new DbContextOptionsBuilder<TeacherDbContext>();
                // https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets
                optionsBuilder.UseSqlServer(Secret.ConnectionString);
                // connection string structure
                //   "Data Source=mssql7.unoeuro.com;Initial Catalog=FROM simply.com;Persist Security Info=True;User ID=FROM simply.com;Password=DB PASSWORD FROM simply.com;TrustServerCertificate=True"

                TeacherDbContext _context = new TeacherDbContext(optionsBuilder.Options);

                // clean database table: remove all rows
                _context.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Teachers");
                _teacherRepository = new TeacherRepositoryDatabase(_context);
            }
            else
            {
                _teacherRepository = new TeacherRepository();
            }
        }

        [TestMethod, Priority(1)]
        [DoNotParallelize]
        public void AddTeacherTest()
        {

            // Act
            _teacherRepository.AddTeacher(new Teacher { Name = "Z", Salary = 200 });
            Teacher tan = _teacherRepository.AddTeacher(new Teacher { Name = "Tan", Salary = 300 });

            // Assert
            Assert.IsTrue(tan.Id >= 0);
            IEnumerable<Teacher> all = _teacherRepository.Get();
            Assert.AreEqual(2, all.Count());

            Assert.ThrowsException<ArgumentNullException>(
                () => _teacherRepository.AddTeacher(new Teacher { Name = null, Salary = 200 }));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () =>  _teacherRepository.AddTeacher(new Teacher { Name = "", Salary = 250 }));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => _teacherRepository.AddTeacher(new Teacher { Name = "Z", Salary = -1 }));
        }
    }
}
