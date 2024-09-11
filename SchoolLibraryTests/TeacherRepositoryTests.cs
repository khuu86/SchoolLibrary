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
    public class TeacherRepositoryTests
    {
        [TestMethod()]
        public void AddTeacherTest()
        {
            // Arrange
            TeacherRepository repository = new TeacherRepository();
            Teacher teacher1 = new Teacher() { Name = "Tan", Salary = 200 };
            Teacher teacher2 = new Teacher() { Name = "Zimon", Salary = 200 };
            // Act
            repository.AddTeacher(teacher1);
            repository.AddTeacher(teacher2);

            // Assert
            Assert.AreEqual(1, teacher1.Id);
            Assert.AreEqual(2, teacher2.Id);

            // Assert: Testing how big the list is
            Assert.AreEqual(2, repository.Get().Count());



            // Arrange: Testing negative salary
            Teacher teacherNegSalary = new Teacher() { Name = "Zimon", Salary = -200 };
            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => repository.AddTeacher(teacherNegSalary));


            // Arrange: Testing null name
            Teacher teacherNullName = new Teacher() { Name = null, Salary = 100 };
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => repository.AddTeacher(teacherNullName));
        }

        [TestMethod()]
        public void GetTest()
        {
            // Arrange 
            TeacherRepository repository = new TeacherRepository();
            Teacher teacher1 = new Teacher() { Name = "an", Salary = 200 };
            Teacher teacher2 = new Teacher() { Name = "Zimon", Salary = 200 };

            // Act
            repository.AddTeacher(teacher1);
            repository.AddTeacher(teacher2);

            // Assert
            Assert.AreEqual(1, repository.Get(1)?.Id);
            Assert.AreEqual(2, repository.Get(2)?.Id);
            Assert.IsNull(repository.Get(3)?.Id);

        }

        [TestMethod()]
        public void GetFilterTest()
        {
            // Arrange Testing Get(name) Filtering System: 
            TeacherRepository repository = new TeacherRepository();
            Teacher teacher3 = new Teacher() { Name = "Tan", Salary = 100 };
            Teacher teacher4 = new Teacher() { Name = "Jonas", Salary = 111 };
            Teacher teacher5 = new Teacher() { Name = "Jonas", Salary = 111 };
            Teacher teacher6 = new Teacher() { Name = "Zimon", Salary = 111 };
            repository.AddTeacher(teacher3);
            repository.AddTeacher(teacher4);
            repository.AddTeacher(teacher5);
            repository.AddTeacher(teacher6);

            // Act
            List<Teacher> tanList = repository.Get(name: "Jonas").ToList();
            List<Teacher> salaryList111 = repository.Get(minSalary: 111).ToList();
            // Assert 
            Assert.AreEqual(2, tanList.Count);
            Assert.AreEqual(3, salaryList111.Count);


        }

        [TestMethod()]
        public void RemoveTest()
        {
            // Arrange 
            TeacherRepository repository = new TeacherRepository();
            Teacher teacher1 = new Teacher() { Name = "Tan", Salary = 200 };
            Teacher teacher2 = new Teacher() { Name = "Zimon", Salary = 200 };
            Teacher teacher3 = new Teacher() { Name = "Rasmus", Salary = 200 };

            // Act
            repository.AddTeacher(teacher1);
            repository.AddTeacher(teacher2);
            repository.AddTeacher(teacher3);
            repository.Remove(1);

            // Assert
            Assert.AreEqual(2, repository.Get().Count());
            Assert.IsNull(repository.Remove(117));
        }

        [TestMethod()]

        public void UpdateTest()
        {
            // Arrange
            TeacherRepository repository = new TeacherRepository();
            Teacher teacher1 = new Teacher() { Name = "Tan", Salary = 200 };
            Teacher data1 = new Teacher() { Name = "Zimon", Salary = 150 };

            // Act
            repository.AddTeacher(teacher1);
            Teacher updated = repository.Update(1, data1);

            // Assert
            Assert.AreEqual("Zimon", updated.Name);
            Assert.AreEqual(150, updated.Salary);
            Assert.IsNull(repository.Update(114, data1));
        }
    }
}