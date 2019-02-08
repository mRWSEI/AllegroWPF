using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using System.IO;

namespace AllegroOffersWPF
{
    public class DBMethods
    {
        public DBMethods()
        {
            //empty constructor
        }

        private SQLiteDataAdapter m_oDataAdapter = null;
        private DataSet m_oDataSet = null;
        private DataTable m_oDataTable = null;
        private SQLiteConnection m_dbConnection;

        private string dbPath = Definitions.dbPATH + Definitions.dbFileName;


        /// <summary>
        /// Method creating error message
        /// </summary>
        /// <param name="Ex"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public string ErrorMessages(Exception Ex, string Message)
        {
            string errorMSG = null;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame(1);
            var methodName = stackFrame.GetMethod().Name;
            errorMSG = $"Błąd {methodName}: {Message} - {Ex.Message}";
            return errorMSG;
        }


        #region Validation sqlite file structure
        /// <summary>
        /// Check if database exist if not then create new one
        /// </summary>
        /// <returns></returns>
        public bool CheckDBExists()
        {
            if(File.Exists($"{dbPath}"))
            { return true; }
            else
            {
                try
                {
                    if(CreateDB())
                        return true;
                    else
                        throw new Exception();
                }
                catch(Exception ex)
                {
                    throw new Exception(ErrorMessages(ex, "Baza nie istnieje lub nie udało się utworzyć nowej"));
                }
            }
        }

        /// <summary>
        /// Create sqlite file and fills it with tables
        /// </summary>
        /// <returns></returns>
        public bool CreateDB()
        {
            try
            {
                if(!File.Exists(dbPath))
                {
                    SQLiteConnection.CreateFile(Definitions.dbFileName);
                }

                if(!CreateDBTable(dbPath))
                    return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ErrorMessages(ex, "Nie udało się utworzyć bazy danych"));
            }
            return true;
        }

        /// <summary>
        /// Create table allegroOffers if not exist
        /// </summary>
        /// <param name="dbPath"></param>
        /// <returns></returns>
        private bool CreateDBTable(string dbPath)
        {
            try
            {
                using(SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    m_dbConnection.Open();

                    string sql =
                    @"CREATE TABLE IF NOT EXISTS AllegroOffers 
                    (
                    Id int identity(1,1) PRIMARY KEY,
                    AllegroOfferId int,
                    AllegroOfferName varchar(200),
                    AllegroSellerName varchar(200),
                    AllegroSellerId varchar(200),
                    AllegroCategoryId int,
                    AllegroOfferPrice decimal(18,2),
                    Insert_Date Datetime DEFAULT CURRENT_TIMESTAMP
                    )";

                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    
                    var count = command.ExecuteNonQuery();


                    m_dbConnection.Close();
                }
            }
            catch(SQLiteException exSql)
            {
                throw new Exception(ErrorMessages(exSql, "Zapytanie SQL zwróciło błąd"));
            }
            catch(Exception ex)
            {
                throw new Exception(ErrorMessages(ex, "Inny błąd"));
            }
            return true;
        }
        #endregion

        public DataSet InitBinding()
        {
            DataTable dt = new DataTable();
            DataSet m_oDataSet = new DataSet();
            try
            {
                using(SQLiteConnection oSQLiteConnection = new SQLiteConnection($"Data Source={dbPath}"))
                {
                    oSQLiteConnection.Open();

                    string sql = "SELECT * FROM AllegroOffers";
                    using(SQLiteCommand oCommand = new SQLiteCommand(sql, oSQLiteConnection))
                    {
                        using(SQLiteDataAdapter m_oDataAdapter = new SQLiteDataAdapter(oCommand))
                        {
                            m_oDataAdapter.Fill(m_oDataSet);
                        }


                        if(m_oDataSet.Tables.Count > 0)
                        {
                            //m_oDataAdapter.Fill(m_oDataSet);
                            //m_oDataTable = m_oDataSet.Tables[0];
                            return m_oDataSet;
                        }
                        else
                            return null;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ErrorMessages(ex, "Błąd podczas pobierania danych z bazy"));
            }
            //return m_oDataTable.DefaultView;
        }

        public void OpenDB()
        {
            m_dbConnection =
                new SQLiteConnection($"Data Source={Definitions.dbFileName};Version=3;");
            m_dbConnection.Open();
            string sql = "select * from CEDIGRecords";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            //command.ExecuteNonQuery();
            SQLiteDataReader reader = command.ExecuteReader();
        }

        public DataTable SelectData()
        {
            DataTable dt = null;
            string sql = "SELECT * From AllegroOffers";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command = new SQLiteCommand(sql, m_dbConnection);
            //dt = command.ExecuteReader();

            return dt;
        }

        public void InsertData()
        {
            /*           
                INSERT INTO AllegroOffers
       (AllegroOfferId, AllegroOfferName, AllegroSellerName,
       AllegroSellerId, AllegroCategoryId, AllegroOfferPrice,
       InsertDate)
       VALUES
       (233, 'Galaxy S7 Gwarancja', 'SmartTel',
       74321, 12, 720, 
       '2018-11-13 21:02:00')
        */
            string sql = "insert into highscores (name, score) values ('Me', 9001)";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        public void AddRecord()
        {
                DataRow oDataRow = m_oDataTable.NewRow();
                oDataRow[0] = 1;
                oDataRow[1] = "Jan";
                oDataRow[2] = "Kowalski";
                oDataRow[3] = 25;
                m_oDataTable.Rows.Add(oDataRow);
                m_oDataAdapter.Update(m_oDataSet);
        }

        public void EditRecord()
        {
            /* to do formsa 
            if(0 == lstItems.SelectedItems.Count)
            {
                return;
            }
            
            DataRow oDataRow = ((DataRowView)lstItems.SelectedItem).Row;
            */
            DataRow oDataRow= null; // dla bezlednej komp
            oDataRow.BeginEdit();
            oDataRow[2] = "Nowak";
            oDataRow[3] = 28;
            oDataRow.EndEdit();
            m_oDataAdapter.Update(m_oDataSet);
        }

        public void DeleteRecord()
        {
            /* forms
            if(0 == lstItems.SelectedItems.Count)
            {
                return;
            }
            DataRow oDataRow = ((DataRowView)lstItems.SelectedItem).Row;
            */
            DataRow oDataRow = null; // dla bezblednej
            oDataRow.Delete();
            m_oDataAdapter.Update(m_oDataSet);
        }
    }
}
