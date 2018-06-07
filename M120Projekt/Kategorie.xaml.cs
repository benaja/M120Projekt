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
        DAL.Kategorie kategorie;
        public Kategorie(long kategorieId)
        {
            InitializeComponent();

            kategorie = BLL.Kategorie.LesenID(kategorieId);

            var passwoerterData = BLL.Passwort.LesenFremdschluesselGleich(kategorie);

            passwoerter.ItemsSource = passwoerterData;
        }

        void selectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var cellItemId = Convert.ToInt32(passwoerter.SelectedItem.ToString());

            var password = BLL.Passwort.LesenID(cellItemId);
            
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.showPassword(cellItemId);
        }

        private void edit_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.showDefault();
            var kategorieElement = new M120Projekt.EditKategorie();
            kategorieElement.setKategorie(kategorie.KategorieId);
            parentWindow.content.Children.Add(kategorieElement);
        }
    }
}
