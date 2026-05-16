using GLS_CLI1;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GLS_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<AutoAdatok> autoLista = new List<AutoAdatok>();
        public MainWindow()
        {
            InitializeComponent();
            string[] adatok = File.ReadAllLines("GLS.txt");
            
            foreach (var item in adatok)
            {
                autoLista.Add(new AutoAdatok(item));
            }
            datagrid.ItemsSource = autoLista;
        }

        private void Felvitel_Click(object sender, RoutedEventArgs e)
        {
            if (validalas() == false)
            {
                return;
            }
            if (autoLista.Find(x=>x.datum == datumBox.Text) != null)
            {
                MessageBox.Show("Már van ilyen dátum", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            AutoAdatok adat = new AutoAdatok(datumBox.Text, nevBox.Text, int.Parse(csomagokBox.Text), int.Parse(fogyasztasBox.Text), int.Parse(kmBox.Text));
            autoLista.Add(adat);
            datagrid.Items.Refresh();
        }

        private void Modositas_Click(object sender, RoutedEventArgs e)
        {
            if (validalas() == false)
            {
                return;
            }
            autoLista.Remove(datagrid.SelectedItem as AutoAdatok);
            if (autoLista.Find(x => x.datum == datumBox.Text) != null)
            {
                MessageBox.Show("Már van ilyen dátum", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            AutoAdatok adat = new AutoAdatok(datumBox.Text, nevBox.Text, int.Parse(csomagokBox.Text), int.Parse(fogyasztasBox.Text), int.Parse(kmBox.Text));
            autoLista.Add(adat);
            datagrid.Items.Refresh();
            
        }
        private bool validalas()
        {
            
            if(datumBox.Text==""||nevBox.Text==""||csomagokBox.Text==""||fogyasztasBox.Text=="") 
            {
                MessageBox.Show("Hibás vagy hiányzó adat!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false; 
            }
            DateOnly datum;
            

            if (DateOnly.TryParse(datumBox.Text, out datum) == false || datumBox.Text.Length!=10)
            {
                MessageBox.Show("Hibás vagy hiányzó adat!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            try
            { 
                if (int.Parse(csomagokBox.Text) < 0 || int.Parse(fogyasztasBox.Text) < 0 || int.Parse(kmBox.Text) < 0)
                {
                    MessageBox.Show("Csak pozítiv nem nulla szám adható meg a beviteli mezőkben!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch(FormatException ex)
            {
                MessageBox.Show("Csak pozítiv nem nulla szám adható meg a beviteli mezőkben!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                if (datagrid.SelectedItem is AutoAdatok)
                {
                    datumBox.Text = ((AutoAdatok)datagrid.SelectedItem).datum;
                    nevBox.Text = ((AutoAdatok)datagrid.SelectedItem).sofNev;
                    csomagokBox.Text = ((AutoAdatok)datagrid.SelectedItem).kezbCsomagSzam.ToString();
                    fogyasztasBox.Text = ((AutoAdatok)datagrid.SelectedItem).napiFogyasztasLiter.ToString();
                    kmBox.Text = ((AutoAdatok)datagrid.SelectedItem).napiKilometer.ToString();

                }
            }
            
        }

        private void Mentes_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                List<string> adatok = new List<string>();
                foreach(var item in autoLista)
                {
                    adatok.Add($"{item.datum};{item.sofNev};{item.napiKilometer};{item.kezbCsomagSzam};{item.napiFogyasztasLiter}");
                }
                File.WriteAllLines("GLS.txt", adatok);
                MessageBox.Show("Sikeres Mentés!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Sikertelen Mentés!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}