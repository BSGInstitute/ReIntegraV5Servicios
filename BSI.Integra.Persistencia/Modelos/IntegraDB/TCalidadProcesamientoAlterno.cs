using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCalidadProcesamientoAlterno
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con T_Oportunidad
        /// </summary>
        public int? IdOportunidad { get; set; }
        /// <summary>
        /// Campos llenos en Perfil
        /// </summary>
        public int PerfilCamposLlenos { get; set; }
        /// <summary>
        /// Total de Campos en Perfil
        /// </summary>
        public int PerfilCamposTotal { get; set; }
        /// <summary>
        /// Estado Tiene Dni
        /// </summary>
        public bool TieneDni { get; set; }
        /// <summary>
        /// Estado de Sentinel Verificado
        /// </summary>
        public bool SentinelVerificado { get; set; }
        /// <summary>
        /// Total de PGeneral Motivacion Validados
        /// </summary>
        public int PgeneralMotivacionValidado { get; set; }
        /// <summary>
        /// Total de PGeneral Motivacion
        /// </summary>
        public int PgeneralMotivacionTotal { get; set; }
        /// <summary>
        /// Total de Publico Objetivo Validados
        /// </summary>
        public int PublicoObjetivoValidado { get; set; }
        /// <summary>
        /// Total de Publico Objetivo
        /// </summary>
        public int PublicoObjetivoTotal { get; set; }
        /// <summary>
        /// Total de Prerequisitos de Programas Validados
        /// </summary>
        public int PrerequisitoProgramaValidado { get; set; }
        /// <summary>
        /// Total de Prerequisitos Programa
        /// </summary>
        public int PrerequisitoProgramaTotal { get; set; }
        /// <summary>
        /// Total de Requisitos de Certificacion Validados
        /// </summary>
        public int RequisitoCertificacionValidado { get; set; }
        /// <summary>
        /// Total de Requisitos de Certificacion
        /// </summary>
        public int RequisitoCertificacionTotal { get; set; }
        /// <summary>
        /// Total de Beneficios Validados
        /// </summary>
        public int BeneficiosValidados { get; set; }
        /// <summary>
        /// Total de Beneficios
        /// </summary>
        public int BeneficiosTotales { get; set; }
        /// <summary>
        /// Estado de Verificacion de Inicio Programa
        /// </summary>
        public bool InicioProgramaVerificado { get; set; }
        /// <summary>
        /// Estado de Verificacion de  Competidores
        /// </summary>
        public bool CompetidoresVerificacion { get; set; }
        /// <summary>
        /// Cantidad de Competidores
        /// </summary>
        public int CantidadCompetidores { get; set; }
        /// <summary>
        /// Cantidad de Problema Seleccionados
        /// </summary>
        public int ProblemaSeleccionados { get; set; }
        /// <summary>
        /// Cantidad de Problema Solucionados
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
