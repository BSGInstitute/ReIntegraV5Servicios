namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ValidacionPaisConfiguracionAsignacionRegularDTO
    {
        public int Id { get; set; }

    }
    public class InsertarAsignacionRegularDTO
    {
        public List<int> Id { get; set; }

    }
    public class ListaCategoriaOrigenDTO
    {
        public List<int> Id { get; set; }

    }
    public class InsertarProgramaGeneralAsignacionRegularDTO
    {
        public List<int> idProgramaGeneral { get; set; }

    }

    public class ConfiguracionPrincipalDTO
    {

        public int Id { get; set; }
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public string Codigo { get; set; }
        public int Prioridad { get; set; }
        public string Coordinador { get; set; }
        public string Asesor { get; set; }
        public bool DatoCalidad { get; set; }
        public bool AplicaProporcionPorPais { get; set; }
        public bool EsLimiteCola { get; set; }
        public int LimiteCola { get; set; }
        public int PorcentajeTolerancia { get; set; }

        public int IdPeru { get; set; }
        public bool EsProporcionManualPeru { get; set; }
        public int ProporcionManualPeru { get; set; }
        public int ProporcionPorPaisPeru { get; set; }


        public int IdColombia { get; set; }
        public bool EsProporcionManualColombia { get; set; }
        public int ProporcionManualColombia { get; set; }
        public int ProporcionPorPaisColombia { get; set; }



        public int IdBolivia { get; set; }
        public bool EsProporcionManualBolivia { get; set; }
        public int ProporcionManualBolivia { get; set; }
        public int ProporcionPorPaisBolivia { get; set; }



        public int IdMexico { get; set; }
        public bool EsProporcionManualMexico { get; set; }
        public int ProporcionManualMexico { get; set; }
        public int ProporcionPorPaisMexico { get; set; }

        public int IdChile { get; set; }
        public bool EsProporcionManualChile { get; set; }
        public int ProporcionManualChile { get; set; }
        public int ProporcionPorPaisChile { get; set; }

        public int IdInternacional { get; set; }
        public bool EsProporcionManualInternacional { get; set; }
        public int ProporcionManualInternacional { get; set; }
        public int ProporcionPorPaisInternacional { get; set; }




    }
    public class RecibirConfiguracionPrincipalDTO
    {
        public int Id { get; set; }
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public string Codigo { get; set; }
        public int Prioridad { get; set; }
        public string Coordinador { get; set; }
        public string Asesor { get; set; }
        public bool DatoCalidad { get; set; }
        public bool AplicaProporcionPorPais { get; set; }
        public bool EsLimiteCola { get; set; }
        public int LimiteCola { get; set; }
        public int PorcentajeTolerancia { get; set; }
    }

    public class RecibirConfiguracionPrincipalActualizadoDTO
    {
        public int Id { get; set; }
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public string Codigo { get; set; }
        public int Prioridad { get; set; }
        public string Coordinador { get; set; }
        public string Asesor { get; set; }
        public bool DatoCalidad { get; set; }
        public bool AplicaProporcionPorPais { get; set; }
        public bool EsLimiteCola { get; set; }
        public int LimiteCola { get; set; }
        public int PorcentajeTolerancia { get; set; }
    }

    public class RecibirConfiguracionPrincipalPorPaisDTO
    {
        public int Id { get; set; }
        public bool EsProporcionManual { get; set; }
        public int ProporcionManual { get; set; }
        public int ProporcionPorPais { get; set; }
    }

    public class ObtenerBloquePorProgramaCriticoDTO
    {
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public String Nombre { get; set; }
        public int CantidadConfiguraciones { get; set; }

    }

    public class ObtenerListaAsesorDTO
    {
        public int Id { get; set; } 
        public String Coordinador { get; set; }
        public String Asesor { get; set; }
        public int? Prioridad { get; set; }
        public String EstadoAsesor { get; set; }
        public int OportunidadesAbiertas { get; set; }
        public int TopeOportunidad { get; set; }
        public int OportunidadesAbiertasHoy { get; set; }
        public int TopeAsignacionDiaria { get; set; }
        public bool ActivarAsignacionAutomatica { get; set; }
    }

    public class ObtenerAsesorConfiguracionPorPaisDTO
    {

        public int Id { get; set; }
        public int IdAsignacionRegular { get; set; }
        public string Codigo { get; set; }
        public int CantidadTotal { get; set; }
        public bool ActivarAsignacionPaisConfiguracion { get; set; }

        public bool DatoCalidadPeru { get; set; }
        public bool DatoCalidadWhatsappPeru { get; set; }
        public bool DatoCalidadMailingPeru{ get; set; }
        public int DistribucionPeru { get; set; }
        public int CantidadTotalPeru { get; set; }


        public bool DatoCalidadMexico { get; set; }
        public bool DatoCalidadWhatsappMexico { get; set; }
        public bool DatoCalidadMailingMexico { get; set; }
        public int DistribucionMexico { get; set; }
        public int CantidadTotalMexico { get; set; }

        public bool DatoCalidadColombia { get; set; }
        public bool DatoCalidadWhatsappColombia { get; set; }
        public bool DatoCalidadMailingColombia { get; set; }
        public int DistribucionColombia { get; set; }
        public int CantidadTotalColombia { get; set; }



        public bool DatoCalidadBolivia { get; set; }
        public bool DatoCalidadWhatsappBolivia { get; set; }
        public bool DatoCalidadMailingBolivia { get; set; }
        public int DistribucionBolivia { get; set; }
        public int CantidadTotalBolivia { get; set; }



        public bool DatoCalidadChile { get; set; }
        public bool DatoCalidadWhatsappChile { get; set; }
        public bool DatoCalidadMailingChile { get; set; }
        public int DistribucionChile { get; set; }
        public int CantidadTotalChile { get; set; }


        public bool DatoCalidadInternacional { get; set; }
        public bool DatoCalidadWhatsappInternacional { get; set; }
        public bool DatoCalidadMailingInternacional { get; set; }
        public int DistribucionInternacional { get; set; }
        public int CantidadTotalInternacional { get; set; }



    }

    public class ComboAsesoresDTO
    {
        public int Id { get; set; }
        public String Asesor { get; set; }
    }

    public class ListaCategoriaOrigenNoConfigurada
    {
        public int Id { get; set; }
        public String nombre { get; set; }
    }
    public class CategoriaOrigenPorSectorDTO
    {
        public int Id { get; set; }
        public int IdOrigenSector { get; set; }
        public String nombre { get; set; }
        public bool AgruparCategoriaOrigen { get; set; }
    }
    public class ObtenerConfiguracionProgramasOtrasAreasDTO
    {
        public int IdProgramaOtraArea { get; set; }

        public int IdGrupoFiltroProgramaCritico { get; set; }
        public int IdAsignacionRegular { get; set; }
        public String? PGventa { get; set; }

        public String Coordinador { get; set; }
        public String Asesor { get; set; }

        public int? IdProgramaGeneral { get; set; }
        public String? Codigo { get; set; }

        public bool BaseHistorica { get; set; }
        public bool DatoCalidad { get; set; }

        public bool EsLimitePeru { get; set; }
        public int LimitePeru { get; set; }
        public bool EsLimiteColombia { get; set; }
        public int LimiteColombia { get; set; }
        public bool EsLimiteMexico { get; set; }
        public int LimiteMexico { get; set; }
        public bool EsLimiteBolivia { get; set; }
        public int LimiteBolivia { get; set; }
        public bool EsLimiteInternacional { get; set; }
        public int LimiteInternacional { get; set; }

    }

    public class ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO
    {
        public int IdProgramaOtraArea { get; set; }

        public int IdGrupoFiltroProgramaCritico { get; set; }
        public int IdAsignacionRegular { get; set; }
        public String PGventa { get; set; }

        public String Coordinador { get; set; }
        public String Asesor { get; set; }

        public List<ListaProgramasGeneralesDTO>? ListaProgramasGenerales { get; set; }

        public bool BaseHistorica { get; set; }
        public bool DatoCalidad { get; set; }
        public bool EsLimitePeru { get; set; }
        public int LimitePeru { get; set; }
        public bool EsLimiteColombia { get; set; }
        public int LimiteColombia { get; set; }
        public bool EsLimiteMexico { get; set; }
        public int LimiteMexico { get; set; }
        public bool EsLimiteBolivia { get; set; }
        public int LimiteBolivia { get; set; }
        public bool EsLimiteInternacional { get; set; }
        public int LimiteInternacional { get; set; }

    }

    public class ObtenerConfiguracionPrincipalPorDTO
    {
        public int IdProgramaOtraArea { get; set; }

        public int IdGrupoFiltroProgramaCritico { get; set; }
        public int IdAsignacionRegular { get; set; }
        public String PGventa { get; set; }

        public String Coordinador { get; set; }
        public String Asesor { get; set; }

        public List<ListaProgramasGeneralesDTO>? ListaProgramasGenerales { get; set; }

        public bool BaseHistorica { get; set; }
        public bool DatoCalidad { get; set; }
        public bool EsLimitePeru { get; set; }
        public int LimitePeru { get; set; }
        public bool EsLimiteColombia { get; set; }
        public int LimiteColombia { get; set; }
        public bool EsLimiteMexico { get; set; }
        public int LimiteMexico { get; set; }
        public bool EsLimiteBolivia { get; set; }
        public int LimiteBolivia { get; set; }
        public bool EsLimiteInternacional { get; set; }
        public int LimiteInternacional { get; set; }

    }
    public class ListaProgramasGeneralesDTO
    {
        public int? IdProgramaGeneral { get; set; }
        public String? Codigo { get; set; }
    }

    public class IdProgramaGeneralDTO
    {
        public int? IdProgramaGeneral { get; set; }
    }
    public class ComboProgramaCriticoDTO
    {
        public int? IdGrupoFiltroProgramaCritico { get; set; }
        public string? Nombre { get; set; }

    }
    public class ComboProgramaGeneralDTO
    {
        public int? IdPGeneral { get; set; }
        public string? Codigo { get; set; }

    }
    public class ComboAsesorDTO
    {
        public int? IdPersonal { get; set; }
        public string? Nombres { get; set; }

    }
    public class ComboCoordinadorDTO
    {
        public int? IdPersonalJefe { get; set; }
        public string? NombresJefe { get; set; }

    }
    public class ComboBusquedaDTO
    {
        public List<ComboProgramaCriticoDTO>? ComboProgramaCritico { get; set; }
        public List<ComboProgramaGeneralDTO>? ComboProgramaGeneral { get; set; }
        public List<ComboAsesorDTO>? ComboAsesor { get; set; }
        public List<ComboCoordinadorDTO>? ComboCoordinador { get; set; }
    }

    public class ListaIdAsignacionRegularDTO
    {
        public int? Id { get; set; }
    }

    public class ObtenerOportunidadDTO
    {
        public int? Id { get; set; }
    }

    public class ObtenerOportunidadConfiguradaDTO
    {
        public int? Id { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdPGeneral { get; set; }
        public int? IdPais { get; set; }
        public bool? DatoCalidad { get; set; }
        public bool? DatoCalidadWhatsapp { get; set; }
        public bool? DatoCalidadMailing { get; set; }
        public bool? AsignacionRegular { get; set; }
    }

    public class ObtenerOportunidadConfiguradaV2DTO
    {
        public int? Id { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdPGeneral { get; set; }
        public int? IdPais { get; set; }
        public bool? AsignacionDirecta { get; set; }
        public bool? AsignacionDirectaWhatsapp { get; set; }
        public bool? AsigancionDirectaMailing { get; set; }
        public bool? AsignacionRegular { get; set; }
    }



    public class DetallePaisConfiguracionAsignacionRegularPaisDTO
    {

        public int Id { get; set; }
        public int IdPaisConfiguracionAsignacionRegular   { get; set; }
        public bool DatoCalidad { get; set; }
        public bool Distribucion { get; set; }
        public bool DatoCalidadWhatsapp { get; set; }
        public bool DatoCalidadMailing { get; set; }
        public int  IdPais { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
	    public DateTime FechaCreacion  { get; set; }
        public DateTime FechaModificacion { get; set; }


    }
    public class ObtenerAsesoresPorOportunidadDTO
    {
        public int? Id { get; set; }
        public int? IdAsignacionRegular { get; set; }
        public int? IdPGeneral { get; set; }
        public int? TopeOportunidad { get; set; }
        public int? IdPersonal { get; set; }
        public int? CantidadTotal { get; set; }
        public bool? ActivarAsignacionAutomatica { get; set; }
        public bool? ActivarAsignacionPaisConfiguracion { get; set; }
        public int? CantidadTotalPeru { get; set; }
        public bool? DatoCalidadPeru { get; set; }
        public bool? DatoCalidadWhatsapp { get; set; }
        public bool? DatoCalidadMailing { get; set; }
        public int? DistribucionPeru { get; set; }
        public int? CantidadTotalColombia { get; set; }
        public bool? DatoCalidadColombia { get; set; }
        public int? DistribucionColombia { get; set; }
        public int? CantidadTotalBolivia { get; set; }
        public bool? DatoCalidadBolivia { get; set; }
        public int? DistribucionBolivia { get; set; }
        public int? CantidadTotalMexico { get; set; }
        public bool? DatoCalidadMexico { get; set; }
        public int? DistribucionMexico { get; set; }
        public int? CantidadTotalChile { get; set; }
        public int? DistribucionChile { get; set; }
        public bool? DatoCalidadChile { get; set; }
        public int? CantidadTotalInternacional { get; set; }
        public int? DistribucionInternacional { get; set; }
        public bool? DatoCalidadInternacional { get; set; }
    }

    public class VerificarSiAplicaProporcionAsignacionRegularDTO
    {
        public string? Nombre { get; set; }
        public int? IdValidarAsignacionRegular { get; set; }
        public string? MensajeValidacion { get; set; }

    }

    public class InsertarOrigenSectorDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

    }

    public class SenderDTO
    {
        public string? Email { get; set; }
        public string? Contrasenia { get; set; }
    }


    public class AddresseeDTO
    {
        public string? Email { get; set; }
    }
    public class PersonalPostulanteDTO
    {
        public int IdPersonal { get; set; }
        public int IdPostulante { get; set; }
    }
    public class AlgoritmoAsignacionAutomaticaPorPaisesDTO
    {
        public int? IdAsignacionRegular { get; set; }
        public int? ProporcionPorPais { get; set; }
        public int? IdPGeneral { get; set; }
        public int? IdPersonal { get; set; }
        public bool? DatoCalidad { get; set; }
        public bool? AplicaProporcionPorPais { get; set; }
        public int? PorcentajeTolerancia { get; set; }
        public bool? EsLimiteCola { get; set; }
        public int? LimiteCola { get; set; }
        public int? CantidadTotal { get; set; }
        public int? IdPais { get; set; }
        public int? cantidadOPOpais { get; set; }
        public int? CantidadCola { get; set; }
        public bool? ValidadorCantidad { get; set; }
        public bool? ValidadorTolerancia { get; set; }
        public bool? ValidadorLimiteCola { get; set; }
    }
    public class EstadoActualizacionDTO
    {
        public bool? Valor { get; set; }
    }

    public class AsignarAsesorManualDTO
    {
        public int?[] IdOportunidades { get; set; }
        public int? IdAsesor { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public int? IdCentroCosto { get; set; }
        public bool? SegunMejorPro { get; set; }
        public bool? envioWhats { get; set; }
        public bool? VentaCruzadaMarketing { get; set; }

    }

    public class WaObjetoDTO
    {
        public string WaObjeto { get; set; }

    }

    public class ObjetoDTO
    {

        public StatusesDTO[] statuses { get; set; }
    }

    public class ContactsDTO
    {
        public ProfileDTO profile { get; set; }
    }
    public class ContactsSDTO
    {
        public NameDTO name { get; set; }
        public PhonesDTO[] phones { get; set; }
    }
    public class MessagesDTO
    {
        public ContextDTO context { get; set; }
        public string from { get; set; }
        public string group_id { get; set; }
        public string id { get; set; }
        public string timestamp { get; set; }
        public string type { get; set; }
        public SystemDTO system { get; set; }
        public text text { get; set; }
        public image image { get; set; }
        public VideoDTO video { get; set; }
        public document document { get; set; }
        public AudioDTO audio { get; set; }
        public VoiceDTO voice { get; set; }
        public contacts[] contacts { get; set; }
        public errors[] errors { get; set; }
    }

    public class StatusesDTO
    {
        public string id { get; set; }
        public string recipient_id { get; set; }
        public string status { get; set; }
        public string timestamp { get; set; }
        public Message message { get; set; }

        public errors[] errors { get; set; }
    }
    public class Message
    {
        public string recipient_id { get; set; }
    }
    public class ProfileDTO
    {
        public string name { get; set; }
    }

    public class ContextDTO
    {
        public string from { get; set; }
        public string id { get; set; }
    }

    public class Text
    {
        public string body { get; set; }
    }

    public class ImageDTO
    {
        public string caption { get; set; }
        public string file { get; set; }
        public string filename { get; set; }
        public string id { get; set; }
        public string mime_type { get; set; }
        public string sha256 { get; set; }
    }

    public class VideoDTO
    {
        public string caption { get; set; }
        public string file { get; set; }
        public string filename { get; set; }
        public string id { get; set; }
        public string mime_type { get; set; }
        public string sha256 { get; set; }
    }

    public class DocumentDTO
    {
        public string caption { get; set; }
        public string file { get; set; }
        public string filename { get; set; }
        public string id { get; set; }
        public string mime_type { get; set; }
        public string sha256 { get; set; }
    }
    public class AudioDTO
    {
        public string caption { get; set; }
        public string file { get; set; }
        public string filename { get; set; }
        public string id { get; set; }
        public string mime_type { get; set; }
        public string sha256 { get; set; }
    }

    public class VoiceDTO
    {
        public string caption { get; set; }
        public string file { get; set; }
        public string filename { get; set; }
        public string id { get; set; }
        public string mime_type { get; set; }
        public string sha256 { get; set; }
    }

    public class NameDTO
    {
        public string first_name { get; set; }
        public string formatted_name { get; set; }
        public string last_name { get; set; }
    }

    public class PhonesDTO
    {
        public string phone { get; set; }
        public string type { get; set; }
        public string wa_id { get; set; }
    }

    public class SystemDTO
    {
        public string body { get; set; }
    }

    public class ErrorsDTO
    {
        public int code { get; set; }
        public string details { get; set; }
        public string title { get; set; }
        public string href { get; set; }
    }
    public class WspDTO
    {
        public string WaId { get; set; }

    }
    public class RecibeOBJ
    {
        public string WaObjeto { get; set; }

    }


    public class AlumnoOp
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Celular { get; set; }

    }

    public class ContadorBicDias
    {
        public int DiasSinContactoManhana { get; set; }
        public int DiasSinContactoTarde { get; set; }

    }

    public class PlantillaAsig
    {
        public string Texto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

    }


    public class PGeneralOportunidad
    {
        public int IdOportunidad { get; set; }
        public int IdPGeneral { get; set; }

    }

    public class AsesoresValidacionDTO
    {
        public int Id { get; set; }
    }




}

