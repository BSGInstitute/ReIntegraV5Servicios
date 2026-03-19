using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IReemplazoEtiquetaPlantillaService
    {
        (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado) ReemplazarEtiquetasComercial(ReemplazoEtiquetaPlantillaDTO reemplazoEtiqueta);
        (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado) ReemplazarEtiquetas(ReemplazoEtiquetaPlantillaDTO reemplazoEtiqueta);
        (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado, PlantillaSmsCalculadoDTO SmsReemplazado) ReemplazarEtiquetasNuevasOportunidades(ReemplazoEtiquetaPlantillaDTO reemplazoEtiqueta, bool personalPorDefecto = false, int idCentroCosto = 0);
        (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado) ReemplazarEtiquetasEnvioCorreoSolicitudEnvioFisico(DatosRegistroEnvioFisicoDTO datosAlumno, int idPlantilla);
        (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado) ReemplazarEtiquetasSinOportunidad(int idPlantilla, int idOportunidad, int? idPersonal, int idCentroCosto);
        (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado) ReemplazarEtiquetasEnvioCorreoAsignacionForoDocente(ForoCorreoDetalleDTO datosAlumno, int idPlantilla);
        (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado) ReemplazarEtiquetasProveedor(ReemplazoEtiquetaPlantillaDTO etiqueta);
        PlantillaWhatsAppCalculadoDTO ReemplazarValoresTilde(PlantillaWhatsAppCalculadoDTO body);
        PlantillaEmailMandrillDTO ReemplazarEtiquetasPostulanteCursoAsesorCapacitacion(int idPlantilla, int idPostulanteProcesoSeleccion);
        PlantillaEmailMandrillDTO ReemplazarEtiquetasAccesosTemporalesPostulante(int idPlantilla, InformacionAccesoPostulanteDTO informacionAccesoTemporal, int idPespecifico, DateTime fechaInicio, DateTime fechaFin, string personalEmail);
        string ReemplazarEtiquetasPlanificacion(string contenido, int idActividadDetalle, int idCentroCosto);
    }
}
