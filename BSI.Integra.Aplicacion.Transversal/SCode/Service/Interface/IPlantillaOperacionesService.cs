using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPlantillaOperacionesService
    {
        void ValidarNumeroConjuntoLista(ref List<WhatsAppResultadoConjuntoListaDTO> numerosValidados);
        void EnvioAutomaticoPlantilla(List<WhatsAppResultadoConjuntoListaDTO> mensajeAlumno);
        bool Envio(string remitente, string codigoAlumno, string destinatarios, int idPlantilla);
        bool EnvioWhatsappNumeroIndividual(int idOportunidad, int idPlantilla, int flagSeccion);
    }
}
