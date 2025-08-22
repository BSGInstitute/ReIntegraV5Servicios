using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPreCalculadaCambioFaseService
    {
        #region Metodos Base
        PreCalculadaCambioFase Add(PreCalculadaCambioFase entidad);
        PreCalculadaCambioFase Update(PreCalculadaCambioFase entidad);
        bool Delete(int id, string usuario);

        List<PreCalculadaCambioFase> Add(List<PreCalculadaCambioFase> listadoEntidad);
        List<PreCalculadaCambioFase> Update(List<PreCalculadaCambioFase> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<PreCalculadaCambioFaseDTO> ObtenerPreCalculadaCambioFase();
        int ExistePreCalculadaCambioFase(PreCalculadaCambioFase tPre);
    }
}
