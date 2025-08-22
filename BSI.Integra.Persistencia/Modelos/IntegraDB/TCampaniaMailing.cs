using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCampaniaMailing
    {
        public TCampaniaMailing()
        {
            TCampaniaMailingDetalles = new HashSet<TCampaniaMailingDetalle>();
            TCampaniaMailingValorTipos = new HashSet<TCampaniaMailingValorTipo>();
            TPrioridadMailChimpLista = new HashSet<TPrioridadMailChimpListum>();
            TPrioridadMailChimpListaCorreos = new HashSet<TPrioridadMailChimpListaCorreo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int PrincipalValor { get; set; }
        public string PrincipalValorTiempo { get; set; } = null!;
        public int SecundarioValor { get; set; }
        public string SecundarioValorTiempo { get; set; } = null!;
        public int ActivaValor { get; set; }
        public string ActivaValorTiempo { get; set; } = null!;
        /// <summary>
        /// Id al parcer autoincremental, por definir relacion
        /// </summary>
        public int IdParaConjuntoAnuncios { get; set; }
        public int IdCategoriaOrigen { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Fecha de inicio para excluir en caso se le ha enviado un correo para el programa general principal
        /// </summary>
        public DateTime? FechaInicioExcluirPorEnviadoMismoProgramaGeneralPrincipal { get; set; }
        /// <summary>
        /// Fecha final para excluir en caso se le ha enviado un correo para el programa general principal
        /// </summary>
        public DateTime? FechaFinExcluirPorEnviadoMismoProgramaGeneralPrincipal { get; set; }

        public virtual ICollection<TCampaniaMailingDetalle> TCampaniaMailingDetalles { get; set; }
        public virtual ICollection<TCampaniaMailingValorTipo> TCampaniaMailingValorTipos { get; set; }
        public virtual ICollection<TPrioridadMailChimpListum> TPrioridadMailChimpLista { get; set; }
        public virtual ICollection<TPrioridadMailChimpListaCorreo> TPrioridadMailChimpListaCorreos { get; set; }
    }
}
