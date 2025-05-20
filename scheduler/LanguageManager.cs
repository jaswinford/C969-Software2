using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows;

namespace scheduler
{
    public class LanguageManager
    {
        private static LanguageManager _instance;
        private readonly ResourceManager _resourceManager;
        private CultureInfo _currentCulture;

        private LanguageManager()
        {
            _currentCulture = CultureInfo.CurrentCulture;
            _resourceManager = new ResourceManager("scheduler.Properties.Strings", Assembly.GetExecutingAssembly());
        }

        public static LanguageManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LanguageManager();
                return _instance;
            }
        }

        public string CurrentLanguage
        {
            get => _currentCulture.DisplayName;
            set
            {
                try
                {
                    var newCulture = CultureInfo.GetCultureInfo(GetCultureCode(value));
                    _currentCulture = newCulture;
                    Thread.CurrentThread.CurrentUICulture = newCulture;
                    Thread.CurrentThread.CurrentCulture = newCulture;
                }
                catch (CultureNotFoundException)
                {
                    var newCulture = CultureInfo.GetCultureInfo("en");
                    _currentCulture = newCulture;
                    Thread.CurrentThread.CurrentCulture = newCulture;
                    Thread.CurrentThread.CurrentUICulture = newCulture;
                    MessageBox.Show("Language not supported. Defaulting to English.", "Language Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                LanguageChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler LanguageChanged;

        public IEnumerable<string> GetAvailableLanguages()
        {
            var languages = new List<string>();

            // Get all culture-specific resource sets
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            foreach (var culture in cultures)
                try
                {
                    var resourceSet = _resourceManager.GetResourceSet(culture, true, false);
                    if (resourceSet != null && culture.Name != "") languages.Add(culture.DisplayName);
                }
                catch (CultureNotFoundException)
                {
                    // Skip invalid cultures
                }

            return languages.OrderBy(l => l);
        }

        private string GetCultureCode(string displayName)
        {
            var culture = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .FirstOrDefault(c => c.DisplayName.Equals(displayName, StringComparison.CurrentCultureIgnoreCase));
            return culture?.Name ?? "en"; // Default to English if not found
        }


        public string GetTranslation(string key)
        {
            try
            {
                return _resourceManager.GetString(key, _currentCulture) ?? key;
            }
            catch
            {
                return key;
            }
        }

        public MessageBoxResult ShowMessageBox(string messageKey, string captionKey, MessageBoxButton buttons,
            MessageBoxImage icon)
        {
            var message = GetTranslation(messageKey);
            var caption = GetTranslation(captionKey);
            return MessageBox.Show(message, caption, buttons, icon);
        }
    }
}