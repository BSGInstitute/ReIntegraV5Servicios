using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class BsgTentoPowerUpDTO
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
        public int Orden { get; set; }
        public List<PowerUpCanalDistribucionDTO> Canales { get; set; }
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
        public int Orden { get; set; }
        public List<CanalDistribucionActualizarDTO> Canales { get; set; }
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
        public int Orden { get; set; }
        public List<CanalDistribucionActualizarDTO> Canales { get; set; }
    }

    public class CanalDistribucionDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
    }

    public class CanalDistribucionActualizarDTO
    {
        public int IdCanalDistribucion { get; set; }
        public bool Disponible { get; set; }
    }

    public class PowerUpCanalDistribucionDTO
    {
        public int IdPowerUpCanalDistribucion { get; set; }
        public int IdPowerUp { get; set; }
        public int IdCanalDistribucion { get; set; }
        public bool Disponible { get; set; }
        public string CodigoCanalDistribucion { get; set; }
        public string NombreCanalDistribucion { get; set; }
    }
}
