namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ModuloCreacionDTO
    {
        public int IdModulo { get; set; }
        public string NombreModulo { get; set; }
        public int IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public string URL { get; set; }
        public string Etiqueta { get; set; }
        public string Icono { get; set; }
    }
    public class ModuloAgrupacionDTO
    {
        public int IdModulo { get; set; }
        public string NombreModulo { get; set; }
        public int IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public string URL { get; set; }
        public string Etiqueta { get; set; }
        public string Icono { get; set; }
        public int? IdModuloSistemaTipo { get; set; }
        public string NombreModuloSistemaTipo { get; set; }
    }

    public class GrupoModuloUsuarioDTO
    {
        public int IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public List<SubGrupoModuloUsuarioDTO> SubGrupoModulo { get; set; }
    }

    public class SubGrupoModuloUsuarioDTO
    {
        public int IdGrupo { get; set; }
        public int? IdModuloSistemaTipo { get; set; }
        public string NombreModuloSistemaTipo { get; set; }
        public List<ModuloUsuarioDTO> Modulos { get; set; }
    }
    public class ModuloUsuarioDTO
    {
        public int IdGrupo { get; set; }
        public int? IdModuloSistemaTipo { get; set; }
        public int IdModulo { get; set; }
        public string NombreModulo { get; set; }
        public string URL { get; set; }
        public string Etiqueta { get; set; }
        public string Icono { get; set; }
    }
    public class IpPublicaDTO
    {
        public string ipIntegra { get; set; }
        public string usuario { get; set; }
    }
}
