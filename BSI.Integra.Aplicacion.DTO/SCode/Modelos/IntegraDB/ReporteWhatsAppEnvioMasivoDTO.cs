namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteWhatsAppEnvioMasivoDTO
    {
        public int IdPersonal { get; set; }
        public string Asesor { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public int CantidadRecibidos { get; set; }
        public int CantidadRespondidos { get; set; }
        public int CantidadEnviado { get; set; }
        public int CantidadEntregado { get; set; }
        public int CantidadLeido { get; set; }
        public int OportunidadesCreadas { get; set; }
        public string ConjuntoListaDetalle { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public string PrioridadMailChimpLista { get; set; }
        public int IdPrioridadMailChimpLista { get; set; }
        public int CantidadMensajeValido { get; set; }
        public int CantidadMensajeInvalido { get; set; }
        public int CantidadOportunidadIS { get; set; }
        public int CantidadOportunidadCerrada { get; set; }
        public int CantidadOportunidadBIC { get; set; }
        public int CantidadOportunidadE { get; set; }
        public int CantidadOportunidadDesuscrito { get; set; }
        public double ratioEntregadoPorEnviado { get; set; }
        public double ratioDesuscritoPorValido { get; set; }
        public double ratioCreadoPorValido { get; set; }
        public double ratioCerradodoPorValido { get; set; }
        public double ratioFaseBICdoPorValido { get; set; }
        public double ratioFaseEPorValido { get; set; }
        public double ratioOportunidadISPorCerrado { get; set; }

        //
        public double ratioLeidoEntregado { get; set; }
        public double ratioRecibidoLeido { get; set; }
        public double ratioOportunidadRecibido { get; set; }
    }

}
