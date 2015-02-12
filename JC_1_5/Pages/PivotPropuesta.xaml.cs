using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace JC_1_5.Pages
{
    public partial class PivotPropuesta : PhoneApplicationPage
    {
        public PivotPropuesta()
        {
            InitializeComponent();
            string txtWebBorw = "<p>De manera <strong>cotidiana<//strong> los automovilistas sufren percances y da&ntilde;os a sus veh&iacute;culos a causa de <strong>calles mal construidas<//strong>, en p&eacute;simo estado y llenas de baches. Sin embargo, los gastos que ocasiona la reparaci&oacute;n de estos da&ntilde;os deber&iacute;an ser cubiertos por los gobiernos municipales mediante una indemnizaci&oacute;n a los propietarios, ya que son las instancias de gobierno encargadas de la construcci&oacute;n y mantenimiento de las calles y avenidas, pero eso no sucede, por lo que se incurre en un incumplimiento de la obligaci&oacute;n constitucional de la responsabilidad patrimonial del Estado que se actualiza cuando, por motivo de la actividad administrativa irregular estatal, -por ejemplo construir mal las calles o por un deficiente trabajo de bacheo- cause da&ntilde;os en los bienes o derechos de los particulares.</p>\r\n<p><img src=\"http://noticiasnet.mx/portal/sites/default/files/fotos/2013/07/18/baches.jpg\" alt=\"\" width=\"1920\" height=\"1080\" /></p>\r\n<p>En este sentido, es urgente que para este tipo de casos, y muchos otros mediante los cuales los ciudadanos se ven perjudicados por la actividad administrativa irregular por parte de los tres niveles de gobierno,(por ejemplo cuando sentencia a prisi&oacute;n a una persona inocente) que las personas afectadas puedan ser indemnizados conforme a derecho.</p>";
            browContenido.NavigateToString("<!doctype html><html><head><style>img {width: 100%;height: auto;} iframe {width:100%; height:500px !important;}</style></head><body>" + txtWebBorw + "</body></html>"); 
            
            
        }
    }
}