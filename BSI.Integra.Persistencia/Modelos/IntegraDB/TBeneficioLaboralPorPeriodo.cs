using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TBeneficioLaboralPorPeriodo
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id de com.T_AgendaTipoUsuario, define si el monto va a ir para un Asesor o Coordinador
        /// </summary>
        public int IdAgendaTipoUsuario { get; set; }
        /// <summary>
        /// Id de mkt.T_Periodo, a que periodo pertenece el pago del MontoTotalBeneficio
        /// </summary>
        public int IdPeriodo { get; set; }
        /// <summary>
        /// Id de fin.T_TipoBeneficioLaboral, a que beneficio pertenece el MontoTotalBeneficio
        /// </summary>
        public int IdBeneficioLaboralTipo { get; set; }
        /// <summary>
        /// Monto acumulado de un beneficio laboral segun el tipo de Personal
        /// </summary>
        public decimal Monto { get; set; }
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
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
