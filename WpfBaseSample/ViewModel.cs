using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfBaseSample
{
    class Ringo
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }



    class ViewModel
    {
        private ObservableCollection<Ringo> _ringoSource;
        public ObservableCollection<Ringo> RingoSource
        {
            get
            {
                if (_ringoSource == null)
                {
                    _ringoSource = new ObservableCollection<Ringo>()
            {
                new Ringo(){ ID = 1, Name = "シナノゴールド" },
                new Ringo(){ ID = 2, Name = "シナノドルチェ" },
                new Ringo(){ ID = 3, Name = "ジョナゴールド" },
                new Ringo(){ ID = 1, Name = "しなのごーるど" },
                new Ringo(){ ID = 2, Name = "しなのどるちぇ" },
                new Ringo(){ ID = 3, Name = "ジョナゴールド" }
            };
                }
                return _ringoSource;
            }
        }

        public Ringo SelectedRingo { get; set; }

        public string SelectedRingoName { get; set; }

        //public AutoCompleteFilterPredicate<object> RingoFilter
        //{
        //    get { return (searchText, obj) => (obj as Ringo).Name.Contains(searchText); }
        //}

        public AutoCompleteFilterPredicate<object> RingoFilter
        {
            get { return (searchText, obj) => true; }
        }

        private ObservableCollection<Person> _personSource;

        public ObservableCollection<Person> PersonSource
        {
            get
            {
                if( _personSource == null )
                {
                    _personSource = new ObservableCollection<Person>
                    {
                        new Person
                        { 
                            Name = "田中 太郎", 
                            Children = new List<Person>
                            { 
                                new Person { Name = "田中 花子" }, 
                                new Person { Name = "田中 一郎" }, 
                                new Person { 
                                    Name = "木村 貫太郎", 
                                    Children = new List<Person> 
                                    { 
                                        new Person { Name = "木村 はな" }, 
                                        new Person { Name = "木村 梅" },
                                    } 
                                } 
                            } 
                        }, 
                        new Person 
                        { 
                            Name = "田中 次郎",
                            Children = new List<Person>
                            { 
                                new Person { Name = "田中 三郎" } 
                            } 
                        }
                    };
                }
                return _personSource;
            }
        }
    }
}
