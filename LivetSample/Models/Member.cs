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

        // プロパティ名を直接書くのをやめた
        // 参考：http://posaune.hatenablog.com/entry/20111213/1323789203

        // 本来は↓こう書くところを
        //if (PropertyChanged != null)
        //{
        //  PropertyChanged(new PropertyChangedEventArgs("Age"));
        //}
        // Livetを使えば↓こう書ける
        // RaisePropertyChanged();

        bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked == value) return;
                _isChecked = value;
                RaisePropertyChanged();
            }
        }

        String _name;
        public String Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                RaisePropertyChanged();
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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
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
