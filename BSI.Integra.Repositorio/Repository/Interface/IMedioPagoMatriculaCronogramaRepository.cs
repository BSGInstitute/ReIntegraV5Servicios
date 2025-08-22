using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMedioPagoMatriculaCronogramaRepository : IGenericRepository<TMedioPagoMatriculaCronograma>
    {
        #region Metodos Base
        TMedioPagoMatriculaCronograma Add(MedioPagoMatriculaCronograma entidad);
        TMedioPagoMatriculaCronograma Update(MedioPagoMatriculaCronograma entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMedioPagoMatriculaCronograma> Add(IEnumerable<MedioPagoMatriculaCronograma> listadoEntidad);
        IEnumerable<TMedioPagoMatriculaCronograma> Update(IEnumerable<MedioPagoMatriculaCronograma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MedioPagoMatriculaCronogramaDTO> ObtenerMedioPagoMatriculaCronograma();
         MedioPagoMatriculaCronogramaDTO ObtenerMedioPagoMatriculaCronogramaPorIdMatricula(int idMatriculaCabecera);
         MedioPagoMatriculaCronogramaDTO MedioPagoMatriculaCronogramaPorIdMatricula(int idMatriculaCabecera);
        bool DesactivarMedioPagoMatriculaCronogramaPorMatricula(int idMatriculaCabecera);
        RegistroMedioPagoMatriculaCronogramaDTO RegistroMedioPagoMatriculaCronograma(RegistroMedioPagoMatriculaCronogramaDTO medioPagoMatricula);
    }
}