using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
    public class ConfiguracionDeEnvioParaWhatsAppDTO : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int? IdPlantilla { get; set; }
        public DateTime FechaDeEnvio { get; set; }
        public DateTime FechaFinDeEnvio { get; set; }
        public int? HoraDeEnvio { get; set; }
        public int TiempoEntreEnvios { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public string Nombre { get; set; } = null!;

    }
    public class ConfiguracionDeEnvioParaWhatsAppMasPlantilla {
        public int Id { get; set; }
        public int? IdPlantilla { get; set; }
        public string NombrePlantilla { get; set; }
        public DateTime FechaDeEnvio { get; set; }
        public DateTime FechaFinDeEnvio { get; set; }
        public int? HoraDeEnvio { get; set; }
        public int TiempoEntreEnvios { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public string Nombre { get; set; } = null!;
        public String UsuarioCreacion { get; set; }
        public String UsuarioModificacion { get; set; }
    }
    public class ConfiguracionDeEnvioParaWhatsAppMasCampaniaGeneralYPlantilla
    {
        public int Id { get; set; }
        public int? IdPlantilla { get; set; }
        public string NombrePlantilla { get; set; }
        public DateTime FechaDeEnvio { get; set; }
        public DateTime FechaFinDeEnvio { get; set; }
        public int? HoraDeEnvio { get; set; }
        public int TiempoEntreEnvios { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public int IdCampaniaGeneral { get; set; }
        public string Nombre { get; set; } = null!;
        public String UsuarioCreacion { get; set; }
        public String UsuarioModificacion { get; set; }
        public String CampaniaGeneralNombre { get; set; }
    }

    public class ConfiguracionDeEnvioParaWhatsAppCapnaiGeneral
    {
        public int Id { get; set; }
    public int? IdPlantilla { get; set; }
    public string FechaDeEnvio { get; set; }
    public string FechaFinDeEnvio { get; set; }
    public int? HoraDeEnvio { get; set; }
    public int TiempoEntreEnvios { get; set; }
    public int IdCampaniaGeneralDetalle { get; set; }
    public string Nombre { get; set; } = null!;
    public int IdCampaniaGeneral { get; set; }
        public string CampaniaGeneralNombre { get; set; }
}
    public class ConfiguracionDeEnvioParaWhatsAppCreate
    {
        public int Id { get; set; }
        public int? IdPlantilla { get; set; }
        public string FechaDeEnvio { get; set; }
        public string FechaFinDeEnvio { get; set; }
        public int? HoraDeEnvio { get; set; }
        public int TiempoEntreEnvios { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
