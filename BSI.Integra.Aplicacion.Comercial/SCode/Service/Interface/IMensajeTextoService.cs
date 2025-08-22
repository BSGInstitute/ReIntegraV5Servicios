using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IMensajeTextoService
    {
        #region Metodos Base
        MensajeTexto Add(MensajeTexto entidad);
        MensajeTexto Update(MensajeTexto entidad);
        bool Delete(int id, string usuario);
        List<MensajeTexto> Add(List<MensajeTexto> listadoEntidad);
        List<MensajeTexto> Update(List<MensajeTexto> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        MatriculaCabeceraCodigoFechaDTO CodigoMatriculaPorOportunidad(int idOportunidad);
        AccesoPortalWebDTO AccesoPorIdAlumno(int idAlumno);
    }
}
