using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using JC_1_5.Code;
using JC_1_5.Code.Entities;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using Facebook;
using Facebook.Client;
namespace JC_1_5.Pages
{
    public partial class PropuestaIsolated : PhoneApplicationPage
    {
        string propID;
        FacebookSession fbSession;

        public PropuestaIsolated()
        {
            fbSession = SessionStorage.Load();
            InitializeComponent();
            

;
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string reqID = "";
            if (NavigationContext.QueryString.TryGetValue("idProp", out reqID))
            {
                propID = reqID;
                loadPropuesta(propID);
            }
        }

        

        private async void loadPropuesta(string uidProp)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var response = await httpClient.GetAsync("http://justiciacotidiana.mx:8080/justiciacotidiana/api/v1/propuestas");
            var responseString = await response.Content.ReadAsStringAsync();
            lstPropuestas objRespPropuestas = JsonConvert.DeserializeObject<lstPropuestas>(responseString);

            Propuesta mainProp = objRespPropuestas.items.Where(p => p._id == propID).Single();

            txtAutor.Text = mainProp.author.name;
            txtTitulo.Text = mainProp.title;

            if (mainProp.author.fcbookid != null)
            {
                string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", mainProp.author.fcbookid, "square", fbSession.AccessToken);
                HttpClient objFotoDown = new HttpClient();
                var respImage = await objFotoDown.GetAsync(profilePictureUrl);

                imgBrsUsr.ImageSource = new System.Windows.Media.Imaging.BitmapImage(respImage.RequestMessage.RequestUri);
            }
            else
            {
                imgBrsUsr.ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Assets/Icons/JC_IconoGrande.png", UriKind.Relative));
            }

            string txtWebBrow = mainProp.description.Replace(@"//www.youtube.com", @"http://www.youtube.com");
            browContenido.NavigateToString("<!doctype html><html><head><style>img {width: 100%;height: auto;} iframe {width:100%; height:500px !important;}</style></head><body>" + txtWebBrow + "</body></html>");

        }


       
    }


}