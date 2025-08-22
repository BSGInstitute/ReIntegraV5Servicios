
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class CrucigramaProgramaCapacitacionDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int IdPespecifico { get; set; }
        public int? OrdenFilaCapitulo { get; set; }
        public int? OrdenFilaSesion { get; set; }
        public string CodigoCrucigrama { get; set; }
        public int IdTipoMarcador { get; set; }
        public decimal ValorMarcador { get; set; }
        public int CantidadFila { get; set; }
        public int CantidadColumna { get; set; }
    }
    public class CrucigramaProgramaCapacitacionRespuestaDTO
    {
        public int Id { get; set; }
        public string CodigoCrucigrama { get; set; }
        public int IdPgeneral { get; set; }
        public int IdPespecifico { get; set; }
        public string? PGeneral { get; set; }
        public int IdCapitulo { get; set; }
        public int? IdSesion { get; set; }
        public int IdTipoMarcador { get; set; }
        public decimal ValorMarcador { get; set; }
        public int CantidadFila { get; set; }
        public int CantidadColumna { get; set; }
    }
    public class CrucigramaProgramaCapacitacionCombosDTO
    {
        public IEnumerable<ComboDTO> ListaPgeneral { get; set; }
        public IEnumerable<ComboDTO> ListaTipoMarcador { get; set; }
        public IEnumerable<PEspecificoPGeneralFiltroDTO> ListaPespecifico { get; set; }
    }
    public class ReporteExcelCrucigramasDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int IdPEspecifico { get; set; }
        public int OrdenFilaCapitulo { get; set; }
        public string Sesion { get; set; }
        public string SubSesion { get; set; }
        public int IdTipoMarcador { get; set; }
        public int ValorMarcador { get; set; }
        public string CodigoCrucigrama { get; set; }
        public int CantidadFila { get; set; }
        public int CantidadColumna { get; set; }
        public int ColumnaInicio { get; set; }
        public int FilaInicio { get; set; }
        public int NumeroPalabra { get; set; }
        public int Tipo { get; set; }
        public string Definicion { get; set; }
        public string Palabra { get; set; }
    }
    public class CompuestoCrucigramaProgramaCapacitacionDTO
    {
        public CrucigramaProgramaCapacitacionRespuestaDTO Crucigrama { get; set; }
        public List<CrucigramaProgramaCapacitacionDetalleDTO> CrucigramaDetalle { get; set; }
    }
    public class CrucigramaProgramaCapacitacionDetalleDTO
    {
        public int Id { get; set; }
        public int NumeroPalabra { get; set; }
        public string Palabra { get; set; }
        public string Definicion { get; set; }
        public int Tipo { get; set; }
        public int ColumnaInicio { get; set; }
        public int FilaInicio { get; set; }
    }
    public class ImportacionCrucigramaProgramaCapacitacionDTO
    {
        public string CodigoCrucigrama { get; set; }
        public int IdPGeneral { get; set; }
        public int IdPEspecifico { get; set; }
        public int? OrdenFilaCapitulo { get; set; }
        public string Sesion { get; set; }
        public string SubSeccion { get; set; }
        public int IdTipoMarcador { get; set; }
        public decimal ValorMarcador { get; set; }
        public int CantidadFila { get; set; }
        public int CantidadColumna { get; set; }

        //============CRUCIGRAMA DETALLE=============================
        public int NumeroPalabra { get; set; }
        public string Palabra { get; set; }
        public string Definicion { get; set; }
        public int Tipo { get; set; }
        public int ColumnaInicio { get; set; }
        public int FilaInicio { get; set; }
    }
    public class CrucigramaProgramaCapacitacionExcelCompuestoDTO
    {
        public CrucigramaProgramaCapacitacionAgrupadoDTO CrucigramaProgramaCapacitacion { get; set; }
    }
    public class CrucigramaProgramaCapacitacionAgrupadoDTO
    {
        public string CodigoCrucigrama { get; set; }
        public int IdPGeneral { get; set; }
        public int IdPEspecifico { get; set; }
        public int? OrdenFilaCapitulo { get; set; }
        public string Sesion { get; set; }
        public string SubSeccion { get; set; }
        public int IdTipoMarcador { get; set; }
        public decimal ValorMarcador { get; set; }
        public int CantidadFila { get; set; }
        public int CantidadColumna { get; set; }
        public List<CrucigramaProgramaCapacitacionDetalleDTO> CrucigramaProgramaCapacitacionDetalle { get; set; }
    }
}