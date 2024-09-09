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
    public class StudentTests
    {
        [TestMethod()]
        public void ValidateNameTest()
        {
            // Arrange: Test null name
            Student studentNullname = new Student
            {
                Id = 1,
                Name = null,
                Semester = 1
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => studentNullname.ValidateName());

            // Arrange: Test empty name
            Student studentEmptyName = new Student
            {
                Id = 2,
                Name = "",
                Semester = 2
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => studentEmptyName.ValidateName());

            // Arrange: Test valid name 
            Student studentValidName = new Student
            {
                Id = 3,
                Name = "Tan",
                Semester = 3
            };

            // Act
            studentValidName.ValidateName();

            // Assert
            Assert.AreEqual("Tan", studentValidName.Name);
        }

        [TestMethod()]
        public void ValidateSemesterTest()
        {
            // Arrange: Test invalid Semester (too low)
            Student studentLowSemester = new Student
            {
                Id = 4,
                Name = "Student4",
                Semester = 0
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => studentLowSemester.ValidateSemester());

            // Arrange: Test invalid semester (too high)
            Student studentHighSemester = new Student
            {
                Id = 5,
                Name = "Student5",
                Semester = 9
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => studentHighSemester.ValidateSemester());

            // Arrange: Test valid semester1
            Student studentValidSemester1 = new Student
            {
                Id = 6,
                Name = "Student6",
                Semester = 1
            };

            // Act
            studentValidSemester1.ValidateSemester();

            // Assert
            Assert.AreEqual(1, studentValidSemester1.Semester);


            // Arrange: Test valid semester8
            Student studentValidSemester8 = new Student
            {
                Id = 7,
                Name = "Student7",
                Semester = 8
            };

            // Act
            studentValidSemester8.ValidateSemester();

            // Assert
            Assert.AreEqual(8, studentValidSemester8.Semester);
        }

        [TestMethod()]
        public void ValidateTest()
        {
            // Arrange: Valid student
            Student validStudent = new Student
            {
                Id = 8,
                Name = "Student8",
                Semester = 5
            };

            // Act & Assert
            validStudent.Validate();

            // Arrange: Invalid student (null name), Valid semester
            Student invalidStudent = new Student
            {
                Id = 9,
                Name = null,
                Semester = 7
            };

            // Act & Assert: Expect ArgumentNullException first
            Assert.ThrowsException<ArgumentNullException>(() => invalidStudent.Validate());


        }

        [TestMethod()]
        public void TostringTest()
        {
            // Arrange
            Student student = new Student
            {
                Id = 10,
                Name = "Frank",
                Semester = 2
            };

            // Act
            string result = student.ToString();

            // Assert
            Assert.AreEqual("10, Frank, Semester: 2", result);
        }
    }
}