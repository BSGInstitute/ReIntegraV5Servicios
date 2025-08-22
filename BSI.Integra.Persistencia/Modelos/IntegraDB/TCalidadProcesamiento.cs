using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCalidadProcesamiento
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Lave foranea con T_Oportunidad
        /// </summary>
        public int? IdOportunidad { get; set; }
        /// <summary>
        /// Campo perfil llenos
        /// </summary>
        public int PerfilCamposLlenos { get; set; }
        /// <summary>
        /// Campo perfil total
        /// </summary>
        public int PerfilCamposTotal { get; set; }
        /// <summary>
        /// Campo dni
        /// </summary>
        public bool Dni { get; set; }
        /// <summary>
        /// Campo programa general validados
        /// </summary>
        public int PgeneralValidados { get; set; }
        /// <summary>
        /// Campo programa general total
        /// </summary>
        public int PgeneralTotal { get; set; }
        /// <summary>
        /// Campo programa especifico validados
        /// </summary>
        public int PespecificoValidados { get; set; }
        /// <summary>
        /// Campo programa especifico total
        /// </summary>
        public int PespecificoTotal { get; set; }
        /// <summary>
        /// Campo beneficio validados
        /// </summary>
        public int BeneficiosValidados { get; set; }
        /// <summary>
        /// Campo beneficio total
        /// </summary>
        public int BeneficiosTotales { get; set; }
        /// <summary>
        /// Campo competidores verificacion
        /// </summary>
        public bool CompetidoresVerificacion { get; set; }
        /// <summary>
        /// Campo problemas seleccionados
        /// </summary>
        public int ProblemaSeleccionados { get; set; }
        /// <summary>
        /// Campo problemas solucionados
        /// </summary>
        public int ProblemaSolucionados { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }

        public virtual TOportunidad? IdOportunidadNavigation { get; set; }
    }
}
