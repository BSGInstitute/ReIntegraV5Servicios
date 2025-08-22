namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AsignacionAutomaticaTempDTO
    {
        public string Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Fijo { get; set; }
        public string Movil { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public string Pais { get; set; }//Codigo Pais
        public string Ciudad { get; set; }
        public string Empresa { get; set; }
        public string Ip { get; set; }
        public string FechaRegistro { get; set; }
        public string HoraRegistro { get; set; }
        public string NombrePrograma { get; set; }
        public string CentroCosto { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdTipoDato { get; set; }
        public string IdOrigen { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public string Campania { get; set; }
        public int? IdCampania { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public string IdFaseOportunidadPortal { get; set; }//Guid
        public string ProveedorAsignado { get; set; }
        public int? IdTiempoCapacitacion { get; set; }

        public int? IdCategoriaOrigen { get; set; }//categoria Dato
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public int? IdInteraccionFormulario { get; set; }
        public string UrlOrigen { get; set; }

        //public int? FKPagina { get; set; }
        public int? IdPagina { get; set; }
        public string? CiudadFacebook { get; set; }
        public bool? AptoProcesamiento { get; set; } = true;
    }

    public class NombreCampaniaAsiAsignacionAutomaticaTempDTO
    {
        public string IdFaseOportunidadPortal { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public string NombreCampania { get; set; }
    }


    public class InsertarAsignacionAutomaticaTempDTO
    {
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Movil { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public string? Origen { get; set; }
        public bool? Procesado { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public int? IdCategoriaDato { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public bool? AptoProcesamiento { get; set; }
        public string? idCampaniaGoogle { get; set; }
        public bool Estado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }


    }

    public class AsignacionAutomaticaTemDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Fijo { get; set; }
        public string Movil { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public string NombrePrograma { get; set; }
        public int? IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public string Origen { get; set; }
        public bool? Procesado { get; set; }
        public int? IdFacebookFormularioLeadgen { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdAnuncioFacebook { get; set; }
        public Guid? IdFaseOportunidadPortal { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public int? IdTiempoCapacitacion { get; set; }
        public int? IdCategoriaDato { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public int? IdInteraccionFormulario { get; set; }
        public string UrlOrigen { get; set; }
        public int? IdPagina { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public string? CiudadFacebook { get; set; }
        public bool? AptoProcesamiento { get; set; } = true;
    }

    public class AsignacionAutomaticaTemModeloDTO
    {
      
            public string Id { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Correo { get; set; }
            public string Fijo { get; set; }
            public string Movil { get; set; }
            public int? IdAreaFormacion { get; set; }
            public int? IdAreaTrabajo { get; set; }
            public int? IdCargo { get; set; }
            public int? IdIndustria { get; set; }
            public string Pais { get; set; }//Codigo Pais
            public string Ciudad { get; set; }
            public string Empresa { get; set; }
            public string Ip { get; set; }
            public string FechaRegistro { get; set; }
            public string HoraRegistro { get; set; }
            public string NombrePrograma { get; set; }
            public string CentroCosto { get; set; }
            public int? IdCentroCosto { get; set; }
            public int? IdTipoDato { get; set; }
            public string IdOrigen { get; set; }
            public int? IdFaseOportunidad { get; set; }
            public string Campania { get; set; }
            public int? IdCampania { get; set; }
            public DateTime? FechaRegistroCampania { get; set; }
            public string IdFaseOportunidadPortal { get; set; }//Guid
            public string ProveedorAsignado { get; set; }
            public int? IdTiempoCapacitacion { get; set; }

            public int? IdCategoriaOrigen { get; set; }//categoria Dato
            public int? IdConjuntoAnuncio { get; set; }
            public int? IdTipoInteraccion { get; set; }
            public int? IdInteraccionFormulario { get; set; }
            public string UrlOrigen { get; set; }

            //public int? FKPagina { get; set; }
            public int? IdPagina { get; set; }
            public bool Estado { get; set; }

            public DateTime FechaCreacion { get; set; }
            public string UsuarioCreacion { get; set; }
            public DateTime FechaModificacion { get; set; }
            public string UsuarioModificacion { get; set; }
        

    }
}
