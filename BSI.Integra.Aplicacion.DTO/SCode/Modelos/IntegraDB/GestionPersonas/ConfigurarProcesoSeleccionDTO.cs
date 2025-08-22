using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    public class ConfigurarProcesoSeleccionDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public int? IdPuestoTrabajo { get; set; }
        public string? PuestoTrabajo { get; set; }
        public int? IdSede { get; set; }
        public string? Sede { get; set; }
        public string? Codigo { get; set; }
        public string? Url { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaInicioProceso { get; set; }
        public DateTime? FechaFinProceso { get; set; }

    }
    public class EstructuraBasicaDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public bool? EsCalificadoPorPostulante { get; set; }
    }

    public class ExamenAsignadoProcesoDTO
    {
        public int? Id { get; set; }
        public int? IdProcesoSeleccion { get; set; }
        public int? IdExamen { get; set; }
        public int? NroOrden { get; set; }
        public string? Nombre { get; set; }
    }
    public class EvaluacionAsignadoProcesoDTO
    {
        public int? Id { get; set; }
        public int? IdProcesoSeleccion { get; set; }
        public int? IdEvaluacion { get; set; }
        public int? NroOrden { get; set; }
        public string? Nombre { get; set; }
        public bool? EsCalificadoPorPostulante { get; set; }
        public int? IdProcesoSeleccionEtapa { get; set; }
    }

    public class ProcesoSeleccionAgrupadoInsertarModificarDTO
    {

        public List<ExamenAsignadoEtapaDTO>? listaEtapas { get; set; }
        public ConfigurarProcesoSeleccionDTO? ConfiguracionProcesoSeleccion { get; set; }
        public List<EvaluacionAsignadoProcesoDTO>? listaEvaluaciones { get; set; }
        public List<EvaluacionAsignadoProcesoDTO>? listaEvaluacionesEvaluador { get; set; }
        //public string Usuario { get; set; }
    }
    public class ExamenAsignadoEtapaDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public int? IdProcesoSeleccion { get; set; }
        public int? NroOrden { get; set; }
    }

    public class EtapaProcesoSeleccionDTO
    {
        public int? IdEtapa { get; set; }
        public int? IdProcesoSeleccionEtapa { get; set; }
    }


    public class EliminacionConfiguracionProceso
    {
        public ConfigurarProcesoSeleccionDTO? ProcesoSeleccion { get; set; }
        public string Usuario { get; set; }
    }


    public class EvaluacionesAsociacionDTO
    {
        public List<EstructuraBasicaDTO>? listaEvaluacionNoAsociado { get; set; }
        public List<EvaluacionAsignadoProcesoDTO>? listaEvaluacionAsociado { get; set; }
        public List<EstructuraBasicaDTO>? listaEvaluacionNoAsociadoEvaluador { get; set; }
        public List<EvaluacionAsignadoProcesoDTO>? listaEvaluacionAsociadoEvaluador { get; set; }
        public List<ProcesoSeleccionEtapaDTO>? listaEtapa { get; set; }
    }



    public class EvaluacionPuntajeDTO
    {
        public List<NombreEvaluacionAgrupadaComponenteDTO> listaEvaluacionesPuntajeCalificacion { get; set; }
        public List<NombreEvaluacionesAgrupadaIndependienteDTO> listaEvaluaciones { get; set; }
        public List<NombreEvaluacionAgrupadaComponenteDTO>? listacomponentes { get; set; }
    }

}
