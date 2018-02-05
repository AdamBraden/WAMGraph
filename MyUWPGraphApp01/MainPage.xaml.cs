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
using Microsoft.Graph;


//<Application.Resources>
//    <x:String x:Key="ida:ClientId">935c61af-136a-4671-b4f6-cabf7964bfb4</x:String>
//    <x:String x:Key="ida:AADInstance">https://login.microsoftonline.com/</x:String>
//    <x:String x:Key="ida:Domain">microsoft.onmicrosoft.com</x:String>
//</Application.Resources>

namespace MyUWPGraphApp01
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        static GraphServiceClient MSGraph = null;
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MSGraph = MicrosoftGraphContext.GetAuthenticatedClient(App.Current.Resources["ida:ClientId"].ToString());

            var user = await MSGraph.Me.Request().GetAsync();
            TextBlock1.Text = String.Format("{0} - '{1}'", user.DisplayName, user.Id);

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var mgr = await MSGraph.Me.Manager.Request().GetAsync();

            TextBlock1.Text = mgr.Id;
                        
        }
    }
}
