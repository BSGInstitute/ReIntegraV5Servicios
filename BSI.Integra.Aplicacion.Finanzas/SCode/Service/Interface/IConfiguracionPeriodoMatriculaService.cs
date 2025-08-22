using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IConfiguracionPeriodoMatriculaService
    {
        #region Metodos Base
        ConfiguracionPeriodoMatricula Add(ConfiguracionPeriodoMatriculaRecibidoDTO data, string Usuario);
        ConfiguracionPeriodoMatricula Update(ConfiguracionPeriodoMatriculaRecibidoDTO data, string Usuario);
        bool Delete(int id, string usuario);

        List<ConfiguracionPeriodoMatricula> Add(List<ConfiguracionPeriodoMatricula> listadoEntidad);
        List<ConfiguracionPeriodoMatricula> Update(List<ConfiguracionPeriodoMatricula> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public IEnumerable<ConfiguracionPeriodoMatriculaRecibidoDTO> ObtenerConfiguracionPeriodoMatricula();
    }
}
