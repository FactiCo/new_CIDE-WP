﻿#pragma checksum "C:\Users\Mario\documents\GitHub\new_CIDE-WP\JC_1_5\Pages\PanoPropuestas.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AFAC58564D48814823FD5C346976C18D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34014
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using AmCharts.Windows.QuickCharts;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace JC_1_5.Pages {
    
    
    public partial class PanoPropuestas : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.ImageBrush imgBrsUsr;
        
        internal System.Windows.Controls.TextBlock txtAutor;
        
        internal System.Windows.Controls.TextBlock txtTitulo;
        
        internal Microsoft.Phone.Controls.WebBrowser browContenido;
        
        internal System.Windows.Controls.Button btnFavor;
        
        internal System.Windows.Controls.Image imgFavor;
        
        internal System.Windows.Controls.Button btnAbstencion;
        
        internal System.Windows.Controls.Image imgAbstencion;
        
        internal System.Windows.Controls.Button btnContra;
        
        internal System.Windows.Controls.Image imgContra;
        
        internal System.Windows.Controls.TextBlock txtVotados;
        
        internal System.Windows.Controls.Grid grdPregunta;
        
        internal System.Windows.Controls.TextBlock txtPregunta;
        
        internal System.Windows.Controls.ListBox lstRespuestas;
        
        internal System.Windows.Controls.TextBlock txtPreguntaVis;
        
        internal AmCharts.Windows.QuickCharts.PieChart chrtPie;
        
        internal System.Windows.Controls.ListBox lstArgumenta;
        
        internal System.Windows.Controls.TextBox txtArgumento;
        
        internal System.Windows.Controls.Button btnEnviar;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/JC_1_5;component/Pages/PanoPropuestas.xaml", System.UriKind.Relative));
            this.imgBrsUsr = ((System.Windows.Media.ImageBrush)(this.FindName("imgBrsUsr")));
            this.txtAutor = ((System.Windows.Controls.TextBlock)(this.FindName("txtAutor")));
            this.txtTitulo = ((System.Windows.Controls.TextBlock)(this.FindName("txtTitulo")));
            this.browContenido = ((Microsoft.Phone.Controls.WebBrowser)(this.FindName("browContenido")));
            this.btnFavor = ((System.Windows.Controls.Button)(this.FindName("btnFavor")));
            this.imgFavor = ((System.Windows.Controls.Image)(this.FindName("imgFavor")));
            this.btnAbstencion = ((System.Windows.Controls.Button)(this.FindName("btnAbstencion")));
            this.imgAbstencion = ((System.Windows.Controls.Image)(this.FindName("imgAbstencion")));
            this.btnContra = ((System.Windows.Controls.Button)(this.FindName("btnContra")));
            this.imgContra = ((System.Windows.Controls.Image)(this.FindName("imgContra")));
            this.txtVotados = ((System.Windows.Controls.TextBlock)(this.FindName("txtVotados")));
            this.grdPregunta = ((System.Windows.Controls.Grid)(this.FindName("grdPregunta")));
            this.txtPregunta = ((System.Windows.Controls.TextBlock)(this.FindName("txtPregunta")));
            this.lstRespuestas = ((System.Windows.Controls.ListBox)(this.FindName("lstRespuestas")));
            this.txtPreguntaVis = ((System.Windows.Controls.TextBlock)(this.FindName("txtPreguntaVis")));
            this.chrtPie = ((AmCharts.Windows.QuickCharts.PieChart)(this.FindName("chrtPie")));
            this.lstArgumenta = ((System.Windows.Controls.ListBox)(this.FindName("lstArgumenta")));
            this.txtArgumento = ((System.Windows.Controls.TextBox)(this.FindName("txtArgumento")));
            this.btnEnviar = ((System.Windows.Controls.Button)(this.FindName("btnEnviar")));
        }
    }
}

