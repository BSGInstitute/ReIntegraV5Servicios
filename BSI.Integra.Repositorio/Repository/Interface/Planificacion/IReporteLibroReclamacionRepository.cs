using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IReporteLibroReclamacionRepository
    {
        IEnumerable<StringDTO> ObtenerListaNombreReclamo(string nombre);
        IEnumerable<StringDTO> ObtenerListaDniReclamo(string dni);
        List<ReporteLibroReclamacionDTO> GenerarReporteLibroReclamacion(ReporteLibroReclamacionFiltroDTO filtroReporte);
    }
}
