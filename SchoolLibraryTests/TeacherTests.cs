using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.Tests
{
    /// <summary>
    /// Testklasse til validering af Teacher-objekter, som arver fra Person-klassen.
    /// Tester navn fra Person, løn og klasser fra Teacher.
    /// </summary>
    [TestClass()]
    public class TeacherTests
    {
        /// <summary>
        /// Tester validering af Teacher-løn, inkl. positive og negative værdier.
        /// </summary>
        [TestMethod()]
        public void ValidateSalaryTest()
        {
            // Arrange: Opretter en teacher med positiv løn (gyldig)
            Teacher teacherPositiveSalary = new Teacher
            {
                Name = "Tan",
                Salary = 100 // Valid salary
            };

            // Act: Validerer den gyldige løn
            teacherPositiveSalary.ValidateSalary();

            // Assert: Bekræfter at lønnen er korrekt
            Assert.AreEqual(100, teacherPositiveSalary.Salary);

            // Arrange: Opretter en teacher med negativ løn (ugyldig)
            Teacher teacherNegativeSalary = new Teacher()
            {
                Id = 1,
                Name = "Rasmus",
                Salary = -100 // Invalid salary
            };

            // Act & Assert: Forventer ArgumentOutOfRangeException, da lønnen er negativ
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => teacherNegativeSalary.ValidateSalary());
        }

        /// <summary>
        /// Tester validering af Teacher-navn (arvet fra Person-klassen), inkl. null, tomme og gyldige værdier.
        /// </summary>
        [TestMethod()]
        public void ValidateNameTest()
        {
            // Arrange: Opretter en teacher med null navn (ugyldigt)
            Teacher teacherNullname = new Teacher
            {
                Id = 2,
                Name = null, // Invalid name
                Salary = 50
            };

            // Act & Assert: Forventer ArgumentNullException, da navnet er null
            Assert.ThrowsException<ArgumentNullException>(
                () => teacherNullname.ValidateName());

            // Arrange: Opretter en teacher med tomt navn (ugyldigt)
            Teacher teacherEmptyName = new Teacher
            {
                Id = 3,
                Name = "", // Invalid name
                Salary = 150
            };

            // Act & Assert: Forventer ArgumentOutOfRangeException, da navnet er tomt
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => teacherEmptyName.ValidateName());

            // Arrange: Opretter en teacher med gyldigt navn
            Teacher teacherValidName = new Teacher
            {
                Id = 4,
                Name = "Tan", // Valid name
                Salary = 100
            };

            // Act: Validerer det gyldige navn (Person's ValidateName())
            teacherValidName.ValidateName();

            // Assert: Bekræfter at navnet stadig er korrekt
            Assert.AreEqual("Tan", teacherValidName.Name);
        }

        /// <summary>
        /// Tester samlet validering af både navn (fra Person) og løn (fra Teacher).
        /// </summary>
        [TestMethod()]
        public void ValidateTest()
        {
            // Arrange: Opretter en gyldig teacher
            Teacher validTeacher = new Teacher
            {
                Id = 5,
                Name = "Alex", // Valid name
                Salary = 200 // Valid salary
            };

            // Act & Assert: Validerer den gyldige teacher
            validTeacher.Validate();

            // Arrange: Opretter en teacher med null navn (ugyldigt) og gyldig løn
            Teacher invalidTeacher = new Teacher
            {
                Id = 6,
                Name = null, // Invalid name
                Salary = 200 // Valid salary
            };

            // Act & Assert: Forventer ArgumentNullException på grund af null navn
            Assert.ThrowsException<ArgumentNullException>(() => invalidTeacher.Validate());
        }

        /// <summary>
        /// Tester tilføjelse og hentning af klasser for en Teacher.
        /// </summary>
        [TestMethod()]
        public void ValidateClassesTest()
        {
            // Arrange: Opretter en teacher og tilføjer klasser
            Teacher validClasses = new Teacher()
            {
            };
            validClasses.AddClass("1z");
            validClasses.AddClass("2z");

            // Act: Henter klasserne som en streng
            string classes = validClasses.GetClasses();

            // Assert: Bekræfter at klasserne er korrekt hentet og formatteret
            Assert.AreEqual("1z, 2z", classes);
        }

        /// <summary>
        /// Tester Teacher-klassens ToString-metode for korrekt format.
        /// </summary>
        [TestMethod()]
        public void TostringTest()
        {
            // Arrange: Opretter en teacher
            Teacher teacher1 = new Teacher()
            {
                Id = 7,
                Name = "Rasmus",
                Salary = 100
            };

            // Act: Kalder ToString-metoden på teacher-objektet
            string result = teacher1.ToString();

            // Assert: Bekræfter at resultatet har det forventede format
            Assert.AreEqual("7, Rasmus, 100", result);
        }

    }
}