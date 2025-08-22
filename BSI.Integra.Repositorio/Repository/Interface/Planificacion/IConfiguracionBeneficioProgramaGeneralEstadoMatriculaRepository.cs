using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepository : IGenericRepository<TConfiguracionBeneficioProgramaGeneralEstadoMatricula>
    {
        #region Metodos Base
        TConfiguracionBeneficioProgramaGeneralEstadoMatricula Add(ConfiguracionBeneficioProgramaGeneralEstadoMatricula entidad);
        TConfiguracionBeneficioProgramaGeneralEstadoMatricula Update(ConfiguracionBeneficioProgramaGeneralEstadoMatricula entidad);
        bool Delete(int id, string usuario); 
        IEnumerable<TConfiguracionBeneficioProgramaGeneralEstadoMatricula> Add(IEnumerable<ConfiguracionBeneficioProgramaGeneralEstadoMatricula> listadoEntidad);
        IEnumerable<TConfiguracionBeneficioProgramaGeneralEstadoMatricula> Update(IEnumerable<ConfiguracionBeneficioProgramaGeneralEstadoMatricula> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ConfiguracionBeneficioProgramaGeneralEstadoMatricula> ObtenerPorIdConfiguracionBeneficioPgneral(int idConfiguracionBeneficioPgneral);
    }
}
