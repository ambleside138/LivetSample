using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfBaseSample
{
    class InsertionAdorner : ControlHostAdornerBase
    {
        private readonly InsertionCursor _insertionCursor;

        public InsertionAdorner(UIElement adornedElement, bool showInBottomSide = false )
            :base(adornedElement)
        {
            _insertionCursor = new InsertionCursor();

            Host.Children.Add(_insertionCursor);

            _insertionCursor.SetValue(VerticalAlignmentProperty, showInBottomSide ? VerticalAlignment.Bottom : VerticalAlignment.Top);
            _insertionCursor.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
        }
    }
}
