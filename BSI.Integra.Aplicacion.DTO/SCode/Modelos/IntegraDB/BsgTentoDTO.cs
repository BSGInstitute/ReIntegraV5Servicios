using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    // --- Read DTOs ---
    public class BsgTentoAreaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class BsgTentoUnidadDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
    }

    public class BsgTentoPasoDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string NombrePGeneral { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
    }

    public class BsgTentoComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    // --- Write DTOs ---
    public class BsgTentoUnidadInsertarDTO
    {
        public int IdAreaCapacitacion { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
    }

    public class BsgTentoUnidadActualizarDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
    }

    public class BsgTentoPasoInsertarDTO
    {
        public int IdUnidadEstudio { get; set; }
        public int IdPGeneral { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
    }

    public class BsgTentoPasoActualizarDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
    }

    public class BsgTentoOrdenDTO
    {
        public int Id { get; set; }
        public int Orden { get; set; }
    }
}
