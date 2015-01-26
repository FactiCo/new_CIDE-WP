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
using JC_1_5.Code.Entities;
using Newtonsoft.Json;

namespace JC_1_5.Pages
{
    public partial class PanoPropuestas : PhoneApplicationPage
    {
        string strHTML;
        Propuesta objPropuesta;

        public PanoPropuestas()
        {
            objPropuesta = new Propuesta();
            
            InitializeComponent();
            
        }
        string idPropRequest;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (NavigationContext.QueryString.TryGetValue("idProp", out idPropRequest))
            {
                loadPropuesta(idPropRequest);
            }
        }

        string jusIDProp;

        private async void loadPropuesta(string uidProp)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync("http://justiciacotidiana.mx:8080/justiciacotidiana/api/v1/propuestas");

            var responseString = await response.Content.ReadAsStringAsync();
            lstPropuestas objRespPropuestas = JsonConvert.DeserializeObject<lstPropuestas>(responseString);
            
            this.objPropuesta = objRespPropuestas.items.First(p=>p._id==uidProp);

            browContenido.NavigateToString("<html><head><meta name='viewport' content='width=480, user-scalable=yes' /></head><body>" + this.objPropuesta.description + "</body></html>"); 
            txtPregunta.Text=objPropuesta.question.title;

            txtTituloWebView.Text = objPropuesta.title;
            string currCategory = objPropuesta.category;
            if (objPropuesta.question.answers!=null)
            {
                if (objPropuesta.question.answers.Count > 0)
                {
                    lstRespuestas.ItemsSource = objPropuesta.question.answers.ToList(); 
                    
                }   
            }
        }

        private void btnFavor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAbstencion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnContra_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstRespuestas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void browContenido_Loaded(object sender, RoutedEventArgs e)
        {

            
        }
    }
}