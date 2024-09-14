using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SchoolLibrary
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }



        public void ValidateName()
        {
            if (Name == null)
            {
                throw new ArgumentNullException($"Name cannot be null {Name}");
            }
            if (Name.Length < 1)
            {
                throw new ArgumentOutOfRangeException($"Name must contain at least 1 character: {Name}");
            }
        }
        public virtual void Validate()
        {
            ValidateName();
        }
    }

}
