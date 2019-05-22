using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.IO;
using Windows.UI.Xaml;
using System.Collections.ObjectModel;

namespace SqlLiteSimple.ViewModel
{
    public class InspectionViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        public string GoBack => ViewModelLocator.GoBack;
        //private StrokeCollection stroke;
        // private readonly StrokeCollection _strokes;
        public InspectionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            //HandleDropCommand = new RelayCommand<DragEventArgs>(e =>
            //{
            //    if (e.Data == null)
            //    {
            //        return;
            //    }

            //    var files = e.Data.GetData(DataFormats.FileDrop)
            //        as FileInfo[];

            //    // This works with multiple files, but in that
            //    // simple case, let's just handle the 1st one
            //    if (files == null
            //        || files.Length == 0)
            //    {
            //        DroppedFileContent = "No files";
            //        return;
            //    }

            //    var file = files[0];

            //    if (!file.Extension.ToLower().Equals(".txt"))
            //    {
            //        DroppedFileContent = "Not a TXT file";
            //        return;
            //    }

            //    using (var stream = file.OpenRead())
            //    {
            //        using (var reader = new StreamReader(stream))
            //        {
            //            // Read the first line
            //            var line = reader.ReadLine();
            //            DroppedFileContent = line;
            //        }
            //    }
            //});

            WorkCentersCollections = new ObservableCollection<WorkCenters>()
            {
                new WorkCenters
                {
                    WorkCenterId=1,WorkCenterName="Work Center 1"
                },
                new WorkCenters
                {
                    WorkCenterId=2,WorkCenterName="Work Center 2"
                },
                new WorkCenters
                {
                    WorkCenterId=3,WorkCenterName="Work Center 3"
                },
                new WorkCenters
                {
                    WorkCenterId=4,WorkCenterName="Work Center 4"
                }
            };

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

        private void CommonGoBack(string obj)
        {
            switch (obj)
            {
                case ViewModelLocator.GoBack: _navigationService.GoBack(); break;
            }
        }
        public const string DroppedFileContentPropertyName = "DroppedFileContent";

        private string _droppedFile = "Drop file here";


        public string DroppedFileContent
        {
            get
            {
                return _droppedFile;
            }

            set
            {
                if (_droppedFile == value)
                {
                    return;
                }

                _droppedFile = value;
                RaisePropertyChanged(DroppedFileContentPropertyName);
            }
        }

        public RelayCommand<DragEventArgs> HandleDropCommand
        {
            get;
            private set;
        }

        private ObservableCollection<WorkCenters> _workCentersCollections;

        public ObservableCollection<WorkCenters> WorkCentersCollections
        {
            get => _workCentersCollections;
            set
            {
                _workCentersCollections = value;
                RaisePropertyChanged("WorkCentersCollections");
            }
        }

    }
   public class WorkCenters: ViewModelBase
    {
        private int _workcenterId;
        public int WorkCenterId {
            get => _workcenterId;
            set
            {
                _workcenterId = value;
                RaisePropertyChanged("WorkCenterId");
            }
        }

        private string _workCenterName;
        public string WorkCenterName
        {
            get => _workCenterName;
            set
            {
                _workCenterName = value;
                RaisePropertyChanged("WorkCenterName");
            }
        }

    }

}
