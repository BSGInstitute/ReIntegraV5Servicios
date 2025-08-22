namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class BeneficioAlumnoPEspecificoDTO
    {
        public int IdPGeneral { get; set; }
        public int IdPEspecifico { get; set; }
        public int? Paquete { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdAlumno { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdPais { get; set; }
    }
    public class BeneficiosProgramaTipo1DTO
    {
        public int Paquete { get; set; }
        public string Descripcion { get; set; }
        public int OrdenBeneficio { get; set; }
    }
    public class BeneficiosProgramaTipo2DTO
    {
        public string Titulo { get; set; }
    }
}
