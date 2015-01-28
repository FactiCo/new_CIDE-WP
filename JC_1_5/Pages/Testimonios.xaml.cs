using System;
using System.Collections.Generic;
using System.Linq;

using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading;
using JC_1_5.Code.Entities;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace JC_1_5.Pages
{
    public partial class Testimonios : PhoneApplicationPage
    {
        string jusID;
        public Testimonios()
        {
            InitializeComponent();
            jusID = "";
            objHelper = new Code.Libs();


        }

        JC_1_5.Code.Libs objHelper;
        private string currCategory;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string valueID;
            if (NavigationContext.QueryString.TryGetValue("jusID", out valueID))
            {
                
                currCategory = objHelper.getTestimonioCategory(valueID);
                this.jusID = valueID;
                loadTestimonios(jusID);
            }
        }



        public async void loadTestimonios(string jusID)
        {
            txtTitlePano.Text = "Justicia";
            
            txtTitleItemPano.Text = this.jusID.First().ToString().ToUpper() + String.Join("", jusID.Skip(1));

            switch (this.jusID)
            {
                case "trabajo":

                    imgLogo.Source = new BitmapImage(new Uri(@"/Assets/Icons/ICONO_JUSTICIA_TRABAJO_TRANS.png", UriKind.Relative));

                    txtExplicacion.Text = @"Despidos injustificados, prestaciones no entregadas, legislación laboral no aplicada, son algunos temas que se abordan en el Foro “Justicia en el trabajo”.

Lugar: Aguascalientes 
Fecha: 22 de enero de 2015

¿Tienes alguna experiencia qué contar? Mándanos tu testimonio. 
";

                    break;
                case "familia":

                    imgLogo.Source = new BitmapImage(new Uri(@"/Assets/Icons/ICONO_JUSTICIA_FAMILIA_TRANS.png", UriKind.Relative));
                    txtExplicacion.Text = @"Divorcios, herencias, pensiones, tutelas o violencia familiar son temas complejos, pues entran en juego sentimientos y relaciones de poder. Estos temas se tratan en el Foro “Justicia para familias”.

Lugar: Tijuana
Fecha: 4 de febrero de 2015

¿Tienes alguna experiencia qué contar? Mándanos tu testimonio. 
";
   
                    break;
                case "vecinal":

                    imgLogo.Source = new BitmapImage(new Uri(@"/Assets/Icons/ICONO_JUSTICIA_VECINAL_TRANS.png", UriKind.Relative));
                    txtExplicacion.Text = @"Contaminación ambiental, usos de suelo y prevención de la violencia vecinal y comunitaria son temas que se tratan en el Foro “Justicia vecinal y comunitaria”. 

Lugar: Tuxtla Gutiérrez 
Fecha: 19 de febrero de 2015

¿Tienes alguna experiencia qué contar? Mándanos tu testimonio. 
";
                    
                    break;
                case "ciudadanos":

                    imgLogo.Source = new BitmapImage(new Uri(@"/Assets/Icons/ICONO_JUSTICIA_CIUDADANOS_TRANS.png", UriKind.Relative));
                    txtExplicacion.Text = @"Una multa injusta, una licitación fuera de la ley o el mal mantenimiento de las calles son ejemplos de actos de autoridad que se combaten ante tribunal. Estos temas se tratan en “Justicia para ciudadanos”. 

Lugar: Guanajuato 
Fecha: 29 de enero de 2015

¿Tienes alguna experiencia qué contar? Mándanos tu testimonio. 

";
                   
                    break;
                case "emprendedores":

                    imgLogo.Source = new BitmapImage(new Uri(@"/Assets/Icons/ICONO_JUSTICIA_EMPRENDEDORES_TRANS.png", UriKind.Relative));
                    txtExplicacion.Text = @"Para las empresas es arriesgado invertir cuando no cuentan con las instituciones de justicia adecuadas. Estos temas se discuten en el Foro “Justicia para emprendedores”. 

Lugar: Monterrey 
Fecha: 12 de febrero de 2015

¿Tienes alguna experiencia qué contar? Mándanos tu testimonio. 
 
";
                    
                    break;
                case "otros":

                    imgLogo.Source = new BitmapImage(new Uri(@"/Assets/Icons/ICONO_JUSTICIA_OTROS_TRANS.png", UriKind.Relative));
                    txtExplicacion.Text = @"Conflictos agrarios, protección a consumidores y a usuarios del sistema bancario son temas que se abordan en el Foro “Otras Justicias”. 

Lugar: Ciudad de México 
Fecha: 25 de febrero de 2015

";


                    break;

            }

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync("http://www.justiciacotidiana.mx:8080/justiciacotidiana/api/v1/testimonios");
            //response.Content.Headers.Add("", "application/json");
            
            var responseString = await response.Content.ReadAsStringAsync();
            Code.Entities.lstTestimonios currObjRespTestimonios = JsonConvert.DeserializeObject<lstTestimonios>(responseString);

            foreach (Testimonio exp in currObjRespTestimonios.items)
            {
                if (exp.gender == "Hombre")
                {
                    exp.gender = @"/Assets/Icons/ICONO_HOMBRE.png";
                }
                else if (exp.gender == "Mujer")
                {
                    exp.gender = @"/Assets/Icons/ICONO_MUJER.png";
                }
                else
                {
                    exp.gender = @"/Assets/Icons/NEW_LOGO.png";
                }
            }

    

            if (currObjRespTestimonios.count > 0)
            {
                lbxTestimoniosRoot.ItemsSource = currObjRespTestimonios.items.Where(p => p.category == currCategory).OrderByDescending(p => p.created).ToList();    
            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {

        }

        private void barAdd_Click(object sender, EventArgs e)
        
        {
            NavigationService.Navigate(new Uri("/Pages/AddTestimonio.xaml?jusID=" + currCategory, UriKind.Relative));
        }

        private void btnVerMas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/DetalleCategoria.xaml?jusID=" + jusID, UriKind.Relative));
        }

        
    }
}