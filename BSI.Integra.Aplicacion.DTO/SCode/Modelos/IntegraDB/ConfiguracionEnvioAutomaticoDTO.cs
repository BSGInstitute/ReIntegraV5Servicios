using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConfiguracionEnvioAutomaticoDTO
    {
        public int Id { get; set; }
        public int? IdEstadoInicial { get; set; }
        public int? IdEstadoDestino { get; set; }
        public int? IdSubEstadoInicial { get; set; }
        public int? IdSubEstadoDestino { get; set; }
        public string Usuario { get; set; }
        public bool? AplicaWhatsApp { get; set; }
        public bool? AplicaSMS { get; set; }
        public bool? AplicaCorreo { get; set; }
        public List<ConfiguracionEnvioAutomaticoDetalleDTO> ListaConfiguracionEnvioAutomatico { get; set; }
    }
    public class ConfiguracionEnvioAutomaticoDetalleDTO
    {
        public TimeSpan? HoraEnvioAutomatico { get; set; }
        public int? IdConfiguracionEnvioAutomatico { get; set; }
        public int Id { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdTiempoFrecuencia { get; set; }
        public int? IdTipoEnvioAutomatico { get; set; }
        public int? Valor { get; set; }
    }
    public class ObtenerConfiguracionEnvioDTO
    {
        public int Id { get; set; }
        public int? IdEstadoInicial { get; set; }
        public int? IdEstadoDestino { get; set; }
        public int? IdSubEstadoInicial { get; set; }
        public int? IdSubEstadoDestino { get; set; }
        public bool Estado { get; set; }
        public bool EnvioWhatsApp { get; set; }
        public bool EnvioCorreo { get; set; }
        public bool EnvioMensajeTexto { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ConfiguracionEnvioDTO
    {
        public int Id { get; set; }
        public int? IdEstadoInicial { get; set; }
        public int? IdEstadoDestino { get; set; }
        public int? IdSubEstadoInicial { get; set; }
        public int? IdSubEstadoDestino { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool? AplicaWhatsApp { get; set; }
        public bool? AplicaSMS { get; set; }
        public bool? AplicaCorreo { get; set; }
    }
}
