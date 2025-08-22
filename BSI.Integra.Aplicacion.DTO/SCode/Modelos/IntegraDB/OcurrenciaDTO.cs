namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OcurrenciaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string NombreM { get; set; } = null!;
        public int? NombreCs { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int? IdActividadCabecera { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int IdEstadoOcurrencia { get; set; }
        public bool Oportunidad { get; set; }
        public string RequiereLlamada { get; set; } = null!;
        public string Roles { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class OcurrenciaComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class HojaActividadesDTO
    {
        public int Id { get; set; }
        public string TipoActividad { get; set; }
        public string Actividad { get; set; }
        public string FechaProgramada { get; set; }
        public int IdOcurrencia { get; set; }
        public int OcurrenciaPadre { get; set; }
    }
    public class ArbolOcurrenciaDTO
    {
        public int IdOcurrenciaActividad { get; set; }
        public int IdOcurrenciaReporte { get; set; }
        public string RequiereLlamada { get; set; }
        public int EstadoOcurrencia { get; set; }
        public string NombreOcurrencia { get; set; }
        public string Color { get; set; }
        public string Roles { get; set; }
        public string Nivel { get; set; }
        public bool TieneOcurrencias { get; set; }
        public string TieneActividades { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdOcurrenciaActividad_Padre { get; set; }
        public int IdActividadCabecera { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdPlantilla_Speech { get; set; }
        public string NombreEstadoOcurrencia { get; set; }
        public bool CrearOportunidad { get; set; }
        public string FaseSiguiente { get; set; }
        public int? IdPlantillaWP { get; set; }
        public int? IdPlantillaCE { get; set; }
    }
}
