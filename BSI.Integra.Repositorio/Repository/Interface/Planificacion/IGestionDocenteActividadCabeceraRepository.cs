using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteActividadCabeceraRepository : IGenericRepository<TGestionDocenteActividadCabecera>
    {
        TGestionDocenteActividadCabecera Add(GestionDocenteActividadCabecera entidad);
        TGestionDocenteActividadCabecera Update(GestionDocenteActividadCabecera entidad);
    }
}
