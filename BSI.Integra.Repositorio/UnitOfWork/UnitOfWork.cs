using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.Comercial;
using BSI.Integra.Repositorio.Repository.Implementation.Configuracion;
using BSI.Integra.Repositorio.Repository.Implementation.Finanzas.Siigo;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.CampaniaMailingWhatsapp;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.FacebookAudiencia;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.FiltroSegmentoFolder;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.FiltroSegmentoTipoContacto;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.LinkedIn;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.Melissa;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.Messenger;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.Sendingblue;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.WhatsApp;
using BSI.Integra.Repositorio.Repository.Implementation.Operaciones;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Comercial;
using BSI.Integra.Repositorio.Repository.Interface.Configuracion;
using BSI.Integra.Repositorio.Repository.Interface.Finanzas.Siigo;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.FacebookAudiencia;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.FiltroSegmentoFolder;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.FiltroSegmentoTipoContacto;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.LinkedIn;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Melissa;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Messenger;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.WhatsApp;
using BSI.Integra.Repositorio.Repository.Interface.Operacion;
using BSI.Integra.Repositorio.Repository.Interface.Operaciones;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Microsoft.EntityFrameworkCore;

namespace BSI.Integra.Repositorio.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IConnectionFactory _connectionFactory;
        private IntegraDBContext _context;
        private bool _disposed;
        private IDapperRepository _dapperRepository;

        public UnitOfWork(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
        {
            _context = context;
            _connectionFactory = connectionFactory;
            _dapperRepository = dapperRepository;
        }

        public void Commit()
        {       
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Rollback()
        {
            try
            {
                // Descartar los cambios no guardados
                foreach (var entry in _context.ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void Dispose()
        {
            try
            {
                _context.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DetachAll()
        {
            try
            {
                foreach (var entry in _context.ChangeTracker.Entries().ToArray())
                {
                    entry.State = EntityState.Detached;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Repositorios

        private ICustomAuthenticationManagerRepository _ICustomAuthenticationManagerRepository;

        ICustomAuthenticationManagerRepository IUnitOfWork.CustomAuthenticationManagerRepository
        {
            get
            {
                return _ICustomAuthenticationManagerRepository ?? new CustomAuthenticationManagerRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IAreaTrabajoRepository _areaTrabajoRepository;

        IAreaTrabajoRepository IUnitOfWork.AreaTrabajoRepository
        {
            get
            {
                return _areaTrabajoRepository ?? new AreaTrabajoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IAgendaTabRepository _agendaTabRepository;

        IAgendaTabRepository IUnitOfWork.AgendaTabRepository
        {
            get
            {
                return _agendaTabRepository ?? new AgendaTabRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IIntegraAspNetUserRepository _integraAspNetUserRepository;

        IIntegraAspNetUserRepository IUnitOfWork.IntegraAspNetUserRepository
        {
            get
            {
                return _integraAspNetUserRepository ?? new IntegraAspNetUserRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISemaforoFinancieroRepository _semaforoFinancieroRepository;
        ISemaforoFinancieroRepository IUnitOfWork.SemaforoFinancieroRepository
        {
            get
            {
                return _semaforoFinancieroRepository ?? new SemaforoFinancieroRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISemaforoFinancieroDetalleRepository _semaforoFinancieroDetalleRepository;
        ISemaforoFinancieroDetalleRepository IUnitOfWork.SemaforoFinancieroDetalleRepository
        {
            get
            {
                return _semaforoFinancieroDetalleRepository ?? new SemaforoFinancieroDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISemaforoFinancieroVariableRepository _semaforoFinancieroVariableRepository;
        ISemaforoFinancieroVariableRepository IUnitOfWork.SemaforoFinancieroVariableRepository
        {
            get
            {
                return _semaforoFinancieroVariableRepository ?? new SemaforoFinancieroVariableRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISemaforoFinancieroDetalleVariableRepository _semaforoFinancieroDetalleVariableRepository;
        ISemaforoFinancieroDetalleVariableRepository IUnitOfWork.SemaforoFinancieroDetalleVariableRepository
        {
            get
            {
                return _semaforoFinancieroDetalleVariableRepository ?? new SemaforoFinancieroDetalleVariableRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralProblemaRepository _programaGeneralProblemaRepository;
        IProgramaGeneralProblemaRepository IUnitOfWork.ProgramaGeneralProblemaRepository
        {
            get
            {
                return _programaGeneralProblemaRepository ?? new ProgramaGeneralProblemaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPersonalRepository _personalRepository;

        IPersonalRepository IUnitOfWork.PersonalRepository
        {
            get
            {
                return _personalRepository ?? new PersonalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPGeneralRepository _pGeneralRepository;

        IPGeneralRepository IUnitOfWork.PGeneralRepository
        {
            get
            {
                return _pGeneralRepository ?? new PGeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IEstadoOcurrenciaRepository _estadoOcurrenciaRepository;

        IEstadoOcurrenciaRepository IUnitOfWork.EstadoOcurrenciaRepository
        {
            get
            {
                return _estadoOcurrenciaRepository ?? new EstadoOcurrenciaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFaseOportunidadRepository _faseOportunidadRepository;

        IFaseOportunidadRepository IUnitOfWork.FaseOportunidadRepository
        {
            get
            {
                return _faseOportunidadRepository ?? new FaseOportunidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITableroComercialCategoriaAsesorRepository _tableroComercialCategoriaAsesorRepository;
        ITableroComercialCategoriaAsesorRepository IUnitOfWork.TableroComercialCategoriaAsesorRepository
        {
            get
            {
                return _tableroComercialCategoriaAsesorRepository ?? new TableroComercialCategoriaAsesorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IRecordAreaComercialRepository _recordAreaComercialRepository;
        IRecordAreaComercialRepository IUnitOfWork.RecordAreaComercialRepository
        {
            get
            {
                return _recordAreaComercialRepository ?? new RecordAreaComercialRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoDatoRepository _tipoDatoRepository;

        ITipoDatoRepository IUnitOfWork.TipoDatoRepository
        {
            get
            {
                return _tipoDatoRepository ?? new TipoDatoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOrigenRepository _origenRepository;

        IOrigenRepository IUnitOfWork.OrigenRepository
        {
            get
            {
                return _origenRepository ?? new OrigenRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IAlumnoRepository _alumnoRepository;

        IAlumnoRepository IUnitOfWork.AlumnoRepository
        {
            get
            {
                return _alumnoRepository ?? new AlumnoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProbabilidadRegistroPwRepository _probabilidadRegistroPwRepository;

        IProbabilidadRegistroPwRepository IUnitOfWork.ProbabilidadRegistroPwRepository
        {
            get
            {
                return _probabilidadRegistroPwRepository ?? new ProbabilidadRegistroPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICategoriaOrigenRepository _categoriaOrigenPwRepository;

        ICategoriaOrigenRepository IUnitOfWork.CategoriaOrigenRepository
        {
            get
            {
                return _categoriaOrigenPwRepository ?? new CategoriaOrigenRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IEmpresaRepository _empresaRepository;

        IEmpresaRepository IUnitOfWork.EmpresaRepository
        {
            get
            {
                return _empresaRepository ?? new EmpresaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IGmailClienteRepository _gmailClienteRepository;

        IGmailClienteRepository IUnitOfWork.GmailClienteRepository
        {
            get
            {
                return _gmailClienteRepository ?? new GmailClienteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICiudadRepository _ciudadRepository;

        ICiudadRepository IUnitOfWork.CiudadRepository
        {
            get
            {
                return _ciudadRepository ?? new CiudadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPlantillaRepository _plantillaRepository;

        IPlantillaRepository IUnitOfWork.PlantillaRepository
        {
            get
            {
                return _plantillaRepository ?? new PlantillaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IGmailCorreoRepository _gmailCorreoRepository;

        IGmailCorreoRepository IUnitOfWork.GmailCorreoRepository
        {
            get
            {
                return _gmailCorreoRepository ?? new GmailCorreoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IWhatsAppMensajeEnviadoRepository _whatsAppMensajeEnviadoRepository;

        IWhatsAppMensajeEnviadoRepository IUnitOfWork.WhatsAppMensajeEnviadoRepository
        {
            get
            {
                return _whatsAppMensajeEnviadoRepository ?? new WhatsAppMensajeEnviadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICentroCostoRepository _centroCostoRepository;

        ICentroCostoRepository IUnitOfWork.CentroCostoRepository
        {
            get
            {
                return _centroCostoRepository ?? new CentroCostoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMatriculaCabeceraDatosCertificadoMensajeRepository _matriculaCabeceraDatosCertificadoMensajeRepository;

        IMatriculaCabeceraDatosCertificadoMensajeRepository IUnitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository
        {
            get
            {
                return _matriculaCabeceraDatosCertificadoMensajeRepository ?? new MatriculaCabeceraDatosCertificadoMensajeRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPlantillaClaveValorRepository _plantillaClaveValorRepository;

        IPlantillaClaveValorRepository IUnitOfWork.PlantillaClaveValorRepository
        {
            get
            {
                return _plantillaClaveValorRepository ?? new PlantillaClaveValorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOportunidadLogRepository _oportunidadLogRepository;

        IOportunidadLogRepository IUnitOfWork.OportunidadLogRepository
        {
            get
            {
                return _oportunidadLogRepository ?? new OportunidadLogRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMatriculaCabeceraRepository _matriculaCabeceraRepository;

        IMatriculaCabeceraRepository IUnitOfWork.MatriculaCabeceraRepository
        {
            get
            {
                return _matriculaCabeceraRepository ?? new MatriculaCabeceraRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IIndustriaRepository _industriaRepository;

        IIndustriaRepository IUnitOfWork.IndustriaRepository
        {
            get
            {
                return _industriaRepository ?? new IndustriaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISentinelRepository _sentinelRepository;

        ISentinelRepository IUnitOfWork.SentinelRepository
        {
            get
            {
                return _sentinelRepository ?? new SentinelRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOportunidadRepository _oportunidadRepository;

        IOportunidadRepository IUnitOfWork.OportunidadRepository
        {
            get
            {
                return _oportunidadRepository ?? new OportunidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISentinelRepLegItemRepository _sentinelRepLegItemRepository;

        ISentinelRepLegItemRepository IUnitOfWork.SentinelRepLegItemRepository
        {
            get
            {
                return _sentinelRepLegItemRepository ?? new SentinelRepLegItemRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISentinelSdtEstandarItemRepository _sentinelSdtEstandarItemRepository;

        ISentinelSdtEstandarItemRepository IUnitOfWork.SentinelSdtEstandarItemRepository
        {
            get
            {
                return _sentinelSdtEstandarItemRepository ?? new SentinelSdtEstandarItemRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISentinelSdtInfGenRepository _sentinelSdtInfGenRepository;

        ISentinelSdtInfGenRepository IUnitOfWork.SentinelSdtInfGenRepository
        {
            get
            {
                return _sentinelSdtInfGenRepository ?? new SentinelSdtInfGenRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISentinelSdtLincreItemRepository _sentinelSdtLincreItemRepository;

        ISentinelSdtLincreItemRepository IUnitOfWork.SentinelSdtLincreItemRepository
        {
            get
            {
                return _sentinelSdtLincreItemRepository ?? new SentinelSdtLincreItemRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISentinelSdtPoshisItemRepository _sentinelSdtPoshisItemRepository;

        ISentinelSdtPoshisItemRepository IUnitOfWork.SentinelSdtPoshisItemRepository
        {
            get
            {
                return _sentinelSdtPoshisItemRepository ?? new SentinelSdtPoshisItemRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISentinelSdtRepSbsitemRepository _sentinelSdtRepSbsitemRepository;

        ISentinelSdtRepSbsitemRepository IUnitOfWork.SentinelSdtRepSbsitemRepository
        {
            get
            {
                return _sentinelSdtRepSbsitemRepository ?? new SentinelSdtRepSbsitemRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISentinelSdtResVenItemRepository _sentinelSdtResVenItemRepository;

        ISentinelSdtResVenItemRepository IUnitOfWork.SentinelSdtResVenItemRepository
        {
            get
            {
                return _sentinelSdtResVenItemRepository ?? new SentinelSdtResVenItemRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISentinelSueldoIndividualRepository _sentinelSueldoIndividualRepository;

        ISentinelSueldoIndividualRepository IUnitOfWork.SentinelSueldoIndividualRepository
        {
            get
            {
                return _sentinelSueldoIndividualRepository ?? new SentinelSueldoIndividualRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISentinelSueldoPorIndustriaRepository _sentinelSueldoPorIndustriaRepository;

        ISentinelSueldoPorIndustriaRepository IUnitOfWork.SentinelSueldoPorIndustriaRepository
        {
            get
            {
                return _sentinelSueldoPorIndustriaRepository ?? new SentinelSueldoPorIndustriaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISentinelSueldoPorIndustriaDataDinamicoRepository _sentinelSueldoPorIndustriaDataDinamicoRepository;

        ISentinelSueldoPorIndustriaDataDinamicoRepository IUnitOfWork.SentinelSueldoPorIndustriaDataDinamicoRepository
        {
            get
            {
                return _sentinelSueldoPorIndustriaDataDinamicoRepository ?? new SentinelSueldoPorIndustriaDataDinamicoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISentinelSueldoPorIndustriaDataTotalRepository _sentinelSueldoPorIndustriaDataTotalRepository;

        ISentinelSueldoPorIndustriaDataTotalRepository IUnitOfWork.SentinelSueldoPorIndustriaDataTotalRepository
        {
            get
            {
                return _sentinelSueldoPorIndustriaDataTotalRepository ?? new SentinelSueldoPorIndustriaDataTotalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMonedaRepository _monedaRepository;

        IMonedaRepository IUnitOfWork.MonedaRepository
        {
            get
            {
                return _monedaRepository ?? new MonedaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IAlumnoCuponRegistroRepository _alumnoCuponRegistroRepository;

        IAlumnoCuponRegistroRepository IUnitOfWork.AlumnoCuponRegistroRepository
        {
            get
            {
                return _alumnoCuponRegistroRepository ?? new AlumnoCuponRegistroRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITiempoCapacitacionRepository _tiempoCapacitacionRepository;

        ITiempoCapacitacionRepository IUnitOfWork.TiempoCapacitacionRepository
        {
            get
            {
                return _tiempoCapacitacionRepository ?? new TiempoCapacitacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICajaRepository _cajaRepository;

        ICajaRepository IUnitOfWork.CajaRepository
        {
            get
            {
                return _cajaRepository ?? new CajaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoImpuestoRepository _tipoImpuestoRepository;

        ITipoImpuestoRepository IUnitOfWork.TipoImpuestoRepository
        {
            get
            {
                return _tipoImpuestoRepository ?? new TipoImpuestoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IRetencionRepository _retencionRepository;

        IRetencionRepository IUnitOfWork.RetencionRepository
        {
            get
            {
                return _retencionRepository ?? new RetencionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoServicioRepository _tipoServicioRepository;
        ITipoServicioRepository IUnitOfWork.TipoServicioRepository
        {
            get
            {
                return _tipoServicioRepository ?? new TipoServicioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEntidadFinancieraRepository _entidadFinancieraRepository;
        IEntidadFinancieraRepository IUnitOfWork.EntidadFinancieraRepository
        {
            get
            {
                return _entidadFinancieraRepository ?? new EntidadFinancieraRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICuentaCorrienteRepository _cuentaCorrienteRepository;
        ICuentaCorrienteRepository IUnitOfWork.CuentaCorrienteRepository
        {
            get
            {
                return _cuentaCorrienteRepository ?? new CuentaCorrienteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICuentaContablePadreRepository _cuentaContablePadreRepository;
        ICuentaContablePadreRepository IUnitOfWork.CuentaContablePadreRepository
        {
            get
            {
                return _cuentaContablePadreRepository ?? new CuentaContablePadreRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoCuentaBancoRepository _tipoCuentaBancoRepository;
        ITipoCuentaBancoRepository IUnitOfWork.TipoCuentaBancoRepository
        {
            get
            {
                return _tipoCuentaBancoRepository ?? new TipoCuentaBancoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEmpresaAutorizadaRepository _empresaAutorizadaRepository;
        IEmpresaAutorizadaRepository IUnitOfWork.EmpresaAutorizadaRepository
        {
            get
            {
                return _empresaAutorizadaRepository ?? new EmpresaAutorizadaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPlanContableRepository _planContableRepository;
        IPlanContableRepository IUnitOfWork.PlanContableRepository
        {
            get
            {
                return _planContableRepository ?? new PlanContableRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProductoRepository _productoRepository;
        IProductoRepository IUnitOfWork.ProductoRepository
        {
            get
            {
                return _productoRepository ?? new ProductoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDetraccionRepository _detraccionRepository;
        IDetraccionRepository IUnitOfWork.DetraccionRepository
        {
            get
            {
                return _detraccionRepository ?? new DetraccionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProveedorCalificacionRepository _proveedorCalificacionRepository;
        IProveedorCalificacionRepository IUnitOfWork.ProveedorCalificacionRepository
        {
            get
            {
                return _proveedorCalificacionRepository ?? new ProveedorCalificacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPrestacionRegistroRepository _prestacionRegistroRepository;
        IPrestacionRegistroRepository IUnitOfWork.PrestacionRegistroRepository
        {
            get
            {
                return _prestacionRegistroRepository ?? new PrestacionRegistroRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProveedorTipoServicioRepository _proveedorTipoServicioRepository;
        IProveedorTipoServicioRepository IUnitOfWork.ProveedorTipoServicioRepository
        {
            get
            {
                return _proveedorTipoServicioRepository ?? new ProveedorTipoServicioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProveedorCuentaBancoRepository _proveedorCuentaBancoRepository;
        IProveedorCuentaBancoRepository IUnitOfWork.ProveedorCuentaBancoRepository
        {
            get
            {
                return _proveedorCuentaBancoRepository ?? new ProveedorCuentaBancoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProveedorSubCriterioCalificacionRepository _proveedorSubCriterioCalificacionRepository;
        IProveedorSubCriterioCalificacionRepository IUnitOfWork.ProveedorSubCriterioCalificacionRepository
        {
            get
            {
                return _proveedorSubCriterioCalificacionRepository ?? new ProveedorSubCriterioCalificacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDocumentoIdentidadRepository _documentoIdentidadRepository;
        IDocumentoIdentidadRepository IUnitOfWork.DocumentoIdentidadRepository
        {
            get
            {
                return _documentoIdentidadRepository ?? new DocumentoIdentidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProveedorCriterioCalificacionRepository _ProveedorCriterioCalificacionRepository;
        IProveedorCriterioCalificacionRepository IUnitOfWork.ProveedorCriterioCalificacionRepository
        {
            get
            {
                return _ProveedorCriterioCalificacionRepository ?? new ProveedorCriterioCalificacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoContribuyenteRepository _tipoContribuyenteRepository;
        ITipoContribuyenteRepository IUnitOfWork.TipoContribuyenteRepository
        {
            get
            {
                return _tipoContribuyenteRepository ?? new TipoContribuyenteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProductoPresentacionRepository _productoPresentacionRepository;
        IProductoPresentacionRepository IUnitOfWork.ProductoPresentacionRepository
        {
            get
            {
                return _productoPresentacionRepository ?? new ProductoPresentacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IHistoricoProductoProveedorRepository _historicoProductoProveedorRepository;
        IHistoricoProductoProveedorRepository IUnitOfWork.HistoricoProductoProveedorRepository
        {
            get
            {
                return _historicoProductoProveedorRepository ?? new HistoricoProductoProveedorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private ICondicionPagoRepository _condicionPagoRepository;

        ICondicionPagoRepository IUnitOfWork.CondicionPagoRepository
        {
            get
            {
                return _condicionPagoRepository ?? new CondicionPagoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICondicionTipoPagoRepository _condicionTipoPagoRepository;

        ICondicionTipoPagoRepository IUnitOfWork.CondicionTipoPagoRepository
        {
            get
            {
                return _condicionTipoPagoRepository ?? new CondicionTipoPagoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoCambioMonedumRepository _tipoCambioMonedumRepository;

        ITipoCambioMonedumRepository IUnitOfWork.TipoCambioMonedumRepository
        {
            get
            {
                return _tipoCambioMonedumRepository ?? new TipoCambioMonedumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoCambioRepository _tipoCambioRepository;

        ITipoCambioRepository IUnitOfWork.TipoCambioRepository
        {
            get
            {
                return _tipoCambioRepository ?? new TipoCambioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoCambioColRepository _tipoCambioColRepository;

        ITipoCambioColRepository IUnitOfWork.TipoCambioColRepository
        {
            get
            {
                return _tipoCambioColRepository ?? new TipoCambioColRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPeriodoRepository _periodoRepository;
        IPeriodoRepository IUnitOfWork.PeriodoRepository
        {
            get
            {
                return _periodoRepository ?? new PeriodoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProveedorRepository _proveedorRepository;
        IProveedorRepository IUnitOfWork.ProveedorRepository
        {
            get
            {
                return _proveedorRepository ?? new ProveedorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralBeneficioArgumentoRepository _programaGeneralBeneficioArgumentoRepository;

        IProgramaGeneralBeneficioArgumentoRepository IUnitOfWork.ProgramaGeneralBeneficioArgumentoRepository
        {
            get
            {
                return _programaGeneralBeneficioArgumentoRepository ?? new ProgramaGeneralBeneficioArgumentoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMandrilRepository _mandrilRepository;

        IMandrilRepository IUnitOfWork.MandrilRepository
        {
            get
            {
                return _mandrilRepository ?? new MandrilRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICompetidorRepository _competidorRepository;

        ICompetidorRepository IUnitOfWork.CompetidorRepository
        {
            get
            {
                return _competidorRepository ?? new CompetidorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IDetalleOportunidadCompetidorRepository _detalleOportunidadCompetidorRepository;

        IDetalleOportunidadCompetidorRepository IUnitOfWork.DetalleOportunidadCompetidorRepository
        {
            get
            {
                return _detalleOportunidadCompetidorRepository ?? new DetalleOportunidadCompetidorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMontoPagoCronogramaRepository _montoPagoCronogramaRepository;

        IMontoPagoCronogramaRepository IUnitOfWork.MontoPagoCronogramaRepository
        {
            get
            {
                return _montoPagoCronogramaRepository ?? new MontoPagoCronogramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMontoPagoCronogramaDetalleRepository _montoPagoCronogramaDetalleRepository;

        IMontoPagoCronogramaDetalleRepository IUnitOfWork.MontoPagoCronogramaDetalleRepository
        {
            get
            {
                return _montoPagoCronogramaDetalleRepository ?? new MontoPagoCronogramaDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPasarelaPagoPwRepository _pasarelaPagoPwRepository;

        IPasarelaPagoPwRepository IUnitOfWork.PasarelaPagoPwRepository
        {
            get
            {
                return _pasarelaPagoPwRepository ?? new PasarelaPagoPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPEspecificoRepository _pEspecificoRepository;

        IPEspecificoRepository IUnitOfWork.PEspecificoRepository
        {
            get
            {
                return _pEspecificoRepository ?? new PEspecificoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPaisRepository _paisRepository;

        IPaisRepository IUnitOfWork.PaisRepository
        {
            get
            {
                return _paisRepository ?? new PaisRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISeguimientoAlumnoComentarioRepository _SeguimientoAlumnoComentarioRepository;
        ISeguimientoAlumnoComentarioRepository IUnitOfWork.SeguimientoAlumnoComentarioRepository
        {
            get
            {
                return _SeguimientoAlumnoComentarioRepository ?? new SeguimientoAlumnoComentarioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISeguimientoAlumnoDetalleRepository _SeguimientoAlumnoDetalleRepository;
        ISeguimientoAlumnoDetalleRepository IUnitOfWork.SeguimientoAlumnoDetalleRepository
        {
            get
            {
                return _SeguimientoAlumnoDetalleRepository ?? new SeguimientoAlumnoDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IDocumentacionComercialPwRepository _documentacionComercialPwRepository;

        IDocumentacionComercialPwRepository IUnitOfWork.DocumentacionComercialPwRepository
        {
            get
            {
                return _documentacionComercialPwRepository ?? new DocumentacionComercialPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IDocumentoSeccionPwRepository _documentoSeccionPwRepository;

        IDocumentoSeccionPwRepository IUnitOfWork.DocumentoSeccionPwRepository
        {
            get
            {
                return _documentoSeccionPwRepository ?? new DocumentoSeccionPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICronogramaPagoDetalleFinalRepository _cronogramaPagoDetalleFinalRepository;

        ICronogramaPagoDetalleFinalRepository IUnitOfWork.CronogramaPagoDetalleFinalRepository
        {
            get
            {
                return _cronogramaPagoDetalleFinalRepository ?? new CronogramaPagoDetalleFinalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPgeneralAsubPgeneralRepository _pgeneralAsubPgeneralRepository;
        IPgeneralAsubPgeneralRepository IUnitOfWork.PgeneralAsubPgeneralRepository
        {
            get
            {
                return _pgeneralAsubPgeneralRepository ?? new PgeneralAsubPgeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMatriculaCabeceraBeneficiosRepository _matriculaCabeceraBeneficiosRepository;

        IMatriculaCabeceraBeneficiosRepository IUnitOfWork.MatriculaCabeceraBeneficiosRepository
        {
            get
            {
                return _matriculaCabeceraBeneficiosRepository ?? new MatriculaCabeceraBeneficiosRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IConfiguracionBeneficioProgramaGeneralRepository _configuracionBeneficioProgramaGeneralRepository;

        IConfiguracionBeneficioProgramaGeneralRepository IUnitOfWork.ConfiguracionBeneficioProgramaGeneralRepository
        {
            get
            {
                return _configuracionBeneficioProgramaGeneralRepository ?? new ConfiguracionBeneficioProgramaGeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPreguntaFrecuentePGeneralRepository _preguntaFrecuentePGeneralRepository;

        IPreguntaFrecuentePGeneralRepository IUnitOfWork.PreguntaFrecuentePGeneralRepository
        {
            get
            {
                return _preguntaFrecuentePGeneralRepository ?? new PreguntaFrecuentePGeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOcurrenciaActividadAlternoRepository _ocurrenciaActividadAlternoRepository;

        IOcurrenciaActividadAlternoRepository IUnitOfWork.OcurrenciaActividadAlternoRepository
        {
            get
            {
                return _ocurrenciaActividadAlternoRepository ?? new OcurrenciaActividadAlternoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IAlumnoLogRepository _alumnoLogRepository;

        IAlumnoLogRepository IUnitOfWork.AlumnoLogRepository
        {
            get
            {
                return _alumnoLogRepository ?? new AlumnoLogRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IDocumentoLegalRepository _documentoLegalRepository;

        IDocumentoLegalRepository IUnitOfWork.DocumentoLegalRepository
        {
            get
            {
                return _documentoLegalRepository ?? new DocumentoLegalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPersonalAreaTrabajoRepository _personalAreaTrabajoRepository;

        IPersonalAreaTrabajoRepository IUnitOfWork.PersonalAreaTrabajoRepository
        {
            get
            {
                return _personalAreaTrabajoRepository ?? new PersonalAreaTrabajoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICargoRepository _cargoRepository;

        ICargoRepository IUnitOfWork.CargoRepository
        {
            get
            {
                return _cargoRepository ?? new CargoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOportunidadMaximaPorCategoriaRepository _oportunidadMaximaPorCategoriaRepository;

        IOportunidadMaximaPorCategoriaRepository IUnitOfWork.OportunidadMaximaPorCategoriaRepository
        {
            get
            {
                return _oportunidadMaximaPorCategoriaRepository ?? new OportunidadMaximaPorCategoriaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMaterialVersionRepository _materialVersionRepository;

        IMaterialVersionRepository IUnitOfWork.MaterialVersionRepository
        {
            get
            {
                return _materialVersionRepository ?? new MaterialVersionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IDocumentoOportunidadRepository _documentoOportunidadRepository;

        IDocumentoOportunidadRepository IUnitOfWork.DocumentoOportunidadRepository
        {
            get
            {
                return _documentoOportunidadRepository ?? new DocumentoOportunidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IAreaFormacionRepository _areaFormacionRepository;

        IAreaFormacionRepository IUnitOfWork.AreaFormacionRepository
        {
            get
            {
                return _areaFormacionRepository ?? new AreaFormacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IAreaCapacitacionRepository _areaCapacitacionRepository;

        IAreaCapacitacionRepository IUnitOfWork.AreaCapacitacionRepository
        {
            get
            {
                return _areaCapacitacionRepository ?? new AreaCapacitacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISubAreaCapacitacionRepository _subAreaCapacitacionRepository;

        ISubAreaCapacitacionRepository IUnitOfWork.SubAreaCapacitacionRepository
        {
            get
            {
                return _subAreaCapacitacionRepository ?? new SubAreaCapacitacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPGeneralTipoDescuentoRepository _pGeneralTipoDescuentoRepository;

        IPGeneralTipoDescuentoRepository IUnitOfWork.PGeneralTipoDescuentoRepository
        {
            get
            {
                return _pGeneralTipoDescuentoRepository ?? new PGeneralTipoDescuentoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IDocumentoOportunidadTipoRepository _documentoOportunidadTipoRepository;

        IDocumentoOportunidadTipoRepository IUnitOfWork.DocumentoOportunidadTipoRepository
        {
            get
            {
                return _documentoOportunidadTipoRepository ?? new DocumentoOportunidadTipoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPlantillaBaseRepository _plantillaBaseRepository;

        IPlantillaBaseRepository IUnitOfWork.PlantillaBaseRepository
        {
            get
            {
                return _plantillaBaseRepository ?? new PlantillaBaseRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IDocumentoLegalAreaTrabajoRepository _documentoLegalAreaTrabajoRepository;

        IDocumentoLegalAreaTrabajoRepository IUnitOfWork.DocumentoLegalAreaTrabajoRepository
        {
            get
            {
                return _documentoLegalAreaTrabajoRepository ?? new DocumentoLegalAreaTrabajoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IDocumentoLegalPaisRepository _documentoLegalPaisRepository;

        IDocumentoLegalPaisRepository IUnitOfWork.DocumentoLegalPaisRepository
        {
            get
            {
                return _documentoLegalPaisRepository ?? new DocumentoLegalPaisRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IWhatsAppPlantillaPorOcurrenciaActividadRepository _whatsAppPlantillaPorOcurrenciaActividadRepository;

        IWhatsAppPlantillaPorOcurrenciaActividadRepository IUnitOfWork.WhatsAppPlantillaPorOcurrenciaActividadRepository
        {
            get
            {
                return _whatsAppPlantillaPorOcurrenciaActividadRepository ?? new WhatsAppPlantillaPorOcurrenciaActividadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IWhatsAppEstadoMensajeEnviadoRepository _whatsAppEstadoMensajeEnviadoRepository;

        IWhatsAppEstadoMensajeEnviadoRepository IUnitOfWork.WhatsAppEstadoMensajeEnviadoRepository
        {
            get
            {
                return _whatsAppEstadoMensajeEnviadoRepository ?? new WhatsAppEstadoMensajeEnviadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IChatDetalleIntegraRepository _chatDetalleIntegraRepository;

        IChatDetalleIntegraRepository IUnitOfWork.ChatDetalleIntegraRepository
        {
            get
            {
                return _chatDetalleIntegraRepository ?? new ChatDetalleIntegraRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IChatDetalleIntegraArchivoRepository _chatDetalleIntegraArchivoRepository;

        IChatDetalleIntegraArchivoRepository IUnitOfWork.ChatDetalleIntegraArchivoRepository
        {
            get
            {
                return _chatDetalleIntegraArchivoRepository ?? new ChatDetalleIntegraArchivoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IEstadoMatriculaRepository _estadoMatriculaRepository;

        IEstadoMatriculaRepository IUnitOfWork.EstadoMatriculaRepository
        {
            get
            {
                return _estadoMatriculaRepository ?? new EstadoMatriculaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISubEstadoMatriculaRepository _subEstadoMatriculaRepository;

        ISubEstadoMatriculaRepository IUnitOfWork.SubEstadoMatriculaRepository
        {
            get
            {
                return _subEstadoMatriculaRepository ?? new SubEstadoMatriculaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralCertificacionRepository _programaGeneralCertificacionRepository;

        IProgramaGeneralCertificacionRepository IUnitOfWork.ProgramaGeneralCertificacionRepository
        {
            get
            {
                return _programaGeneralCertificacionRepository ?? new ProgramaGeneralCertificacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralCertificacionArgumentoRepository _programaGeneralCertificacionArgumentoRepository;

        IProgramaGeneralCertificacionArgumentoRepository IUnitOfWork.ProgramaGeneralCertificacionArgumentoRepository
        {
            get
            {
                return _programaGeneralCertificacionArgumentoRepository ?? new ProgramaGeneralCertificacionArgumentoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralMotivacionRepository _programaGeneralMotivacionRepository;

        IProgramaGeneralMotivacionRepository IUnitOfWork.ProgramaGeneralMotivacionRepository
        {
            get
            {
                return _programaGeneralMotivacionRepository ?? new ProgramaGeneralMotivacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralMotivacionArgumentoRepository _programaGeneralMotivacionArgumentoRepository;

        IProgramaGeneralMotivacionArgumentoRepository IUnitOfWork.ProgramaGeneralMotivacionArgumentoRepository
        {
            get
            {
                return _programaGeneralMotivacionArgumentoRepository ?? new ProgramaGeneralMotivacionArgumentoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IClasificacionPersonaRepository _clasificacionPersonaRepository;

        IClasificacionPersonaRepository IUnitOfWork.ClasificacionPersonaRepository
        {
            get
            {
                return _clasificacionPersonaRepository ?? new ClasificacionPersonaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralProblemaDetalleSolucionRepository _programaGeneralProblemaDetalleSolucionRepository;

        IProgramaGeneralProblemaDetalleSolucionRepository IUnitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository
        {
            get
            {
                return _programaGeneralProblemaDetalleSolucionRepository ?? new ProgramaGeneralProblemaDetalleSolucionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralPrerequisitoRepository _programaGeneralPrerequisitoRepository;

        IProgramaGeneralPrerequisitoRepository IUnitOfWork.ProgramaGeneralPrerequisitoRepository
        {
            get
            {
                return _programaGeneralPrerequisitoRepository ?? new ProgramaGeneralPrerequisitoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralBeneficioRepository _programaGeneralBeneficioRepository;

        IProgramaGeneralBeneficioRepository IUnitOfWork.ProgramaGeneralBeneficioRepository
        {
            get
            {
                return _programaGeneralBeneficioRepository ?? new ProgramaGeneralBeneficioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOportunidadCompetidorRepository _oportunidadCompetidorRepository;

        IOportunidadCompetidorRepository IUnitOfWork.OportunidadCompetidorRepository
        {
            get
            {
                return _oportunidadCompetidorRepository ?? new OportunidadCompetidorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoDescuentoRepository _tipoDescuentoRepository;

        ITipoDescuentoRepository IUnitOfWork.TipoDescuentoRepository
        {
            get
            {
                return _tipoDescuentoRepository ?? new TipoDescuentoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoDescuentoSolicitudRepository _tipoDescuentoSolicitudRepository;

        ITipoDescuentoSolicitudRepository IUnitOfWork.TipoDescuentoSolicitudRepository
        {
            get
            {
                return _tipoDescuentoSolicitudRepository ?? new TipoDescuentoSolicitudRepository(_connectionFactory, _dapperRepository);
            }
        }

        private IMontoPagoRepository _montoPagoRepository;

        IMontoPagoRepository IUnitOfWork.MontoPagoRepository
        {
            get
            {
                return _montoPagoRepository ?? new MontoPagoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOperadorComparacionRepository _operadorComparacionRepository;

        IOperadorComparacionRepository IUnitOfWork.OperadorComparacionRepository
        {
            get
            {
                return _operadorComparacionRepository ?? new OperadorComparacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IConfiguracionFijaRepository _configuracionFijaRepository;

        IConfiguracionFijaRepository IUnitOfWork.ConfiguracionFijaRepository
        {
            get
            {
                return _configuracionFijaRepository ?? new ConfiguracionFijaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITableroComercialUnidadRepository _tableroComercialUnidadRepository;

        ITableroComercialUnidadRepository IUnitOfWork.TableroComercialUnidadRepository
        {
            get
            {
                return _tableroComercialUnidadRepository ?? new TableroComercialUnidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IDocumentoAgendaRepository _documentoAgendaRepository;

        IDocumentoAgendaRepository IUnitOfWork.DocumentoAgendaRepository
        {
            get
            {
                return _documentoAgendaRepository ?? new DocumentoAgendaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMedioPagoMatriculaCronogramaRepository _medioPagoMatriculaCronogramaRepository;

        IMedioPagoMatriculaCronogramaRepository IUnitOfWork.MedioPagoMatriculaCronogramaRepository
        {
            get
            {
                return _medioPagoMatriculaCronogramaRepository ?? new MedioPagoMatriculaCronogramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralProblemaModalidadRepository _programaGeneralProblemaModalidadRepository;

        IProgramaGeneralProblemaModalidadRepository IUnitOfWork.ProgramaGeneralProblemaModalidadRepository
        {
            get
            {
                return _programaGeneralProblemaModalidadRepository ?? new ProgramaGeneralProblemaModalidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICronogramaPagoRepository _cronogramaPagoRepository;

        ICronogramaPagoRepository IUnitOfWork.CronogramaPagoRepository
        {
            get
            {
                return _cronogramaPagoRepository ?? new CronogramaPagoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IModalidadCursoRepository _modalidadCursoRepository;
        IModalidadCursoRepository IUnitOfWork.ModalidadCursoRepository
        {
            get
            {
                return _modalidadCursoRepository ?? new ModalidadCursoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IPlantillaPwRepository _plantillaPwRepository;

        IPlantillaPwRepository IUnitOfWork.PlantillaPwRepository
        {
            get
            {
                return _plantillaPwRepository ?? new PlantillaPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IVersionProgramaRepository _versionProgramaRepository;

        IVersionProgramaRepository IUnitOfWork.VersionProgramaRepository
        {
            get
            {
                return _versionProgramaRepository ?? new VersionProgramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPGeneralDocumentoPwRepository _pGeneralDocumentoPwRepository;

        IPGeneralDocumentoPwRepository IUnitOfWork.PGeneralDocumentoPwRepository
        {
            get
            {
                return _pGeneralDocumentoPwRepository ?? new PGeneralDocumentoPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IEtiquetaRepository _etiquetaRepository;

        IEtiquetaRepository IUnitOfWork.EtiquetaRepository
        {
            get
            {
                return _etiquetaRepository ?? new EtiquetaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPgeneralParametroSeoPwRepository _PGeneralParametroSeoPwRepository;

        IPgeneralParametroSeoPwRepository IUnitOfWork.PGeneralParametroSeoPwRepository
        {
            get
            {
                return _PGeneralParametroSeoPwRepository ?? new PgeneralParametroSeoPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoCategoriaOrigenRepository _tipoCategoriaOrigenRepository;
        ITipoCategoriaOrigenRepository IUnitOfWork.TipoCategoriaOrigenRepository
        {
            get
            {
                return _tipoCategoriaOrigenRepository ?? new TipoCategoriaOrigenRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IExcepcionFrecuenciaPwRepository _excepcionFrecuenciaPwRepository;

        IExcepcionFrecuenciaPwRepository IUnitOfWork.ExcepcionFrecuenciaPwRepository
        {
            get
            {
                return _excepcionFrecuenciaPwRepository ?? new ExcepcionFrecuenciaPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPersonalHorarioRepository _personalHorarioRepository;

        IPersonalHorarioRepository IUnitOfWork.PersonalHorarioRepository
        {
            get
            {
                return _personalHorarioRepository ?? new PersonalHorarioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IReprogramacionCabeceraRepository _reprogramacionCabeceraRepository;

        IReprogramacionCabeceraRepository IUnitOfWork.ReprogramacionCabeceraRepository
        {
            get
            {
                return _reprogramacionCabeceraRepository ?? new ReprogramacionCabeceraRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITiempoLibreRepository _tiempoLibreRepository;

        ITiempoLibreRepository IUnitOfWork.TiempoLibreRepository
        {
            get
            {
                return _tiempoLibreRepository ?? new TiempoLibreRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IHoraBloqueadaRepository _horaBloqueadaRepository;

        IHoraBloqueadaRepository IUnitOfWork.HoraBloqueadaRepository
        {
            get
            {
                return _horaBloqueadaRepository ?? new HoraBloqueadaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPersonaRepository _personaRepository;
        IPersonaRepository IUnitOfWork.PersonaRepository
        {
            get
            {
                return _personaRepository ?? new PersonaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IOcurrenciaRepository _ocurrenciaRepository;

        IOcurrenciaRepository IUnitOfWork.OcurrenciaRepository
        {
            get
            {
                return _ocurrenciaRepository ?? new OcurrenciaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOportunidadRemarketingAgendaRepository _oportunidadRemarketingAgendaRepository;

        IOportunidadRemarketingAgendaRepository IUnitOfWork.OportunidadRemarketingAgendaRepository
        {
            get
            {
                return _oportunidadRemarketingAgendaRepository ?? new OportunidadRemarketingAgendaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPreCalculadaCambioFaseRepository _preCalculadaCambioFaseRepository;

        IPreCalculadaCambioFaseRepository IUnitOfWork.PreCalculadaCambioFaseRepository
        {
            get
            {
                return _preCalculadaCambioFaseRepository ?? new PreCalculadaCambioFaseRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IActividadDetalleRepository _actividadDetalleRepository;

        IActividadDetalleRepository IUnitOfWork.ActividadDetalleRepository
        {
            get
            {
                return _actividadDetalleRepository ?? new ActividadDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IComprobantePagoOportunidadRepository _comprobantePagoOportunidadRepository;

        IComprobantePagoOportunidadRepository IUnitOfWork.ComprobantePagoOportunidadRepository
        {
            get
            {
                return _comprobantePagoOportunidadRepository ?? new ComprobantePagoOportunidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IReprogramacionCabeceraPersonalRepository _reprogramacionCabeceraPersonalRepository;

        IReprogramacionCabeceraPersonalRepository IUnitOfWork.ReprogramacionCabeceraPersonalRepository
        {
            get
            {
                return _reprogramacionCabeceraPersonalRepository ?? new ReprogramacionCabeceraPersonalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralBeneficioRespuestaRepository _programaGeneralBeneficioRespuestaRepository;

        IProgramaGeneralBeneficioRespuestaRepository IUnitOfWork.ProgramaGeneralBeneficioRespuestaRepository
        {
            get
            {
                return _programaGeneralBeneficioRespuestaRepository ?? new ProgramaGeneralBeneficioRespuestaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralMotivacionRespuestaRepository _programaGeneralMotivacionRespuestaRepository;

        IProgramaGeneralMotivacionRespuestaRepository IUnitOfWork.ProgramaGeneralMotivacionRespuestaRepository
        {
            get
            {
                return _programaGeneralMotivacionRespuestaRepository ?? new ProgramaGeneralMotivacionRespuestaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPublicoObjetivoRespuestaRepository _publicoObjetivoRespuestaRepository;

        IPublicoObjetivoRespuestaRepository IUnitOfWork.PublicoObjetivoRespuestaRepository
        {
            get
            {
                return _publicoObjetivoRespuestaRepository ?? new PublicoObjetivoRespuestaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralCertificacionRespuestaRepository _programaGeneralCertificacionRespuestaRepository;

        IProgramaGeneralCertificacionRespuestaRepository IUnitOfWork.ProgramaGeneralCertificacionRespuestaRepository
        {
            get
            {
                return _programaGeneralCertificacionRespuestaRepository ?? new ProgramaGeneralCertificacionRespuestaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralProblemaDetalleSolucionRespuestaRepository _programaGeneralProblemaDetalleSolucionRespuestaRepository;

        IProgramaGeneralProblemaDetalleSolucionRespuestaRepository IUnitOfWork.ProgramaGeneralProblemaDetalleSolucionRespuestaRepository
        {
            get
            {
                return _programaGeneralProblemaDetalleSolucionRespuestaRepository ?? new ProgramaGeneralProblemaDetalleSolucionRespuestaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralPrerequisitoRespuestaRepository _programaGeneralPrerequisitoRespuestaRepository;

        IProgramaGeneralPrerequisitoRespuestaRepository IUnitOfWork.ProgramaGeneralPrerequisitoRespuestaRepository
        {
            get
            {
                return _programaGeneralPrerequisitoRespuestaRepository ?? new ProgramaGeneralPrerequisitoRespuestaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IDocumentoEnviadoWebPwRepository _documentoEnviadoWebPwRepository;

        IDocumentoEnviadoWebPwRepository IUnitOfWork.DocumentoEnviadoWebPwRepository
        {
            get
            {
                return _documentoEnviadoWebPwRepository ?? new DocumentoEnviadoWebPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICalidadLlamadaLogRepository _calidadLlamadaLogRepository;

        ICalidadLlamadaLogRepository IUnitOfWork.CalidadLlamadaLogRepository
        {
            get
            {
                return _calidadLlamadaLogRepository ?? new CalidadLlamadaLogRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOcurrenciaAlternoRepository _ocurrenciaAlternoRepository;

        IOcurrenciaAlternoRepository IUnitOfWork.OcurrenciaAlternoRepository
        {
            get
            {
                return _ocurrenciaAlternoRepository ?? new OcurrenciaAlternoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISmsConfiguracionEnvioRepository _smsConfiguracionEnvioRepository;

        ISmsConfiguracionEnvioRepository IUnitOfWork.SmsConfiguracionEnvioRepository
        {
            get
            {
                return _smsConfiguracionEnvioRepository ?? new SmsConfiguracionEnvioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPartnerPwRepository _partnerPwRepository;

        IPartnerPwRepository IUnitOfWork.PartnerPwRepository
        {
            get
            {
                return _partnerPwRepository ?? new PartnerPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IExpositorRepository _expositorRepository;

        IExpositorRepository IUnitOfWork.ExpositorRepository
        {
            get
            {
                return _expositorRepository ?? new ExpositorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMessengerChatRepository _messengerChatRepository;

        IMessengerChatRepository IUnitOfWork.MessengerChatRepository
        {
            get
            {
                return _messengerChatRepository ?? new MessengerChatRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IInteraccionRepository interaccionRepository;
        IInteraccionRepository IUnitOfWork.InteraccionRepository
        {
            get
            {
                return interaccionRepository ?? new InteraccionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMandrilEnvioCorreoRepository mandrilEnvioCorreoRepository;
        IMandrilEnvioCorreoRepository IUnitOfWork.MandrilEnvioCorreoRepository
        {
            get
            {
                return mandrilEnvioCorreoRepository ?? new MandrilEnvioCorreoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IGmailCorreoArchivoAdjuntoRepository gmailCorreoArchivoAdjuntoRepository;
        IGmailCorreoArchivoAdjuntoRepository IUnitOfWork.GmailCorreoArchivoAdjuntoRepository
        {
            get
            {
                return gmailCorreoArchivoAdjuntoRepository ?? new GmailCorreoArchivoAdjuntoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IConfigurarVideoProgramaRepository configurarVideoProgramaRepository;
        IConfigurarVideoProgramaRepository IUnitOfWork.ConfigurarVideoProgramaRepository
        {
            get
            {
                return configurarVideoProgramaRepository ?? new ConfigurarVideoProgramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEstructuraEspecificaRepository estructuraEspecificaRepository;
        IEstructuraEspecificaRepository IUnitOfWork.EstructuraEspecificaRepository
        {
            get
            {
                return estructuraEspecificaRepository ?? new EstructuraEspecificaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProcedenciaFormularioRepository _procedenciaFormularioRepository;

        IProcedenciaFormularioRepository IUnitOfWork.ProcedenciaFormularioRepository
        {
            get
            {
                return _procedenciaFormularioRepository ?? new ProcedenciaFormularioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProcedenciaFormularioDetalleRepository _procedenciaFormularioDetalleRepository;

        IProcedenciaFormularioDetalleRepository IUnitOfWork.ProcedenciaFormularioDetalleRepository
        {
            get
            {
                return _procedenciaFormularioDetalleRepository ?? new ProcedenciaFormularioDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoInteraccionRepository _tipoInteraccionRepository;

        ITipoInteraccionRepository IUnitOfWork.TipoInteraccionRepository
        {
            get
            {
                return _tipoInteraccionRepository ?? new TipoInteraccionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProveedorCampaniaIntegraRepository _proveedorCampaniaIntegraRepository;

        IProveedorCampaniaIntegraRepository IUnitOfWork.ProveedorCampaniaIntegraRepository
        {
            get
            {
                return _proveedorCampaniaIntegraRepository ?? new ProveedorCampaniaIntegraRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICampoContactoRepository _campoContactoRepository;

        ICampoContactoRepository IUnitOfWork.CampoContactoRepository
        {
            get
            {
                return _campoContactoRepository ?? new CampoContactoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICampaniaGeneralRepository _campaniaGeneralRepository;

        ICampaniaGeneralRepository IUnitOfWork.CampaniaGeneralRepository
        {
            get
            {
                return _campaniaGeneralRepository ?? new CampaniaGeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICampaniaGeneralDetalleProgramaRepositorio _campaniaGeneralDetalleProgramaRepository;

        ICampaniaGeneralDetalleProgramaRepositorio IUnitOfWork.CampaniaGeneralDetalleProgramaRepositorio
        {
            get
            {
                return _campaniaGeneralDetalleProgramaRepository ?? new CampaniaGeneralDetalleProgramaRepositorio(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICampaniaGeneralDetalleSubAreaRepositorio _campaniaGeneralDetalleSubAreaRepository;

        ICampaniaGeneralDetalleSubAreaRepositorio IUnitOfWork.CampaniaGeneralDetalleSubAreaRepositorio
        {
            get
            {
                return _campaniaGeneralDetalleSubAreaRepository ?? new CampaniaGeneralDetalleSubAreaRepositorio(_context, _connectionFactory, _dapperRepository);
            }
        }


        private ICampaniaGeneralDetalleRepository _campaniaGeneralDetalleRepository;

        ICampaniaGeneralDetalleRepository IUnitOfWork.CampaniaGeneralDetalleRepository
        {
            get
            {
                return _campaniaGeneralDetalleRepository ?? new CampaniaGeneralDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }



        private IRegistroArchivoStorageRepository _registroArchivoStorageRepository;

        IRegistroArchivoStorageRepository IUnitOfWork.RegistroArchivoStorageRepository
        {
            get
            {
                return _registroArchivoStorageRepository ?? new RegistroArchivoStorageRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IUrlBlockStorageRepository _urlBlockStorageRepository;

        IUrlBlockStorageRepository IUnitOfWork.UrlBlockStorageRepository
        {
            get
            {
                return _urlBlockStorageRepository ?? new UrlBlockStorageRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IUrlSubContenedorRepository _urlSubContenedorRepository;

        IUrlSubContenedorRepository IUnitOfWork.UrlSubContenedorRepository
        {
            get
            {
                return _urlSubContenedorRepository ?? new UrlSubContenedorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IFormularioSolicitudRepository _formularioSolicitudRepository;

        IFormularioSolicitudRepository IUnitOfWork.FormularioSolicitudRepository
        {
            get
            {
                return _formularioSolicitudRepository ?? new FormularioSolicitudRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFormularioRespuestaRepository _formularioRespuestumRepository;
        IFormularioRespuestaRepository IUnitOfWork.FormularioRespuestaRepository
        {
            get
            {
                return _formularioRespuestumRepository ?? new FormularioRespuestaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFormularioSolicitudTextoBotonRepository _formularioSolicitudTextoBotonRepository;
        IFormularioSolicitudTextoBotonRepository IUnitOfWork.FormularioSolicitudTextoBotonRepository
        {
            get
            {
                return _formularioSolicitudTextoBotonRepository ?? new FormularioSolicitudTextoBotonRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IAvatarRepository avatarRepository;
        IAvatarRepository IUnitOfWork.AvatarRepository
        {
            get
            {
                return avatarRepository ?? new AvatarRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoBusquedumRepository dataCreditoBusquedumRepository;
        IDataCreditoBusquedumRepository IUnitOfWork.DataCreditoBusquedumRepository
        {
            get
            {
                return dataCreditoBusquedumRepository ?? new DataCreditoBusquedumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoLogRepository dataCreditoLogRepository;
        IDataCreditoLogRepository IUnitOfWork.DataCreditoLogRepository
        {
            get
            {
                return dataCreditoLogRepository ?? new DataCreditoLogRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataNaturalNacionalRepository dataCreditoDataNaturalNacionalRepository;
        IDataCreditoDataNaturalNacionalRepository IUnitOfWork.DataCreditoDataNaturalNacionalRepository
        {
            get
            {
                return dataCreditoDataNaturalNacionalRepository ?? new DataCreditoDataNaturalNacionalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataScoreRepository dataCreditoDataScoreRepository;
        IDataCreditoDataScoreRepository IUnitOfWork.DataCreditoDataScoreRepository
        {
            get
            {
                return dataCreditoDataScoreRepository ?? new DataCreditoDataScoreRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataCuentaAhorroRepository dataCreditoDataCuentaAhorroRepository;
        IDataCreditoDataCuentaAhorroRepository IUnitOfWork.DataCreditoDataCuentaAhorroRepository
        {
            get
            {
                return dataCreditoDataCuentaAhorroRepository ?? new DataCreditoDataCuentaAhorroRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataTarjetaCreditoRepository dataCreditoDataTarjetaCreditoRepository;
        IDataCreditoDataTarjetaCreditoRepository IUnitOfWork.DataCreditoDataTarjetaCreditoRepository
        {
            get
            {
                return dataCreditoDataTarjetaCreditoRepository ?? new DataCreditoDataTarjetaCreditoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IDataCreditoDataCuentaCarteraRepository dataCreditoDataCuentaCarteraRepository;
        IDataCreditoDataCuentaCarteraRepository IUnitOfWork.DataCreditoDataCuentaCarteraRepository
        {
            get
            {
                return dataCreditoDataCuentaCarteraRepository ?? new DataCreditoDataCuentaCarteraRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoConsultumRepository dataCreditoConsultumRepository;
        IDataCreditoConsultumRepository IUnitOfWork.DataCreditoConsultumRepository
        {
            get
            {
                return dataCreditoConsultumRepository ?? new DataCreditoConsultumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataEndeudamientoGlobalRepository dataCreditoDataEndeudamientoGlobalRepository;
        IDataCreditoDataEndeudamientoGlobalRepository IUnitOfWork.DataCreditoDataEndeudamientoGlobalRepository
        {
            get
            {
                return dataCreditoDataEndeudamientoGlobalRepository ?? new DataCreditoDataEndeudamientoGlobalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataProductoValorRepository dataCreditoDataProductoValorRepository;
        IDataCreditoDataProductoValorRepository IUnitOfWork.DataCreditoDataProductoValorRepository
        {
            get
            {
                return dataCreditoDataProductoValorRepository ?? new DataCreditoDataProductoValorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfAgrResumenPrincipalRepository dataCreditoDataInfAgrResumenPrincipalRepository;
        IDataCreditoDataInfAgrResumenPrincipalRepository IUnitOfWork.DataCreditoDataInfAgrResumenPrincipalRepository
        {
            get
            {
                return dataCreditoDataInfAgrResumenPrincipalRepository ?? new DataCreditoDataInfAgrResumenPrincipalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfAgrResumenSaldoRepository dataCreditoDataInfAgrResumenSaldoRepository;
        IDataCreditoDataInfAgrResumenSaldoRepository IUnitOfWork.DataCreditoDataInfAgrResumenSaldoRepository
        {
            get
            {
                return dataCreditoDataInfAgrResumenSaldoRepository ?? new DataCreditoDataInfAgrResumenSaldoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfAgrResumenSaldoSectorRepository dataCreditoDataInfAgrResumenSaldoSectorRepository;
        IDataCreditoDataInfAgrResumenSaldoSectorRepository IUnitOfWork.DataCreditoDataInfAgrResumenSaldoSectorRepository
        {
            get
            {
                return dataCreditoDataInfAgrResumenSaldoSectorRepository ?? new DataCreditoDataInfAgrResumenSaldoSectorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfAgrResumenSaldoMeRepository dataCreditoDataInfAgrResumenSaldoMeRepository;
        IDataCreditoDataInfAgrResumenSaldoMeRepository IUnitOfWork.DataCreditoDataInfAgrResumenSaldoMeRepository
        {
            get
            {
                return dataCreditoDataInfAgrResumenSaldoMeRepository ?? new DataCreditoDataInfAgrResumenSaldoMeRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfAgrResumenComportamientoRepository dataCreditoDataInfAgrResumenComportamientoRepository;
        IDataCreditoDataInfAgrResumenComportamientoRepository IUnitOfWork.DataCreditoDataInfAgrResumenComportamientoRepository
        {
            get
            {
                return dataCreditoDataInfAgrResumenComportamientoRepository ?? new DataCreditoDataInfAgrResumenComportamientoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfAgrTotalRepository dataCreditoDataInfAgrTotalRepository;
        IDataCreditoDataInfAgrTotalRepository IUnitOfWork.DataCreditoDataInfAgrTotalRepository
        {
            get
            {
                return dataCreditoDataInfAgrTotalRepository ?? new DataCreditoDataInfAgrTotalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfAgrComposicionPortafolioRepository dataCreditoDataInfAgrComposicionPortafolioRepository;
        IDataCreditoDataInfAgrComposicionPortafolioRepository IUnitOfWork.DataCreditoDataInfAgrComposicionPortafolioRepository
        {
            get
            {
                return dataCreditoDataInfAgrComposicionPortafolioRepository ?? new DataCreditoDataInfAgrComposicionPortafolioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfAgrEvolucionDeudaTrimestreRepository dataCreditoDataInfAgrEvolucionDeudaTrimestreRepository;
        IDataCreditoDataInfAgrEvolucionDeudaTrimestreRepository IUnitOfWork.DataCreditoDataInfAgrEvolucionDeudaTrimestreRepository
        {
            get
            {
                return dataCreditoDataInfAgrEvolucionDeudaTrimestreRepository ?? new DataCreditoDataInfAgrEvolucionDeudaTrimestreRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepository dataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepository;
        IDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepository IUnitOfWork.DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepository
        {
            get
            {
                return dataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepository ?? new DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository dataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository;
        IDataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository IUnitOfWork.DataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository
        {
            get
            {
                return dataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository ?? new DataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfAgrHistoricoSaldoTotalRepository dataCreditoDataInfAgrHistoricoSaldoTotalRepository;
        IDataCreditoDataInfAgrHistoricoSaldoTotalRepository IUnitOfWork.DataCreditoDataInfAgrHistoricoSaldoTotalRepository
        {
            get
            {
                return dataCreditoDataInfAgrHistoricoSaldoTotalRepository ?? new DataCreditoDataInfAgrHistoricoSaldoTotalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfAgrResumenEndeudamientoRepository dataCreditoDataInfAgrResumenEndeudamientoRepository;
        IDataCreditoDataInfAgrResumenEndeudamientoRepository IUnitOfWork.DataCreditoDataInfAgrResumenEndeudamientoRepository
        {
            get
            {
                return dataCreditoDataInfAgrResumenEndeudamientoRepository ?? new DataCreditoDataInfAgrResumenEndeudamientoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfMicroPerfilGeneralRepository dataCreditoDataInfMicroPerfilGeneralRepository;
        IDataCreditoDataInfMicroPerfilGeneralRepository IUnitOfWork.DataCreditoDataInfMicroPerfilGeneralRepository
        {
            get
            {
                return dataCreditoDataInfMicroPerfilGeneralRepository ?? new DataCreditoDataInfMicroPerfilGeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfMicroVectorSaldoMoraRepository dataCreditoDataInfMicroVectorSaldoMoraRepository;
        IDataCreditoDataInfMicroVectorSaldoMoraRepository IUnitOfWork.DataCreditoDataInfMicroVectorSaldoMoraRepository
        {
            get
            {
                return dataCreditoDataInfMicroVectorSaldoMoraRepository ?? new DataCreditoDataInfMicroVectorSaldoMoraRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfMicroEndeudamientoActualRepository dataCreditoDataInfMicroEndeudamientoActualRepository;
        IDataCreditoDataInfMicroEndeudamientoActualRepository IUnitOfWork.DataCreditoDataInfMicroEndeudamientoActualRepository
        {
            get
            {
                return dataCreditoDataInfMicroEndeudamientoActualRepository ?? new DataCreditoDataInfMicroEndeudamientoActualRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfMicroImagenTendenciaEndeudamientoRepository dataCreditoDataInfMicroImagenTendenciaEndeudamientoRepository;
        IDataCreditoDataInfMicroImagenTendenciaEndeudamientoRepository IUnitOfWork.DataCreditoDataInfMicroImagenTendenciaEndeudamientoRepository
        {
            get
            {
                return dataCreditoDataInfMicroImagenTendenciaEndeudamientoRepository ?? new DataCreditoDataInfMicroImagenTendenciaEndeudamientoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfMicroAnalisisVectorRepository dataCreditoDataInfMicroAnalisisVectorRepository;
        IDataCreditoDataInfMicroAnalisisVectorRepository IUnitOfWork.DataCreditoDataInfMicroAnalisisVectorRepository
        {
            get
            {
                return dataCreditoDataInfMicroAnalisisVectorRepository ?? new DataCreditoDataInfMicroAnalisisVectorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDataCreditoDataInfMicroEvolucionDeudumRepository dataCreditoDataInfMicroEvolucionDeudumRepository;
        IDataCreditoDataInfMicroEvolucionDeudumRepository IUnitOfWork.DataCreditoDataInfMicroEvolucionDeudumRepository
        {
            get
            {
                return dataCreditoDataInfMicroEvolucionDeudumRepository ?? new DataCreditoDataInfMicroEvolucionDeudumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        // IDataCreditoDataScoreRepository

        private INotaIngresoCajaRepository notaIngresoCajaRepository;
        INotaIngresoCajaRepository IUnitOfWork.NotaIngresoCajaRepository
        {
            get
            {
                return notaIngresoCajaRepository ?? new NotaIngresoCajaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOrigenIngresoCajaRepository origenIngresoCajaRepository;
        IOrigenIngresoCajaRepository IUnitOfWork.OrigenIngresoCajaRepository
        {
            get
            {
                return origenIngresoCajaRepository ?? new OrigenIngresoCajaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IContactoConfiguracionRepository contactoConfiguracionRepository;
        IContactoConfiguracionRepository IUnitOfWork.ContactoConfiguracionRepository
        {
            get
            {
                return contactoConfiguracionRepository ?? new ContactoConfiguracionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IReferidoConfiguracionRepository referidoConfiguracionRepository;
        IReferidoConfiguracionRepository IUnitOfWork.ReferidoConfiguracionRepository
        {
            get
            {
                return referidoConfiguracionRepository ?? new ReferidoConfiguracionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICampoFormularioRepository campoFormularioRepository;
        ICampoFormularioRepository IUnitOfWork.CampoFormularioRepository
        {
            get
            {
                return campoFormularioRepository ?? new CampoFormularioRepository(_context, _connectionFactory, _dapperRepository);
            }

        }

        private ICampoFormularioOpcionRepository campoFormularioOpcionRepository;
        ICampoFormularioOpcionRepository IUnitOfWork.CampoFormularioOpcionRepository
        {
            get
            {
                return campoFormularioOpcionRepository ?? new CampoFormularioOpcionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICajaPorRendirRepository cajaPorRendirRepository;
        ICajaPorRendirRepository IUnitOfWork.CajaPorRendirRepository
        {
            get
            {
                return cajaPorRendirRepository ?? new CajaPorRendirRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICajaPorRendirCabeceraRepository cajaPorRendirCabeceraRepository;
        ICajaPorRendirCabeceraRepository IUnitOfWork.CajaPorRendirCabeceraRepository
        {
            get
            {
                return cajaPorRendirCabeceraRepository ?? new CajaPorRendirCabeceraRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IFurRepository furRepository;
        IFurRepository IUnitOfWork.FurRepository
        {
            get
            {
                return furRepository ?? new FurRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IConjuntoAnuncioRepository conjuntoAnuncioRepository;

        IConjuntoAnuncioRepository IUnitOfWork.ConjuntoAnuncioRepository
        {
            get
            {
                return conjuntoAnuncioRepository ?? new ConjuntoAnuncioRepository(_context, _connectionFactory, _dapperRepository);
            }

        }


        private IRegionCiudadRepository regionCiudadRepository;

        IRegionCiudadRepository IUnitOfWork.RegionCiudadRepository
        {
            get
            {
                return regionCiudadRepository ?? new RegionCiudadRepository(_context, _connectionFactory, _dapperRepository);
            }

        }

        private IFuentesPortalWebRepository fuentesPortalWebRepository;

        IFuentesPortalWebRepository IUnitOfWork.FuentesPortalWebRepository
        {
            get
            {
                return fuentesPortalWebRepository ?? new FuentesPortalWebRepository(_context, _connectionFactory, _dapperRepository);
            }

        }

        private IEstilosCssRepository estilosCssRepository;

        IEstilosCssRepository IUnitOfWork.EstilosCssRepository
        {
            get
            {
                return estilosCssRepository ?? new EstilosCssRepository(_context, _connectionFactory, _dapperRepository);
            }

        }


        private ITagsRepository tagsRepository;

        ITagsRepository IUnitOfWork.TagsRepository
        {
            get
            {
                return tagsRepository ?? new TagsRepository(_context, _connectionFactory, _dapperRepository);
            }

        }

        private ITagsEstiloRepository tagsEstiloRepository;

        ITagsEstiloRepository IUnitOfWork.TagsEstiloRepository
        {
            get
            {
                return tagsEstiloRepository ?? new TagsEstiloRepository(_context, _connectionFactory, _dapperRepository);
            }

        }
        private ITipoLandingPageRepository tipoLandingPageRepository;

        ITipoLandingPageRepository IUnitOfWork.TipoLandingPageRepository
        {
            get
            {
                return tipoLandingPageRepository ?? new TipoLandingPageRepository(_context, _connectionFactory, _dapperRepository);
            }

        }

        private IPlantillaV2Repository plantillaV2Repository;

        IPlantillaV2Repository IUnitOfWork.PlantillaV2Repository
        {
            get
            {
                return plantillaV2Repository ?? new PlantillaV2Repository(_context, _connectionFactory, _dapperRepository);
            }

        }

        private ISeccionRepository seccionRepository;

        ISeccionRepository IUnitOfWork.SeccionRepository
        {
            get
            {
                return seccionRepository ?? new SeccionRepository(_context, _connectionFactory, _dapperRepository);
            }

        }

        private IPlantillaV2SeccionRepository plantillaV2SeccionRepository;

        IPlantillaV2SeccionRepository IUnitOfWork.PlantillaV2SeccionRepository
        {
            get
            {
                return plantillaV2SeccionRepository ?? new PlantillaV2SeccionRepository(_context, _connectionFactory, _dapperRepository);
            }

        }

        private IPlantillaV2SeccionEstiloRepository plantillaV2SeccionEstiloRepository;

        IPlantillaV2SeccionEstiloRepository IUnitOfWork.PlantillaV2SeccionEstiloRepository
        {
            get
            {
                return plantillaV2SeccionEstiloRepository ?? new PlantillaV2SeccionEstiloRepository(_context, _connectionFactory, _dapperRepository);
            }

        }

        private ILandingPageRepository landingPageRepository;

        ILandingPageRepository IUnitOfWork.LandingPageRepository
        {
            get
            {
                return landingPageRepository ?? new LandingPageRepository(_context, _connectionFactory, _dapperRepository);
            }

        }


        private IProgramaGeneralModeloCertificadoRepository programaGeneralModeloCertificadoRepository;
        IProgramaGeneralModeloCertificadoRepository IUnitOfWork.ProgramaGeneralModeloCertificadoRepository
        {
            get
            {
                return programaGeneralModeloCertificadoRepository ?? new ProgramaGeneralModeloCertificadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICajaEgresoAprobadoRepository cajaEgresoAprobadoRepository;
        ICajaEgresoAprobadoRepository IUnitOfWork.CajaEgresoAprobadoRepository
        {
            get
            {
                return cajaEgresoAprobadoRepository ?? new CajaEgresoAprobadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICajaEgresoRepository cajaEgresoRepository;
        ICajaEgresoRepository IUnitOfWork.CajaEgresoRepository
        {
            get
            {
                return cajaEgresoRepository ?? new CajaEgresoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IComprobantePagoPorFurRepository comprobantePagoPorFurRepository;
        IComprobantePagoPorFurRepository IUnitOfWork.ComprobantePagoPorFurRepository
        {
            get
            {
                return comprobantePagoPorFurRepository ?? new ComprobantePagoPorFurRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFurPagoRepository furPagoRepository;
        IFurPagoRepository IUnitOfWork.FurPagoRepository
        {
            get
            {
                return furPagoRepository ?? new FurPagoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IComprobantePagoRepository comprobantePagoRepository;
        IComprobantePagoRepository IUnitOfWork.ComprobantePagoRepository
        {
            get
            {
                return comprobantePagoRepository ?? new ComprobantePagoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IOrigenDatoCalidadDetalleRepository _origenDatoCalidadDetalleRepository;

        IOrigenDatoCalidadDetalleRepository IUnitOfWork.OrigenDatoCalidadDetalleRepository
        {
            get
            {
                return _origenDatoCalidadDetalleRepository ?? new OrigenDatoCalidadDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOrigenSectorRepository _origenSectorRepository;

        IOrigenSectorRepository IUnitOfWork.OrigenSectorRepository
        {
            get
            {
                return _origenSectorRepository ?? new OrigenSectorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOrigenDatoCalidadRepository _origenDatoCalidadRepository;

        IOrigenDatoCalidadRepository IUnitOfWork.OrigenDatoCalidadRepository
        {
            get
            {
                return _origenDatoCalidadRepository ?? new OrigenDatoCalidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IOportunidadConfiguradoRepository _oportunidadConfiguradoRepository;

        IOportunidadConfiguradoRepository IUnitOfWork.OportunidadConfiguradoRepository
        {
            get
            {
                return _oportunidadConfiguradoRepository ?? new OportunidadConfiguradoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOportunidadPreAsignadumRepository _oportunidadPreAsignadumRepository;

        IOportunidadPreAsignadumRepository IUnitOfWork.OportunidadPreAsignadumRepository
        {
            get
            {
                return _oportunidadPreAsignadumRepository ?? new OportunidadPreAsignadumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOportunidadErradoRepository _oportunidadErradoRepository;

        IOportunidadErradoRepository IUnitOfWork.OportunidadErradoRepository
        {
            get
            {
                return _oportunidadErradoRepository ?? new OportunidadErradoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IReporteRepository reportesRepository;
        IReporteRepository IUnitOfWork.ReportesRepository
        {
            get
            {
                return reportesRepository ?? new ReporteRepository(_dapperRepository);
            }
        }
        private IWhatsAppConfiguracionRepository whatsAppConfiguracionRepository;
        IWhatsAppConfiguracionRepository IUnitOfWork.WhatsAppConfiguracionRepository
        {
            get
            {
                return whatsAppConfiguracionRepository ?? new WhatsAppConfiguracionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IWhatsAppUsuarioCredencialRepository whatsAppUsuarioCredencialRepository;
        IWhatsAppUsuarioCredencialRepository IUnitOfWork.WhatsAppUsuarioCredencialRepository
        {
            get
            {
                return whatsAppUsuarioCredencialRepository ?? new WhatsAppUsuarioCredencialRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMoodleCronogramaEvaluacionRepository moodleCronogramaEvaluacionRepository;
        IMoodleCronogramaEvaluacionRepository IUnitOfWork.MoodleCronogramaEvaluacionRepository
        {
            get
            {
                return moodleCronogramaEvaluacionRepository ?? new MoodleCronogramaEvaluacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IInteraccionChatIntegraRepository interaccionChatIntegraRepository;
        IInteraccionChatIntegraRepository IUnitOfWork.InteraccionChatIntegraRepository
        {
            get
            {
                return interaccionChatIntegraRepository ?? new InteraccionChatIntegraRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMensajeTextoRepository mensajeTextoRepository;
        IMensajeTextoRepository IUnitOfWork.MensajeTextoRepository
        {
            get
            {
                return mensajeTextoRepository ?? new MensajeTextoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IConfiguracionDatoRemarketingRepository configuracionDatoRemarketingRepository;
        IConfiguracionDatoRemarketingRepository IUnitOfWork.ConfiguracionDatoRemarketingRepository
        {
            get
            {
                return configuracionDatoRemarketingRepository ?? new ConfiguracionDatoRemarketingRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IConfiguracionDatoRemarketingTipoDatoRepository configuracionDatoRemarketingTipoDatoRepository;
        IConfiguracionDatoRemarketingTipoDatoRepository IUnitOfWork.ConfiguracionDatoRemarketingTipoDatoRepository
        {
            get
            {
                return configuracionDatoRemarketingTipoDatoRepository ?? new ConfiguracionDatoRemarketingTipoDatoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IConfiguracionDatoRemarketingTipoCategoriaOrigenRepository configuracionDatoRemarketingTipoCategoriaOrigenRepository;
        IConfiguracionDatoRemarketingTipoCategoriaOrigenRepository IUnitOfWork.ConfiguracionDatoRemarketingTipoCategoriaOrigenRepository
        {
            get
            {
                return configuracionDatoRemarketingTipoCategoriaOrigenRepository ?? new ConfiguracionDatoRemarketingTipoCategoriaOrigenRepository(_context, _connectionFactory, _dapperRepository);
            }
        }



        private IProcedenciaVentaCruzadumRepository procedenciaVentaCruzadumRepository;
        IProcedenciaVentaCruzadumRepository IUnitOfWork.ProcedenciaVentaCruzadumRepository
        {
            get
            {
                return procedenciaVentaCruzadumRepository ?? new ProcedenciaVentaCruzadumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICampaniaMailingDetalleRepository campaniaMailingDetalleRepository;
        ICampaniaMailingDetalleRepository IUnitOfWork.CampaniaMailingDetalleRepository
        {
            get
            {
                return campaniaMailingDetalleRepository ?? new CampaniaMailingDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IAsignacionOportunidadRepository asignacionOportunidadRepository;
        IAsignacionOportunidadRepository IUnitOfWork.AsignacionOportunidadRepository
        {
            get
            {
                return asignacionOportunidadRepository ?? new AsignacionOportunidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IAsignacionOportunidadLogRepository asignacionOportunidadLogRepository;
        IAsignacionOportunidadLogRepository IUnitOfWork.AsignacionOportunidadLogRepository
        {
            get
            {
                return asignacionOportunidadLogRepository ?? new AsignacionOportunidadLogRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IModeloDataMiningRepository modeloDataMiningRepository;
        IModeloDataMiningRepository IUnitOfWork.ModeloDataMiningRepository
        {
            get
            {
                return modeloDataMiningRepository ?? new ModeloDataMiningRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ILogRepository logRepository;
        ILogRepository IUnitOfWork.LogRepository
        {
            get
            {
                return logRepository ?? new LogRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICriterioCalificacionRepository _criterioCalificacionRepository;
        ICriterioCalificacionRepository IUnitOfWork.CriterioCalificacionRepository
        {
            get
            {
                return _criterioCalificacionRepository ?? new CriterioCalificacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IModoFurRepository _modoFurRepository;
        IModoFurRepository IUnitOfWork.ModoFurRepository
        {
            get
            {
                return _modoFurRepository ?? new ModoFurRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IModoPersonalFurRepository _modoPersonalFurRepository;
        IModoPersonalFurRepository IUnitOfWork.ModoPersonalFurRepository
        {
            get
            {
                return _modoPersonalFurRepository ?? new ModoPersonalFurRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFurFaseAprobacionRepository _furFaseAprobacionRepository;
        IFurFaseAprobacionRepository IUnitOfWork.FurFaseAprobacionRepository
        {
            get
            {
                return _furFaseAprobacionRepository ?? new FurFaseAprobacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IConfiguracionDatoRemarketingCategoriaOrigenRepository configuracionDatoRemarketingCategoriaOrigenRepository;
        IConfiguracionDatoRemarketingCategoriaOrigenRepository IUnitOfWork.ConfiguracionDatoRemarketingCategoriaOrigenRepository
        {
            get
            {
                return configuracionDatoRemarketingCategoriaOrigenRepository ?? new ConfiguracionDatoRemarketingCategoriaOrigenRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IConfiguracionDatoRemarketingProbabilidadRegistroRepository configuracionDatoRemarketingProbabilidadRegistroRepository;
        IConfiguracionDatoRemarketingProbabilidadRegistroRepository IUnitOfWork.ConfiguracionDatoRemarketingProbabilidadRegistroRepository
        {
            get
            {
                return configuracionDatoRemarketingProbabilidadRegistroRepository ?? new ConfiguracionDatoRemarketingProbabilidadRegistroRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IGrupoFiltroProgramaCriticoRepository grupoFiltroProgramaCriticoRepository;
        IGrupoFiltroProgramaCriticoRepository IUnitOfWork.GrupoFiltroProgramaCriticoRepository
        {
            get
            {
                return grupoFiltroProgramaCriticoRepository ?? new GrupoFiltroProgramaCriticoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IGrupoFiltroProgramaCriticoPorAsesorRepository grupoFiltroProgramaCriticoPorAsesorRepository;
        IGrupoFiltroProgramaCriticoPorAsesorRepository IUnitOfWork.GrupoFiltroProgramaCriticoPorAsesorRepository
        {
            get
            {
                return grupoFiltroProgramaCriticoPorAsesorRepository ?? new GrupoFiltroProgramaCriticoPorAsesorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IGrupoFiltroProgramaCriticoPgeneralRepository grupoFiltroProgramaCriticoPgeneralRepository;
        IGrupoFiltroProgramaCriticoPgeneralRepository IUnitOfWork.GrupoFiltroProgramaCriticoPgeneralRepository
        {
            get
            {
                return grupoFiltroProgramaCriticoPgeneralRepository ?? new GrupoFiltroProgramaCriticoPgeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IPuntoCorteRepository puntoCorteRepository;
        IPuntoCorteRepository IUnitOfWork.PuntoCorteRepository
        {
            get
            {
                return puntoCorteRepository ?? new PuntoCorteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralPuntoCorteRepository programaGeneralPuntoCorteRepository;
        IProgramaGeneralPuntoCorteRepository IUnitOfWork.ProgramaGeneralPuntoCorteRepository
        {
            get
            {
                return programaGeneralPuntoCorteRepository ?? new ProgramaGeneralPuntoCorteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralPuntoCorteConfiguracionRepository programaGeneralPuntoCorteConfiguracionRepository;
        IProgramaGeneralPuntoCorteConfiguracionRepository IUnitOfWork.ProgramaGeneralPuntoCorteConfiguracionRepository
        {
            get
            {
                return programaGeneralPuntoCorteConfiguracionRepository ?? new ProgramaGeneralPuntoCorteConfiguracionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralPuntoCorteDetalleRepository programaGeneralPuntoCorteDetalleRepository;
        IProgramaGeneralPuntoCorteDetalleRepository IUnitOfWork.ProgramaGeneralPuntoCorteDetalleRepository
        {
            get
            {
                return programaGeneralPuntoCorteDetalleRepository ?? new ProgramaGeneralPuntoCorteDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IBloqueHorarioRepository bloqueHorarioRepository;
        IBloqueHorarioRepository IUnitOfWork.BloqueHorarioRepository
        {
            get
            {
                return bloqueHorarioRepository ?? new BloqueHorarioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }






        private ITroncalPgeneralRepository troncalPgeneralrRepository;
        ITroncalPgeneralRepository IUnitOfWork.TroncalPgeneralRepository
        {
            get
            {
                return troncalPgeneralrRepository ?? new TroncalPgeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPgeneralConfiguracionPlantillaDetalleRepository pgeneralConfiguracionPlantillaDetalleRepository;
        IPgeneralConfiguracionPlantillaDetalleRepository IUnitOfWork.PgeneralConfiguracionPlantillaDetalleRepository
        {
            get
            {
                return pgeneralConfiguracionPlantillaDetalleRepository ?? new PgeneralConfiguracionPlantillaDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IPgeneralCodigoPartnerRepository pgeneralCodigoPartnerRepository;
        IPgeneralCodigoPartnerRepository IUnitOfWork.PgeneralCodigoPartnerRepository
        {
            get
            {
                return pgeneralCodigoPartnerRepository ?? new PgeneralCodigoPartnerRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPgeneralCodigoPartnerVersionProgramaRepository pgeneralCodigoPartnerVersionProgramaRepository;
        IPgeneralCodigoPartnerVersionProgramaRepository IUnitOfWork.PgeneralCodigoPartnerVersionProgramaRepository
        {
            get
            {
                return pgeneralCodigoPartnerVersionProgramaRepository ?? new PgeneralCodigoPartnerVersionProgramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPgeneralCodigoPartnerModalidadCursoRepository pgeneralCodigoPartnerModalidadCursoRepository;
        IPgeneralCodigoPartnerModalidadCursoRepository IUnitOfWork.PgeneralCodigoPartnerModalidadCursoRepository
        {
            get
            {
                return pgeneralCodigoPartnerModalidadCursoRepository ?? new PgeneralCodigoPartnerModalidadCursoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPgeneralProyectoAplicacionRepository pgeneralProyectoAplicacionRepository;
        IPgeneralProyectoAplicacionRepository IUnitOfWork.PgeneralProyectoAplicacionRepository
        {
            get
            {
                return pgeneralProyectoAplicacionRepository ?? new PgeneralProyectoAplicacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPgeneralProyectoAplicacionModalidadRepository pgeneralProyectoAplicacionModalidadRepository;
        IPgeneralProyectoAplicacionModalidadRepository IUnitOfWork.PgeneralProyectoAplicacionModalidadRepository
        {
            get
            {
                return pgeneralProyectoAplicacionModalidadRepository ?? new PgeneralProyectoAplicacionModalidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPgeneralProyectoAplicacionProveedorRepository pgeneralProyectoAplicacionProveedorRepository;
        IPgeneralProyectoAplicacionProveedorRepository IUnitOfWork.PgeneralProyectoAplicacionProveedorRepository
        {
            get
            {
                return pgeneralProyectoAplicacionProveedorRepository ?? new PgeneralProyectoAplicacionProveedorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPgeneralCriterioEvaluacionHijoRepository pgeneralCriterioEvaluacionHijoRepository;
        IPgeneralCriterioEvaluacionHijoRepository IUnitOfWork.PgeneralCriterioEvaluacionHijoRepository
        {
            get
            {
                return pgeneralCriterioEvaluacionHijoRepository ?? new PgeneralCriterioEvaluacionHijoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPgeneralForoAsignacionProveedorRepository pgeneralForoAsignacionProveedorRepository;
        IPgeneralForoAsignacionProveedorRepository IUnitOfWork.PgeneralForoAsignacionProveedorRepository
        {
            get
            {
                return pgeneralForoAsignacionProveedorRepository ?? new PgeneralForoAsignacionProveedorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }




        private IPostulanteRepository postulanteRepository;
        IPostulanteRepository IUnitOfWork.PostulanteRepository
        {
            get
            {
                return postulanteRepository ?? new PostulanteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IChatIntegraHistorialAsesorRepository chatIntegraHistorialAsesorRepository;
        IChatIntegraHistorialAsesorRepository IUnitOfWork.ChatIntegraHistorialAsesorRepository
        {
            get
            {
                return chatIntegraHistorialAsesorRepository ?? new ChatIntegraHistorialAsesorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IWhatsAppMensajeRecibidoRepository whatsAppMensajeRecibidoRepository;
        IWhatsAppMensajeRecibidoRepository IUnitOfWork.WhatsAppMensajeRecibidoRepository
        {
            get
            {
                return whatsAppMensajeRecibidoRepository ?? new WhatsAppMensajeRecibidoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ILlamadaWebphoneAsteriskRepository llamadaWebphoneAsteriskRepository;
        ILlamadaWebphoneAsteriskRepository IUnitOfWork.LlamadaWebphoneAsteriskRepository
        {
            get
            {
                return llamadaWebphoneAsteriskRepository ?? new LlamadaWebphoneAsteriskRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IControlDocAlumnoRepository controlDocAlumnoRepository;
        IControlDocAlumnoRepository IUnitOfWork.ControlDocAlumnoRepository
        {
            get
            {
                return controlDocAlumnoRepository ?? new ControlDocAlumnoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IBeneficioAlumnoPEspecificoRepository beneficioAlumnoPEspecificoRepository;
        IBeneficioAlumnoPEspecificoRepository IUnitOfWork.BeneficioAlumnoPEspecificoRepository
        {
            get
            {
                return beneficioAlumnoPEspecificoRepository ?? new BeneficioAlumnoPEspecificoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPaisConfiguracionAsignacionRegularRepository _paisConfiguracionAsignacionRegularRepository;
        IPaisConfiguracionAsignacionRegularRepository IUnitOfWork.PaisConfiguracionAsignacionRegularRepository
        {
            get
            {
                return _paisConfiguracionAsignacionRegularRepository ?? new PaisConfiguracionAsignacionRegularRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPaisAsignacionRegularRepository _paisAsignacionRegularRepository;
        IPaisAsignacionRegularRepository IUnitOfWork.PaisAsignacionRegularRepository
        {
            get
            {
                return _paisAsignacionRegularRepository ?? new PaisAsignacionRegularRepository(_context, _connectionFactory, _dapperRepository);
            }
        }



        private IBeneficioLaboralTipoRepository _beneficioLaboralTipoRepository;
        IBeneficioLaboralTipoRepository IUnitOfWork.BeneficioLaboralTipoRepository
        {
            get
            {
                return _beneficioLaboralTipoRepository ?? new BeneficioLaboralTipoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }




        private IAsignacionRegularRepository _asignacionRegularRepository;
        IAsignacionRegularRepository IUnitOfWork.AsignacionRegularRepository
        {
            get
            {
                return _asignacionRegularRepository ?? new AsignacionRegularRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IAdworkCredencialApiRepository _adworkCredencialApiRepository;
        IAdworkCredencialApiRepository IUnitOfWork.AdworkCredencialApiRepository
        {
            get
            {
                return _adworkCredencialApiRepository ?? new AdworkCredencialApiRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IAdwordsApiPalabraClaveRepository _adwordsApiPalabraClaveRepository;
        IAdwordsApiPalabraClaveRepository IUnitOfWork.AdwordsApiPalabraClaveRepository
        {
            get
            {
                return _adwordsApiPalabraClaveRepository ?? new AdwordsApiPalabraClaveRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IAdwordsApiVolumenBusquedumRepository a_dwordsApiVolumenBusquedumRepository;
        IAdwordsApiVolumenBusquedumRepository IUnitOfWork.AdwordsApiVolumenBusquedumRepository
        {
            get
            {
                return a_dwordsApiVolumenBusquedumRepository ?? new AdwordsApiVolumenBusquedumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IAsignacionAutomaticaRepository _asignacionAutomaticaRepository;
        IAsignacionAutomaticaRepository IUnitOfWork.AsignacionAutomaticaRepository
        {
            get
            {
                return _asignacionAutomaticaRepository ?? new AsignacionAutomaticaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConfiguracionAsignacionCoordinadorOportunidadOperacionRepository configuracionAsignacionCoordinadorOportunidadOperacionRepository;
        IConfiguracionAsignacionCoordinadorOportunidadOperacionRepository IUnitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository
        {
            get
            {
                return configuracionAsignacionCoordinadorOportunidadOperacionRepository ?? new ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IOportunidadIsVerificadaRepository oportunidadVerificadaRepository;
        IOportunidadIsVerificadaRepository IUnitOfWork.OportunidadIsVerificadaRepository
        {
            get
            {
                return oportunidadVerificadaRepository ?? new OportunidadIsVerificadaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISolicitudCertificadoFisicoRepository solicitudCertificadoFisicoRepository;
        ISolicitudCertificadoFisicoRepository IUnitOfWork.SolicitudCertificadoFisicoRepository
        {
            get
            {
                return solicitudCertificadoFisicoRepository ?? new SolicitudCertificadoFisicoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPEspecificoSesionRepository pEspecificoSesionRepository;
        IPEspecificoSesionRepository IUnitOfWork.PEspecificoSesionRepository
        {
            get
            {
                return pEspecificoSesionRepository ?? new PEspecificoSesionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IOportunidadClasificacionOperacionesRepository oportunidadClasificacionOperacionesRepository;
        IOportunidadClasificacionOperacionesRepository IUnitOfWork.OportunidadClasificacionOperacionesRepository
        {
            get
            {
                return oportunidadClasificacionOperacionesRepository ?? new OportunidadClasificacionOperacionesRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISolicitudOperacionesRepository solicitudOperacionesRepository;
        ISolicitudOperacionesRepository IUnitOfWork.SolicitudOperacionesRepository
        {
            get
            {
                return solicitudOperacionesRepository ?? new SolicitudOperacionesRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IReporteComisionMatriculaRepository _reporteComisionMatriculaRepository;
        IReporteComisionMatriculaRepository IUnitOfWork.ReporteComisionMatriculaRepository
        {
            get
            {
                return _reporteComisionMatriculaRepository ?? new ReporteComisionMatriculaRepository(_dapperRepository);
            }
        }

        private ISendinblueCampaniaRepository _sendinblueCampaniaRepository;
        ISendinblueCampaniaRepository IUnitOfWork.sendinblueCampaniaRepository
        {
            get
            {
                return _sendinblueCampaniaRepository ?? new SendingblueCampaniaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISendinblueCarpetaRepository _sendinblueCarpetaRepository;
        ISendinblueCarpetaRepository IUnitOfWork.sendinblueCarpetaRepository
        {
            get
            {
                return _sendinblueCarpetaRepository ?? new SendinblueCarpetaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISendinblueContactoRepository _sendinblueContactoRepository;
        ISendinblueContactoRepository IUnitOfWork.sendinblueContactoRepository
        {
            get
            {
                return _sendinblueContactoRepository ?? new SendinblueContactoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISendinblueListaRepository _sendinblueListaRepository;
        ISendinblueListaRepository IUnitOfWork.sendinblueListaRepository
        {
            get
            {
                return _sendinblueListaRepository ?? new SendinblueListaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IUsuarioRepository _usuarioRepository;
        IUsuarioRepository IUnitOfWork.UsuarioRepository
        {
            get
            {
                return _usuarioRepository ?? new UsuarioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITarifarioDetalleAlternoRepository _tarifarioDetalleAlternoRepository;
        ITarifarioDetalleAlternoRepository IUnitOfWork.TarifarioDetalleAlternoRepository
        {
            get
            {
                return _tarifarioDetalleAlternoRepository ?? new TarifarioDetalleAlternoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFiltroSegmentoSendinBlueRepository _filtroSegmentoSendinBlueRepository;
        IFiltroSegmentoSendinBlueRepository IUnitOfWork.filtroSegmentoSendinBlueRepository
        {
            get
            {
                return _filtroSegmentoSendinBlueRepository ?? new FiltroSegmentoSendinBlueRepository(_context, _connectionFactory, _dapperRepository);

            }
        }
        private ICorreoGmailRepository _correoGmailRepository;
        ICorreoGmailRepository IUnitOfWork.CorreoGmailRepository
        {
            get
            {
                return _correoGmailRepository ?? new CorreoGmailRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConfiguracionEnvioMailingRepository _configuracionEnvioMailingRepository;
        IConfiguracionEnvioMailingRepository IUnitOfWork.ConfiguracionEnvioMailingRepository
        {
            get
            {
                return _configuracionEnvioMailingRepository ?? new ConfiguracionEnvioMailingRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IReclamoRepository _reclamoRepository;
        IReclamoRepository IUnitOfWork.ReclamoRepository
        {
            get
            {
                return _reclamoRepository ?? new ReclamoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEstadoCertificadoFisicoRepository _estadoCertificadoFisicoRepository;
        IEstadoCertificadoFisicoRepository IUnitOfWork.EstadoCertificadoFisicoRepository
        {
            get
            {
                return _estadoCertificadoFisicoRepository ?? new EstadoCertificadoFisicoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISeguimientoAlumnoCategoriaRepository _seguimientoAlumnoCategoriaRepository;
        ISeguimientoAlumnoCategoriaRepository IUnitOfWork.SeguimientoAlumnoCategoriaRepository
        {
            get
            {
                return _seguimientoAlumnoCategoriaRepository ?? new SeguimientoAlumnoCategoriaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICategoriaAlumnoRepository _categoriaAlumnoRepository;
        ICategoriaAlumnoRepository IUnitOfWork.CategoriaAlumnoRepository
        {
            get
            {
                return _categoriaAlumnoRepository ?? new CategoriaAlumnoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEsquemaEvaluacionRepository _esquemaEvaluacionRepository;
        IEsquemaEvaluacionRepository IUnitOfWork.EsquemaEvaluacionRepository
        {
            get
            {
                return _esquemaEvaluacionRepository ?? new EsquemaEvaluacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private INotaRepository _notaRepository;
        INotaRepository IUnitOfWork.NotaRepository
        {
            get
            {
                return _notaRepository ?? new NotaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IRecuperacionSesionRepository _recuperacionSesionRepository;
        IRecuperacionSesionRepository IUnitOfWork.RecuperacionSesionRepository
        {
            get
            {
                return _recuperacionSesionRepository ?? new RecuperacionSesionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IEvaluacionEscalaCalificacionRepository _evaluacionEscalaCalificacionRepository;
        IEvaluacionEscalaCalificacionRepository IUnitOfWork.EvaluacionEscalaCalificacionRepository
        {
            get
            {
                return _evaluacionEscalaCalificacionRepository ?? new EvaluacionEscalaCalificacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPEspecificoMatriculaAlumnoRepository _pEspecificoMatriculaAlumnoRepository;
        IPEspecificoMatriculaAlumnoRepository IUnitOfWork.PEspecificoMatriculaAlumnoRepository
        {
            get
            {
                return _pEspecificoMatriculaAlumnoRepository ?? new PEspecificoMatriculaAlumnoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISendingblueRelacionAlumnoSbRepository _sendingblueRelacionAlumnoSbRepository;
        ISendingblueRelacionAlumnoSbRepository IUnitOfWork.sendingblueRelacionAlumnoSbRepository
        {
            get
            {
                return _sendingblueRelacionAlumnoSbRepository ?? new SendingblueRelacionAlumnoSbRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISendinblueAtributoRepository _sendinblueAtributoRepository;
        ISendinblueAtributoRepository IUnitOfWork.sendinblueAtributoRepository
        {
            get
            {
                return _sendinblueAtributoRepository ?? new SendinblueAtributoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IAsignacionAutomaticaErrorRepository asignacionAutomaticaErrorRepository;
        IAsignacionAutomaticaErrorRepository IUnitOfWork.AsignacionAutomaticaErrorRepository
        {
            get
            {
                return asignacionAutomaticaErrorRepository ?? new AsignacionAutomaticaErrorRepository(_context, _connectionFactory, _dapperRepository);

            }
        }
        private IRemitenteMailingRepository remitenteMailingRepository;
        IRemitenteMailingRepository IUnitOfWork.RemitenteMailingRepository
        {
            get
            {
                return remitenteMailingRepository ?? new RemitenteMailingRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IRemitenteMailingAsesorRepository remitenteMailingAsesorRepository;
        IRemitenteMailingAsesorRepository IUnitOfWork.RemitenteMailingAsesorRepository
        {
            get
            {
                return remitenteMailingAsesorRepository ?? new RemitenteMailingAsesorRepository(_context, _connectionFactory, _dapperRepository);

            }
        }
        private IAsesorChatRepository asesorChatRepository;
        IAsesorChatRepository IUnitOfWork.AsesorChatRepository
        {
            get
            {
                return asesorChatRepository ?? new AsesorChatRepository(_context, _connectionFactory, _dapperRepository);

            }
        }
        private IAsesorChatDetalleRepository asesorChatDetalleRepository;
        IAsesorChatDetalleRepository IUnitOfWork.AsesorChatDetalleRepository
        {
            get
            {
                return asesorChatDetalleRepository ?? new AsesorChatDetalleRepository(_context, _connectionFactory, _dapperRepository);

            }
        }

        private ICampaniasMailingWhatsappRepository _campaniasMailingWhatsappRepository;
        ICampaniasMailingWhatsappRepository IUnitOfWork.campaniasMailingWhatsappRepository
        {
            get
            {
                return _campaniasMailingWhatsappRepository ?? new CampaniasMailingWhatsappRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICampaniaWhatsappFiltradoRepository _campaniaWhatsappFiltradoRepository;
        ICampaniaWhatsappFiltradoRepository IUnitOfWork.campaniaWhatsappFiltradoRepository
        {
            get
            {
                return _campaniaWhatsappFiltradoRepository ?? new CampaniaWhatsappFiltradoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICampaniaMailingFiltradoRepositorio _campaniaMailingFiltradoRepositorio;
        ICampaniaMailingFiltradoRepositorio IUnitOfWork.campaniaMailingFiltradoRepositorioa
        {
            get
            {
                return _campaniaMailingFiltradoRepositorio ?? new CampaniaMailingFiltradoRepositorio(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICampaniaGeneralRepositorio _campaniaGeneralRepositorio;
        ICampaniaGeneralRepositorio IUnitOfWork.campaniaGeneralRepositorio
        {
            get
            {
                return _campaniaGeneralRepositorio ?? new CampaniaGeneralRepositorio(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFurTipoPedidoRepository FurTipoPedidoRepository;
        IFurTipoPedidoRepository IUnitOfWork.FurTipoPedidoRepository
        {
            get
            {
                return FurTipoPedidoRepository ?? new FurTipoPedidoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IBeneficioLaboralPorPeriodoRepository BeneficioLaboralPorPeriodoRepository;
        IBeneficioLaboralPorPeriodoRepository IUnitOfWork.BeneficioLaboralPorPeriodoRepository
        {
            get
            {
                return BeneficioLaboralPorPeriodoRepository ?? new BeneficioLaboralPorPeriodoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFacebookFormularioLeadgenRepository _facebookFormularioLeadgenRepository;
        IFacebookFormularioLeadgenRepository IUnitOfWork.FacebookFormularioLeadgenRepository
        {
            get
            {
                return _facebookFormularioLeadgenRepository ?? new FacebookFormularioLeadgenRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFacebookFormularioLeadgenLogRepository _facebookFormularioLeadgenLogRepository;
        IFacebookFormularioLeadgenLogRepository IUnitOfWork.FacebookFormularioLeadgenLogRepository
        {
            get
            {
                return _facebookFormularioLeadgenLogRepository ?? new FacebookFormularioLeadgenLogRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IAsignacionAutomaticaTempRepository _asignacionAutomaticaTempRepository;
        IAsignacionAutomaticaTempRepository IUnitOfWork.AsignacionAutomaticaTempRepository
        {
            get
            {
                return _asignacionAutomaticaTempRepository ?? new AsignacionAutomaticaTempRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IAsignacionAutomaticaConfiguracionRepository _asignacionAutomaticaConfiguracionRepository;
        IAsignacionAutomaticaConfiguracionRepository IUnitOfWork.AsignacionAutomaticaConfiguracionRepository
        {
            get
            {
                return _asignacionAutomaticaConfiguracionRepository ?? new AsignacionAutomaticaConfiguracionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IBloqueHorarioProcesaOportunidadRepository _bloqueHorarioProcesaOportunidadRepository;
        IBloqueHorarioProcesaOportunidadRepository IUnitOfWork.BloqueHorarioProcesaOportunidadRepository
        {
            get
            {
                return _bloqueHorarioProcesaOportunidadRepository ?? new BloqueHorarioProcesaOportunidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IArticuloRepository _articuloRepository;
        IArticuloRepository IUnitOfWork.ArticuloRepository
        {
            get
            {
                return _articuloRepository ?? new ArticuloRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoArticuloRepository _tipoArticuloRepository;
        ITipoArticuloRepository IUnitOfWork.TipoArticuloRepository
        {
            get
            {
                return _tipoArticuloRepository ?? new TipoArticuloRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IArticuloSeoRepository _articuloSeoRepository;
        IArticuloSeoRepository IUnitOfWork.ArticuloSeoRepository
        {
            get
            {
                return _articuloSeoRepository ?? new ArticuloSeoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICategoriaProgramaRepository _categoriaProgramaRepository;
        ICategoriaProgramaRepository IUnitOfWork.CategoriaProgramaRepository
        {
            get
            {
                return _categoriaProgramaRepository ?? new CategoriaProgramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITagRepository _tagRepository;
        ITagRepository IUnitOfWork.TagRepository
        {
            get
            {
                return _tagRepository ?? new TagRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IArticuloTagRepository _articuloTagRepository;
        IArticuloTagRepository IUnitOfWork.ArticuloTagRepository
        {
            get
            {
                return _articuloTagRepository ?? new ArticuloTagRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IArticuloPGeneralRepository _articuloPGeneralRepository;
        IArticuloPGeneralRepository IUnitOfWork.ArticuloPGeneralRepository
        {
            get
            {
                return _articuloPGeneralRepository ?? new ArticuloPGeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICalidadProcesamientoRepository _calidadProcesamientoRepository;
        ICalidadProcesamientoRepository IUnitOfWork.CalidadProcesamientoRepository
        {
            get
            {
                return _calidadProcesamientoRepository ?? new CalidadProcesamientoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IOportunidadPrerequisitoGeneralRepository _oportunidadPrerequisitoGeneralRepository;
        IOportunidadPrerequisitoGeneralRepository IUnitOfWork.OportunidadPrerequisitoGeneralRepository
        {
            get
            {
                return _oportunidadPrerequisitoGeneralRepository ?? new OportunidadPrerequisitoGeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IOportunidadPrerequisitoEspecificoRepository _oportunidadPrerequisitoEspecificoRepository;
        IOportunidadPrerequisitoEspecificoRepository IUnitOfWork.OportunidadPrerequisitoEspecificoRepository
        {
            get
            {
                return _oportunidadPrerequisitoEspecificoRepository ?? new OportunidadPrerequisitoEspecificoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISolucionClienteByActividadRepository _solucionClienteByActividadRepository;
        ISolucionClienteByActividadRepository IUnitOfWork.SolucionClienteByActividadRepository
        {
            get
            {
                return _solucionClienteByActividadRepository ?? new SolucionClienteByActividadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IModeloPredictivoProbabilidadRepository _modeloPredictivoProbabilidadRepository;
        IModeloPredictivoProbabilidadRepository IUnitOfWork.ModeloPredictivoProbabilidadRepository
        {
            get
            {
                return _modeloPredictivoProbabilidadRepository ?? new ModeloPredictivoProbabilidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IWhatsAppConfiguracionEnvioRepository _whatsAppConfiguracionEnvioRepository;

        IWhatsAppConfiguracionEnvioRepository IUnitOfWork.WhatsAppConfiguracionEnvioRepository
        {
            get
            {
                return _whatsAppConfiguracionEnvioRepository ?? new WhatsAppConfiguracionEnvioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOportunidadBeneficioRepository _oportunidadBeneficioRepository;
        IOportunidadBeneficioRepository IUnitOfWork.OportunidadBeneficioRepository
        {
            get
            {
                return _oportunidadBeneficioRepository ?? new OportunidadBeneficioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IAreaCampaniaGeneralDetalleRepository _areaCampaniaGeneralDetalleRepository;
        IAreaCampaniaGeneralDetalleRepository IUnitOfWork.areaCampaniaGeneralDetalleRepository
        {
            get
            {
                return _areaCampaniaGeneralDetalleRepository ?? new AreaCampaniaGeneralDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICampaniaGeneralDetalleResponsableRepositorio campaniaGeneralDetalleResponsableRepositorio;
        ICampaniaGeneralDetalleResponsableRepositorio IUnitOfWork.campaniaGeneralDetalleResponsableRepositorio
        {
            get
            {
                return campaniaGeneralDetalleResponsableRepositorio ?? new CampaniaGeneralDetalleResponsableRepositorio(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPGeneralConfiguracionPlantillaRepository _pGeneralConfiguracionPlantillaRepository;
        IPGeneralConfiguracionPlantillaRepository IUnitOfWork.PGeneralConfiguracionPlantillaRepository
        {
            get
            {
                return _pGeneralConfiguracionPlantillaRepository ?? new PGeneralConfiguracionPlantillaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private CertificadoGeneradoAutomaticoRepository _certificadoGeneradoAutomaticoRepository;
        ICertificadoGeneradoAutomaticoRepository IUnitOfWork.CertificadoGeneradoAutomaticoRepository
        {
            get
            {
                return _certificadoGeneradoAutomaticoRepository ?? new CertificadoGeneradoAutomaticoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private CertificadoGeneradoAutomaticoContenidoRepository _certificadoGeneradoAutomaticoContenidoRepository;
        ICertificadoGeneradoAutomaticoContenidoRepository IUnitOfWork.CertificadoGeneradoAutomaticoContenidoRepository
        {
            get
            {
                return _certificadoGeneradoAutomaticoContenidoRepository ?? new CertificadoGeneradoAutomaticoContenidoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private TarifarioRepository _tarifarioRepository;
        ITarifarioRepository IUnitOfWork.TarifarioRepository
        {
            get
            {
                return _tarifarioRepository ?? new TarifarioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private MatriculaCabeceraDatosCertificadoRepository _matriculaCabeceraDatosCertificadoRepository;
        IMatriculaCabeceraDatosCertificadoRepository IUnitOfWork.MatriculaCabeceraDatosCertificadoRepository
        {
            get
            {
                return _matriculaCabeceraDatosCertificadoRepository ?? new MatriculaCabeceraDatosCertificadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ErrorRepository _errorRepository;
        IErrorRepository IUnitOfWork.ErrorRepository
        {
            get
            {
                return _errorRepository ?? new ErrorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IWhatsAppNumeroValidadoRepository _whatsAppNumeroValidadoRepository;
        IWhatsAppNumeroValidadoRepository IUnitOfWork.WhatsAppNumeroValidadoRepository
        {
            get
            {
                return _whatsAppNumeroValidadoRepository ?? new WhatsAppNumeroValidadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IWhatsAppDesuscritoRepository _whatsAppDesuscritoRepository;
        IWhatsAppDesuscritoRepository IUnitOfWork.WhatsAppDesuscritoRepository
        {
            get
            {
                return _whatsAppDesuscritoRepository ?? new WhatsAppDesuscritoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoIdentificadorRepository _tipoIdentificadorRepository;
        ITipoIdentificadorRepository IUnitOfWork.TipoIdentificadorRepository
        {
            get
            {
                return _tipoIdentificadorRepository ?? new TipoIdentificadorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITamanioEmpresaRepository _tamanioEmpresaRepository;
        ITamanioEmpresaRepository IUnitOfWork.TamanioEmpresaRepository
        {
            get
            {
                return _tamanioEmpresaRepository ?? new TamanioEmpresaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICodigoCiiuIndustriaRepository _codigoCiiuIndustriaRepository;
        ICodigoCiiuIndustriaRepository IUnitOfWork.CodigoCiiuIndustriaRepository
        {
            get
            {
                return _codigoCiiuIndustriaRepository ?? new CodigoCiiuIndustriaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICompromisoAlumnoRepository _compromisoAlumnoRepository;
        ICompromisoAlumnoRepository IUnitOfWork.CompromisoAlumnoRepository
        {
            get
            {
                return _compromisoAlumnoRepository ?? new CompromisoAlumnoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoReclamoAlumnoRepository _tipoReclamoAlumnoRepository;
        ITipoReclamoAlumnoRepository IUnitOfWork.TipoReclamoAlumnoRepository
        {
            get
            {
                return _tipoReclamoAlumnoRepository ?? new TipoReclamoAlumnoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPanelIngresoDisponibleRepository _panelIngresoDisponibleRepository;
        IPanelIngresoDisponibleRepository IUnitOfWork.PanelIngresoDisponibleRepository
        {
            get
            {
                return _panelIngresoDisponibleRepository ?? new PanelIngresoDisponibleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IDiaSemanaRepository _diaSemanaRepository;
        IDiaSemanaRepository IUnitOfWork.DiaSemanaRepository
        {
            get
            {
                return _diaSemanaRepository ?? new DiaSemanaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFormaPagoRepository _formaPagoRepository;
        IFormaPagoRepository IUnitOfWork.FormaPagoRepository
        {
            get
            {
                return _formaPagoRepository ?? new FormaPagoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IOcurrenciaActividadRepository _ocurrenciaActividadRepository;
        IOcurrenciaActividadRepository IUnitOfWork.OcurrenciaActividadRepository
        {
            get
            {
                return _ocurrenciaActividadRepository ?? new OcurrenciaActividadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConfiguracionEnvioAutomaticoRepository _configuracionEnvioAutomaticoRepository;
        IConfiguracionEnvioAutomaticoRepository IUnitOfWork.ConfiguracionEnvioAutomaticoRepository
        {
            get
            {
                return _configuracionEnvioAutomaticoRepository ?? new ConfiguracionEnvioAutomaticoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoEnvioAutomaticoRepository _tipoEnvioAutomaticoRepository;
        ITipoEnvioAutomaticoRepository IUnitOfWork.TipoEnvioAutomaticoRepository
        {
            get
            {
                return _tipoEnvioAutomaticoRepository ?? new TipoEnvioAutomaticoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConfiguracionEnvioAutomaticoDetalleRepository _configuracionEnvioAutomaticoDetalleRepository;
        IConfiguracionEnvioAutomaticoDetalleRepository IUnitOfWork.ConfiguracionEnvioAutomaticoDetalleRepository
        {
            get
            {
                return _configuracionEnvioAutomaticoDetalleRepository ?? new ConfiguracionEnvioAutomaticoDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IOtroMovimientoCajaRepository _otroMovimientoCajaRepository;
        IOtroMovimientoCajaRepository IUnitOfWork.OtroMovimientoCajaRepository
        {
            get
            {
                return _otroMovimientoCajaRepository ?? new OtroMovimientoCajaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoMovimientoCajaRepository _tipoMovimientoCajaRepository;
        ITipoMovimientoCajaRepository IUnitOfWork.TipoMovimientoCajaRepository
        {
            get
            {
                return _tipoMovimientoCajaRepository ?? new TipoMovimientoCajaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISubTipoMovimientoCajaRepository _subTipoMovimientoCajaRepository;
        ISubTipoMovimientoCajaRepository IUnitOfWork.SubTipoMovimientoCajaRepository
        {
            get
            {
                return _subTipoMovimientoCajaRepository ?? new SubTipoMovimientoCajaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IWhatsAppConfiguracionEnvioPorProgramaRepository _whatsAppConfiguracionEnvioPorProgramaRepository;
        IWhatsAppConfiguracionEnvioPorProgramaRepository IUnitOfWork.WhatsAppConfiguracionEnvioPorProgramaRepository
        {
            get
            {
                return _whatsAppConfiguracionEnvioPorProgramaRepository ?? new WhatsAppConfiguracionEnvioPorProgramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IRegistroRecuperacionWhatsAppRepository _registroRecuperacionWhatsAppRepository;
        IRegistroRecuperacionWhatsAppRepository IUnitOfWork.RegistroRecuperacionWhatsAppRepository
        {
            get
            {
                return _registroRecuperacionWhatsAppRepository ?? new RegistroRecuperacionWhatsAppRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IWhatsAppConfiguracionLogEjecucionRepository _whatsAppConfiguracionLogEjecucionRepository;
        IWhatsAppConfiguracionLogEjecucionRepository IUnitOfWork.WhatsAppConfiguracionLogEjecucionRepository
        {
            get
            {
                return _whatsAppConfiguracionLogEjecucionRepository ?? new WhatsAppConfiguracionLogEjecucionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IConfiguracionEnvioMailingDetalleRepository _configuracionEnvioMailingDetalleRepository;
        IConfiguracionEnvioMailingDetalleRepository IUnitOfWork.ConfiguracionEnvioMailingDetalleRepository
        {
            get
            {
                return _configuracionEnvioMailingDetalleRepository ?? new ConfiguracionEnvioMailingDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IFiltradoDeDatosPorPrioridadWhatsAppRepository _filtradoDeDatosPorPrioridadWhatsAppRepository;
        IFiltradoDeDatosPorPrioridadWhatsAppRepository IUnitOfWork.FiltradoDeDatosPorPrioridadWhatsAppRepository
        {
            get
            {
                return _filtradoDeDatosPorPrioridadWhatsAppRepository ?? new FiltradoDeDatosPorPrioridadWhatsAppRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPersonalEncargadoDeEnvioDeConsultumRepository _personalEncargadoDeEnvioDeConsultumRepository;
        IPersonalEncargadoDeEnvioDeConsultumRepository IUnitOfWork.PersonalEncargadoDeEnvioDeConsultumRepository
        {
            get
            {
                return _personalEncargadoDeEnvioDeConsultumRepository ?? new PersonalEncargadoDeEnvioDeConsultumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IConfiguracionDeEnvioParaWhatsAppRepository _configuracionDeEnvioParaWhatsAppRepository;
        IConfiguracionDeEnvioParaWhatsAppRepository IUnitOfWork.ConfiguracionDeEnvioParaWhatsAppRepository
        {
            get
            {
                return _configuracionDeEnvioParaWhatsAppRepository ?? new ConfiguracionDeEnvioParaWhatsAppRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IWhatsAppMensajePublicidadRepository _whatsAppMensajePublicidadRepository;
        IWhatsAppMensajePublicidadRepository IUnitOfWork.WhatsAppMensajePublicidadRepository
        {
            get
            {
                return _whatsAppMensajePublicidadRepository ?? new WhatsAppMensajePublicidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IRecuperacionAutomaticoModuloSistemaRepository _recuperacionAutomaticoModuloSistemaRepository;
        IRecuperacionAutomaticoModuloSistemaRepository IUnitOfWork.RecuperacionAutomaticoModuloSistemaRepository
        {
            get
            {
                return _recuperacionAutomaticoModuloSistemaRepository ?? new RecuperacionAutomaticoModuloSistemaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private INuevoAlumnoCongeladoRepository _nuevoAlumnoCongeladoRepository;
        INuevoAlumnoCongeladoRepository IUnitOfWork.NuevoAlumnoCongeladoRepository
        {
            get
            {
                return _nuevoAlumnoCongeladoRepository ?? new NuevoAlumnoCongeladoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISolicitudTipoReporteRepository _solicitudTipoReporteRepository;
        ISolicitudTipoReporteRepository IUnitOfWork.SolicitudTipoReporteRepository
        {
            get
            {
                return _solicitudTipoReporteRepository ?? new SolicitudTipoReporteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IControlSolicitudOrigenRepository _controlSolicitudOrigenRepository;
        IControlSolicitudOrigenRepository IUnitOfWork.ControlSolicitudOrigenRepository
        {
            get
            {
                return _controlSolicitudOrigenRepository ?? new ControlSolicitudOrigenRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IEstadoSolicitudBeneficioRepository _estadoSolicitudBeneficioRepository;
        IEstadoSolicitudBeneficioRepository IUnitOfWork.EstadoSolicitudBeneficioRepository
        {
            get
            {
                return _estadoSolicitudBeneficioRepository ?? new EstadoSolicitudBeneficioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISolicitudSubCategoriaRepository _solicitudSubCategoriaRepository;
        ISolicitudSubCategoriaRepository IUnitOfWork.SolicitudSubCategoriaRepository
        {
            get
            {
                return _solicitudSubCategoriaRepository ?? new SolicitudSubCategoriaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISolicitudCategoriaRepository _solicitudCategoriaRepository;
        ISolicitudCategoriaRepository IUnitOfWork.SolicitudCategoriaRepository
        {
            get
            {
                return _solicitudCategoriaRepository ?? new SolicitudCategoriaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IWhatsAppConfiguracionPreEnvioRepository _whatsAppConfiguracionPreEnvioRepository;
        IWhatsAppConfiguracionPreEnvioRepository IUnitOfWork.WhatsAppConfiguracionPreEnvioRepository
        {
            get
            {
                return _whatsAppConfiguracionPreEnvioRepository ?? new WhatsAppConfiguracionPreEnvioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IWhatsAppConfiguracionEnvioDetalleRepository _whatsAppConfiguracionEnvioDetalleRepository;
        IWhatsAppConfiguracionEnvioDetalleRepository IUnitOfWork.WhatsAppConfiguracionEnvioDetalleRepository
        {
            get
            {
                return _whatsAppConfiguracionEnvioDetalleRepository ?? new WhatsAppConfiguracionEnvioDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConjuntoListaResultadoRepository _conjuntoListaResultadoRepository;
        IConjuntoListaResultadoRepository IUnitOfWork.ConjuntoListaResultadoRepository
        {
            get
            {
                return _conjuntoListaResultadoRepository ?? new ConjuntoListaResultadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISolicitudRepository _solicitudRepository;
        ISolicitudRepository IUnitOfWork.SolicitudRepository
        {
            get
            {
                return _solicitudRepository ?? new SolicitudRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IAnuncioFacebookMetricaRepository _anuncioFacebookMetricaRepository;
        IAnuncioFacebookMetricaRepository IUnitOfWork.AnuncioFacebookMetricaRepository
        {
            get
            {
                return _anuncioFacebookMetricaRepository ?? new AnuncioFacebookMetricaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        IInteraccionPaginaRepository _interaccionPaginaRepository;
        IInteraccionPaginaRepository IUnitOfWork.InteraccionPaginaRepository
        {
            get
            {
                return _interaccionPaginaRepository ?? new InteraccionPaginaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        IFacebookCuentaPublicitariaRepository _facebookCuentaPublicitariaRepository;
        IFacebookCuentaPublicitariaRepository IUnitOfWork.FacebookCuentaPublicitariaRepository
        {
            get
            {
                return _facebookCuentaPublicitariaRepository ?? new FacebookCuentaPublicitariaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        ICampaniaFacebookRepository _campaniaFacebookRepository;
        ICampaniaFacebookRepository IUnitOfWork.CampaniaFacebookRepository
        {
            get
            {
                return _campaniaFacebookRepository ?? new CampaniaFacebookRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        IConjuntoAnuncioFacebookRepository _conjuntoAnuncioFacebookRepository;
        IConjuntoAnuncioFacebookRepository IUnitOfWork.ConjuntoAnuncioFacebookRepository
        {
            get
            {
                return _conjuntoAnuncioFacebookRepository ?? new ConjuntoAnuncioFacebookRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        IAnuncioFacebookRepository _anuncioFacebookRepository;
        IAnuncioFacebookRepository IUnitOfWork.AnuncioFacebookRepository
        {
            get
            {
                return _anuncioFacebookRepository ?? new AnuncioFacebookRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        IAutenticacionServicioExternoRepository _autenticacionServicioExternoRepository;
        IAutenticacionServicioExternoRepository IUnitOfWork.AutenticacionServicioExternoRepository
        {
            get
            {
                return _autenticacionServicioExternoRepository ?? new AutenticacionServicioExternoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }



        private IGastoFinancieroCronogramaRepository _gastoFinancieroCronogramaRepository;
        IGastoFinancieroCronogramaRepository IUnitOfWork.GastoFinancieroCronogramaRepository
        {
            get
            {
                return _gastoFinancieroCronogramaRepository ?? new GastoFinancieroCronogramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IGastoFinancieroCronogramaDetalleRepository _gastoFinancieroCronogramaDetalleRepository;
        IGastoFinancieroCronogramaDetalleRepository IUnitOfWork.GastoFinancieroCronogramaDetalleRepository
        {
            get
            {
                return _gastoFinancieroCronogramaDetalleRepository ?? new GastoFinancieroCronogramaDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISolicitudOperacionesAccesoTemporalDetalleRepository _solicitudOperacionesAccesoTemporalDetalleRepository;
        ISolicitudOperacionesAccesoTemporalDetalleRepository IUnitOfWork.SolicitudOperacionesAccesoTemporalDetalleRepository
        {
            get
            {
                return _solicitudOperacionesAccesoTemporalDetalleRepository ?? new SolicitudOperacionesAccesoTemporalDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICertificadoSolicitudRepository _certificadoSolicitudRepository;
        ICertificadoSolicitudRepository IUnitOfWork.CertificadoSolicitudRepository
        {
            get
            {
                return _certificadoSolicitudRepository ?? new CertificadoSolicitudRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICertificadoTipoRepository _certificadoTipoRepository;
        ICertificadoTipoRepository IUnitOfWork.CertificadoTipoRepository
        {
            get
            {
                return _certificadoTipoRepository ?? new CertificadoTipoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICertificadoTipoProgramaRepository _certificadoTipoProgramaRepository;
        ICertificadoTipoProgramaRepository IUnitOfWork.CertificadoTipoProgramaRepository
        {
            get
            {
                return _certificadoTipoProgramaRepository ?? new CertificadoTipoProgramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoComprobanteRepository _tipoComprobanteRepository;
        ITipoComprobanteRepository IUnitOfWork.TipoComprobanteRepository
        {
            get
            {
                return _tipoComprobanteRepository ?? new TipoComprobanteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISedeRepository _SedeRepository;
        ISedeRepository IUnitOfWork.SedeRepository
        {
            get
            {
                return _SedeRepository ?? new SedeRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFurTipoSolicitudRepository _FurTipoSolicitudRepository;
        IFurTipoSolicitudRepository IUnitOfWork.FurTipoSolicitudRepository
        {
            get
            {
                return _FurTipoSolicitudRepository ?? new FurTipoSolicitudRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IVerificacionManualDatosRepository _verificacionManualDatosRepository;
        IVerificacionManualDatosRepository IUnitOfWork.VerificacionManualDatosRepository
        {
            get
            {
                return _verificacionManualDatosRepository ?? new VerificacionManualDatosRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFiltroSegmentoTipoContactoRepository _filtroSegmentoTipoContactoRepository;
        IFiltroSegmentoTipoContactoRepository IUnitOfWork.FiltroSegmentoTipoContactoRepository
        {
            get
            {
                return _filtroSegmentoTipoContactoRepository ?? new FiltroSegmentoTipoContactoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoFormularioRepository _tipoFormularioRepository;
        ITipoFormularioRepository IUnitOfWork.TipoFormularioRepository
        {
            get
            {
                return _tipoFormularioRepository ?? new TipoFormularioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITiempoFrecuenciaRepository _tiempoFrecuenciaRepository;
        ITiempoFrecuenciaRepository IUnitOfWork.TiempoFrecuenciaRepository
        {
            get
            {
                return _tiempoFrecuenciaRepository ?? new TiempoFrecuenciaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private EstadoActividadDetalleRepository _estadoActividadDetalleRepository;
        IEstadoActividadDetalleRepository IUnitOfWork.EstadoActividadDetalleRepository
        {
            get
            {
                return _estadoActividadDetalleRepository ?? new EstadoActividadDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private EmbudoNivelRepository _embudoNivelRepository;
        IEmbudoNivelRepository IUnitOfWork.EmbudoNivelRepository
        {
            get
            {
                return _embudoNivelRepository ?? new EmbudoNivelRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private EstadoPagoMatriculaRepository _estadoPagoMatriculaRepository;
        IEstadoPagoMatriculaRepository IUnitOfWork.EstadoPagoMatriculaRepository
        {
            get
            {
                return _estadoPagoMatriculaRepository ?? new EstadoPagoMatriculaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }



        private CriterioDocRepository _criterioDocRepository;
        ICriterioDocRepository IUnitOfWork.CriterioDocRepository
        {
            get
            {
                return _criterioDocRepository ?? new CriterioDocRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ActividadCabeceraRepository _actividadCabeceraRepository;
        IActividadCabeceraRepository IUnitOfWork.ActividadCabeceraRepository
        {
            get
            {
                return _actividadCabeceraRepository ?? new ActividadCabeceraRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private LogFiltroSegmentoEjecutadoRepository _logFiltroSegmentoEjecutadoRepository;
        ILogFiltroSegmentoEjecutadoRepository IUnitOfWork.LogFiltroSegmentoEjecutadoRepository
        {
            get
            {
                return _logFiltroSegmentoEjecutadoRepository ?? new LogFiltroSegmentoEjecutadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private FacebookAudienciaRepository _facebookAudienciaRepository;
        IFacebookAudienciaRepository IUnitOfWork.FacebookAudienciaRepository
        {
            get
            {
                return _facebookAudienciaRepository ?? new FacebookAudienciaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }





        private IDocumentoPagoRepository _documentoPagoRepository;
        IDocumentoPagoRepository IUnitOfWork.DocumentoPagoRepository
        {
            get
            {
                return _documentoPagoRepository ?? new DocumentoPagoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }



        private IControlDocRepository _controlDocRepository;
        IControlDocRepository IUnitOfWork.ControlDocRepository
        {
            get
            {
                return _controlDocRepository ?? new ControlDocRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private ICronogramaDetalleCambioRepository _cronogramaDetalleCambioRepository;
        ICronogramaDetalleCambioRepository IUnitOfWork.CronogramaDetalleCambioRepository
        {
            get
            {
                return _cronogramaDetalleCambioRepository ?? new CronogramaDetalleCambioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICronogramaCabeceraCambioRepository _cronogramaCabeceraCambioRepository;
        ICronogramaCabeceraCambioRepository IUnitOfWork.CronogramaCabeceraCambioRepository
        {
            get
            {
                return _cronogramaCabeceraCambioRepository ?? new CronogramaCabeceraCambioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICronogramaPagoDetalleModLogFinalRepository _cronogramaPagoDetalleModLogFinalRepository;
        ICronogramaPagoDetalleModLogFinalRepository IUnitOfWork.CronogramaPagoDetalleModLogFinalRepository
        {
            get
            {
                return _cronogramaPagoDetalleModLogFinalRepository ?? new CronogramaPagoDetalleModLogFinalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private ICronogramaPagoDetalleModRepository _cronogramaPagoDetalleModRepository;
        ICronogramaPagoDetalleModRepository IUnitOfWork.CronogramaPagoDetalleModRepository
        {
            get
            {
                return _cronogramaPagoDetalleModRepository ?? new CronogramaPagoDetalleModRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICronogramaPagoDetalleOriginalRepository _cronogramaPagoDetalleOriginalRepository;
        ICronogramaPagoDetalleOriginalRepository IUnitOfWork.CronogramaPagoDetalleOriginalRepository
        {
            get
            {
                return _cronogramaPagoDetalleOriginalRepository ?? new CronogramaPagoDetalleOriginalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }



        private IMatriculaDetalleRepository _matriculaDetalleRepository;
        IMatriculaDetalleRepository IUnitOfWork.MatriculaDetalleRepository
        {
            get
            {
                return _matriculaDetalleRepository ?? new MatriculaDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICronogramaPagoDetalleRepository _cronogramaPagoDetalleRepository;
        ICronogramaPagoDetalleRepository IUnitOfWork.CronogramaPagoDetalleRepository
        {
            get
            {
                return _cronogramaPagoDetalleRepository ?? new CronogramaPagoDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPagoFinalRepository _pagoFinalRepository;
        IPagoFinalRepository IUnitOfWork.PagoFinalRepository
        {
            get
            {
                return _pagoFinalRepository ?? new PagoFinalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPagoRepository _pagoRepository;
        IPagoRepository IUnitOfWork.PagoRepository
        {
            get
            {
                return _pagoRepository ?? new PagoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IReporteTipoDeCambioFinancieroMensualRepository _ReporteTipoDeCambioFinancieroMensualRepository;
        IReporteTipoDeCambioFinancieroMensualRepository IUnitOfWork.ReporteTipoDeCambioFinancieroMensualRepository
        {
            get
            {
                return _ReporteTipoDeCambioFinancieroMensualRepository ?? new ReporteTipoDeCambioFinancieroMensualRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IActividadCrepLogRepository _ActividadCrepLogRepository;
        IActividadCrepLogRepository IUnitOfWork.ActividadCrepLogRepository
        {
            get
            {
                return _ActividadCrepLogRepository ?? new ActividadCrepLogRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFiltroSegmentoRepository _FiltroSegmentoRepository;
        IFiltroSegmentoRepository IUnitOfWork.FiltroSegmentoRepository
        {
            get
            {
                return _FiltroSegmentoRepository ?? new FiltroSegmentoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IFiltroSegmentoValorTipoRepository _FiltroSegmentoValorTipoRepository;
        IFiltroSegmentoValorTipoRepository IUnitOfWork.FiltroSegmentoValorTipoRepository
        {
            get
            {
                return _FiltroSegmentoValorTipoRepository ?? new FiltroSegmentoValorTipoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IFiltroSegmentoDetalleRepository _FiltroSegmentoDetalleRepository;
        IFiltroSegmentoDetalleRepository IUnitOfWork.FiltroSegmentoDetalleRepository
        {
            get
            {
                return _FiltroSegmentoDetalleRepository ?? new FiltroSegmentoDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private ITipoCambioEntreMonedaRepository _tipoCambioEntreMonedaRepository;
        ITipoCambioEntreMonedaRepository IUnitOfWork.TipoCambioEntreMonedaRepository
        {
            get
            {
                return _tipoCambioEntreMonedaRepository ?? new TipoCambioEntreMonedaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IPespecificoPadrePespecificoHijoRepository _pespecificoPadrePespecificoHijoRepository;
        IPespecificoPadrePespecificoHijoRepository IUnitOfWork.PespecificoPadrePespecificoHijoRepository
        {
            get
            {
                return _pespecificoPadrePespecificoHijoRepository ?? new PespecificoPadrePespecificoHijoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IMoodleCursoRepository _moodleCursoRepository;
        IMoodleCursoRepository IUnitOfWork.MoodleCursoRepository
        {
            get
            {
                return _moodleCursoRepository ?? new MoodleCursoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }



        private IFeriadoRepository _feriadoRepository;
        IFeriadoRepository IUnitOfWork.FeriadoRepository
        {
            get
            {
                return _feriadoRepository ?? new FeriadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private ISeccionPreguntaFrecuenteRepository _seccionPreguntaFrecuenteRepository;
        ISeccionPreguntaFrecuenteRepository IUnitOfWork.SeccionPreguntaFrecuenteRepository
        {
            get
            {
                return _seccionPreguntaFrecuenteRepository ?? new SeccionPreguntaFrecuenteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IPreguntaFrecuenteAreaRepository _preguntaFrecuenteAreaRepository;
        IPreguntaFrecuenteAreaRepository IUnitOfWork.PreguntaFrecuenteAreaRepository
        {
            get
            {
                return _preguntaFrecuenteAreaRepository ?? new PreguntaFrecuenteAreaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IPreguntaFrecuenteSubAreaRepository _preguntaFrecuenteSubAreaRepository;
        IPreguntaFrecuenteSubAreaRepository IUnitOfWork.PreguntaFrecuenteSubAreaRepository
        {
            get
            {
                return _preguntaFrecuenteSubAreaRepository ?? new PreguntaFrecuenteSubAreaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }



        private IPreguntaFrecuenteTipoRepository _preguntaFrecuenteTipoRepository;
        IPreguntaFrecuenteTipoRepository IUnitOfWork.PreguntaFrecuenteTipoRepository
        {
            get
            {
                return _preguntaFrecuenteTipoRepository ?? new PreguntaFrecuenteTipoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }



        private IPreguntaFrecuenteRepository _preguntaFrecuenteRepository;
        IPreguntaFrecuenteRepository IUnitOfWork.PreguntaFrecuenteRepository
        {
            get
            {
                return _preguntaFrecuenteRepository ?? new PreguntaFrecuenteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IFacebookAudienciumRepository _facebookAudienciumRepository;
        IFacebookAudienciumRepository IUnitOfWork.FacebookAudienciumRepository
        {
            get
            {
                return _facebookAudienciumRepository ?? new FacebookAudienciumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFacebookAudienciaAlumnoRepository _facebookAudienciaAlumnoRepository;
        IFacebookAudienciaAlumnoRepository IUnitOfWork.FacebookAudienciaAlumnoRepository
        {
            get
            {
                return _facebookAudienciaAlumnoRepository ?? new FacebookAudienciaAlumnoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IFacebookAudienciaCuentaPublicitariumRepository _facebookAudienciaCuentaPublicitariumRepository;
        IFacebookAudienciaCuentaPublicitariumRepository IUnitOfWork.FacebookAudienciaCuentaPublicitariumRepository
        {
            get
            {
                return _facebookAudienciaCuentaPublicitariumRepository ?? new FacebookAudienciaCuentaPublicitariumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEstructuraEspecificaTareaRepository _estructuraEspecificaTareaRepository;
        IEstructuraEspecificaTareaRepository IUnitOfWork.EstructuraEspecificaTareaRepository
        {
            get
            {
                return _estructuraEspecificaTareaRepository ?? new EstructuraEspecificaTareaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISolicitudAlumnoRepository _oolicitudAlumnoRepository;
        ISolicitudAlumnoRepository IUnitOfWork.SolicitudAlumnoRepository
        {
            get
            {
                return _oolicitudAlumnoRepository ?? new SolicitudAlumnoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISolicitudInternaRepository _oolicitudInternaRepository;
        ISolicitudInternaRepository IUnitOfWork.SolicitudInternaRepository
        {
            get
            {
                return _oolicitudInternaRepository ?? new SolicitudInternaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISendinBlueDataDeEventoRepository _SendinBlueDataDeEventoRepository;
        ISendinBlueDataDeEventoRepository IUnitOfWork.SendinBlueDataDeEventoRepository
        {
            get
            {
                return _SendinBlueDataDeEventoRepository ?? new SendinBlueDataDeEventoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IConfiguracionProyeccionFurRepository _configuracionProyeccionFurRepository;
        IConfiguracionProyeccionFurRepository IUnitOfWork.ConfiguracionProyeccionFurRepository
        {
            get
            {
                return _configuracionProyeccionFurRepository ?? new ConfiguracionProyeccionFurRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IPeriodoMesProyeccionRepository _periodoMesProyeccionRepository;
        IPeriodoMesProyeccionRepository IUnitOfWork.PeriodoMesProyeccionRepository
        {
            get
            {
                return _periodoMesProyeccionRepository ?? new PeriodoMesProyeccionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IEstadoProyeccionFurRepository _estadoProyeccionFurRepository;
        IEstadoProyeccionFurRepository IUnitOfWork.EstadoProyeccionFurRepository
        {
            get
            {
                return _estadoProyeccionFurRepository ?? new EstadoProyeccionFurRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICabeceraFurConfiguracionAutomaticaRepository _cabeceraFurConfiguracionAutomaticaRepository;
        ICabeceraFurConfiguracionAutomaticaRepository IUnitOfWork.CabeceraFurConfiguracionAutomaticaRepository
        {
            get
            {
                return _cabeceraFurConfiguracionAutomaticaRepository ?? new CabeceraFurConfiguracionAutomaticaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFurConfiguracionAutomaticaRepository _furConfiguracionAutomaticaRepository;
        IFurConfiguracionAutomaticaRepository IUnitOfWork.FurConfiguracionAutomaticaRepository
        {
            get
            {
                return _furConfiguracionAutomaticaRepository ?? new FurConfiguracionAutomaticaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFrecuenciaRepository _frecuenciaRepository;
        IFrecuenciaRepository IUnitOfWork.FrecuenciaRepository
        {
            get
            {
                return _frecuenciaRepository ?? new FrecuenciaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ILogProyeccionFurRepository _logProyeccionFurRepository;
        ILogProyeccionFurRepository IUnitOfWork.LogProyeccionFurRepository
        {
            get
            {
                return _logProyeccionFurRepository ?? new LogProyeccionFurRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICongelamientoProyeccionFurRepository _congelamientoProyeccionFurRepository;
        ICongelamientoProyeccionFurRepository IUnitOfWork.CongelamientoProyeccionFurRepository
        {
            get
            {
                return _congelamientoProyeccionFurRepository ?? new CongelamientoProyeccionFurRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IReporteResumenMontoRepository _reporteResumenMontoRepository;
        IReporteResumenMontoRepository IUnitOfWork.ReporteResumenMontoRepository
        {
            get
            {
                return _reporteResumenMontoRepository ?? new ReporteResumenMontoRepository(_dapperRepository);
            }
        }

        private ILlamadaWebphoneCruceCentralRepository _llamadaWebphoneCruceCentralRepository;
        ILlamadaWebphoneCruceCentralRepository IUnitOfWork.LlamadaWebphoneCruceCentralRepository
        {
            get
            {
                return _llamadaWebphoneCruceCentralRepository ?? new LlamadaWebphoneCruceCentralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISendinBlueEventoWebHookRepository _sendinBlueEventoWebHookRepository;
        ISendinBlueEventoWebHookRepository IUnitOfWork.SendinBlueEventoWebHookRepository
        {
            get
            {
                return _sendinBlueEventoWebHookRepository ?? new SendinBlueEventoWebHookRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISendinblueRelacionListaContactoEmailRepository _sendinblueRelacionListaContactoEmailRepository;
        ISendinblueRelacionListaContactoEmailRepository IUnitOfWork.SendinblueRelacionListaContactoEmailRepository
        {
            get
            {
                return _sendinblueRelacionListaContactoEmailRepository ?? new SendinblueRelacionListaContactoEmailRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMaterialAccionRepository _materialAccionRepository;
        IMaterialAccionRepository IUnitOfWork.MaterialAccionRepository
        {
            get
            {
                return _materialAccionRepository ?? new MaterialAccionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IAreaCcRepository _areaCentroCostoRepository;
        IAreaCcRepository IUnitOfWork.AreaCentroCostoRepository
        {
            get
            {
                return _areaCentroCostoRepository ?? new AreaCcRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMaterialCriterioVerificacionRepository _materialCriterioVerificacionRepository;
        IMaterialCriterioVerificacionRepository IUnitOfWork.MaterialCriterioVerificacionRepository
        {
            get
            {
                return _materialCriterioVerificacionRepository ?? new MaterialCriterioVerificacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IReporteLibroReclamacionRepository _reporteLibroReclamacionRepository;
        IReporteLibroReclamacionRepository IUnitOfWork.ReporteLibroReclamacionRepository
        {
            get
            {
                return _reporteLibroReclamacionRepository ?? new ReporteLibroReclamacionRepository(_dapperRepository);
            }
        }
        private IMoodleCategoriaTipoRepository _moodleCategoriaTipoRepository;
        IMoodleCategoriaTipoRepository IUnitOfWork.MoodleCategoriaTipoRepository
        {
            get
            {
                return _moodleCategoriaTipoRepository ?? new MoodleCategoriaTipoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMoodleCategoriaRepository _moodleCategoriaRepository;
        IMoodleCategoriaRepository IUnitOfWork.MoodleCategoriaRepository
        {
            get
            {
                return _moodleCategoriaRepository ?? new MoodleCategoriaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IParametroSeoPwRepository _parametroSeoPwRepository;
        IParametroSeoPwRepository IUnitOfWork.ParametroSeoPwRepository
        {
            get
            {
                return _parametroSeoPwRepository ?? new ParametroSeoPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IAmbienteRepository _ambienteRepository;
        IAmbienteRepository IUnitOfWork.AmbienteRepository
        {
            get
            {
                return _ambienteRepository ?? new AmbienteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IOrigenProgramaRepository _origenProgramaRepository;
        IOrigenProgramaRepository IUnitOfWork.OrigenProgramaRepository
        {
            get
            {
                return _origenProgramaRepository ?? new OrigenProgramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ILocacionRepository _locacionRepository;
        ILocacionRepository IUnitOfWork.LocacionRepository
        {
            get
            {
                return _locacionRepository ?? new LocacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEstadoPespecificoRepository _estadoPespecificoRepository;
        IEstadoPespecificoRepository IUnitOfWork.EstadoPespecificoRepository
        {
            get
            {
                return _estadoPespecificoRepository ?? new EstadoPespecificoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISubAreaParametroSeoPwRepository _subAreaParametroSeoPwRepository;
        ISubAreaParametroSeoPwRepository IUnitOfWork.SubAreaParametroSeoPwRepository
        {
            get
            {
                return _subAreaParametroSeoPwRepository ?? new SubAreaParametroSeoPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISubNivelCcRepository _subNivelCcRepository;
        ISubNivelCcRepository IUnitOfWork.SubNivelCcRepository
        {
            get
            {
                return _subNivelCcRepository ?? new SubNivelCcRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConjuntoListaRepository _conjuntoListaRepository;
        IConjuntoListaRepository IUnitOfWork.ConjuntoListaRepository
        {
            get
            {
                return _conjuntoListaRepository ?? new ConjuntoListaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConjuntoListaDetalleRepository _conjuntoListaDetalleRepository;
        IConjuntoListaDetalleRepository IUnitOfWork.ConjuntoListaDetalleRepository
        {
            get
            {
                return _conjuntoListaDetalleRepository ?? new ConjuntoListaDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConjuntoListaDetalleValorRepository _conjuntoListaDetalleValorRepository;
        IConjuntoListaDetalleValorRepository IUnitOfWork.ConjuntoListaDetalleValorRepository
        {
            get
            {
                return _conjuntoListaDetalleValorRepository ?? new ConjuntoListaDetalleValorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IModuloSistemaV5Repository _moduloSistemaV5Repository;
        IModuloSistemaV5Repository IUnitOfWork.ModuloSistemaV5Repository
        {
            get
            {
                return _moduloSistemaV5Repository ?? new ModuloSistemaV5Repository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFaseByPlantillaRepository _faseByPlantillaRepository;
        IFaseByPlantillaRepository IUnitOfWork.FaseByPlantillaRepository
        {
            get
            {
                return _faseByPlantillaRepository ?? new FaseByPlantillaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPlantillaAsociacionModuloSistemaRepository _plantillaAsociacionModuloSistemaRepository;
        IPlantillaAsociacionModuloSistemaRepository IUnitOfWork.PlantillaAsociacionModuloSistemaRepository
        {
            get
            {
                return _plantillaAsociacionModuloSistemaRepository ?? new PlantillaAsociacionModuloSistemaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDiferenciaHorariaRepository _diferenciaHorariaRepository;
        IDiferenciaHorariaRepository IUnitOfWork.DiferenciaHorariaRepository
        {
            get
            {
                return _diferenciaHorariaRepository ?? new DiferenciaHorariaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPespecificoCronogramaRepository _pEspecificoCronogramaRepository;
        IPespecificoCronogramaRepository IUnitOfWork.PespecificoCronogramaRepository
        {
            get
            {
                return _pEspecificoCronogramaRepository ?? new PespecificoCronogramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPespecificoCronogramaGrupoRepository _pespecificoCronogramaGrupoRepository;
        IPespecificoCronogramaGrupoRepository IUnitOfWork.PespecificoCronogramaGrupoRepository
        {
            get
            {
                return _pespecificoCronogramaGrupoRepository ?? new PespecificoCronogramaGrupoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICursoPespecificoRepository _cursoPespecificoRepository;
        ICursoPespecificoRepository IUnitOfWork.CursoPespecificoRepository
        {
            get
            {
                return _cursoPespecificoRepository ?? new CursoPespecificoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICriterioEvaluacionCategoriumRepository _criterioEvaluacionCategoriumRepository;
        ICriterioEvaluacionCategoriumRepository IUnitOfWork.CriterioEvaluacionCategoriumRepository
        {
            get
            {
                return _criterioEvaluacionCategoriumRepository ?? new CriterioEvaluacionCategoriumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IEvaluacionCategoriumRepository _evaluacionCategoriumRepository;
        IEvaluacionCategoriumRepository IUnitOfWork.EvaluacionCategoriumRepository
        {
            get
            {
                return _evaluacionCategoriumRepository ?? new EvaluacionCategoriumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IAreaParametroSeoPwRepository _areaParametroSeoPwRepository;
        IAreaParametroSeoPwRepository IUnitOfWork.AreaParametroSeoPwRepository
        {
            get
            {
                return _areaParametroSeoPwRepository ?? new AreaParametroSeoPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFormulaTipoDescuentoRepository _formulaTipoDescuentoRepository;

        IFormulaTipoDescuentoRepository IUnitOfWork.FormulaTipoDescuentoRepository
        {
            get

            {
                return _formulaTipoDescuentoRepository ?? new FormulaTipoDescuentoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IAgendaTipoUsuarioRepository _agendaTipoUsuarioRepository;
        IAgendaTipoUsuarioRepository IUnitOfWork.AgendaTipoUsuarioRepository
        {
            get
            {
                return _agendaTipoUsuarioRepository ?? new AgendaTipoUsuarioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoDescuentoAsesorCoordinadorPwRepository _tipoDescuentoAsesorCoordinadorPwRepository;
        ITipoDescuentoAsesorCoordinadorPwRepository IUnitOfWork.TipoDescuentoAsesorCoordinadorPwRepository
        {
            get
            {
                return _tipoDescuentoAsesorCoordinadorPwRepository ?? new TipoDescuentoAsesorCoordinadorPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IFeedbackTipoRepository _feedbackTipoRepository;
        IFeedbackTipoRepository IUnitOfWork.FeedbackTipoRepository
        {
            get
            {
                return _feedbackTipoRepository ?? new FeedbackTipoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoDocumentoAlumnoRepository _tipoDocumentoAlumnoRepository;
        ITipoDocumentoAlumnoRepository IUnitOfWork.TipoDocumentoAlumnoRepository
        {
            get
            {
                return _tipoDocumentoAlumnoRepository ?? new TipoDocumentoAlumnoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoDocumentoAlumnoModalidadCursoRepository _tipoDocumentoAlumnoModalidadCursoRepository;
        ITipoDocumentoAlumnoModalidadCursoRepository IUnitOfWork.TipoDocumentoAlumnoModalidadCursoRepository
        {
            get
            {
                return _tipoDocumentoAlumnoModalidadCursoRepository ?? new TipoDocumentoAlumnoModalidadCursoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoDocumentoAlumnoEstadoMatriculaRepository _tipoDocumentoAlumnoEstadoMatriculaRepository;
        ITipoDocumentoAlumnoEstadoMatriculaRepository IUnitOfWork.TipoDocumentoAlumnoEstadoMatriculaRepository
        {
            get
            {
                return _tipoDocumentoAlumnoEstadoMatriculaRepository ?? new TipoDocumentoAlumnoEstadoMatriculaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoDocumentoAlumnoSubEstadoMatriculaRepository _tipoDocumentoAlumnoSubEstadoMatriculaRepository;
        ITipoDocumentoAlumnoSubEstadoMatriculaRepository IUnitOfWork.TipoDocumentoAlumnoSubEstadoMatriculaRepository
        {
            get
            {
                return _tipoDocumentoAlumnoSubEstadoMatriculaRepository ?? new TipoDocumentoAlumnoSubEstadoMatriculaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoDocumentoAlumnoPgeneralRepository _tipoDocumentoAlumnoPgeneralRepository;
        ITipoDocumentoAlumnoPgeneralRepository IUnitOfWork.TipoDocumentoAlumnoPgeneralRepository
        {
            get
            {
                return _tipoDocumentoAlumnoPgeneralRepository ?? new TipoDocumentoAlumnoPgeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMaterialEstadoRepository _materialEstadoRepository;
        IMaterialEstadoRepository IUnitOfWork.MaterialEstadoRepository
        {
            get
            {
                return _materialEstadoRepository ?? new MaterialEstadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoPersonaRepository _tipoPersonaRepository;
        ITipoPersonaRepository IUnitOfWork.TipoPersonaRepository
        {
            get
            {
                return _tipoPersonaRepository ?? new TipoPersonaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITagPwRepository _tagPwRepository;
        ITagPwRepository IUnitOfWork.TagPwRepository
        {
            get
            {
                return _tagPwRepository ?? new TagPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEscalaCalificacionRepository _escalaCalificacionRepository;
        IEscalaCalificacionRepository IUnitOfWork.EscalaCalificacionRepository
        {
            get
            {
                return _escalaCalificacionRepository ?? new EscalaCalificacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEscalaCalificacionDetalleRepository _escalaCalificacionDetalleRepository;
        IEscalaCalificacionDetalleRepository IUnitOfWork.EscalaCalificacionDetalleRepository
        {
            get
            {
                return _escalaCalificacionDetalleRepository ?? new EscalaCalificacionDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPgeneralTagsPwRepository _pGeneralTagsPwRepository;
        IPgeneralTagsPwRepository IUnitOfWork.PgeneralTagsPwRepository
        {
            get
            {
                return _pGeneralTagsPwRepository ?? new PgeneralTagsPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDescuentoPromocionRepository _descuentoPromocionRepository;
        IDescuentoPromocionRepository IUnitOfWork.DescuentoPromocionRepository
        {
            get
            {
                return _descuentoPromocionRepository ?? new DescuentoPromocionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IFeedbackConfigurarGrupoPreguntaRepository _feedbackConfigurarGrupoPreguntaRepository;
        IFeedbackConfigurarGrupoPreguntaRepository IUnitOfWork.FeedbackConfigurarGrupoPreguntaRepository
        {
            get
            {
                return _feedbackConfigurarGrupoPreguntaRepository ?? new FeedbackConfigurarGrupoPreguntaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IFeedbackGrupoPreguntaProgramaEspecificoRepository _feedbackGrupoPreguntaProgramaEspecificoRepository;
        IFeedbackGrupoPreguntaProgramaEspecificoRepository IUnitOfWork.FeedbackGrupoPreguntaProgramaEspecificoRepository
        {
            get
            {
                return _feedbackGrupoPreguntaProgramaEspecificoRepository ?? new FeedbackGrupoPreguntaProgramaEspecificoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IFeedbackGrupoPreguntaProgramaGeneralRepository _feedbackGrupoPreguntaProgramaGeneralRepository;
        IFeedbackGrupoPreguntaProgramaGeneralRepository IUnitOfWork.FeedbackGrupoPreguntaProgramaGeneralRepository
        {
            get
            {
                return _feedbackGrupoPreguntaProgramaGeneralRepository ?? new FeedbackGrupoPreguntaProgramaGeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICategoriaCiudadRepository _categoriaCiudadRepository;
        ICategoriaCiudadRepository IUnitOfWork.CategoriaCiudadRepository
        {
            get
            {
                return _categoriaCiudadRepository ?? new CategoriaCiudadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPespecificoParticipacionExpositorRepository _pEspecificoParticipacionExpositorRepository;
        IPespecificoParticipacionExpositorRepository IUnitOfWork.PespecificoParticipacionExpositorRepository
        {
            get
            {
                return _pEspecificoParticipacionExpositorRepository ?? new PespecificoParticipacionExpositorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPespecificoFrecuenciaRepository _pEspecificoFrecuenciaRepository;
        IPespecificoFrecuenciaRepository IUnitOfWork.PespecificoFrecuenciaRepository
        {
            get
            {
                return _pEspecificoFrecuenciaRepository ?? new PespecificoFrecuenciaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConfigurarWebinarRepository _configurarWebinarRepository;
        IConfigurarWebinarRepository IUnitOfWork.ConfigurarWebinarRepository
        {
            get
            {
                return _configurarWebinarRepository ?? new ConfigurarWebinarRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPespecificoPadreFrecuenciaRepository _pEspecificoPadreFrecuenciaRepository;
        IPespecificoPadreFrecuenciaRepository IUnitOfWork.PespecificoPadreFrecuenciaRepository
        {
            get
            {
                return _pEspecificoPadreFrecuenciaRepository ?? new PespecificoPadreFrecuenciaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPEspecificoPadreFrecuenciaSesionRepository _pEspecificoPadreFrecuenciaSesionRepository;
        IPEspecificoPadreFrecuenciaSesionRepository IUnitOfWork.PEspecificoPadreFrecuenciaSesionRepository
        {
            get
            {
                return _pEspecificoPadreFrecuenciaSesionRepository ?? new PEspecificoPadreFrecuenciaSesionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFeedbackConfigurarRepository _feedbackConfigurarRepository;
        IFeedbackConfigurarRepository IUnitOfWork.FeedbackConfigurarRepository
        {
            get
            {
                return _feedbackConfigurarRepository ?? new FeedbackConfigurarRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPespecificoFrecuenciaDetalleRepository _pEspecificoFrecuenciaDetalleRepository;
        IPespecificoFrecuenciaDetalleRepository IUnitOfWork.PespecificoFrecuenciaDetalleRepository
        {
            get
            {
                return _pEspecificoFrecuenciaDetalleRepository ?? new PespecificoFrecuenciaDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMaterialTipoRepository _materialTipoRepository;
        IMaterialTipoRepository IUnitOfWork.MaterialTipoRepository
        {
            get
            {
                return _materialTipoRepository ?? new MaterialTipoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMaterialAsociacionAccionRepository _materialAsociacionAccionRepository;
        IMaterialAsociacionAccionRepository IUnitOfWork.MaterialAsociacionAccionRepository
        {
            get
            {
                return _materialAsociacionAccionRepository ?? new MaterialAsociacionAccionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMaterialAsociacionVersionRepository _materialAsociacionVersionRepository;
        IMaterialAsociacionVersionRepository IUnitOfWork.MaterialAsociacionVersionRepository
        {
            get
            {
                return _materialAsociacionVersionRepository ?? new MaterialAsociacionVersionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMaterialAsociacionCriterioVerificacionRepository _materialAsociacionCriterioVerificacionRepository;
        IMaterialAsociacionCriterioVerificacionRepository IUnitOfWork.MaterialAsociacionCriterioVerificacionRepository
        {
            get
            {
                return _materialAsociacionCriterioVerificacionRepository ?? new MaterialAsociacionCriterioVerificacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IFormaCalificacionEvaluacionRepository _formaCalificacionEvaluacionRepository;
        IFormaCalificacionEvaluacionRepository IUnitOfWork.FormaCalificacionEvaluacionRepository
        {
            get
            {
                return _formaCalificacionEvaluacionRepository ?? new FormaCalificacionEvaluacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoProgramaRepository _tipoProgramaRepository;
        ITipoProgramaRepository IUnitOfWork.TipoProgramaRepository
        {
            get
            {
                return _tipoProgramaRepository ?? new TipoProgramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICriterioEvaluacionRepository _criterioEvaluacionRepository;
        ICriterioEvaluacionRepository IUnitOfWork.CriterioEvaluacionRepository
        {
            get
            {
                return _criterioEvaluacionRepository ?? new CriterioEvaluacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICriterioEvaluacionTipoProgramaRepository _criterioEvaluacionTipoProgramaRepository;
        ICriterioEvaluacionTipoProgramaRepository IUnitOfWork.CriterioEvaluacionTipoProgramaRepository
        {
            get
            {
                return _criterioEvaluacionTipoProgramaRepository ?? new CriterioEvaluacionTipoProgramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICriterioEvaluacionTipoPersonaRepository _criterioEvaluacionTipoPersonaRepository;
        ICriterioEvaluacionTipoPersonaRepository IUnitOfWork.CriterioEvaluacionTipoPersonaRepository
        {
            get
            {
                return _criterioEvaluacionTipoPersonaRepository ?? new CriterioEvaluacionTipoPersonaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICriterioEvaluacionModalidadCursoRepository _criterioEvaluacionModalidadCursoRepository;
        ICriterioEvaluacionModalidadCursoRepository IUnitOfWork.CriterioEvaluacionModalidadCursoRepository
        {
            get
            {
                return _criterioEvaluacionModalidadCursoRepository ?? new CriterioEvaluacionModalidadCursoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IParametroEvaluacionRepository _parametroEvaluacionRepository;
        IParametroEvaluacionRepository IUnitOfWork.ParametroEvaluacionRepository
        {
            get
            {
                return _parametroEvaluacionRepository ?? new ParametroEvaluacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMaterialAdicionalAulaVirtualRepository _materialAdicionalAulaVirtualRepository;
        IMaterialAdicionalAulaVirtualRepository IUnitOfWork.MaterialAdicionalAulaVirtualRepository
        {
            get
            {
                return _materialAdicionalAulaVirtualRepository ?? new MaterialAdicionalAulaVirtualRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IVisualizacionBsPlayRepository _visualizacionBsPlayRepository;
        IVisualizacionBsPlayRepository IUnitOfWork.VisualizacionBsPlayRepository
        {
            get
            {
                return _visualizacionBsPlayRepository ?? new VisualizacionBsPlayRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITituloRepository _tituloRepository;
        ITituloRepository IUnitOfWork.TituloRepository
        {
            get
            {
                return _tituloRepository ?? new TituloRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPerfilContactoProgramaColumnaRepository _perfilContactoProgramaColumnaRepository;
        IPerfilContactoProgramaColumnaRepository IUnitOfWork.PerfilContactoProgramaColumnaRepository
        {
            get
            {
                return _perfilContactoProgramaColumnaRepository ?? new PerfilContactoProgramaColumnaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPaginaWebPwRepository _paginaWebPwRepository;
        IPaginaWebPwRepository IUnitOfWork.PaginaWebPwRepository
        {
            get
            {
                return _paginaWebPwRepository ?? new PaginaWebPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilScoringCiudadRepository _programaGeneralPerfilScoringCiudadRepository;
        IProgramaGeneralPerfilScoringCiudadRepository IUnitOfWork.ProgramaGeneralPerfilScoringCiudadRepository
        {
            get
            {
                return _programaGeneralPerfilScoringCiudadRepository ?? new ProgramaGeneralPerfilScoringCiudadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilScoringModalidadRepository _programaGeneralPerfilScoringModalidadRepository;
        IProgramaGeneralPerfilScoringModalidadRepository IUnitOfWork.ProgramaGeneralPerfilScoringModalidadRepository
        {
            get
            {
                return _programaGeneralPerfilScoringModalidadRepository ?? new ProgramaGeneralPerfilScoringModalidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilScoringAformacionRepository _programaGeneralPerfilScoringAformacionRepository;
        IProgramaGeneralPerfilScoringAformacionRepository IUnitOfWork.ProgramaGeneralPerfilScoringAformacionRepository
        {
            get
            {
                return _programaGeneralPerfilScoringAformacionRepository ?? new ProgramaGeneralPerfilScoringAformacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilScoringIndustriaRepository _programaGeneralPerfilScoringIndustriaRepository;
        IProgramaGeneralPerfilScoringIndustriaRepository IUnitOfWork.ProgramaGeneralPerfilScoringIndustriaRepository
        {
            get
            {
                return _programaGeneralPerfilScoringIndustriaRepository ?? new ProgramaGeneralPerfilScoringIndustriaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISuscripcionProgramaGeneralRepository _suscripcionProgramaGeneralRepository;
        ISuscripcionProgramaGeneralRepository IUnitOfWork.SuscripcionProgramaGeneralRepository
        {
            get
            {
                return _suscripcionProgramaGeneralRepository ?? new SuscripcionProgramaGeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoPagoRepository _tipoPagoRepository;
        ITipoPagoRepository IUnitOfWork.TipoPagoRepository
        {
            get
            {
                return _tipoPagoRepository ?? new TipoPagoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPlataformaPagoRepository _plataformaPagoRepository;
        IPlataformaPagoRepository IUnitOfWork.PlataformaPagoRepository
        {
            get
            {
                return _plataformaPagoRepository ?? new PlataformaPagoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPEspecificoConsumoRepository _pEspecificoConsumoRepository;
        IPEspecificoConsumoRepository IUnitOfWork.PEspecificoConsumoRepository
        {
            get
            {
                return _pEspecificoConsumoRepository ?? new PEspecificoConsumoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilScoringCargoRepository _programaGeneralPerfilScoringCargoRepository;
        IProgramaGeneralPerfilScoringCargoRepository IUnitOfWork.ProgramaGeneralPerfilScoringCargoRepository
        {
            get
            {
                return _programaGeneralPerfilScoringCargoRepository ?? new ProgramaGeneralPerfilScoringCargoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilScoringAtrabajoRepository _programaGeneralPerfilScoringAtrabajoRepository;
        IProgramaGeneralPerfilScoringAtrabajoRepository IUnitOfWork.ProgramaGeneralPerfilScoringAtrabajoRepository
        {
            get
            {
                return _programaGeneralPerfilScoringAtrabajoRepository ?? new ProgramaGeneralPerfilScoringAtrabajoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilScoringCategoriaRepository _programaGeneralPerfilScoringCategoriaRepository;
        IProgramaGeneralPerfilScoringCategoriaRepository IUnitOfWork.ProgramaGeneralPerfilScoringCategoriaRepository
        {
            get
            {
                return _programaGeneralPerfilScoringCategoriaRepository ?? new ProgramaGeneralPerfilScoringCategoriaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilCiudadCoeficienteRepository _programaGeneralPerfilCiudadCoeficienteRepository;
        IProgramaGeneralPerfilCiudadCoeficienteRepository IUnitOfWork.ProgramaGeneralPerfilCiudadCoeficienteRepository
        {
            get
            {
                return _programaGeneralPerfilCiudadCoeficienteRepository ?? new ProgramaGeneralPerfilCiudadCoeficienteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilModalidadCoeficienteRepository _programaGeneralPerfilModalidadCoeficienteRepository;
        IProgramaGeneralPerfilModalidadCoeficienteRepository IUnitOfWork.ProgramaGeneralPerfilModalidadCoeficienteRepository
        {
            get
            {
                return _programaGeneralPerfilModalidadCoeficienteRepository ?? new ProgramaGeneralPerfilModalidadCoeficienteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilCategoriaCoeficienteRepository _programaGeneralPerfilCategoriaCoeficienteRepository;
        IProgramaGeneralPerfilCategoriaCoeficienteRepository IUnitOfWork.ProgramaGeneralPerfilCategoriaCoeficienteRepository
        {
            get
            {
                return _programaGeneralPerfilCategoriaCoeficienteRepository ?? new ProgramaGeneralPerfilCategoriaCoeficienteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilCargoCoeficienteRepository _programaGeneralPerfilCargoCoeficienteRepository;
        IProgramaGeneralPerfilCargoCoeficienteRepository IUnitOfWork.ProgramaGeneralPerfilCargoCoeficienteRepository
        {
            get
            {
                return _programaGeneralPerfilCargoCoeficienteRepository ?? new ProgramaGeneralPerfilCargoCoeficienteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilIndustriaCoeficienteRepository _programaGeneralPerfilIndustriaCoeficienteRepository;
        IProgramaGeneralPerfilIndustriaCoeficienteRepository IUnitOfWork.ProgramaGeneralPerfilIndustriaCoeficienteRepository
        {
            get
            {
                return _programaGeneralPerfilIndustriaCoeficienteRepository ?? new ProgramaGeneralPerfilIndustriaCoeficienteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilAformacionCoeficienteRepository _programaGeneralPerfilAformacionCoeficienteRepository;
        IProgramaGeneralPerfilAformacionCoeficienteRepository IUnitOfWork.ProgramaGeneralPerfilAformacionCoeficienteRepository
        {
            get
            {
                return _programaGeneralPerfilAformacionCoeficienteRepository ?? new ProgramaGeneralPerfilAformacionCoeficienteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilAtrabajoCoeficienteRepository _programaGeneralPerfilAtrabajoCoeficienteRepository;
        IProgramaGeneralPerfilAtrabajoCoeficienteRepository IUnitOfWork.ProgramaGeneralPerfilAtrabajoCoeficienteRepository
        {
            get
            {
                return _programaGeneralPerfilAtrabajoCoeficienteRepository ?? new ProgramaGeneralPerfilAtrabajoCoeficienteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilTipoDatoRepository _programaGeneralPerfilTipoDatoRepository;
        IProgramaGeneralPerfilTipoDatoRepository IUnitOfWork.ProgramaGeneralPerfilTipoDatoRepository
        {
            get
            {
                return _programaGeneralPerfilTipoDatoRepository ?? new ProgramaGeneralPerfilTipoDatoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilEscalaProbabilidadRepository _programaGeneralPerfilEscalaProbabilidadRepository;
        IProgramaGeneralPerfilEscalaProbabilidadRepository IUnitOfWork.ProgramaGeneralPerfilEscalaProbabilidadRepository
        {
            get
            {
                return _programaGeneralPerfilEscalaProbabilidadRepository ?? new ProgramaGeneralPerfilEscalaProbabilidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPerfilInterceptoRepository _programaGeneralPerfilInterceptoRepository;
        IProgramaGeneralPerfilInterceptoRepository IUnitOfWork.ProgramaGeneralPerfilInterceptoRepository
        {
            get
            {
                return _programaGeneralPerfilInterceptoRepository ?? new ProgramaGeneralPerfilInterceptoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IBeneficioDatoAdicionalRepository _beneficioDatoAdicionalRepository;
        IBeneficioDatoAdicionalRepository IUnitOfWork.BeneficioDatoAdicionalRepository
        {
            get
            {
                return _beneficioDatoAdicionalRepository ?? new BeneficioDatoAdicionalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPGeneralVersionProgramaRepository _pGeneralVersionProgramaRepository;
        IPGeneralVersionProgramaRepository IUnitOfWork.PGeneralVersionProgramaRepository
        {
            get
            {
                return _pGeneralVersionProgramaRepository ?? new PGeneralVersionProgramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IWhatsAppEstadoValidacionRepository _whatsAppEstadoValidacionRepository;
        IWhatsAppEstadoValidacionRepository IUnitOfWork.WhatsAppEstadoValidacionRepository
        {
            get
            {
                return _whatsAppEstadoValidacionRepository ?? new WhatsAppEstadoValidacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISeguimientoPreProcesoListaWhatsAppRepository _seguimientoPreProcesoListaWhatsAppRepository;
        ISeguimientoPreProcesoListaWhatsAppRepository IUnitOfWork.SeguimientoPreProcesoListaWhatsAppRepository
        {
            get
            {
                return _seguimientoPreProcesoListaWhatsAppRepository ?? new SeguimientoPreProcesoListaWhatsAppRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IActividadCabeceraDiaSemanaRepository _actividadCabeceraDiaSemanaRepository;
        IActividadCabeceraDiaSemanaRepository IUnitOfWork.ActividadCabeceraDiaSemanaRepository
        {
            get
            {
                return _actividadCabeceraDiaSemanaRepository ?? new ActividadCabeceraDiaSemanaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConvocatoriaPersonalRepository _convocatoriaPersonalRepository;
        IConvocatoriaPersonalRepository IUnitOfWork.ConvocatoriaPersonalRepository
        {
            get
            {
                return _convocatoriaPersonalRepository ?? new ConvocatoriaPersonalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IFormaCalculoEvaluacionRepository _formaCalculoEvaluacionRepository;
        IFormaCalculoEvaluacionRepository IUnitOfWork.FormaCalculoEvaluacionRepository
        {
            get
            {
                return _formaCalculoEvaluacionRepository ?? new FormaCalculoEvaluacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IModeloPredictivoEscalaProbabilidadRepository _modeloPredictivoEscalaProbabilidadRepository;
        IModeloPredictivoEscalaProbabilidadRepository IUnitOfWork.ModeloPredictivoEscalaProbabilidadRepository
        {
            get
            {
                return _modeloPredictivoEscalaProbabilidadRepository ?? new ModeloPredictivoEscalaProbabilidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IModeloPredictivoTipoDatoRepository _modeloPredictivoTipoDatoRepository;
        IModeloPredictivoTipoDatoRepository IUnitOfWork.ModeloPredictivoTipoDatoRepository
        {
            get
            {
                return _modeloPredictivoTipoDatoRepository ?? new ModeloPredictivoTipoDatoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IModeloPredictivoIndustriaRepository _modeloPredictivoIndustriaRepository;
        IModeloPredictivoIndustriaRepository IUnitOfWork.ModeloPredictivoIndustriaRepository
        {
            get
            {
                return _modeloPredictivoIndustriaRepository ?? new ModeloPredictivoIndustriaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IModeloPredictivoCargoRepository _modeloPredictivoCargoRepository;
        IModeloPredictivoCargoRepository IUnitOfWork.ModeloPredictivoCargoRepository
        {
            get
            {
                return _modeloPredictivoCargoRepository ?? new ModeloPredictivoCargoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IModeloPredictivoFormacionRepository _modeloPredictivoFormacionRepository;
        IModeloPredictivoFormacionRepository IUnitOfWork.ModeloPredictivoFormacionRepository
        {
            get
            {
                return _modeloPredictivoFormacionRepository ?? new ModeloPredictivoFormacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMaterialAdicionalAulaVirtualRegistroRepository _materialAdicionalAulaVirtualRegistroRepository;
        IMaterialAdicionalAulaVirtualRegistroRepository IUnitOfWork.MaterialAdicionalAulaVirtualRegistroRepository
        {
            get
            {
                return _materialAdicionalAulaVirtualRegistroRepository ?? new MaterialAdicionalAulaVirtualRegistroRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMaterialAdicionalAulaVirtualPespecificoRepository _materialAdicionalAulaVirtualPespecificoRepository;
        IMaterialAdicionalAulaVirtualPespecificoRepository IUnitOfWork.MaterialAdicionalAulaVirtualPespecificoRepository
        {
            get
            {
                return _materialAdicionalAulaVirtualPespecificoRepository ?? new MaterialAdicionalAulaVirtualPespecificoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IModeloPredictivoTrabajoRepository _modeloPredictivoTrabajoRepository;
        IModeloPredictivoTrabajoRepository IUnitOfWork.ModeloPredictivoTrabajoRepository
        {
            get
            {
                return _modeloPredictivoTrabajoRepository ?? new ModeloPredictivoTrabajoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IModeloPredictivoCategoriaDatoRepository _modeloPredictivoCategoriaDatoRepository;
        IModeloPredictivoCategoriaDatoRepository IUnitOfWork.ModeloPredictivoCategoriaDatoRepository
        {
            get
            {
                return _modeloPredictivoCategoriaDatoRepository ?? new ModeloPredictivoCategoriaDatoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IModeloGeneralRepository _modeloGeneralRepository;
        IModeloGeneralRepository IUnitOfWork.ModeloGeneralRepository
        {
            get
            {
                return _modeloGeneralRepository ?? new ModeloGeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IModeloGeneralPGeneralRepository _modeloGeneralPGeneralRepository;
        IModeloGeneralPGeneralRepository IUnitOfWork.ModeloGeneralPGeneralRepository
        {
            get
            {
                return _modeloGeneralPGeneralRepository ?? new ModeloGeneralPGeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IModeloPredictivoRepository _modeloPredictivoRepository;
        IModeloPredictivoRepository IUnitOfWork.ModeloPredictivoRepository
        {
            get
            {
                return _modeloPredictivoRepository ?? new ModeloPredictivoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPgeneralProyectoAplicacionAnexoRepository _pgeneralProyectoAplicacionAnexoRepository;
        IPgeneralProyectoAplicacionAnexoRepository IUnitOfWork.PgeneralProyectoAplicacionAnexoRepository
        {
            get
            {
                return _pgeneralProyectoAplicacionAnexoRepository ?? new PgeneralProyectoAplicacionAnexoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDocumentoPwRepository _documentoPwRepository;
        IDocumentoPwRepository IUnitOfWork.DocumentoPwRepository
        {
            get
            {
                return _documentoPwRepository ?? new DocumentoPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPGeneralRelacionadoRepository _pGeneralRelacionadoRepository;
        IPGeneralRelacionadoRepository IUnitOfWork.PGeneralRelacionadoRepository
        {
            get
            {
                return _pGeneralRelacionadoRepository ?? new PGeneralRelacionadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralMaterialEstudioAdicionalRepository _programaGeneralMaterialEstudioAdicionalRepository;
        IProgramaGeneralMaterialEstudioAdicionalRepository IUnitOfWork.ProgramaGeneralMaterialEstudioAdicionalRepository
        {
            get
            {
                return _programaGeneralMaterialEstudioAdicionalRepository ?? new ProgramaGeneralMaterialEstudioAdicionalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralMaterialEstudioAdicionalEspecificoRepository _programaGeneralMaterialEstudioAdicionalEspecificoRepository;
        IProgramaGeneralMaterialEstudioAdicionalEspecificoRepository IUnitOfWork.ProgramaGeneralMaterialEstudioAdicionalEspecificoRepository
        {
            get
            {
                return _programaGeneralMaterialEstudioAdicionalEspecificoRepository ?? new ProgramaGeneralMaterialEstudioAdicionalEspecificoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPGeneralCriterioEvaluacionRepository _pGeneralCriterioEvaluacionRepository;
        IPGeneralCriterioEvaluacionRepository IUnitOfWork.PGeneralCriterioEvaluacionRepository
        {
            get
            {
                return _pGeneralCriterioEvaluacionRepository ?? new PGeneralCriterioEvaluacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralPrerequisitoModalidadRepository _programaGeneralPrerequisitoModalidadRepository;
        IProgramaGeneralPrerequisitoModalidadRepository IUnitOfWork.ProgramaGeneralPrerequisitoModalidadRepository
        {
            get
            {
                return _programaGeneralPrerequisitoModalidadRepository ?? new ProgramaGeneralPrerequisitoModalidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPGeneralDescripcionRepository _pGeneralDescripcionRepository;
        IPGeneralDescripcionRepository IUnitOfWork.PGeneralDescripcionRepository
        {
            get
            {
                return _pGeneralDescripcionRepository ?? new PGeneralDescripcionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IAdicionalProgramaGeneralRepository _adicionalProgramaGeneralRepository;
        IAdicionalProgramaGeneralRepository IUnitOfWork.AdicionalProgramaGeneralRepository
        {
            get
            {
                return _adicionalProgramaGeneralRepository ?? new AdicionalProgramaGeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPGeneralExpositorRepository _pGeneralExpositorRepository;
        IPGeneralExpositorRepository IUnitOfWork.PGeneralExpositorRepository
        {
            get
            {
                return _pGeneralExpositorRepository ?? new PGeneralExpositorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPGeneralModalidadRepository _pGeneralModalidadRepository;
        IPGeneralModalidadRepository IUnitOfWork.PGeneralModalidadRepository
        {
            get
            {
                return _pGeneralModalidadRepository ?? new PGeneralModalidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaAreaRelacionadaRepository _programaAreaRelacionadaRepository;
        IProgramaAreaRelacionadaRepository IUnitOfWork.ProgramaAreaRelacionadaRepository
        {
            get
            {
                return _programaAreaRelacionadaRepository ?? new ProgramaAreaRelacionadaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoPagoCategoriaRepository _tipoPagoCategoriaRepository;
        ITipoPagoCategoriaRepository IUnitOfWork.TipoPagoCategoriaRepository
        {
            get
            {
                return _tipoPagoCategoriaRepository ?? new TipoPagoCategoriaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMontoPagoPlataformaRepository _montoPagoPlataformaRepository;
        IMontoPagoPlataformaRepository IUnitOfWork.MontoPagoPlataformaRepository
        {
            get
            {
                return _montoPagoPlataformaRepository ?? new MontoPagoPlataformaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMontoPagoSuscripcionRepository _montoPagoSuscripcionRepository;
        IMontoPagoSuscripcionRepository IUnitOfWork.MontoPagoSuscripcionRepository
        {
            get
            {
                return _montoPagoSuscripcionRepository ?? new MontoPagoSuscripcionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISedeTrabajoRepository _sedeTrabajoRepository;
        ISedeTrabajoRepository IUnitOfWork.SedeTrabajoRepository
        {
            get
            {
                return _sedeTrabajoRepository ?? new SedeTrabajoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProcesoSeleccionRepository _procesoSeleccionRepository;
        IProcesoSeleccionRepository IUnitOfWork.ProcesoSeleccionRepository
        {
            get
            {
                return _procesoSeleccionRepository ?? new ProcesoSeleccionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IEstadoConvocatoriaRepository _estadoConvocatoriaRepository;
        IEstadoConvocatoriaRepository IUnitOfWork.EstadoConvocatoriaRepository
        {
            get
            {
                return _estadoConvocatoriaRepository ?? new EstadoConvocatoriaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IModalidadTrabajoRepository _modalidadTrabajoRepository;
        IModalidadTrabajoRepository IUnitOfWork.ModalidadTrabajoRepository
        {
            get
            {
                return _modalidadTrabajoRepository ?? new ModalidadTrabajoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICategoriaAsignacionRepository _categoriaAsignacionRepository;
        ICategoriaAsignacionRepository IUnitOfWork.CategoriaAsignacionRepository
        {
            get
            {
                return _categoriaAsignacionRepository ?? new CategoriaAsignacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IExperienciaRepository _experienciaRepository;
        IExperienciaRepository IUnitOfWork.ExperienciaRepository
        {
            get
            {
                return _experienciaRepository ?? new ExperienciaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private INivelEstudioRepository _nivelEstudioRepository;
        INivelEstudioRepository IUnitOfWork.NivelEstudioRepository
        {
            get
            {
                return _nivelEstudioRepository ?? new NivelEstudioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IIdiomaRepository _idiomaRepository;
        IIdiomaRepository IUnitOfWork.IdiomaRepository
        {
            get
            {
                return _idiomaRepository ?? new IdiomaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoContratoRepository _tipoContratoRepository;
        ITipoContratoRepository IUnitOfWork.TipoContratoRepository
        {
            get
            {
                return _tipoContratoRepository ?? new TipoContratoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPlantillaMaestroPwRepository _plantillaMaestroPwRepository;
        IPlantillaMaestroPwRepository IUnitOfWork.PlantillaMaestroPwRepository
        {
            get
            {
                return _plantillaMaestroPwRepository ?? new PlantillaMaestroPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IRevisionPwRepository _revisionPwRepository;
        IRevisionPwRepository IUnitOfWork.RevisionPwRepository
        {
            get
            {
                return _revisionPwRepository ?? new RevisionPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISeccionTipoContenidoPwRepository _seccionTipoContenidoPwRepository;
        ISeccionTipoContenidoPwRepository IUnitOfWork.SeccionTipoContenidoPwRepository
        {
            get
            {
                return _seccionTipoContenidoPwRepository ?? new SeccionTipoContenidoPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEsquemaEvaluacionPgeneralRepository _esquemaEvaluacionPgeneralRepository;
        IEsquemaEvaluacionPgeneralRepository IUnitOfWork.EsquemaEvaluacionPgeneralRepository
        {
            get
            {
                return _esquemaEvaluacionPgeneralRepository ?? new EsquemaEvaluacionPgeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEsquemaEvaluacionPgeneralModalidadRepository _esquemaEvaluacionPgeneralModalidadRepository;
        IEsquemaEvaluacionPgeneralModalidadRepository IUnitOfWork.EsquemaEvaluacionPgeneralModalidadRepository
        {
            get
            {
                return _esquemaEvaluacionPgeneralModalidadRepository ?? new EsquemaEvaluacionPgeneralModalidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEsquemaEvaluacionPgeneralProveedorRepository _esquemaEvaluacionPgeneralProveedorRepository;
        IEsquemaEvaluacionPgeneralProveedorRepository IUnitOfWork.EsquemaEvaluacionPgeneralProveedorRepository
        {
            get
            {
                return _esquemaEvaluacionPgeneralProveedorRepository ?? new EsquemaEvaluacionPgeneralProveedorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IRevisionNivelPwRepository _revisionNivelPwRepository;
        IRevisionNivelPwRepository IUnitOfWork.RevisionNivelPwRepository
        {
            get
            {
                return _revisionNivelPwRepository ?? new RevisionNivelPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISeccionPwRepository _seccionPwRepository;
        ISeccionPwRepository IUnitOfWork.SeccionPwRepository
        {
            get
            {
                return _seccionPwRepository ?? new SeccionPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICourierRepository _courierRepository;
        ICourierRepository IUnitOfWork.CourierRepository
        {
            get
            {
                return _courierRepository ?? new CourierRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICourierDetalleRepository _courierDetalleRepository;
        ICourierDetalleRepository IUnitOfWork.CourierDetalleRepository
        {
            get
            {
                return _courierDetalleRepository ?? new CourierDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPlantillaPaisRepository _plantillaPaisRepository;
        IPlantillaPaisRepository IUnitOfWork.PlantillaPaisRepository
        {
            get
            {
                return _plantillaPaisRepository ?? new PlantillaPaisRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISeccionTipoDetallePwRepository _seccionTipoDetallePwRepository;
        ISeccionTipoDetallePwRepository IUnitOfWork.SeccionTipoDetallePwRepository
        {
            get
            {
                return _seccionTipoDetallePwRepository ?? new SeccionTipoDetallePwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IReporteControlTareaAlumnoRepository _reporteControlTareaAlumnoRepository;
        IReporteControlTareaAlumnoRepository IUnitOfWork.ReporteControlTareaAlumnoRepository
        {
            get
            {
                return _reporteControlTareaAlumnoRepository ?? new ReporteControlTareaAlumnoRepository(_dapperRepository);
            }
        }
        private IReporteProblemasAulaVirtualRepository _reporteProblemasAulaVirtualRepository;
        IReporteProblemasAulaVirtualRepository IUnitOfWork.ReporteProblemasAulaVirtualRepository
        {
            get
            {
                return _reporteProblemasAulaVirtualRepository ?? new ReporteProblemasAulaVirtualRepository(_dapperRepository);
            }
        }
        private IReporteEncuestasRepository _reporteEncuestasRepository;
        IReporteEncuestasRepository IUnitOfWork.ReporteEncuestasRepository
        {
            get
            {
                return _reporteEncuestasRepository ?? new ReporteEncuestasRepository(_dapperRepository);
            }
        }

        private IReporteEncuestasSincronicoRepository _reporteEncuestasSincronicoRepository;

        IReporteEncuestasSincronicoRepository IUnitOfWork.ReporteEncuestasSincronicoRepository
        {
            get
            {
                return _reporteEncuestasSincronicoRepository ?? new ReporteEncuestasSincronicoRepository(_dapperRepository);
            }
        }

        private IMaterialPespecificoRepository _materialPespecificoRepository;
        IMaterialPespecificoRepository IUnitOfWork.MaterialPespecificoRepository
        {
            get
            {
                return _materialPespecificoRepository ?? new MaterialPespecificoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMaterialPespecificoDetalleRepository _materialPespecificoDetalleRepository;
        IMaterialPespecificoDetalleRepository IUnitOfWork.MaterialPespecificoDetalleRepository
        {
            get
            {
                return _materialPespecificoDetalleRepository ?? new MaterialPespecificoDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITroncalCiudadRepository _troncalCiudadRepository;
        ITroncalCiudadRepository IUnitOfWork.TroncalCiudadRepository
        {
            get
            {
                return _troncalCiudadRepository ?? new TroncalCiudadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IAreaRepository _areaRepository;
        IAreaRepository IUnitOfWork.AreaRepository
        {
            get
            {
                return _areaRepository ?? new AreaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISubAreaRepository _subAreaRepository;
        ISubAreaRepository IUnitOfWork.SubAreaRepository
        {
            get
            {
                return _subAreaRepository ?? new SubAreaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IActividadMarcadorLogRepository _actividadMarcadorLogRepository;
        IActividadMarcadorLogRepository IUnitOfWork.ActividadMarcadorLogRepository
        {
            get
            {
                return _actividadMarcadorLogRepository ?? new ActividadMarcadorLogRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITagParametroSeoPwRepository _tagParametroSeoPwRepository;
        ITagParametroSeoPwRepository IUnitOfWork.TagParametroSeoPwRepository
        {
            get
            {
                return _tagParametroSeoPwRepository ?? new TagParametroSeoPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPreguntaProgramaCapacitacionRepository _preguntaProgramaCapacitacionRepository;
        IPreguntaProgramaCapacitacionRepository IUnitOfWork.PreguntaProgramaCapacitacionRepository
        {
            get
            {
                return _preguntaProgramaCapacitacionRepository ?? new PreguntaProgramaCapacitacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoVistumRepository _tipoVistumRepository;
        ITipoVistumRepository IUnitOfWork.TipoVistumRepository
        {
            get
            {
                return _tipoVistumRepository ?? new TipoVistumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoEvaluacionTrabajoRepository _tipoEvaluacionTrabajoRepository;
        ITipoEvaluacionTrabajoRepository IUnitOfWork.TipoEvaluacionTrabajoRepository
        {
            get
            {
                return _tipoEvaluacionTrabajoRepository ?? new TipoEvaluacionTrabajoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoMarcadorRepository _tipoMarcadorRepository;
        ITipoMarcadorRepository IUnitOfWork.TipoMarcadorRepository
        {
            get
            {
                return _tipoMarcadorRepository ?? new TipoMarcadorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISesionConfigurarVideoRepository _sesionConfigurarVideoRepository;
        ISesionConfigurarVideoRepository IUnitOfWork.SesionConfigurarVideoRepository
        {
            get
            {
                return _sesionConfigurarVideoRepository ?? new SesionConfigurarVideoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConfigurarEvaluacionTrabajoRepository _configurarEvaluacionTrabajoRepository;
        IConfigurarEvaluacionTrabajoRepository IUnitOfWork.ConfigurarEvaluacionTrabajoRepository
        {
            get
            {
                return _configurarEvaluacionTrabajoRepository ?? new ConfigurarEvaluacionTrabajoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPreguntaEvaluacionTrabajoRepository preguntaEvaluacionTrabajoRepository;
        IPreguntaEvaluacionTrabajoRepository IUnitOfWork.PreguntaEvaluacionTrabajoRepository
        {
            get
            {
                return preguntaEvaluacionTrabajoRepository ?? new PreguntaEvaluacionTrabajoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IQuejaSugerenciaRepository _quejaSugerenciaRepository;
        IQuejaSugerenciaRepository IUnitOfWork.QuejaSugerenciaRepository
        {
            get
            {
                return _quejaSugerenciaRepository ?? new QuejaSugerenciaRepository(_dapperRepository);
            }
        }
        private IPreguntaIntentoDetalleRepository _preguntaIntentoDetalleRepository;
        IPreguntaIntentoDetalleRepository IUnitOfWork.PreguntaIntentoDetalleRepository
        {
            get
            {
                return _preguntaIntentoDetalleRepository ?? new PreguntaIntentoDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IRespuestaPreguntaProgramaCapacitacionRepository _respuestaPreguntaProgramaCapacitacionRepository;
        IRespuestaPreguntaProgramaCapacitacionRepository IUnitOfWork.RespuestaPreguntaProgramaCapacitacionRepository
        {
            get
            {
                return _respuestaPreguntaProgramaCapacitacionRepository ?? new RespuestaPreguntaProgramaCapacitacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPreguntaIntentoRepository _preguntaIntentoRepository;
        IPreguntaIntentoRepository IUnitOfWork.PreguntaIntentoRepository
        {
            get
            {
                return _preguntaIntentoRepository ?? new PreguntaIntentoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        public ICampaniaGeneralWhatsAppRepository _campaniaGeneralWhatsAppRepository;
        ICampaniaGeneralWhatsAppRepository IUnitOfWork.CampaniaGeneralWhatsAppRepository
        {
            get
            {
                return _campaniaGeneralWhatsAppRepository ?? new CampaniaGeneralWhatsAppRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        public IPlantillaSendinblueImagenRepository _plantillaSendinblueImagenRepository;
        IPlantillaSendinblueImagenRepository IUnitOfWork.PlantillaSendinblueImagenRepository
        {
            get
            {
                return _plantillaSendinblueImagenRepository ?? new PlantillaSendinblueImagenRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        public IPlantillaSendinblueBaseRepository _plantillaSendinblueBaseRepository;
        IPlantillaSendinblueBaseRepository IUnitOfWork.PlantillaSendinblueBaseRepository
        {
            get
            {
                return _plantillaSendinblueBaseRepository ?? new PlantillaSendinblueBaseRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        public IPlantillaSendinblueEtiquetaPlantillaRepository _plantillaSendinblueEtiquetaPlantillaRepository;
        IPlantillaSendinblueEtiquetaPlantillaRepository IUnitOfWork.PlantillaSendinblueEtiquetaPlantillaRepository
        {
            get
            {
                return _plantillaSendinblueEtiquetaPlantillaRepository ?? new PlantillaSendinblueEtiquetaPlantillaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        public IPlantillaSendinblueEtiquetaRepository _plantillaSendinblueEtiquetaRepository;
        IPlantillaSendinblueEtiquetaRepository IUnitOfWork.PlantillaSendinblueEtiquetaRepository
        {
            get
            {
                return _plantillaSendinblueEtiquetaRepository ?? new PlantillaSendinblueEtiquetaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        public IPlantillaSendinblueRepository _plantillaSendinblueRepository;
        IPlantillaSendinblueRepository IUnitOfWork.PlantillaSendinblueRepository
        {
            get
            {
                return _plantillaSendinblueRepository ?? new PlantillaSendinblueRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        public IPlantillaSendinblueDatoRepository _plantillaSendinblueDatoRepository;
        IPlantillaSendinblueDatoRepository IUnitOfWork.PlantillaSendinblueDatoRepository
        {
            get
            {
                return _plantillaSendinblueDatoRepository ?? new PlantillaSendinblueDatoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEstadoProgramaEspecificoRepository _estadoProgramaEspecificoRepository;
        IEstadoProgramaEspecificoRepository IUnitOfWork.EstadoProgramaEspecificoRepository
        {
            get
            {
                return _estadoProgramaEspecificoRepository ?? new EstadoProgramaEspecificoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICriteriosEvaluacionProgramasEspecificosRepository _criteriosEvaluacionProgramasEspecificosRepository;
        ICriteriosEvaluacionProgramasEspecificosRepository IUnitOfWork.CriteriosEvaluacionProgramasEspecificosRepository
        {
            get
            {
                return _criteriosEvaluacionProgramasEspecificosRepository ?? new CriteriosEvaluacionProgramasEspecificosRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPEspecificoCriterioEvaluacionRepository _pEspecificoCriterioEvaluacionRepository;
        IPEspecificoCriterioEvaluacionRepository IUnitOfWork.PEspecificoCriterioEvaluacionRepository
        {
            get
            {
                return _pEspecificoCriterioEvaluacionRepository ?? new PEspecificoCriterioEvaluacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPgeneralAsubPgeneralVersionProgramaRepository _pgeneralAsubPgeneralVersionProgramaRepository;
        IPgeneralAsubPgeneralVersionProgramaRepository IUnitOfWork.PgeneralAsubPgeneralVersionProgramaRepository
        {
            get
            {
                return _pgeneralAsubPgeneralVersionProgramaRepository ?? new PgeneralAsubPgeneralVersionProgramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralBeneficioModalidadRepository _programaGeneralBeneficioModalidadRepository;
        IProgramaGeneralBeneficioModalidadRepository IUnitOfWork.ProgramaGeneralBeneficioModalidadRepository
        {
            get
            {
                return _programaGeneralBeneficioModalidadRepository ?? new ProgramaGeneralBeneficioModalidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralCertificacionModalidadRepository _programaGeneralCertificacionModalidadRepository;
        IProgramaGeneralCertificacionModalidadRepository IUnitOfWork.ProgramaGeneralCertificacionModalidadRepository
        {
            get
            {
                return _programaGeneralCertificacionModalidadRepository ?? new ProgramaGeneralCertificacionModalidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralModeloCertificadoModalidadRepository _programaGeneralModeloCertificadoModalidadRepository;
        IProgramaGeneralModeloCertificadoModalidadRepository IUnitOfWork.ProgramaGeneralModeloCertificadoModalidadRepository
        {
            get
            {
                return _programaGeneralModeloCertificadoModalidadRepository ?? new ProgramaGeneralModeloCertificadoModalidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralMotivacionModalidadRepository _programaGeneralMotivacionModalidadRepository;
        IProgramaGeneralMotivacionModalidadRepository IUnitOfWork.ProgramaGeneralMotivacionModalidadRepository
        {
            get
            {
                return _programaGeneralMotivacionModalidadRepository ?? new ProgramaGeneralMotivacionModalidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConfiguracionAccesoPersonalRepository _configuracionAccesoPersonalRepository;
        IConfiguracionAccesoPersonalRepository IUnitOfWork.ConfiguracionAccesoPersonalRepository
        {
            get
            {
                return _configuracionAccesoPersonalRepository ?? new ConfiguracionAccesoPersonalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPortalWebRepository _portalWebRepository;
        IPortalWebRepository IUnitOfWork.PortalWebRepository
        {
            get
            {
                return _portalWebRepository ?? new PortalWebRepository(_dapperRepository);
            }
        }

        private IReporteConsultasForoAulaVirtualRepository _reporteConsultasForoAulaVirtualRepository;
        IReporteConsultasForoAulaVirtualRepository IUnitOfWork.ReporteConsultasForoAulaVirtualRepository
        {
            get
            {
                return _reporteConsultasForoAulaVirtualRepository ?? new ReporteConsultasForoAulaVirtualRepository(_dapperRepository);
            }
        }
        private IContenidoCertificadoIrcaRepository _contenidoCertificadoIrcaRepository;
        IContenidoCertificadoIrcaRepository IUnitOfWork.ContenidoCertificadoIrcaRepository
        {
            get
            {
                return _contenidoCertificadoIrcaRepository ?? new ContenidoCertificadoIrcaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPreguntaTipoRepository _preguntaTipoRepository;
        IPreguntaTipoRepository IUnitOfWork.PreguntaTipoRepository
        {
            get
            {
                return _preguntaTipoRepository ?? new PreguntaTipoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoRespuestaCalificacionRepository _tipoRespuestaCalificacionRepository;
        ITipoRespuestaCalificacionRepository IUnitOfWork.TipoRespuestaCalificacionRepository
        {
            get
            {
                return _tipoRespuestaCalificacionRepository ?? new TipoRespuestaCalificacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEsquemaEvaluacionPgeneralDetalleRepository _esquemaEvaluacionPgeneralDetalleRepository;
        IEsquemaEvaluacionPgeneralDetalleRepository IUnitOfWork.EsquemaEvaluacionPgeneralDetalleRepository
        {
            get
            {
                return _esquemaEvaluacionPgeneralDetalleRepository ?? new EsquemaEvaluacionPgeneralDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPgeneralConfiguracionPlantillaEstadoMatriculaRepository _pgeneralConfiguracionPlantillaEstadoMatriculaRepository;
        IPgeneralConfiguracionPlantillaEstadoMatriculaRepository IUnitOfWork.PgeneralConfiguracionPlantillaEstadoMatriculaRepository
        {
            get
            {
                return _pgeneralConfiguracionPlantillaEstadoMatriculaRepository ?? new PgeneralConfiguracionPlantillaEstadoMatriculaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPgeneralConfiguracionPlantillaSubEstadoMatriculaRepository _pgeneralConfiguracionPlantillaSubEstadoMatriculaRepository;
        IPgeneralConfiguracionPlantillaSubEstadoMatriculaRepository IUnitOfWork.PgeneralConfiguracionPlantillaSubEstadoMatriculaRepository
        {
            get
            {
                return _pgeneralConfiguracionPlantillaSubEstadoMatriculaRepository ?? new PgeneralConfiguracionPlantillaSubEstadoMatriculaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepository _configuracionBeneficioProgramaGeneralEstadoMatriculaRepository;
        IConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepository IUnitOfWork.ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepository
        {
            get
            {
                return _configuracionBeneficioProgramaGeneralEstadoMatriculaRepository ?? new ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConfiguracionBeneficioProgramaGeneralSubEstadoRepository _configuracionBeneficioProgramaGeneralSubEstadoRepository;
        IConfiguracionBeneficioProgramaGeneralSubEstadoRepository IUnitOfWork.ConfiguracionBeneficioProgramaGeneralSubEstadoRepository
        {
            get
            {
                return _configuracionBeneficioProgramaGeneralSubEstadoRepository ?? new ConfiguracionBeneficioProgramaGeneralSubEstadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEsquemaEvaluacionDetalleRepository _esquemaEvaluacionDetalleRepository;
        IEsquemaEvaluacionDetalleRepository IUnitOfWork.EsquemaEvaluacionDetalleRepository
        {
            get
            {
                return _esquemaEvaluacionDetalleRepository ?? new EsquemaEvaluacionDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConfiguracionBeneficioProgramaGeneralPaisRepository _configuracionBeneficioProgramaGeneralPaisRepository;
        IConfiguracionBeneficioProgramaGeneralPaisRepository IUnitOfWork.ConfiguracionBeneficioProgramaGeneralPaisRepository
        {
            get
            {
                return _configuracionBeneficioProgramaGeneralPaisRepository ?? new ConfiguracionBeneficioProgramaGeneralPaisRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConfiguracionBeneficioProgramaGeneralDatoAdicionalRepository _configuracionBeneficioProgramaGeneralDatoAdicionalRepository;
        IConfiguracionBeneficioProgramaGeneralDatoAdicionalRepository IUnitOfWork.ConfiguracionBeneficioProgramaGeneralDatoAdicionalRepository
        {
            get
            {
                return _configuracionBeneficioProgramaGeneralDatoAdicionalRepository ?? new ConfiguracionBeneficioProgramaGeneralDatoAdicionalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConfiguracionBeneficioProgramaGeneralVersionRepository _configuracionBeneficioProgramaGeneralVersionRepository;
        IConfiguracionBeneficioProgramaGeneralVersionRepository IUnitOfWork.ConfiguracionBeneficioProgramaGeneralVersionRepository
        {
            get
            {
                return _configuracionBeneficioProgramaGeneralVersionRepository ?? new ConfiguracionBeneficioProgramaGeneralVersionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMaterialCriterioVerificacionDetalleRepository _materialCriterioVerificacionDetalleRepository;
        IMaterialCriterioVerificacionDetalleRepository IUnitOfWork.MaterialCriterioVerificacionDetalleRepository
        {
            get
            {
                return _materialCriterioVerificacionDetalleRepository ?? new MaterialCriterioVerificacionDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPreguntumRepository _preguntumRepository;
        IPreguntumRepository IUnitOfWork.PreguntumRepository
        {
            get
            {
                return _preguntumRepository ?? new PreguntumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IReporteIndicadoresProductividadRepository _reporteIndicadoresProductividadRepository;
        IReporteIndicadoresProductividadRepository IUnitOfWork.ReporteIndicadoresProductividadRepository
        {
            get
            {
                return _reporteIndicadoresProductividadRepository ?? new ReporteIndicadoresProductividadRepository(_dapperRepository);
            }
        }
        private IPartnerContactoPwRepository _partnerContactoPwRepository;
        IPartnerContactoPwRepository IUnitOfWork.PartnerContactoPwRepository
        {
            get
            {
                return _partnerContactoPwRepository ?? new PartnerContactoPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPartnerBeneficioPwRepository _partnerBeneficioPwRepository;
        IPartnerBeneficioPwRepository IUnitOfWork.PartnerBeneficioPwRepository
        {
            get
            {
                return _partnerBeneficioPwRepository ?? new PartnerBeneficioPwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IScrapingAerolineaResultadoRepository _scrapingAerolineaResultadoRepository;
        IScrapingAerolineaResultadoRepository IUnitOfWork.ScrapingAerolineaResultadoRepository
        {
            get
            {
                return _scrapingAerolineaResultadoRepository ?? new ScrapingAerolineaResultadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IScrapingAerolineaResultadoDetalleRepository _scrapingAerolineaResultadoDetalleRepository;
        IScrapingAerolineaResultadoDetalleRepository IUnitOfWork.ScrapingAerolineaResultadoDetalleRepository
        {
            get
            {
                return _scrapingAerolineaResultadoDetalleRepository ?? new ScrapingAerolineaResultadoDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IScrapingAerolineaConfiguracionRepository _scrapingAerolineaConfiguracionRepository;
        IScrapingAerolineaConfiguracionRepository IUnitOfWork.ScrapingAerolineaConfiguracionRepository
        {
            get
            {
                return _scrapingAerolineaConfiguracionRepository ?? new ScrapingAerolineaConfiguracionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IScrapingAerolineaEstadoConsultaRepository _scrapingAerolineaEstadoConsultaRepository;
        IScrapingAerolineaEstadoConsultaRepository IUnitOfWork.ScrapingAerolineaEstadoConsultaRepository
        {
            get
            {
                return _scrapingAerolineaEstadoConsultaRepository ?? new ScrapingAerolineaEstadoConsultaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IFlujoRepository _flujoRepository;
        IFlujoRepository IUnitOfWork.FlujoRepository
        {
            get
            {
                return _flujoRepository ?? new FlujoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IFlujoActividadRepository _flujoActividadRepository;
        IFlujoActividadRepository IUnitOfWork.FlujoActividadRepository
        {
            get
            {
                return _flujoActividadRepository ?? new FlujoActividadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IFlujoFaseRepository _flujoFaseRepository;
        IFlujoFaseRepository IUnitOfWork.FlujoFaseRepository
        {
            get
            {
                return _flujoFaseRepository ?? new FlujoFaseRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IFlujoOcurrenciaRepository _flujoOcurrenciaRepository;
        IFlujoOcurrenciaRepository IUnitOfWork.FlujoOcurrenciaRepository
        {
            get
            {
                return _flujoOcurrenciaRepository ?? new FlujoOcurrenciaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IFlujoPorPespecificoRepository _flujoPorPespecificoRepository;
        IFlujoPorPespecificoRepository IUnitOfWork.FlujoPorPespecificoRepository
        {
            get
            {
                return _flujoPorPespecificoRepository ?? new FlujoPorPespecificoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMaterialEnvioRepository _materialEnvioRepository;
        IMaterialEnvioRepository IUnitOfWork.MaterialEnvioRepository
        {
            get
            {
                return _materialEnvioRepository ?? new MaterialEnvioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMaterialEnvioDetalleRepository _materialEnvioDetalleRepository;
        IMaterialEnvioDetalleRepository IUnitOfWork.MaterialEnvioDetalleRepository
        {
            get
            {
                return _materialEnvioDetalleRepository ?? new MaterialEnvioDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMaterialEstadoRecepcionRepository _materialEstadoRecepcionRepository;
        IMaterialEstadoRecepcionRepository IUnitOfWork.MaterialEstadoRecepcionRepository
        {
            get
            {
                return _materialEstadoRecepcionRepository ?? new MaterialEstadoRecepcionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICertificadoPartnerComplementoRepository _certificadoPartnerComplementoRepository;
        ICertificadoPartnerComplementoRepository IUnitOfWork.CertificadoPartnerComplementoRepository
        {
            get
            {
                return _certificadoPartnerComplementoRepository ?? new CertificadoPartnerComplementoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoDocumentoRepository _tipoDocumentoRepository;
        ITipoDocumentoRepository IUnitOfWork.TipoDocumentoRepository
        {
            get
            {
                return _tipoDocumentoRepository ?? new TipoDocumentoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICoordinadoraRepository _coordinadoraRepository;
        ICoordinadoraRepository IUnitOfWork.CoordinadoraRepository
        {
            get
            {
                return _coordinadoraRepository ?? new CoordinadoraRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IReporteFurPorPagarRepository _reporteFurPorPagarRepository;
        IReporteFurPorPagarRepository IUnitOfWork.ReporteFurPorPagarRepository
        {
            get
            {
                return _reporteFurPorPagarRepository ?? new ReporteFurPorPagarRepository(_dapperRepository);
            }
        }

        private IReporteCambiosCodigosCuotasRepository _reporteCambiosCodigosCuotasRepository;
        IReporteCambiosCodigosCuotasRepository IUnitOfWork.ReporteCambiosCodigosCuotasRepository
        {
            get
            {
                return _reporteCambiosCodigosCuotasRepository ?? new ReporteCambiosCodigosCuotasRepository(_dapperRepository);
            }
        }
        private ICalidadProcesamientoAlternoRepository _calidadProcesamientoAlternoRepository;
        ICalidadProcesamientoAlternoRepository IUnitOfWork.CalidadProcesamientoAlternoRepository
        {
            get
            {
                return _calidadProcesamientoAlternoRepository ?? new CalidadProcesamientoAlternoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IContadorBicRepository _contadorBicRepository;
        IContadorBicRepository IUnitOfWork.ContadorBicRepository
        {
            get
            {
                return _contadorBicRepository ?? new ContadorBicRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IContadorBicLogRepository _contadorBicLogRepository;
        IContadorBicLogRepository IUnitOfWork.ContadorBicLogRepository
        {
            get
            {
                return _contadorBicLogRepository ?? new ContadorBicLogRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IContadorBicLogDetalleRepository _contadorBicLogDetalleRepository;
        IContadorBicLogDetalleRepository IUnitOfWork.ContadorBicLogDetalleRepository
        {
            get
            {
                return _contadorBicLogDetalleRepository ?? new ContadorBicLogDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IReportePresupuestoRepository _reportePresupuestoRepository;
        IReportePresupuestoRepository IUnitOfWork.ReportePresupuestoRepository
        {
            get
            {
                return _reportePresupuestoRepository ?? new ReportePresupuestoRepository(_dapperRepository);
            }
        }

        private IReporteIngresoRepository _reporteIngresoRepository;
        IReporteIngresoRepository IUnitOfWork.ReporteIngresoRepository
        {
            get
            {
                return _reporteIngresoRepository ?? new ReporteIngresoRepository(_dapperRepository);
            }
        }

        private IPespecificoCursoAdicionalRepository _pespecificoCursoAdicionalRepository;
        IPespecificoCursoAdicionalRepository IUnitOfWork.PespecificoCursoAdicionalRepository
        {
            get
            {
                return _pespecificoCursoAdicionalRepository ?? new PespecificoCursoAdicionalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICarreraPreRequisitoPgeneralRepository _carreraPreRequisitoPgeneralRepository;
        ICarreraPreRequisitoPgeneralRepository IUnitOfWork.CarreraPreRequisitoPgeneralRepository
        {
            get
            {
                return _carreraPreRequisitoPgeneralRepository ?? new CarreraPreRequisitoPgeneralRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICicloRepository _cicloRepository;
        ICicloRepository IUnitOfWork.CicloRepository
        {
            get
            {
                return _cicloRepository ?? new CicloRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPeriodoLectivoRepository _periodoLectivoRepository;
        IPeriodoLectivoRepository IUnitOfWork.PeriodoLectivoRepository
        {
            get
            {
                return _periodoLectivoRepository ?? new PeriodoLectivoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICrucigramaProgramaCapacitacionRepository _crucigramaProgramaCapacitacionRepository;
        ICrucigramaProgramaCapacitacionRepository IUnitOfWork.CrucigramaProgramaCapacitacionRepository
        {
            get
            {
                return _crucigramaProgramaCapacitacionRepository ?? new CrucigramaProgramaCapacitacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICrucigramaProgramaCapacitacionDetalleRepository _crucigramaProgramaCapacitacionDetalleRepository;
        ICrucigramaProgramaCapacitacionDetalleRepository IUnitOfWork.CrucigramaProgramaCapacitacionDetalleRepository
        {
            get
            {
                return _crucigramaProgramaCapacitacionDetalleRepository ?? new CrucigramaProgramaCapacitacionDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICentroCostoCertificadoRepository _centroCostoCertificadoRepository;
        ICentroCostoCertificadoRepository IUnitOfWork.CentroCostoCertificadoRepository
        {
            get
            {
                return _centroCostoCertificadoRepository ?? new CentroCostoCertificadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IFeedbackConfigurarDetalleRepository _feedbackConfigurarDetalleRepository;
        IFeedbackConfigurarDetalleRepository IUnitOfWork.FeedbackConfigurarDetalleRepository
        {
            get
            {
                return _feedbackConfigurarDetalleRepository ?? new FeedbackConfigurarDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }



        private ISexoRepository _sexoRepository;
        ISexoRepository IUnitOfWork.SexoRepository
        {
            get
            {
                return _sexoRepository ?? new SexoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IReportePendienteMesCoordinadoraRepository _reportePendienteMesCoordinadoraRepository;
        IReportePendienteMesCoordinadoraRepository IUnitOfWork.ReportePendienteMesCoordinadoraRepository
        {
            get
            {
                return _reportePendienteMesCoordinadoraRepository ?? new ReportePendienteMesCoordinadoraRepository(_dapperRepository);
            }
        }

        private IReporteComprobantesRepository _reporteComprobantesRepository;
        IReporteComprobantesRepository IUnitOfWork.ReporteComprobantesRepository
        {
            get
            {
                return _reporteComprobantesRepository ?? new ReporteComprobantesRepository(_dapperRepository);
            }
        }

        private IReporteDocumentosRepository _reporteDocumentosRepository;
        IReporteDocumentosRepository IUnitOfWork.ReporteDocumentosRepository
        {
            get
            {
                return _reporteDocumentosRepository ?? new ReporteDocumentosRepository(_dapperRepository);
            }
        }

        private ICongelamientoReporteFlujoRepository _congelamientoReporteFlujoRepository;
        ICongelamientoReporteFlujoRepository IUnitOfWork.CongelamientoReporteFlujoRepository
        {
            get
            {
                return _congelamientoReporteFlujoRepository ?? new CongelamientoReporteFlujoRepository(_dapperRepository);
            }
        }

        private ICongelamientoPeriodoReporteFlujoRepository _congelamientoPeriodoReporteFlujoRepository;
        ICongelamientoPeriodoReporteFlujoRepository IUnitOfWork.CongelamientoPeriodoReporteFlujoRepository
        {
            get
            {
                return _congelamientoPeriodoReporteFlujoRepository ?? new CongelamientoPeriodoReporteFlujoRepository(_dapperRepository);
            }
        }

        private IReportePendienteV2Repository _reportePendienteV2Repository;
        IReportePendienteV2Repository IUnitOfWork.ReportePendienteV2Repository
        {
            get
            {
                return _reportePendienteV2Repository ?? new ReportePendienteV2Repository(_dapperRepository);
            }
        }

        private IReporteFlujoCongeladoPorDiumRepository _reporteFlujoCongeladoPorDiumRepository;
        IReporteFlujoCongeladoPorDiumRepository IUnitOfWork.ReporteFlujoCongeladoPorDiumRepository
        {
            get
            {
                return _reporteFlujoCongeladoPorDiumRepository ?? new ReporteFlujoCongeladoPorDiumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }



        private ICronogramaOriginalesCongeladoPorDiumRepository _cronogramaOriginalesCongeladoPorDiumRepository;
        ICronogramaOriginalesCongeladoPorDiumRepository IUnitOfWork.CronogramaOriginalesCongeladoPorDiumRepository
        {
            get
            {
                return _cronogramaOriginalesCongeladoPorDiumRepository ?? new CronogramaOriginalesCongeladoPorDiumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IReportePagoTasaAcademicaRepository _reportePagoTasaAcademicaRepository;
        IReportePagoTasaAcademicaRepository IUnitOfWork.ReportePagoTasaAcademicaRepository
        {
            get
            {
                return _reportePagoTasaAcademicaRepository ?? new ReportePagoTasaAcademicaRepository(_dapperRepository);
            }
        }

        private IConfiguracionPeriodoMatriculaRepository _configuracionPeriodoMatriculaRepository;
        IConfiguracionPeriodoMatriculaRepository IUnitOfWork.ConfiguracionPeriodoMatriculaRepository
        {
            get
            {
                return _configuracionPeriodoMatriculaRepository ?? new ConfiguracionPeriodoMatriculaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IReportePorEstadoMatriculaRepository _reportePorEstadoMatriculaRepository;
        IReportePorEstadoMatriculaRepository IUnitOfWork.ReportePorEstadoMatriculaRepository
        {
            get
            {
                return _reportePorEstadoMatriculaRepository ?? new ReportePorEstadoMatriculaRepository(_dapperRepository);
            }
        }
        private IBandejaPendientePwRepository _bandejaPendientePwRepository;
        IBandejaPendientePwRepository IUnitOfWork.BandejaPendientePwRepository
        {
            get
            {
                return _bandejaPendientePwRepository ?? new BandejaPendientePwRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IModuloSistemaPaqueteRepository _moduloSistemaPaqueteRepository;
        IModuloSistemaPaqueteRepository IUnitOfWork.ModuloSistemaPaqueteRepository
        {
            get
            {
                return _moduloSistemaPaqueteRepository ?? new ModuloSistemaPaqueteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProcesoPagoIvrRepository _procesoPagoIvrRepository;
        IProcesoPagoIvrRepository IUnitOfWork.ProcesoPagoIvrRepository
        {
            get
            {
                return _procesoPagoIvrRepository ?? new ProcesoPagoIvrRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IReporteTresCxRepository _reporteTresCxRepository;
        IReporteTresCxRepository IUnitOfWork.ReporteTresCxRepository
        {
            get
            {
                return _reporteTresCxRepository ?? new ReporteTresCxRepository(_dapperRepository);
            }
        }

        private ICampaniaGeneralSmsRepository _campaniaGeneralSmsRepository;
        ICampaniaGeneralSmsRepository IUnitOfWork.CampaniaGeneralSmsRepository
        {
            get
            {
                return _campaniaGeneralSmsRepository ?? new CampaniaGeneralSmsRepository(_dapperRepository);
            }
        }

        private IRecordatorioClasesOnlineIvrRepository _recordatorioClasesOnlineIvrRepository;
        IRecordatorioClasesOnlineIvrRepository IUnitOfWork.RecordatorioClasesOnlineIvrRepository
        {
            get
            {
                return _recordatorioClasesOnlineIvrRepository ?? new RecordatorioClasesOnlineIvrRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPuestoTrabajoNivelRepository _maestroNivelPuestoTrabajoRepository;
        IPuestoTrabajoNivelRepository IUnitOfWork.MaestroNivelPuestoTrabajoRepository
        {
            get
            {
                return _maestroNivelPuestoTrabajoRepository ?? new PuestoTrabajoNivelRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IRecordatorioWebinarIvrRepository _recordatorioWebinarIvrRepository;
        IRecordatorioWebinarIvrRepository IUnitOfWork.RecordatorioWebinarIvrRepository
        {
            get
            {
                return _recordatorioWebinarIvrRepository ?? new RecordatorioWebinarIvrRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConfiguracionIntegraRepository _configuracionIntegraRepository;
        IConfiguracionIntegraRepository IUnitOfWork.ConfiguracionIntegraRepository
        {
            get
            {
                return _configuracionIntegraRepository ?? new ConfiguracionIntegraRepository(_dapperRepository);
            }
        }

        private IIvrPlantillaRepository _ivrPlantillaRepository;
        IIvrPlantillaRepository IUnitOfWork.IvrPlantillaRepository
        {
            get
            {
                return _ivrPlantillaRepository ?? new IvrPlantillaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IIvrTipoConfiguracionRepository _ivrTipoConfiguracionRepository;
        IIvrTipoConfiguracionRepository IUnitOfWork.IvrTipoConfiguracionRepository
        {
            get
            {
                return _ivrTipoConfiguracionRepository ?? new IvrTipoConfiguracionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IIvrEjecucionRepository _ivrEjecucionRepository;
        IIvrEjecucionRepository IUnitOfWork.IvrEjecucionRepository
        {
            get
            {
                return _ivrEjecucionRepository ?? new IvrEjecucionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICabeceraConfiguracionLlamadaAutomaticaRepository _cabeceraConfiguracionLlamadaAutomaticaRepository;
        ICabeceraConfiguracionLlamadaAutomaticaRepository IUnitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository
        {
            get
            {
                return _cabeceraConfiguracionLlamadaAutomaticaRepository ?? new CabeceraConfiguracionLlamadaAutomaticaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IAdwordsRepository _adwordsRepository;
        IAdwordsRepository IUnitOfWork.AdwordsRepository
        {
            get
            {
                return _adwordsRepository ?? new AdwordsRepository(_dapperRepository);
            }
        }

        private ILlamadaAutomaticaDetalleCabeceraConfiguracionRepository _llamadaAutomaticaDetalleCabeceraConfiguracionRepository;
        ILlamadaAutomaticaDetalleCabeceraConfiguracionRepository IUnitOfWork.LlamadaAutomaticaDetalleCabeceraConfiguracionRepository
        {
            get
            {
                return _llamadaAutomaticaDetalleCabeceraConfiguracionRepository ?? new LlamadaAutomaticaDetalleCabeceraConfiguracionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICategoriaEvaluacionRepository _categoriaEvaluacionRepository;
        ICategoriaEvaluacionRepository IUnitOfWork.CategoriaEvaluacionRepository
        {
            get
            {
                return _categoriaEvaluacionRepository ?? new CategoriaEvaluacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICategoriaPreguntaRepository _categoriaPreguntaRepository;
        ICategoriaPreguntaRepository IUnitOfWork.CategoriaPreguntaRepository
        {
            get
            {
                return _categoriaPreguntaRepository ?? new CategoriaPreguntaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        public IGradoEstudioRepository _gradoEstudioRepository;
        IGradoEstudioRepository IUnitOfWork.GradoEstudioRepository
        {
            get
            {
                return _gradoEstudioRepository ?? new GradoEstudioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        public INivelCompetenciaTecnicaRepository _nivelCompetenciaTecnicaRepository;
        INivelCompetenciaTecnicaRepository IUnitOfWork.NivelCompetenciaTecnicaRepository
        {
            get
            {
                return _nivelCompetenciaTecnicaRepository ?? new NivelCompetenciaTecnicaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        public ICentroEstudioRepository _centroEstudioRepository;
        ICentroEstudioRepository IUnitOfWork.CentroEstudioRepository
        {
            get
            {
                return _centroEstudioRepository ?? new CentroEstudioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        public ITipoCentroEstudioRepository _tipoCentroEstudioRepository;
        ITipoCentroEstudioRepository IUnitOfWork.TipoCentroEstudioRepository
        {
            get
            {
                return _tipoCentroEstudioRepository ?? new TipoCentroEstudioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        public ITipoExperienciaRepository _tipoExperienciaRepository;
        ITipoExperienciaRepository IUnitOfWork.TipoExperienciaRepository
        {
            get
            {
                return _tipoExperienciaRepository ?? new TipoExperienciaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IContratoEstadoRepository _contratoEstadoRepository;
        IContratoEstadoRepository IUnitOfWork.ContratoEstadoRepository
        {
            get
            {
                return _contratoEstadoRepository ?? new ContratoEstadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoFormacionRepository _tipoFormacionRepository;
        ITipoFormacionRepository IUnitOfWork.TipoFormacionRepository
        {
            get
            {
                return _tipoFormacionRepository ?? new TipoFormacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICriterioEvaluacionProcesoRepository _criterioEvaluacionProcesoRepository;
        ICriterioEvaluacionProcesoRepository IUnitOfWork.CriterioEvaluacionProcesoRepository
        {
            get
            {
                return _criterioEvaluacionProcesoRepository ?? new CriterioEvaluacionProcesoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IExamenRepository _examenRepository;
        IExamenRepository IUnitOfWork.ExamenRepository
        {
            get
            {
                return _examenRepository ?? new ExamenRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMaestroCursoComplementarioRepository _maestroCursoComplementarioRepository;
        IMaestroCursoComplementarioRepository IUnitOfWork.MaestroCursoComplementarioRepository
        {
            get
            {
                return _maestroCursoComplementarioRepository ?? new MaestroCursoComplementarioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICompetenciaTecnicaRepository _competenciaTecnicaRepository;
        ICompetenciaTecnicaRepository IUnitOfWork.CompetenciaTecnicaRepository
        {
            get
            {
                return _competenciaTecnicaRepository ?? new CompetenciaTecnicaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoCompetenciaTecnicaRepository _tipoCompetenciaTecnicaRepository;
        ITipoCompetenciaTecnicaRepository IUnitOfWork.TipoCompetenciaTecnicaRepository
        {
            get
            {
                return _tipoCompetenciaTecnicaRepository ?? new TipoCompetenciaTecnicaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IReporteIngresoCongelamientoRepository _reporteIngresoCongelamientoRepository;
        IReporteIngresoCongelamientoRepository IUnitOfWork.ReporteIngresoCongelamientoRepository
        {
            get
            {
                return _reporteIngresoCongelamientoRepository ?? new ReporteIngresoCongelamientoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IEstadoEtapaProcesoSeleccionRepository _estadoEtapaProcesoSeleccionRepository;
        IEstadoEtapaProcesoSeleccionRepository IUnitOfWork.EstadoEtapaProcesoSeleccionRepository
        {
            get
            {
                return _estadoEtapaProcesoSeleccionRepository ?? new EstadoEtapaProcesoSeleccionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPerfilPuestoTrabajoEstadoSolicitudRepository _perfilPuestoTrabajoEstadoSolicitudRepository;
        IPerfilPuestoTrabajoEstadoSolicitudRepository IUnitOfWork.PerfilPuestoTrabajoEstadoSolicitudRepository
        {
            get
            {
                return _perfilPuestoTrabajoEstadoSolicitudRepository ?? new PerfilPuestoTrabajoEstadoSolicitudRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IExamenFeedbackRepository _examenFeedbackRepository;
        IExamenFeedbackRepository IUnitOfWork.ExamenFeedbackRepository
        {
            get
            {
                return _examenFeedbackRepository ?? new ExamenFeedbackRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IFrecuenciaPuestoTrabajoRepository _frecuenciaPuestoTrabajoRepository;
        IFrecuenciaPuestoTrabajoRepository IUnitOfWork.FrecuenciaPuestoTrabajoRepository
        {
            get
            {
                return _frecuenciaPuestoTrabajoRepository ?? new FrecuenciaPuestoTrabajoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IGrupoComparacionProcesoSeleccionRepository _grupoComparacionProcesoSeleccionRepository;
        IGrupoComparacionProcesoSeleccionRepository IUnitOfWork.GrupoComparacionProcesoSeleccionRepository
        {
            get
            {
                return _grupoComparacionProcesoSeleccionRepository ?? new GrupoComparacionProcesoSeleccionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPostulanteComparacionRepository _postulanteComparacionRepository;
        IPostulanteComparacionRepository IUnitOfWork.PostulanteComparacionRepository
        {
            get
            {
                return _postulanteComparacionRepository ?? new PostulanteComparacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPuestoTrabajoGrupoComparacionRepository _puestoTrabajoGrupoComparacionRepository;
        IPuestoTrabajoGrupoComparacionRepository IUnitOfWork.PuestoTrabajoGrupoComparacionRepository
        {
            get
            {
                return _puestoTrabajoGrupoComparacionRepository ?? new PuestoTrabajoGrupoComparacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISedeTrabajoGrupoComparacionRepository _sedeTrabajoGrupoComparacionRepository;
        ISedeTrabajoGrupoComparacionRepository IUnitOfWork.SedeTrabajoGrupoComparacionRepository
        {
            get
            {
                return _sedeTrabajoGrupoComparacionRepository ?? new SedeTrabajoGrupoComparacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPostulanteNivelPotencialRepository _postulanteNivelPotencialRepository;
        IPostulanteNivelPotencialRepository IUnitOfWork.PostulanteNivelPotencialRepository
        {
            get
            {
                return _postulanteNivelPotencialRepository ?? new PostulanteNivelPotencialRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPersonalTipoFuncionRepository _personalTipoFuncionRepository;
        IPersonalTipoFuncionRepository IUnitOfWork.PersonalTipoFuncionRepository
        {
            get
            {
                return _personalTipoFuncionRepository ?? new PersonalTipoFuncionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPersonalRelacionExternaRepository _personalRelacionExternaRepository;
        IPersonalRelacionExternaRepository IUnitOfWork.PersonalRelacionExternaRepository
        {
            get
            {
                return _personalRelacionExternaRepository ?? new PersonalRelacionExternaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMensajeTiempoInactivoRepository _mensajeTiempoInactivoRepository;
        IMensajeTiempoInactivoRepository IUnitOfWork.MensajeTiempoInactivoRepository
        {
            get
            {
                return _mensajeTiempoInactivoRepository ?? new MensajeTiempoInactivoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoSangreRepository _tipoSangreRepository;
        ITipoSangreRepository IUnitOfWork.TipoSangreRepository
        {
            get
            {
                return _tipoSangreRepository ?? new TipoSangreRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEtapaProcesoSeleccionCalificadoRepository _etapaProcesoSeleccionCalificadoRepository;
        IEtapaProcesoSeleccionCalificadoRepository IUnitOfWork.EtapaProcesoSeleccionCalificadoRepository
        {
            get
            {
                return _etapaProcesoSeleccionCalificadoRepository ?? new EtapaProcesoSeleccionCalificadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IExamenAsignadoRepository _examenAsignadoRepository;
        IExamenAsignadoRepository IUnitOfWork.ExamenAsignadoRepository
        {
            get
            {
                return _examenAsignadoRepository ?? new ExamenAsignadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IExamenAsignadoEvaluadorRepository _examenAsignadoEvaluadorRepository;
        IExamenAsignadoEvaluadorRepository IUnitOfWork.ExamenAsignadoEvaluadorRepository
        {
            get
            {
                return _examenAsignadoEvaluadorRepository ?? new ExamenAsignadoEvaluadorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConfiguracionAsignacionEvaluacionRepository _configuracionAsignacionEvaluacionRepository;
        IConfiguracionAsignacionEvaluacionRepository IUnitOfWork.ConfiguracionAsignacionEvaluacionRepository
        {
            get
            {
                return _configuracionAsignacionEvaluacionRepository ?? new ConfiguracionAsignacionEvaluacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IExamenTestRepository _examenTestRepository;
        IExamenTestRepository IUnitOfWork.ExamenTestRepository
        {
            get
            {
                return _examenTestRepository ?? new ExamenTestRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IRespuestaPreguntaRepository _respuestaPreguntaRepository;
        IRespuestaPreguntaRepository IUnitOfWork.RespuestaPreguntaRepository
        {
            get
            {
                return _respuestaPreguntaRepository ?? new RespuestaPreguntaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IExamenRealizadoRespuestaEvaluadorRepository _examenRealizadoRespuestaEvaluadorRepository;
        IExamenRealizadoRespuestaEvaluadorRepository IUnitOfWork.ExamenRealizadoRespuestaEvaluadorRepository
        {
            get
            {
                return _examenRealizadoRespuestaEvaluadorRepository ?? new ExamenRealizadoRespuestaEvaluadorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoDocumentacionPersonalRepository _tipoDocumentacionPersonalRepository;
        ITipoDocumentacionPersonalRepository IUnitOfWork.TipoDocumentacionPersonalRepository
        {
            get
            {
                return _tipoDocumentacionPersonalRepository ?? new TipoDocumentacionPersonalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoEstudioRepository _tipoEstudioRepository;
        ITipoEstudioRepository IUnitOfWork.TipoEstudioRepository
        {
            get
            {
                return _tipoEstudioRepository ?? new TipoEstudioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPostulanteProcesoSeleccionRepository _postulanteProcesoSeleccionRepository;
        IPostulanteProcesoSeleccionRepository IUnitOfWork.PostulanteProcesoSeleccionRepository
        {
            get
            {
                return _postulanteProcesoSeleccionRepository ?? new PostulanteProcesoSeleccionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITokenPostulanteProcesoSeleccionRepository _tokenPostulanteProcesoSeleccionRepository;
        ITokenPostulanteProcesoSeleccionRepository IUnitOfWork.TokenPostulanteProcesoSeleccionRepository
        {
            get
            {
                return _tokenPostulanteProcesoSeleccionRepository ?? new TokenPostulanteProcesoSeleccionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IWhatsAppMensajeEnviadoPostulanteRepository _whatsAppMensajeEnviadoPostulanteRepository;
        IWhatsAppMensajeEnviadoPostulanteRepository IUnitOfWork.WhatsAppMensajeEnviadoPostulanteRepository
        {
            get
            {
                return _whatsAppMensajeEnviadoPostulanteRepository ?? new WhatsAppMensajeEnviadoPostulanteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPostulanteFormacionRepository _postulanteFormacionRepository;
        IPostulanteFormacionRepository IUnitOfWork.PostulanteFormacionRepository
        {
            get
            {
                return _postulanteFormacionRepository ?? new PostulanteFormacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPostulanteFormacionLogRepository _postulanteFormacionLogRepository;
        IPostulanteFormacionLogRepository IUnitOfWork.PostulanteFormacionLogRepository
        {
            get
            {
                return _postulanteFormacionLogRepository ?? new PostulanteFormacionLogRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPostulanteExperienciaRepository _postulanteExperienciaRepository;
        IPostulanteExperienciaRepository IUnitOfWork.PostulanteExperienciaRepository
        {
            get
            {
                return _postulanteExperienciaRepository ?? new PostulanteExperienciaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPostulanteExperienciaLogRepository _postulanteExperienciaLogRepository;
        IPostulanteExperienciaLogRepository IUnitOfWork.PostulanteExperienciaLogRepository
        {
            get
            {
                return _postulanteExperienciaLogRepository ?? new PostulanteExperienciaLogRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPostulanteLogRepository _postulanteLogRepository;
        IPostulanteLogRepository IUnitOfWork.PostulanteLogRepository
        {
            get
            {
                return _postulanteLogRepository ?? new PostulanteLogRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPuestoTrabajoRepository _puestoTrabajoRepository;
        IPuestoTrabajoRepository IUnitOfWork.PuestoTrabajoRepository
        {
            get
            {
                return _puestoTrabajoRepository ?? new PuestoTrabajoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IGrupoComponenteEvaluacionRepository _grupoComponenteEvaluacionRepository;
        IGrupoComponenteEvaluacionRepository IUnitOfWork.GrupoComponenteEvaluacionRepository
        {
            get
            {
                return _grupoComponenteEvaluacionRepository ?? new GrupoComponenteEvaluacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPostulanteIdiomaRepository _postulanteIdiomaRepository;
        IPostulanteIdiomaRepository IUnitOfWork.PostulanteIdiomaRepository
        {
            get
            {
                return _postulanteIdiomaRepository ?? new PostulanteIdiomaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPostulanteEquipoComputoRepository _postulanteEquipoComputoRepository;
        IPostulanteEquipoComputoRepository IUnitOfWork.PostulanteEquipoComputoRepository
        {
            get
            {
                return _postulanteEquipoComputoRepository ?? new PostulanteEquipoComputoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPostulanteCursoPortalNotasHistoricoRepository _postulanteCursoPortalNotasHistoricoRepository;
        IPostulanteCursoPortalNotasHistoricoRepository IUnitOfWork.PostulanteCursoPortalNotasHistoricoRepository
        {
            get
            {
                return _postulanteCursoPortalNotasHistoricoRepository ?? new PostulanteCursoPortalNotasHistoricoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IConfiguracionAsignacionExamenRepository _configuracionAsignacionExamenRepository;
        IConfiguracionAsignacionExamenRepository IUnitOfWork.ConfiguracionAsignacionExamenRepository
        {
            get
            {
                return _configuracionAsignacionExamenRepository ?? new ConfiguracionAsignacionExamenRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPostulanteAccesoTemporalAulaVirtualRepository _postulanteAccesoTemporalAulaVirtualRepository;
        IPostulanteAccesoTemporalAulaVirtualRepository IUnitOfWork.PostulanteAccesoTemporalAulaVirtualRepository
        {
            get
            {
                return _postulanteAccesoTemporalAulaVirtualRepository ?? new PostulanteAccesoTemporalAulaVirtualRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPersonalAccesoTemporalAulaVirtualRepository _personalAccesoTemporalAulaVirtualRepository;
        IPersonalAccesoTemporalAulaVirtualRepository IUnitOfWork.PersonalAccesoTemporalAulaVirtualRepository
        {
            get
            {
                return _personalAccesoTemporalAulaVirtualRepository ?? new PersonalAccesoTemporalAulaVirtualRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProcesoSeleccionEtapaRepository _procesoSeleccionEtapaRepository;
        IProcesoSeleccionEtapaRepository IUnitOfWork.ProcesoSeleccionEtapaRepository
        {
            get
            {
                return _procesoSeleccionEtapaRepository ?? new ProcesoSeleccionEtapaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICentilRepository _centilRepository;
        ICentilRepository IUnitOfWork.CentilRepository
        {
            get
            {
                return _centilRepository ?? new CentilRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IFormulaPuntajeRepository _formulaPuntajeRepository;
        IFormulaPuntajeRepository IUnitOfWork.FormulaPuntajeRepository
        {
            get
            {
                return _formulaPuntajeRepository ?? new FormulaPuntajeRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IAsignacionPreguntaExamenRepository _asignacionPreguntaExamenRepository;
        IAsignacionPreguntaExamenRepository IUnitOfWork.AsignacionPreguntaExamenRepository
        {
            get
            {
                return _asignacionPreguntaExamenRepository ?? new AsignacionPreguntaExamenRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IWolkboxRepository _wolkboxRepository;
        IWolkboxRepository IUnitOfWork.WolkboxRepository
        {
            get
            {
                return _wolkboxRepository ?? new WolkboxRepository(_dapperRepository);
            }
        }

        private IRegistroCertificadoFisicoGeneradoRepository _registroCertificadoFisicoGeneradoRepository;
        IRegistroCertificadoFisicoGeneradoRepository IUnitOfWork.RegistroCertificadoFisicoGeneradoRepository
        {
            get
            {
                return _registroCertificadoFisicoGeneradoRepository ?? new RegistroCertificadoFisicoGeneradoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMunicipioRepository _municipioRepository;
        IMunicipioRepository IUnitOfWork.MunicipioRepository
        {
            get
            {
                return _municipioRepository ?? new MunicipioRepository(_dapperRepository);
            }
        }
        private IAsentamientoMunicipioRepository _asentamientoMunicipioRepository;
        IAsentamientoMunicipioRepository IUnitOfWork.AsentamientoMunicipioRepository
        {
            get
            {
                return _asentamientoMunicipioRepository ?? new AsentamientoMunicipioRepository(_dapperRepository);
            }
        }
        private ILinkedInApiRepository _linkedInApiRepository;
        ILinkedInApiRepository IUnitOfWork.LinkedInApiRepository
        {
            get
            {
                return _linkedInApiRepository ?? new LinkedInApiRepository(_dapperRepository);
            }
        }


        private IPreguntaEncuestaCategoriaRepository _preguntaEncuestaCategoriaRepository;
        IPreguntaEncuestaCategoriaRepository IUnitOfWork.PreguntaEncuestaCategoriaRepository
        {
            get
            {
                return _preguntaEncuestaCategoriaRepository ?? new PreguntaEncuestaCategoriaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPreguntaEncuestaRepository _preguntaEncuestaRepository;
        IPreguntaEncuestaRepository IUnitOfWork.PreguntaEncuestaRepository
        {
            get
            {
                return _preguntaEncuestaRepository ?? new PreguntaEncuestaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPreguntaEncuestaRespuestaRepository _preguntaEncuestaRespuestaRepository;
        IPreguntaEncuestaRespuestaRepository IUnitOfWork.PreguntaEncuestaRespuestaRepository
        {
            get
            {
                return _preguntaEncuestaRespuestaRepository ?? new PreguntaEncuestaRespuestaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IEncuestaOnlineRepository _encuestaOnlineRepository;
        IEncuestaOnlineRepository IUnitOfWork.EncuestaOnlineRepository
        {
            get
            {
                return _encuestaOnlineRepository ?? new EncuestaOnlineRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPreguntaEncuestaOnlineRepository _preguntaEncuestaOnlineRepository;
        IPreguntaEncuestaOnlineRepository IUnitOfWork.PreguntaEncuestaOnlineRepository
        {
            get
            {
                return _preguntaEncuestaOnlineRepository ?? new PreguntaEncuestaOnlineRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ITipoEncuestumRepository _tipoEncuestumRepository;
        ITipoEncuestumRepository IUnitOfWork.TipoEncuestumRepository
        {
            get
            {
                return _tipoEncuestumRepository ?? new TipoEncuestumRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IEncuestaSesionProgramaRepository _encuestaSesionProgramaRepository;
        IEncuestaSesionProgramaRepository IUnitOfWork.EncuestaSesionProgramaRepository
        {
            get
            {
                return _encuestaSesionProgramaRepository ?? new EncuestaSesionProgramaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IWhatsAppUsuarioRepository _whatsAppUsuarioRepository;
        IWhatsAppUsuarioRepository IUnitOfWork.WhatsAppUsuarioRepository
        {
            get
            {
                return _whatsAppUsuarioRepository ?? new WhatsAppUsuarioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IWhatsAppConfiguracionApiRepository _whatsAppConfiguracionApiRepository;
        IWhatsAppConfiguracionApiRepository IUnitOfWork.WhatsAppConfiguracionApiRepository
        {
            get
            {
                return _whatsAppConfiguracionApiRepository ?? new WhatsAppConfiguracionApiRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDepartamentoPaiRepository _departamentoPaiRepository;
        IDepartamentoPaiRepository IUnitOfWork.DepartamentoPaiRepository
        {
            get
            {
                return _departamentoPaiRepository ?? new DepartamentoPaiRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ICiudadDepartamentoPaiRepository _ciudadDepartamentoPaiRepository;
        ICiudadDepartamentoPaiRepository IUnitOfWork.CiudadDepartamentoPaiRepository
        {
            get
            {
                return _ciudadDepartamentoPaiRepository ?? new CiudadDepartamentoPaiRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ISiigoApiRepository _siigoApiRepository;
        ISiigoApiRepository IUnitOfWork.SiigoApiRepository
        {
            get
            {
                return _siigoApiRepository ?? new SiigoApiRepository(_dapperRepository);
            }
        }

        private IWavixRepository _wavixRepository;
        IWavixRepository IUnitOfWork.WavixRepository
        {
            get
            {
                return _wavixRepository ?? new WavixRepository(_dapperRepository);
            }
        }


        private IProgramaGeneralPresentacionArgumentoRepository _programaGeneralPresentacionArgumentoRepository;
        IProgramaGeneralPresentacionArgumentoRepository IUnitOfWork.ProgramaGeneralPresentacionArgumentoRepository
        {
            get
            {
                return _programaGeneralPresentacionArgumentoRepository ?? new ProgramaGeneralPresentacionArgumentoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IGestionRemuneracionPuestoTrabajoRepository _gestionRemuneracionPuestoTrabajoRepository;
        IGestionRemuneracionPuestoTrabajoRepository IUnitOfWork.GestionRemuneracionPuestoTrabajoRepository
        {
            get
            {
                return _gestionRemuneracionPuestoTrabajoRepository ?? new GestionRemuneracionPuestoTrabajoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IGestionRemuneracionPuestoTrabajoDetalleRepository _gestionRemuneracionPuestoTrabajoDetalleRepository;
        IGestionRemuneracionPuestoTrabajoDetalleRepository IUnitOfWork.GestionRemuneracionPuestoTrabajoDetalleRepository
        {
            get
            {
                return _gestionRemuneracionPuestoTrabajoDetalleRepository ?? new GestionRemuneracionPuestoTrabajoDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMaestroEvaluacionRepository _maestroEvaluacionRepository;
        IMaestroEvaluacionRepository IUnitOfWork.MaestroEvaluacionRepository
        {
            get
            {
                return _maestroEvaluacionRepository ?? new MaestroEvaluacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralPresentacionArgumentoModalidadRepository _programaGeneralPresentacionArgumentoModalidadRepository;
        IProgramaGeneralPresentacionArgumentoModalidadRepository IUnitOfWork.ProgramaGeneralPresentacionArgumentoModalidadRepository
        {
            get
            {
                return _programaGeneralPresentacionArgumentoModalidadRepository ?? new ProgramaGeneralPresentacionArgumentoModalidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralPresentacionArgumentoDetalleSolucionRepository _programaGeneralPresentacionArgumentoDetalleSolucionRepository;
        IProgramaGeneralPresentacionArgumentoDetalleSolucionRepository IUnitOfWork.ProgramaGeneralPresentacionArgumentoDetalleSolucionRepository
        {
            get
            {
                return _programaGeneralPresentacionArgumentoDetalleSolucionRepository ?? new ProgramaGeneralPresentacionArgumentoDetalleSolucionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFormularioProgresivoRepository _formularioProgresivoRepository;
        IFormularioProgresivoRepository IUnitOfWork.FormularioProgresivoRepository
        {
            get
            {
                return _formularioProgresivoRepository ?? new FormularioProgresivoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFormularioProgresivoTipoRepository _formularioProgresivoTipoRepository;
        IFormularioProgresivoTipoRepository IUnitOfWork.FormularioProgresivoTipoRepository
        {
            get
            {
                return _formularioProgresivoTipoRepository ?? new FormularioProgresivoTipoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFormularioProgresivoCondicionMostrarRepository _formularioProgresivoCondicionMostrarRepository;
        IFormularioProgresivoCondicionMostrarRepository IUnitOfWork.FormularioProgresivoCondicionMostrarRepository
        {
            get
            {
                return _formularioProgresivoCondicionMostrarRepository ?? new FormularioProgresivoCondicionMostrarRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFormularioProgresivoAccionBotonRepository _formularioProgresivoAccionBotonRepository;
        IFormularioProgresivoAccionBotonRepository IUnitOfWork.FormularioProgresivoAccionBotonRepository
        {
            get
            {
                return _formularioProgresivoAccionBotonRepository ?? new FormularioProgresivoAccionBotonRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFormularioProgresivoSeccionPortalRepository _formularioProgresivoSeccionPortalRepository;
        IFormularioProgresivoSeccionPortalRepository IUnitOfWork.FormularioProgresivoSeccionPortalRepository
        {
            get
            {
                return _formularioProgresivoSeccionPortalRepository ?? new FormularioProgresivoSeccionPortalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFormularioProgresivoConfiguracionBotonRepository _formularioProgresivoConfiguracionBotonRepository;
        IFormularioProgresivoConfiguracionBotonRepository IUnitOfWork.FormularioProgresivoConfiguracionBotonRepository
        {
            get
            {
                return _formularioProgresivoConfiguracionBotonRepository ?? new FormularioProgresivoConfiguracionBotonRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private ILlamadaWavixWebhookRepository _llamadaWavixWebhookRepository;
        ILlamadaWavixWebhookRepository IUnitOfWork.LlamadaWavixWebhookRepository
        {
            get
            {
                return _llamadaWavixWebhookRepository ?? new LlamadaWavixWebhookRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        public IWhatsAppMensajeRecibidoPostulanteRepository _whatsAppMensajeRecibidoPostulanteRepository;
        IWhatsAppMensajeRecibidoPostulanteRepository IUnitOfWork.WhatsAppMensajeRecibidoPostulanteRepository
        {
            get
            {
                return _whatsAppMensajeRecibidoPostulanteRepository ?? new WhatsAppMensajeRecibidoPostulanteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProcesoSeleccionRangoRepository _procesoSeleccionRangoRepository;
        IProcesoSeleccionRangoRepository IUnitOfWork.ProcesoSeleccionRangoRepository
        {
            get
            {
                return _procesoSeleccionRangoRepository ?? new ProcesoSeleccionRangoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPerfilPuestoTrabajoRepository _perfilPuestoTrabajoRepository;
        IPerfilPuestoTrabajoRepository IUnitOfWork.PerfilPuestoTrabajoRepository
        {
            get
            {
                return _perfilPuestoTrabajoRepository ?? new PerfilPuestoTrabajoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPuestoTrabajoFuncionRepository _puestoTrabajoFuncionRepository;
        IPuestoTrabajoFuncionRepository IUnitOfWork.PuestoTrabajoFuncionRepository
        {
            get
            {
                return _puestoTrabajoFuncionRepository ?? new PuestoTrabajoFuncionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPuestoTrabajoReporteRepository _puestoTrabajoReporteRepository;
        IPuestoTrabajoReporteRepository IUnitOfWork.PuestoTrabajoReporteRepository
        {
            get
            {
                return _puestoTrabajoReporteRepository ?? new PuestoTrabajoReporteRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPuestoTrabajoCursoComplementarioRepository _puestoTrabajoCursoComplementarioRepository;
        IPuestoTrabajoCursoComplementarioRepository IUnitOfWork.PuestoTrabajoCursoComplementarioRepository
        {
            get
            {
                return _puestoTrabajoCursoComplementarioRepository ?? new PuestoTrabajoCursoComplementarioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPuestoTrabajoExperienciaRepository _puestoTrabajoExperienciaRepository;
        IPuestoTrabajoExperienciaRepository IUnitOfWork.PuestoTrabajoExperienciaRepository
        {
            get
            {
                return _puestoTrabajoExperienciaRepository ?? new PuestoTrabajoExperienciaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPuestoTrabajoCaracteristicaPersonalRepository _puestoTrabajoCaracteristicaPersonalRepository;
        IPuestoTrabajoCaracteristicaPersonalRepository IUnitOfWork.PuestoTrabajoCaracteristicaPersonalRepository
        {
            get
            {
                return _puestoTrabajoCaracteristicaPersonalRepository ?? new PuestoTrabajoCaracteristicaPersonalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPuestoTrabajoFormacionAcademicaRepository _puestoTrabajoFormacionAcademicaRepository;
        IPuestoTrabajoFormacionAcademicaRepository IUnitOfWork.PuestoTrabajoFormacionAcademicaRepository
        {
            get
            {
                return _puestoTrabajoFormacionAcademicaRepository ?? new PuestoTrabajoFormacionAcademicaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPuestoTrabajoPuntajeCalificacionRepository _puestoTrabajoPuntajeCalificacionRepository;
        IPuestoTrabajoPuntajeCalificacionRepository IUnitOfWork.PuestoTrabajoPuntajeCalificacionRepository
        {
            get
            {
                return _puestoTrabajoPuntajeCalificacionRepository ?? new PuestoTrabajoPuntajeCalificacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEstadoCivilRepository _estadoCivilRepository;
        IEstadoCivilRepository IUnitOfWork.EstadoCivilRepository
        {
            get
            {
                return _estadoCivilRepository ?? new EstadoCivilRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPuestoTrabajoRelacionDetalleRepository _puestoTrabajoRelacionDetalleRepository;
        IPuestoTrabajoRelacionDetalleRepository IUnitOfWork.PuestoTrabajoRelacionDetalleRepository
        {
            get
            {
                return _puestoTrabajoRelacionDetalleRepository ?? new PuestoTrabajoRelacionDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPuestoTrabajoRelacionRepository _puestoTrabajoRelacionRepository;
        IPuestoTrabajoRelacionRepository IUnitOfWork.PuestoTrabajoRelacionRepository
        {
            get
            {
                return _puestoTrabajoRelacionRepository ?? new PuestoTrabajoRelacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IModuloSistemaPuestoTrabajoRepository _moduloSistemaPuestoTrabajoRepository;
        IModuloSistemaPuestoTrabajoRepository IUnitOfWork.ModuloSistemaPuestoTrabajoRepository
        {
            get
            {
                return _moduloSistemaPuestoTrabajoRepository ?? new ModuloSistemaPuestoTrabajoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IModuloSistemaAccesoRepository _moduloSistemaAccesoRepository;
        IModuloSistemaAccesoRepository IUnitOfWork.ModuloSistemaAccesoRepository
        {
            get
            {
                return _moduloSistemaAccesoRepository ?? new ModuloSistemaAccesoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IPersonalPuestoSedeHistoricoRepository _personalPuestoSedeHistoricoRepository;
        IPersonalPuestoSedeHistoricoRepository IUnitOfWork.PersonalPuestoSedeHistoricoRepository
        {
            get
            {
                return _personalPuestoSedeHistoricoRepository ?? new PersonalPuestoSedeHistoricoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEntidadSistemaPensionarioRepository _entidadSistemaPensionarioRepository;
        IEntidadSistemaPensionarioRepository IUnitOfWork.EntidadSistemaPensionarioRepository
        {
            get
            {
                return _entidadSistemaPensionarioRepository ?? new EntidadSistemaPensionarioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoDocumentoPersonalRepository _tipoDocumentoPersonalRepository;
        ITipoDocumentoPersonalRepository IUnitOfWork.TipoDocumentoPersonalRepository
        {
            get
            {
                return _tipoDocumentoPersonalRepository ?? new TipoDocumentoPersonalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMotivoCeseRepository _motivoCeseRepository;
        IMotivoCeseRepository IUnitOfWork.MotivoCeseRepository
        {
            get
            {
                return _motivoCeseRepository ?? new MotivoCeseRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IEntidadSeguroSaludRepository _entidadSeguroSaludRepository;
        IEntidadSeguroSaludRepository IUnitOfWork.EntidadSeguroSaludRepository
        {
            get
            {
                return _entidadSeguroSaludRepository ?? new EntidadSeguroSaludRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private INivelIdiomaRepository _nivelIdiomaRepository;
        INivelIdiomaRepository IUnitOfWork.NivelIdiomaRepository
        {
            get
            {
                return _nivelIdiomaRepository ?? new NivelIdiomaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IParentescoPersonalRepository _parentescoPersonalRepository;
        IParentescoPersonalRepository IUnitOfWork.ParentescoPersonalRepository
        {
            get
            {
                return _parentescoPersonalRepository ?? new ParentescoPersonalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ITipoPagoRemuneracionRepository _tipoPagoRemuneracionRepository;
        ITipoPagoRemuneracionRepository IUnitOfWork.TipoPagoRemuneracionRepository
        {
            get
            {
                return _tipoPagoRemuneracionRepository ?? new TipoPagoRemuneracionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMotivoInactividadRepository _motivoInactividadRepository;
        IMotivoInactividadRepository IUnitOfWork.MotivoInactividadRepository
        {
            get
            {
                return _motivoInactividadRepository ?? new MotivoInactividadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ISistemaPensionarioRepository _sistemaPensionarioRepository;
        ISistemaPensionarioRepository IUnitOfWork.SistemaPensionarioRepository
        {
            get
            {
                return _sistemaPensionarioRepository ?? new SistemaPensionarioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMelissaRepository _melissaRepository;
        IMelissaRepository IUnitOfWork.MelissaRepository
        {
            get
            {
                return _melissaRepository ?? new MelissaRepository(_dapperRepository);
            }
        }
        private IDatoContratoPersonalRepository _datoContratoPersonalRepository;
        IDatoContratoPersonalRepository IUnitOfWork.DatoContratoPersonalRepository
        {
            get
            {
                return _datoContratoPersonalRepository ?? new DatoContratoPersonalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IFacturamaRepository _facturamaRepository;
        IFacturamaRepository IUnitOfWork.FacturamaRepository
        {
            get
            {
                return _facturamaRepository ?? new FacturamaRepository(_dapperRepository);
            }
        }
        private IPerfilPuestoTrabajoPersonalAprobacionRepository _perfilPuestoTrabajoPersonalAprobacionRepository;
        IPerfilPuestoTrabajoPersonalAprobacionRepository IUnitOfWork.PerfilPuestoTrabajoPersonalAprobacionRepository
        {
            get
            {
                return _perfilPuestoTrabajoPersonalAprobacionRepository ?? new PerfilPuestoTrabajoPersonalAprobacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPersonalRemuneracionRepository _personalRemuneracionRepository;
        IPersonalRemuneracionRepository IUnitOfWork.PersonalRemuneracionRepository
        {
            get
            {
                return _personalRemuneracionRepository ?? new PersonalRemuneracionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPersonalCeseRepository _personalCeseRepository;
        IPersonalCeseRepository IUnitOfWork.PersonalCeseRepository
        {
            get
            {
                return _personalCeseRepository ?? new PersonalCeseRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPersonalFormacionRepository _personalFormacionRepository;
        IPersonalFormacionRepository IUnitOfWork.PersonalFormacionRepository
        {
            get
            {
                return _personalFormacionRepository ?? new PersonalFormacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPersonalComputoRepository _personalComputoRepository;
        IPersonalComputoRepository IUnitOfWork.PersonalComputoRepository
        {
            get
            {
                return _personalComputoRepository ?? new PersonalComputoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPersonalIdiomaRepository _personalIdiomaRepository;
        IPersonalIdiomaRepository IUnitOfWork.PersonalIdiomaRepository
        {
            get
            {
                return _personalIdiomaRepository ?? new PersonalIdiomaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPersonalCertificacionRepository _personalCertificacionRepository;
        IPersonalCertificacionRepository IUnitOfWork.PersonalCertificacionRepository
        {
            get
            {
                return _personalCertificacionRepository ?? new PersonalCertificacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPersonalExperienciaRepository _personalExperienciaRepository;
        IPersonalExperienciaRepository IUnitOfWork.PersonalExperienciaRepository
        {
            get
            {
                return _personalExperienciaRepository ?? new PersonalExperienciaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPersonalInformacionMedicaRepository _personalInformacionMedicaRepository;
        IPersonalInformacionMedicaRepository IUnitOfWork.PersonalInformacionMedicaRepository
        {
            get
            {
                return _personalInformacionMedicaRepository ?? new PersonalInformacionMedicaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPersonalHistorialMedicoRepository _personalHistorialMedicoRepository;
        IPersonalHistorialMedicoRepository IUnitOfWork.PersonalHistorialMedicoRepository
        {
            get
            {
                return _personalHistorialMedicoRepository ?? new PersonalHistorialMedicoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPersonalSistemaPensionarioRepository _personalSistemaPensionarioRepository;
        IPersonalSistemaPensionarioRepository IUnitOfWork.PersonalSistemaPensionarioRepository
        {
            get
            {
                return _personalSistemaPensionarioRepository ?? new PersonalSistemaPensionarioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPersonalSeguroSaludRepository _personalSeguroSaludRepository;
        IPersonalSeguroSaludRepository IUnitOfWork.PersonalSeguroSaludRepository
        {
            get
            {
                return _personalSeguroSaludRepository ?? new PersonalSeguroSaludRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDatoFamiliarPersonalRepository _datoFamiliarPersonalRepository;
        IDatoFamiliarPersonalRepository IUnitOfWork.DatoFamiliarPersonalRepository
        {
            get
            {
                return _datoFamiliarPersonalRepository ?? new DatoFamiliarPersonalRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPersonalLogRepository _personalLogRepository;
        IPersonalLogRepository IUnitOfWork.PersonalLogRepository
        {
            get
            {
                return _personalLogRepository ?? new PersonalLogRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPersonalMotivoTiempoInactividadRepository _personalMotivoTiempoInactividadRepository;
        IPersonalMotivoTiempoInactividadRepository IUnitOfWork.PersonalMotivoTiempoInactividadRepository
        {
            get
            {
                return _personalMotivoTiempoInactividadRepository ?? new PersonalMotivoTiempoInactividadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPersonalDireccionRepository _personalDireccionRepository;
        IPersonalDireccionRepository IUnitOfWork.PersonalDireccionRepository
        {
            get
            {
                return _personalDireccionRepository ?? new PersonalDireccionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPersonalArchivoRepository _personalArchivoRepository;
        IPersonalArchivoRepository IUnitOfWork.PersonalArchivoRepository
        {
            get
            {
                return _personalArchivoRepository ?? new PersonalArchivoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IAsesorMarcadorRepository _asesorMarcadorRepository;
        IAsesorMarcadorRepository IUnitOfWork.AsesorMarcadorRepository
        {
            get
            {
                return _asesorMarcadorRepository ?? new AsesorMarcadorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IGrabacionesClasesOnlineRepository _grabacionesClasesOnlineRepository;
        IGrabacionesClasesOnlineRepository IUnitOfWork.GrabacionesClasesOnlineRepository
        {
            get
            {
                return _grabacionesClasesOnlineRepository ?? new GrabacionesClasesOnlineRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IConfiguracionResumenGrabacionOnlineRepository _configuracionResumenGrabacionOnlineRepository;
        IConfiguracionResumenGrabacionOnlineRepository IUnitOfWork.ConfiguracionResumenGrabacionOnlineRepository
        {
            get
            {
                return _configuracionResumenGrabacionOnlineRepository ?? new ConfiguracionResumenGrabacionOnlineRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IResumenGrabacionOnlineRepository _resumenGrabacionOnlineRepository;
        IResumenGrabacionOnlineRepository IUnitOfWork.ResumenGrabacionOnlineRepository
        {
            get
            {
                return _resumenGrabacionOnlineRepository ?? new ResumenGrabacionOnlineRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IDatoContratoComisionBonoRepository _datoContratoComisionBonoRepository;
        IDatoContratoComisionBonoRepository IUnitOfWork.DatoContratoComisionBonoRepository
        {
            get
            {
                return _datoContratoComisionBonoRepository ?? new DatoContratoComisionBonoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        //victor hinojosa

        private ICentralLlamadaDireccionRepository _centralLlamadaDireccionRepository;
        ICentralLlamadaDireccionRepository IUnitOfWork.CentralLlamadaDireccionRepository
        {
            get
            {
                return _centralLlamadaDireccionRepository ?? new CentralLlamadaDireccionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        //private IPersonalLogRepository _personalLogRepository;
        //IPersonalLogRepository IUnitOfWork.PersonalLogRepository
        //{
        //    get
        //    {
        //        return _personalLogRepository ?? new PersonalLogRepository(_context, _connectionFactory, _dapperRepository);
        //    }
        //}

        private IEvaluacionCategoriaRepository _evaluacionCategoriaRepository;
        IEvaluacionCategoriaRepository IUnitOfWork.EvaluacionCategoriaRepository
        {
            get
            {
                return _evaluacionCategoriaRepository ?? new EvaluacionCategoriaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPEspecificoCodigoPartnerRepository _pEspecificoCodigoPartnerRepository;
        IPEspecificoCodigoPartnerRepository IUnitOfWork.PEspecificoCodigoPartnerRepository
        {
            get
            {
                return _pEspecificoCodigoPartnerRepository ?? new PEspecificoCodigoPartnerRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMatriculaFormularioProgresivoRepository _matriculaFormularioProgresivoRepository;
        IMatriculaFormularioProgresivoRepository IUnitOfWork.MatriculaFormularioProgresivoRepository
        {
            get
            {
                return _matriculaFormularioProgresivoRepository ?? new MatriculaFormularioProgresivoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProcesoSeleccionPuntajeCalificacionRepository _procesoSeleccionPuntajeCalificacionRepository;
        IProcesoSeleccionPuntajeCalificacionRepository IUnitOfWork.ProcesoSeleccionPuntajeCalificacionRepository
        {
            get
            {
                return _procesoSeleccionPuntajeCalificacionRepository ?? new ProcesoSeleccionPuntajeCalificacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralProblemaFactorRepository _programaGeneralProblemaFactorRepository;

        IProgramaGeneralProblemaFactorRepository IUnitOfWork.ProgramaGeneralProblemaFactorRepository
        {
            get
            {
                return _programaGeneralProblemaFactorRepository ?? new ProgramaGeneralProblemaFactorRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IProgramaGeneralProblemaFactorDetalleRepository _programaGeneralProblemaFactorDetalleRepository;

        IProgramaGeneralProblemaFactorDetalleRepository IUnitOfWork.ProgramaGeneralProblemaFactorDetalleRepository
        {
            get
            {
                return _programaGeneralProblemaFactorDetalleRepository ?? new ProgramaGeneralProblemaFactorDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralProblemaFactorSolucionRepository _programaGeneralProblemaFactorSolucionRepository;

        IProgramaGeneralProblemaFactorSolucionRepository IUnitOfWork.ProgramaGeneralProblemaFactorSolucionRepository
        {
            get
            {
                return _programaGeneralProblemaFactorSolucionRepository ?? new ProgramaGeneralProblemaFactorSolucionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralProblemaFactorSubSolucionRepository _programaGeneralProblemaFactorSubSolucionRepository;

        IProgramaGeneralProblemaFactorSubSolucionRepository IUnitOfWork.ProgramaGeneralProblemaFactorSubSolucionRepository
        {
            get
            {
                return _programaGeneralProblemaFactorSubSolucionRepository ?? new ProgramaGeneralProblemaFactorSubSolucionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }


        private IProgramaGeneralArgumentoRepository _programaGeneralArgumentoRepository;

        IProgramaGeneralArgumentoRepository IUnitOfWork.ProgramaGeneralArgumentoRepository
        {
            get
            {
                return _programaGeneralArgumentoRepository ?? new ProgramaGeneralArgumentoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralArgumentoDetalleRepository _programaGeneralArgumentoDetalleRepository;

        IProgramaGeneralArgumentoDetalleRepository IUnitOfWork.ProgramaGeneralArgumentoDetalleRepository
        {
            get
            {
                return _programaGeneralArgumentoDetalleRepository ?? new ProgramaGeneralArgumentoDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralArgumentoModalidadRepository _programaGeneralArgumentoModalidadRepository;

        IProgramaGeneralArgumentoModalidadRepository IUnitOfWork.ProgramaGeneralArgumentoModalidadRepository
        {
            get
            {
                return _programaGeneralArgumentoModalidadRepository ?? new ProgramaGeneralArgumentoModalidadRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralArgumentoDetalleMotivacionRepository _programaGeneralArgumentoDetalleMotivacionRepository;

        IProgramaGeneralArgumentoDetalleMotivacionRepository IUnitOfWork.ProgramaGeneralArgumentoDetalleMotivacionRepository
        {
            get
            {
                return _programaGeneralArgumentoDetalleMotivacionRepository ?? new ProgramaGeneralArgumentoDetalleMotivacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IProgramaGeneralProblemaDetalleRepository _programaGeneralProblemaDetalleRepository;

        IProgramaGeneralProblemaDetalleRepository IUnitOfWork.ProgramaGeneralProblemaDetalleRepository
        {
            get
            {
                return _programaGeneralProblemaDetalleRepository ?? new ProgramaGeneralProblemaDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralProblemaFactorSubSolucionAsignadaRepository _programaGeneralProblemaFactorSubSolucionAsignadaRepository;

        IProgramaGeneralProblemaFactorSubSolucionAsignadaRepository IUnitOfWork.ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository
        {
            get
            {
                return _programaGeneralProblemaFactorSubSolucionAsignadaRepository ?? new ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IProgramaGeneralProblemaFactorSolucionRespuestaSolucionRespuestaRepository _programaGeneralProblemaFactorSolucionRespuestaSolucionRespuestaRepository;
        IProgramaGeneralProblemaFactorSolucionRespuestaSolucionRespuestaRepository IUnitOfWork.ProgramaGeneralProblemaFactorSolucionRespuestaSolucionRespuestaRepository
        {
            get
            {
                return _programaGeneralProblemaFactorSolucionRespuestaSolucionRespuestaRepository ?? new ProgramaGeneralProblemaFactorSolucionRespuestaSolucionRespuestaRepository(_context, _connectionFactory, _dapperRepository);
            }

        }
        private IProgramaMotivacionRepository _programaMotivacionRepository;
        IProgramaMotivacionRepository IUnitOfWork.ProgramaMotivacionRepository
        {
            get
            {
                return _programaMotivacionRepository ?? new ProgramaMotivacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IOportunidadProgramaMotivacionSeleccionRepository _oportunidadProgramaMotivacionSeleccionRepository;
        IOportunidadProgramaMotivacionSeleccionRepository IUnitOfWork.OportunidadProgramaMotivacionSeleccionRepository
        {
            get
            {
                return _oportunidadProgramaMotivacionSeleccionRepository ?? new OportunidadProgramaMotivacionSeleccionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IMessengerFacebookChatRepository _messengerFacebookChatRepository;
        IMessengerFacebookChatRepository IUnitOfWork.MessengerFacebookChatRepository
        {
            get
            {
                return _messengerFacebookChatRepository ?? new MessengerFacebookChatRepository(_dapperRepository);
            }
        }

        private IPaqueteTutorVirtualRepository _paqueteTutorVirtualRepository;
        IPaqueteTutorVirtualRepository IUnitOfWork.PaqueteTutorVirtualRepository
        {
            get
            {
                return _paqueteTutorVirtualRepository ?? new PaqueteTutorVirtualRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPaqueteTutorVirtualPaisRepository _paqueteTutorVirtualPaisRepository;
        IPaqueteTutorVirtualPaisRepository IUnitOfWork.PaqueteTutorVirtualPaisRepository
        {
            get
            {
                return _paqueteTutorVirtualPaisRepository ?? new PaqueteTutorVirtualPaisRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IPaqueteTutorVirtualBeneficioRepository _paqueteTutorVirtualBeneficioRepository;
        IPaqueteTutorVirtualBeneficioRepository IUnitOfWork.PaqueteTutorVirtualBeneficioRepository
        {
            get
            {
                return _paqueteTutorVirtualBeneficioRepository ?? new PaqueteTutorVirtualBeneficioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMontoPagoLogRepository _montoPagoLogRepository;
        IMontoPagoLogRepository IUnitOfWork.MontoPagoLogRepository
        {
            get
            {
                return _montoPagoLogRepository ?? new MontoPagoLogRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IMedioComunicacionRepository _medioComunicacionRepository;
        IMedioComunicacionRepository IUnitOfWork.MedioComunicacionRepository
        {
            get
            {
                return _medioComunicacionRepository ?? new MedioComunicacionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IBloqueHorarioDetalleRepository _bloqueHorarioDetalleRepository;
        IBloqueHorarioDetalleRepository IUnitOfWork.BloqueHorarioDetalleRepository
        {
            get
            {
                return _bloqueHorarioDetalleRepository ?? new BloqueHorarioDetalleRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPreferenciaComunicacionAcademicaRepository _preferenciaComunicacionAcademicaRepository;
        IPreferenciaComunicacionAcademicaRepository IUnitOfWork.PreferenciaComunicacionAcademicaRepository
        {
            get
            {
                return _preferenciaComunicacionAcademicaRepository ?? new PreferenciaComunicacionAcademicaRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private IPreferenciaComunicacionAcademicaHorarioRepository _preferenciaComunicacionAcademicaHorarioRepository;
        IPreferenciaComunicacionAcademicaHorarioRepository IUnitOfWork.PreferenciaComunicacionAcademicaHorarioRepository
        {
            get
            {
                return _preferenciaComunicacionAcademicaHorarioRepository ?? new PreferenciaComunicacionAcademicaHorarioRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IConfirmacionWebinarRepository _confirmacionWebinarRepository;
        IConfirmacionWebinarRepository IUnitOfWork.ConfirmacionWebinarRepository
        {
            get
            {
                return _confirmacionWebinarRepository ?? new ConfirmacionWebinarRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
    }
}
