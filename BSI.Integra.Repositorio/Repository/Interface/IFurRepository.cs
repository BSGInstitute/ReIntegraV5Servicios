using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFurRepository : IGenericRepository<TFur>
    {
        #region Metodos Base
        TFur Add(Fur entidad);
        TFur Update(Fur entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFur> Add(IEnumerable<Fur> listadoEntidad);
        IEnumerable<TFur> Update(IEnumerable<Fur> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        Fur? ObtenerPorId(int id);
        object ObtenerDatosFur();
        object ObtenerFursREC();

        public object ObtenerDatosFurAutocomplete(string codigo);
        IEnumerable<FurDTO> ObtenerFursParaGrid(ParametrosFurGrillaDTO json);
        IEnumerable<FurDTO> ObtenerFursBusquedaCodigo(string codigo);
        IEnumerable<ProductoFurDTO> ObtenerProductoFur(int IdProveedor);
        IEnumerable<FurPorAprobarDTO> ObtenerFurPorAprobar(int IdArea, string Codigo, int IdRol, int tipo);
        IEnumerable<FurAprobadoNoEjecutadoDTO> ObtenerFursNoEjecutados();
        List<FurCajaPRDTO> ObtenerDatosFurcajaEgreso(string codigo, int IdCajaPorRendirCabecera);
        object ObtenerDatosFurSolicitudEfectivo(string codigo);
        public bool EliminarLogicamenteFurProyectadoPorHistorico(EliminarFurProyectadoDTO data);
        public int ObtenerNivelAcceso(string usuario, int idPer);
        IEnumerable<ProgramaEspecificoFURDTO> ObtenerFurProgramaEspecifico(int idPespecifico, bool esDocente);
        int ObtenerNumeroSemana(DateTime fecha);
    }
}
