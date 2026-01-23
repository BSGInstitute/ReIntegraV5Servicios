namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{



    public class InformacionBaseOportunidadMasiva
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Cargo { get; set; }
        public string AreaFormacion { get; set; }
        public string AreaTrabajo { get; set; }
        public string Industria { get; set; }
        public string CentroCosto { get; set; }
        public string Origen { get; set; }
        public string Asesor { get; set; }
        public string TipoDato { get; set; }
        public string FaseOportunidad { get; set; }

    }
    public class HistorialOportunidaMasivodDTO
    {
        public int IdOportunidad { get; set; }
        public string Usuario { get; set; }
    }


    public class OportunidadMasivaDTO
    {
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombrePais { get; set; }
        public string NombreCiudad { get; set; }
        public string? Celular { get; set; }
        public string? Email1 { get; set; }
        public string NombreCargo { get; set; }
        public string NombreFormacion { get; set; }
        public string NombreAreaTrabajo { get; set; }
        public string NombreIndustria { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombrePersonal { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public string CodigoFase { get; set; }

    }





}
