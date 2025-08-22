using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FaseOportunidad : BaseIntegraEntity
    {
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public int? NroMinutos { get; set; }
        public int? IdActividad { get; set; }
        public int? MaxNumDias { get; set; }
        public int? MinNumDias { get; set; }
        public int? TasaConversionEsperada { get; set; }
        public int? Meta { get; set; }
        public bool? Final { get; set; }
        public bool? ReporteMeta { get; set; }
        public bool? EnSeguimiento { get; set; }
        public bool? EsCierre { get; set; }
        public string? Descripcion { get; set; }
        public bool? VisibleEnReporte { get; set; }
        ///<summary>Base No Coberturada</summary>
        public const int BNC = 2;
        ///<summary>No Interesados</summary>
        public const int NI = 3;
        ///<summary>Base de Inubicables para Coberturar</summary>
        public const int BIC = 4;
        ///<summary>Inscrito</summary>
        public const int IS = 5;
        ///<summary>Respuesta Negativa 3</summary>
        public const int RN3 = 6;
        ///<summary>Respuesta Negativa</summary>
        public const int RN = 7;
        ///<summary>Interesado Potencial</summary>
        public const int IP = 8;
        ///<summary>Respuesta Negativa 1</summary>
        public const int RN1 = 9;
        ///<summary>Respuesta Negativa 2 B</summary>
        public const int RN2_B = 10;
        ///<summary>Datos por Eliminar</summary>
        ///<value>10</value>
        public const int RN2 = 10;
        ///<value>41</value>
        public const int RN2A = 41;
        ///<value>11</value>
        public const int E = 11;
        ///<summary>Interesado Concreto</summary>
        public const int IC = 12;
        ///<summary>Interesado por Trabajar</summary>
        public const int IT = 13;
        ///<summary>Promesa de Ficha</summary>
        public const int PF = 22;
        ///<summary>Matriculado</summary>
        public const int M = 23;
        ///<summary>Base Reciclada Marketing</summary>
        public const int BRM = 24;
        ///<summary>devolucion</summary>
        public const int D = 25;
        ///<summary>Reportado</summary>
        public const int RN4 = 26;
        ///<summary>sin respuesta a correo informativo</summary>
        public const int RN5 = 27;
        ///<summary>BNC1</summary>
        public const int BNC1 = 28;
        ///<summary>BNCs cerrados por Antiguedad</summary>
        public const int BRM1 = 29;
        ///<summary>Oportunidad Duplicada</summary>
        public const int OD = 32;
        ///<summary>Oportunidad Multiple</summary>
        public const int OM = 33;
        ///<summary>Respuesta Negativa 8</summary>
        public const int RN8 = 34;
        ///<summary>No Solicito Información</summary>
        public const int NS = 36;
        ///<summary>Respuesta Negativa 2 A</summary>
        public const int RN2_A = 41;
    }
}
