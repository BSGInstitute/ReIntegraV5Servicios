using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOrigenSectorRepository : IGenericRepository<TOrigenSector>
    {
        #region Metodos Base
        TOrigenSector Add(OrigenSector entidad);
        TOrigenSector Update(OrigenSector entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOrigenSector> Add(IEnumerable<OrigenSector> listadoEntidad);
        IEnumerable<TOrigenSector> Update(IEnumerable<OrigenSector> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<OrigenSectorDTO> ObtenerOrigenConfigurado();
        List<OrigenSectorDTO> ObtenerOrigenNoConfigurado();
        List<OrigenSectorConfiguradoDTO> ObtenerOrigenSectorConfigurado();
        bool EliminarOrigenSector(int IdOrigenSector, string UsuarioModificacion);
        bool ActualizarDatosDeConfiguracion(List<ActualizarDatosDeConfiguracionDTO> ListaConfiguracionActualizada);
        List<ListaIdCategoriaOrigenDTO> ObtenerOrigenDatoCalidadDetalle(int? idOrigensector);
        bool EliminarOportunidadConfiguracion();
        bool? InsertarOrigenSector(string Nombre, string Descripcion, int Orden, String UsuarioCreacion);

    }
}
