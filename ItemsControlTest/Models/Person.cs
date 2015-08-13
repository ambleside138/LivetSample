using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace ItemsControlTest.Models
{
    public class Person : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        public int Age { get; set; }

        public string Name { get; set; }

        public string Display
        {
            get{
                return Name + "さん" + Environment.NewLine + Age + "歳";
            }
            
        }
    }
}
