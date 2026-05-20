namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    // --- Tipos de condición ---
    public class BsgTentoTipoCondicionDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
    }

    // --- Tipos de misión ---
    public class BsgTentoTipoMisionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
    }

    // --- Logros ---
    public class BsgTentoLogroDTO
    {
        public int IdLogro { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int TipoLogro { get; set; }
        public int ValorObjetivo { get; set; }
        public string IconoCodigo { get; set; }
        public string ColorHexadecimal { get; set; }
        public int IdLogroTipoCondicion { get; set; }
        public string NombreTipoCondicion { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public string NombreAreaCapacitacion { get; set; }
        public int? IdPGeneral { get; set; }
        public string NombrePGeneral { get; set; }
        public int Orden { get; set; }
    }

    public class BsgTentoLogroInsertarDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int TipoLogro { get; set; }
        public int IdLogroTipoCondicion { get; set; }
        public int ValorObjetivo { get; set; }
        public string IconoCodigo { get; set; }
        public string ColorHexadecimal { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int? IdPGeneral { get; set; }
        public int Orden { get; set; }
    }

    public class BsgTentoLogroActualizarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int TipoLogro { get; set; }
        public int IdLogroTipoCondicion { get; set; }
        public int ValorObjetivo { get; set; }
        public string IconoCodigo { get; set; }
        public string ColorHexadecimal { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int? IdPGeneral { get; set; }
    }

    // --- Misiones ---
    public class BsgTentoMisionDTO
    {
        public int IdMision { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdMisionTipo { get; set; }
        public string NombreTipoMision { get; set; }
        public int ValorObjetivo { get; set; }
        public int RecompensaXP { get; set; }
        public int RecompensaPuntos { get; set; }
        public string IconoCodigo { get; set; }
        public string ColorHexadecimal { get; set; }
        public int IdLogroTipoCondicion { get; set; }
        public string NombreTipoCondicion { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public string NombreAreaCapacitacion { get; set; }
        public int Orden { get; set; }
    }

    public class BsgTentoMisionInsertarDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdMisionTipo { get; set; }
        public int IdLogroTipoCondicion { get; set; }
        public int ValorObjetivo { get; set; }
        public int RecompensaXP { get; set; }
        public int RecompensaPuntos { get; set; }
        public string IconoCodigo { get; set; }
        public string ColorHexadecimal { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int Orden { get; set; }
    }

    public class BsgTentoMisionActualizarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdMisionTipo { get; set; }
        public int IdLogroTipoCondicion { get; set; }
        public int ValorObjetivo { get; set; }
        public int RecompensaXP { get; set; }
        public int RecompensaPuntos { get; set; }
        public string IconoCodigo { get; set; }
        public string ColorHexadecimal { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int Orden { get; set; }
    }
}
