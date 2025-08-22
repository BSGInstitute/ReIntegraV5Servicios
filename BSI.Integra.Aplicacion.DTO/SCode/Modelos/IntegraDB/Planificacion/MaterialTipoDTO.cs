namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ListaCombosDTO
    {
        public IEnumerable<ComboDTO> MaterialAccion {  get; set; }
        public IEnumerable<ComboDTO> MaterialVersion {  get; set; }
        public IEnumerable<ComboDTO> MaterialCriterio {  get; set; }
    }
    public class MaterialTipoDetalleDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdAccion { get; set; }
        public int? IdMaterialAccion { get; set; }
        public string? NombreMaterialAccion { get; set; }
        public int? IdCriterio { get; set; }
        public int? IdMaterialCriterioVerificacion { get; set; }
        public string? NombreMaterialCriterioVerificacion { get; set; }
        public int? IdVersion { get; set; }
        public int? IdMaterialVersion { get; set; }
        public string? NombreMaterialVersion { get; set; }
    }
    public class MaterialTipoAsociacionEntidadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public List<int>? IdsMaterialAsociacionAccion { get; set; }
        public List<int>? IdsMaterialAsociacionVersion { get; set; }
        public List<int>? IdsMaterialAsociacionCriterioVerificacion { get; set; }
    }
    public class MaterialTipoAgrupadoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<MaterialTipoAccionDTO>? ListaMaterialAccion { get; set; }
        public List<MaterialTipoVersionDTO>? ListaMaterialVersion { get; set; }
        public List<MaterialTipoCriterioVerificacionDTO>? ListaMaterialCriterioVerificacion { get; set; }
    }

    public class MaterialTipoAccionDTO
    {
        public int? IdAccion { get; set; }
        public int? IdMaterialAccion { get; set; }
        public string? NombreMaterialAccion { get; set; }
    }

    public class MaterialTipoVersionDTO
    {
        public int? IdVersion { get; set; }
        public int? IdMaterialVersion { get; set; }
        public string? NombreMaterialVersion { get; set; }
    }

    public class MaterialTipoCriterioVerificacionDTO
    {
        public int? IdCriterio { get; set; }
        public int? IdMaterialCriterioVerificacion { get; set; }
        public string? NombreMaterialCriterioVerificacion { get; set; }
    }
}
