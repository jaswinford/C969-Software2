using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace customer_scheduler
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    ///
    /// TODO: Determin users' location
    /// TODO: Translate login and error control messages into English and one additional language
    /// TODO: Verify credentials.
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Gets the user's current country using Geolocator.
        /// </summary>
        /// <returns>The user's country.</returns>
        private async string GetUserLocation()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 100 };
                    Geoposition pos = await geolocator.GetGeopositionAsync();
                    return pos.CivicAddress.Country;
                    break;
                case GeolocationAccessStatus.Denied:
                    break;
                case GeolocationAccessStatus.Unspecified:
                    break;
            }
        }
    }
}
