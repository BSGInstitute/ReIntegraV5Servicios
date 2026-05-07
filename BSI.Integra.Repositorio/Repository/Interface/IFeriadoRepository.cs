using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFeriadoRepository : IGenericRepository<TFeriado>
    {
        #region Metodos Base
        TFeriado Add(Feriado entidad);
        TFeriado Update(Feriado entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TFeriado> Add(IEnumerable<Feriado> listadoEntidad);
        IEnumerable<TFeriado> Update(IEnumerable<Feriado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<FeriadoDTO> Obtener();
        Feriado? ObtenerPorId(int id);
        IEnumerable<Feriado> ObtenerPorIds(IEnumerable<int> ids);
        IEnumerable<Feriado> ObtenerPorTipo(int tipo);
        IEnumerable<FeriadoConPaisDTO> ObtenerPorPaises(IEnumerable<int> idsTroncalPais);
        IEnumerable<ComboTroncalCiudadDTO> ObtenerComboTroncalCiudad();
        IEnumerable<ComboTroncalPaisDTO> ObtenerComboTroncalPais();
    }
}
