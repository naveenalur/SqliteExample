using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SqlLiteSimple.Model
{
    public class DatabaseHelperClass
    {
        public void CreateDatabase(string DB_PATH)
        {
            if (!CheckFileExists(DB_PATH).Result)
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
                {
                    conn.CreateTable<ContactList>();
                }
            }
        }

        private async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Insert(ContactList objContact)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(objContact);
                });
            }
        }

        public ObservableCollection<ContactList> ReadAllContacts()
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<ContactList> myCollection = conn.Table<ContactList>().ToList<ContactList>();
                    ObservableCollection<ContactList> ContactsList = new ObservableCollection<ContactList>(myCollection);
                    return ContactsList;
                }
            }
            catch
            {
                return null;
            }
        }

        public ObservableCollection<Products> ReadAllProducts()
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<Products> myCollection = conn.Table<Products>().ToList<Products>();
                    ObservableCollection<Products> ProductList = new ObservableCollection<Products>(myCollection);
                    return ProductList;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}