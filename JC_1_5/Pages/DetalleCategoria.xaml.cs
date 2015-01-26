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
namespace JC_1_5.Pages
{
    public partial class DetalleCategoria : PhoneApplicationPage
    {
        public DetalleCategoria()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string valueID;
            if (NavigationContext.QueryString.TryGetValue("jusID", out valueID))
            {   
                loadDetalle(valueID);
            }
        }

        Libs objHelper;
        private void loadDetalle(string jusID)
        {
            objHelper = new Libs();
            txtTitle.Text = objHelper.getTestimonioCategory(jusID);
            switch (jusID)
            {
                case "trabajo":

                    txtDescripcion.Text = @"¿Está funcionando la reforma laboral?
¿Resuelven las juntas los conflictos obrero patronales?
¿Una sola ley produce criterios uniformes?

Estas y otras interrogantes se abordan aquí y en el Foro de Justicia en el trabajo que se desarrolla el 22 de enero en la ciudad de Aguascalientes. 

¿Por qué es importante? 

El taller, la fábrica, la oficina y otros centros de trabajo son campos propicios para que afloren problemas, conflictos y controversias.

Quienes participan en el mundo laboral, el trabajador y el empleado, el patrón y el empleador, al igual que el administrador o gerente, tienen intereses encontrados y a menudo opuestos. En el proceso productivo desempeñan papeles bien delimitados en el que unos supervisan, exigen y controlan el trabajo de otros para lograr las metas y objetivos que permitirán competir, vender, ganar y expandir mercados. A menudo, la comunicación entre quienes participan en el proceso no es clara, frecuente ni oportuna y esto genera conflictos por no compartirse objetivos.

Las unidades económicas requiren mecanismos ágiles, sencillos, poco costosos, pero sobre todo efectivos que canalicen y resuelvan con prontitud, certeza y transparencia las controversias que se dan en la oficina, la fábrica, el taller, y cualquier centro de trabajo. 

Te invitamos a compartir con el CIDE tus testimonios al utilizar o acercarte a la justicia en materia laboral en México. Estos testimonios nos permiten mapear problemáticas concretas y estar en condiciones de elaborar recomendaciones de mejora cercanas a la gente. 

Tema: Justicia en el trabajo
Lugar: Aguascalientes
Fecha: 22 de enero de 2015";
                    break;
                case "familia":


                    txtDescripcion.Text = @"¿En qué casos se puede y debe simplificar la legislación en materia de justicia familiar? 
¿Cuándo es necesario utilizar sistemas alternativos de solución de conflictos?
¿Se requieren nuevas políticas públicas para la justicia familiar en México?

Estas y otras interrogantes se abordan aquí y en el “Foro de Justicia para familias” que se desarrolla el 4 de febrero en la ciudad de Tijuana. 
El Foro “Justicia para familias” busca discutir problemáticas e identificar fórmulas sencillas para mantener el equilibrio y la cohesión familiar.
¿Por qué es importante?
Divorcio: Después del DF, al menos 8 entidades tienen la figura del divorcio incausado o sin causales, que en los hechos ha mostrado gran utilidad práctica.
Alimentos: Los juicios siguen siendo largos y el deudor alimentario tiene vías para evitar el cumplimiento de obligaciones.
Sucesiones: Indispensable resolver problemas de sucesiones en familias de escasos recursos.
Adopción: Los trámites de adopción son largos y engorrosos, por ello deben analizarse de manera tal que faciliten los procesos de los estudios psicológicos y económicos que ayudan a determinar la idoneidad de la adopción. 
Interdicción y tutela: Requerirá una revisión profunda para dar alternativas viables a personas que en los hechos pueden ser inexistentes jurídicamente por lo complejo del nombramiento de un tutor. 
Violencia intrafamiliar: Es mucho lo que se puede proponer para ayudar a personas que sufren violencia y que no encuentran actualmente cauce idóneo, oportuno y eficaz de protección.
Te invitamos a compartir con el CIDE tus testimonios al utilizar o acercarte a la justicia en materia familiar en México. Estos testimonios nos permiten mapear problemáticas concretas y estar en condiciones de elaborar recomendaciones de mejora cercanas a la gente. 
Tema: Justicia para familias
Lugar: Tijuana
Fecha: 4 de febrero de 2015
";
                    break;
                case "vecinal":

                    txtDescripcion.Text = @"¿Cuáles son los conflictos más comunes en los espacios de convivencia? 
¿Qué mecanismos garantizan una rápida y eficaz resolución de problemas entre vecinos?
¿Cómo mejorar la justicia de proximidad? 

Estas y otras interrogantes se abordan aquí y en el “Foro de Justicia vecinal y comunitaria” que se desarrolla el 19 de febrero en la ciudad de Tuxtla Gutiérrez. 

¿Por qué es importante?

Los conflictos comunitarios y vecinales representan un área sensible y compleja del mundo social. Prácticamente en todos los espacios de convivencia, ya sean rurales o urbanos, el conflicto representa un aspecto constitutivo de la vida cotidiana.

Disputas por el espacio y el uso de suelo, problemas sobre basura y contaminación auditiva en barrios y condominios, pleitos entre automovilistas por el derecho de paso o litigios entre autoridades y vecinos son, tan solo, una mínima lista del conjunto de interacciones sociales de carácter conflictivo que se presentan cotidianamente en la sociedad.

Se entiende por conflicto vecinal a un tipo particular de relaciones sociales que es el resultado de la convergencia espacio-temporal de intereses incompatibles [razones del conflicto] entre dos o más actores [partes del conflicto] con respecto a los usos o localización de un pedazo concreto del territorio habitado [componente geográfico del conflicto]. 

El Foro de Justicia vecinal y comunitaria busca identificar cómo mejorar los mecanismos de mediación y conciliación entre vecinos, acercar la justicia a las comunidades y prevenir la violencia en espacios o territorios específicos. 

Te invitamos a compartir con el CIDE tus testimonios al utilizar o acercarte a la justicia vecinal o comunitaria en México. Estos testimonios nos permiten mapear problemáticas concretas y estar en condiciones de elaborar recomendaciones de mejora cercanas a la gente. 

Tema: Justicia vecinal y comunitaria
Lugar: Tuxtla Gutiérrez
Fecha: 19 de febrero de 2015
";
                    break;
                case "ciudadanos":

                    txtDescripcion.Text = @"¿Qué tan conocidos y utilizados son los tribunales administrativos por los ciudadanos? 
¿Cómo lograr que los juicios administrativos sean más ágiles, sencillos y baratos? 
¿Qué procedimientos deben modificarse para hacer la justicia administrativa más cercana y útil a la gente? 

Estas y otras interrogantes se abordan aquí y en el Foro de Justicia para ciudadanos que se desarrolla el 29 de enero en la ciudad de Guanajuato. 

¿Por qué es importante? 

Una multa o clausura injustas, una licitación alejada de la ley o el mal mantenimiento de las calles, son ejemplos de actos de autoridad que se pueden combatir ante un Tribunal Contencioso Administrativo.

Los juicios ante estos tribunales buscan solucionar los conflictos entre el Estado y los ciudadanos, a quienes se les brinda la posibilidad obtener un trato justo y asegurar una reparación adecuada de los agravios causados.

Cuando existe certeza sobre la aplicación del derecho, hay confianza en el funcionamiento de las instituciones. La justicia accesible, oportuna y las resoluciones respetadas, son un claro indicador de que se vive en un Estado de derecho. Ello resulta en garantía a la ciudadanía para poder resolver los conflictos directamente con la propia autoridad, así como para acudir a los tribunales contenciosos administrativos y obtener remedios efectivos en contra de abusos en el ejercicio del poder.

Sin embargo, la justicia administrativa no escapa a los problemas que enfrentan otros tipos de mecanismos judiciales, por lo que es recomendable preguntarse, de manera crítica, qué vale la pena cambiar para mejorar. 

Te invitamos a compartir con el CIDE tus testimonios al utilizar o acercarte a la justicia en materia administrativa en México. Estos testimonios nos permiten mapear problemáticas concretas y estar en condiciones de elaborar recomendaciones de mejora cercanas a la gente. 

Tema: Justicia para ciudadanos
Lugar: Guanajuato
Fecha: 29 de enero de 2015
";
                    break;
                case "emprendedores":

                    txtDescripcion.Text = @"¿Cuáles son los conflictos más comunes a los que se enfrentan los emprendedores de nuestro país? 
¿Se resuelven o no? ¿Cómo? ¿A qué costos? 
¿Qué se podría hacer para que la resolución de conflictos propios de la actividad empresarial fuese rápida, accesible y eficaz?

Estas y otras interrogantes se abordan aquí y en el “Foro de Justicia para emprendedores” que se desarrolla el 12 de febrero en la ciudad de Monterrey. 

¿Por qué es importante? 

Los conflictos a los que se enfrentan los empresarios son de muchos tipos y en varios ámbitos:

Es muy común que tengan que lidiar con la extorsión y la “mordida” por parte de las autoridades administrativas.  
En el ámbito laboral, un pleito mal llevado puede significar la quiebra del negocio. 
Tratándose de conflictos mercantiles, el cobro de deudas de montos menores es prácticamente incosteable con el sistema de justicia actual. Todo está diseñado para tener que contratar un abogado y, durante el proceso, tener que dar muchas propinas en el juzgado: al actuario, al secretario, al que saca las copias, etcétera.

El objetivo de este foro es conocer a fondo las características de los conflictos a los que se enfrentan los empresarios en México con más frecuencia y las limitaciones del sistema de justicia para resolverlos.

Te invitamos a compartir con el CIDE tus testimonios al utilizar o acercarte a la justicia en materia familiar en México. Estos testimonios nos permiten mapear problemáticas concretas y estar en condiciones de elaborar recomendaciones de mejora cercanas a la gente. 

Tema: Justicia para emprendedores
Lugar: Monterrey
Fecha: 12 de febrero de 2015
";
                    break;
                case "otros":

  

                    txtDescripcion.Text = @"Los abusos e injusticias suceden en numerosos ámbitos y es fundamental conocerlos, entenderlos y modificarlos. 

Desde la resolución de conflictos agrarios, la necesidad de mejorar la capacitación de jueces y defensores, hasta la protección de consumidores y de usuarios del sistema bancario son otros temas de justicia cotidiana que requieren especial atención y consulta. 

Los temas que se abordan en el Foro “Otras Justicias” son:

Protección a consumidores
Sistema bancario
Justicia agraria

Te invitamos a compartir con el CIDE tus testimonios al utilizar o acercarte a la justicia en materia agraria, bancaria o de protección a consumidores en México. Estos testimonios nos permiten mapear problemáticas concretas y estar en condiciones de elaborar recomendaciones de mejora cercanas a la gente. Consensado Consensuado 

Tema: Justicia para consumidores, campesinos y usuarios de la banca
Lugar: Ciudad de México
Fecha: 25 de febrero de 2015
";
                    break;

            }
        }

    }
}