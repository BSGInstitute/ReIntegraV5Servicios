namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PgeneralCriterioEvaluacionHijoDTO
    {
        public int IdPgeneral { get; set; }
        public bool ConsiderarNota { get; set; }
        public int? Porcentaje { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdTipoPromedio { get; set; }
        public int IdPgeneralHijo { get; set; }
        public int? IdCriterioEvaluacion { get; set; }
    }
    public class PgeneralCriterioEvaluacionHijoV2DTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public bool ConsiderarNota { get; set; }
        public int Porcentaje { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdTipoPromedio { get; set; }
    }
}
