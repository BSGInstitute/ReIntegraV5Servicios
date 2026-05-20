namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class BsgTentoPowerUpDTO
    {
        public int IdPowerUp { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Efecto { get; set; }
        public int CostoMonedas { get; set; }
        public int CostoPuntos { get; set; }
        public string IconoCodigo { get; set; }
        public string ColorHexadecimal { get; set; }
        public bool DisponibleTienda { get; set; }
        public bool RecompensaDisponible { get; set; }
        public bool DisponibleRuletaDiaria { get; set; }
        public int Orden { get; set; }
    }

    public class BsgTentoPowerUpInsertarDTO
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Efecto { get; set; }
        public int CostoMonedas { get; set; }
        public int CostoPuntos { get; set; }
        public string IconoCodigo { get; set; }
        public string ColorHexadecimal { get; set; }
        public bool DisponibleTienda { get; set; }
        public bool RecompensaDisponible { get; set; }
        public bool DisponibleRuletaDiaria { get; set; }
        public int Orden { get; set; }
    }

    public class BsgTentoPowerUpActualizarDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Efecto { get; set; }
        public int CostoMonedas { get; set; }
        public int CostoPuntos { get; set; }
        public string IconoCodigo { get; set; }
        public string ColorHexadecimal { get; set; }
        public bool DisponibleTienda { get; set; }
        public bool RecompensaDisponible { get; set; }
        public bool DisponibleRuletaDiaria { get; set; }
        public int Orden { get; set; }
    }
}
