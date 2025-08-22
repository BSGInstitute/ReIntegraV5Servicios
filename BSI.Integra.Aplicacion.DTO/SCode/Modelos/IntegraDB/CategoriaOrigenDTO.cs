namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CategoriaOrigenDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdTipoDato { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
        public int Meta { get; set; }
        public int? IdProveedorCampaniaIntegra { get; set; }
        public int? IdFormularioProcedencia { get; set; }
        public bool Considerar { get; set; }
        public string CodigoOrigen { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? CodigoPublicidad { get; set; }
    }



    public class CategoriaOrigenComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class FiltroTipoDatoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Prioridad { get; set; }
    }

    public class FiltroProcedenciaFormularioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }

    public class FiltroProveedorCampaniaIntegraDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool PorDefecto { get; set; }
    }
    public class FiltroTipoCategoriaOrigenDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Meta { get; set; }
        public int OportunidadMaxima { get; set; }
    }

    public class FiltroTipoCategoriaOrigenTodoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }

    public class FiltroTipoInteraccionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;


    }

    public class ComboFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

    }
    public class CategoriaOrigenFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

    }

    public class ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO
    {
        public int IdCategoriaOrigen { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
    }
    public class CategoriaOrigenSubCategoriaDatoDTO
    {
        public int IdCategoriaOrigen { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
        public int IdSubCategoriaDato { get; set; }
        public string CodigoOrigen { get; set; } = "";
    }
    public class OrigenesCategoriaOrigenDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCategoria { get; set; }
    }
    public class CategoriasOrigenDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdTipoDato { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
        public int Meta { get; set; }
        public int? IdProveedorCampaniaIntegra { get; set; }
        public int? IdFormularioProcedencia { get; set; }
        public bool Considerar { get; set; }
        public string CodigoOrigen { get; set; } = null!;
 
    }

    public class CategoriaOrigenFiltroGrupoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
    }
    public class CategoriaOrigeCombonDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdTipoDato { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
    }
    public class CategoriaOrigenConHijosDTO
    {
        public int IdCategoriaOrigen { get; set; }
        public int IdSubCategoria { get; set; }
        public int IdTipoFormulario { get; set; }
        public string NombreTipoFormulario { get; set; } 
        public string Nombre { get; set; }
    }
    public class CompuestoCategoriaOrigenConHijosDTO
    {
        public ComboDTO CategoriaOrigen { get; set; }
        public List<SubCategoriaFormularioDTO> SubCategoriaFormulario { get; set; }
    }
    public class SubCategoriaFormularioDTO
    {
        public int IdSubCategoria { get; set; }
        public int IdTipoFormulario { get; set; }
        public string NombreTipoFormulario { get; set; }
    }
}
