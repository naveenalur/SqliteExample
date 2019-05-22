using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using SqlLiteSimple.Model;

namespace SqlLiteSimple.ViewModel
{
    public class ViewModelLocator
    {
        public const string SecondPageKey = "SecondPage";
        public const string ContactDetails = "ContactDetails";
        public const string BackButton = "BackButton";
        public const string SyncButton = "SyncButton";
        public const string CancleButton = "CancleButton";
        public const string DynamicGrid = "DynamicGrid";
        public const string GoBack = "GoBack";
        public const string NotificationView = "Notifications";
        public const string EquipmentsView = "Equipments";
        public const string WorkOrderView = "WorkOrders";
        public const string InspectionView = "Inspections";

        public const string CameraButton = "CameraButton";
        public const string GetCollectionDataButton = "GetCollectionDataButton";
        //public const string CameraButton = "CameraButton";
        //public const string CameraButton = "CameraButton";


        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new NavigationService();
            nav.Configure(SecondPageKey, typeof(SecondPage));
            nav.Configure(ContactDetails, typeof(Views.ContactDetails));
            nav.Configure(DynamicGrid, typeof(Views.DynamicGrid));
            nav.Configure(NotificationView, typeof(Views.NotificationView));
            nav.Configure(EquipmentsView, typeof(Views.Equipments));
            nav.Configure(WorkOrderView, typeof(Views.WorkOreder));
            nav.Configure(InspectionView, typeof(Views.Inspections));

            SimpleIoc.Default.Register<INavigationService>(() => nav);

            SimpleIoc.Default.Register<IDialogService, DialogService>();

            //if (ViewModelBase.IsInDesignModeStatic)
            //{
            //    SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            //}
            //else
            //{
            //    SimpleIoc.Default.Register<IDataService, DataService>();
            //}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AddingViewModel>();

            SimpleIoc.Default.Register<DatabaseHelperClass>();
            SimpleIoc.Default.Register<DetailSViewModel>();
            SimpleIoc.Default.Register<InspectionViewModel>();

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public AddingViewModel Adding => ServiceLocator.Current.GetInstance<AddingViewModel>();
        public DetailSViewModel Details => ServiceLocator.Current.GetInstance<DetailSViewModel>();
        public InspectionViewModel InspectionViewModels => ServiceLocator.Current.GetInstance<InspectionViewModel>();
    }
}
