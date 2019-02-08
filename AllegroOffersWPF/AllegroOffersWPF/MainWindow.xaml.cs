using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using AllegroClass;


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

            textBoxClientId.Text = "0069626c63e2456282e868e6ee62cad2";
            textBoxClientSecret.Text = "oUjfWSVkSC4FDNGFHnqBkqJV3mBLPyRpAzadKOpICPF6kq2tFJQSlsaImffod9w4";
            rest = new AllegroRest(textBoxClientId.Text, textBoxClientSecret.Text);

            buttonSearch.IsEnabled = false;

            // grid initialization
            dataGridAllegro.AutoGenerateColumns = true;
            dataGridAllegro.DataContext = allegroItems;
            //dataGridDB.AutoGenerateColumns = true;
            //dataGridDB.ItemsSource = db.InitBinding();
            //dataGridDB.DataMember = "Table";

            DBMethods db = new DBMethods();
            if (!db.CheckDBExists())
            {
                throw new Exception("Błąd podłączenia bazy danych");
            }

            textBoxProductName.Focus();
        }


        #region Button Clicks
        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            buttonSearch.IsEnabled = false;

            //Task T = Task.Run(() => SearchItem(rest, textBoxProductName.Text));
            //Action a = async () => { SearchItem(rest, textBoxProductName.Text); dataGridAllegro.DataContext = Dt.DefaultView; };
            //Task.Run(() => /*Search() */ dataGridAllegro.Dispatcher.InvokeAsync(() => a));
            //dataGridAllegro.DataContext = dt.DefaultView;

            //var Z = await SearchItem(rest, textBoxProductName.Text);
            //dataGridAllegro.DataContext = dt.DefaultView;
            /*
            T.ContinueWith((t) =>
            {
                dataGridAllegro.DataContext = dt.DefaultView;
                //buttonSearch.IsEnabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
            */

            // dziala ale nie async
            /*
            dataGridAllegro.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                new Action(delegate () { Search(); }) );
            */

            //await dataGridAllegro.Dispatcher.InvokeAsync(() => SearchItem(rest, textBoxProductName.Text) ); // dziala alenie async

            //System.Threading.Thread test = new System.Threading.Thread(new System.Threading.ThreadStart(Search));
            //test.Start();
            // SearchItem(rest, textBoxProductName.Text

            /*
            Task T =  Task.Run(SearchItem(rest, textBoxProductName.Text) );
            
            T.ContinueWith((t) =>
            {
                dataGridAllegro.Dispatcher.InvokeAsync(() => dataGridAllegro.DataContext = Dt.DefaultView);
                dataGridAllegro.Dispatcher.InvokeAsync(() => buttonSearch.IsEnabled = true);
            }, TaskScheduler.FromCurrentSynchronizationContext());
            */
            //await Task.Run(() => dataGridAllegro.Dispatcher.InvokeAsync(() => { SearchItem(rest, textBoxProductName.Text); buttonSearch.IsEnabled = true; }));
            //dataGridAllegro.DataContext = Dt.DefaultView;
            //dataGridAllegro.Dispatcher.InvokeAsync(() => dataGridAllegro.DataContext = Dt.DefaultView);
            var productName = textBoxProductName.Text;
            var priceFrom = textBoxPriceFrom.Text;
            var priceTo = textBoxPriceTo.Text;

            await Task.Run(() => SearchItem(productName, priceFrom, priceTo));
            // SearchItem(rest, textBoxProductName.Text);
            dataGridAllegro.ItemsSource = dt.DefaultView;

            //SearchItem(rest, textBoxProductName.Text);
            //dataGridAllegro.DataContext = Dt.DefaultView;
            //dataGridViewAllegro.Refresh(); 

            buttonSearch.IsEnabled = true;
        }
        #endregion

        #region TextBox Events
        private void TextBoxClientId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxClientId.Text))
                buttonSearch.IsEnabled = false;
            else
                buttonSearch.IsEnabled = true;
        }

        private void TextBoxClientSecret_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxClientSecret.Text))
                buttonSearch.IsEnabled = false;
            else
                buttonSearch.IsEnabled = true;
        }

        private void TextBoxProductName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxProductName.Text))
                buttonSearch.IsEnabled = false;
            else
                buttonSearch.IsEnabled = true;
        }
        #endregion
    }
}
