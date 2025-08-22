using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class EstadoMatricula : BaseIntegraEntity
    {
        [StringLength(50)]
        public string _EstadoMatricula { get; set; } = null!;

        ///<value>1</value>
        public static int Regular { get; } = 1;
        ///<value>2</value>
        public static int Reservado { get; } = 2;
        ///<value>3</value>
        public static int RetiroAprobado { get; } = 3;
        ///<value>4</value>
        public static int Beca { get; } = 4;
        ///<value>5</value>
        public static int Culminado { get; } = 5;
        ///<value>6</value>
        public static int Reincorporado { get; } = 6;
        ///<value>7</value>
        public static int CulminadoDeudor { get; } = 7;
        ///<value>8</value>
        public static int Abandono { get; } = 8;
        ///<value>11</value>
        public static int AbandonoReincorporado { get; } = 11;
        ///<value>12</value>
        public static int Certificado { get; } = 12;
        ///<value>13</value>
        public static int EvaluacionTrasladoBeneficio { get; } = 13;
        ///<value>14</value>
        public static int EvaluacionPeriodoGracia { get; } = 14;
        ///<value>15</value>
        public static int EvaluacionRetiro { get; } = 15;
        ///<value>20</value>
        public static int PorAbandonar { get; } = 20;
        ///<value>25</value>
        public static int Moroso { get; } = 25;
        ///<value>26</value>
        public static int EnEvaluacion { get; } = 26;
        ///<value>28</value>
        public static int MorosoReportado { get; } = 28;
    }
}
