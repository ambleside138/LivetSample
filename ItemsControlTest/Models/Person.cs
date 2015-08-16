using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using System.Collections.ObjectModel;

namespace ItemsControlTest.Models
{
    public class Person : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        private static int _idGen = 0;

        public int Age { get; set; }

        public string Name { get; set; }

        public string Display
        {
            get{
                return Name + "さん" + Environment.NewLine + Age + "歳";
            }
            
        }


        #region Children変更通知プロパティ
        private ObservableCollection<Person> _Children;

        public ObservableCollection<Person> Children
        {
            get
            { return _Children; }
            set
            { 
                if (_Children == value)
                    return;
                _Children = value;
                RaisePropertyChanged("Children");
            }
        }
        #endregion


        public int MyProperty { get; set; }

        public decimal MyProperty2 { get; set; }

        public int MyProperty3 { get; set; }

        public Person()
        {
            _idGen++;
            MyProperty = _idGen;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Person;
            if(other==null)
            {
                return false;
            }

            return other.Age == this.Age && other.Name == this.Name && other.MyProperty == this.MyProperty && other.MyProperty2 == this.MyProperty2 && other.MyProperty3 == this.MyProperty3;
        }

        public override int GetHashCode()
        {
            return Age.GetHashCode() ^ Name.GetHashCode() ^ MyProperty.GetHashCode() ^ MyProperty2.GetHashCode() ^ MyProperty3.GetHashCode();
        }
    }
}
