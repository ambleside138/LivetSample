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

using MetroRadianceSample.Models;

namespace MetroRadianceSample.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private IReadOnlyCollection<Player> _Players;

        public void Initialize()
        {
        }


        #region TopMost変更通知プロパティ
        private bool _TopMost;

        public bool TopMost
        {
            get
            { return _TopMost; }
            set
            { 
                if (_TopMost == value)
                    return;
                _TopMost = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        public IReadOnlyCollection<Player> Players
        {
            get
            {
                return _Players ?? (this._Players = new List<Player>
                    {
                        new Player("33","菊池"),
                        new Player("4","小窪"),
                        new Player("6","梵"),
                    });
            }
        }
    }

    public class Player
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public Player( string number, string name )
        {
            Number = number;
            Name = name;
        }
    }
}
