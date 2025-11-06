using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaMotivacionRepository
    {
        #region Metodos Base
        TProgramaMotivacion Add(ProgramaMotivacion entidad);
        TProgramaMotivacion Update(ProgramaMotivacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaMotivacion> Add(IEnumerable<ProgramaMotivacion> listadoEntidad);
        IEnumerable<TProgramaMotivacion> Update(IEnumerable<ProgramaMotivacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaMotivacion? ObtenerPorId(int id);
        List<ProgramaMotivacion> ObtenerTodo();
        IEnumerable<ComboDTO> Obtener();

    }
}
