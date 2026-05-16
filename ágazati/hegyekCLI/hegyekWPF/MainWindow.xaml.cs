using hegyekCLI;
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

namespace hegyekWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public static List<Hegy> hegyCsucsok = new List<Hegy>();
        public MainWindow()
        {
            InitializeComponent();
            var Adatok = File.ReadAllLines("hegyek.csv").Skip(1);
            foreach (string sor in Adatok)
            {
                hegyCsucsok.Add(new Hegy(sor));
            }
            dataGrid.ItemsSource = hegyCsucsok;


        }

        private void addButton(object sender, RoutedEventArgs e)
        {
            if (int.Parse(magassagBox.Text) > 0 || int.Parse(magassagBox.Text) < 2000)
            {
                Hegy ujHegy = new Hegy($"{nevBox.Text};{hegysegBox.Text};{magassagBox.Text}");
                hegyCsucsok.Add(ujHegy);
                dataGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show("nem megfelelő értékek");
            }
        }

        private void saveButton(object sender, RoutedEventArgs e)
        {
            try
            {
                string fajlTartalma = "";
                foreach (Hegy hegy in hegyCsucsok)
                {
                    fajlTartalma += $"{hegy.hegyCsucsNeve};{hegy.hegyseg};{hegy.magassag}\n";
                }
                File.WriteAllText("hegycsucsok2.csv", fajlTartalma);
                MessageBox.Show("A mentés sikeresen megtörtént!");
            }
            catch (Exception)
            {

                MessageBox.Show("A sikertelen mentés sikeresen megtörtént");
            }
        }
    }
}