using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AllegroOffersWPF
{
    public partial class MainWindow
    {
        private async void SearchItem(string ProductName) //AllegroRest Rest, string ItemName
        {
            try
            {
                var x = rest.GetTokenJ().Result;
                //show token
                //MessageBox.Show(String.Format("Access Token: {0}", rest.accessToken));

                AllegroOffersWPF.Rootobject searchResponse = rest.requestSearchItem(ProductName);
                GetItemsCollection(searchResponse);
                //searchResponse.categories.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                //return null;
            }            
        }

        private void GetItemsCollection(Rootobject ItemsClass)
        {
            //Dt.Clear();
            //allegroItems.Clear();
            List<AllegroItem> abc = new List<AllegroItem>();
            var items = ItemsClass.items.regular;
            foreach (var item in items)
            {
                AllegroItem itemObj = new AllegroItem();
                itemObj.FreeDelivery = Convert.ToBoolean(item.delivery.availableForFree);
                itemObj.ProductId = Convert.ToInt64(item.id);
                itemObj.ItemName = item.name;

                //delivery
                itemObj.FreeDelivery = item.delivery.availableForFree;
                itemObj.PriceDelivery = Convert.ToDecimal(item.delivery.lowestPrice.amount.Replace(".",","));

                //seller
                itemObj.Company = item.seller.company;
                itemObj.SuperSeller = item.seller.superSeller;
                itemObj.SellerId = Convert.ToInt64(item.seller.id);

                itemObj.PriceItem = Convert.ToDecimal(item.sellingMode.price.amount.Replace(".",","));
                itemObj.StockQuantity = Convert.ToInt32(item.stock.available);

                //allegroItems.Add(itemObj);
                abc.Add(itemObj);
            }
            //return allegroItems;
                        dt = ConvertToDataTable(abc); //dt
            //return ConvertToDataTable(abc); //dt
            //DataTable dt2 = CreateDataTable<AllegroItem>(allegroItems);
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        /*
        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        */
    }
}
