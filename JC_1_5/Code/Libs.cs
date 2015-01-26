using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading;
using Facebook;

using JC_1_5.Code.Entities;

namespace JC_1_5.Code
{
    
    class Libs
    {
        public lstTestimonios currObjRespTestimonios;

        public Libs()
        {
            
        }

        public string getTestimonioCategory(string jusID)
        {
            string currCategory;
            
            switch (jusID)
            {
                case "trabajo":
                    currCategory = "Justicia en el trabajo";
                    break;
                case "familia":
                    currCategory = "Justicia en las familias";
                    break;
                case "vecinal":
                    currCategory = "Justicia vecinal y comunitaria";
                    break;
                case "ciudadanos":
                    currCategory = "Justicia para ciudadanos";
                    break;
                case "emprendedores":
                    currCategory = "Justicia para emprendedores";
                    break;
                case "otros":
                    currCategory = "Otros temas de Justicia Cotidiana";
                    break;
                default:
                    currCategory = "Otros temas de Justicia Cotidiana";
                    break;
            }
            return currCategory;
        }
    }
}
