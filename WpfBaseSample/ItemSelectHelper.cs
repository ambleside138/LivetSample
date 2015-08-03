using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBaseSample
{
    class ItemSelectHelper<T> : INotifyPropertyChanged where T: class
    {

        #region Item変更通知プロパティ
        private T _Item;

        public T Item
        {
            get
            { return _Item; }
            set
            { 
                if (_Item == value)
                    return;
                _Item = value;
                RaisePropertyChanged("Item");
            }
        }
        #endregion


        #region IsSelected変更通知プロパティ
        private bool _IsSelected;

        public bool IsSelected
        {
            get
            { return _IsSelected; }
            set
            { 
                if (_IsSelected == value)
                    return;
                _IsSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }
        #endregion

        public ItemSelectHelper( T item )
        {
            Item = item;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // INotifyPropertyChanged.PropertyChangedイベントを発生させる。
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
