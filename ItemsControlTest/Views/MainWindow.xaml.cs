using ItemsControlTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItemsControlTest.Views
{
    /* 
     * ViewModelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedWeakEventListenerや
     * CollectionChangedWeakEventListenerを使うと便利です。独自イベントの場合はLivetWeakEventListenerが使用できます。
     * クローズ時などに、LivetCompositeDisposableに格納した各種イベントリスナをDisposeする事でイベントハンドラの開放が容易に行えます。
     *
     * WeakEventListenerなので明示的に開放せずともメモリリークは起こしませんが、できる限り明示的に開放するようにしましょう。
     */

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void textbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key!=Key.Enter)
            {
                return;
            }

            var collection = listbox.ItemsSource as IList<Person>;
            var selected = ((TextBox)sender).DataContext as Person;
            var selectedIndex = collection.IndexOf(selected);
            var newPerson = new Person { Name = "NewPerson", Age = 0 };
            collection.Insert(selectedIndex + 1, newPerson);
            
            // 要素を作成
            listbox.UpdateLayout();

            //listbox.SelectedIndex = selectedIndex + 1;
            FocusContainerChild(newPerson);
        }

        void FocusContainerChild( object dataContext )
        {
            var item = listbox.ItemContainerGenerator.ContainerFromItem(dataContext) as ListBoxItem;


            listbox.ScrollIntoView(item);

            

            var textbox = FindVisualChild<TextBox>(item);
            textbox.Focus();
        }

        void FocusTextBox()
        {
            
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
    where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
