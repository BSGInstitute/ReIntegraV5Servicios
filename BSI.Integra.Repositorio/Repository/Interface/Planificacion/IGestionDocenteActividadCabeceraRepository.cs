using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteActividadCabeceraRepository : IGenericRepository<TGestionDocenteActividadCabecera>
    {
        TGestionDocenteActividadCabecera Add(GestionDocenteActividadCabecera entidad);
        TGestionDocenteActividadCabecera Update(GestionDocenteActividadCabecera entidad);
        GestionDocenteActividadCabeceraOutputDTO ObtenerCabeceraPorId(int id);
        Task<GestionDocenteActividadCabeceraOutputDTO> ObtenerCabeceraPorIdAsync(int id);
    }
}
