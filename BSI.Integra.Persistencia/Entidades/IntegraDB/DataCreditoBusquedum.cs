using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoBusquedum : BaseIntegraEntity
    {
        public DateTime FechaConsulta { get; set; }
        [StringLength(10)]
        public string CodigoSeguridad { get; set; } = null!;
        public int TipoIdentificacion { get; set; }
        [StringLength(14)]
        public string NroDocumento { get; set; } = null!;

        public ICollection<DataCreditoConsultum> DataCreditoConsulta { get; set; }
        public ICollection<DataCreditoDataCuentaAhorro> DataCreditoDataCuentaAhorros { get; set; }
        public ICollection<DataCreditoDataCuentaCartera> DataCreditoDataCuentaCarteras { get; set; }
        public ICollection<DataCreditoDataEndeudamientoGlobal> DataCreditoDataEndeudamientoGlobals { get; set; }
        public ICollection<DataCreditoDataInfAgrComposicionPortafolio> DataCreditoDataInfAgrComposicionPortafolios { get; set; }
        public ICollection<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedios { get; set; }
        public ICollection<DataCreditoDataInfAgrEvolucionDeudaTrimestre> DataCreditoDataInfAgrEvolucionDeudaTrimestres { get; set; }
        public ICollection<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum> DataCreditoDataInfAgrHistoricoSaldoTipoCuenta { get; set; }
        public ICollection<DataCreditoDataInfAgrHistoricoSaldoTotal> DataCreditoDataInfAgrHistoricoSaldoTotals { get; set; }
        public ICollection<DataCreditoDataInfAgrResumenComportamiento> DataCreditoDataInfAgrResumenComportamientos { get; set; }
        public ICollection<DataCreditoDataInfAgrResumenEndeudamiento> DataCreditoDataInfAgrResumenEndeudamientos { get; set; }
        public ICollection<DataCreditoDataInfAgrResumenPrincipal> DataCreditoDataInfAgrResumenPrincipals { get; set; }
        public ICollection<DataCreditoDataInfAgrResumenSaldoMe> DataCreditoDataInfAgrResumenSaldoMes { get; set; }
        public ICollection<DataCreditoDataInfAgrResumenSaldoSector> DataCreditoDataInfAgrResumenSaldoSectors { get; set; }
        public ICollection<DataCreditoDataInfAgrResumenSaldo> DataCreditoDataInfAgrResumenSaldos { get; set; }
        public ICollection<DataCreditoDataInfAgrTotal> DataCreditoDataInfAgrTotals { get; set; }
        public ICollection<DataCreditoDataInfMicroAnalisisVector> DataCreditoDataInfMicroAnalisisVectors { get; set; }
        public ICollection<DataCreditoDataInfMicroEndeudamientoActual> DataCreditoDataInfMicroEndeudamientoActuals { get; set; }
        public ICollection<DataCreditoDataInfMicroEvolucionDeudum> DataCreditoDataInfMicroEvolucionDeuda { get; set; }
        public ICollection<DataCreditoDataInfMicroImagenTendenciaEndeudamiento> DataCreditoDataInfMicroImagenTendenciaEndeudamientos { get; set; }
        public ICollection<DataCreditoDataInfMicroPerfilGeneral> DataCreditoDataInfMicroPerfilGenerals { get; set; }
        public ICollection<DataCreditoDataInfMicroVectorSaldoMora> DataCreditoDataInfMicroVectorSaldoMoras { get; set; }
        public ICollection<DataCreditoDataNaturalNacional> DataCreditoDataNaturalNacionals { get; set; }
        public ICollection<DataCreditoDataProductoValor> DataCreditoDataProductoValors { get; set; }
        public ICollection<DataCreditoDataScore> DataCreditoDataScores { get; set; }
        public ICollection<DataCreditoDataTarjetaCredito> DataCreditoDataTarjetaCreditos { get; set; }
    }
}
