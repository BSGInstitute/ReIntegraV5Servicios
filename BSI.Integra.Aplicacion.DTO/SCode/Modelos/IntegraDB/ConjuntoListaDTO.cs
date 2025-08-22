using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConjuntoListaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdCategoriaObjetoFiltro { get; set; }
        public int IdFiltroSegmento { get; set; }
        public byte NroListasRepeticionContacto { get; set; }
        public int? ConsiderarYaEnviados { get; set; }

    }


    public class ConjuntoListaDetalleCompletoDTO
    {
        public ConjuntoListaDTO ConjuntoLista { get; set; }
        public List<ConjuntoListaDetalleDTO> ConjuntoListaDetalle { get; set; }
        public string NombreUsuario { get; set; }
    }


    public class ConjuntoListaDetalleCompletoListoDTO
    {
        public ConjuntoListaDTO ConjuntoLista { get; set; }
    }



    public class ConjuntoListaCompuestoDTO
    {
        public int IdAlumno { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string NombreAlumno { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public string NombreConjuntoListaDetalle { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string NombrePais { get; set; }
        public string NombreCiudad { get; set; }
        public string NombreAreaFormacion { get; set; }
        public string NombreAreaTrabajo { get; set; }
        public string NombreIndustria { get; set; }
        public string NombreCargo { get; set; }
        public bool? EsVentaCruzada { get; set; }

        public string NombreCentroCosto { get; set; }
        public string CodigoMatricula { get; set; }
        public int? IdOportunidad { get; set; }

    }

    public class ConjuntoListaEnvioDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdCategoriaObjetoFiltro { get; set; }
        public int IdFiltroSegmento { get; set; }
        public byte NroListasRepeticionContacto { get; set; }
        public int? ConsiderarYaEnviados { get; set; }
        public DateTime? HoraEjecucion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }

        public List<ConjuntoListaDetalleEnvioDTO> ListaConjuntoListaDetalle { get; set; }

        public ConjuntoListaEnvioDTO()
        {
            ListaConjuntoListaDetalle = new List<ConjuntoListaDetalleEnvioDTO>();
        }

    }


    public class ConjuntoListaGrillaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdCategoriaObjetoFiltro { get; set; }
        public string NombreCategoriaObjeto { get; set; }
        public int IdFiltroSegmento { get; set; }
        public byte NroListasRepeticionContacto { get; set; }
        public int? ConsiderarYaEnviados { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

    }

    public class ConjuntoListaSubirDTO
    {
        public int idConjuntoLista { get; set; }
        public string listaIds { get; set; }
        
    }





    public class EtiquetaPlantillaSendinblueDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Etiqueta { get; set; }
        public int IdPlantillaSendinblueTipoEtiqueta { get; set; }
        public string NombreTipo { get; set; }
    }

    public class UpdatePlantillaSendinblueDTO
    {
        public int Id { get; set; }
        public string HtmlContenido { get; set; }


    }

    public class InsertarPlantillaSendinblueDTO
    {
        public string Nombre { get; set; }
        public string HtmlContent { get; set; }
        public int IdTipoPlantilla { get; set; }
        public bool Estado { get; set; }
        public string? Usuario { get; set; }
        public DateTime Fecha { get; set; }


    }

    public class InsertarImagenesSendinblueDTO
    {
        public string NombreArchivo { get; set; }
        public string Ruta { get; set; }
        public string Extension { get; set; }
        public bool Estado { get; set; }
        public string? Usuario { get; set; }
        public DateTime Fecha { get; set; }


    }


    public class InsertarDatosEtiquetaDTO
    {
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public string Label { get; set;  }
    }

    public class ImagenMarketingDTO
    {
        public IFormFile file { get; set; }

    }

    public class GenerarFormularioDTO
    {
        public string Nombre { get; set; }
        public int IdCentroCosto { get; set; }
    }



}
