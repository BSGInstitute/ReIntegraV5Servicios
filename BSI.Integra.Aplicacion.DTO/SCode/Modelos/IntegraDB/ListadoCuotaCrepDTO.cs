namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ListadoCuotaCrepDTO
    {
        public int Id { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public string FechaVencimiento { get; set; }
        public string Moneda { get; set; }
        public decimal Cuota { get; set; }
        public decimal Mora { get; set; }
        public decimal Total { get; set; }
        public bool Enviado { get; set; }
        public string FechaAnterior { get; set; }
        public string Adicional { get; set; }
        public bool Cancelado { get; set; }
    }
    public class ListaCrepsDTO
    {
        public List<CrepListaCuotasSeleccionadasDTO> lista { get; set; }
        public CrepCabeceraDTO objeto { get; set; }
        public List<CrepListaAlumnosDTO> listaalumnos { get; set; }
    }
    public class CrepListaCuotasSeleccionadasDTO
    {
        public int Id { get; set; }
        public int nroCuota { get; set; }//ok
        public int nroSubcuota { get; set; }//ok
        public string fechaVencimiento { get; set; }//ok
        public string Moneda { get; set; }//ok
        public decimal Cuota { get; set; }//ok
        public decimal Mora { get; set; }//ok
        public decimal Total { get; set; }//ok
        public bool enviado { get; set; }//ok
        public string fechaAnterior { get; set; }//ok
        public string Adicional { get; set; }//ok
        public int Enviar { get; set; }//ok
        public string CodUsuario { get; set; }
        public string CodigoEspecial { get; set; }
    }
    public class CrepCabeceraDTO
    {
        public string Cuenta { get; set; }
        public string NombreArchivo { get; set; }
        public string Moneda { get; set; }
        public string hidCiudad { get; set; }
        public string hidCuenta { get; set; }
        public string ManualAutomatico { get; set; }
        public string ActualizarEliminar { get; set; }

    }
    public class CrepListaAlumnosDTO
    {
        public string CodigoProgramaEspecifico { get; set; }
        public string NombreProgramaEspecifico { get; set; }
        public string CodigoMatricula { get; set; }
        public int CodigoAlumno { get; set; }
        public string NombreCompletoAlumno { get; set; }
        public DateTime FechaMatricula { get; set; }
        public string Estado { get; set; }
    }
}
