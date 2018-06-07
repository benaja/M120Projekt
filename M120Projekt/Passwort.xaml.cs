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
    /// Interaction logic for Passwort.xaml
    /// </summary>
    public partial class Passwort : UserControl
    {
        private DAL.Passwort passwort;
        public Passwort()
        {
            InitializeComponent();

            var kategorien = BLL.Kategorie.LesenAlle();

            //DAL.Kategorie selectedKategorie = passwort.Kategorie;

            foreach (DAL.Kategorie kategorieModel in kategorien)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = kategorieModel.Name;
                item.Value = kategorieModel.KategorieId;

                kategorie.Items.Add(item);
            }

            erstelldatum.Content = DateTime.Now.ToString("dd.MM.yyyy");
        }

        public void setPassword(int passwortId)
        {
            passwort = BLL.Passwort.LesenID(passwortId);

            login.Text = passwort.Login;

            ComboboxItem selectedItem = new ComboboxItem
            {
                Text = passwort.Kategorie.Name,
                Value = passwort.Kategorie.KategorieId
            };

            kategorie.Text = passwort.Kategorie.Name;

            zielsystem.Text = passwort.Zielsystem;

            ablaufdatum.SelectedDate = passwort.Ablaufdatum;

            erstelldatum.Content = passwort.Eingabedatum.ToString("dd.MM.yyyy");

            password_buttons.Visibility = Visibility.Visible;
            set_new_password.Visibility = Visibility.Hidden;
            save_button.IsEnabled = false;
            delete_button.Visibility = Visibility.Visible;
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            if(passwort != null)
            {
                Boolean passwortValidation = true;
                if (set_new_password.Visibility == Visibility.Visible)
                {   
                    if(String.IsNullOrWhiteSpace(new_passwort.Password))
                    {
                        MessageBox.Show("Geben sie ein Passwort ein!");
                        passwortValidation = false;
                    }
                    else if (new_passwort.Password != repeat_password.Password)
                    {
                        MessageBox.Show("Passwörter stimmen nicht überein!");
                        passwortValidation = false;
                    }
                    else
                    {
                        passwort.PSW = new_passwort.Password;
                    }
                }
                if (passwortValidation)
                {
                    passwort.Zielsystem = zielsystem.Text;
                    passwort.Login = login.Text;
                    passwort.Ablaufdatum = ablaufdatum.SelectedDate.Value;
                    var selectedItem = (ComboboxItem)kategorie.SelectedItem;
                    passwort.Kategorie = BLL.Kategorie.LesenID(selectedItem.Value);

                    BLL.Passwort.Aktualisieren(passwort);
                    MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
                    parentWindow.showDefault();
                }
            }
            else
            {
                if(String.IsNullOrWhiteSpace(new_passwort.Password))
                {
                    MessageBox.Show("Geben sie ein Passwort ein!");
                }
                else if(new_passwort.Password != repeat_password.Password)
                {
                    MessageBox.Show("Passwörter stimmen nicht überein!");
                }
                else
                {
                    passwort = new DAL.Passwort();
                    var selectedKategorie = (ComboboxItem)kategorie.SelectedItem;
                    passwort.Kategorie = BLL.Kategorie.LesenID(selectedKategorie.Value);
                    passwort.Zielsystem = zielsystem.Text;
                    passwort.Login = login.Text;
                    passwort.Ablaufdatum = ablaufdatum.SelectedDate.Value;
                    passwort.Eingabedatum = DateTime.Now;
                    passwort.PSW = new_passwort.Password;

                    BLL.Passwort.Erstellen(passwort);

                    MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
                    parentWindow.content.Children.Clear();
                }
            }
        }

        private void show_password_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Soll das Passwort wirklich angezeigt werden?", "Information", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                MessageBox.Show($"Ihr Passwort lautet: {passwort.PSW}", "Passwort");
            }
        }

        private void edit_password_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Soll das Passwort wirklich geändert werden?", "Information", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                set_new_password.Visibility = Visibility.Visible;
                activateSaveButton();
            }
        }

        private void kategorie_DropDownClosed(object sender, EventArgs e)
        {
            activateSaveButton();
        }

        private void ablaufdatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            activateSaveButton();
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.showDefault();
        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            BLL.Passwort.Loeschen(BLL.Passwort.LesenID(passwort.PasswortId));
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.showDefault();
        }

        private void zielsystem_TextChanged(object sender, TextChangedEventArgs e)
        {
            activateSaveButton();
        }

        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {
            activateSaveButton();
        }

        private void activateSaveButton()
        {
            if (validiereInput())
            {
                save_button.IsEnabled = true;
            }
            else
            {
                save_button.IsEnabled = false;
            }
        }

        private Boolean validiereInput()
        {
            if(String.IsNullOrWhiteSpace(zielsystem.Text))
            {
                return false;
            }
            if(String.IsNullOrWhiteSpace(login.Text))
            {
                return false;
            }
            if(kategorie.SelectedItem == null)
            {
                return false;
            }
            if(ablaufdatum.SelectedDate == null)
            {
                return false;
            }
            return true;
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public long Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
