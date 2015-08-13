using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using ItemsControlTest.Models;

namespace ItemsControlTest.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {

        #region Person変更通知プロパティ
        private Person _Person;

        public Person Person
        {
            get
            { return _Person; }
            set
            { 
                if (_Person == value)
                    return;
                _Person = value;
                RaisePropertyChanged("Person");
            }
        }
        #endregion


        #region Persons変更通知プロパティ
        private ObservableSynchronizedCollection<Person> _Persons;

        public ObservableSynchronizedCollection<Person> Persons
        {
            get
            { return _Persons; }
            set
            { 
                if (_Persons == value)
                    return;
                _Persons = value;
                RaisePropertyChanged("Persons");
            }
        }
        #endregion


        public void Initialize()
        {
            Person = new Person { Age = 27, Name = "孫悟空" };


        }

        public void Init()
        {
            Persons = new ObservableSynchronizedCollection<Person>(Enumerable.Range(0, 100).Select(i => new Person { Name = "TEST" + i, Age = i }));
            Persons.Add(new Person2 { ExName = "ExNameSample", Name = "Ex", Age = 2015 });
        }
    }
}
