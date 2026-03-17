using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteDisparadorOcurrenciaDetalleRepository : IGenericRepository<TGestionDocenteDisparadorOcurrenciaDetalle>
    {
        TGestionDocenteDisparadorOcurrenciaDetalle Add(GestionDocenteDisparadorOcurrenciaDetalle entidad);
        TGestionDocenteDisparadorOcurrenciaDetalle Update(GestionDocenteDisparadorOcurrenciaDetalle entidad);
    }
}
