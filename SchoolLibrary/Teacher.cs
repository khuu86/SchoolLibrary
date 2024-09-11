namespace SchoolLibrary
{
    public class Teacher : Person
    {
        public int Salary { get; set; }
        private List<string>? Classes { get; set; } = new();
        public void AddClass(string classname)
        {
            Classes.Add(classname);
        }

        public void ValidateSalary()
        {
            if (Salary < 0)
            {
                throw new ArgumentOutOfRangeException(
                    $"Salary must be positive: {Salary}");
            }
        }


        public string GetClasses()
        {
            if (Classes.Count == 0)
                return "";
            string classes = "";
            foreach (var clas in Classes)
                classes += clas + ", ";
            return classes.Substring(0, classes.Length - 2); // -2 removes the last 2 letters ", "
        }
        public void ValidateClasses()
        {
            if (Classes == null)
            {
                throw new ArgumentNullException("Classes cannot be null");
            }
        }

        public override void Validate()
        {
            ValidateSalary();
            ValidateClasses();
            base.Validate();
        }

        public override string ToString()
        {
            return $"{Id}, {Name}, {Salary}";
        }
    }
}
