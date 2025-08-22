namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class TCRM_CentroCostoByAsesorDetallesDTO
    {
        public int Id { get; set; }
        public string Alumno { get; set; }
        public string Asesor { get; set; }
        public int idCentroCosto { get; set; }
        public string nombreCC { get; set; }
        public int idAsesor { get; set; }
        public int idCodigoPais { get; set; }
        public double PrecioReal { get; set; }//nuevos valores solo para calcular valor
        public double PrecioRealDolar { get; set; }
        public double PrecioSinDesc { get; set; }
        public double PrecioSinDescDolar { get; set; }
        public double Descuento { get; set; }//nuevos valores solo para calcular valor
        public double DescuentoDolar { get; set; }
        public double Mes { get; set; }
        public double MesDolar { get; set; }
        public string CodigoMatricula { get; set; }

    }

}

