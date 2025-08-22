using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConjuntoAnuncio : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;

        public int? IdCategoriaOrigen { get; set; }

        public string? Origen { get; set; }

        public string? IdConjuntoAnuncioFacebook { get; set; }

        public DateTime? FechaCreacionCampania { get; set; }


        public int? IdConjuntoAnuncioFuente { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdConjuntoAnuncioTipoObjetivo { get; set; }
        public int? IdFormularioPlantilla { get; set; }
        public string Adicional { get; set; }
        public string NumeroAnuncio { get; set; }
        public string NumeroSemana { get; set; }
        public string Propietario { get; set; }
        public string DiaEnvio { get; set; }
        public string EnlaceFormulario { get; set; }
        public bool? EsGrupal { get; set; }
        public bool? EsPaginaWeb { get; set; }
        public bool? EsPrelanzamiento { get; set; }
        public int? IdConjuntoAnuncioSegmento { get; set; }
        public int? IdConjuntoAnuncioTipoGenero { get; set; }
        public int? IdPais { get; set; }
    }
}
