using SqlLiteSimple.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SqlLiteSimple.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotificationView : Page
    {
     

        public NotificationView()
        {
            this.InitializeComponent();
            suggestions = new ObservableCollection<string>();
            MyInkCanvas.InkPresenter.InputDeviceTypes =
        Windows.UI.Core.CoreInputDeviceTypes.Mouse |
        Windows.UI.Core.CoreInputDeviceTypes.Pen;

            // Set initial ink stroke attributes.
            InkDrawingAttributes drawingAttributes = new InkDrawingAttributes();
            drawingAttributes.Color = Windows.UI.Colors.Black;
            drawingAttributes.IgnorePressure = false;
            drawingAttributes.FitToCurve = true;
            MyInkCanvas.InkPresenter.UpdateDefaultDrawingAttributes(drawingAttributes);
        }

        private ObservableCollection<String> suggestions;

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)

        {

            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)

            {

                suggestions.Clear();

                suggestions.Add(sender.Text + "1");

                suggestions.Add(sender.Text + "2");

                suggestions.Add(sender.Text + "3");

                suggestions.Add(sender.Text + "4");

                suggestions.Add(sender.Text + "5");

                suggestions.Add(sender.Text + "6");

                suggestions.Add(sender.Text + "7");

                suggestions.Add(sender.Text + "8");

                suggestions.Add(sender.Text + "9");

                sender.ItemsSource = suggestions;

            }

        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)

        {

            //if (args.ChosenSuggestion != null)

            //    txtAutoSuggestBox.Text = args.ChosenSuggestion.ToString();

            //else

            //    txtAutoSuggestBox.Text = sender.Text;

        }


        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("text.png", CreationCollisionOption.ReplaceExisting);

            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                await MyInkCanvas.InkPresenter.StrokeContainer.SaveAsync(stream);
            }

            await LoadSaveImage();
        }


        private async Task LoadSaveImage()
        {
            var file = await ApplicationData.Current.LocalFolder.GetFileAsync("text.png");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            var bitmapImage = new BitmapImage();
            await bitmapImage.SetSourceAsync(stream);
            image.Source = bitmapImage;
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }

        private void asb_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }

        private void asb_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }

        private void asb_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {

        }
    }
}
