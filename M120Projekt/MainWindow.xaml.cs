using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            updateKategorienButtons();

            showAbgelaufenePasswörter();
        }

        private void search_textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            search_textbox.Text = "";
        }

        private void search_textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            //search_textbox.Text = "Suche";
        }

        private void search_button_Click(object sender, RoutedEventArgs e)
        {
            var passwörter = BLL.Passwort.LesenAttributWie(search_textbox.Text);

            List<long> passwörterId = new List<long>();
            foreach(DAL.Passwort passwort in passwörter)
            {
                passwörterId.Add(passwort.PasswortId);
            }

            var passwortList = new M120Projekt.PasswortList(passwörterId);

            content.Children.Add(passwortList);
        }

        private void select_kategorie_Click(object sender, RoutedEventArgs e)
        {
            var clicktButtonTag = (sender as Button).Tag.ToString();
            var clicketButtonname = (sender as Button).Name;

            var kategorie = BLL.Kategorie.LesenID(Convert.ToInt64(clicktButtonTag));

            //var passwörter = BLL.Passwort.LesenFremdschluesselGleich(kategorie);

            var kagegorieElement = new M120Projekt.Kategorie(kategorie.KategorieId);

            content.Children.Add(kagegorieElement);
        }

        public void showPassword(int passwordId)
        {
            content.Children.Clear();

            var passwordUserControl = new Passwort();
            passwordUserControl.setPassword(passwordId);

            content.Children.Add(passwordUserControl);
        }

        public void showAbgelaufenePasswörter()
        {
            var passwörter = BLL.Passwort.LesenAlle();
            List<long> abgeloffenePasswörter = new List<long>();
            foreach (DAL.Passwort passwort in passwörter)
            {
                if (passwort.Abgelaufen)
                {
                    abgeloffenePasswörter.Add(passwort.PasswortId);
                }
            }
            var passwortListElement = new M120Projekt.PasswortList(abgeloffenePasswörter);

            content.Children.Clear();
            content.Children.Add(passwortListElement);

            if (abgeloffenePasswörter.Count() > 0)
            {
                MessageBox.Show("Bitte aktualisieren sie folgende Passwörter!");
            }
        }

        public void showDefault()
        {
            content.Children.Clear();
        }

        private void new_button_Click(object sender, RoutedEventArgs e)
        {
            content.Children.Clear();
            var passwordUserControl = new Passwort();

            content.Children.Add(passwordUserControl);
        }

        private void abgeloffene_passwoerter_Click(object sender, RoutedEventArgs e)
        {
            showAbgelaufenePasswörter();
        }

        private void neue_kategorie_Click(object sender, RoutedEventArgs e)
        {
            showDefault();
            var kategorieElement = new M120Projekt.EditKategorie();
            content.Children.Add(kategorieElement);
        }

        public void updateKategorienButtons()
        {
            kategorien.Children.Clear();
            List<DAL.Kategorie> kategorienData = BLL.Kategorie.LesenAlle();


            foreach (var kategorie in kategorienData)
            {
                var button = new Button
                {
                    Content = kategorie.Name,
                    Height = 30,
                    Width = 200,
                    Margin = new Thickness
                    {
                        Top = 5,
                        Bottom = 5
                    },
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    Padding = new Thickness
                    {
                        Left = 5
                    },
                    Tag = kategorie.KategorieId
                };
                button.Click += select_kategorie_Click;
                kategorien.Children.Add(button);

            }
        }
    }
}
