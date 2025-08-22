using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPuestoTrabajoFormacionAcademicaRepository : IGenericRepository<TPuestoTrabajoFormacionAcademica>
    {
        #region Metodos Base
        TPuestoTrabajoFormacionAcademica Add(PuestoTrabajoFormacionAcademica entidad);

        TPuestoTrabajoFormacionAcademica Update(PuestoTrabajoFormacionAcademica entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPuestoTrabajoFormacionAcademica> Add(IEnumerable<PuestoTrabajoFormacionAcademica> listadoEntidad);
        IEnumerable<TPuestoTrabajoFormacionAcademica> Update(IEnumerable<PuestoTrabajoFormacionAcademica> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PuestoTrabajoFormacionAcademicaFiltroDTO> ObtenerPuestoTrabajoFormacionAcademica(int? idPerfilPuestoTrabajo);
    }
}
