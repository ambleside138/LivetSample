using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfBaseSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var data = new ObservableCollection<Person>(
                Enumerable.Range(1, 100).Select(i => new Person
                {
                    Name = "田中 太郎" + i,
                    Gender = i % 2 == 0 ? Gender.Men : Gender.Women,
                    Age = 20 + i % 50,
                    AuthMember = i % 5 == 0
                }));

            ///dataGrid.ItemsSource = data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ///textblock.Text = textbox.Text;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("keyDown=" + e.Key);
            Console.WriteLine("keyDown_IME=" + e.ImeProcessedKey);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine("textChanged=" + textbox.Text );

            
        }

        private void textbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Pre_keyDown=" + e.Key);
            Console.WriteLine("Pre_keyDown_IME=" + e.ImeProcessedKey);
        }

        private void TextBox_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if( e.Key == Key.Enter )
            {
                var list = listBox.ItemsSource as ObservableCollection<Ringo>;
                var newRingo = new Ringo() { Name = "追加" };
                list.Insert( listBox.SelectedIndex + 1, newRingo);

                listBox.UpdateLayout();

                var listboxItem = listBox.ItemContainerGenerator.ContainerFromItem(newRingo);

                var contentPresenter = FindVisualChild<ContentPresenter>(listboxItem);

                var dataTemplate = contentPresenter.ContentTemplate;
                var textbox = (TextBox)dataTemplate.FindName("tbRingo", contentPresenter);

                

                textbox.Focus();
            }



        }

        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
