using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IConfiguracionBeneficioProgramaGeneralSubEstadoRepository : IGenericRepository<TConfiguracionBeneficioProgramaGeneralSubEstado>
    {
        #region Metodos Base
        TConfiguracionBeneficioProgramaGeneralSubEstado Add(ConfiguracionBeneficioProgramaGeneralSubEstado entidad);
        TConfiguracionBeneficioProgramaGeneralSubEstado Update(ConfiguracionBeneficioProgramaGeneralSubEstado entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TConfiguracionBeneficioProgramaGeneralSubEstado> Add(IEnumerable<ConfiguracionBeneficioProgramaGeneralSubEstado> listadoEntidad);
        IEnumerable<TConfiguracionBeneficioProgramaGeneralSubEstado> Update(IEnumerable<ConfiguracionBeneficioProgramaGeneralSubEstado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ConfiguracionBeneficioProgramaGeneralSubEstado> ObtenerPorIdConfiguracionBeneficioPgneral(int idConfiguracionBeneficioPgneral);
    }
}
