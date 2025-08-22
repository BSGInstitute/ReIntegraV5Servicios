using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CategoriaPrograma : BaseIntegraEntity
    {
        public string Categoria { get; set; } = null!;
        public bool Visible { get; set; } = false;
        ///<value>3</value>
        public const int PROGRAMAS = 3;
        ///<value>4</value>
        public const int CURSOS = 4;
        ///<value>11</value>
        public const int CARRERA_PROFESIONAL = 11;
        ///<value>14</value>
        public const int SUBCRIPCIONES = 14;
        ///<value>15</value>
        public const int BOOTCAMP = 15;
    }
}
