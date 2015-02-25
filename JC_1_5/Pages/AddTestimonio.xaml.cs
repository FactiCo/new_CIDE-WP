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
using System.IO;
using System.Text;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls.Primitives;
using Microsoft.Phone.Tasks;
using JC_1_5.usrControls;

namespace JC_1_5.Pages
{
    

    public partial class AddTestimonio : PhoneApplicationPage
    {

        public AddTestimonio()
        {
            DataContext = this;
            InitializeComponent();
            loadListas();
        }
        
        private void loadListas()
        {
             
        }
        string jusID;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (NavigationContext.QueryString.TryGetValue("jusID", out currCategory))
            {
                jusID=currCategory;
                txtCategoria.Text = currCategory;
            }
        }

        string currCategory;

        
                
    

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if ((lstEdad.SelectedIndex == -1) | (lstGenero.SelectedIndex == -1) | (lstEscolaridad.SelectedIndex == -1) | (lstEntidad.SelectedIndex == -1) | (txtExplicacion.Text==""))
            {
                MessageBox.Show("Capture todos los campos obligatorios");
                return;
            }

            TestimonioAdding objTestimonio = new TestimonioAdding();

            objTestimonio.name = txtNombre.Text;
            objTestimonio.email = txtCorreo.Text;
            //objTestimonio.category = txtCategoria.Text;

            objTestimonio.category = currCategory;
            objTestimonio.explanation = txtExplicacion.Text;
           
            objTestimonio.state = (lstEntidad.SelectedIndex + 1).ToString();
            objTestimonio.age = (lstEdad.SelectedItem as ListBoxItem).Content.ToString();

            //string selectedAge= (lpickEdad.SelectedItem as Edad).descripcion;

            //objTestimonio.age =  selectedAge;

            //objTestimonio.age = (lngListaEdad.SelectedItem as ListBoxItem).Content.ToString();

            objTestimonio.gender = (lstGenero.SelectedItem as ListBoxItem).Content.ToString();
            objTestimonio.grade = (lstEscolaridad.SelectedItem as ListBoxItem).Content.ToString();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            string strJSON = JsonConvert.SerializeObject(objTestimonio, Formatting.None);
            HttpContent content = new StringContent(strJSON);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync("http://www.justiciacotidiana.mx:8080/justiciacotidiana/api/v1/testimonios", content);

           var responseString = await response.Content.ReadAsStringAsync();
            var serializer = new JsonSerializer();

            Popup popup = new Popup();
            popup.Height = 300;
            popup.Width = 400;
            popup.VerticalOffset = 100;
            popup.HorizontalAlignment = HorizontalAlignment.Center;

            popupAfterConfirm control = new popupAfterConfirm();
            popup.Child = control;
            
            var values = serializer.Deserialize(new StringReader(responseString), typeof(responseAfterAddT));

            if (values != null)
            {
                popup.IsOpen = true;
            }
            
            control.btnOK.Click += (s, args) =>
            {

                if (this.NavigationService.CanGoBack)
                {
                    popup.IsOpen = false;
                    this.NavigationService.GoBack();
                }

            };

            control.btnCompartir.Click += (s, args) =>
            {
                if (this.NavigationService.CanGoBack)
                {
                    ShareStatusTask shareStatusTask = new ShareStatusTask();

                    shareStatusTask.Status = "He enviado un testimonio sobre #JusticiaCotidiana desde www.justiciacotidiana.mx @JusCotidiana";
                    shareStatusTask.Show();
                    popup.IsOpen = false;

                    this.NavigationService.GoBack();

                }

            };

        }
    }
}