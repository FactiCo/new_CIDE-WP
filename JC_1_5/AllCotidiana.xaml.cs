using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using System.Threading.Tasks;
using JC_1_5.Code.Entities;
using JC_1_5.Code;


using Facebook.Client;
using Facebook;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Security.Cryptography;


namespace JC_1_5
{
    public partial class AllCotidiana : PhoneApplicationPage
    {
        public AllCotidiana()
        {
            
            
            InitializeComponent();
            this.Loaded += this.MainPageLoaded;
        }

        private async void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            var session = SessionStorage.Load();

            if (null != session)
            {
                //this.ExpiryText.Text = string.Format("Login expires on: {0}", session.Expires.ToString());

                //this.ProgressText = "Fetching details from Facebook...";
                //this.ProgressIsVisible = true;

                try
                {
                    var fb = new FacebookClient(session.AccessToken);

                    dynamic result = await fb.GetTaskAsync("me");
                    var user = new GraphUser(result);
                    user.ProfilePictureUrl = new Uri(string.Format("https://graph.facebook.com/{0}/picture?access_token={1}", user.Id, session.AccessToken));

                    //this.CurrentUser = user;

                    await this.GetUserStatus(fb);

                    sitLogged.Visibility = Visibility.Visible;
                    notLogged.Visibility = Visibility.Collapsed;
                }
                catch (FacebookOAuthException exception)
                {
                    MessageBox.Show("Error fetching user data: " + exception.Message);
                }

                //this.ProgressText = string.Empty;
                //this.ProgressIsVisible = false;
            }
            else
            {
                notLogged.Visibility = Visibility.Visible;
                sitLogged.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Gets the user status.
        /// </summary>
        /// <param name="fb">The facebook client.</param>
        /// <returns>
        /// a task
        /// </returns>
        private async Task GetUserStatus(FacebookClient fb)
        {
            dynamic statusResult = await fb.GetTaskAsync("me?fields=statuses.limit(1).fields(message)");

            //this.StatusText.Text = statusResult.statuses.data[0].message;
        }

       

        private async void UpdateStatusButton_OnClick(object sender, RoutedEventArgs e)
        {
            var session = SessionStorage.Load();
            if (null == session)
            {
                return;
            }

            //this.ProgressText = "Updating status...";
            //this.ProgressIsVisible = true;

            //this.UpdateStatusButton.IsEnabled = false;

            try
            {
                var fb = new FacebookClient(session.AccessToken);

                await fb.PostTaskAsync(string.Format("me/feed?message={0}", this.Title), null);

                await this.GetUserStatus(fb);

                //this.UpdateStatusBox.Text = string.Empty;
            }
            catch (FacebookOAuthException exception)
            {
                MessageBox.Show("Error fetching user data: " + exception.Message);
            }

            /*this.ProgressText = string.Empty;
            this.ProgressIsVisible = false;
            this.UpdateStatusButton.IsEnabled = true;*/
        }


        string jusID;
        string fbIDJC = "654570061331636";
        

        private void mnuAcerca_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Acerca.xaml", UriKind.Relative));
        }

        private void mnuTerminos_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Terminos.xaml", UriKind.Relative));
        }

        private void tileJTrabajo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Testimonios.xaml?jusID=trabajo", UriKind.Relative));

        }

        private void tileJFamilia_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Testimonios.xaml?jusID=familia", UriKind.Relative));
        }

        private void tileJVecinal_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Testimonios.xaml?jusID=vecinal", UriKind.Relative));
        }

        private void tileJCiudadanos_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Testimonios.xaml?jusID=ciudadanos", UriKind.Relative));
        }

        private void tileJEmprendedores_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Testimonios.xaml?jusID=emprendedores", UriKind.Relative));
        }

        private void tileJOtros_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Testimonios.xaml?jusID=otros", UriKind.Relative));
        }

     

       

        // Create a Uri object from a URI string 
        

        // Launch the URI


        async void DefaultLaunch()
        {
             // The URI to launch
            string uriToLaunch = @"fbconnect://authorize?client_id=" + fbIDJC + "&scope=user_about_me&redirect_uri=msft-399d99ba06894d4b8136e749e7c25266://authorize&state=custom_state_string";

            Uri uri = new Uri(uriToLaunch);

            // Launch the URI
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);

            if (success)
            {
                // URI launched
            }
            else
            {
                // URI launch failed
            }
        }


        private void btnLoginManual_Click(object sender, RoutedEventArgs e)
        {

            SessionStorage.Remove();

            //DefaultLaunch();


            FacebookSessionClient fb = new FacebookSessionClient(fbIDJC);
            fb.LoginWithApp("user_about_me", "custom_state_string");
            
        }

        private void btnVerPopuestas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Propuestas.xaml", UriKind.Relative));
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            SessionStorage.Remove();
            notLogged.Visibility = Visibility.Visible;
            sitLogged.Visibility = Visibility.Collapsed;
        }
            
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMapa_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Mapa.xaml", UriKind.Relative));
        }

        

     

        
    }
}