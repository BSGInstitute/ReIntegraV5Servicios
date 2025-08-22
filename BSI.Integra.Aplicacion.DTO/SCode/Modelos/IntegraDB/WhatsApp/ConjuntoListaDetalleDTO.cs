using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
    public class ConjuntoListaDetalleDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public int IdConjuntoLista { get; set; }

        public byte Prioridad { get; set; }


    }


    public class ConjuntoListaDetalleListasDTO
    {
        public int IdConjuntoLista { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte Prioridad { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public List<FiltroSegmentoValorTipoDTO> ListaArea { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaSubArea { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaProgramaGeneral { get; set; }

    }



    public class ConjuntoListaDetalleEnvioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdConjuntoLista { get; set; }
        public byte Prioridad { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public List<ConjuntoListaDetalleValor> ListaConjuntoListaDetalleValor { get; set; }
    }

   
}
