using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteIaEntrenamientoEjemploRepository : IGenericRepository<TGestionDocenteIaEntrenamientoEjemplo>
    {
        TGestionDocenteIaEntrenamientoEjemplo Add(GestionDocenteIaEntrenamientoEjemplo entidad);
        TGestionDocenteIaEntrenamientoEjemplo Update(GestionDocenteIaEntrenamientoEjemplo entidad);
        IEnumerable<GestionDocenteIaEntrenamientoEjemploOutputDTO> ObtenerEjemplosEntrenamientoPorConfiguracion(int idIaConfiguracion);
        IEnumerable<GestionDocenteIaEntrenamientoClasificacionTipoDTO> ObtenerClasificacionTipos();
    }
}