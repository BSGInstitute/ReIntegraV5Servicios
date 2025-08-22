namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class GrupoFiltroProgramaCriticoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public bool Estado { get; set; }


        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;


        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }


    }
    public class GrupoFiltroProgramaCriticoCombosDTO
    {
        public IEnumerable<ComboDTO> ListaAreaCapacitacion { get; set; }
        public List<SubAreaCapacitacionFiltroDTO> ListaSubAreaCapacitacion { get; set; }
    }

    public class PGeneralProgramaCriticoSubAreaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
        public string NombreAreaCapacitacion { get; set; }
        public string NombreSubAreaCapacitacion { get; set; }
        public string EstadoPGeneral { get; set; }
    }
    public class PGeneralSubAreaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
        public string? NombreAreaCapacitacion { get; set; }
        public string? NombreSubAreaCapacitacion { get; set; }
        public bool? AsignaVenta { get; set; }
    }

    public class CompuestoGrupoFiltroProgramaCriticoDTO
    {
        public GrupoFiltroProgramaCriticoDatoDTO GrupoFiltroProgramaCritico { get; set; }
        public List<int>? GrupoFiltroProgramaCriticoPorAsesor { get; set; }
        public string Usuario { get; set; }
        public int IdGrupo { get; set; }
    }

    public class GrupoFiltroProgramaCriticoDatoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class EliminacionGrupoFiltroCentroCostoDTO
    {
        public int Id { get; set; }
        public int IdCentroCosto { get; set; }
    }

    public class EliminacionGrupoFiltroPGeneralDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
    }
    public class PGeneralAsignaVentaDTO
    {
        public int Id { get; set; }
        public bool AsignaVenta { get; set; }
        public string UsuarioModificacion { get; set; }
    }

    public class FiltroIdNombreDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

    }

}


