using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using Windows.Storage;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ALEX.Bible
{
    public sealed partial class MenuPanel : UserControl
    {
        public MenuPanel()
        {
            this.InitializeComponent();
        }

        ToggleButton _lastButton = null;
        UserControl _lastPanel = null;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_lastButton != null)
            {
                _lastButton.IsChecked = false;
                _lastButton = null;
            }
            if(_lastPanel!= null)
            {
                _lastPanel.Visibility = Visibility.Collapsed;
                _lastPanel = null;
            }

            _lastButton = sender as ToggleButton;
            var tag = _lastButton.Tag as string;

            switch (tag)
            {
                case "Search": _lastPanel = _searchPanel; break;
                case "Bibles": _lastPanel = _bibleListPanel; break;
                case "BibleBooks": _lastPanel = _bibleBookListPanel; break;
                default: throw new Exception("Button [" + tag + "] not yet supported");
            }

            _lastPanel.Visibility = Visibility.Visible;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(SettingsPage));
        }

        public void SaveSettings(ApplicationDataCompositeValue settings)
        {
            if (_lastButton != null)
            {
                settings["MenuButton"] = _lastButton.Tag;
            }
            else
            {
                settings["MenuButton"] = null;
            }
        }

        public void RestoreSettings(ApplicationDataCompositeValue settings)
        {
            if (settings.ContainsKey("MenuButton"))
            {
                if (settings["MenuButton"] != null)
                {
                    switch (settings["MenuButton"] as string)
                    {
                        case "Search": _lastButton = _searchButton; _lastPanel = _searchPanel; break;
                        case "Bibles": _lastButton = _biblesButton; _lastPanel = _bibleListPanel; break;
                        case "BibleBooks": _lastButton = _biblesBookButton; _lastPanel = _bibleBookListPanel; break;
                    }
                    _lastButton.IsChecked = true;
                    _lastPanel.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
