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
    /// Testklasse for TeacherRepository, der håndterer CRUD-operationer for Teacher-objekter.
    /// Tester tilføjelse, fjernelse, opdatering og hentning af lærere, inkl. filtrering og sortering.
    /// </summary>
    [TestClass()]
    public class TeacherRepositoryTests
    {
        /// <summary>
        /// Tester tilføjelse af Teacher-objekter i repository, herunder ID-generation og validering af data.
        /// </summary>
        [TestMethod()]
        public void AddTeacherTest()
        {
            // Arrange: Opretter repository og lærere
            TeacherRepository repository = new TeacherRepository();
            Teacher teacher1 = new Teacher() { Name = "Tan", Salary = 200 };
            Teacher teacher2 = new Teacher() { Name = "Zimon", Salary = 200 };

            // Act: Tilføjer lærere til repository
            repository.AddTeacher(teacher1);
            repository.AddTeacher(teacher2);

            // Assert: Kontrollerer at ID'erne tildeles korrekt
            Assert.AreEqual(1, teacher1.Id);
            Assert.AreEqual(2, teacher2.Id);

            // Assert: Kontrollerer at der er 2 lærere i listen
            Assert.AreEqual(2, repository.Get().Count());

            // Arrange: Test for negativ løn
            Teacher teacherNegSalary = new Teacher() { Name = "Zimon", Salary = -200 };
            // Act & Assert: Forventer ArgumentOutOfRangeException pga. negativ løn
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => repository.AddTeacher(teacherNegSalary));

            // Arrange: Test for null navn
            Teacher teacherNullName = new Teacher() { Name = null, Salary = 100 };
            // Act & Assert: Forventer ArgumentNullException pga. null navn
            Assert.ThrowsException<ArgumentNullException>(() => repository.AddTeacher(teacherNullName));
        }

        /// <summary>
        /// Tester hentning af Teacher baseret på ID.
        /// </summary>
        [TestMethod()]
        public void GetTest()
        {
            // Arrange: Opretter repository og tilføjer lærere
            TeacherRepository repository = new TeacherRepository();
            Teacher teacher1 = new Teacher() { Name = "Tan", Salary = 200 };
            Teacher teacher2 = new Teacher() { Name = "Zimon", Salary = 200 };

            // Act: Tilføjer lærere og henter dem baseret på ID
            repository.AddTeacher(teacher1);
            repository.AddTeacher(teacher2);

            // Assert: Kontrollerer at lærerne hentes korrekt baseret på ID
            Assert.AreEqual(1, repository.Get(1)?.Id);
            Assert.AreEqual(2, repository.Get(2)?.Id);
            Assert.IsNull(repository.Get(3)?.Id); // Kontrollerer for ugyldigt ID
        }

        /// <summary>
        /// Tester filtrering af lærere baseret på navn og minimumsløn.
        /// </summary>
        [TestMethod()]
        public void GetFilterTest()
        {
            // Arrange: Tilføjer lærere til repository for filtrering
            TeacherRepository repository = new TeacherRepository();
            Teacher teacher3 = new Teacher() { Name = "Tan", Salary = 100 };
            Teacher teacher4 = new Teacher() { Name = "Jonas", Salary = 111 };
            Teacher teacher5 = new Teacher() { Name = "Jonas", Salary = 111 };
            Teacher teacher6 = new Teacher() { Name = "Zimon", Salary = 111 };
            repository.AddTeacher(teacher3);
            repository.AddTeacher(teacher4);
            repository.AddTeacher(teacher5);
            repository.AddTeacher(teacher6);

            // Act: Filtrerer på navn og minimumsløn
            List<Teacher> tanList = repository.Get(name: "Jonas").ToList();
            List<Teacher> salaryList111 = repository.Get(minSalary: 111).ToList();

            // Assert: Kontrollerer korrekt filtrering af lærere baseret på navn og løn
            Assert.AreEqual(2, tanList.Count); // To lærere med navnet Jonas
            Assert.AreEqual(3, salaryList111.Count); // Tre lærere med løn >= 111
        }

        /// <summary>
        /// Tester sortering af lærere med en ugyldig sorteringsfelt. 
        /// Forventer en ArgumentException, da sorteringsfeltet ikke er gyldigt.
        /// </summary>
        [TestMethod()]
        public void GetSortByTest()
        {
            // Arrange: Opretter repository og tilføjer en lærer
            TeacherRepository repository = new TeacherRepository();
            Teacher teacher = new Teacher() { Name = "Tan", Salary = 200 };
            repository.AddTeacher(teacher);

            // Act & Assert: Forventer ArgumentException for et ugyldigt sorteringsfelt
            Assert.ThrowsException<ArgumentException>(() => repository.Get(sortBy: "invalidSortField"));
        }

        /// <summary>
        /// Tester sortering efter navn i stigende orden.
        /// </summary>
        [TestMethod()]
        public void GetSortByNameTest()
        {
            // Arrange: Tilføjer lærere til repository
            TeacherRepository repository = new TeacherRepository();
            Teacher teacher1 = new Teacher() { Name = "Tan", Salary = 200 };
            Teacher teacher2 = new Teacher() { Name = "Zimon", Salary = 150 };
            Teacher teacher3 = new Teacher() { Name = "Rasmus", Salary = 180 };

            repository.AddTeacher(teacher1);
            repository.AddTeacher(teacher2);
            repository.AddTeacher(teacher3);

            // Act: Sorterer lærere efter navn i stigende rækkefølge
            var sortedByName = repository.Get(sortBy: "name").ToList();

            // Assert: Kontrollerer at lærerne er sorteret korrekt efter navn
            Assert.AreEqual("Rasmus", sortedByName[0].Name);
            Assert.AreEqual("Tan", sortedByName[1].Name);
            Assert.AreEqual("Zimon", sortedByName[2].Name);
        }

        /// <summary>
        /// Tester sortering efter navn i faldende orden.
        /// </summary>
        [TestMethod()]
        public void GetSortByNameDescTest()
        {
            // Arrange: Tilføjer lærere til repository
            TeacherRepository repository = new TeacherRepository();
            Teacher teacher1 = new Teacher() { Name = "Tan", Salary = 200 };
            Teacher teacher2 = new Teacher() { Name = "Zimon", Salary = 150 };
            Teacher teacher3 = new Teacher() { Name = "Rasmus", Salary = 180 };

            repository.AddTeacher(teacher1);
            repository.AddTeacher(teacher2);
            repository.AddTeacher(teacher3);

            // Act: Sorterer lærere efter navn i faldende rækkefølge
            var sortedByNameDesc = repository.Get(sortBy: "namedesc").ToList();

            // Assert: Kontrollerer at lærerne er sorteret korrekt i faldende rækkefølge
            Assert.AreEqual("Zimon", sortedByNameDesc[0].Name);
            Assert.AreEqual("Tan", sortedByNameDesc[1].Name);
            Assert.AreEqual("Rasmus", sortedByNameDesc[2].Name);
        }

        /// <summary>
        /// Tester sortering efter løn i stigende rækkefølge.
        /// </summary>
        [TestMethod()]
        public void GetSortBySalaryTest()
        {
            // Arrange: Tilføjer lærere til repository
            TeacherRepository repository = new TeacherRepository();
            Teacher teacher1 = new Teacher() { Name = "Tan", Salary = 200 };
            Teacher teacher2 = new Teacher() { Name = "Zimon", Salary = 150 };
            Teacher teacher3 = new Teacher() { Name = "Rasmus", Salary = 180 };

            repository.AddTeacher(teacher1);
            repository.AddTeacher(teacher2);
            repository.AddTeacher(teacher3);

            // Act: Sorterer lærere efter løn i stigende rækkefølge
            var sortedBySalary = repository.Get(sortBy: "salary").ToList();

            // Assert: Kontrollerer at lærerne er sorteret korrekt efter løn
            Assert.AreEqual(150, sortedBySalary[0].Salary);
            Assert.AreEqual(180, sortedBySalary[1].Salary);
            Assert.AreEqual(200, sortedBySalary[2].Salary);
        }

        /// <summary>
        /// Tester fjernelse af lærere fra repository.
        /// </summary>
        [TestMethod()]
        public void RemoveTest()
        {
            // Arrange: Tilføjer lærere til repository
            TeacherRepository repository = new TeacherRepository();
            Teacher teacher1 = new Teacher() { Name = "Tan", Salary = 200 };
            Teacher teacher2 = new Teacher() { Name = "Zimon", Salary = 200 };
            Teacher teacher3 = new Teacher() { Name = "Rasmus", Salary = 200 };

            repository.AddTeacher(teacher1);
            repository.AddTeacher(teacher2);
            repository.AddTeacher(teacher3);

            // Act: Fjerner en lærer fra repository
            repository.Remove(1);

            // Assert: Kontrollerer at læreren blev fjernet korrekt, og at listen er reduceret
            Assert.AreEqual(2, repository.Get().Count());
            Assert.IsNull(repository.Remove(117)); // Kontrollerer fjernelse af ikke-eksisterende ID
        }

        /// <summary>
        /// Tester opdatering af lærerdata i repository.
        /// </summary>
        [TestMethod()]
        public void UpdateTest()
        {
            // Arrange: Tilføjer en lærer og opdaterer lærerens data
            TeacherRepository repository = new TeacherRepository();
            Teacher teacher1 = new Teacher() { Name = "Tan", Salary = 200 };
            Teacher data1 = new Teacher() { Name = "Zimon", Salary = 150 };

            repository.AddTeacher(teacher1);

            // Act: Opdaterer lærerens data
            Teacher updated = repository.Update(1, data1);

            // Assert: Kontrollerer at opdateringen blev gennemført korrekt
            Assert.AreEqual("Zimon", updated.Name);
            Assert.AreEqual(150, updated.Salary);
            Assert.IsNull(repository.Update(114, data1)); // Kontrollerer opdatering af ikke-eksisterende ID
        }
    }
}