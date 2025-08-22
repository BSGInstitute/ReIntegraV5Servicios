using System.ComponentModel.DataAnnotations;
namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    public class CentroEstudioDTO
    {
		public int Id { get; set; }
		[StringLength(600, MinimumLength = 1)]
		public string Nombre { get; set; } = null!;
		[Required]
		public int IdPais { get; set; }
		[Required]
		public int IdCiudad { get; set; }
		[Required]
		public int IdTipoCentroEstudio { get; set; }
		public string? Pais { get; set; }
		public string? Ciudad { get; set; }
		public string? TipoCentroEstudio { get; set; }
	}

    public class CentroEstudioComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

}
