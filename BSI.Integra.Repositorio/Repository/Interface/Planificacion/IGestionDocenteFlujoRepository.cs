using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteFlujoRepository : IGenericRepository<TGestionDocenteFlujo>
    {
        TGestionDocenteFlujo Add(GestionDocenteFlujo entidad);
        TGestionDocenteFlujo Update(GestionDocenteFlujo entidad);
        IEnumerable<GestionDocenteEstadoDTO> ObtenerEstadosFlujo();
        IEnumerable<GestionDocenteCategoriaDTO> ObtenerCategorias();
        IEnumerable<GestionDocenteActividadCabeceraListaDTO> ObtenerActividadesCabecera(int? idCategoria = null);
        GestionDocenteFlujoOutputDTO ObtenerFlujoPorId(int id);
    }
}
