using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBaseSample
{
    enum Gender
    {
        None,
        Men,
        Women,
    }

    class Person
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public bool AuthMember { get; set; }

        public List<Person> Children { get; set; }
    }
}
