using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFurConfiguracionAutomaticaRepository : IGenericRepository<TFurConfiguracionAutomatica>
    {
        #region Metodos Base
        TFurConfiguracionAutomatica Add(FurConfiguracionAutomatica entidad);
        TFurConfiguracionAutomatica Update(FurConfiguracionAutomatica entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFurConfiguracionAutomatica> Add(IEnumerable<FurConfiguracionAutomatica> listadoEntidad);
        IEnumerable<TFurConfiguracionAutomatica> Update(IEnumerable<FurConfiguracionAutomatica> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        public FurConfiguracionAutomatica ObtenerFurConfiguracionAutomaticaById(int id);
        public List<FurConfiguracionAutomaticaVersionDetalleDTO> ObtenerFurConfiguracionAutomaticaByIdArea(string idArea);
        public List<FurConfiguracionAutomaticaVersionDetalleDTO> ObtenerFurConfiguracionAutomaticaByIdAreaActivo(string idArea);
        public List<FurConfiguracionAutomaticaVersionDetalleDTO> ObtenerFurConfiguracionAutomaticaNoValida(ParametrosEnvioDTO data);
        bool CambiarActivoFurConfiguracionAutomatica(string IdSeleccion, string Usuario);
        public bool DesactivarFurConfiguracionAutomatica(int IdArea, string Usuario);
        bool InsertarFursParaProyeccionCostosFijos(string data);
    }
}
