namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CajaDTO
    {
        public int Id { get; set; }
        public string CodigoCaja { get; set; } = null!;
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; } = null!;
        public int IdBanco { get; set; }
        public string Banco { get; set; } = null!;
        public int IdCuenta { get; set; }
        public string Cuenta { get; set; } = null!;
        public int IdMoneda { get; set; }
        public string Moneda { get; set; } = null!;
        public int IdPais { get; set; }
        public string Pais { get; set; } = null!;
        public int IdCiudad { get; set; }
        public string Ciudad { get; set; } = null!;
        public int IdPersonal { get; set; }
        public string Personal { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Activo { get; set; }
    }

    public class CajaComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string PersonalResponsable { get; set; } = null!;
        public int IdPersonalResponsable { get; set; }
        public int IdMoneda { get; set; }
        public string Moneda { get; set; } = null!;
    }

    public class CajaResponsableComboDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }

    }
    public class CajaDatosDTO
    {
        public int Id { get; set; }
        public string CodigoCaja { get; set; } = null!;
        public int IdMoneda { get; set; }
        public int IdEmpresaAutorizada { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public int IdCuentaCorriente { get; set; }
        public int IdCiudad { get; set; }
        public int IdPersonalResponsable { get; set; }
        public bool Activo { get; set; }
    }


}
