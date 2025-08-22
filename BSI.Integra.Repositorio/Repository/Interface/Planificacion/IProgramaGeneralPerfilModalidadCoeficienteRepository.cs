using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilModalidadCoeficienteRepository : IGenericRepository<TProgramaGeneralPerfilModalidadCoeficiente>
    {
        #region Metodos Base
        TProgramaGeneralPerfilModalidadCoeficiente Add(ProgramaGeneralPerfilModalidadCoeficiente entidad);
        TProgramaGeneralPerfilModalidadCoeficiente Update(ProgramaGeneralPerfilModalidadCoeficiente entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilModalidadCoeficiente> Add(IEnumerable<ProgramaGeneralPerfilModalidadCoeficiente> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilModalidadCoeficiente> Update(IEnumerable<ProgramaGeneralPerfilModalidadCoeficiente> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilModalidadCoeficiente? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPerfilModalidadCoeficiente> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
