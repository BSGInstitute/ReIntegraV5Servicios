using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteDisparadorReglaTiempoRelativoReferenciaRepository : IGenericRepository<TGestionDocenteDisparadorReglaTiempoRelativoReferencium>
    {
        TGestionDocenteDisparadorReglaTiempoRelativoReferencium Add(GestionDocenteDisparadorReglaTiempoRelativoReferencia entidad);
        TGestionDocenteDisparadorReglaTiempoRelativoReferencium Update(GestionDocenteDisparadorReglaTiempoRelativoReferencia entidad);
    }
}
