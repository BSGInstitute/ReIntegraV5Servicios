using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IFurService
    {
        #region Metodos Base
        Fur Add(Fur entidad);
        Fur Update(Fur entidad);
        bool Delete(int id, string usuario);

        List<Fur> Add(List<Fur> listadoEntidad);
        List<Fur> Update(List<Fur> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        object ObtenerDatosFur();

        IEnumerable<FurDTO> ObtenerFursParaGrid(ParametrosFurDTO json);

        IEnumerable<FurDTO> ObtenerFursBusquedaCodigo(string codigo);
        IEnumerable<ProductoFurDTO> ObtenerProductoFur(int IdProveedor);
        Fur ActualizarFur(FurDTO Json);
        Fur InsertarFur(FurDTO Json);
        IEnumerable<FurPorAprobarDTO> ObtenerFurPorAprobar(int IdArea, string Codigo, int IdRol, int tipo);
        List<FurCajaPRDTO> ObtenerDatosFurcajaEgreso(string codigo, int IdCajaPorRendirCabecera);
        object ObtenerDatosFurSolicitudEfectivo(string codigo);
        public bool AprobarFurProyectado(FurAprobarPoryectadosDTO json);
        public int ObtenerNivelAcceso(string usuario, int idPer);
        IEnumerable<ProgramaEspecificoFURDTO> ObtenerFurProgramaEspecifico(int idPespecifico, bool esDocente);
    }
}
