using Livet.Behaviors;
using System;
using System.Collections.Generic;
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

namespace MetroTest.Controls
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MetroTest.Controls"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MetroTest.Controls;assembly=MetroTest.Controls"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを参照し、選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:CallMethodButton/>
    ///
    /// </summary>
    public class CallMethodButton : Button
    {
        static CallMethodButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CallMethodButton), new FrameworkPropertyMetadata(typeof(CallMethodButton)));
        }

        private readonly MethodBinder _binder = new MethodBinder();
        private readonly MethodBinderWithArgument _binderWithArgument = new MethodBinderWithArgument();
        private bool _hasParameter;

        #region MethodTarget 依存関係プロパティ
        public static readonly DependencyProperty MethodTargetProperty =
            DependencyProperty.Register("MethodTarget", typeof(object), typeof(CallMethodButton), new UIPropertyMetadata(null));

        public object MethodTarget
        {
            get { return GetValue(MethodTargetProperty); }
            set { SetValue(MethodTargetProperty, value); }
        }
        #endregion

        #region MethodName 依存関係プロパティ

        public string MethodName
        {
            get { return (string)this.GetValue(MethodNameProperty); }
            set { this.SetValue(MethodNameProperty, value); }
        }
        public static readonly DependencyProperty MethodNameProperty =
            DependencyProperty.Register("MethodName", typeof(string), typeof(CallMethodButton), new UIPropertyMetadata(null));

        #endregion

        #region MethodParameter 依存関係プロパティ

        public object MethodParameter
        {
            get { return this.GetValue(MethodParameterProperty); }
            set { this.SetValue(MethodParameterProperty, value); }
        }
        public static readonly DependencyProperty MethodParameterProperty =
            DependencyProperty.Register("MethodParameter", typeof(object), typeof(CallMethodButton), new UIPropertyMetadata(null, MethodParameterPropertyChangedCallback));

        private static void MethodParameterPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (CallMethodButton)d;
            source._hasParameter = true;
        }

        #endregion

        protected override void OnClick()
        {
            base.OnClick();

            if (string.IsNullOrEmpty(MethodName)) return;

            var target = MethodTarget ?? DataContext;
            if (target == null) return;

            if( _hasParameter )
            {
                _binderWithArgument.Invoke(target, MethodName, MethodParameter);
            }
            else
            {
                _binder.Invoke(target, MethodName);
            }
        }
    }
}
