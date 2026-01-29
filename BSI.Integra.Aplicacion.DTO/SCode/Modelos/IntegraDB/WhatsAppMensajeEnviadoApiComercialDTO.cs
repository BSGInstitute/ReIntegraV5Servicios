using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class WhatsAppMensajeEnviadoApiComercialDTO
    {
        public class WhatsAppMensajeTextoComDTO
        {
            public string WaTo { get; set; }
            public string WaBody { get; set; }
            public int IdPais { get; set; }
            public int IdAlumno { get; set; }
            public int? IdPersonal { get; set; }
        }
        public class WhatsAppMensajePlantillaComDTO
        {
            public string WaTo { get; set; }
            public string WaCaption { get; set; }
            public string WaBody { get; set; }
            public int WaTypeMensaje { get; set; }
            public int IdPlantilla { get; set; }
            public int IdPais { get; set; }
            public int IdAlumno { get; set; }
            public int? IdPersonal { get; set; }
            public List<DatosPlantillaWhatsAppDTO> DatosPlantillaWhatsApp { get; set; }
        }
        public class WhatsAppMensajeArchivoComDTO
        {
            public string WaTo { get; set; }
            public string WaType { get; set; }
            public string WaLink { get; set; }
            public string WaFileName { get; set; }
            public int IdPais { get; set; }
            public int IdAlumno { get; set; }
            public int? IdPersonal { get; set; }
        }


        //Asistente Comercial

        public class AsistenteComercialMensajeTextoComDTO
        {
            public int ChatId { get; set; }
            public string EntradaUsuario { get; set; }
            public string NombrePrograma { get; set; }
            public string NombreAsesor { get; set; }
            public int CodigoPais { get; set; }
            public int IdAlumno { get; set; }
            public int IdOportunidad { get; set; }
            public int IdPGeneral { get; set; }
            public int IdCentroCosto { get; set; }
            public DateTime? TiempoActual { get; set; }
            public InformacionClienteAsistente InformacionCliente { get;set;}
            public List<ObjecionesAsistente> Objeciones { get; set; }
            public List<MotivacionesAsistente> Motivaciones { get; set; }
            public List<PreRequisitosAsistente> PreRequisitos { get; set; }
            public List<PublicoObjetivoAsistente> PublicoObjetivo { get; set; }

        }

        public class ResultadoChatAsistente
        {
            public string result { get; set; }
            public int ChatId { get; set; }
        }

        public class PublicoObjetivoAsistente
        {
            public int id { get; set; }
            public string contenido { get; set; }
            public string? respuesta { get; set; }
        }
        public class PreRequisitosAsistente
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public string? respuesta { get; set; }

        }

        public class InformacionClienteAsistente
        {
            public string Nombre { get; set; }
            public string AreaFormacion { get; set; }
            public string Cargo { get; set; }
            public string AreaTrabajo { get; set; }
            public string Industria { get; set; }
            public string AniosExperiencia { get; set; }
            public string Empresa { get; set; }
            public string TamanioEmpresa { get; set; }

        }
        public class ObjecionesAsistente
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public List<DetallesObjecionesAsistente> detalles { get; set; }
        }

        public class DetallesObjecionesAsistente
        {
            public int? id { get; set; }
            public string? nombre { get; set; }
            public List<SolucionesDetallesObjecionesAsistente> soluciones { get; set; }
        }
        public class SolucionesDetallesObjecionesAsistente
        {
            public int id { get; set; }
            public string? titulo { get; set; }
            public string? subtitulo { get; set; }
            public string? descripcion { get; set; }
            public List<DetallesSolucionesDetallesObjecionesAsistente> detalles { get; set; }
        }
        public class DetallesSolucionesDetallesObjecionesAsistente
        {
            public int orden { get; set; }
            public string descripcion { get; set; }
        }

        public class MotivacionesAsistente 
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public string tipo { get; set; }
            public string descripcion { get; set; }
            public ArgumentosMotivacionesAsistente argumentos { get; set; }

        }
        public class ArgumentosMotivacionesAsistente
        {
            public List<ArgumentosSeccionMotivacionesAsistente>? estructuraCurricular { get; set; }
            public List<ArgumentosSeccionMotivacionesAsistente>? demostracionDeValor { get; set; }
            public List<ArgumentosSeccionMotivacionesAsistente>? garantiaDePrograma { get; set; }
            public List<ArgumentosSeccionMotivacionesAsistente>? aspectosDiferenciadores { get; set; }
            public List<ArgumentosSeccionMotivacionesAsistente>? argumentosDePerdidaPotencial { get; set; }
        }

        public class ArgumentosSeccionMotivacionesAsistente
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public List<ArgumentosSeccionDetalleMotivacionesAsistente> detalles { get; set; }
        }
        public class ArgumentosSeccionDetalleMotivacionesAsistente
        {
            public int id { get; set; }
            public string detalle { get; set; }
        }

    }
}
