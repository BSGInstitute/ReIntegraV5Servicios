namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TipoDescuentoDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Formula { get; set; }
        public int? PorcentajeGeneral { get; set; }
        public int? PorcentajeMatricula { get; set; }
        public int? FraccionesMatricula { get; set; }
        public int? PorcentajeCuotas { get; set; }
        public int? CuotasAdicionales { get; set; }
        public int? IdTipoDescuentoNivelAprobacion { get; set; }
        public bool? TipoPrograma { get; set; }
    }
    public class TipoDescuentoComboDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
    public class TipoDescuentoOportunidadDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Formula { get; set; }
        public int? PorcentajeGeneral { get; set; }
        public int? PorcentajeMatricula { get; set; }
        public int? FraccionesMatricula { get; set; }
        public int? PorcentajeCuotas { get; set; }
        public int? CuotasAdicionales { get; set; }
        public string Tipo { get; set; } = null!;
    }

    public class ComboTipoDescuentoDTO
    {
        public IEnumerable<FormulaTipoDescuentoDTO> FormulaTipoDescuentos { get; set; }
        public IEnumerable<AgendaTipoUsuarioDTO> TiposUsuario { get; set; }
        public IEnumerable<ComboDTO> ProgramasGeneral { get; set; }
    }

    public class CompuestoTipoDescuentoDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Formula { get; set; }
        public int? PorcentajeGeneral { get; set; }
        public int? PorcentajeMatricula { get; set; }
        public int? FraccionesMatricula { get; set; }
        public int? PorcentajeCuotas { get; set; }
        public int? CuotasAdicionales { get; set; }
        public int? IdTipoDescuentoNivelAprobacion { get; set; }
        public bool? TipoPrograma { get; set; }
        public List<string> TipoDescuentoAsesorCoordinadorPw { get; set; }

    }
    public class TipoDescuentoProgramaDTO
    {
        public int IdTipoDescuento { get; set; }
        public List<int> IdPgeneral { get; set; }
    }

    public class TipoDescuentoConNivelAprobacionDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Formula { get; set; }
        public int? PorcentajeGeneral { get; set; }
        public int? PorcentajeMatricula { get; set; }
        public int? FraccionesMatricula { get; set; }
        public int? PorcentajeCuotas { get; set; }
        public int? CuotasAdicionales { get; set; }
        public int? IdTipoDescuentoNivelAprobacion { get; set; }
        public bool? TipoPrograma { get; set; }
    }

    public class TipoDescuentoNivelAprobacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
    }

    public class ActualizarNivelAprobacionDTO
    {
        public int Id { get; set; }
        public int? IdTipoDescuentoNivelAprobacion { get; set; }
    }


}
