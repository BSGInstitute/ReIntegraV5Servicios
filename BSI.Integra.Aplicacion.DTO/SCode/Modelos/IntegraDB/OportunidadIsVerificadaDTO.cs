namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OportunidadIsVerificadaDTO
    {
        public int IdOportunidad { get; set; }
        public string Asesor { get; set; }
        public string Alumno { get; set; }
        public string CentroCosto { get; set; }
        public string CodigoFaseOportunidad { get; set; }
        public string CodigoMatricula { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public bool Verificado { get; set; }
        public DateTime? UltimaFechaProgramada { get; set; }
        public DateTime FechaCambioIs { get; set; }
    }
    public class OportunidadesVerificadasDTO
    {
        public string Coordinador { get; set; }
        public string Alumno { get; set; }
        public string CentroCosto { get; set; }
        public string FaseOportunidad { get; set; }
        public string CodigoMatricula { get; set; }
    }
    public class OportunidadVerificadaDTO
    {
        public int IdOportunidad { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public bool Verificado { get; set; }
        public string Usuario { get; set; }
    }

}
