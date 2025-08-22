using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDatoContratoPersonal
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreign key con T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Es foreign key con T_TipoContrato
        /// </summary>
        public int IdTipoContrato { get; set; }
        /// <summary>
        /// Estado del contrato
        /// </summary>
        public bool EstadoContrato { get; set; }
        /// <summary>
        /// Fecha inicio del contrato
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha fin del contrato
        /// </summary>
        public DateTime? FechaFin { get; set; }
        /// <summary>
        /// Remuneracion Fija
        /// </summary>
        public decimal RemuneracionFija { get; set; }
        /// <summary>
        /// Fk t_tipopagoremuneracion
        /// </summary>
        public int? IdTipoPagoRemuneracion { get; set; }
        /// <summary>
        /// fk t_entidad financiera
        /// </summary>
        public int? IdEntidadFinancieraPago { get; set; }
        /// <summary>
        /// numero cuenta de pago
        /// </summary>
        public string? NumeroCuentaPago { get; set; }
        /// <summary>
        /// fk t_puestotrabajo
        /// </summary>
        public int IdPuestoTrabajo { get; set; }
        /// <summary>
        /// fk t_sedetrabajo
        /// </summary>
        public int IdSedeTrabajo { get; set; }
        /// <summary>
        /// fk t_personalareatrabajo
        /// </summary>
        public int IdPersonalAreaTrabajo { get; set; }
        /// <summary>
        /// fk t_cargo
        /// </summary>
        public int IdCargo { get; set; }
        /// <summary>
        /// fk t_tipoperfil
        /// </summary>
        public int? IdTipoPerfil { get; set; }
        /// <summary>
        /// fk t_personal
        /// </summary>
        public int? IdPersonalJefe { get; set; }
        /// <summary>
        /// fk t_entidadfinanciera
        /// </summary>
        public int? IdEntidadFinancieraCts { get; set; }
        /// <summary>
        /// Numero cuenta cts
        /// </summary>
        public string? NumeroCuentaCts { get; set; }
        /// <summary>
        /// Es periodo prueba
        /// </summary>
        public bool? EsPeridoPrueba { get; set; }
        /// <summary>
        /// Fecha fin periodo prueba
        /// </summary>
        public DateTime? FechaFinPeriodoPrueba { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de Modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Relacion con el id de la tabla original
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// FK de TContratoEstado
        /// </summary>
        public int? IdContratoEstado { get; set; }
        /// <summary>
        /// Url Ubicación de Documento Contrato
        /// </summary>
        public string? UrlDocumentoContrato { get; set; }

        public virtual TCargo IdCargoNavigation { get; set; } = null!;
        public virtual TContratoEstado? IdContratoEstadoNavigation { get; set; }
        public virtual TPuestoTrabajo IdPuestoTrabajoNavigation { get; set; } = null!;
        public virtual TSedeTrabajo IdSedeTrabajoNavigation { get; set; } = null!;
        public virtual TTipoContrato IdTipoContratoNavigation { get; set; } = null!;
        public virtual TTipoPagoRemuneracion? IdTipoPagoRemuneracionNavigation { get; set; }
        public virtual TTipoPerfil? IdTipoPerfilNavigation { get; set; }
    }
}
