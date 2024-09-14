using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.Tests
{
    /// <summary>
    /// Testklasse til validering af Student-objekter, som arver fra Person-klassen.
    /// Tester navn fra Person samt semester fra Student.
    /// </summary>
    [TestClass()]
    public class StudentTests
    {
        /// <summary>
        /// Tester validering af Student-navn (arvet fra Person-klassen), inkl. null, tomme og gyldige værdier.
        /// </summary>
        [TestMethod()]
        public void ValidateNameTest()
        {
            // Arrange: Opretter en student med null navn (ugyldigt)
            Student studentNullname = new Student
            {
                Id = 1,
                Name = null,  // Invalid name
                Semester = 1
            };

            // Act & Assert: Forventer ArgumentNullException, da navnet er null
            Assert.ThrowsException<ArgumentNullException>(
                () => studentNullname.ValidateName());

            // Arrange: Opretter en student med tomt navn (ugyldigt)
            Student studentEmptyName = new Student
            {
                Id = 2,
                Name = "",  // Invalid name
                Semester = 2
            };

            // Act & Assert: Forventer ArgumentOutOfRangeException, da navnet er tomt
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => studentEmptyName.ValidateName());

            // Arrange: Opretter en student med gyldigt navn
            Student studentValidName = new Student
            {
                Id = 3,
                Name = "Tan", // Valid name
                Semester = 3
            };

            // Act: Validerer det gyldige navn (Person's ValidateName())
            studentValidName.ValidateName();

            // Assert: Bekræfter at navnet stadig er korrekt
            Assert.AreEqual("Tan", studentValidName.Name);
        }

        /// <summary>
        /// Tester validering af Student-semester, inkl. for lave, for høje og gyldige værdier.
        /// </summary>
        [TestMethod()]
        public void ValidateSemesterTest()
        {
            // Arrange: Opretter en student med semester 0 (ugyldigt)
            Student studentLowSemester = new Student
            {
                Id = 4,
                Name = "Student4",
                Semester = 0  // Invalid semester
            };

            // Act & Assert: Forventer ArgumentOutOfRangeException, da semester er for lavt
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => studentLowSemester.ValidateSemester());

            // Arrange: Opretter en student med semester 9 (ugyldigt)
            Student studentHighSemester = new Student
            {
                Id = 5,
                Name = "Student5",
                Semester = 9 // Invalid semester
            };

            // Act & Assert: Forventer ArgumentOutOfRangeException, da semester er for højt
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => studentHighSemester.ValidateSemester());

            // Arrange: Opretter en student med semester 1 (gyldigt)
            Student studentValidSemester1 = new Student
            {
                Id = 6,
                Name = "Student6",
                Semester = 1 // Valid semester
            };

            // Act: Validerer det gyldige semester
            studentValidSemester1.ValidateSemester();

            // Assert: Bekræfter at semester er korrekt
            Assert.AreEqual(1, studentValidSemester1.Semester);


            // Arrange: Opretter en student med semester 8 (gyldigt)
            Student studentValidSemester8 = new Student
            {
                Id = 7,
                Name = "Student7",
                Semester = 8
            };

            // Act: Validerer det gyldige semester
            studentValidSemester8.ValidateSemester();

            // Assert: Bekræfter at semester er korrekt
            Assert.AreEqual(8, studentValidSemester8.Semester);
        }

        /// <summary>
        /// Tester samlet validering af både navn (fra Person) og semester (fra Student).
        /// </summary>
        [TestMethod()]
        public void ValidateTest()
        {
            // Arrange: Opretter en gyldig student
            Student validStudent = new Student
            {
                Id = 8,
                Name = "Student8", // Valid name
                Semester = 5 // Valid semester
            };

            // Act & Assert: Validerer den gyldige student
            validStudent.Validate();

            // Arrange: Opretter en student med null navn og gyldigt semester
            Student invalidStudent = new Student
            {
                Id = 9,
                Name = null, // Invalid name
                Semester = 7 // Valid semester
            };

            // Act & Assert: Forventer ArgumentNullException på grund af null navn (fra Person's Validate())
            Assert.ThrowsException<ArgumentNullException>(() => invalidStudent.Validate());

            // Arrange: Opretter en student med både ugyldigt navn og semester
            Student invalidStudent2 = new Student
            {
                Id = 10,
                Name = "", // Invalid name
                Semester = 0 // Invalid semester
            };

            // Act & Assert: Forventer ArgumentOutOfRangeException
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => invalidStudent2.Validate());
        }

        /// <summary>
        /// Tester Student-klassens ToString metode for korrekt format.
        /// </summary>
        [TestMethod()]
        public void TostringTest()
        {
            // Arrange: Opretter en student
            Student student = new Student
            {
                Id = 11,
                Name = "Frank",
                Semester = 2
            };

            // Act: Kalder ToString-metoden på student-objektet
            string result = student.ToString();

            // Assert: Sammenligner resultatet med det forventede output
            Assert.AreEqual("11, Frank, Semester: 2", result);
        }
    }
}