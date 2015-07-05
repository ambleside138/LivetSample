using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfBaseSample
{
    class DragContentAdorner : ControlHostAdornerBase
    {
        private readonly ContentPresenter _contentPresenter;
        private TranslateTransform _translate;
        private Point _offset;

        public DragContentAdorner(UIElement adornedElement, object draggedData, DataTemplate dataTemplate, Point offset )
            :base( adornedElement )
        {
            _contentPresenter = new ContentPresenter { Content = draggedData, ContentTemplate = dataTemplate, Opacity = 0.7 };
            _translate = new TranslateTransform { X = 0, Y = 0 };
            _contentPresenter.RenderTransform = _translate;

            _offset = offset;

            Host.Children.Add(_contentPresenter);

            _contentPresenter.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Left);
            _contentPresenter.SetValue(VerticalAlignmentProperty, VerticalAlignment.Top);
        }

        public void SetScreenPosition(Point p)
        {
            var positionInControl = base.AdornedElement.PointFromScreen(p);
            _translate.X = positionInControl.X - _offset.X;
            _translate.Y = positionInControl.Y - _offset.Y;
            base.AdornerLayer.Update();
        }
    }
}
