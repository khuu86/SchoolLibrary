using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.Tests
{
    [TestClass()]
    public class TeacherTests
    {
        [TestMethod()]
        public void ValidateSalaryTest()
        {
            // Arrange
            Teacher teacherPositiveSalary = new Teacher
            {
                Name = "Tan",
                Salary = 100
            };

            // Act
            teacherPositiveSalary.ValidateSalary();

            // Assert 
            Assert.AreEqual(100, teacherPositiveSalary.Salary);

            // Arrange for negative salary
            Teacher teacherNegativeSalary = new Teacher()
            {
                Id = 1,
                Name = "Rasmus",
                Salary = -100
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => teacherNegativeSalary.ValidateSalary());
        }

        [TestMethod()]
        public void ValidateNameTest()
        {
            // Arrange: Test null name
            Teacher teacherNullname = new Teacher
            {
                Id = 2,
                Name = null,
                Salary = 50
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => teacherNullname.ValidateName());

            // Arrange: Test empty name
            Teacher teacherEmptyName = new Teacher
            {
                Id = 3,
                Name = "",
                Salary = 150
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => teacherEmptyName.ValidateName());

            // Arrange: Test valid name 
            Teacher teacherValidName = new Teacher
            {
                Id = 4,
                Name = "Tan",
                Salary = 100
            };

            // Act
            teacherValidName.ValidateName();

            // Assert
            Assert.AreEqual("Tan", teacherValidName.Name);
        }

        [TestMethod()]
        public void ValidateTest()
        {
            // Arrange: Valid teacher
            Teacher validTeacher = new Teacher
            {
                Id = 5,
                Name = "Alex",
                Salary = 200
            };

            // Act & Assert
            validTeacher.Validate();

            // Arrange: Invalid teacher (null name and negative salary)
            Teacher invalidTeacher = new Teacher
            {
                Id = 6,
                Name = null,
                Salary = 200
            };

            // Act & Assert: Expect exceptions
            Assert.ThrowsException<ArgumentNullException>(() => invalidTeacher.Validate());
        }

        [TestMethod()]
        public void ValidateClasses()
        {
            // Arrange: Add Class

            Teacher validClasses = new Teacher
            {
            };
            validClasses.AddClass("1z");
            validClasses.AddClass("2z");
            string classes = validClasses.GetClasses();


            // Arrange: Expect Result, Actual Result
            Assert.AreEqual("1z, 2z", classes);
        }

        [TestMethod()]

        public void TostringTest()
        {
            // Arrange
            Teacher teacher1 = new Teacher()
            {
                Id = 7,
                Name = "Rasmus",
                Salary = 100
            };

            // Act
            string result = teacher1.ToString();

            // Assert
            Assert.AreEqual("7, Rasmus, 100", result);
        }

    }
}