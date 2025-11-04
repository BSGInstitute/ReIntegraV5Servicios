using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IOportunidadProgramaMotivacionSeleccionRepository
    {
        #region Metodos Base
        TOportunidadProgramaMotivacionSeleccion Add(OportunidadProgramaMotivacionSeleccion entidad);
        TOportunidadProgramaMotivacionSeleccion Update(OportunidadProgramaMotivacionSeleccion entidad);
        bool Delete(int id, string usuario);

        //IEnumerable<TProgramaGeneralArgumento> Add(IEnumerable<ProgramaGeneralArgumento> listadoEntidad);
        //IEnumerable<TProgramaGeneralArgumento> Update(IEnumerable<ProgramaGeneralArgumento> listadoEntidad);
        //bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<OportunidadProgramaMotivacionSeleccion> ObtenerTodoByIdOportunidad(int idOportunidad);
    }
}
