using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICabeceraFurConfiguracionAutomaticaRepository : IGenericRepository<TCabeceraFurConfiguracionAutomatica>
    {
        #region Metodos Base
        TCabeceraFurConfiguracionAutomatica Add(CabeceraFurConfiguracionAutomatica entidad);
        TCabeceraFurConfiguracionAutomatica Update(CabeceraFurConfiguracionAutomatica entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCabeceraFurConfiguracionAutomatica> Add(IEnumerable<CabeceraFurConfiguracionAutomatica> listadoEntidad);
        IEnumerable<TCabeceraFurConfiguracionAutomatica> Update(IEnumerable<CabeceraFurConfiguracionAutomatica> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public CabeceraFurConfiguracionAutomatica ObtenerCabeceraFurConfiguracionAutomaticaById(int Id);
        public IEnumerable<CabeceraFurConfiguracionAutomaticaDTO> ObtenerCabeceraFurConfiguracionAutomatica(FiltroBusquedaCabeceraFCADTO filtro);
        public List<DetalleProyeccionFur> ObtenerCabeceraFurConfiguracionAutomaticaEnRevisionByIdAreas(string IdAreas);

        public bool ValidarCabeceraFurConfiguracionAutomaticaEnProcesoByIdArea(int IdArea);
        public bool CongelarProyeccionYCambiarEstadoProyeccionFur(string data, string configuracion, string Usuario);
        public bool PrepararConfiguracionFurProyeccion(int IdConfiguracion, int IdArea, string Usuario);
    }
}
