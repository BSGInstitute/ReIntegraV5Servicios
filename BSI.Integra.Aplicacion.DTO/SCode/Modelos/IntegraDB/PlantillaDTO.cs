namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PlantillaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int IdPlantillaBase { get; set; }
        public bool EstadoAgenda { get; set; }
        public int Documento { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
    }
    public class PlantillaAsuntoCuerpoDTO
    {
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
    }
    public class PlantillaTipoEnvioDTO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public int? IdPlantillaBase { get; set; }
        public int? IdTipoEnvio { get; set; }
    }
    public class DatosPlantillaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPlantillaBase { get; set; }
        public bool EstadoAgenda { get; set; }
        public bool EstadoPlantilla { get; set; }
        public int Documento { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
    }

    public class PlantillaAsociacionModuloSistemaDTO
    {
        public int Id { get; set; }
        public int IdPlantilla { get; set; }
        public int IdModuloSistema { get; set; }
        public string? NombreModulo { get; set; }
    }

    public class PlantillaDatoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPlantillaBase { get; set; }
        public string NombrePlantillaBase { get; set; }
        public bool EstadoAgenda { get; set; }
        public bool Estado { get; set; }
        public bool? EstadoPlantilla { get; set; }
        public int Documento { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public string NombrePersonalAreaTrabajo { get; set; }
        public List<PlantillaAsociacionModuloSistemaDTO> ListaPlantillaAsociacionModuloSistema { get; set; }


    }

    public class CategoriasPlantillaDTO
    {
        public int Id { get; set; }
    }
    public class FiltroPlantillasDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class FiltroPlantillaTipadaDTO
    {
        public List<FiltroPlantillasDTO> Email { get; set; }
        public List<FiltroPlantillasDTO> Whatsapp { get; set; }
    }


    public class ComboPlantillaNombrePlantillaBaseDTO
    {
        public string IdPlantilla { get; set; }
        public string NombrePlantillaBase { get; set; }
        public string Descripcion { get; set; }
    }
}
