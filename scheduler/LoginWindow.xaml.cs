using System.Windows;
using System.Windows.Controls;

namespace scheduler
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    ///
    /// DONE: Determine users' location
    /// DONE: Translate login and error control messages into English and one additional language
    /// TODO: Verify credentials.
    public partial class LoginWindow : Window
    {
        private readonly LanguageManager _languageManager = LanguageManager.Instance;

        public LoginWindow()
        {
            InitializeComponent();

            // Populate the language combo box with languages that we have a translation for
            PopulateLanguageComboBox();

            // Update the current display with the default translations
            UpdateLanguage();

            // Subscribe to the LanguageChanged event to update the UI when the language changes
            LanguageManager.Instance.LanguageChanged += (s, e) => { UpdateLanguage(); };
        }


        private void PopulateLanguageComboBox()
        {
            LanguageComboBox.ItemsSource = _languageManager.GetAvailableLanguages();
            LanguageComboBox.SelectedItem = _languageManager.CurrentLanguage;
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageComboBox.SelectedItem is string selectedLanguage)
                _languageManager.CurrentLanguage = selectedLanguage;
        }


        private void UpdateLanguage()
        {
            // Update window title
            Title = _languageManager.GetTranslation("TitleLogin");

            // Update labels
            UsernameLabel.Content = _languageManager.GetTranslation("Label.Username");
            PasswordLabel.Content = _languageManager.GetTranslation("Label.Password");
            LanguageLabel.Content = _languageManager.GetTranslation("Label.Language");

            // Update buttons
            LoginButton.Content = _languageManager.GetTranslation("Label.Login");
            CancelButton.Content = _languageManager.GetTranslation("Label.Cancel");
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordTextBox.Password;

            if (ValidateLogin(username, password))
            {
                DialogResult = true;
                Close();
            }
            else
            {
                _languageManager.ShowMessageBox(
                    "Message.InvalidCredentials",
                    "Title.LoginFailed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var result = _languageManager.ShowMessageBox(
                "Message.ExitConfirm",
                "Title.ExitConfirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes) Application.Current.Shutdown();
        }

        private bool ValidateLogin(string username, string password)
        {
            // TODO: Login validation
            return false;
        }
    }
}