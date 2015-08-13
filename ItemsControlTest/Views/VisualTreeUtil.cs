using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ItemsControlTest.Views
{
    public static class VisualTreeUtil
    {
        public static T FindAncestor<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);
 
            if (parent == null) return null;
 
            var parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }
    }
}
