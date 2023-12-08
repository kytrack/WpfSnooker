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

namespace WpfSnooker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>///
    public partial class MainWindow : Window
    {
        List<Versenyzo> versenyzoklista = new List<Versenyzo>();
        public MainWindow()
        {
            InitializeComponent();
            File.ReadAllLines("snooker.txt").Skip(1).ToList().ForEach(line => versenyzoklista.Add(new Versenyzo(line)));
            dgTablazat.ItemsSource = versenyzoklista;

            cbOrszagok.ItemsSource = versenyzoklista.OrderBy(x=>x.Orszag).Select(x=>x.Orszag).Distinct();
        }

        private void btnF3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"A világranglistán {versenyzoklista.Count()} versenyző szerepel");
        }

        private void btnF4_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"A versenyzők átlagosan {versenyzoklista.Average(x => x.Nyeremeny):f2} fontot kerestek");
        }

        private void btnF5_Click(object sender, RoutedEventArgs e)
        {

            var legjobbkinai= versenyzoklista.Where(x => x.Orszag==cbOrszagok.SelectedItem).OrderByDescending(x=>x.Nyeremeny).First();
            lblHelyezes.Content = legjobbkinai.Helyezes;
            lblNev.Content = legjobbkinai.Nev;
            lblOrszag.Content = legjobbkinai.Orszag;
            lblNyeremeny.Content = legjobbkinai.Nyeremeny * int.Parse(txtArfolyam.Text);
        }

        private void btnF6_Click(object sender, RoutedEventArgs e)
        {
            bool vaneilyen = versenyzoklista.Select(x => x.Orszag).Contains(txtVanIlyenOrszag.Text);
            MessageBox.Show(vaneilyen?"Van ilyen":"Nincs ilyen");
        }

        private void btnF7_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            versenyzoklista.GroupBy(x => x.Orszag).Where(x => x.Count() >= sliMinLetszam.Value).OrderByDescending(x=>x.Count()).ToList().ForEach(x => list.Add($"{x.Key} : {x.Count()}"));
            lbStatisztika.ItemsSource = list;

        }
    }
}
