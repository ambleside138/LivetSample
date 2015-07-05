using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WpfBaseSample
{
    class ControlHostAdornerBase : Adorner, IDisposable
    {
        private AdornerLayer _adnornerLayer;
        protected Grid Host { get; set; }

        protected ControlHostAdornerBase(UIElement adornedElement )
            :base(adornedElement)
        {
            _adnornerLayer = AdornerLayer.GetAdornerLayer(adornedElement);
            Host = new Grid();

            if (_adnornerLayer != null)
            {
                _adnornerLayer.Add(this);
            }
        }

        public void Detach()
        {
            _adnornerLayer.Remove(this);
        }

        protected AdornerLayer AdornerLayer
        {
            get { return _adnornerLayer; }
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Host.Measure(constraint);
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Host.Arrange(new Rect(finalSize));
            return base.ArrangeOverride(finalSize);
        }

        protected override System.Windows.Media.Visual GetVisualChild(int index)
        {
            if( VisualChildrenCount <= index )
            {
                throw new IndexOutOfRangeException();
            }

            return Host;
        }

        #region Dispose Pattern

        private bool _disposed;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        ~ControlHostAdornerBase()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            if (disposing)
            {
                Detach();
            }
        }

        #endregion
    }
}
