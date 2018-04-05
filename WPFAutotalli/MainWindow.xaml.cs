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
using JAMK.IT;

namespace WPFAutotalli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string polku = "d:/K9105/";
        List<Auto> autot = new List<Auto>();

        public MainWindow()
        {
            InitializeComponent();
            //aloituskuva
            NaytaKuva("autotalli.png");
            //ladataan kaikki autot muistiin
            autot = Autotalli.Haeautot();
            //täytetään ComboBox autojen merkeillä
            //VE1: manuaalisesti
            List<string> merkit = new List<string>();
            merkit.Add("Audi");
            merkit.Add("Saab");
            merkit.Add("Volvo");
            //cmbMerkit.ItemsSource = merkit;
            //VE2: automaattisesti LINQ:lla datasta
            var result = autot.Select(m => m.Merkki).Distinct();
            cmbMerkit.ItemsSource = result;
        }
        private void NaytaKuva(string url)
        {
            try
            {
                //lisätään vakiopolku kuvatiedostoon
                if (url != "")
                {
                    url = polku + url;
                    if (System.IO.File.Exists(url))
                    {
                        //kuva nnäyttö
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.UriSource = new Uri(url);
                        bi.EndInit();
                        imgAuto.Stretch = Stretch.Fill;
                        imgAuto.Source = bi;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHaeAutot_Click(object sender, RoutedEventArgs e)
        {
            //dgAutot.ItemsSource = JAMK.IT.Autotalli.Haeautot();
            dgAutot.ItemsSource = autot;
        }

        private void dgAutot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*// olemme itse populoineet DataGridin Auto-olioilla, joten kukin 
            // DataGridin rivi = alkio on itseasiassa Auto-olio
            object obj = dgAutot.SelectedItem;
            if (obj != null)
            {
                Auto valittu = (Auto)obj;
                NaytaKuva(valittu.URL);
            }*/
            // tai lyhyesti
            Auto valittu = dgAutot.SelectedItem as Auto;
            if (valittu != null)
            {
                NaytaKuva(valittu.URL);
            }
            
        }

        private void btnHaeAudit_Click(object sender, RoutedEventArgs e)
        {
            // näkyviin pelkästään audi merkkiset  -> suodatetaan autot-listasta audit
            // LINQ
            var result = autot.Where(m => m.Merkki.Contains("Audi"));
            dgAutot.ItemsSource = result;
        }

        private void cmbMerkit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string merkki = cmbMerkit.SelectedItem.ToString();
            var result = autot.Where(m => m.Merkki.Contains(merkki)).ToList();
            dgAutot.ItemsSource = result;
            NaytaKuva("autotalli.png");
        }
    }
}
