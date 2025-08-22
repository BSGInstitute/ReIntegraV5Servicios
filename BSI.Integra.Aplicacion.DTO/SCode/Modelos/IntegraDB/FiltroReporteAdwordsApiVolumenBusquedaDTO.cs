namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FiltroReporteAdwordsApiVolumenBusquedaDTO
    {
        //public string Palabras { get; set; }
        public List<FiltroReporteGrupoPalabrasTipoPalabra> ListaPalabras { get; set; }
        public int TipoPalabra { get; set; }
        public int[] Paises { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Usuario { get; set; }
        public int IdIdioma { get; set; }
    }

    public class FiltroReporteGrupoPalabrasTipoPalabra
    {
        public int TipoTexto { get; set; }
        public string CadenaTexto { get; set; }
    }

    public class AdwordsApiVolumenBusquedaHistoricoDTO
    {
        public string PalabraClave { get; set; }
        public int PromedioBusqueda { get; set; }
        public int Mes { get; set; }
        public int Anho { get; set; }
        public int IdPais { get; set; }
    }
    public class AdwordsApiVolumenBusquedaHistoricoRespuestaDTO
    {
        public string PalabraClave { get; set; }
        public int PromedioBusqueda { get; set; }
        public int Mes { get; set; }
        public int Anho { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
    }

}
