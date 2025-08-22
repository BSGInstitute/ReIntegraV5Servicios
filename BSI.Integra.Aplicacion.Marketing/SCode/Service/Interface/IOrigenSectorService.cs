using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IOrigenSectorService
    {
        #region Metodos Base
        OrigenSector Add(OrigenSector entidad);
        OrigenSector Update(OrigenSector entidad);
        bool Delete(int id, string usuario);

        List<OrigenSector> Add(List<OrigenSector> listadoEntidad);
        List<OrigenSector> Update(List<OrigenSector> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        List<OrigenSectorConfiguradoDTO> ObtenerOrigenSector();
        bool EliminarSector(int IdOrigenSector, string UsuarioModificacion);
        bool ActualizarDatosDeConfiguracion(List<ActualizarDatosDeConfiguracionDTO> ListaConfiguracionActualizada);
        bool ActualizarDatosDeConfiguracionAgrupados(ActualizarDatosDeConfiguracionAgrupadoDTO ActualizarConfiguracionDatosAgrupados);



    }
}
