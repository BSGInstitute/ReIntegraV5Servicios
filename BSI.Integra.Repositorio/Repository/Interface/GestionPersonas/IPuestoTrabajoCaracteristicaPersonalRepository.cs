using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
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
    public interface IPuestoTrabajoCaracteristicaPersonalRepository : IGenericRepository<TPuestoTrabajoCaracteristicaPersonal>
    {
        #region Metodos Base
        TPuestoTrabajoCaracteristicaPersonal Add(PuestoTrabajoCaracteristicaPersonal entidad);

        TPuestoTrabajoCaracteristicaPersonal Update(PuestoTrabajoCaracteristicaPersonal entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPuestoTrabajoCaracteristicaPersonal> Add(IEnumerable<PuestoTrabajoCaracteristicaPersonal> listadoEntidad);
        IEnumerable<TPuestoTrabajoCaracteristicaPersonal> Update(IEnumerable<PuestoTrabajoCaracteristicaPersonal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PuestoTrabajoCaracteristicaPersonalDTO> ObtenerPuestoTrabajoCaracteristicaPersonal(int? idPerfilPuestoTrabajo);
    }
}
