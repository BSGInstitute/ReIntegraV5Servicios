using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
    public class GeneracionDeDataParaConfiguracionPreEnvio
    {
        public class ObtenerGeneracionDeDataParaConfiguracionPreEnvio
        {
            public int IdPlantilla { get; set; }
            public int IdConjuntoListaDetalle { get; set; }
            public string Clave { get; set; }
            public string Valor { get; set; }
            public int IdCampaniaGeneralDetalle { get; set; }
            public int IdCampaniaGeneral { get; set; }
        }
        public class GenerarDTOParaConfiguracionPreEnvio
        {
            public int IdPlantilla { get; set; }
            public int IdConjuntoListaDetalle { get; set; }
            public string Clave { get; set; }
            public string Valor { get; set; }
            public int IdCampaniaGeneralDetalle { get; set; }
            public int IdCampaniaGeneral { get; set; }
            public int idMensajePublicidad { get; set; }
            public int IdPersonal { get; set; }
            public int IdAlumno { get; set; }
            public string Celular { get; set; }
            public int IdWppConfiguracionEnvio { get; set; }
            public int idPais { get; set; }
            public bool EsValido { get; set; }
            public int IdWhatsAppEstadoValidacion { get; set; }
            public int IdPrioridadMailChimpListaCorreo { get; set; }
        }
    }
}
