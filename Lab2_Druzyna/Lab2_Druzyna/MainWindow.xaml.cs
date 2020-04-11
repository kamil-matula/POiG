using System;
using System.Collections.Generic;
using System.IO;
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

namespace Lab2_Druzyna
{
    public partial class MainWindow : Window
    {
        private double max = 100, min = 50;
        private List<Player> playerList;
        private List<int> ageList;
        private bool temporaryboolean = true;
        public MainWindow()
        {
            InitializeComponent();
            playerList = new List<Player>(); players_lbx.ItemsSource = playerList;
            ageList = new List<int>(); for (int i = 18; i <= 50; i++) ageList.Add(i);
            age_cmbx.ItemsSource = ageList; age_cmbx.Items.Refresh(); age_cmbx.SelectedIndex = 0;
            if (File.Exists(@"data.txt"))
            {
                using (StreamReader sr = File.OpenText(@"data.txt")) 
                {
                    string s;
                    while ((s = sr.ReadLine()) != null) 
                    {
                        string[] data = s.Split(' ');
                        playerList.Add(new Player(data[0], data[1], int.Parse(data[2]), double.Parse(data[3])));
                        players_lbx.Items.Refresh();
                    }
                }
            }
        }

        private void textHasChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtbx = (TextBox)sender;
            if (txtbx.Text == "")
            {
                txtbx.BorderBrush = Brushes.Red;
                txtbx.BorderThickness = new Thickness(2);
                txtbx.ToolTip = "Uzupełnij pole!";
            }
            else
            {
                txtbx.BorderBrush = Brushes.Black;
                txtbx.BorderThickness = new Thickness(1);
                txtbx.ToolTip = null;
            }
        }

        private void isFocused(object sender, RoutedEventArgs e)
        {
            TextBox txtbx = (TextBox)sender;
            if (txtbx.Text == "Podaj imię" || txtbx.Text == "Podaj nazwisko")
            {
                txtbx.Foreground = Brushes.Black;
                txtbx.Text = "";
            }
        }

        private void addClicked(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox_txtbx.Text;
            string lastname = lastnameTextBox_txtbx.Text;
            int age = int.Parse(age_cmbx.Text);
            double weight = double.Parse(weight_txtblck.Text);
            if (name == "" || lastname == "" || name == "Podaj imię" || lastname == "Podaj nazwisko") {
                MessageBox.Show("Uzupełnij wszystkie pola!", "Wyskakujące łokienko");
            }
            else
            {
                Player newplayer = new Player(name, lastname, age, weight);
                playerList.Add(newplayer);
                players_lbx.Items.Refresh();
            }
        }

        private void deleteClicked(object sender, RoutedEventArgs e)
        {
            if (players_lbx.SelectedIndex == -1) MessageBox.Show("Zaznacz zawodnika!", "Wyskakujące łokienko");
            else {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć?", "Wyskakujące łokienko", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) 
                {
                    temporaryboolean = false;
                    playerList.RemoveAt(players_lbx.SelectedIndex);
                    players_lbx.Items.Refresh();
                    temporaryboolean = true;
                }
            }
        }

        private void modifyClicked(object sender, RoutedEventArgs e)
        {
            if (players_lbx.SelectedIndex == -1) MessageBox.Show("Zaznacz zawodnika!", "Wyskakujące łokienko");
            else
            {
                string name = nameTextBox_txtbx.Text;
                string lastname = lastnameTextBox_txtbx.Text;
                int age = int.Parse(age_cmbx.Text);
                double weight = double.Parse(weight_txtblck.Text);
                if (name == "" || lastname == "" || name == "Podaj imię" || lastname == "Podaj nazwisko")
                    MessageBox.Show("Uzupełnij wszystkie pola!", "Wyskakujące łokienko");
                else
                {
                    MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz zmodyfikować?", "Wyskakujące łokienko", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        Player newplayer = new Player(name, lastname, age, weight);
                        playerList[players_lbx.SelectedIndex] = newplayer;
                        temporaryboolean = false;
                        players_lbx.Items.Refresh();
                        temporaryboolean = true;
                    }
                }
            }
        }

        private void itemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (playerList.Count > 0 && temporaryboolean == true)
            {
                Player newplayer = new Player(playerList[players_lbx.SelectedIndex]);
                nameTextBox_txtbx.Text = newplayer.Imie;
                nameTextBox_txtbx.Foreground = Brushes.Black;
                lastnameTextBox_txtbx.Foreground = Brushes.Black;
                lastnameTextBox_txtbx.Text = newplayer.Nazwisko;
                age_cmbx.SelectedIndex = ageList.IndexOf(newplayer.Wiek);
                weight_slr.Value = Math.Round((newplayer.Waga - min) / (max - min) * 10, 2);
            }
        }

        private void windowClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (StreamWriter sw = File.CreateText(@"data.txt"))
                for (int i = 0; i < playerList.Count; i++)
                    sw.WriteLine(playerList[i].Imie + " " + playerList[i].Nazwisko + " " + playerList[i].Wiek + " " + playerList[i].Waga);
        }

        private void sliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double value = e.NewValue;
            value = Math.Round(value / 10 * (max - min) + min, 1);
            weight_txtblck.Text = value.ToString();
        }
    }
}