using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionContactoRepository
    {
        TGestionContacto AddAsync(GestionContacto entidad);
        Task<bool> ExisteCentroCostoAsync(int id);
        Task<bool> ExisteClasificacionPersonaAsync(int id);
        Task<bool> ExisteFaseGestionAsync(int id);
        Task<bool> ExisteGestionActivaAsync(int idDocente, int idCentroCosto);
        Task<bool> ExisteOrigenAsync(int id);
        Task<bool> ExistePersonalAsync(int id);
    }
}
