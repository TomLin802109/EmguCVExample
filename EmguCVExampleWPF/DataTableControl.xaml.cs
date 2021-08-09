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

namespace Quadrep
{
    /// <summary>
    /// Interaction logic for DataTableControl.xaml
    /// </summary>
    public partial class DataTableControl : UserControl
    {
        private IEnumerable<object> _data;
        public DataTableControl(IEnumerable<object> objs)
        {
            InitializeComponent();
            datagrid.ItemsSource = objs;
            //_data = objs;
            //datagrid.DataContext = _data;
            
        }

        private void datagrid_Selected(object sender, RoutedEventArgs e)
        {
            ;
            MessageBox.Show($"", "Row Info");
        }

        private void datagrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var item = datagrid.SelectedItem;

            MessageBox.Show($"{item}", "Row Info");
        }
    }
}
