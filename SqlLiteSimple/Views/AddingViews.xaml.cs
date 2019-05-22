using SqlLiteSimple.Model;
using SqlLiteSimple.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SqlLiteSimple.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddingViews : Page
    {
        //public AddingViewModel add = new AddingViewModel();
        public AddingViewModel Vm => (AddingViewModel)DataContext;
        //ObservableCollection<ContactList> DB_ContactList = new ObservableCollection<ContactList>();
        public AddingViews()
        {
            this.InitializeComponent();
            InkPresenter myPresenter = myCanvas.InkPresenter;
            myPresenter.InputDeviceTypes = Windows.UI.Core.CoreInputDeviceTypes.Pen;
            //Loaded +=(s,e)=> Vm.LoadAddedData();
            Loaded += (s, e) => Vm.StartClock();
            Unloaded += (s, e) => Vm.StopClock();
           
        }

        private void myCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            InkPresenter inkPresenter = myCanvas.InkPresenter;
            inkPresenter.InputDeviceTypes = Windows.UI.Core.CoreInputDeviceTypes.Pen | Windows.UI.Core.CoreInputDeviceTypes.Mouse | Windows.UI.Core.CoreInputDeviceTypes.Touch;
            InkDrawingAttributes myAttributes = inkPresenter.CopyDefaultDrawingAttributes();
            myAttributes.Color = Windows.UI.Colors.Crimson;
            myAttributes.PenTip = PenTipShape.Circle;
            myAttributes.PenTipTransform = System.Numerics.Matrix3x2.CreateRotation((float)Math.PI / 4);
            myAttributes.Size = new Size(2, 5);
            inkPresenter.UpdateDefaultDrawingAttributes(myAttributes);

        }

        private void myCanvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {

        }

        private void myCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }
        //private void loding(object sender, RoutedEventArgs e)
        //{
        //    DB_ContactList = add.LoadFunction();
        //    GridListView.ItemsSource = DB_ContactList.OrderByDescending(i => i.Id).ToList() ;
        //}
    }
}
