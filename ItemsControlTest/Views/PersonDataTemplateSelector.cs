using ItemsControlTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ItemsControlTest.Views
{
    class PersonDataTemplateSelector : DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var p = (Person)item;
            if( p is Person2)
            {
                return (DataTemplate)((FrameworkElement)container).FindResource("personView2");
            }
            else
            {
                return (DataTemplate)((FrameworkElement)container).FindResource("personView");
            }
        }
    }
}
