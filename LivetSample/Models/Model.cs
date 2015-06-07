using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using System.Collections.ObjectModel;

namespace LivetSample.Models
{
    public class Model : NotificationObject
    {
        ObservableCollection<Member> _listMember;

        public ObservableCollection<Member> ListMember
        {
            get
            {
                if( _listMember == null)
                {
                    _listMember = new ObservableCollection<Member>
                    {
                        new Member(_listMember){ Name = "hoge", Birthday = new DateTime(1988,6,5), Memo = "memo1" },
                        new Member(_listMember){ Name = "huga", Birthday = new DateTime(1999,11,26), Memo = "memo2" },
                    };
                }
                return _listMember;
            }
        }
    }
}
