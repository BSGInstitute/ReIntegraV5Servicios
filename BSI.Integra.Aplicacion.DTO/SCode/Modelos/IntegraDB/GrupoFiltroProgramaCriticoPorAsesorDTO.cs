namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class GrupoFiltroProgramaCriticoPorAsesorDTO
    {

        public int Id { get; set; }
        public int? IdGrupoFiltroProgramaCritico { get; set; }
        public int IdPersonal { get; set; }

        public bool Estado { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }


    }

}
