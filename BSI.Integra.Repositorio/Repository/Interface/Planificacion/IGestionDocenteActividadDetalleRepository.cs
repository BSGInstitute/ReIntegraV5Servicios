using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteActividadDetalleRepository : IGenericRepository<TGestionDocenteActividadDetalle>
    {
        TGestionDocenteActividadDetalle Add(GestionDocenteActividadDetalle entidad);
        TGestionDocenteActividadDetalle Update(GestionDocenteActividadDetalle entidad);
        IEnumerable<GestionDocenteSesionDTO> ObtenerSesiones();
        IEnumerable<GestionDocenteConfianzaUmbralNivelDTO> ObtenerConfianzaUmbralNiveles();
        IEnumerable<GestionDocenteOcurrenciaTipoDTO> ObtenerOcurrenciaTipos();
        IEnumerable<GestionDocenteReferenciaTiempoDTO> ObtenerReferenciasTiempo();
    }
}
