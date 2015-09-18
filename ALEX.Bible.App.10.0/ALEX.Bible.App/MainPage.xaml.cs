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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ALEX.Bible
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            _menuPanel.Visibility = Visibility.Collapsed;
            _favoritesPanel.Visibility = Visibility.Collapsed;
        }

        private void _menuButton_Click(object sender, RoutedEventArgs e)
        {
            if (_menuPanel.Visibility == Visibility.Collapsed)
            {
                _menuPanel.Visibility = Visibility.Visible;
                _favoritesPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                _menuPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void _favoritesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_favoritesPanel.Visibility == Visibility.Collapsed)
            {
                _favoritesPanel.Visibility = Visibility.Visible;
                _menuPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                _favoritesPanel.Visibility = Visibility.Collapsed;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            var settings = new ApplicationDataCompositeValue();
            settings["MenuState"] = _menuPanel.Visibility.ToString();
            settings["FavoritesState"] = _favoritesPanel.Visibility.ToString();
            _menuPanel.SaveSettings(settings);
            ApplicationData.Current.LocalSettings.Values["MainPageSettings"] = settings;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode != NavigationMode.New)
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("MainPageSettings"))
                {
                    var settings = ApplicationData.Current.LocalSettings.Values["MainPageSettings"] as ApplicationDataCompositeValue;
                    _menuPanel.Visibility = (Visibility)Enum.Parse(typeof(Visibility), settings["MenuState"] as string);
                    _favoritesPanel.Visibility = (Visibility)Enum.Parse(typeof(Visibility), settings["FavoritesState"] as string);
                    _menuPanel.RestoreSettings(settings);
                }
            }
            ApplicationData.Current.LocalSettings.Values.Remove("MainPageSettings");

            Debug.WriteLine("NavigateTo");
        }
    }
}
