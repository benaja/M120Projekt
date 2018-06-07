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
    /// Interaction logic for EditKategorie.xaml
    /// </summary>
    public partial class EditKategorie : UserControl
    {
        DAL.Kategorie kategorie;
        public EditKategorie()
        {
            InitializeComponent();
        }

        public void setKategorie(long kategorieId)
        {
            kategorie = BLL.Kategorie.LesenID(kategorieId);

            name.Text = kategorie.Name;

            delete_button.Visibility = Visibility.Visible;

        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            if (kategorie == null)
            {
                kategorie = new DAL.Kategorie
                {
                    Name = name.Text
                };
                BLL.Kategorie.Erstellen(kategorie);
            }
            else
            {
                kategorie.Name = name.Text;
                BLL.Kategorie.Aktualisieren(kategorie);
            }
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.showDefault();
            parentWindow.updateKategorienButtons();
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.showDefault();
        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            BLL.Kategorie.Loeschen(kategorie);
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.updateKategorienButtons();
            parentWindow.showDefault();
        }

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(name.Text))
            {
                save_button.IsEnabled = false;
            }
            else
            {
                save_button.IsEnabled = true;
            }
        }
    }
}
