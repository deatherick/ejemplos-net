using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfExample
{
    public class ItemsMenu
    {
        public string Nombre { get; set; }
        public string Formulario { get; set; }
    }
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu
    {
     
        public Menu()
        {
            InitializeComponent();
            DemoItemsListBox.ItemsSource = new List<ItemsMenu>
            {
                new ItemsMenu {Nombre = "Inicio"},
                new ItemsMenu {Nombre = "Catalogos"},
                new ItemsMenu {Nombre = "Modulos"},
            };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Owner.Hide();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Owner.Show();
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
