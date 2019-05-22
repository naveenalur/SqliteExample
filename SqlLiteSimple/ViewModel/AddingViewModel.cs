using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using SqlLiteSimple.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using System.IO;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.Storage.Streams;
using SqlLiteSimple.Views;
using Windows.System;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.UI.Popups;

namespace SqlLiteSimple.ViewModel
{
    public class SimpleDemoData
    {
        public string GTitle { get; set; }
        public int Id { get; set; }
        public int count { get; set; }
        public string Image { get; set; }
    }

    public class AddingViewModel : ViewModelBase
    {
        private string _Name = string.Empty;
        private int _Id = 0;
        private long _Number = 0;
        public string GoBack => ViewModelLocator.GoBack;
        public string GetCollectionDataButton => ViewModelLocator.GetCollectionDataButton;

        private string _textData = string.Empty;
        private readonly IDialogService _dialogService;
        private ObservableCollection<ContactList> _ContactListCollection;
        private ObservableCollection<SimpleDemoData> _WorkOrder;

        public AddingViewModel(IDialogService dialogService, INavigationService navigationService)
        {
            IsActivitySplitViewPaneOpen = false;
            _dialogService = dialogService;
            _navigationService = navigationService;
            ContactListCollection = new ObservableCollection<ContactList>();
            int counts = ContactListCollection.Count;
            //VideosObservableCollections = GetVideos();
            WorkOrder = new ObservableCollection<SimpleDemoData>() {
            new SimpleDemoData {GTitle="Equipments",Id=1, count=3,Image="/Images/Big/Artboard 1@4x.png" },
            new SimpleDemoData {GTitle="Inspections",Id=2 ,count=53,Image="/Images/Big/Artboard 1 copy 3@4x.png"},
            new SimpleDemoData {GTitle="Notifications",Id=3,count=counts,Image="/Images/Big/Artboard 1 copy@4x.png"},
            new SimpleDemoData {GTitle="WorkOrders",Id=4 ,count=90,Image="/Images/Big/Artboard 1 copy 2@4x.png"}
            };
            NotificationsStatus = new ObservableCollection<string>() { "Completed", "Progress", "Submitted" };
          // TreeItems = BuildTree(5, 5);
            Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs
        }

        private string m_ReceivedQuantity = "0";

        public string ReceivedQuantity
        {
            get
            {
                return m_ReceivedQuantity;
            }

            set
            {
                m_ReceivedQuantity = value;
                RaisePropertyChanged("ReceivedQuantity");

                try
                {
                    Db_Helper.Insert(new ContactList("Name",11111, recievedData: m_ReceivedQuantity));
                }
                catch (Exception ex)
                {
                   
                }
            }
        }

        public ContactList SelectedContactList
        {
            get => _SelectedContactList;
            set
            {
                _SelectedContactList = value;
                RaisePropertyChanged("SelectedContactList");
            }
        }
        public ObservableCollection<ContactList> ContactListCollection
        {
            get
            {
                return _ContactListCollection;
            }
            set
            {
                _ContactListCollection = value;
                RaisePropertyChanged("ContactListCollection");
            }
        }

        public ObservableCollection<SimpleDemoData> WorkOrder
        {
            get
            {
                return _WorkOrder;
            }
            set
            {
                _WorkOrder = value;
                RaisePropertyChanged("WorkOrder");
            }
        }

        private ObservableCollection<string> n_status;

        public ObservableCollection<string> NotificationsStatus
        {
            get
            {
                return n_status;
            }
            set
            {
                n_status = value;
                RaisePropertyChanged("NotificationsStatus");
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string TextData
        {
            get
            {
                return _textData;
            }

            set
            {
                _textData = value;
                RaisePropertyChanged("TextData");
            }
        }

        public long Number
        {
            get
            {
                return _Number;
            }

            set
            {
                _Number = value;
                RaisePropertyChanged("Number");
            }
        }

        public int Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
                RaisePropertyChanged("Id");
            }
        }


        public bool IsActivitySplitViewPaneOpen
        {
            get => _IsActivitySplitViewPaneOpen;
            set
            {
                _IsActivitySplitViewPaneOpen = value;
                RaisePropertyChanged("IsActivitySplitViewPaneOpen");
            }
        }

        public DatabaseHelperClass Db_Helper;
        private INavigationService _navigationService;

        private ObservableCollection<ContactList> _ContactList;

        public ObservableCollection<ContactList> ContactList
        {
            get
            {
                return _ContactList;
            }
            set
            {
                _ContactList = value;
                RaisePropertyChanged("ContactList");
            }
        }

        private List<string> names = new List<string>() { "Inspections", "Notification", "Equipment" };

        public List<string> NamesFor
        {
            get { return names; }
            set { names = value; RaisePropertyChanged("NamesFor"); }
        }

        public ObservableCollection<ContactList> LoadAddedData()
        {
            ContactListCollection = Db_Helper.ReadAllContacts();
            return ContactListCollection;
        }

        private ICommand _SelectedContactListCommand;

        public ICommand SelectedContactListCommand
        {
            get
            {
                if (_SelectedContactListCommand == null)
                {
                    _SelectedContactListCommand = new RelayCommand<ItemClickEventArgs>(ListItemClickedData);
                }

                return _SelectedContactListCommand;
            }
        }

        private void ListItemClickedData(ItemClickEventArgs obj)
        {
            ContactList item = obj?.ClickedItem as ContactList;
            SelectedContactList = item;
            IsActivitySplitViewPaneOpen = false;
        }

        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand
        {
            get
            {
                if (_itemClickCommand == null)
                {
                    _itemClickCommand = new RelayCommand<ItemClickEventArgs>(OnItemClick);
                }

                return _itemClickCommand;
            }
        }
        private ICommand _itemClickImageCommand;

        public ICommand ItemClickImageCommand
        {
            get
            {
                if (_itemClickImageCommand == null)
                {
                    _itemClickImageCommand = new RelayCommand<ItemClickEventArgs>(OnItemImageClick);
                }

                return _itemClickImageCommand;
            }
        }

        private void OnItemImageClick(ItemClickEventArgs obj)
        {
           
        }

        private RelayCommand _AddPictureCommand;

        public RelayCommand AddPictureCommand
        {
            get {
                if(_AddPictureCommand==null)
                {
                    _AddPictureCommand = new RelayCommand(SaveImagesInDb);
                }
                return _AddPictureCommand;
            }
        }



        private RelayCommand _AddVideosCommand;

        public RelayCommand AddVideosCommand
        {
            get
            {
                if (_AddVideosCommand == null)
                {
                    _AddVideosCommand = new RelayCommand(SaveVideosInDb);
                }
                return _AddVideosCommand;
            }
        }

        private async void SaveVideosInDb()
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.VideosLibrary;
            picker.FileTypeFilter.Add(".mp4");

            StorageFile files = await picker.PickSingleFileAsync();

            if (files != null)
            {
                Videos videos = new Videos();
                
                videos.Video= await ConvertImageToByte(files);
                videos.Name = files.Name;
                    using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                    {
                        conn.RunInTransaction(() =>
                        {
                            conn.Insert(videos);
                        });
                    }
                VideosObservableCollections = GetVideos();
                await _dialogService.ShowMessage("Videos", "Videos Saved Sucessfully...!");
                }
            }

        public string CameraButton => ViewModelLocator.CameraButton;

        private RelayCommand<string> m_ClickEventCommand;

        public RelayCommand<string> ClickEventCommand => m_ClickEventCommand ?? (m_ClickEventCommand = new RelayCommand<string>((eventRaiserButtonName) => ClickEventHandler(eventRaiserButtonName)));
         private RelayCommand<object> _buttonSaveCommand;

        public RelayCommand<object> SaveImageAnnotateCommand => _buttonSaveCommand ?? (_buttonSaveCommand = new RelayCommand<object>((eventRaiserButtonName) => ClickAnnotate(eventRaiserButtonName)));

        private void ClickAnnotate(object eventRaiserButtonName)
        {
            var image = eventRaiserButtonName as Image;
           var data= ((SqlLiteSimple.ViewModel.AddingViewModel)image.DataContext).ImagePathForShow;
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Insert(data);
                    });
                }
            }
            catch(Exception ex)
            {
                _dialogService.ShowMessage(ex.Message, "Error");
            }

            

        }

        private void ClickEventHandler(string eventRaiserButtonName)
        {
            switch (eventRaiserButtonName)
            {
                case ViewModelLocator.CameraButton:
                    TakePhotoButtonClickedAsync();
                    break;
                case ViewModelLocator.GetCollectionDataButton:
                    LoadAddedData();
                    IsActivitySplitViewPaneOpen = true;
                    break;

            }
        }
       public MediaCapture mediaCapture;
        private async Task TakePhotoButtonClickedAsync()
        {
            mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync();
            var lowLagCapture = await mediaCapture.PrepareLowLagPhotoCaptureAsync(ImageEncodingProperties.CreateUncompressed(MediaPixelFormat.Bgra8));

            var capturedPhoto = await lowLagCapture.CaptureAsync();
            var softwareBitmap = capturedPhoto.Frame.SoftwareBitmap;

            await lowLagCapture.FinishAsync();
            //var cam = new CameraCaptureUI();
            //cam.PhotoSettings.MaxResolution = _resolution;
            //cam.PhotoSettings.AllowCropping = true;

            //var capturedFile = await cam.CaptureFileAsync(CameraCaptureUIMode.Photo);

            //if (capturedFile == null) return;
            //if (!string.IsNullOrEmpty(capturedFile.Name))
            //{
            //    await AddFileToAttachmentListAsync(capturedFile);
            //}
            //var myVideos = await Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Videos);
            //StorageFile file = await myVideos.SaveFolder.CreateFileAsync("video.mp4", CreationCollisionOption.GenerateUniqueName);
            //_mediaRecording = await mediaCapture.PrepareLowLagRecordToStorageFileAsync(
            //        MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Auto), file);
            //await _mediaRecording.StartAsync();
        }

        private async Task AddFileToAttachmentListAsync(StorageFile capturedFile)
        {
            if (capturedFile == null) return;

            if (capturedFile != null)
            {
                Picture picture = new Picture();
                picture.Image = await ConvertImageToByte(capturedFile);
                picture.Name = capturedFile.Properties.GetImagePropertiesAsync().ToString();
                picture.Imagepath =null;
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Insert(picture);
                    });
                }

                //foreach (var item in capturedFile)
                //{
                //    var data = item.Properties.GetImagePropertiesAsync();
                //    picture.Image = await ConvertImageToByte(item);
                //    picture.Name = item.Name;
                //    picture.Imagepath = item.Path;
                   
                //}
                //if (file != null)
                //{

                //Picture picture = new Picture();
                //picture.Image = await ConvertImageToByte(file);
                //picture.Name = file.Name;


                ImagesObservableCollections = GetImagesFromDb();
                await _dialogService.ShowMessage("Saved", "SucessFully Saved...!");


            }
            //  var file = await Attachments.CopyFileToLocalFolder(capturedFile, capturedFile.Name);
            //  if (file == null) return;
        }

        private async void SaveImagesInDb()
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            //picker.FileTypeFilter.Add(".mp4");

            IReadOnlyList<StorageFile> files = await picker.PickMultipleFilesAsync();
           
            if (files != null)
            {
                Picture picture = new Picture();
                foreach (var item in files)
                {
                    var data = item.Properties.GetImagePropertiesAsync();
                    picture.Image = await ConvertImageToByte(item);
                    picture.Name = item.Name;
                    picture.Imagepath = item.Path;
                    using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                    {
                        conn.RunInTransaction(() =>
                        {
                            conn.Insert(picture);
                        });
                    }
                }
            //if (file != null)
            //{

                //Picture picture = new Picture();
                //picture.Image = await ConvertImageToByte(file);
                //picture.Name = file.Name;

                
                ImagesObservableCollections = GetImagesFromDb();
                await _dialogService.ShowMessage("Saved", "SucessFully Saved...!");
                

            }
        }

        private async Task<byte[]> ConvertImageToByte(StorageFile file)
        {
            using (var inputStream = await file.OpenSequentialReadAsync())
            {
                Stream readStream = inputStream.AsStreamForRead();
                byte[] byteArray = new byte[readStream.Length];
                await readStream.ReadAsync(byteArray, 0, byteArray.Length);
                return byteArray;
            }
        }

        private ObservableCollection<Picture> _ImagesObservableCollections;
        private ObservableCollection<Videos> _VideosObservableCollections;

        
        public ObservableCollection<Picture> ImagesObservableCollections
        {
            get { return _ImagesObservableCollections = GetImagesFromDb(); }
            set { _ImagesObservableCollections = value;RaisePropertyChanged("ImagesObservableCollections"); }
        }
        public ObservableCollection<Videos> VideosObservableCollections
        {
            get { return _VideosObservableCollections; }
            set { _VideosObservableCollections = value; RaisePropertyChanged("VideosObservableCollections"); }
        }

        public ObservableCollection<Picture> GetImagesFromDb()
        {

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                List<Picture> myCollection = conn.Table<Picture>().ToList();
                ObservableCollection<Picture> PictureList = new ObservableCollection<Picture>(myCollection);
                return PictureList;
            }
        }

        private ObservableCollection<Videos> GetVideos()
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                List<Videos> myCollection = conn.Table<Videos>().ToList();
                ObservableCollection<Videos> videoList= new ObservableCollection<Videos>(myCollection);
                return videoList;
            }
        }

        private ICommand _itemGridCliked;

        public ICommand ItemGridCliked
        {
            get
            {
                if (_itemGridCliked == null)
                {
                    _itemGridCliked = new RelayCommand<ItemClickEventArgs>(OnclickGridCliked);
                }

                return _itemGridCliked;
            }
        }

        private Visibility _isVisible = Visibility.Collapsed;
        public Visibility IsVisible { get { return _isVisible; } set { _isVisible = value; RaisePropertyChanged("IsVisible"); } }

        /************************* commands sections ************************************** */

        private RelayCommand _SyncCommand;
        public RelayCommand SyncCommand => _SyncCommand ?? (_SyncCommand = new RelayCommand(SyncAction));
        private RelayCommand<object> _SaveCommand;
        public ICommand SaveCommand => _SaveCommand ?? (_SaveCommand = new RelayCommand<object>((data) => SaveData(data)));

        private RelayCommand<object> _DeleteCommand;

        public RelayCommand<object> DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new RelayCommand<object>(DeleteMethod);
                }
                return _DeleteCommand;
            }
        }

        private RelayCommand<string> _GoBackCommand;

        public RelayCommand<string> GoBackCommand
        {
            get
            {
                if (_GoBackCommand == null)
                {
                    _GoBackCommand = new RelayCommand<string>(CommonGoBack);
                }
                return _GoBackCommand;
            }
        }

        private ICommand m_ImageTappedCommand;

        public ICommand ImageTappedCommand
        {
            get
            {
                if (m_ImageTappedCommand == null)
                {
                    m_ImageTappedCommand = new RelayCommand<ItemClickEventArgs>(BindingImages);
                }
                return m_ImageTappedCommand;
            }
        }

        private Visibility _IsVisible=Visibility.Collapsed;
        public Visibility IsVisibles
        {
            get
            {
                return _IsVisible;
            }
            set
            {
                _IsVisible = value;
                RaisePropertyChanged("IsVisibles");
            }
        }
        public byte[] _image;

        public byte[] ImageShow
            { get=>_image; set {
                _image = value;
                RaisePropertyChanged("ImageShow");
            } }
        private Picture _imagePath;
        public Picture ImagePathForShow
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                RaisePropertyChanged("ImagePathForShow");
            }
        }


        private  async void BindingImages(ItemClickEventArgs sender)
        {
            //IsVisibles = Visibility.Visible;
            Picture picture = sender?.ClickedItem as Picture;
            ImagePathForShow = picture;
            ImageShow = picture.Image;
            //LaunchFile(picture.Imagepath);

         

            // byte[] imageBytes = (byte[])picture.Image;
            //BitmapImage source = await ConvertByteToImage(imageBytes);
            //ContentDialog noWifiDialog = new ContentDialog()
            //{
            //    Title = "Image You Cliked Is ..!",
            //    Content = source,
            //    CloseButtonText = "Ok"
            //};
            //await noWifiDialog.ShowAsync();
            //var addingViewmodel = new AddingViewModel(); 
            //m_ViewImages = new ViewImages();
            ////m_ViewImages.

            //    var t = new ViewImages();
            //t.o

            // Picture picture = sender ?.ClickedItem as  Picture;

            // byte[] imageBytes = (byte[])picture.Image;


            // Viewbox image = new Viewbox();

            // Image imageControl = new Image() { Width = 500, Height = 500 };
            //// Uri uri = new Uri(ConvertByteToImage(imageBytes););

            // BitmapImage source = await ConvertByteToImage(imageBytes);
            // imageControl.Source = source;
            // image.Child = imageControl;
            // Panel parent = image.Parent as Panel;
            // if (parent != null)
            // {
            //     image.RenderTransform = new ScaleTransform() { ScaleX = 2, ScaleY = 2 };
            //     parent.Children.Remove(image);
            //     parent.Children.Add(new Popup() { Child = image, IsOpen = true, Tag = parent });
            // }
            // else
            // {
            //     Popup popup = image.Parent as Popup;
            //     //popup.Child = null;
            //     Panel panel = popup.Tag as Panel;
            //     image.RenderTransform = null;
            //     panel.Children.Add(image);
            // }
        }
        public async void LaunchFile(string filePath)
        {
            // MessageDialog dialog = null;

            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    var file = await StorageFile.GetFileFromPathAsync(filePath);
                    if (file == null) return;
                    var targetFileF = await DownloadsFolder.CreateFileAsync(file.Name, CreationCollisionOption.GenerateUniqueName);

                    if (targetFileF != null)
                    {
                        await file.CopyAndReplaceAsync(targetFileF);
                    }

                    var success = await Launcher.LaunchFileAsync(targetFileF);

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message + "Hase been occcus");
                }
            }
        }

        private async Task<BitmapImage> ConvertByteToImage(byte[] imageBytes)
        {
            BitmapImage image = new BitmapImage();

            using (InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(randomAccessStream.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(imageBytes);
                    await writer.StoreAsync();
                    image.SetSourceAsync(randomAccessStream);
                }

            }
            return image;
        }
        private RelayCommand<AutoSuggestBoxSuggestionChosenEventArgs> _autoName;

        public RelayCommand<AutoSuggestBoxSuggestionChosenEventArgs> AutoNameCommand
        {
            get
            {
                if (_autoName == null)
                {
                    _autoName = new RelayCommand<AutoSuggestBoxSuggestionChosenEventArgs>(SetSearchName);
                }
                return _autoName;
            }
        }

        private RelayCommand _GridCommand;
        private ObservableCollection<ContactList> _children;

        public RelayCommand GridCommand => _GridCommand ?? (_GridCommand = new RelayCommand(NavigationsGrid));

        /************************** Method Sections *********************************/

        private void SyncAction()
        {
            Utils.Util.ShowToastMessage("Notification", "Master Data Is Downloaded");
            IsVisible = Visibility.Visible;
        }

        private async void SaveData(object data)
        {
            AddingViewModel datas = ((AddingViewModel)data);

            if (datas.Name != "" && datas.Number != 0)
            {

                MessageDialog message = new MessageDialog("Do you want to Save This Contact?");
                message.Commands.Clear();
                message.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                message.Commands.Add(new UICommand { Label = "Save Later", Id = 1 });
                message.Commands.Add(new UICommand { Label = "NO", Id = 2 });

                var res = await message.ShowAsync();

                if ((int)res.Id == 1)
                {
                    Db_Helper.Insert(new ContactList(datas.Name, datas.Number, "Null"));
                    datas.Name = null;
                    datas.Number = 0;
                    LoadAddedData();
                }
            }
            else
            {
               // await _dialogService.ShowMessageBox(
                await _dialogService.ShowMessage("Please fill two fields", "Warning");
            }
            //    await _dialogService.ShowMessage("Do you want to Save This Contact?",
            //       "Save",
            //       buttonConfirmText: "Ok", buttonCancelText: "Cancel",
            //       afterHideCallback: (confirmed) =>
            //       {
            //           if (confirmed)
            //           {
            //               Db_Helper.Insert(new ContactList(datas.Name, datas.Number,"Null"));
            //               datas.Name = null;
            //               datas.Number = 0;
            //               LoadAddedData();
            //           }
            //       }
            //);
            //}

        }

        private void OnItemClick(ItemClickEventArgs args)
        {
            _navigationService.NavigateTo(ViewModelLocator.ContactDetails);
            ContactList item = args?.ClickedItem as ContactList;
            MessengerInstance.Send(item);
        }

        private void OnclickGridCliked(ItemClickEventArgs obj)
        {
            SimpleDemoData item = obj.ClickedItem as SimpleDemoData;
            try
            {
                switch (item.GTitle)
                {
                    case ViewModelLocator.NotificationView:
                        _navigationService.NavigateTo(ViewModelLocator.NotificationView);
                        break;

                    case ViewModelLocator.InspectionView:
                        _navigationService.NavigateTo(ViewModelLocator.InspectionView);
                        break;

                    case ViewModelLocator.EquipmentsView:
                        
                        _navigationService.NavigateTo(ViewModelLocator.EquipmentsView);
                        break;

                    case ViewModelLocator.WorkOrderView:
                        _navigationService.NavigateTo(ViewModelLocator.WorkOrderView);
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + "Hase been occcus");
            }
        }
        private ObservableCollection<Products> _productscollection;
        public ObservableCollection<Products> ProductsCollections
        {
            get { return _productscollection; }
            set
            {
                _productscollection = value;
                RaisePropertyChanged("ProductsCollections");
            }
        }
        private Random _rand = new Random();
        //public ObservableCollection<TreeItemsModel> BuildTree(int depth, int branches)
        //{
        //    var tree = new ObservableCollection<TreeItemsModel>();
        //    ContactListCollection = Db_Helper.ReadAllContacts();
        //   // ProductsCollections = Db_Helper.ReadAllProducts();
        //    if (depth > 0)
        //    {
        //        var depthIndices = Enumerable.Range(0, branches).ToArray();

        //        for (int i = 0; i < ContactListCollection.Count; i++)
        //        {
        //            //var d = depthIndices[i] % depth;
        //            //  var b = _rand.Next(branches / 2, branches);
        //            tree.Add(
        //                new TreeItemsModel
        //                {
        //                    Branch = branches,
        //                    Depth = ContactListCollection.Count,
        //                    Text = ContactListCollection[i].Name,
        //                    Children = BuildTree(ContactListCollection.Count,2)
        //                });
        //        }
        //    }
        //    return tree;
        //}
       
        private async void DeleteMethod(object Id)
        {
            SqlLiteSimple.Model.ContactList  contact =  (Id) as SqlLiteSimple.Model.ContactList ;


            await _dialogService.ShowMessage("Do You Want to Delete This Contact?", "Delete...",
                buttonConfirmText: "Yes", buttonCancelText: "No",
                afterHideCallback: (confirmed) =>
                {
                    if (confirmed)
                    {
                        using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                        {
                            var existingconact = conn.Query<ContactList>("select * from ContactList where Id =" + contact.Id).FirstOrDefault();
                            if (existingconact != null)
                            {
                                conn.RunInTransaction(() =>
                                {
                                    conn.Delete(existingconact);
                                });
                            }
                        }
                    }
                });
            ContactListCollection = Db_Helper.ReadAllContacts();
        }

        private void CommonGoBack(string arg)
        {
            switch (arg)
            {
                case ViewModelLocator.GoBack: _navigationService.GoBack(); break;
            }
        }

        private void NavigationsGrid()
        {
            _navigationService.NavigateTo(ViewModelLocator.DynamicGrid);
        }

        private void SetSearchName(AutoSuggestBoxSuggestionChosenEventArgs obj)

        {
            ContactList item = obj.SelectedItem as ContactList;
            TextData = item.Name;
            SearchMethod(item.Name);
        }

        private async void SearchMethod(string name)
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<ContactList> existingconact = conn.Query<ContactList>("select * from ContactList where Name like'%" + name + "%'").ToList<ContactList>();
                    ObservableCollection<ContactList> ContactsList = new ObservableCollection<ContactList>(existingconact);
                    ContactListCollection = ContactsList;
                }
            }
            catch
            {
                await _dialogService.ShowMessage("No Contact found..!", "Searching..");
            }
        }

        private string _time = "00:00:00";
        private bool _clockIsRunning;
        private CameraCaptureUIMaxPhotoResolution _resolution;
        private LowLagMediaRecording _mediaRecording;

        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                Set(ref _time, value);
            }
        }

        public void StartClock()
        {
            _clockIsRunning = true;

            Task.Run(async () =>
            {
                while (_clockIsRunning)
                {
                    await Task.Delay(1000);
                    if (_clockIsRunning)
                    {

                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                                try
                                {
                                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                {
                                    Time = DateTime.Now.ToString("HH:mm:ss");
                                });
                               
                            }
                                catch (Exception e)
                                {
                                    Time = e.Message.ToString();
                                }
                            });
                    }
                }
            });
        }

        public void StopClock()
        {
            _clockIsRunning = false;
        }
        private RelayCommand<object> m_FocusCommand;
        public RelayCommand<object> FocusCommand => m_FocusCommand ?? (m_FocusCommand = new RelayCommand<object>(controlDetails => FocusEventHandler(controlDetails)));

        bool a = false;
        private bool _IsActivitySplitViewPaneOpen;
        private ContactList _SelectedContactList;

        private async void FocusEventHandler(object controlDetails)
        {
            ContentDialog contentDialog = controlDetails as ContentDialog;
            if (a)
            {
                await contentDialog.ShowAsync();
            }
            else
            {
                return;
            }
        }
    }
}