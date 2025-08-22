using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IGestionRemuneracionPuestoTrabajoDetalleRepository : IGenericRepository<TPuestoTrabajoRemuneracionDetalle>
    {
        #region Metodos Base
        bool Delete(int id, string usuario);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
