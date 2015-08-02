using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        //public override bool Equals(object obj)
        //{
        //    return ((Ringo)obj).Name == Name;
        //}
    }



    class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Ringo> _ringoSource;
        //public ObservableCollection<Ringo> RingoSource
        //{
        //    get
        //    {
        //        if (_ringoSource == null)
        //        {
        //            _ringoSource = new ObservableCollection<Ringo>()
        //    {
        //        new Ringo(){ ID = 1, Name = "シナノゴールド" },
        //        new Ringo(){ ID = 2, Name = "シナノドルチェ" },
        //        new Ringo(){ ID = 3, Name = "ジョナゴールド" },
        //        new Ringo(){ ID = 1, Name = "しなのごーるど" },
        //        new Ringo(){ ID = 2, Name = "しなのどるちぇ" },
        //        new Ringo(){ ID = 3, Name = "ジョナゴールドながいもじがあるとき" }
        //    };
        //        }
        //        return _ringoSource;
        //    }
        //}
        public ObservableCollection<Ringo> RingoSource
        {
            get
            {
                if (_ringoSource == null)
                {
                    _ringoSource = new ObservableCollection<Ringo>( Enumerable.Range(0, 5).Select(i => new Ringo() { ID = i, Name = "RINGO" + i + Environment.NewLine + "HOGE"}) );
                }
                return _ringoSource;
            }
        }

        private Ringo _SelectedRingo;

        public Ringo SelectedRingo
        {
            get { return _SelectedRingo; }
            set { 
                if( _SelectedRingo == value) return;
                _SelectedRingo = value;
                RaisePropertyChanged("SelectedRingo");
            }
        }


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

        public event PropertyChangedEventHandler PropertyChanged;

        // INotifyPropertyChanged.PropertyChangedイベントを発生させる。
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewModel()
        {
            var selected = new Ringo{ Name = "SELECTED_RINGO", ID=100};
            RingoSource.Add(selected );
            RingoSource.Add(new Ringo() { ID = 3, Name = "ジョナゴールドながいもじがあるとき" });
            SelectedRingo = selected;
        }
    }
}
