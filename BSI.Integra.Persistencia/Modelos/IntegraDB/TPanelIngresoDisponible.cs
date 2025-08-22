using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPanelIngresoDisponible
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave Fonaea con T_FormaPago
        /// </summary>
        public int? IdFormaPago { get; set; }
        /// <summary>
        /// Dias Deposito
        /// </summary>
        public int DiasDeposito { get; set; }
        /// <summary>
        /// Dias Disponible
        /// </summary>
        public int DiasDisponible { get; set; }
        /// <summary>
        /// Cuenta Feriados
        /// </summary>
        public bool CuentaFeriados { get; set; }
        /// <summary>
        /// Considera Viernes Sabado Domingo
        /// </summary>
        public bool ConsideraVsd { get; set; }
        /// <summary>
        /// Considera Dias Habiles Lunes Sabado
        /// </summary>
        public bool ConsideraDiasHabilesLunesSabado { get; set; }
        /// <summary>
        /// Considera Dias Habiles Lunes Viernes
        /// </summary>
        public bool ConsideraDiasHabilesLunesViernes { get; set; }
        /// <summary>
        /// Considera Dias Fijo Semana
        /// </summary>
        public bool ConsideraDiasFijoSemana { get; set; }
        /// <summary>
        /// Hora Corte
        /// </summary>
        public int HoraCorte { get; set; }
        /// <summary>
        /// Minuto Corte
        /// </summary>
        public int MinutoCorte { get; set; }
        /// <summary>
        /// Porcentaje Cobro
        /// </summary>
        public decimal PorcentajeCobro { get; set; }
        /// <summary>
        /// Cuenta Feriados Estatales
        /// </summary>
        public bool CuentaFeriadosEstatales { get; set; }
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
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Dias Fijo Semana
        /// </summary>
        public int? IdDiaSemanaFijo { get; set; }
    }
}
