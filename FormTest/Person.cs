using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormTest
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public string Display
        {
            get
            {
                return Name + "さん" + Environment.NewLine + Age + "歳";
            }

        }
    }
}
