using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class InsertarWhatsAppConfiguracionEnvioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPersonal { get; set; }
        public int IdPlantilla { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public DateTime? FechaDesactivacion { get; set; }
        public string Usuario { get; set; }
        public List<FiltroDTO> ProgramaPrincipal { get; set; }
        public List<FiltroDTO> ProgramaSecundario { get; set; }
    }

    public class WhatsAppHabilitadoRecuperacionDTO
    {
        public string Tipo { get; set; }
        public string UsuarioResponsable { get; set; }
        public bool EstadoHabilitado { get; set; }
    }

    public class InsertarSmsConfiguracionEnvioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPersonal { get; set; }
        public int IdPlantilla { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public DateTime? FechaDesactivacion { get; set; }
        public string Usuario { get; set; }
        public int IdPGeneral { get; set; }
    }
}
