namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CampoFormularioDTO
    {
        public int Id { get; set; }

        public int? IdFormularioSolicitud { get; set; }

        public int IdCampoContacto { get; set; }

        public int? NroVisitas { get; set; }

        public string? Codigo { get; set; }

        public string? Campo { get; set; }


        public bool? Siempre { get; set; }

        public bool? Inteligente { get; set; }

        public bool? Probabilidad { get; set; }
    }

    public class datosInsertarCamposDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public int? NroVisitas { get; set; }
        public bool? Siempre { get; set; }
        public bool? Inteligente { get; set; }
        public bool? Probabilidad { get; set; }
        public string? ListaOpcion { get; set; } 
    }

    public class CampoFormularioSeleccionadoDTO
    {
        public int Id { get; set; }
        public int IdFormularioSolicitud { get; set; }
        public int IdCampoContacto { get; set; }
        public int NroVisitas { get; set; }
        public string Codigo { get; set; }
        public bool? Estado { get; set; }
        public string Nombre { get; set; }
        public bool? Siempre { get; set; }
        public bool? Inteligente { get; set; }
        public bool? Probabilidad { get; set; }
        public string? ValorOpciones { get; set; }
    }

}
