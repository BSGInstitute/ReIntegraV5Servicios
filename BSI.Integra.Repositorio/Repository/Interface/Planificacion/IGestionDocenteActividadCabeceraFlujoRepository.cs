using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteActividadCabeceraFlujoRepository : IGenericRepository<TGestionDocenteActividadCabeceraFlujo>
    {
        TGestionDocenteActividadCabeceraFlujo Add(GestionDocenteActividadCabeceraFlujo entidad);
        TGestionDocenteActividadCabeceraFlujo Update(GestionDocenteActividadCabeceraFlujo entidad);
        bool Delete(int id, string usuario);
    }
}
