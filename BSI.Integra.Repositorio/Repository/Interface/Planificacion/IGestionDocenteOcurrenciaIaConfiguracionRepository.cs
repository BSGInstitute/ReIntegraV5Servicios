using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteOcurrenciaIaConfiguracionRepository : IGenericRepository<TGestionDocenteOcurrenciaIaConfiguracion>
    {
        TGestionDocenteOcurrenciaIaConfiguracion Add(GestionDocenteOcurrenciaIaConfiguracion entidad);
        TGestionDocenteOcurrenciaIaConfiguracion Update(GestionDocenteOcurrenciaIaConfiguracion entidad);
    }
}
