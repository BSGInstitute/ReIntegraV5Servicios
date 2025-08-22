using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface
{
    public interface IDatoContratoPersonalService
    {
        Object ObtenerCombos();
        Object ObtenerComboContrato();
        List<DatoContratoPersonalFiltroDTO> ObtenerContratosPorFiltro(ContratoFiltroDTO filtro);
        DatosFormularioPersonalDTO ObtenerDataFormulario(int IdPersonal);
        List<ContratoHistoricoRegistroRDTO> ObtenerContratosHistoricos(int IdPersonal);
        Object ObtenerRemuneracionVariableDisplay(int IdPuestoTrabajo);
        Boolean InsertarContrato(DatoContratoPersonalDTO ContratoPersonal);
        byte[] GenerarPDFDatoContratoPersonalV2(string datosControPersonal);
        List<PersonalAutocompleteDTO> CargarPersonalAutoCompleteContrato(string nombre);
        List<DatosRemuneracionVariableDTO> ObtenerComboDatosRemuneracionVariable();
    }
}
