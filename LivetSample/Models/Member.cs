using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace LivetSample.Models
{
    public class Member : NotificationObject
    {
        //Main m_parent;

        String _name;
        public String Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        DateTime _birthday;
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                if (_birthday == value) return;
                _birthday = value;
                RaisePropertyChanged("BirthDay");
            }
        }

        String _memo;
        public String Memo
        {
            get { return _memo; }
            set
            {
                if (_memo == value) return;
                _memo = value;
                RaisePropertyChanged("Memo");
            }
        }

        //public Member(Main parent)
        //{
        //    m_parent = parent;
        //}

        //public bool IsIncludedInMainCollection()
        //{
        //    return m_parent.Members.Contains(this);
        //}

        //public void AddThisToMainCollection()
        //{
        //    m_parent.Members.Add(this);
        //}

        //public void RemoveThisFromMainCollection()
        //{
        //    m_parent.Members.Remove(this);
        //}
    }
}
