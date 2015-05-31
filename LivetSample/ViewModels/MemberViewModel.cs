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

using LivetSample.Models;

namespace LivetSample.ViewModels
{
    public class MemberViewModel : ViewModel
    {
        private readonly Member _member;

        public MemberViewModel( Member member )
        {
            _member = member;
        }

        public void Initialize()
        {
        }

        public string Name
        {
            get { return _member.Name; }
            set { _member.Name = value; }
        }

        public DateTime Birthday
        {
            get { return _member.Birthday; }
            set { _member.Birthday = value; }
        }

        public string Memo
        {
            get { return _member.Memo; }
            set { _member.Memo = value; }
        }

        public int Age
        {
            get { return (DateTime.Now - Birthday).Days / 365; }
        }

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
    }
}
