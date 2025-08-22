using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPuestoTrabajoFuncionRepository : IGenericRepository<TPuestoTrabajoFuncion>
    {
        #region Metodos Base
        TPuestoTrabajoFuncion Add(PuestoTrabajoFuncion entidad);
        TPuestoTrabajoFuncion Update(PuestoTrabajoFuncion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPuestoTrabajoFuncion> Add(IEnumerable<PuestoTrabajoFuncion> listadoEntidad);
        IEnumerable<TPuestoTrabajoFuncion> Update(IEnumerable<PuestoTrabajoFuncion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PuestoTrabajoFuncionDTO> ObtenerPuestoTrabajoFuncion(int? idPerfilPuestoTrabajo);
    }
}
