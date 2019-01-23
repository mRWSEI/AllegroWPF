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
using System.Data;
using Newtonsoft.Json;

namespace AllegroOffersWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AllegroRest rest;

        List<AllegroItem> allegroItems = new List<AllegroItem>();
        DataTable dt;
        public DataTable Dt { get => dt; set { dt = value; dataGridAllegro.DataContext = Dt.DefaultView; } }


        public MainWindow()
        {
            //http://www.altcontroldelete.pl/artykuly/c-wpf-oraz-sqlite-razem-w-jednym-projekcie/


            InitializeComponent();

            dataGridAllegro.AutoGenerateColumns = true;
            //dataGridAllegro.ItemsSource = allegroItems;
            dataGridAllegro.DataContext = allegroItems;

            DBMethods db = new DBMethods();
            if (!db.CheckDBExists())
            {
                throw new Exception("Błąd podłączenia bazy danych");
            }

            // inicjalizacja grida
            //dataGridDB.AutoGenerateColumns = true;
            //dataGridDB.ItemsSource = db.InitBinding();
            //dataGridDB.DataMember = "Table";
        }


        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {


            //buttonSearch.IsEnabled = false;
            rest = new AllegroRest(textBoxClientId.Text, textBoxClientSecret.Text);
            /*Task T = Task.Run(() => SearchItem(rest, textBoxProductName.Text));

            T.ContinueWith((t) =>
            {
                dataGridAllegro.DataContext = dt.DefaultView;
                //buttonSearch.IsEnabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
            */
            SearchItem(rest, textBoxProductName.Text);
            //dataGridAllegro.DataContext = Dt.DefaultView;
            //dataGridViewAllegro.Refresh(); 
        }
    }
}
