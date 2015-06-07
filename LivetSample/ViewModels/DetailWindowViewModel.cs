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
    public class DetailWindowViewModel : ViewModel, IDataErrorInfo
    {
        /* コマンド、プロパティの定義にはそれぞれ 
         * 
         *  lvcom   : ViewModelCommand
         *  lvcomn  : ViewModelCommand(CanExecute無)
         *  llcom   : ListenerCommand(パラメータ有のコマンド)
         *  llcomn  : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop   : 変更通知プロパティ(.NET4.5ではlpropn)
         *  
         * を使用してください。
         * 
         * Modelが十分にリッチであるならコマンドにこだわる必要はありません。
         * View側のコードビハインドを使用しないMVVMパターンの実装を行う場合でも、ViewModelにメソッドを定義し、
         * LivetCallMethodActionなどから直接メソッドを呼び出してください。
         * 
         * ViewModelのコマンドを呼び出せるLivetのすべてのビヘイビア・トリガー・アクションは
         * 同様に直接ViewModelのメソッドを呼び出し可能です。
         */

        /* ViewModelからViewを操作したい場合は、View側のコードビハインド無で処理を行いたい場合は
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信する事を検討してください。
         */

        /* Modelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedEventListenerや
         * CollectionChangedEventListenerを使うと便利です。各種ListenerはViewModelに定義されている
         * CompositeDisposableプロパティ(LivetCompositeDisposable型)に格納しておく事でイベント解放を容易に行えます。
         * 
         * ReactiveExtensionsなどを併用する場合は、ReactiveExtensionsのCompositeDisposableを
         * ViewModelのCompositeDisposableプロパティに格納しておくのを推奨します。
         * 
         * LivetのWindowテンプレートではViewのウィンドウが閉じる際にDataContextDisposeActionが動作するようになっており、
         * ViewModelのDisposeが呼ばれCompositeDisposableプロパティに格納されたすべてのIDisposable型のインスタンスが解放されます。
         * 
         * ViewModelを使いまわしたい時などは、ViewからDataContextDisposeActionを取り除くか、発動のタイミングをずらす事で対応可能です。
         */

        /* UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         * 
         * LivetのViewModelではプロパティ変更通知(RaisePropertyChanged)やDispatcherCollectionを使ったコレクション変更通知は
         * 自動的にUIDispatcher上での通知に変換されます。変更通知に際してUIDispatcherを操作する必要はありません。
         */

        private Member _member;

        private bool _isEditMode;

        #region コンストラクタ
        public DetailWindowViewModel( Member member )
        {
            _member = member;
            InputName = _member.Name;
            if( _member.Birthday != DateTime.MinValue )
            {
                 InputBirthday = _member.Birthday.ToString("yyyy/MM/dd");
            }
            
            InputMemo = _member.Memo;

            _isEditMode = !String.IsNullOrEmpty(_member.Name);
        }
        #endregion

        public void Initialize()
        {
        }

        #region Property
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

        #endregion

        #region Input
        string _inputName;
        public string InputName
        {
            get { return _inputName; }
            set
            {
                if (_inputName == value) return;
                _inputName = value;
                if( _inputName == null || String.IsNullOrEmpty(value.Trim()))
                {
                    _errors[ErrInputName] = "名前は必須です";
                }
                else
                {
                    _errors[ErrInputName] = null;
                }
                RaisePropertyChanged("Error");
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        string _inputBirthday;
        public string InputBirthday
        {
            get { return _inputBirthday; }
            set
            {
                if (_inputBirthday == value) return;
                _inputBirthday = value;
                if(_inputBirthday == null || String.IsNullOrEmpty(_inputBirthday.Trim()))
                {
                    _errors[ErrInputBirthday] = "生年月日は必須です";
                }
                else
                {
                    _errors[ErrInputBirthday] = null;
                }
                RaisePropertyChanged("Error");
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string InputMemo { get; set; }
        #endregion


        #region SaveCommand
        private ViewModelCommand _SaveCommand;

        public ViewModelCommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new ViewModelCommand(Save, CanSave);
                }
                return _SaveCommand;
            }
        }

        public bool CanSave()
        {
            return String.IsNullOrEmpty(Error);
        }

        public void Save()
        {
            Name = InputName;
            Birthday = DateTime.Parse( InputBirthday );
            Memo = InputMemo;

            // 編集
            if( _isEditMode )
            {

            }
            // 追加
            else
            {
                _member.AddThisToMainCollection();
            }

            // 閉じる
            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
        }
        #endregion


        #region CancelCommand
        private ViewModelCommand _CancelCommand;

        public ViewModelCommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new ViewModelCommand(Cancel);
                }
                return _CancelCommand;
            }
        }

        public void Cancel()
        {
            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
        }
        #endregion


        #region IDataErrorInfo
        const string ErrInputName = "InputName";
        const string ErrInputBirthday = "InputBirthday";

        Dictionary<string, string> _errors = new Dictionary<string, string>()
        {
            {ErrInputName, null},
            {ErrInputBirthday, null}
        };

        public string Error
        {
            get 
            {
                var list = new List<string>();

                if (!String.IsNullOrEmpty(this[ErrInputName]))
                {
                    list.Add("名前");
                }
                if (!String.IsNullOrEmpty(this[ErrInputBirthday]))
                {
                    list.Add("生年月日");
                }
                if (!list.Any())
                {
                    return null;
                }

                return String.Join("・", list) + "が不正です";
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (_errors.ContainsKey(columnName))
                {
                    return _errors[columnName];
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion


    }
}
