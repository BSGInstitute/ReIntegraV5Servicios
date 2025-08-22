using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilCiudadCoeficienteRepository : IGenericRepository<TProgramaGeneralPerfilCiudadCoeficiente>
    {
        #region Metodos Base
        TProgramaGeneralPerfilCiudadCoeficiente Add(ProgramaGeneralPerfilCiudadCoeficiente entidad);
        TProgramaGeneralPerfilCiudadCoeficiente Update(ProgramaGeneralPerfilCiudadCoeficiente entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilCiudadCoeficiente> Add(IEnumerable<ProgramaGeneralPerfilCiudadCoeficiente> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilCiudadCoeficiente> Update(IEnumerable<ProgramaGeneralPerfilCiudadCoeficiente> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilCiudadCoeficiente? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPerfilCiudadCoeficiente> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
