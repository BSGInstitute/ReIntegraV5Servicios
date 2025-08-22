using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPreCalculadaCambioFaseRepository : IGenericRepository<TPreCalculadaCambioFase>
    {
        #region Metodos Base
        TPreCalculadaCambioFase Add(PreCalculadaCambioFase entidad);
        TPreCalculadaCambioFase AddAsync(PreCalculadaCambioFase entidad);
        TPreCalculadaCambioFase Update(PreCalculadaCambioFase entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPreCalculadaCambioFase> Add(IEnumerable<PreCalculadaCambioFase> listadoEntidad);
        IEnumerable<TPreCalculadaCambioFase> Update(IEnumerable<PreCalculadaCambioFase> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PreCalculadaCambioFaseDTO> ObtenerPreCalculadaCambioFase();
        int ExistePreCalculadaCambioFase(PreCalculadaCambioFase tPre);
    }
}