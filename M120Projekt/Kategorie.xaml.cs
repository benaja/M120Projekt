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

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for Kategorie.xaml
    /// </summary>
    public partial class Kategorie : UserControl
    {
        public Kategorie(int kategorieId)
        {
            InitializeComponent();

            var kategorieData = BLL.Kategorie.LesenID(kategorieId);

            var passwoerterData = BLL.Passwort.LesenFremdschluesselGleich(kategorieData);

            passwoerter.ItemsSource = passwoerterData;
        }

        void selectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var cellItemId = Convert.ToInt32(passwoerter.SelectedItem.ToString());

            var password = BLL.Passwort.LesenID(cellItemId);
            
            MessageBoxResult result = MessageBox.Show("Wählen sie ihre Aktion!", "Navigation", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("Hello to you too!", "My App");
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Oh well, too bad!", "My App");
                    break;
                case MessageBoxResult.Cancel:
                    MessageBox.Show("Nevermind then...", "My App");
                    break;
            }


            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.showPassword(cellItemId);
        }
    }
}
