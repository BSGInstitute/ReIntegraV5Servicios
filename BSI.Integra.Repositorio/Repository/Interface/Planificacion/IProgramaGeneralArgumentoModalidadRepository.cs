using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralArgumentoModalidadRepository
    {
        #region Metodos Base
        TProgramaGeneralArgumentoModalidad Add(ProgramaGeneralArgumentoModalidad entidad);
        TProgramaGeneralArgumentoModalidad Update(ProgramaGeneralArgumentoModalidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralArgumentoModalidad> Add(IEnumerable<ProgramaGeneralArgumentoModalidad> listadoEntidad);
        IEnumerable<TProgramaGeneralArgumentoModalidad> Update(IEnumerable<ProgramaGeneralArgumentoModalidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
