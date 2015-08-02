using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// ItemSelectPopup.xaml の相互作用ロジック
    /// </summary>
    public partial class ItemSelectPopup : UserControl
    {
        #region const
        /// <summary> デフォルト列数 </summary>
        const int DefaultColumnCount = 3;

        /// <summary> デフォルト最大高さ </summary>
        const int DefaultListBoxMaxHeight = 300;
        #endregion



        public ItemSelectPopup()
        {
            InitializeComponent();           
        }

        // propdpでコードスニペット利用可能
        #region Placement依存関係プロパティ
        /// <summary>
        /// ポップアップを表示する位置を取得または設定します
        /// </summary>
        public PlacementMode Placement
        {
            get { return (PlacementMode)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register("Placement", typeof(PlacementMode), typeof(ItemSelectPopup), new PropertyMetadata(PlacementMode.Bottom));
        #endregion

        #region ItemsSource依存関係プロパティ
        /// <summary>
        /// 選択肢となるコレクションを取得または設定します
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ItemSelectPopup), new PropertyMetadata(null));
        #endregion

        #region ColumnCount依存関係プロパティ
        /// <summary>
        /// 選択候補表示時の列数を取得または設定します
        /// </summary>
        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColumnCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register("ColumnCount", typeof(int), typeof(ItemSelectPopup), new PropertyMetadata(DefaultColumnCount));
        #endregion

        #region ListBoxMaxHeight依存関係プロパティ
        /// <summary>
        /// リストボックスの最大高さを取得または設定します
        /// </summary>
        public int ListBoxMaxHeight
        {
            get { return (int)GetValue(ListBoxMaxHeightProperty); }
            set { SetValue(ListBoxMaxHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemViewerMaxHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListBoxMaxHeightProperty =
            DependencyProperty.Register("ListBoxMaxHeight", typeof(int), typeof(ItemSelectPopup), new PropertyMetadata(DefaultListBoxMaxHeight));
        #endregion

        #region SelectionMode依存関係プロパティ
        /// <summary>
        /// 項目の選択方法を取得または設定します
        /// </summary>
        public SelectionMode SelectionMode
        {
            get { return (SelectionMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register("SelectionMode", typeof(SelectionMode), typeof(ItemSelectPopup), new PropertyMetadata(SelectionMode.Single));
        #endregion

        #region SelectedItem依存関係プロパティ
        /// <summary>
        /// 選択中の項目を取得または設定します
        /// </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        // バインド方向はデフォルトでTwoWay
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(ItemSelectPopup), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion

        #region ItemTemplate依存関係プロパティ
        /// <summary>
        /// ItemTemplateを取得または設定します
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(ItemSelectPopup), new PropertyMetadata(null));
        #endregion

        #region ButtonContent依存関係プロパティ
        /// <summary>
        /// ボタンに表示するコンテンツを取得または設定します
        /// </summary>
        public object ButtonContent
        {
            get { return (object)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.Register("ButtonContent", typeof(object), typeof(ItemSelectPopup), new PropertyMetadata("ItemSelectPopupButton"));
        #endregion

        /// <summary>
        /// ポップアップの表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            var isSingleSelectMode = SelectionMode == System.Windows.Controls.SelectionMode.Single;
            btnSelectAll.Visibility = isSingleSelectMode ? Visibility.Collapsed : System.Windows.Visibility.Visible;
            btnClear.Visibility = isSingleSelectMode ? Visibility.Collapsed : System.Windows.Visibility.Visible;

            popup.IsOpen = true;
            var ringo = (Ringo)SelectedItem;
            if( ringo == null )
            {
                Console.WriteLine("NULL");
            }
            else
            {
                Console.WriteLine("SELECTED: " + ringo.Name);
            }
            

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch( SelectionMode )
            {
                // Singleモード時は選択されたら終了
                case System.Windows.Controls.SelectionMode.Single:
                    if( SelectedItem == null )
                    {
                        
                    }
                    else
                    {
                        popup.IsOpen = false;
                    }
                    break;
            }
        }

        bool Validate()
        {
            switch (SelectionMode)
            {
                // Singleモード時は選択されたら終了
                case System.Windows.Controls.SelectionMode.Single:
                    if (SelectedItem == null)
                    {
                        return false;
                    }
                    else
                    {
                        popup.IsOpen = false;
                    }
                    break;
            }

            return true;
        }

        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            listBox.SelectAll();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            listBox.UnselectAll();
        }
    }
}
