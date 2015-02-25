using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using JC_1_5.Code.Entities;
using Newtonsoft.Json;
using JC_1_5.Code;
using Facebook;
using Facebook.Client;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Windows.Media;

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

            InitializeComponent();

            

            objComentarioAdd = new commentToPost();

            objPropuesta = new Propuesta();
            sessionStg = SessionStorage.Load();

            clientFB = new FacebookClient(sessionStg.AccessToken);

            LoadUserInfo();

            


        }

       



        string idPropRequest;
        string idPropToLoad;
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

        private async void loadFBPhoto()
        {

            if (objPropuesta.author.fcbookid != null)
            {

                string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", this.objPropuesta.author.fcbookid, "square", sessionStg.AccessToken);
                HttpClient objFotoDown = new HttpClient();
                var respImage = await objFotoDown.GetAsync(profilePictureUrl);

                imgBrsUsr.ImageSource=new System.Windows.Media.Imaging.BitmapImage(respImage.RequestMessage.RequestUri);

            }
            else
            {
                imgBrsUsr.ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Assets/Icons/JC_IconoGrande.png", UriKind.Relative));
                
            }
        }
        string txtWebBorw;
        private async void loadPropuesta(string uidProp)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync("http://justiciacotidiana.mx:8080/justiciacotidiana/api/v1/propuestas");

            var responseString = await response.Content.ReadAsStringAsync();
            lstPropuestas objRespPropuestas = JsonConvert.DeserializeObject<lstPropuestas>(responseString);
            
            this.objPropuesta = objRespPropuestas.items.First(p=>p._id==uidProp);

            loadFBPhoto();
            txtTitulo.Text = objPropuesta.title;
            
                                                     
            //txtWebBorw = this.objPropuesta.description.Replace(@"//www.youtube.com", @"http://www.youtube.com");


            string testvideo = @"<iframe src='https://www.youtube.com/embed/WoLF-eYu9gk' width='560' height='315' frameborder='0' allowfullscreen='allowfullscreen'></iframe>";
            string metaScale = "<meta name='viewport' content='width=device-width, initial-scale=1, maximum-scale=1' />";

            txtWebBorw = this.objPropuesta.description;

            if (txtWebBorw.Contains("iframe"))
            {
                browContenido.IsHitTestVisible = true;
            }
            else
            {
                browContenido.IsHitTestVisible = false;
            }


            
            browContenido.NavigateToString("<!doctype html><html><head><style>img {width: 100%;height: auto;} iframe {width:100%; height:500px !important;}</style></head><body onLoad=window.external.notify('rendered_height='+document.getElementById('content').scrollHeight);><div id='content'>" + txtWebBorw + "</div></body></html>");


            txtAutor.Text = objPropuesta.author.name;


            string currCategory = objPropuesta.category;
            if (objPropuesta.question.title!=null)
            {
                txtPregunta.Text=objPropuesta.question.title;
                txtPreguntaVis.Text = objPropuesta.question.title;
                var responseRespuestas = await httpClient.GetAsync("http://justiciacotidiana.mx:8080/justiciacotidiana/api/v1/respuestas");

                var responseStringRespuestas = await responseRespuestas.Content.ReadAsStringAsync();

                lstRespuestaGrafica objRespuestas = JsonConvert.DeserializeObject<lstRespuestaGrafica>(responseStringRespuestas);

                List<RespuestaGrafica> lstFiltered = objRespuestas.items.Where(p => p.questionId == objPropuesta.question._id).ToList();

                  // ObservableCollection<PieDataItem> data = new ObservableCollection<PieDataItem>();

                RespuestaGrafica ansFilt = lstFiltered.Find(p=>p.fcbookid==id);

                if (ansFilt!=null)
                {
                    
                    
                    chrtPie.Visibility = Visibility.Visible;
                    grdPregunta.Visibility = Visibility.Collapsed;
                    txtPreguntaVis.Visibility = Visibility.Visible;
                    graficaRespuestas(lstFiltered);
                }
                else

                {
                    string[] colores = new string[] { "#4A8293", "#189A8E", "#58CA8F", "#34CA67" };
                    int i=0;
                    foreach (Answer ans in objPropuesta.question.answers)
                    {
                        ans.ColorBrush = colores[i];
                        i++;
                    }

                    grdPregunta.Visibility = Visibility.Visible;
                    lstRespuestas.ItemsSource = objPropuesta.question.answers.ToList(); 
                }      
 
            }

            loadVotos();
            loadComentarios(false);
        }

        private async void loadComentarios(bool afterAdd)
        {
            List<Comentario> sourceComs= new List<Comentario>();

            if (afterAdd)
            { 
                HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync("http://justiciacotidiana.mx:8080/justiciacotidiana/api/v1/comentarios");

            var responseString = await response.Content.ReadAsStringAsync();
            lstComentarioIsolated lstComs = JsonConvert.DeserializeObject<lstComentarioIsolated>(responseString);

            sourceComs = lstComs.items.Where(c => c.proposalId == objPropuesta._id).ToList();

            txtArgumento.Text = "";

            }
            else
            { 
                sourceComs=objPropuesta.comments.data;
            }

            
            

            foreach (Comentario prop in sourceComs)
            {

                if (prop.from.fcbookid != null)
                {

                    string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", prop.from.fcbookid, "normal", sessionStg.AccessToken);
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

            lstArgumenta.ItemsSource = sourceComs.ToList();
            
                
        }

        private void graficaRespuestas(List<RespuestaGrafica> lstSource)
        {

            
            var data = lstSource.GroupBy(info => info.answerId)
                    .Select(group => new PieDataItem
                    {
                        Title = objPropuesta.question.answers.Find(ans => ans._id == group.Key).title,
                        Value = group.Count()
                    });

            chrtPie.DataSource = data;


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
            
            
            
            var responseVotes = await httpClient.GetAsync("http://justiciacotidiana.mx:8080/justiciacotidiana/api/v1/votos");
            var responseStringVotes = await responseVotes.Content.ReadAsStringAsync();

            lstVotoIsolated objVotos = JsonConvert.DeserializeObject<lstVotoIsolated>(responseStringVotes);

            if (objVotos != null)
            {
                MessageBox.Show("Gracias por enviar tu voto", "Justicia Cotidiana", MessageBoxButton.OK);

                reloadVotes(objVotos, objVoto.value);

            }
            
        }

        private void reloadVotes(lstVotoIsolated lstVotos,string voteType)
        {

            if (voteType=="favor")
            {
                btnFavor.Click -= btnFavor_Click;
                btnAbstencion.Click -= btnAbstencion_Click;
                btnContra.Click -= btnContra_Click;

                btnAbstencion.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnAbstencion.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnContra.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnContra.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            }

            if (voteType == "abstencion")
            {
                btnFavor.Click -= btnFavor_Click;
                btnAbstencion.Click -= btnAbstencion_Click;
                btnContra.Click -= btnContra_Click;

                btnFavor.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnFavor.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnContra.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnContra.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            }

            if (voteType == "contra")
            {
                btnFavor.Click -= btnFavor_Click;
                btnAbstencion.Click -= btnAbstencion_Click;
                btnContra.Click -= btnContra_Click;
                
                btnFavor.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnFavor.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnAbstencion.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnAbstencion.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            }
             
            txtVotados.Visibility=Visibility.Visible;
            txtVotados.Text = lstVotos.items.Where(v=>v.value==voteType && v.proposalId==objPropuesta._id).Count().ToString() + " personas han votado " + voteType + " como tú";   
            
        }

        private void loadVotos()
        {
            bool findedVote = false;
            int votesCount = 0;
            string voteType = "";

            if (objPropuesta.votes.favor.participantes.Where(p => p.fcbookid == id).ToList().Count > 0)
            {
                findedVote = true;
                votesCount = objPropuesta.votes.favor.participantes.Count;
                voteType = "a favor";

                btnFavor.Click -= btnFavor_Click;
                btnAbstencion.Click -= btnAbstencion_Click;
                btnContra.Click -= btnContra_Click;

                btnAbstencion.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnAbstencion.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnContra.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnContra.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            }

            if (objPropuesta.votes.abstencion.participantes.Where(p => p.fcbookid == id).ToList().Count > 0)
            {
                findedVote = true;
                votesCount = objPropuesta.votes.abstencion.participantes.Count;
                voteType = "en abstencion";

                
                btnFavor.Click -= btnFavor_Click;
                btnAbstencion.Click -= btnAbstencion_Click;
                btnContra.Click -= btnContra_Click;

                btnFavor.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnFavor.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnContra.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnContra.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            }

            if (objPropuesta.votes.contra.participantes.Where(p => p.fcbookid == id).ToList().Count > 0)
            {
                findedVote = true;
                votesCount = objPropuesta.votes.contra.participantes.Count;
                voteType = "en contra";

                btnFavor.Click -= btnFavor_Click;
                btnAbstencion.Click -= btnAbstencion_Click;
                btnContra.Click -= btnContra_Click;

                btnFavor.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnFavor.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnAbstencion.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                btnAbstencion.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            }

            if (findedVote)
            {
                txtVotados.Text = votesCount.ToString() + " personas han votado " + voteType + " como tú";
            }
        }

        


        private void btnFavor_Click(object sender, RoutedEventArgs e)
        {
            voteToPost objVotoAdd = new voteToPost();
            objVotoAdd.fcbookid = id;
            objVotoAdd.proposalId = this.objPropuesta._id;
            objVotoAdd.value = "favor";
            
            enviaVoto(objVotoAdd);

        }

        private void btnAbstencion_Click(object sender, RoutedEventArgs e)
        {
            voteToPost objVotoAdd = new voteToPost();
            objVotoAdd.fcbookid = id;
            objVotoAdd.proposalId = this.objPropuesta._id;
            objVotoAdd.value = "abstencion";

            enviaVoto(objVotoAdd);
        }

        private void btnContra_Click(object sender, RoutedEventArgs e)
        {
            voteToPost objVotoAdd = new voteToPost();
            objVotoAdd.fcbookid = id;
            objVotoAdd.proposalId = this.objPropuesta._id;
            objVotoAdd.value = "contra";

            enviaVoto(objVotoAdd);

        }

        private void lstRespuestas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            respondePregunta();
        }

        private async void respondePregunta()
        {

            Answer objAnsSelected = lstRespuestas.SelectedItem as Answer;
 

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //string strJSON = JsonConvert.SerializeObject(objAnsSelected, Formatting.None);

            string strJSON = "{\"fcbookid\":\"" + id + "\"}";
            HttpContent content = new StringContent(strJSON);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string urlPOST = "http://justiciacotidiana.mx:8080/justiciacotidiana/api/v1/preguntas/" + objPropuesta.question._id + "?answer=" + objAnsSelected._id;
            var response = await httpClient.PostAsync(urlPOST,content);

            var responseString = await response.Content.ReadAsStringAsync();

            var responseRespuestas = await httpClient.GetAsync("http://justiciacotidiana.mx:8080/justiciacotidiana/api/v1/respuestas");

            var responseStringRespuestas = await responseRespuestas.Content.ReadAsStringAsync();

            lstRespuestaGrafica objRespuestas = JsonConvert.DeserializeObject<lstRespuestaGrafica>(responseStringRespuestas);
            List<RespuestaGrafica> lstFiltered = objRespuestas.items.Where(p => p.questionId == objPropuesta.question._id).ToList();

            //RespuestaGrafica ansFilt = lstFiltered.Find(p => p.fcbookid == id);


            if (lstFiltered != null)
            {
                MessageBox.Show("Gracias por enviar tu respuesta","Justicia Cotidiana",MessageBoxButton.OK);
                chrtPie.Visibility = Visibility.Visible;
                grdPregunta.Visibility = Visibility.Collapsed;
                txtPreguntaVis.Visibility = Visibility.Visible;

                graficaRespuestas(lstFiltered);
            }
            
        }

        private void browContenido_Loaded(object sender, RoutedEventArgs e)
        {

            string javascriptKillScroll = @"window.manipulationTarget = null;

window.onmanipulationdelta = function () {
    if (!window.manipulationTarget) {
        return '';
    }

    var target = window.manipulationTarget;

    var top = target.scrollTop == 0;
    var bottom = target.scrollTop + target.clientHeight == target.scrollHeight;

    return top ? bottom ? 'both' : 'top': bottom ? 'bottom' : '';
};

window.onmanipulationcompleted = function () {
    window.manipulationTarget = null;
};

// and you'll need to make calls to every elements
// with overflow auto or scroll with this:
function preventBouncing(ele) {
    // using jQuery.
    ele.on('MSPointerDown pointerdown', function (e) {
        window.manipulationTarget = this;
    });
}";


            

            browContenido.NavigateToString("<!doctype html><html><head><style>img {width: 100%;height: auto;} iframe {width:100%; height:500px !important;}</style></head><body onLoad='window.external.notify('rendered_height='+document.getElementById('content').clientHeight);'><div id='content'>" + txtWebBorw + "</div></body></html>");
            browContenido.UpdateLayout();

        }

        

    private void Browser_dohack(object sender, NavigationEventArgs e)
    {
        string html = browContenido.SaveToString();
        string hackstring = "<meta name=\"viewport\" content=\"width=320,user-scalable=yes\" />";
        html = html.Insert(html.IndexOf("<head>", 0) + 6, hackstring);
        browContenido.NavigateToString(html);
        browContenido.LoadCompleted -= Browser_dohack;
    }

        public commentToPost objComentarioAdd;

        private void LoadUserInfo()
        {
            var fb = new FacebookClient(sessionStg.AccessToken);

            fb.GetCompleted += (o, e) =>
            {
                if (e.Error != null)
                {
                    Dispatcher.BeginInvoke(() => MessageBox.Show(e.Error.Message));
                    return;
                }

                var result = (IDictionary<string, object>)e.GetResultData();

                Dispatcher.BeginInvoke(() =>
                {
                    //var profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", App.FacebookId, "square", App.AccessToken);
                    

                    name= (string)result["name"];
                    id= (string)result["id"];
                });
            };

            fb.GetTaskAsync("me");
        }

        private async void btnEnviar_Click(object sender, RoutedEventArgs e)
        {

            string strJSON = "{\"parent\":\"\",\"proposalId\":\"" + this.objPropuesta._id + "\",\"from\":{\"fcbookid\":\"" + id + "\",\"name\":\"" + name + "\"},\"message\":\"" + txtArgumento.Text + "\"}";

            //string result = json.Replace("\"", "\"\"");

            //string strJSON = @"{'parent':'','proposalId':'"+this.objPropuesta._id+"','from'{'fcbookid':'"+id+"','name':'"+name+"'},'message':'"+txtArgumento.Text+"'}";

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //string strJSON = JsonConvert.SerializeObject(objComentarioAdd, Formatting.None);
            HttpContent content = new StringContent(strJSON);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync("http://justiciacotidiana.mx:8080/justiciacotidiana/api/v1/comentarios", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var serializer = new JsonSerializer();

            MessageBox.Show("Gracias por enviar tu comentario", "Gracias", MessageBoxButton.OK);

        

            loadComentarios(true);



        }

        private void browContenido_ScriptNotify(object sender, NotifyEventArgs e)
        {
            string[] valuePair = e.Value.Split('=');
            if (valuePair != null && valuePair[0] == "rendered_height")
                browContenido.Height = double.Parse(valuePair[1])/2.8;


            browContenido.SizeChanged += browContenido_SizeChanged;

        }

        void browContenido_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //browContenido.IsHitTestVisible = true;
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }



    }

    public class PieDataItem
    {
        public string Title { get; set; }
        public double Value { get; set; }
    }
}