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
    public interface IPaqueteTutorVirtualPaisRepository : IGenericRepository<TPaqueteTutorVirtualPai>
    {
        #region Metodos Base
        TPaqueteTutorVirtualPai Add(PaqueteTutorVirtualPais entidad);
        TPaqueteTutorVirtualPai Update(PaqueteTutorVirtualPais entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPaqueteTutorVirtualPai> Add(IEnumerable<PaqueteTutorVirtualPais> listadoEntidad);
        IEnumerable<TPaqueteTutorVirtualPai> Update(IEnumerable<PaqueteTutorVirtualPais> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<PaqueteTutorVirtualPaisDTO> Obtener();
        PaqueteTutorVirtualPais? ObtenerPorId(int id);
        IEnumerable<PaqueteTutorVirtualPaisDTO> ObtenerPorIdPaquete(int idPaqueteTutorVirtual);
    }
}
