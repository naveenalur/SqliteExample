using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Simple.OData.Client;
using SqlLiteSimple.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace SqlLiteSimple.ViewModel
{
    public class DetailSViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IDialogService _dialogService;

        public DatabaseHelperClass Db_Helper;

        public bool flag = false;

        public string BackButton => ViewModelLocator.BackButton;
        public string SyncButton => ViewModelLocator.SyncButton;
        public string CancleButton => ViewModelLocator.CancleButton;

        public DetailSViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;

            _dialogService = dialogService;
            MessengerInstance.Register<ContactList>(this, p => GetContacts(p));
            Db_Helper = new DatabaseHelperClass();
        }

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        private long _number;

        public long Number
        {
            get { return _number; }
            set { _number = value; RaisePropertyChanged("Number"); }
        }

        public ObservableCollection<Products> _productsListCollection;

        public ObservableCollection<Products> ProductsListCollection
        {
            get { return _productsListCollection; }
            set { _productsListCollection = value; RaisePropertyChanged("ProductsListCollection"); }
        }

        bool _isPullRefresh = false;
        public bool IsPullRefresh
        {
            get
            {
                return _isPullRefresh;
            }

            set
            {
                _isPullRefresh = value;
                RaisePropertyChanged("IsPullRefresh");
            }
        }

        private RelayCommand<object> _scrollCommand;
        public RelayCommand<object> ScrollCommand
        {
            get
            {
                if (_scrollCommand == null)
                {
                    _scrollCommand = new RelayCommand<object>((data) => scrollViewer_ViewChanged(data));


                }
                return _scrollCommand;
            }
        }

        public ObservableCollection<object> Items { get; set; }
        private async void scrollViewer_ViewChanged(object sender)
        {
            //var sv = sender as ScrollViewer;

            //if (!e.IsIntermediate)
            //{
            //if (sv.VerticalOffset == 0.0)
            //{
            //    IsPullRefresh = true;
            //    await Task.Delay(2000);
            //    for (int i = 0; i < 5; i++)
            //    {
            //        Items.Insert(0, i);
            //    }
            //    sv.ChangeView(null, 30, null);
            //}
            //IsPullRefresh = false;
            //}
        }

        private ObservableCollection<ContactList> _ContactLists;

        public ObservableCollection<ContactList> ContactCollections
        {
            get
            {
                return _ContactLists;
            }
            set
            {
                _ContactLists = value;
                RaisePropertyChanged("ContactCollections");
            }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged("Id"); }
        }

        private bool _isProgress;

        public bool IsProgress
        {
            get
            {
                return _isProgress;
            }
            set
            {
                _isProgress = value;
                RaisePropertyChanged("IsProgress");
            }
        }

        private bool _isHits = true;

        public bool isHits
        {
            get
            {
                return _isHits;
            }
            set
            {
                _isHits = value;
                RaisePropertyChanged("isHits");
            }
        }

        /*************** Commands Area ***********************/

        public RelayCommand<object> _updateCommand;

        public RelayCommand<object> UpdateCommand
        {
            get
            {
                if (_updateCommand == null)
                {
                    _updateCommand = new RelayCommand<object>(UpdateData);
                }
                return _updateCommand;
            }
        }

        private RelayCommand<string> _ClickEventCommand;
        public RelayCommand<string> ClickEventCommand => _ClickEventCommand ?? (_ClickEventCommand = new RelayCommand<string>((buttonName) => ClikEvenButton(buttonName)));






        /********** Method Area *************/
        private void ClikEvenButton(string buttonName)
        {
            switch (buttonName)
            {
                case ViewModelLocator.BackButton:
                    _navigationService.GoBack();
                    break;

                case ViewModelLocator.SyncButton:
                    syncProductData();
                    syncCauseData();
                    break;

                case ViewModelLocator.CancleButton:
                    _navigationService.GoBack();
                    break;
            }
        }
        private void GetContacts(ContactList p)
        {
            ContactList c = p;
            Name = p.Name;
            Number = p.Number;
            Id = p.Id;
        }
        private async void UpdateData(object data)
        {
            DetailSViewModel ObjContact = ((DetailSViewModel)data);
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingconact = conn.Query<ContactList>("select * from ContactList where Id =" + Id).FirstOrDefault();
                if (existingconact != null)
                {
                    existingconact.Name = Name;
                    existingconact.Number = Number;
                    existingconact.UpdateDate = DateTime.Now.ToString();
                    conn.RunInTransaction(() =>
                      {
                          conn.Update(existingconact);
                      });
                }
            }

            MessageDialog messageDialog = new MessageDialog("Updated Successes fully");
            await messageDialog.ShowAsync();
            _navigationService.GoBack();
        }
        private async void syncProductData()
        {
            IsProgress = true;
            isHits = false;
            try
            {
                if (flag == false)
                {
                    var x = ODataDynamic.Expression;

                    var client = new ODataClient("https://services.odata.org/V4/(S(bnsnosiufp5dcvccgvqylhxc))/OData/OData.svc");
                    var sproductList = await client.For("Products").FindEntriesAsync();
                    foreach (var data in sproductList)
                    {
                        int ID = int.Parse(data["ID"].ToString());

                        using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                        {
                            conn.RunInTransaction(() =>
                            {
                                conn.Insert(new Products(data["Name"].ToString(), ID, data["Description"].ToString()));
                            });
                        }
                    }
                    flag = true;
                    IsProgress = false;
                    isHits = true;

                    await _dialogService.ShowMessage("Sync Successfully Check Your Data Base..!", "Success");
                    ProductsListCollection = new DatabaseHelperClass().ReadAllProducts();
                }
                else
                {
                    await _dialogService.ShowMessage("Already Sync..!", "Warning");
                    IsProgress = false;
                    isHits = true;
                }

            }
            catch (Exception e)
            {
                await _dialogService.ShowMessage(e.Message, "Warning");
            }
        }


        private async void syncCauseData()
        {
           // flag == false;
            IsProgress = true;
            isHits = false;
            try
            {
                if (flag == false)
                {
                    var x = ODataDynamic.Expression;
                    var client = new ODataClient("http://NWGateway.indience.in:8000/sap/opu/odata/sap/ZGWS_PM_RWO_SRV/");
                    

                    var sproductList = await client.For("CauseSet").FindEntriesAsync();
                    var data = sproductList;
                    //foreach (var data in sproductList)
                    //{
                    //    int ID = int.Parse(data["ID"].ToString());

                    //    using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                    //    {
                    //        conn.RunInTransaction(() =>
                    //        {
                    //            conn.Insert(new Products(data["Name"].ToString(), ID, data["Description"].ToString()));
                    //        });
                    //    }
                    //}
                    //flag = true;
                    //IsProgress = false;
                    //isHits = true;

                    //await _dialogService.ShowMessage("Sync Successfully Check Your Data Base..!", "Success");
                    //ProductsListCollection = new DatabaseHelperClass().ReadAllProducts();
                }
            }
            catch(Exception e)
            {
                await _dialogService.ShowMessage(e.Message, "Warning");

            }
            }
    }
}