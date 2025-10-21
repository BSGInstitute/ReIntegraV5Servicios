using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAlumno
    {
        public TAlumno()
        {
            TChatbotPortalHiloChats = new HashSet<TChatbotPortalHiloChat>();
            TGoogleAdsConversionQueues = new HashSet<TGoogleAdsConversionQueue>();
            TModeloDataMinings = new HashSet<TModeloDataMining>();
            TOportunidadGoogleLeads = new HashSet<TOportunidadGoogleLead>();
        }

        /// <summary>
        /// Identificador
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Primer nombre
        /// </summary>
        public string? Nombre1 { get; set; }
        /// <summary>
        /// Segundo nombre
        /// </summary>
        public string? Nombre2 { get; set; }
        /// <summary>
        /// Apellido paterno
        /// </summary>
        public string? ApellidoPaterno { get; set; }
        /// <summary>
        /// Apellido materno
        /// </summary>
        public string? ApellidoMaterno { get; set; }
        /// <summary>
        /// Numero de DNI
        /// </summary>
        public string? Dni { get; set; }
        /// <summary>
        /// Direccion de donde vive el alumno
        /// </summary>
        public string? Direccion { get; set; }
        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        public DateTime? FechaNacimiento { get; set; }
        /// <summary>
        /// Nombre del pais
        /// </summary>
        public string? Pais { get; set; }
        /// <summary>
        /// Nombre de la ciudad o region
        /// </summary>
        public int? Ciudad { get; set; }
        /// <summary>
        /// Telefono fijo
        /// </summary>
        public string? Telefono { get; set; }
        /// <summary>
        /// Telefono movil
        /// </summary>
        public string? Celular { get; set; }
        /// <summary>
        /// Correo electronico principal
        /// </summary>
        public string? Email1 { get; set; }
        /// <summary>
        /// Correo electronico secundario
        /// </summary>
        public string? Email2 { get; set; }
        /// <summary>
        /// Nombre del nivel de formacion
        /// </summary>
        public string? NivelFormacion { get; set; }
        /// <summary>
        /// Nombre de la profesion
        /// </summary>
        public string? Profesion { get; set; }
        /// <summary>
        /// Nombre de la empresa
        /// </summary>
        public string? Empresa { get; set; }
        /// <summary>
        /// Nombre del estado civil
        /// </summary>
        public string? EstadoCivil { get; set; }
        /// <summary>
        /// Telefono de un familiar
        /// </summary>
        public string? TelefonoFamiliar { get; set; }
        /// <summary>
        /// Nombres completos de un familiar
        /// </summary>
        public string? NombreFamiliar { get; set; }
        /// <summary>
        /// Parentesco con el familiar
        /// </summary>
        public string? Parentesco { get; set; }
        /// <summary>
        /// Numero de telefono del trabajo
        /// </summary>
        public string? TelefonoTrabajo { get; set; }
        /// <summary>
        /// En anexo de la oficina del trabajo
        /// </summary>
        public string? TelefonoTrabajoAnexo { get; set; }
        /// <summary>
        /// Si es hombre o mujer
        /// </summary>
        public string? Genero { get; set; }
        /// <summary>
        /// Cuenta de skype
        /// </summary>
        public string? Skype { get; set; }
        /// <summary>
        /// Numero de fax
        /// </summary>
        public string? Fax { get; set; }
        /// <summary>
        /// codigo del pais
        /// </summary>
        public int? IdPais { get; set; }
        /// <summary>
        /// Nombre del pais
        /// </summary>
        public string? UbigeoPais { get; set; }
        /// <summary>
        /// Nombre del departamento
        /// </summary>
        public string? UbigeoDepartamento { get; set; }
        /// <summary>
        /// Nombre de la provincia
        /// </summary>
        public string? UbigeoProvincia { get; set; }
        /// <summary>
        /// Nombre de la ciudad o region
        /// </summary>
        public string? UbigeoCiudad { get; set; }
        /// <summary>
        /// Nombre del distrito
        /// </summary>
        public string? UbigeoDistrito { get; set; }
        /// <summary>
        /// Nombre de la calle
        /// </summary>
        public string? DireccionCalle { get; set; }
        /// <summary>
        /// Nombre de la avenida
        /// </summary>
        public string? DireccionAv { get; set; }
        /// <summary>
        /// Nombre de la zona geografica
        /// </summary>
        public string? DireccionZona { get; set; }
        /// <summary>
        /// POR DEFINIR
        /// </summary>
        public string? DireccionComp { get; set; }
        /// <summary>
        /// Nombre de la torre
        /// </summary>
        public string? DireccionTorre { get; set; }
        /// <summary>
        /// Nombre del edificio
        /// </summary>
        public string? DireccionEdificio { get; set; }
        /// <summary>
        /// Numero del departamento
        /// </summary>
        public string? DireccionDpto { get; set; }
        /// <summary>
        /// Nombre de la urbanizacion
        /// </summary>
        public string? DireccionUrb { get; set; }
        /// <summary>
        /// Nombre por la manzana de la ubicacion
        /// </summary>
        public string? DireccionMz { get; set; }
        /// <summary>
        /// Nombre por el lote
        /// </summary>
        public string? DireccionLt { get; set; }
        /// <summary>
        /// Nombre de la referencia para llagar
        /// </summary>
        public string? ReferenciaDetallada { get; set; }
        /// <summary>
        /// Horario donde el alumno es localizado
        /// </summary>
        public string? HoraMaxima { get; set; }
        /// <summary>
        /// Nombre del puesto que ocupa
        /// </summary>
        public string? Puesto { get; set; }
        /// <summary>
        /// Fecha del aniversario de la boda del alumno
        /// </summary>
        public string? AniversarioBodas { get; set; }
        /// <summary>
        /// Numero de hijos
        /// </summary>
        public string? NroHijo { get; set; }
        /// <summary>
        /// Validacion por llamada telefonica
        /// </summary>
        public bool? ValidacionTelefonica { get; set; }
        /// <summary>
        /// Fase en que esta el contacto (No se usa)
        /// </summary>
        public string? FaseContacto { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_CARGO
        /// </summary>
        public int? IdCargo { get; set; }
        /// <summary>
        /// Nombre del cargo
        /// </summary>
        public string? Cargo { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_AREAFORMACION
        /// </summary>
        public int? IdAformacion { get; set; }
        /// <summary>
        /// Nombre del area de formacion
        /// </summary>
        public string? Aformacion { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_AREATRABAJO
        /// </summary>
        public int? IdAtrabajo { get; set; }
        /// <summary>
        /// Nombre del area de trabajo
        /// </summary>
        public string? Atrabajo { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_INDUSTRIA
        /// </summary>
        public int? IdIndustria { get; set; }
        /// <summary>
        /// Nombre de la industria
        /// </summary>
        public string? Industria { get; set; }
        /// <summary>
        /// Identificador del referido (no se usa)
        /// </summary>
        public int? IdReferido { get; set; }
        /// <summary>
        /// Nombre del referido (no se usa)
        /// </summary>
        public string? Referido { get; set; }
        /// <summary>
        /// Codigo del pais
        /// </summary>
        public int? IdCodigoPais { get; set; }
        /// <summary>
        /// Nombre del pais
        /// </summary>
        public string? NombrePais { get; set; }
        /// <summary>
        /// Id de  T_Ciudad
        /// </summary>
        public int? IdCiudad { get; set; }
        /// <summary>
        /// Nombre de la ciudad o region
        /// </summary>
        public string? NombreCiudad { get; set; }
        /// <summary>
        /// Hora del contacto con el alumno
        /// </summary>
        public string? HoraContacto { get; set; }
        /// <summary>
        /// Horario en peru del contacto con el alumno
        /// </summary>
        public string? HoraPeru { get; set; }
        /// <summary>
        /// Identificador del codigo de la ciudad (no se usa)
        /// </summary>
        public int? IdCodigoRegionCiudad { get; set; }
        /// <summary>
        /// Numero de telefono fijo
        /// </summary>
        public string? Telefono2 { get; set; }
        /// <summary>
        /// Numero de telefono movil
        /// </summary>
        public string? Celular2 { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Empresas
        /// </summary>
        public int? IdEmpresa { get; set; }
        /// <summary>
        /// Identificador de la oportunidad inicial
        /// </summary>
        public int? IdOportunidadInicial { get; set; }
        /// <summary>
        /// Clave del usuario para la web
        /// </summary>
        public string? UsClave { get; set; }
        /// <summary>
        /// Identificador del tipo de documento
        /// </summary>
        public Guid? IdTipoDocumento { get; set; }
        /// <summary>
        /// Numero del documento de identidad
        /// </summary>
        public string? NroDocumento { get; set; }
        /// <summary>
        /// Descripcion del cargo
        /// </summary>
        public string? DescripcionCargo { get; set; }
        /// <summary>
        /// No se usa
        /// </summary>
        public bool? Asociado { get; set; }
        public bool? DeSuscrito { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Almacena la cantidad de oportunidades que tiene este contacto
        /// </summary>
        public int? NroOportunidades { get; set; }
        public bool? EsPersonaValida { get; set; }
        public bool? EsEliminadoPorRegularizacion { get; set; }
        public bool? TieneOportunidad { get; set; }
        public bool? TieneMatricula { get; set; }
        public bool? EsRepetido { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_EstadoContactoWhatsApp
        /// </summary>
        public int? IdEstadoContactoWhatsApp { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_EstadoContactoMailling
        /// </summary>
        public int? IdEstadoContactoMailing { get; set; }
        /// <summary>
        /// nueva direccion para envio de certificados
        /// </summary>
        public string? DireccionEnvioCertificado { get; set; }
        /// <summary>
        /// indica si se usara una ubicacion adicional o la que existe actualmente para envio de certificado
        /// </summary>
        public bool? UsarNuevaDireccionParaEnvio { get; set; }
        /// <summary>
        /// Ciudad a la que se hara el envio de certificado fisico
        /// </summary>
        public string? CiudadEnvioCertificado { get; set; }
        /// <summary>
        /// FK de T_EstadoContactoWhatsApp para celular secundario
        /// </summary>
        public int? IdEstadoContactoWhatsAppSecundario { get; set; }
        /// <summary>
        /// FK de T_TipoDocumentoPersonal
        /// </summary>
        public int? CodigoPortal { get; set; }
        /// <summary>
        /// Identificador numero de la tabla tipo documento del portal
        /// </summary>
        public int? IdNumeroTipoDocumento { get; set; }
        /// <summary>
        /// FK de T_Genero
        /// </summary>
        public int? IdGenero { get; set; }
        public string? Comentario { get; set; }
        /// <summary>
        /// Url del CV del Alumno
        /// </summary>
        public string? UrlCvAlumno { get; set; }
        /// <summary>
        /// Nombre del CV del Alumno
        /// </summary>
        public string? NombreArchivoCvAlumno { get; set; }
        /// <summary>
        /// Municipio del lugar de residencia del alumno
        /// </summary>
        public string? Municipio { get; set; }
        /// <summary>
        /// Estado o provincia del lugar de residencia del alumno
        /// </summary>
        public string? EstadoLugar { get; set; }
        /// <summary>
        /// Código postal del lugar de residencia del alumno
        /// </summary>
        public string? CodigoPostal { get; set; }
        /// <summary>
        /// Nombre de la colonia o barrio del lugar de residencia del alumno
        /// </summary>
        public string? Colonia { get; set; }
        /// <summary>
        /// Id del Asentamiento/Colonia a la que pertenece
        /// </summary>
        public int? IdAsentamientoMexico { get; set; }
        /// <summary>
        /// Id del Municipio a la que pertenece
        /// </summary>
        public int? IdMunicipioMexico { get; set; }
        /// <summary>
        ///  Clave Única de Registro de Población 
        /// </summary>
        public string? Curp { get; set; }
        /// <summary>
        /// (FK) de T_CiudadMexico
        /// </summary>
        public int? IdCiudadMexico { get; set; }
        /// <summary>
        /// Nombre del Registro Federal de Contribuyentes
        /// </summary>
        public string? Rfc { get; set; }
        public string? PrincipalResponsabilidadProfesional { get; set; }
        public int? IdExperiencia { get; set; }
        /// <summary>
        /// (FK) de mkt.T_TamanioEmpresa, detalle de tamaño de la empresa donde trabaja el alumno
        /// </summary>
        public int? IdTamanioEmpresa { get; set; }
        /// <summary>
        /// (FK) de pla.T_TamanioEmpresaAgenda
        /// </summary>
        public int? IdTamanioEmpresaAgenda { get; set; }

        public virtual TAlumnoCuponRegistro TAlumnoCuponRegistro { get; set; } = null!;
        public virtual ICollection<TChatbotPortalHiloChat> TChatbotPortalHiloChats { get; set; }
        public virtual ICollection<TGoogleAdsConversionQueue> TGoogleAdsConversionQueues { get; set; }
        public virtual ICollection<TModeloDataMining> TModeloDataMinings { get; set; }
        public virtual ICollection<TOportunidadGoogleLead> TOportunidadGoogleLeads { get; set; }
    }
}
