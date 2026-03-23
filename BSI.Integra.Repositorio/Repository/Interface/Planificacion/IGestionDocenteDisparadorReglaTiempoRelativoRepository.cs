using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteDisparadorReglaTiempoRelativoRepository : IGenericRepository<TGestionDocenteDisparadorReglaTiempoRelativo>
    {
        TGestionDocenteDisparadorReglaTiempoRelativo Add(GestionDocenteDisparadorReglaTiempoRelativo entidad);
        TGestionDocenteDisparadorReglaTiempoRelativo Update(GestionDocenteDisparadorReglaTiempoRelativo entidad);
    }
}
