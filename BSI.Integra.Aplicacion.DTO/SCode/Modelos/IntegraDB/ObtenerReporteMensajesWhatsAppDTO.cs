namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ObtenerReporteMensajesWhatsAppDTO
    {
        public string Pais { get; set; }
        public int IdPais { get; set; }
        public string Asesor { get; set; }
        public int IdPersonal { get; set; }
        public int PlantillaEnviadas { get; set; }
        public int PlantillaEntregadas { get; set; }
        public int MensajeEnviados { get; set; }
        public int MensajeEntregados { get; set; }
        public int TotalEnviados { get; set; }
        public int TotalEntregados { get; set; }
        public int PlantillaLeidas { get; set; }
        public int MensajesLeidos { get; set; }
        public int TotalLeidos { get; set; }
        public int Leidos { get; set; }
        public int Recibidos { get; set; }
        public decimal Costo { get; set; }
        public decimal CostoTotal { get; set; }
    }

    public class ObtenerReporteMensajesWhatsAppPorTipoDTO
    {
        public string Pais { get; set; }
        public int IdPais { get; set; }
        public string Asesor { get; set; }
        public int IdPersonal { get; set; }
        public int Plantilla1Enviadas { get; set; }
        public int Plantilla2Enviadas { get; set; }
        public int Plantilla3Enviadas { get; set; }
        public int Plantilla1Recibidas { get; set; }
        public int Plantilla2Recibidas { get; set; }
        public int Plantilla3Recibidas { get; set; }
        public int Plantilla1Leidas { get; set; }
        public int Plantilla2Leidas { get; set; }
        public int Plantilla3Leidas { get; set; }
        public int Plantilla1Contestada { get; set; }
        public int Plantilla2Contestada { get; set; }
        public int Plantilla3Contestada { get; set; }
        public int MensajesEnviados { get; set; }
        public int MensajesRecibidos { get; set; }
        public int MensajesLeidos { get; set; }
        public int MensajesContestados { get; set; }
        public decimal CostoTotal { get; set; }
    }

}
