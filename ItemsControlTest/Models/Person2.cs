using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace ItemsControlTest.Models
{
    public class Person2 : Person
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */


        #region ExName変更通知プロパティ
        private string _ExName;

        public string ExName
        {
            get
            { return _ExName; }
            set
            { 
                if (_ExName == value)
                    return;
                _ExName = value;
                RaisePropertyChanged();
            }
        }
        #endregion

    }
}
