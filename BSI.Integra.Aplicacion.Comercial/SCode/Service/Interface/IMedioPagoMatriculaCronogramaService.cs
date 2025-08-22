using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IMedioPagoMatriculaCronogramaService
    {
        #region Metodos Base
        MedioPagoMatriculaCronograma Add(MedioPagoMatriculaCronograma entidad);
        MedioPagoMatriculaCronograma Update(MedioPagoMatriculaCronograma entidad);
        bool Delete(int id, string usuario);

        List<MedioPagoMatriculaCronograma> Add(List<MedioPagoMatriculaCronograma> listadoEntidad);
        List<MedioPagoMatriculaCronograma> Update(List<MedioPagoMatriculaCronograma> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MedioPagoMatriculaCronogramaDTO> ObtenerMedioPagoMatriculaCronograma();
         MedioPagoMatriculaCronogramaDTO ObtenerMedioPagoMatriculaCronogramaPorIdMatricula(int idMatriculaCabecera);
         MedioPagoMatriculaCronogramaDTO MedioPagoMatriculaCronogramaPorIdMatricula(int idMatriculaCabecera);
        bool DesactivarMedioPagoMatriculaCronogramaPorMatricula(int idMatriculaCabecera);
        RegistroMedioPagoMatriculaCronogramaDTO RegistroMedioPagoMatriculaCronograma(RegistroMedioPagoMatriculaCronogramaDTO medioPagoMatricula);
    }
}
