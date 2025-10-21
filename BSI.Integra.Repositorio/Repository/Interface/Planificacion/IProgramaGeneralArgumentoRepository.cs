using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralArgumentoRepository
    {
        #region Metodos Base
        TProgramaGeneralArgumento Add(ProgramaGeneralArgumento entidad);
        TProgramaGeneralArgumento Update(ProgramaGeneralArgumento entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralArgumento> Add(IEnumerable<ProgramaGeneralArgumento> listadoEntidad);
        IEnumerable<TProgramaGeneralArgumento> Update(IEnumerable<ProgramaGeneralArgumento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        bool ObtenerPorId(int id);
    }
}
