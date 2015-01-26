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
using JC_1_5.Code;
using Facebook;
using Facebook.Client;

namespace JC_1_5.Pages
{
    public partial class PanoPropuestas : PhoneApplicationPage
    {
        string strHTML;
        Propuesta objPropuesta;

        FacebookSession sessionStg;
        FacebookClient clientFB;

        string name;
        string id;


        public PanoPropuestas()
        {

            sessionStg=new FacebookSession();
            objPropuesta = new Propuesta();
            sessionStg = SessionStorage.Load();


            clientFB = new FacebookClient(sessionStg.AccessToken);

            
            InitializeComponent();
            
        }

       



        string idPropRequest;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (NavigationContext.QueryString.TryGetValue("idProp", out idPropRequest))
            {
                sessionStg = SessionStorage.Load();
                loadPropuesta(idPropRequest);
            }
        }

        string jusIDProp;
        dynamic result;
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

            result = await clientFB.GetTaskAsync("me", new { fields = "name,id" });

            name = result.name;
            id = result.id;

            bool findedVote=false;
            int votesCount=0;
            string voteType="";

            if (objPropuesta.votes.favor.participantes.Where(p => p.fcbookid == id).ToList().Count>0)
            {
                findedVote = true;
                votesCount = objPropuesta.votes.favor.participantes.Count;
                voteType="a favor";
            }

            if (objPropuesta.votes.abstencion.participantes.Where(p => p.fcbookid == id).ToList().Count > 0)
            {
                findedVote = true;
                votesCount = objPropuesta.votes.abstencion.participantes.Count;
                voteType="en abstencion";
            }

            if (objPropuesta.votes.contra.participantes.Where(p => p.fcbookid == id).ToList().Count > 0)
            {
                findedVote = true;
                votesCount = objPropuesta.votes.contra.participantes.Count;
                voteType="en contra";
            }

            if (findedVote)
            {
                btnFavor.IsEnabled = false;
                btnAbstencion.IsEnabled = false;
                btnContra.IsEnabled = false;

                txtVotados.Text = votesCount.ToString() + " personas han votado " + voteType + " como tu";

            }



            if (objPropuesta.comments.data.Count>0)
            {
                foreach (Comentario prop in objPropuesta.comments.data)
                {

                    if (prop.from.fcbookid != null)
                    {
                        
                        string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", prop.from.fcbookid, "square", sessionStg.AccessToken);
                        HttpClient objFotoDown = new HttpClient();
                        var respImage = await objFotoDown.GetAsync(profilePictureUrl);

                        prop.from.urlFoto = respImage.RequestMessage.RequestUri;

                    }
                    else
                    {
                        prop.from.urlFoto = new Uri(@"/Assets/Icons/JC_IconoGrande.png", UriKind.Relative);
                    }

                    if (prop.from.fcbookid == sessionStg.FacebookId)
                    {
                        btnEnviar.IsEnabled = false;
                    }
                }

                lstArgumenta.ItemsSource = objPropuesta.comments.data.ToList();
            }

        }

        private async void enviaVoto(voteToPost objVoto)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string strJSON = JsonConvert.SerializeObject(objVoto, Formatting.None);
            HttpContent content = new StringContent(strJSON);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync("http://justiciacotidiana.mx:8080/justiciacotidiana/api/v1/votos", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var serializer = new JsonSerializer();
        }

        private void btnFavor_Click(object sender, RoutedEventArgs e)
        {
            voteToPost objVotoAdd = new voteToPost();
            objVotoAdd.fcbookid=sessionStg.FacebookId;
            objVotoAdd.proposalId = this.objPropuesta._id;
            objVotoAdd.value = "favor";

            enviaVoto(objVotoAdd);

        }

        private void btnAbstencion_Click(object sender, RoutedEventArgs e)
        {
            voteToPost objVotoAdd = new voteToPost();
            objVotoAdd.fcbookid = sessionStg.FacebookId;
            objVotoAdd.proposalId = this.objPropuesta._id;
            objVotoAdd.value = "abstencion";

            enviaVoto(objVotoAdd);


        }

        private void btnContra_Click(object sender, RoutedEventArgs e)
        {
            voteToPost objVotoAdd = new voteToPost();
            objVotoAdd.fcbookid = sessionStg.FacebookId;
            objVotoAdd.proposalId = this.objPropuesta._id;
            objVotoAdd.value = "abstencion";

            enviaVoto(objVotoAdd);

        }

        private void lstRespuestas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void browContenido_Loaded(object sender, RoutedEventArgs e)
        {

            
        }
        

        private async void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            commentToPost objComentarioAdd = new commentToPost();
            

            /*dynamic resultComment = await clientFB.GetTaskAsync("me",new
            {
                id = "id",
                name = "name",
                
                
            });*/

            dynamic resultComment = await clientFB.GetTaskAsync("me", new { fields = "name,id" });

            objComentarioAdd.from.fcbookid = resultComment.id;
            objComentarioAdd.from.name = resultComment.name;

            objComentarioAdd.proposalId = this.objPropuesta._id;
            objComentarioAdd.message = txtArgumento.Text;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string strJSON = JsonConvert.SerializeObject(objComentarioAdd, Formatting.None);
            HttpContent content = new StringContent(strJSON);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync("http://justiciacotidiana.mx:8080/justiciacotidiana/api/v1/comentarios", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var serializer = new JsonSerializer();

        }
    }
}