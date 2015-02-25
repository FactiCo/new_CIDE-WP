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
using JC_1_5.Code.Entities;
using Facebook.Client;
using Facebook;
using JC_1_5.Code;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Security.Cryptography;


namespace JC_1_5.Pages
{

    public class catPropuesta
    {
        public string Titulo { get; set; }
        public lstPropuestas itemsProp { get; set; }
    }

    public partial class Propuestas : PhoneApplicationPage
    {
        public List<catPropuesta> Data { get; set; }

        FacebookSession sessionStg;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            loadList();
        }

        public Propuestas()
        {
            
            sessionStg = SessionStorage.Load();

            InitializeComponent();

        }

        private void loadList()
        {
            loadPropuestas(lstPropuestas_Familiar, "Justicia para familias");
            loadPropuestas(lstPropuestas_Trabajo, "Justicia en el trabajo");
            loadPropuestas(lstPropuestas_Empresarios, "Justicia para emprendedores");
            loadPropuestas(lstPropuestas_Otros, "Otros temas de Justicia Cotidiana");
            loadPropuestas(lstPropuestas_Ciudadanos, "Justicia para ciudadanos");
            loadPropuestas(lstPropuestas_Vecinal, "Justicia vecinal y comunitaria");
            
        }

        
            

        

        private async void loadPropuestas(ListBox lpicker, string cat)
        {
           
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Data = new List<catPropuesta>();

            string [] pivotCategoria = { "Trabajo", "Familiar", "Empresarios", "Otros", "Ciudadanos","Vecinal" };
                
            var response = await httpClient.GetAsync("http://justiciacotidiana.mx:8080/justiciacotidiana/api/v1/propuestas");

            var responseString = await response.Content.ReadAsStringAsync();
            lstPropuestas objRespPropuestas = JsonConvert.DeserializeObject<lstPropuestas>(responseString);
            objRespPropuestas.items= objRespPropuestas.items.Where(p => p.category == cat).ToList();
            if (objRespPropuestas.items.Count> 0)
            {             
                try
                {
                    var fb = new FacebookClient(sessionStg.AccessToken);

                    dynamic result = await fb.GetTaskAsync("me");
                    var user = new GraphUser(result);
                    
                    
                    
                    foreach (Propuesta prop in objRespPropuestas.items)
                    {
                     
                        prop.votes.Total = prop.votes.favor.participantes.Count + prop.votes.contra.participantes.Count + prop.votes.abstencion.participantes.Count;
                        

                        if (prop.author.fcbookid != null)
                        {
                            string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", prop.author.fcbookid, "square", sessionStg.AccessToken);
                            HttpClient objFotoDown = new HttpClient();
                            var respImage = await objFotoDown.GetAsync(profilePictureUrl);

                            prop.author.urlFoto = respImage.RequestMessage.RequestUri;


                            
                        }
                        else
                        {
                            prop.author.urlFoto = new Uri(@"/Assets/Icons/NEW_LOGO.png", UriKind.Relative);
                        }   
                    }

                    lpicker.ItemsSource = objRespPropuestas.items.ToList();

                }

                    

                    
                
                catch (FacebookOAuthException exception)
                {
                    MessageBox.Show("Error fetching user data: " + exception.Message);
                }
                
                    
            }
            else
            {
                lpicker.Visibility = Visibility.Collapsed;
                switch (lpicker.Name)
                {
                    case "lstPropuestas_Trabajo":
                        lstPropuestas_Trabajo.Visibility = Visibility.Collapsed;
                        sinPropuestasTrabajo.Visibility = Visibility.Visible;
                        break;
                    case "lstPropuestas_Ciudadanos":
                        lstPropuestas_Ciudadanos.Visibility = Visibility.Collapsed;
                        sinPropuestasCiudadanos.Visibility = Visibility.Visible;
                        break;
                    case "lstPropuestas_Familiar":
                        
                        lstPropuestas_Familiar.Visibility = Visibility.Collapsed;
                        sinPropuestasFamilia.Visibility = Visibility.Visible;
                        break;
                    case "lstPropuestas_Empresarios":
                        lstPropuestas_Empresarios.Visibility = Visibility.Collapsed;
                        sinPropuestasEmprendedores.Visibility = Visibility.Visible;
                        break;
                    case "lstPropuestas_Vecinal":

                        lstPropuestas_Vecinal.Visibility = Visibility.Collapsed;
                        sinPropuestasVecinal.Visibility = Visibility.Visible;
                        break;
                    case "lstPropuestas_Otros":
                        
                        lstPropuestas_Otros.Visibility = Visibility.Collapsed;
                        sinPropuestasOtros.Visibility = Visibility.Visible;
                        break;
                }
            }
            

        }

        private void Trabajo_Loaded(object sender, RoutedEventArgs e)
        {
         
        }

        private void lstPropuestas_Trabajo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItems.Count > 0)
            {
                Propuesta selectedProp = lstPropuestas_Trabajo.SelectedItem as Propuesta;
                NavigationService.Navigate(new Uri("/Pages/PanoPropuestas.xaml?idProp=" + selectedProp._id, UriKind.Relative));
            }
            
        }
   

        private void lstPropuestas_Familiar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if ((sender as ListBox).SelectedItems.Count > 0)
            {

                Propuesta selectedProp = lstPropuestas_Familiar.SelectedItem as Propuesta;
                NavigationService.Navigate(new Uri("/Pages/PanoPropuestas.xaml?idProp=" + selectedProp._id, UriKind.Relative));
            }

        }

        private void lstPropuestas_Ciudadanos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if ((sender as ListBox).SelectedItems.Count > 0)
            {
                Propuesta selectedProp = lstPropuestas_Ciudadanos.SelectedItem as Propuesta;

                NavigationService.Navigate(new Uri("/Pages/PanoPropuestas.xaml?idProp=" + selectedProp._id, UriKind.Relative));
            }

        }

        private void lstPropuestas_Empresarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if ((sender as ListBox).SelectedItems.Count > 0)
            {

                Propuesta selectedProp = lstPropuestas_Empresarios.SelectedItem as Propuesta;
                NavigationService.Navigate(new Uri("/Pages/PanoPropuestas.xaml?idProp=" + selectedProp._id, UriKind.Relative));
            }            
            
           
        }

        private void lstPropuestas_Vecinal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItems.Count > 0)
            {
                Propuesta selectedProp = lstPropuestas_Vecinal.SelectedItem as Propuesta;
                NavigationService.Navigate(new Uri("/Pages/PanoPropuestas.xaml?idProp=" + selectedProp._id, UriKind.Relative));
            }

        }

        private void lstPropuestas_Otros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItems.Count > 0)
            {
                Propuesta selectedProp = lstPropuestas_Otros.SelectedItem as Propuesta;

                NavigationService.Navigate(new Uri("/Pages/PanoPropuestas.xaml?idProp=" + selectedProp._id, UriKind.Relative));
            }

        }

    }
}