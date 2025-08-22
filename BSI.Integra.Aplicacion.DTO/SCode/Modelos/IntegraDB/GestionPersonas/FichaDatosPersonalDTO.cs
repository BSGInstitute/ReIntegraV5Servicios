using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Google.Api.Ads.AdWords.v201809;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class FichaDatosPersonalDTO
    {
        public MaestroPersonalPuestoSedeDTO DatosPersonal { get; set; }
        public PersonalCeseDTO DatosPersonalCese { get; set; }
        public IEnumerable<PersonalRemuneracionDTO> PersonalRemuneracion { get; set; }
        public List<PersonalFormacionDTO> Formacion { get; set; }
        public List<PersonalInformaticaDTO> Computo { get; set; }
        public List<PersonalIdiomaDTO> Idioma { get; set; }
        public List<PersonalCertificacionDTO> Certificacion { get; set; }
        public List<PersonalExperienciaDTO> Experiencia { get; set; }
        public List<DatoFamiliarPersonalDTO>  DatoFamiliar { get; set; }
        public List<PersonalInformacionMedicaDTO> InformacionMedica { get; set; }
        public List<PersonalHistorialMedicoDTO> HistorialMedico { get; set; }
        public List<PersonalSistemaPensionarioDTO> SistemaPensionario { get; set; }
        public List<PersonalSeguroSaludDTO> SeguroSalud { get; set; }
        public DatoContratoPersonalDTO DatoContratoPersonal { get; set; }
        public List<MaestroPersonalGrupoAccesoTemporalDTO> ListaAccesoTemporal { get; set; }
        public List<PersonalPuestoTrabajoDTO> listaPuestoTrabajo { get; set; }
        public IEnumerable<PersonalDireccionVistaDTO> PersonalDireccion { get; set; }
        public List<PersonalTipoAsesorDTO> listaTipoAsesorHistorico { get; set; }
        public List<PersonalJefeInmediatoDTO> listaJefeInmediatoHistorico { get; set; }
        public List<PersonalTiempoInactivoHistoricoDTO> listaPeriodoInactivoHistorico { get; set; }
        public PersonalTiempoInactivoHistoricoDTO DatoPersonalDescanso { get; set; }

    }
    public class MaestroPersonalCompuestoDTO
    {
        public DatosMaestroPersonalDTO? Personal { get; set; }
        public DatosPersonalCeseDTO? PersonalCese { get; set; }
        public DatosPersonalDescansoDTO? PersonalDescanso { get; set; }
        public DatosPersonalRemuneracionDTO? PersonalRemuneracion { get; set; }
        public List<PersonalCertificacionDTO>? PersonalCertificacion { get; set; }
        public List<PersonalExperienciaDTO>? PersonalExperiencia { get; set; }
        public List<PersonalFamiliarDTO>? PersonalFamiliar { get; set; }
        public List<PersonalFormacionDTO>? PersonalFormacion { get; set; }
        public List<PersonalHistorialMedicoDTO>? PersonalHistorialMedico { get; set; }
        public List<PersonalIdiomaDTO>? PersonalIdiomas { get; set; }
        public List<PersonalInformacionMedicaDTO>? PersonalInformacionMedica { get; set; }
        public List<PersonalInformaticaDTO>? PersonalInformatica { get; set; }
        public PersonalSeguroSaludDTO? PersonalSeguroSalud { get; set; }
        public PersonalSistemaPensionarioDTO? PersonalSistemaPensionario { get; set; }
        public PersonalDireccionDTO? PersonalDireccion { get; set; }
    }



    public class DatosPersonalDescansoDTO
    {
        public DateTime? FechaInicioDescanso { get; set; }
        public DateTime? FechaFinDescanso { get; set; }
        public int? IdMotivoInactividad { get; set; }
        public bool EsModificado { get; set; }
    }
    public class PersonalFamiliarDTO
    {
        public string Apellidos { get; set; }
        public bool DerechoHabiente { get; set; }
        public bool EsContactoInmediato { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Id { get; set; }
        public int IdParentescoPersonal { get; set; }
        public int IdPersonal { get; set; }
        public int IdSexo { get; set; }
        public int IdTipoDocumentoPersonal { get; set; }
        public string Nombres { get; set; }
        public string NumeroDocumento { get; set; }
        public string NumeroReferencia { get; set; }
    }
    public class PersonalDireccionDTO
    {
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public string? Distrito { get; set; }
        public int? Lote { get; set; }
        public string? Manzana { get; set; }
        public string? NombreVia { get; set; }
        public string? NombreZonaUrbana { get; set; }
        public string? TipoVia { get; set; }
        public string? TipoZonaUrbana { get; set; }
        public bool EsModificado { get; set; }
    }

    public class DatosMaestroPersonalDTO
    {
        public int Id { get; set; }
        public string Apellidos { get; set; }
        public string? DistritoDireccion { get; set; }
        public string? EmailReferencia { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdCiudadNacimiento { get; set; }
        public int? IdCiudadReferencia { get; set; }
        public int? IdEstadocivil { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public int? IdPaisReferencia { get; set; }
        public int? IdSexo { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string? NombreDireccion { get; set; }
        public string Nombres { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? TelefonoFijo { get; set; }
        public string? TelefonoMovil { get; set; }
        //=============================================
        public string? Email { get; set; }
        public int? IdJefe { get; set; }
        public string? TipoPersonal { get; set; }
        public string? Central { get; set; }
        public string? Anexo3CX { get; set; }
        public string? UrlFirmaCorreos { get; set; }
        public bool Activo { get; set; }

        public string? Area { get; set; }
        public string? AreaAbrev { get; set; }
        public int? IdPuestoTrabajo { get; set; }
        public int? IdSede { get; set; }
        public int? IdTipoSangre { get; set; }
        public bool? EsCerrador { get; set; }
        public int? IdAsesorAsociado { get; set; }
        public int? IdPuestoTrabajoNivel { get; set; }
        public int? IdPersonalArchivo { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdTableroComercialCategoriaAsesor { get; set; }
    }

    public class  ArchivoDTO
    {
        public  bool Respuesta { get; set; }
        public PersonalArchivoDTO? Datos { get; set; }
        public string? Html { get; set; }
        public string? Mensaje { get; set; }

    }
    public class DescargarArchivoDTO
    {
        public bool Respuesta { get; set; }
        public bool EsImagen { get; set; }
        public string? RutaArchivo { get; set; }
        public PersonalArchivoDTO? Datos { get; set; }
        public string? Mensaje { get; set; }

    }


    public class ActualizarAccesoTemporalDTO
    {
        public int IdPersonal { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public int? IdPEspecificoPadreAnterior { get; set; }
        public List<int> ListaPEspecificoHijo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime? FechaInicioAnterior { get; set; }
        public DateTime? FechaFinAnterior { get; set; }
        public bool EvaluacionHabilitada { get; set; }
        public string? Usuario { get; set; }
    }

    public class EliminarAccesoTemporalDTO
    {
        public int IdPersonal { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }


    public class ArchivoPersonalDTO
    {
        public IFormFile File { get; set; }
        public string? Usuario { get; set; }
        public int? Id { get; set; }

    }


}
