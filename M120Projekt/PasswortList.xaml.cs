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
    /// Interaction logic for PasswortList.xaml
    /// </summary>
    public partial class PasswortList : UserControl
    {
        public PasswortList(List<long> passwörterId)
        {
            InitializeComponent();

            List<DAL.Passwort> passwörter = new List<DAL.Passwort>();
            foreach(int passwortId in passwörterId)
            {
                passwörter.Add(BLL.Passwort.LesenID(passwortId));
            }

            passwoerter.ItemsSource = passwörter;
        }

        void selectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var cellItemId = Convert.ToInt32(passwoerter.SelectedItem.ToString());

            var password = BLL.Passwort.LesenID(cellItemId);

            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.showPassword(cellItemId);
        }

        public void setAnzahlErgebnisse(int ergebnisse)
        {
            anzahl_ergebnisse.Visibility = Visibility.Visible;
            anzahl_ergebnisse.Content = $"Anzahl Ergebnisse: {ergebnisse}";
            if(ergebnisse > 0)
            {
                anzahl_ergebnisse.Foreground = new SolidColorBrush(Color.FromRgb(45, 194, 38));
            }
            else
            {
                anzahl_ergebnisse.Foreground = new SolidColorBrush(Color.FromRgb(244, 48, 38));
            }
        }
    }
}
