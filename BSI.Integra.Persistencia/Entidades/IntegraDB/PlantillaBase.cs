using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PlantillaBase : BaseIntegraEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        [StringLength(200)]
        public string Descripcion { get; set; } = null!;
        ///<value>1</value>
        public const int Speech = 1;
        ///<value>2</value>
        public const int Email = 2;
        ///<value>3</value>
        public const int DocumentoVentas = 3;
        ///<value>4</value>
        public const int SpeechDespedida = 4;
        ///<value>7</value>
        public const int WhatsappPropio = 7;
        ///<value>8</value>
        public const int WhatsappFacebook = 8;
        ///<value>9</value>
        public const int FacebookMessenger = 9;
        ///<value>10</value>
        public const int FacebookComentarios = 10;
        ///<value>11</value>
        public const int InstagramComentarios = 11;
        ///<value>12</value>
        public const int Certificado = 12;
        ///<value>13</value>
        public const int Constancia = 13;
        ///<value>15</value>
        public const int MensajeTexto = 15;
        ///<value>16</value>
        public const int Carta = 16;
    }
}
