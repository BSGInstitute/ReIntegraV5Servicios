using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfiguracionPeriodoMatriculaRepository : IGenericRepository<TConfiguracionPeriodoMatricula>
    {
        #region Metodos Base
        TConfiguracionPeriodoMatricula Add(ConfiguracionPeriodoMatricula entidad);
        TConfiguracionPeriodoMatricula Update(ConfiguracionPeriodoMatricula entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConfiguracionPeriodoMatricula> Add(IEnumerable<ConfiguracionPeriodoMatricula> listadoEntidad);
        IEnumerable<TConfiguracionPeriodoMatricula> Update(IEnumerable<ConfiguracionPeriodoMatricula> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        public IEnumerable<ConfiguracionPeriodoMatriculaRecibidoDTO> ObtenerConfiguracionPeriodoMatricula();
    }
}
