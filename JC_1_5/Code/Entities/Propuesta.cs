using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC_1_5.Code.Entities
{
    public class Edad
    {
        public Edad(String desc)
        {
            descripcion=desc;
        }
        public string descripcion {get;set;}
    }

    public class Propuesta
    {
        public string _id { get; set; }
        public string created { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public Question question { get; set; }
        public bool valid { get; set; }
        public Comments  comments { get; set; }
        public Votes votes { get; set; }
        public Author author { get; set; }
        
    }

    public class Author
    {
        public string name { get; set; }
        public object fcbookid { get; set; }
        public Uri urlFoto { get; set; }
    }

    public class From
    {
        public string name { get; set; }
        public string fcbookid { get; set; }
        public Uri urlFoto { get; set; }
    }


    public class Participante
    {
        public string _id { get; set; }
        public string fcbookid { get; set; }
    }

    public class Answer
    {
        public string _id { get; set; }
        public string title { get; set; }
        public int count { get; set; }
        public List<Participante> participantes { get; set; }
    }

    public class Question
    {
        public string _id { get; set; }
        public string title { get; set; }
        public List<Answer> answers { get; set; }
    }

    public class Favor
    {
        public List<Participante> participantes { get; set; }
    }

    public class Contra
    {
        public List<Participante> participantes { get; set; }
    }

    public class Abstencion
    {
        public List<Participante> participantes { get; set; }
    }

    public class Votes
    {
        public Favor favor { get; set; }
        public Contra contra { get; set; }
        public Abstencion abstencion { get; set; }
        public int Total { get; set; }
        public string voteType { get; set; }        
    }


    public class lstPropuestas
    {
        public int count { get; set; }
        public List<Propuesta> items { get; set; }
    }

    public class TestimonioAdding
    {
        public string name { get; set; }
        public string email { get; set; }
        public string category { get; set; }
        public string explanation { get; set; }
        public string state { get; set; }
        public string gender { get; set; }
        public string age { get; set; }
        public string grade { get; set; }
    }

    public class Testimonio
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string category { get; set; }
        public string explanation { get; set; }
        public string state { get; set; }
        public string gender { get; set; }
        public string age { get; set; }
        public string education { get; set; }
        public string created { get; set; }
        public string fcbookid { get; set; }
        public bool valid { get; set; }
    }

    public class lstTestimonios
    {
        public int count { get; set; }
        public List<Testimonio> items { get; set; }
    }

    public class responseAfterAddT
    {
        public string result { get; set; }
        public string _id { get; set; }
    }

    public class voteToPost
    {
        public string proposalId { get; set; }
        public string fcbookid { get; set; }
        public string value { get; set; }
    }

    public class commentToPost
    {
        public string parent { get; set; }
        public string proposalId { get; set; }
        public From from { get; set; }
        public string message { get; set; }
    }


    public class Comments
    {
        public List<Comentario> data { get; set; }
    }

    public class Comentario
    {
        public string _id { get; set; }
        public string message { get; set; }
        public string parent { get; set; }
        public string created { get; set; }
        public From from { get; set; }
        public string ipuser { get; set; }
        public bool? active { get; set; }
    }

   

    public class VotoIsolated
    {
        public string _id { get; set; }
        public string fcbookid { get; set; }
        public string proposalId { get; set; }
        public string value { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
        public string facebookname { get; set; }
    }

    public class lstVotoIsolated
    {
        public int count { get; set; }
        public List<VotoIsolated> items { get; set; }
    }


    public class answerToPost
    {
        //public string _id { get; set; }
        //public string created { get; set; }
        public string answerId { get; set; }
        public string fcbookid { get; set; }
        public string questionId { get; set; }
    }

    public class Statistic
    {
        public string answerId { get; set; }
        public string title { get; set; }
        public int count { get; set; }
    }

    public class GraphStats
    {
        public string result { get; set; }
        public List<Statistic> statistics { get; set; }
        public string _id { get; set; }
    }

    public class RespuestaGrafica
    {
        public string _id { get; set; }
        public string created { get; set; }
        public string answerId { get; set; }
        public string fcbookid { get; set; }
        public string questionId { get; set; }
    }

    public class lstRespuestaGrafica
    {
        public int count { get; set; }
        public List<RespuestaGrafica> items { get; set; }
    }

    
}