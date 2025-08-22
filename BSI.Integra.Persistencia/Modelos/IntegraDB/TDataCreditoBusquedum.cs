using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDataCreditoBusquedum
    {
        public TDataCreditoBusquedum()
        {
            TDataCreditoConsulta = new HashSet<TDataCreditoConsultum>();
            TDataCreditoDataCuentaAhorros = new HashSet<TDataCreditoDataCuentaAhorro>();
            TDataCreditoDataCuentaCarteras = new HashSet<TDataCreditoDataCuentaCartera>();
            TDataCreditoDataEndeudamientoGlobals = new HashSet<TDataCreditoDataEndeudamientoGlobal>();
            TDataCreditoDataInfAgrComposicionPortafolios = new HashSet<TDataCreditoDataInfAgrComposicionPortafolio>();
            TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedios = new HashSet<TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio>();
            TDataCreditoDataInfAgrEvolucionDeudaTrimestres = new HashSet<TDataCreditoDataInfAgrEvolucionDeudaTrimestre>();
            TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta = new HashSet<TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum>();
            TDataCreditoDataInfAgrHistoricoSaldoTotals = new HashSet<TDataCreditoDataInfAgrHistoricoSaldoTotal>();
            TDataCreditoDataInfAgrResumenComportamientos = new HashSet<TDataCreditoDataInfAgrResumenComportamiento>();
            TDataCreditoDataInfAgrResumenEndeudamientos = new HashSet<TDataCreditoDataInfAgrResumenEndeudamiento>();
            TDataCreditoDataInfAgrResumenPrincipals = new HashSet<TDataCreditoDataInfAgrResumenPrincipal>();
            TDataCreditoDataInfAgrResumenSaldoMes = new HashSet<TDataCreditoDataInfAgrResumenSaldoMe>();
            TDataCreditoDataInfAgrResumenSaldoSectors = new HashSet<TDataCreditoDataInfAgrResumenSaldoSector>();
            TDataCreditoDataInfAgrResumenSaldos = new HashSet<TDataCreditoDataInfAgrResumenSaldo>();
            TDataCreditoDataInfAgrTotals = new HashSet<TDataCreditoDataInfAgrTotal>();
            TDataCreditoDataInfMicroAnalisisVectors = new HashSet<TDataCreditoDataInfMicroAnalisisVector>();
            TDataCreditoDataInfMicroEndeudamientoActuals = new HashSet<TDataCreditoDataInfMicroEndeudamientoActual>();
            TDataCreditoDataInfMicroEvolucionDeuda = new HashSet<TDataCreditoDataInfMicroEvolucionDeudum>();
            TDataCreditoDataInfMicroImagenTendenciaEndeudamientos = new HashSet<TDataCreditoDataInfMicroImagenTendenciaEndeudamiento>();
            TDataCreditoDataInfMicroPerfilGenerals = new HashSet<TDataCreditoDataInfMicroPerfilGeneral>();
            TDataCreditoDataInfMicroVectorSaldoMoras = new HashSet<TDataCreditoDataInfMicroVectorSaldoMora>();
            TDataCreditoDataNaturalNacionals = new HashSet<TDataCreditoDataNaturalNacional>();
            TDataCreditoDataProductoValors = new HashSet<TDataCreditoDataProductoValor>();
            TDataCreditoDataScores = new HashSet<TDataCreditoDataScore>();
            TDataCreditoDataTarjetaCreditos = new HashSet<TDataCreditoDataTarjetaCredito>();
        }

        public int Id { get; set; }
        public DateTime FechaConsulta { get; set; }
        public string CodigoSeguridad { get; set; } = null!;
        public int TipoIdentificacion { get; set; }
        public string NroDocumento { get; set; } = null!;
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

        public virtual ICollection<TDataCreditoConsultum> TDataCreditoConsulta { get; set; }
        public virtual ICollection<TDataCreditoDataCuentaAhorro> TDataCreditoDataCuentaAhorros { get; set; }
        public virtual ICollection<TDataCreditoDataCuentaCartera> TDataCreditoDataCuentaCarteras { get; set; }
        public virtual ICollection<TDataCreditoDataEndeudamientoGlobal> TDataCreditoDataEndeudamientoGlobals { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrComposicionPortafolio> TDataCreditoDataInfAgrComposicionPortafolios { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedios { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrEvolucionDeudaTrimestre> TDataCreditoDataInfAgrEvolucionDeudaTrimestres { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum> TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrHistoricoSaldoTotal> TDataCreditoDataInfAgrHistoricoSaldoTotals { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrResumenComportamiento> TDataCreditoDataInfAgrResumenComportamientos { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrResumenEndeudamiento> TDataCreditoDataInfAgrResumenEndeudamientos { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrResumenPrincipal> TDataCreditoDataInfAgrResumenPrincipals { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrResumenSaldoMe> TDataCreditoDataInfAgrResumenSaldoMes { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrResumenSaldoSector> TDataCreditoDataInfAgrResumenSaldoSectors { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrResumenSaldo> TDataCreditoDataInfAgrResumenSaldos { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrTotal> TDataCreditoDataInfAgrTotals { get; set; }
        public virtual ICollection<TDataCreditoDataInfMicroAnalisisVector> TDataCreditoDataInfMicroAnalisisVectors { get; set; }
        public virtual ICollection<TDataCreditoDataInfMicroEndeudamientoActual> TDataCreditoDataInfMicroEndeudamientoActuals { get; set; }
        public virtual ICollection<TDataCreditoDataInfMicroEvolucionDeudum> TDataCreditoDataInfMicroEvolucionDeuda { get; set; }
        public virtual ICollection<TDataCreditoDataInfMicroImagenTendenciaEndeudamiento> TDataCreditoDataInfMicroImagenTendenciaEndeudamientos { get; set; }
        public virtual ICollection<TDataCreditoDataInfMicroPerfilGeneral> TDataCreditoDataInfMicroPerfilGenerals { get; set; }
        public virtual ICollection<TDataCreditoDataInfMicroVectorSaldoMora> TDataCreditoDataInfMicroVectorSaldoMoras { get; set; }
        public virtual ICollection<TDataCreditoDataNaturalNacional> TDataCreditoDataNaturalNacionals { get; set; }
        public virtual ICollection<TDataCreditoDataProductoValor> TDataCreditoDataProductoValors { get; set; }
        public virtual ICollection<TDataCreditoDataScore> TDataCreditoDataScores { get; set; }
        public virtual ICollection<TDataCreditoDataTarjetaCredito> TDataCreditoDataTarjetaCreditos { get; set; }
    }
}
