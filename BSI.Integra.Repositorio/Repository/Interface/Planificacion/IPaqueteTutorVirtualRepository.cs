using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPaqueteTutorVirtualRepository : IGenericRepository<TPaqueteTutorVirtual>
    {
        #region Metodos Base
        TPaqueteTutorVirtual Add(PaqueteTutorVirtual entidad);
        TPaqueteTutorVirtual Update(PaqueteTutorVirtual entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPaqueteTutorVirtual> Add(IEnumerable<PaqueteTutorVirtual> listadoEntidad);
        IEnumerable<TPaqueteTutorVirtual> Update(IEnumerable<PaqueteTutorVirtual> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PaqueteTutorVirtualDTO> Obtener();
        PaqueteTutorVirtual? ObtenerPorId(int id);
    }
}
