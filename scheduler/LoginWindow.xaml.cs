using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using scheduler.structs;

namespace scheduler
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        private readonly LanguageManager _languageManager = LanguageManager.Instance;

        /// <summary>
        ///     Represents the login window for user authentication.
        ///     Implements functionality to allow UI-culture language switching,
        ///     dynamically updating translations for labels, buttons, and window titles based on the selected language.
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();

            // A.1 Populate the language combo box with languages that we have a translation for
            PopulateLanguageComboBox();

            // A.1 Update the current display with the default translations
            UpdateLanguage();

            // A.1 Subscribe to the LanguageChanged event to update the UI when the language changes
            LanguageManager.Instance.LanguageChanged += (s, e) => { UpdateLanguage(); };
        }


        /// <summary>
        ///     Populates the language combo box with the list of available languages retrieved from the LanguageManager instance.
        ///     Sets the currently selected item in the combo box to the current language.
        /// </summary>
        private void PopulateLanguageComboBox()
        {
            LanguageComboBox.ItemsSource = _languageManager.GetAvailableLanguages();
            LanguageComboBox.SelectedItem = _languageManager.CurrentLanguage;
        }

        /// <summary>
        ///     Handles the event triggered when the selection in the language combo box changes.
        ///     Updates the current language in the LanguageManager to reflect the selected language.
        /// </summary>
        /// <param name="sender">The source of the event, typically the language combo box.</param>
        /// <param name="e">The event data containing information about the selection change.</param>
        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageComboBox.SelectedItem is string selectedLanguage)
                _languageManager.CurrentLanguage = selectedLanguage;
        }


        /// <summary>
        ///     Updates the UI elements of the login window with the currently selected language translations.
        ///     Modifies the content of labels, buttons, and the window title to reflect the appropriate translations
        ///     retrieved from the language manager.
        /// </summary>
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

        /// <summary>
        ///     Handles the click event for the login button in the login window.
        ///     Authenticates the user based on entered credentials and navigates to the main window upon successful login.
        ///     Displays an appropriate error message if authentication fails.
        /// </summary>
        /// <param name="sender">The object that triggered the event, typically the login button.</param>
        /// <param name="e">The event data associated with the click action.</param>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateLogin())
            {
                new MainWindow().Show();
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

        /// <summary>
        ///     Handles the click event of the Cancel button.
        ///     Prompts the user with a confirmation dialog to exit the application.
        ///     If the user confirms, the application shuts down.
        /// </summary>
        /// <param name="sender">The source of the event, typically the Cancel button.</param>
        /// <param name="e">The event data associated with the button click.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var result = _languageManager.ShowMessageBox(
                "Message.ExitConfirm",
                "Title.ExitConfirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes) Application.Current.Shutdown();
        }

        /// <summary>
        ///     Validates the user's login credentials by verifying the provided username and password.
        ///     Checks against stored user information to determine whether the user is authenticated.
        /// </summary>
        /// <returns>True if the credentials are valid and the user is authenticated; otherwise, false.</returns>
        private bool ValidateLogin()
        {
            var user = new User { Name = UsernameTextBox.Text };
            return user.Authenticated(PasswordTextBox.Password);
        }

        private void PasswordTextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }
    }
}