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

        // http://posaune.hatenablog.com/entry/2012/12/07/220208
        // D&D=====================================================

        private object _draggedData = null;
        private Point? _initialPosition =null;
        private Point _mouseOffsetFromItem = new Point();
        private int? _draggedItemIndex = null;
        private DragContentAdorner _dragContentAdorner = null;
        private InsertionAdorner _insertionAdorner = null;

        private void listBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var itemsControl = sender as ItemsControl;
            var draggedItem = e.OriginalSource as FrameworkElement;

            if( itemsControl == null || draggedItem == null )
            {
                return; 
            }

            _draggedData = itemsControl.GetItemData(draggedItem);
            if( _draggedData == null)
            {
                return;
            }

            _initialPosition = this.PointToScreen(e.GetPosition(this));
            _mouseOffsetFromItem = itemsControl.PointToItem( draggedItem, _initialPosition.Value );
            _draggedItemIndex = itemsControl.GetItemIndex(_draggedData);
            Console.WriteLine("LeftButtonDown");
        }

        private void listBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var itemsControl = sender as ItemsControl;

            if( _draggedData == null || _initialPosition == null || itemsControl == null )
            {
                Console.WriteLine("mouseMove_NULL_01");
                return;
            }

            var currentPos = this.PointToScreen(e.GetPosition(this));
            var v = _initialPosition - currentPos;

            Console.WriteLine("==============");
            Console.WriteLine(_initialPosition);
            Console.WriteLine(currentPos);
            Console.WriteLine("==============");

            if( MovedEnoughForDrag(v.Value) )
            {
                Console.WriteLine("mouseMove_NOT_DRAG");
                return;
            }

            _dragContentAdorner = new DragContentAdorner(
                itemsControl, _draggedData, itemsControl.ItemTemplate, _mouseOffsetFromItem);
            _dragContentAdorner.SetScreenPosition(currentPos);

            DragDrop.DoDragDrop(itemsControl, _draggedData, DragDropEffects.Move);

            CleanUpData();
        }


        private void CreateInsertionAdorner(DependencyObject draggedItem, ItemsControl itemsControl)
        {
            var draggedOveredContainer = itemsControl.GetItemContainer(draggedItem);
            bool showInRight = false;
            if (draggedOveredContainer == null)
            {
                draggedOveredContainer = itemsControl.GetLastContainer();
                showInRight = true;
            }

            _insertionAdorner = new InsertionAdorner(draggedOveredContainer, showInRight);
        }

        private static bool MovedEnoughForDrag(Vector delta)
        {
            return Math.Abs(delta.X) > SystemParameters.MinimumHorizontalDragDistance
                   && Math.Abs(delta.Y) > SystemParameters.MinimumVerticalDragDistance;
        }

        private void CleanUpData()
        {
            _initialPosition = null;
            _draggedData = null;

            if (_insertionAdorner != null) { _insertionAdorner.Detach(); }

            if (_dragContentAdorner != null) { _dragContentAdorner.Detach(); }

            _insertionAdorner = null;
            _draggedItemIndex = null;
            Console.WriteLine("CLEAN_UP");
        }

        private void listBox_PreviewDragEnter(object sender, DragEventArgs e)
        {
            var itemsControl = sender as ItemsControl;
            if( itemsControl == null )
            {
                Console.WriteLine("dragEnter_NULL_01"); 
                return;
            }

            CreateInsertionAdorner( e.OriginalSource as DependencyObject, itemsControl );
        }

        private void listBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            var currentPos = this.PointToScreen(e.GetPosition(this));
            _dragContentAdorner.SetScreenPosition(currentPos);
        }

        private void listBox_PreviewDragLeave(object sender, DragEventArgs e)
        {
            if( _insertionAdorner != null )
            {
                Console.WriteLine("dragLeave"); 
                CreanUpInsertionAdorner();
            }
        }

        private void CreanUpInsertionAdorner()
        {
            _insertionAdorner.Detach();
            _insertionAdorner = null;
        }

        private void listBox_PreviewDrop(object sender, DragEventArgs e)
        {
            var itemsControl = sender as ItemsControl;
            if( itemsControl == null )
            {
                return;
            }

            var dropTargetData = itemsControl.GetItemData(e.OriginalSource as DependencyObject);
            DropItemAt(itemsControl.GetItemIndex(dropTargetData), itemsControl);
        }

        private void DropItemAt(int? droppedItemIndex, ItemsControl itemsControl)
        {
            itemsControl.RemoveItem(_draggedData);

            if (droppedItemIndex != null)
            {
                droppedItemIndex -= droppedItemIndex > _draggedItemIndex ? 1 : 0;
                itemsControl.InsertItemAt((int)droppedItemIndex, _draggedData);
            }
            else
            {
                itemsControl.AddItem(_draggedData);
            }
        }

        private void listBox_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            CleanUpData();
        }

    }
}
