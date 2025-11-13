using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{

    public class ProgramaGeneralArgumentoDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool EsVisibleAgenda { get; set; }
        public List<ProgramaGeneralArgumentoModalidadDTO> Modalidades { get; set; } = [];
        public List<ProgramaGeneralArgumentoDetalleDTO> ArgumentoDetalle { get; set; } = [];
    }

    public class ProgramaGeneralArgumentoModalidadDTO
    {
        public int Id { get; set; } //nullable
        public int IdModalidad { get; set; } //ModalidadCurso
        public string Nombre { get; set; }
    }
    public class ProgramaGeneralArgumentoDetalleDTO
    {
        public int Id { get; set; }
        public string Detalle { get; set; }
        public PGArgumentoDetalleMotivacionDTO Motivacion { get; set; }
    }
    public class ProgramaGeneralArgumentoDetalleModelDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralArgumento { get; set; }

        public string Detalle { get; set; }
   
    }

    public class PGArgumentoDetalleMotivacionDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }
    public class ProgramaGeneralArgumentoDetalleMotivacionDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralArgumentoDetalle { get; set; }
        public int IdProgramaGeneralMotivacion { get; set; }
        public string NombreMotivacion { get; set; }
    }
    public class ArgumentoMotivacionProgramaGeneralDTO
    {
        public List<ArgumentoMotivacionEstructuraDTO> GarantiaDePrograma { get; set; }
        public List<ArgumentoMotivacionEstructuraDTO> EstructuraCurricular { get; set; }
        public List<ArgumentoMotivacionEstructuraDTO> DemostracionDeValor { get; set; }
        public List<ArgumentoMotivacionEstructuraDTO> AspectosDiferenciadores { get; set; }
        public List<ArgumentoMotivacionEstructuraDTO> ArgumentosDePerdidaPotencial { get; set; }
    }
    public class ArgumentoMotivacionEstructuraDTO
    {
        public ProgramaGeneralArgumentoDTO Argumento { get; set; }
        public List<ProgramaGeneralArgumentoDetalleDTO> ArgumentoDetalle { get; set; }
        public List<ProgramaGeneralArgumentoModalidadDTO> Modalidades { get; set; }
    }
    public class ProgramaGeneralArgumentoMotivacionDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
       
    }

    public class ProgramaArgumentoMotivacionSeleccionDTO
    {
        public int IdOportunidad { get; set; }
        public int IdPGeneral { get; set; }
        public List<SeleccionMotivacionDTO> SeleccionMotivacion { get; set; } = [];
    }

    public class SeleccionMotivacionDTO
    {
        public int IdMotivacion { get; set; }
        public string descripcionMotivacion { get; set; }
        public bool seleccionado { get; set; }
    }
    public class FactorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class FactorDetalleDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
    }
    public class FactorSolucionDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
    }
    public class SubSolucionDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralProblemaFactorSolucion { get; set; }
        public string Solucion { get; set; }
        public int Orden { get; set; }
        public int Nivel { get; set; }
    }
    public class ProgramaGeneralProblemaDetalleObtener2
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int IdProgramaGeneralProblemaFactor { get; set; }
        public int? IdProgramaGeneralProblemaFactorDetalle { get; set; }
        public int? IdProgramaGeneralProblemaFactorSolucion { get; set; }
        public bool AplicaTituloDetalle { get; set; }
        public bool AplicaNombreDetalle { get; set; }
        public bool AplicaPieDePagina { get; set; }
        public bool AplicaDescripcionSolucion { get; set; }
        public bool AplicaTituloSolucion { get; set; }
        public bool AplicaSubTituloSolucion { get; set; }
        public List<int> SubSolucionIds { get; set; }
    }
    public class ConfiguracionProblemaJerarquicaDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public FactorDTO Factor { get; set; }
        public FactorDetalleDTO Detalle { get; set; }
        public FactorSolucionDTO Solucion { get; set; }
        public List<SubSolucionDTO> SubSoluciones { get; set; }
        public bool AplicaTituloDetalle { get; set; }
        public bool AplicaNombreDetalle { get; set; }
        public bool AplicaPieDePagina { get; set; }
        public bool AplicaDescripcionSolucion { get; set; }
        public bool AplicaTituloSolucion { get; set; }
        public bool AplicaSubTituloSolucion { get; set; }
        public bool EsSeleccionado { get; set; }
    }
    public class RespuestaHistoricaDTO
    {
        public int IdProgramaGeneralProblemaFactorSolucion { get; set; }
        public bool EsSolucionado { get; set; }
    }


/*
**********************************************************
* PASO 1: DTOS DE SALIDA (PARA EL JSON FINAL - TURNO 55/77)
* (Estos DTOs (Salida/Agrupado/Detalle) no cambian
* respecto al Turno 75)
**********************************************************
*/

public class MotivacionSalidaDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; } // (Viene de T_ProgramaMotivacion.Descripcion)
        [JsonProperty("tipo")]
        public string Tipo { get; set; } // Principal, Secundaria, o null
        [JsonProperty("descripcion")]
        public string Descripcion { get; set; } // (Viene de T_ProgramaGeneralMotivacionArgumento.Nombre)

        [JsonProperty("argumentos")]
        public Dictionary<string, List<ArgumentoAgrupadoDTO>> Argumentos { get; set; }
    }

    public class ArgumentoAgrupadoDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        [JsonProperty("detalles")]
        public List<DetalleSalidaDTO> Detalles { get; set; }
    }

    public class DetalleSalidaDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("detalle")]
        public string Detalle { get; set; }
    }


    /*
    **********************************************************
    * PASO 2: DTOS DE REPOSITORIO (CORREGIDOS - TURNO 79)
    * (DTOs internos para el N+1)
    **********************************************************
    */

    // Para Consulta 2 (Motivaciones)
    public class MotivacionRepoDTO
    {
        public int Id { get; set; } // (Viene de T_ProgramaMotivacion.Id)
        public string Nombre { get; set; } // (Viene de T_ProgramaMotivacion.Descripcion)
    }

    // Para Consulta 3 (Descripciones HTML)
    public class DescripcionRepoDTO
    {
        // CORRECCION (Turno 79): Esta es la FK a T_ProgramaMotivacion (Generico)
        // (FIX (Turn 79): This is the FK to T_ProgramaMotivacion (Generic))
        public int IdProgramaMotivacion { get; set; }
        public string Descripcion { get; set; } // (Viene de T_PGM_Argumento.Nombre)
    }

    // Para Consulta 4 (Argumentos)
    public class ArgumentoRepoDTO
    {
        /*
        **********************************************************
        * CORRECCION DEL ERROR (TURNO 72)
        * (ERROR FIX (TURN 72))
        * 'Nombre' debe ser 'string'.
        * (Make sure 'Nombre' is 'string'.)
        **********************************************************
        */
        public int Id { get; set; }
        public string Nombre { get; set; } // <-- Debe ser 'string', no 'int'
    }

    // Para Consulta 5 (Detalles)
    public class DetalleRepoDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralArgumento { get; set; } // FK
        public string Detalle { get; set; }
    }

    // Para Consulta 6 (Links)
    public class DetalleMotivacionLinkRepoDTO
    {
        public int IdProgramaGeneralArgumentoDetalle { get; set; } // FK
        public int IdProgramaMotivacion { get; set; } // FK
    }

    // Para Consulta 1 (Prioridades/Seleccionadas)
    public class PrioridadRepoDTO
    {
        public int IdProgramaMotivacion { get; set; } // FK
        public int Prioridad { get; set; } // 1 o 2
    }


}
