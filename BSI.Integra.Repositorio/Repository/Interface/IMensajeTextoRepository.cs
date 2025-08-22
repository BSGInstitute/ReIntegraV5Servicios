using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMensajeTextoRepository : IGenericRepository<TMensajeTexto>
    {
        #region Metodos Base
        TMensajeTexto Add(MensajeTexto entidad);
        TMensajeTexto Update(MensajeTexto entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMensajeTexto> Add(IEnumerable<MensajeTexto> listadoEntidad);
        IEnumerable<TMensajeTexto> Update(IEnumerable<MensajeTexto> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        MatriculaCabeceraCodigoFechaDTO ObtenerCodigoMatriculaPorOportunidad(int idOportunidad);
        AccesoPortalWebDTO ObtenerAccesoPorIdAlumno(int idAlumno);
    }
}
