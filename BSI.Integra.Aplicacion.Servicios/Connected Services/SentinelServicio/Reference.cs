//------------------------------------------------------------------------------
// <generado automáticamente>
//     Este código fue generado por una herramienta.
//     //
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </generado automáticamente>
//------------------------------------------------------------------------------

namespace SentinelServicio
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "PrivadoE2Cliente", ConfigurationName = "SentinelServicio.WS_BSGrupoSoapPort")]
    public interface WS_BSGrupoSoapPort
    {

        // CODEGEN: Generando contrato de mensaje, ya que la operación tiene múltiples valores devueltos.
        [System.ServiceModel.OperationContractAttribute(Action = "PrivadoE2Clienteaction/AWS_BSGRUPO.Execute", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<SentinelServicio.ExecuteResponse> ExecuteAsync(SentinelServicio.ExecuteRequest request);
        [System.ServiceModel.OperationContractAttribute(Action = "PrivadoE2Clienteaction/AWS_BSGRUPO.Execute", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        SentinelServicio.ExecuteResponse Execute(SentinelServicio.ExecuteRequest request);
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "SDT_IC_Estandar.SDT_IC_EstandarItem", Namespace = "PrivadoE2Cliente")]
    public partial class SDT_IC_EstandarSDT_IC_EstandarItem
    {

        private string tipoDocumentoField;

        private string documentoField;

        private string razonSocialField;

        private System.Nullable<System.DateTime> fechaProcesoField;

        private string semaforosField;

        private string scoreField;

        private string nroBancosField;

        private string deudaTotalField;

        private string vencidoBancoField;

        private string calificativoField;

        private string veces24mField;

        private string semaActualField;

        private string semaPrevioField;

        private string semaPeorMejorField;

        private string documento2Field;

        private string estDomicField;

        private string condDomicField;

        private string deudaTributariaField;

        private string deudaLaboralField;

        private string deudaImpagaField;

        private string deudaProtestosField;

        private string deudaSBSField;

        private string tarCtasField;

        private string repNegField;

        private string tipoActvField;

        private string fechIniActvField;

        private string deudaDirectaField;

        private string deudaIndirectaField;

        private string deudaCastigadaField;

        private string lineaCreditoNoUtiField;

        private string totalRiesgoField;

        private string peorCalificacionField;

        private string porCalNormalField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string TipoDocumento
        {
            get
            {
                return this.tipoDocumentoField;
            }
            set
            {
                this.tipoDocumentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string Documento
        {
            get
            {
                return this.documentoField;
            }
            set
            {
                this.documentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string RazonSocial
        {
            get
            {
                return this.razonSocialField;
            }
            set
            {
                this.razonSocialField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", IsNullable = true, Order = 3)]
        public System.Nullable<System.DateTime> FechaProceso
        {
            get
            {
                return this.fechaProcesoField;
            }
            set
            {
                this.fechaProcesoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string Semaforos
        {
            get
            {
                return this.semaforosField;
            }
            set
            {
                this.semaforosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string Score
        {
            get
            {
                return this.scoreField;
            }
            set
            {
                this.scoreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string NroBancos
        {
            get
            {
                return this.nroBancosField;
            }
            set
            {
                this.nroBancosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string DeudaTotal
        {
            get
            {
                return this.deudaTotalField;
            }
            set
            {
                this.deudaTotalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public string VencidoBanco
        {
            get
            {
                return this.vencidoBancoField;
            }
            set
            {
                this.vencidoBancoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public string Calificativo
        {
            get
            {
                return this.calificativoField;
            }
            set
            {
                this.calificativoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public string Veces24m
        {
            get
            {
                return this.veces24mField;
            }
            set
            {
                this.veces24mField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public string SemaActual
        {
            get
            {
                return this.semaActualField;
            }
            set
            {
                this.semaActualField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public string SemaPrevio
        {
            get
            {
                return this.semaPrevioField;
            }
            set
            {
                this.semaPrevioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public string SemaPeorMejor
        {
            get
            {
                return this.semaPeorMejorField;
            }
            set
            {
                this.semaPeorMejorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        public string Documento2
        {
            get
            {
                return this.documento2Field;
            }
            set
            {
                this.documento2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
        public string EstDomic
        {
            get
            {
                return this.estDomicField;
            }
            set
            {
                this.estDomicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
        public string CondDomic
        {
            get
            {
                return this.condDomicField;
            }
            set
            {
                this.condDomicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
        public string DeudaTributaria
        {
            get
            {
                return this.deudaTributariaField;
            }
            set
            {
                this.deudaTributariaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
        public string DeudaLaboral
        {
            get
            {
                return this.deudaLaboralField;
            }
            set
            {
                this.deudaLaboralField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
        public string DeudaImpaga
        {
            get
            {
                return this.deudaImpagaField;
            }
            set
            {
                this.deudaImpagaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
        public string DeudaProtestos
        {
            get
            {
                return this.deudaProtestosField;
            }
            set
            {
                this.deudaProtestosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 21)]
        public string DeudaSBS
        {
            get
            {
                return this.deudaSBSField;
            }
            set
            {
                this.deudaSBSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 22)]
        public string TarCtas
        {
            get
            {
                return this.tarCtasField;
            }
            set
            {
                this.tarCtasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 23)]
        public string RepNeg
        {
            get
            {
                return this.repNegField;
            }
            set
            {
                this.repNegField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 24)]
        public string TipoActv
        {
            get
            {
                return this.tipoActvField;
            }
            set
            {
                this.tipoActvField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 25)]
        public string FechIniActv
        {
            get
            {
                return this.fechIniActvField;
            }
            set
            {
                this.fechIniActvField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 26)]
        public string DeudaDirecta
        {
            get
            {
                return this.deudaDirectaField;
            }
            set
            {
                this.deudaDirectaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 27)]
        public string DeudaIndirecta
        {
            get
            {
                return this.deudaIndirectaField;
            }
            set
            {
                this.deudaIndirectaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 28)]
        public string DeudaCastigada
        {
            get
            {
                return this.deudaCastigadaField;
            }
            set
            {
                this.deudaCastigadaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 29)]
        public string LineaCreditoNoUti
        {
            get
            {
                return this.lineaCreditoNoUtiField;
            }
            set
            {
                this.lineaCreditoNoUtiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 30)]
        public string TotalRiesgo
        {
            get
            {
                return this.totalRiesgoField;
            }
            set
            {
                this.totalRiesgoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 31)]
        public string PeorCalificacion
        {
            get
            {
                return this.peorCalificacionField;
            }
            set
            {
                this.peorCalificacionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 32)]
        public string PorCalNormal
        {
            get
            {
                return this.porCalNormalField;
            }
            set
            {
                this.porCalNormalField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "SDT_IC_PosHis.SDT_IC_PosHisItem", Namespace = "PrivadoE2Cliente")]
    public partial class SDT_IC_PosHisSDT_IC_PosHisItem
    {

        private string cnhCPTTDocField;

        private string cnhCPTNroDocField;

        private string cnhFchProField;

        private string cnhSemActField;

        private string cnhSemActDesField;

        private double cnhScoreField;

        private short cnhVarCodField;

        private string cnhVarDesField;

        private sbyte cnhNumEntField;

        private double cnhDeutotField;

        private double cnhPorCalField;

        private sbyte cnhPeoCalField;

        private string cnhPeoCalDesField;

        private double cnhMonVenSBSField;

        private double cnhProRegField;

        private double cnhDocImpField;

        private double cnhDeuTriField;

        private double cnhAFPField;

        private sbyte cnhTarCreField;

        private sbyte cnhCtaCteField;

        private sbyte cnhRepNegField;

        private double cnhDeuDirField;

        private double cnhDeuIndField;

        private double cnhLinCreNoUField;

        private double cnhDeuCastField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string CnhCPTTDoc
        {
            get
            {
                return this.cnhCPTTDocField;
            }
            set
            {
                this.cnhCPTTDocField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string CnhCPTNroDoc
        {
            get
            {
                return this.cnhCPTNroDocField;
            }
            set
            {
                this.cnhCPTNroDocField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public System.String CnhFchPro
        {
            get
            {
                return this.cnhFchProField;
            }
            set
            {
                this.cnhFchProField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string CnhSemAct
        {
            get
            {
                return this.cnhSemActField;
            }
            set
            {
                this.cnhSemActField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string CnhSemActDes
        {
            get
            {
                return this.cnhSemActDesField;
            }
            set
            {
                this.cnhSemActDesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public double CnhScore
        {
            get
            {
                return this.cnhScoreField;
            }
            set
            {
                this.cnhScoreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public short CnhVarCod
        {
            get
            {
                return this.cnhVarCodField;
            }
            set
            {
                this.cnhVarCodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string CnhVarDes
        {
            get
            {
                return this.cnhVarDesField;
            }
            set
            {
                this.cnhVarDesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public sbyte CnhNumEnt
        {
            get
            {
                return this.cnhNumEntField;
            }
            set
            {
                this.cnhNumEntField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public double CnhDeutot
        {
            get
            {
                return this.cnhDeutotField;
            }
            set
            {
                this.cnhDeutotField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public double CnhPorCal
        {
            get
            {
                return this.cnhPorCalField;
            }
            set
            {
                this.cnhPorCalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public sbyte CnhPeoCal
        {
            get
            {
                return this.cnhPeoCalField;
            }
            set
            {
                this.cnhPeoCalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public string CnhPeoCalDes
        {
            get
            {
                return this.cnhPeoCalDesField;
            }
            set
            {
                this.cnhPeoCalDesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public double CnhMonVenSBS
        {
            get
            {
                return this.cnhMonVenSBSField;
            }
            set
            {
                this.cnhMonVenSBSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        public double CnhProReg
        {
            get
            {
                return this.cnhProRegField;
            }
            set
            {
                this.cnhProRegField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
        public double CnhDocImp
        {
            get
            {
                return this.cnhDocImpField;
            }
            set
            {
                this.cnhDocImpField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
        public double CnhDeuTri
        {
            get
            {
                return this.cnhDeuTriField;
            }
            set
            {
                this.cnhDeuTriField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
        public double CnhAFP
        {
            get
            {
                return this.cnhAFPField;
            }
            set
            {
                this.cnhAFPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
        public sbyte CnhTarCre
        {
            get
            {
                return this.cnhTarCreField;
            }
            set
            {
                this.cnhTarCreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
        public sbyte CnhCtaCte
        {
            get
            {
                return this.cnhCtaCteField;
            }
            set
            {
                this.cnhCtaCteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
        public sbyte CnhRepNeg
        {
            get
            {
                return this.cnhRepNegField;
            }
            set
            {
                this.cnhRepNegField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 21)]
        public double CnhDeuDir
        {
            get
            {
                return this.cnhDeuDirField;
            }
            set
            {
                this.cnhDeuDirField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 22)]
        public double CnhDeuInd
        {
            get
            {
                return this.cnhDeuIndField;
            }
            set
            {
                this.cnhDeuIndField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 23)]
        public double CnhLinCreNoU
        {
            get
            {
                return this.cnhLinCreNoUField;
            }
            set
            {
                this.cnhLinCreNoUField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 24)]
        public double CnhDeuCast
        {
            get
            {
                return this.cnhDeuCastField;
            }
            set
            {
                this.cnhDeuCastField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "SDT_IC_RepLeg.SDT_IC_RepLegItem", Namespace = "PrivadoE2Cliente")]
    public partial class SDT_IC_RepLegSDT_IC_RepLegItem
    {

        private string nMDocRLIDField;

        private string eTNumDocRLField;

        private string eTNombreRLField;

        private string eTApePatRLField;

        private string eTApeMatRLField;

        private string eTNomRLField;

        private string eTFchCargotRLField;

        private string eTCargoRLField;

        private string eTSemActRLField;

        private string eTSemPreRLField;

        private string eTSemMitRLField;

        private string eTEstadoRLField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string NMDocRLID
        {
            get
            {
                return this.nMDocRLIDField;
            }
            set
            {
                this.nMDocRLIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string ETNumDocRL
        {
            get
            {
                return this.eTNumDocRLField;
            }
            set
            {
                this.eTNumDocRLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string ETNombreRL
        {
            get
            {
                return this.eTNombreRLField;
            }
            set
            {
                this.eTNombreRLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string ETApePatRL
        {
            get
            {
                return this.eTApePatRLField;
            }
            set
            {
                this.eTApePatRLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string ETApeMatRL
        {
            get
            {
                return this.eTApeMatRLField;
            }
            set
            {
                this.eTApeMatRLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string ETNomRL
        {
            get
            {
                return this.eTNomRLField;
            }
            set
            {
                this.eTNomRLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string ETFchCargotRL
        {
            get
            {
                return this.eTFchCargotRLField;
            }
            set
            {
                this.eTFchCargotRLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string ETCargoRL
        {
            get
            {
                return this.eTCargoRLField;
            }
            set
            {
                this.eTCargoRLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public string ETSemActRL
        {
            get
            {
                return this.eTSemActRLField;
            }
            set
            {
                this.eTSemActRLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public string ETSemPreRL
        {
            get
            {
                return this.eTSemPreRLField;
            }
            set
            {
                this.eTSemPreRLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public string ETSemMitRL
        {
            get
            {
                return this.eTSemMitRLField;
            }
            set
            {
                this.eTSemMitRLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public string ETEstadoRL
        {
            get
            {
                return this.eTEstadoRLField;
            }
            set
            {
                this.eTEstadoRLField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "PrivadoE2Cliente")]
    public partial class SDT_IC_InfGen
    {

        private string dNIField;

        private string fecnacField;

        private string sexoField;

        private string digitoField;

        private string digitoAnteriorField;

        private string rUCField;

        private string razonSocialField;

        private string nomComercialField;

        private string fechaBajaField;

        private string tipoContribField;

        private string codTipoContribField;

        private string estContribField;

        private string codEstContribField;

        private string condContribField;

        private string codCondContribField;

        private string actEconomicaField;

        private string cIIUField;

        private string actEconomica2Field;

        private string cIIU2Field;

        private string actEconomica3Field;

        private string cIIU3Field;

        private string fIniActividadField;

        private string direccionField;

        private string referenciaField;

        private string departamentoField;

        private string provinciaField;

        private string distritoField;

        private string ubigeoField;

        private string fConstitucionField;

        private string actvComercioExteriorField;

        private string codActvComerExtField;

        private string codDependenciaField;

        private string dependenciaField;

        private string folioField;

        private string asientoField;

        private string tomoField;

        private string partidaRegField;

        private string cPatronField;

        private System.Nullable<System.DateTime> fechaUltActField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string DNI
        {
            get
            {
                return this.dNIField;
            }
            set
            {
                this.dNIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string fecnac
        {
            get
            {
                return this.fecnacField;
            }
            set
            {
                this.fecnacField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string sexo
        {
            get
            {
                return this.sexoField;
            }
            set
            {
                this.sexoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string digito
        {
            get
            {
                return this.digitoField;
            }
            set
            {
                this.digitoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string digitoAnterior
        {
            get
            {
                return this.digitoAnteriorField;
            }
            set
            {
                this.digitoAnteriorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string RUC
        {
            get
            {
                return this.rUCField;
            }
            set
            {
                this.rUCField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string RazonSocial
        {
            get
            {
                return this.razonSocialField;
            }
            set
            {
                this.razonSocialField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string NomComercial
        {
            get
            {
                return this.nomComercialField;
            }
            set
            {
                this.nomComercialField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public string FechaBaja
        {
            get
            {
                return this.fechaBajaField;
            }
            set
            {
                this.fechaBajaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public string TipoContrib
        {
            get
            {
                return this.tipoContribField;
            }
            set
            {
                this.tipoContribField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public string CodTipoContrib
        {
            get
            {
                return this.codTipoContribField;
            }
            set
            {
                this.codTipoContribField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public string EstContrib
        {
            get
            {
                return this.estContribField;
            }
            set
            {
                this.estContribField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public string CodEstContrib
        {
            get
            {
                return this.codEstContribField;
            }
            set
            {
                this.codEstContribField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public string CondContrib
        {
            get
            {
                return this.condContribField;
            }
            set
            {
                this.condContribField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        public string CodCondContrib
        {
            get
            {
                return this.codCondContribField;
            }
            set
            {
                this.codCondContribField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
        public string ActEconomica
        {
            get
            {
                return this.actEconomicaField;
            }
            set
            {
                this.actEconomicaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
        public string CIIU
        {
            get
            {
                return this.cIIUField;
            }
            set
            {
                this.cIIUField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
        public string ActEconomica2
        {
            get
            {
                return this.actEconomica2Field;
            }
            set
            {
                this.actEconomica2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
        public string CIIU2
        {
            get
            {
                return this.cIIU2Field;
            }
            set
            {
                this.cIIU2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
        public string ActEconomica3
        {
            get
            {
                return this.actEconomica3Field;
            }
            set
            {
                this.actEconomica3Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
        public string CIIU3
        {
            get
            {
                return this.cIIU3Field;
            }
            set
            {
                this.cIIU3Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 21)]
        public string FIniActividad
        {
            get
            {
                return this.fIniActividadField;
            }
            set
            {
                this.fIniActividadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 22)]
        public string Direccion
        {
            get
            {
                return this.direccionField;
            }
            set
            {
                this.direccionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 23)]
        public string Referencia
        {
            get
            {
                return this.referenciaField;
            }
            set
            {
                this.referenciaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 24)]
        public string Departamento
        {
            get
            {
                return this.departamentoField;
            }
            set
            {
                this.departamentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 25)]
        public string Provincia
        {
            get
            {
                return this.provinciaField;
            }
            set
            {
                this.provinciaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 26)]
        public string Distrito
        {
            get
            {
                return this.distritoField;
            }
            set
            {
                this.distritoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 27)]
        public string Ubigeo
        {
            get
            {
                return this.ubigeoField;
            }
            set
            {
                this.ubigeoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 28)]
        public string FConstitucion
        {
            get
            {
                return this.fConstitucionField;
            }
            set
            {
                this.fConstitucionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 29)]
        public string ActvComercioExterior
        {
            get
            {
                return this.actvComercioExteriorField;
            }
            set
            {
                this.actvComercioExteriorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 30)]
        public string CodActvComerExt
        {
            get
            {
                return this.codActvComerExtField;
            }
            set
            {
                this.codActvComerExtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 31)]
        public string CodDependencia
        {
            get
            {
                return this.codDependenciaField;
            }
            set
            {
                this.codDependenciaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 32)]
        public string Dependencia
        {
            get
            {
                return this.dependenciaField;
            }
            set
            {
                this.dependenciaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 33)]
        public string Folio
        {
            get
            {
                return this.folioField;
            }
            set
            {
                this.folioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 34)]
        public string Asiento
        {
            get
            {
                return this.asientoField;
            }
            set
            {
                this.asientoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 35)]
        public string Tomo
        {
            get
            {
                return this.tomoField;
            }
            set
            {
                this.tomoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 36)]
        public string PartidaReg
        {
            get
            {
                return this.partidaRegField;
            }
            set
            {
                this.partidaRegField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 37)]
        public string CPatron
        {
            get
            {
                return this.cPatronField;
            }
            set
            {
                this.cPatronField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", IsNullable = true, Order = 38)]
        public System.Nullable<System.DateTime> FechaUltAct
        {
            get
            {
                return this.fechaUltActField;
            }
            set
            {
                this.fechaUltActField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "SDT_IC_ResVen.SDT_IC_ResVenItem", Namespace = "PrivadoE2Cliente")]
    public partial class SDT_IC_ResVenSDT_IC_ResVenItem
    {

        private string tipoDocumentoField;

        private string nroDocumentoField;

        private int cantidadDocsField;

        private string fuenteField;

        private string entidadField;

        private double montoField;

        private short cantidadField;

        private string diasVencidosField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string TipoDocumento
        {
            get
            {
                return this.tipoDocumentoField;
            }
            set
            {
                this.tipoDocumentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string NroDocumento
        {
            get
            {
                return this.nroDocumentoField;
            }
            set
            {
                this.nroDocumentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public int CantidadDocs
        {
            get
            {
                return this.cantidadDocsField;
            }
            set
            {
                this.cantidadDocsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string Fuente
        {
            get
            {
                return this.fuenteField;
            }
            set
            {
                this.fuenteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string Entidad
        {
            get
            {
                return this.entidadField;
            }
            set
            {
                this.entidadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public double Monto
        {
            get
            {
                return this.montoField;
            }
            set
            {
                this.montoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public short Cantidad
        {
            get
            {
                return this.cantidadField;
            }
            set
            {
                this.cantidadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string DiasVencidos
        {
            get
            {
                return this.diasVencidosField;
            }
            set
            {
                this.diasVencidosField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "SDT_IC_LinCre.Item", Namespace = "PrivadoE2Cliente")]
    public partial class SDT_IC_LinCreItem
    {

        private string tipDocField;

        private string numDocField;

        private string cnsEntNomRazLNField;

        private string tipoCuentaField;

        private double linCredField;

        private double linNoUtilField;

        private double linUtilField;

        private string porLinUtiField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string TipDoc
        {
            get
            {
                return this.tipDocField;
            }
            set
            {
                this.tipDocField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string NumDoc
        {
            get
            {
                return this.numDocField;
            }
            set
            {
                this.numDocField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string CnsEntNomRazLN
        {
            get
            {
                return this.cnsEntNomRazLNField;
            }
            set
            {
                this.cnsEntNomRazLNField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string TipoCuenta
        {
            get
            {
                return this.tipoCuentaField;
            }
            set
            {
                this.tipoCuentaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public double LinCred
        {
            get
            {
                return this.linCredField;
            }
            set
            {
                this.linCredField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public double LinNoUtil
        {
            get
            {
                return this.linNoUtilField;
            }
            set
            {
                this.linNoUtilField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public double LinUtil
        {
            get
            {
                return this.linUtilField;
            }
            set
            {
                this.linUtilField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string PorLinUti
        {
            get
            {
                return this.porLinUtiField;
            }
            set
            {
                this.porLinUtiField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "SDT_IC_RepSBS.SDT_IC_RepSBSItem", Namespace = "PrivadoE2Cliente")]
    public partial class SDT_IC_RepSBSSDT_IC_RepSBSItem
    {

        private string tipoDocCPTField;

        private string nroDocCPTField;

        private string nomRazSocEntField;

        private string calificacionField;

        private double montoDeudaField;

        private short diasVencidosField;

        private System.Nullable<System.DateTime> fechaReporteField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string TipoDocCPT
        {
            get
            {
                return this.tipoDocCPTField;
            }
            set
            {
                this.tipoDocCPTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string NroDocCPT
        {
            get
            {
                return this.nroDocCPTField;
            }
            set
            {
                this.nroDocCPTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string NomRazSocEnt
        {
            get
            {
                return this.nomRazSocEntField;
            }
            set
            {
                this.nomRazSocEntField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string Calificacion
        {
            get
            {
                return this.calificacionField;
            }
            set
            {
                this.calificacionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public double MontoDeuda
        {
            get
            {
                return this.montoDeudaField;
            }
            set
            {
                this.montoDeudaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public short DiasVencidos
        {
            get
            {
                return this.diasVencidosField;
            }
            set
            {
                this.diasVencidosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", IsNullable = true, Order = 6)]
        public System.Nullable<System.DateTime> FechaReporte
        {
            get
            {
                return this.fechaReporteField;
            }
            set
            {
                this.fechaReporteField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "WS_BSGrupo.Execute", WrapperNamespace = "PrivadoE2Cliente", IsWrapped = true)]
    public partial class ExecuteRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "PrivadoE2Cliente", Order = 0)]
        public string Usuario;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "PrivadoE2Cliente", Order = 1)]
        public string Password;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "PrivadoE2Cliente", Order = 2)]
        public long Servicio;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "PrivadoE2Cliente", Order = 3)]
        public string Tipodoc;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "PrivadoE2Cliente", Order = 4)]
        public string Nrodoc;

        public ExecuteRequest()
        {
        }

        public ExecuteRequest(string Usuario, string Password, long Servicio, string Tipodoc, string Nrodoc)
        {
            this.Usuario = Usuario;
            this.Password = Password;
            this.Servicio = Servicio;
            this.Tipodoc = Tipodoc;
            this.Nrodoc = Nrodoc;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "WS_BSGrupo.ExecuteResponse", WrapperNamespace = "PrivadoE2Cliente", IsWrapped = true)]
    public partial class ExecuteResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "PrivadoE2Cliente", Order = 0)]
        public string Codigows;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "PrivadoE2Cliente", Order = 1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SDT_IC_EstandarItem", IsNullable = false)]
        public SentinelServicio.SDT_IC_EstandarSDT_IC_EstandarItem[] Sdt_bsgrupo_estandar;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "PrivadoE2Cliente", Order = 2)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SDT_IC_RepSBSItem", IsNullable = false)]
        public SentinelServicio.SDT_IC_RepSBSSDT_IC_RepSBSItem[] Sdt_bsgrupo_repsbs;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "PrivadoE2Cliente", Order = 3)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Item", IsNullable = false)]
        public SentinelServicio.SDT_IC_LinCreItem[] Sdt_bsgrupo_lincre;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "PrivadoE2Cliente", Order = 4)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SDT_IC_ResVenItem", IsNullable = false)]
        public SentinelServicio.SDT_IC_ResVenSDT_IC_ResVenItem[] Sdt_bsgrupo_resven;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "PrivadoE2Cliente", Order = 5)]
        public SentinelServicio.SDT_IC_InfGen Sdt_bsgrupo_infgen;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "PrivadoE2Cliente", Order = 6)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SDT_IC_RepLegItem", IsNullable = false)]
        public SentinelServicio.SDT_IC_RepLegSDT_IC_RepLegItem[] Sdt_bsgrupo_repleg;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "PrivadoE2Cliente", Order = 7)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SDT_IC_PosHisItem", IsNullable = false)]
        public SentinelServicio.SDT_IC_PosHisSDT_IC_PosHisItem[] Sdt_bsgrupo_poshis;

        public ExecuteResponse()
        {
        }

        public ExecuteResponse(string Codigows, SentinelServicio.SDT_IC_EstandarSDT_IC_EstandarItem[] Sdt_bsgrupo_estandar, SentinelServicio.SDT_IC_RepSBSSDT_IC_RepSBSItem[] Sdt_bsgrupo_repsbs, SentinelServicio.SDT_IC_LinCreItem[] Sdt_bsgrupo_lincre, SentinelServicio.SDT_IC_ResVenSDT_IC_ResVenItem[] Sdt_bsgrupo_resven, SentinelServicio.SDT_IC_InfGen Sdt_bsgrupo_infgen, SentinelServicio.SDT_IC_RepLegSDT_IC_RepLegItem[] Sdt_bsgrupo_repleg, SentinelServicio.SDT_IC_PosHisSDT_IC_PosHisItem[] Sdt_bsgrupo_poshis)
        {
            this.Codigows = Codigows;
            this.Sdt_bsgrupo_estandar = Sdt_bsgrupo_estandar;
            this.Sdt_bsgrupo_repsbs = Sdt_bsgrupo_repsbs;
            this.Sdt_bsgrupo_lincre = Sdt_bsgrupo_lincre;
            this.Sdt_bsgrupo_resven = Sdt_bsgrupo_resven;
            this.Sdt_bsgrupo_infgen = Sdt_bsgrupo_infgen;
            this.Sdt_bsgrupo_repleg = Sdt_bsgrupo_repleg;
            this.Sdt_bsgrupo_poshis = Sdt_bsgrupo_poshis;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public interface WS_BSGrupoSoapPortChannel : SentinelServicio.WS_BSGrupoSoapPort, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public partial class WS_BSGrupoSoapPortClient : System.ServiceModel.ClientBase<SentinelServicio.WS_BSGrupoSoapPort>, SentinelServicio.WS_BSGrupoSoapPort
    {

        /// <summary>
        /// Implemente este método parcial para configurar el punto de conexión de servicio.
        /// </summary>
        /// <param name="serviceEndpoint">El punto de conexión para configurar</param>
        /// <param name="clientCredentials">Credenciales de cliente</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);

        public WS_BSGrupoSoapPortClient() :
                base(WS_BSGrupoSoapPortClient.GetDefaultBinding(), WS_BSGrupoSoapPortClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.WS_BSGrupoSoapPort.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public WS_BSGrupoSoapPortClient(EndpointConfiguration endpointConfiguration) :
                base(WS_BSGrupoSoapPortClient.GetBindingForEndpoint(endpointConfiguration), WS_BSGrupoSoapPortClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public WS_BSGrupoSoapPortClient(EndpointConfiguration endpointConfiguration, string remoteAddress) :
                base(WS_BSGrupoSoapPortClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public WS_BSGrupoSoapPortClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) :
                base(WS_BSGrupoSoapPortClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public WS_BSGrupoSoapPortClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        public System.Threading.Tasks.Task<SentinelServicio.ExecuteResponse> ExecuteAsync(SentinelServicio.ExecuteRequest request)
        {
            return base.Channel.ExecuteAsync(request);
        }
        SentinelServicio.ExecuteResponse SentinelServicio.WS_BSGrupoSoapPort.Execute(SentinelServicio.ExecuteRequest request)
        {
            return base.Channel.Execute(request);
        }
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }

        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }

        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.WS_BSGrupoSoapPort))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }

        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.WS_BSGrupoSoapPort))
            {
                return new System.ServiceModel.EndpointAddress("https://www2.sentinelperu.com/wsevo2/aws_bsgrupo.aspx");
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }

        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return WS_BSGrupoSoapPortClient.GetBindingForEndpoint(EndpointConfiguration.WS_BSGrupoSoapPort);
        }

        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return WS_BSGrupoSoapPortClient.GetEndpointAddress(EndpointConfiguration.WS_BSGrupoSoapPort);
        }
        public string Execute(string Usuario, string Password, long Servicio, string Tipodoc, string Nrodoc, out SentinelServicio.SDT_IC_EstandarSDT_IC_EstandarItem[] Sdt_bsgrupo_estandar, out SentinelServicio.SDT_IC_RepSBSSDT_IC_RepSBSItem[] Sdt_bsgrupo_repsbs, out SentinelServicio.SDT_IC_LinCreItem[] Sdt_bsgrupo_lincre, out SentinelServicio.SDT_IC_ResVenSDT_IC_ResVenItem[] Sdt_bsgrupo_resven, out SentinelServicio.SDT_IC_InfGen Sdt_bsgrupo_infgen, out SentinelServicio.SDT_IC_RepLegSDT_IC_RepLegItem[] Sdt_bsgrupo_repleg, out SentinelServicio.SDT_IC_PosHisSDT_IC_PosHisItem[] Sdt_bsgrupo_poshis)
        {
            SentinelServicio.ExecuteRequest inValue = new SentinelServicio.ExecuteRequest();
            inValue.Usuario = Usuario;
            inValue.Password = Password;
            inValue.Servicio = Servicio;
            inValue.Tipodoc = Tipodoc;
            inValue.Nrodoc = Nrodoc;
            SentinelServicio.ExecuteResponse retVal = ((SentinelServicio.WS_BSGrupoSoapPort)(this)).Execute(inValue);
            Sdt_bsgrupo_estandar = retVal.Sdt_bsgrupo_estandar;
            Sdt_bsgrupo_repsbs = retVal.Sdt_bsgrupo_repsbs;
            Sdt_bsgrupo_lincre = retVal.Sdt_bsgrupo_lincre;
            Sdt_bsgrupo_resven = retVal.Sdt_bsgrupo_resven;
            Sdt_bsgrupo_infgen = retVal.Sdt_bsgrupo_infgen;
            Sdt_bsgrupo_repleg = retVal.Sdt_bsgrupo_repleg;
            Sdt_bsgrupo_poshis = retVal.Sdt_bsgrupo_poshis;
            return retVal.Codigows;
        }

        public enum EndpointConfiguration
        {

            WS_BSGrupoSoapPort,
        }
    }
}
