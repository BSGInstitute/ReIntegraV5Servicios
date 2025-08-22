using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
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
    public interface IProgramaGeneralPresentacionArgumentoModalidadRepository: IGenericRepository<TProgramaGeneralPresentacionArgumentoModalidad>
    {
        #region Metodos Base
        TProgramaGeneralPresentacionArgumentoModalidad Add(ProgramaGeneralPresentacionArgumentoModalidad entidad);
        TProgramaGeneralPresentacionArgumentoModalidad Update(ProgramaGeneralPresentacionArgumentoModalidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPresentacionArgumentoModalidad> Add(IEnumerable<ProgramaGeneralPresentacionArgumentoModalidad> listadoEntidad);
        IEnumerable<TProgramaGeneralPresentacionArgumentoModalidad> Update(IEnumerable<ProgramaGeneralPresentacionArgumentoModalidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralPresentacionArgumentoModalidadDTO> ObtenerProgramaGeneralPresentacionArgumentoModalidad();
        IEnumerable<ProgramaGeneralPresentacionArgumentoModalidadDTO> ObtenerModalidadPorIdPresentacionArgumento(int id);
    }
}
