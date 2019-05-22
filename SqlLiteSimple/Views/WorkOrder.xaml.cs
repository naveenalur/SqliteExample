using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SqlLiteSimple.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 


    
    public sealed partial class WorkOreder : Page
    {
        public ObservableCollection<Wares> WaresCollection = new ObservableCollection<Wares>();

        public WorkOreder()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            WaresCollection.Clear();
            WaresCollection.Add(new Wares { WaresImage = "/Assets/miao5.jpg" });
            WaresCollection.Add(new Wares { WaresImage = "/Assets/miao5.jpg" });
            WaresCollection.Add(new Wares { WaresImage = "/Assets/miao5.jpg" });
            WaresCollection.Add(new Wares { WaresImage = "/Assets/miao5.jpg" });
            WaresCollection.Add(new Wares { WaresImage = "/Assets/miao5.jpg" });
            WaresCollection.Add(new Wares { WaresImage = "/Assets/miao5.jpg" });
            WaresCollection.Add(new Wares { WaresImage = "/Assets/miao5.jpg" });
            
          
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //FilterGrid.Visibility = Visibility.Visible;
        }

        private void Done_Button_Click(object sender, RoutedEventArgs e)
        {
           // FilterGrid.Visibility = Visibility.Collapsed;
        }
    }

    public class Wares
    {
        public string WaresImage { get; set; }
    }
}
