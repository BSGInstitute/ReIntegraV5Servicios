using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteDisparadorReglaTiempoFijoRepository : IGenericRepository<TGestionDocenteDisparadorReglaTiempoFijo>
    {
        TGestionDocenteDisparadorReglaTiempoFijo Add(GestionDocenteDisparadorReglaTiempoFijo entidad);
        TGestionDocenteDisparadorReglaTiempoFijo Update(GestionDocenteDisparadorReglaTiempoFijo entidad);
        int ObtenerIdReglaTiempoPorTipo(string tipoRegla);
    }
}
