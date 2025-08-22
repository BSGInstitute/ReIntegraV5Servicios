using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Builder;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using SentinelServicio;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SentinelService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/06/2022
    /// <summary>
    /// Gestión general de T_Sentinel
    /// </summary>
    public class SentinelService : ISentinelService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SentinelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSentinel, Sentinel>(MemberList.None).ReverseMap();
                    cfg.CreateMap<SentinelDTO, ComboDTO>(MemberList.None);
                    cfg.CreateMap<SentinelDatosAlumnoAgendaDTO, SentinelDatosAlumnoDetalleAgendaDTO>(MemberList.None);
                    cfg.CreateMap<SentinelDTO, Sentinel>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public Sentinel Add(Sentinel entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Sentinel>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Sentinel Update(Sentinel entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Sentinel>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.SentinelRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Sentinel> Add(List<Sentinel> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Sentinel>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Sentinel> Update(List<Sentinel> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Sentinel>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.SentinelRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Sentinel
        /// </summary>
        /// <returns> List<SentinelDTO> </returns>
        public IEnumerable<SentinelDTO> ObtenerSentinel()
        {
            try
            {
                return _unitOfWork.SentinelRepository.ObtenerSentinel();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Sentinel para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelComboDTO> </returns>
        public IEnumerable<SentinelComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SentinelRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Sentinel para mostrarse en combo.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> List<SentinelDatosAlumnoDetalleAgendaDTO> </returns>
        public SentinelDatosAlumnoDetalleAgendaDTO ObtenerDatosSentinelDetallePorIdAlumno(int idAlumno)
        {
            try
            {
                var datosSentinelAlumno = _unitOfWork.SentinelRepository.ObtenerSentinelTipoDocumentoDNIPorIdAlumno(idAlumno);
                if (datosSentinelAlumno == null)
                {
                    datosSentinelAlumno = _unitOfWork.SentinelRepository.ObtenerSentinelPorIdAlumno(idAlumno);
                }
                var datosSentinelAlumnoDetalle = _mapper.Map<SentinelDatosAlumnoDetalleAgendaDTO>(datosSentinelAlumno);
                if (datosSentinelAlumnoDetalle != null && datosSentinelAlumnoDetalle.IdSentinel != null)
                {
                    var lineaCreditoService = new SentinelSdtLincreItemService(_unitOfWork);
                    datosSentinelAlumnoDetalle.lineaCredito = lineaCreditoService.ObtenerLineaCreditoPorIdSentinel(datosSentinelAlumnoDetalle.IdSentinel.Value);
                    var lineaDeudaService = new SentinelSdtRepSbsitemService(_unitOfWork);
                    datosSentinelAlumnoDetalle.lineaDeuda = lineaDeudaService.ObtenerLineaDeudaPorIdSentinel(datosSentinelAlumnoDetalle.IdSentinel.Value);
                }
                return datosSentinelAlumnoDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Sueldo Promedio asociado a una Persona, su Cargo, Empresa e Industria.
        /// </summary>
        /// <param name="argumentos">Argumentos relacionados a la Empresa e Industria</param>
        /// <returns> SueldoPromedioDTO </returns>
        public SueldoPromedioDTO ObtenerSueldoPromedio(SueldoPromedioArgumentosDTO argumentos)
        {
            try
            {
                var servicioSueldoIndividual = new SentinelSueldoIndividualService(_unitOfWork);
                var servicioIndustriaTotal = new SentinelSueldoPorIndustriaDataTotalService(_unitOfWork);
                var servicioSueldoIndustria = new SentinelSueldoPorIndustriaService(_unitOfWork);
                var servicioIndustriaDinamico = new SentinelSueldoPorIndustriaDataDinamicoService(_unitOfWork);

                var sueldoPromedio = new SueldoPromedioDTO()
                {
                    Valor = null,
                    Descripcion = "SD"
                };

                var promedioDni = servicioSueldoIndividual.ObtenerSueldoPromedioPorDni(argumentos.Dni);
                var promedioCargoIndustria = servicioIndustriaTotal.ObtenerSueldoPorCargoIndustria(argumentos.IdCargo!.Value, argumentos.IdIndustria!.Value)?.Valor;
                var promedioDinamicoTamanio = argumentos.IdTamanioEmpresa != null && argumentos.IdTamanioEmpresa != 0 ?
                    servicioIndustriaDinamico.ObtenerValorSueldoIndustria(argumentos.IdCargo!.Value, argumentos.IdIndustria!.Value, argumentos.IdTamanioEmpresa.Value).Valor :
                    null;

                if (promedioDni.Valor != null)
                {
                    sueldoPromedio.Valor = (int?)promedioDni.Valor;
                    sueldoPromedio.Descripcion = "SD";
                    return sueldoPromedio;
                }

                if (argumentos.IdIndustria == 1)
                {
                    if (promedioCargoIndustria != null)
                    {
                        sueldoPromedio.Valor = promedioCargoIndustria;
                        sueldoPromedio.Descripcion = "PM";
                    }
                    return sueldoPromedio;
                }

                var tipoSueldo = servicioSueldoIndustria.ObtenerTipoSueldoIndustria(argumentos.IdCargo!.Value, argumentos.IdIndustria!.Value).Valor;

                if (tipoSueldo != null)
                {
                    switch (tipoSueldo)
                    {
                        case 1: // Solo Mercado
                            if (promedioCargoIndustria != null)
                            {
                                sueldoPromedio.Valor = promedioCargoIndustria;
                                sueldoPromedio.Descripcion = "SM";
                            }
                            break;
                        case 2: // Solo Industria
                            if (promedioCargoIndustria != null)
                            {
                                sueldoPromedio.Valor = promedioCargoIndustria;
                                sueldoPromedio.Descripcion = "SI";
                            }
                            break;
                        case 3: // Mercado por Categoria
                            if (argumentos.IdEmpresa == null)
                            {
                                if (promedioCargoIndustria != null)
                                {
                                    sueldoPromedio.Valor = promedioCargoIndustria;
                                    sueldoPromedio.Descripcion = "PM";
                                }
                            }
                            else
                            {
                                if (argumentos.IdTamanioEmpresa != null)
                                {
                                    if (promedioDinamicoTamanio != null)
                                    {
                                        sueldoPromedio.Valor = promedioDinamicoTamanio;
                                        sueldoPromedio.Descripcion = "MC";
                                    }
                                }
                                else
                                {
                                    if (promedioCargoIndustria != null)
                                    {
                                        sueldoPromedio.Valor = promedioCargoIndustria;
                                        sueldoPromedio.Descripcion = "PM";
                                    }
                                }
                            }
                            break;
                        case 4: // Industria por Categoria
                            if (argumentos.IdEmpresa == null)
                            {
                                if (promedioDinamicoTamanio != null)
                                {
                                    sueldoPromedio.Valor = promedioDinamicoTamanio;
                                    sueldoPromedio.Descripcion = "PI";
                                }
                            }
                            else
                            {
                                if (argumentos.IdTamanioEmpresa != null)
                                {
                                    sueldoPromedio.Valor = promedioDinamicoTamanio;
                                    sueldoPromedio.Descripcion = "IC";
                                }
                                else
                                {
                                    sueldoPromedio.Valor = promedioDinamicoTamanio;
                                    sueldoPromedio.Descripcion = "PI";
                                }
                            }
                            break;
                    }
                }
                else
                {
                    if (promedioCargoIndustria != null)
                    {
                        sueldoPromedio.Valor = promedioCargoIndustria;
                        sueldoPromedio.Descripcion = "PM";
                    }
                }

                return sueldoPromedio;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el identificador de sentinel relacionado al DNI recibido.
        /// </summary>
        /// <param name="dni">DNI asociado a registro Sentinel</param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerIdSentinelPorDni(string dni)
        {
            try
            {
                return _unitOfWork.SentinelRepository.ObtenerIdSentinelPorDni(dni);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_Sentinel relacionado al DNI recibido.
        /// </summary>
        /// <param name="dni">DNI asociado a registro Sentinel</param>
        /// <returns> SentinelDTO </returns>
        public SentinelDTO ObtenerSentinelPorDni(string dni)
        {
            try
            {
                return _unitOfWork.SentinelRepository.ObtenerSentinelPorDni(dni);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 24/09/2022
        /// Version: 1.0
        /// <summary>
        /// Consulta al servicio de Sentinel por DNI  del Alumno y guarda la informacion retornada
        /// asociada al Id del Alumno en Integra
        /// </summary>
        /// <param name="dni">dni de alumno</param>
        /// <param name="usuario">usuario que genera la consulta</param>
        /// <returns>ActualizarSentinelResultadoDTO</returns>
        public ActualizarSentinelResultadoDTO ActualizarSentinelAlumno(string dni, string usuario)
        {
            try
            {
                WS_BSGrupoSoapPortClient client = new WS_BSGrupoSoapPortClient();

                SDT_IC_EstandarSDT_IC_EstandarItem[] sdt_bsgrupo_estandar;
                SDT_IC_RepSBSSDT_IC_RepSBSItem[] sdt_bsgrupo_repsbs;
                SDT_IC_LinCreItem[] sdt_bsgrupo_lincre;
                SDT_IC_ResVenSDT_IC_ResVenItem[] sdt_bsgrupo_resven;
                SDT_IC_InfGen sdt_bsgrupo_infgen;
                SDT_IC_RepLegSDT_IC_RepLegItem[] sdt_bsgrupo_repleg;
                SDT_IC_PosHisSDT_IC_PosHisItem[] sdt_bsgrupo_poshis;

                //Consultamos al servicio de Sentinel//Antigua Contraseña:Empres@grup09
                SentinelCredencialDTO sentinelCredencialDTO = _unitOfWork.SentinelRepository.ObtenerCredencial();
                client.Execute(sentinelCredencialDTO.DNI, sentinelCredencialDTO.Clave, sentinelCredencialDTO.Servicio.Value, sentinelCredencialDTO.TipoDocumento, dni, out sdt_bsgrupo_estandar, out sdt_bsgrupo_repsbs,
               out sdt_bsgrupo_lincre, out sdt_bsgrupo_resven, out sdt_bsgrupo_infgen, out sdt_bsgrupo_repleg, out sdt_bsgrupo_poshis);

                var sdt_bsgrupo_estandar_dtos = BuilderOrquestaSentinel_SDT_BSGrupo_EstandarItemDTO.builderListEntityDTO(sdt_bsgrupo_estandar.ToList());
                var sdt_bsgrupo_repsbs_dtos = BuilderOrquestaSentinel_SDT_BSGrupo_RepSBSItemDTO.builderListEntityDTO(sdt_bsgrupo_repsbs.ToList());
                var sdt_bsgrupo_lincre_dtos = BuilderOrquestaSentinel_SDT_BSGrupo_LinCreItemDTO.builderListEntityDTO(sdt_bsgrupo_lincre.ToList());
                var sdt_bsgrupo_resven_dtos = BuilderOrquestaSentinel_SDT_BSGrupo_ResVenItemDTO.builderListEntityDTO(sdt_bsgrupo_resven.ToList());
                var sdt_bsgrupo_infgen_dto = BuilderOrquestaSentinel_SDT_BSGrupo_InfGenDTO.builderEntityDTO(sdt_bsgrupo_infgen);
                var sdt_bsgrupo_repleg_dtos = BuilderOrquestaSentinel_SDT_BSGrupo_RepLegItemDTO.builderListEntityDTO(sdt_bsgrupo_repleg.ToList());
                var sdt_bsgrupo_poshis_dtos = BuilderOrquestaSentinel_SDT_BSGrupo_PosHisItemDTO.builderListEntityDTO(sdt_bsgrupo_poshis.ToList());

                var dniRuc = new List<SentinelSdtEstandarItemDTO>();
                var deuda = new List<SentinelSdtRepSbsitemDTO>();
                var lineaCredito = new List<SentinelSdtLincreItemDTO>();
                var datosVencidas = new List<SentinelSdtResVenItemDTO>();
                var datosGenerales = new SentinelSdtInfGenDTO();
                var cargo = new List<SentinelRepLegItemDTO>();
                var posicionHistoria = new List<SentinelSdtPoshisItemDTO>();


                foreach (var entity in sdt_bsgrupo_estandar_dtos)
                {
                    SentinelSdtEstandarItemDTO rpta = new SentinelSdtEstandarItemDTO();
                    rpta.TipoDocumento = entity.TipoDocumento;
                    rpta.Documento = entity.Documento;
                    rpta.RazonSocial = entity.RazonSocial;
                    rpta.FechaProceso = entity.FechaProceso;
                    rpta.Semaforos = entity.Semaforos;
                    rpta.Score = entity.Score;
                    rpta.NroBancos = entity.NroBancos;
                    rpta.DeudaTotal = entity.DeudaTotal;
                    rpta.VencidoBanco = entity.VencidoBanco;
                    rpta.Calificativo = entity.Calificativo;
                    rpta.Veces24m = entity.Veces24m;
                    rpta.SemanaActual = entity.SemanaActual;
                    rpta.SemanaPrevio = entity.SemanaPrevio;
                    rpta.SemanaPeorMejor = entity.SemanaPeorMejor;
                    rpta.Documento2 = entity.Documento2;
                    rpta.EstadoDomicilio = entity.EstadoDomicilio;
                    rpta.CondicionDomicilio = entity.CondicionDomicilio;
                    rpta.DeudaTributaria = entity.DeudaTributaria;
                    rpta.DeudaLaboral = entity.DeudaLaboral;
                    rpta.DeudaImpagable = entity.DeudaImpagable;
                    rpta.DeudaProtestos = entity.DeudaProtestos;
                    rpta.DeudaSbs = entity.DeudaSbs;
                    rpta.CuentasTarjetas = entity.CuentasTarjetas;
                    rpta.ReporteNegativo = entity.ReporteNegativo;
                    rpta.TipoActividad = entity.TipoActividad;
                    rpta.FechaInicioActividad = entity.FechaInicioActividad;
                    rpta.DeudaDirecta = entity.DeudaDirecta;
                    rpta.DeudaIndirecta = entity.DeudaIndirecta;
                    rpta.DeudaCastigada = entity.DeudaCastigada;
                    rpta.LineaCreditoNoUtilizada = entity.LineaCreditoNoUtilizada;
                    rpta.TotalRiesgo = entity.TotalRiesgo;
                    rpta.PeorCalificacion = entity.PeorCalificacion;
                    rpta.PorcentajeCalificacionNormal = entity.PorcentajeCalificacionNormal;
                    rpta.FechaCreacion = DateTime.Now;
                    rpta.FechaModificacion = DateTime.Now;
                    rpta.UsuarioCreacion = usuario;
                    rpta.UsuarioModificacion = usuario;

                    dniRuc.Add(rpta);
                }

                foreach (var entity in sdt_bsgrupo_repsbs_dtos)
                {
                    SentinelSdtRepSbsitemDTO rpta = new SentinelSdtRepSbsitemDTO();
                    rpta.TipoDoc = entity.TipoDoc;
                    rpta.NroDoc = entity.NroDoc;
                    rpta.NombreRazonSocial = entity.NombreRazonSocial;
                    rpta.Calificacion = entity.Calificacion;
                    rpta.MontoDeuda = entity.MontoDeuda;
                    rpta.DiasVencidos = entity.DiasVencidos;
                    rpta.FechaReporte = entity.FechaReporte;
                    rpta.FechaCreacion = DateTime.Now;
                    rpta.FechaModificacion = DateTime.Now;
                    rpta.UsuarioCreacion = usuario;
                    rpta.UsuarioModificacion = usuario;

                    deuda.Add(rpta);
                }

                foreach (var entity in sdt_bsgrupo_lincre_dtos)
                {
                    SentinelSdtLincreItemDTO rpta = new SentinelSdtLincreItemDTO();
                    rpta.TipoDocumento = entity.TipoDocumento;
                    rpta.NumeroDocumento = entity.NumeroDocumento;
                    rpta.TipoCuenta = entity.TipoCuenta;
                    rpta.LineaCredito = entity.LineaCredito;
                    rpta.LineaCreditoNoUtil = entity.LineaCreditoNoUtil;
                    rpta.LineaUtil = entity.LineaUtil;
                    rpta.CnsEntNomRazLn = entity.CnsEntNomRazLn;
                    rpta.FechaCreacion = DateTime.Now;
                    rpta.FechaModificacion = DateTime.Now;
                    rpta.UsuarioCreacion = usuario;
                    rpta.UsuarioModificacion = usuario;

                    lineaCredito.Add(rpta);
                }

                foreach (var entity in sdt_bsgrupo_resven_dtos)
                {
                    SentinelSdtResVenItemDTO rpta = new SentinelSdtResVenItemDTO();
                    rpta.TipoDocumento = entity.TipoDocumento;
                    rpta.NroDocumento = entity.NroDocumento;
                    rpta.CantidadDocs = entity.CantidadDocs;
                    rpta.Fuente = entity.Fuente;
                    rpta.Entidad = entity.Entidad;
                    rpta.Monto = entity.Monto;
                    rpta.Cantidad = entity.Cantidad;
                    rpta.DiasVencidos = entity.DiasVencidos;
                    rpta.FechaCreacion = DateTime.Now;
                    rpta.FechaModificacion = DateTime.Now;
                    rpta.UsuarioCreacion = usuario;
                    rpta.UsuarioModificacion = usuario;

                    datosVencidas.Add(rpta);
                }

                if (sdt_bsgrupo_infgen_dto != null)
                {
                    datosGenerales = new SentinelSdtInfGenDTO();
                    datosGenerales.Dni = sdt_bsgrupo_infgen_dto.Dni;
                    datosGenerales.FechaNacimiento = sdt_bsgrupo_infgen_dto.FechaNacimiento;
                    datosGenerales.Sexo = sdt_bsgrupo_infgen_dto.Sexo;
                    datosGenerales.Digito = sdt_bsgrupo_infgen_dto.Digito;
                    datosGenerales.DigitoAnterior = sdt_bsgrupo_infgen_dto.DigitoAnterior;
                    datosGenerales.Ruc = sdt_bsgrupo_infgen_dto.Ruc;
                    datosGenerales.RazonSocial = sdt_bsgrupo_infgen_dto.RazonSocial;
                    datosGenerales.NombreComercial = sdt_bsgrupo_infgen_dto.NombreComercial;
                    datosGenerales.FechaBaja = sdt_bsgrupo_infgen_dto.FechaBaja;
                    //rpta.FechaBaja = entity.FechaBaja == "" ? "" : Convert.ToDateTime(entity.FechaBaja);
                    datosGenerales.TipoContribuyente = sdt_bsgrupo_infgen_dto.TipoContribuyente;
                    datosGenerales.CodigoTipoContribuyente = sdt_bsgrupo_infgen_dto.CodigoTipoContribuyente;
                    datosGenerales.EstadoContribuyente = sdt_bsgrupo_infgen_dto.EstadoContribuyente;
                    datosGenerales.CodigoEstadoContribuyente = sdt_bsgrupo_infgen_dto.CodigoEstadoContribuyente;
                    datosGenerales.CondicionContribuyente = sdt_bsgrupo_infgen_dto.CondicionContribuyente;
                    datosGenerales.CodigoCondicionContribuyente = sdt_bsgrupo_infgen_dto.CodigoCondicionContribuyente;
                    datosGenerales.ActividadEconomica = sdt_bsgrupo_infgen_dto.ActividadEconomica;
                    datosGenerales.Ciiu = sdt_bsgrupo_infgen_dto.Ciiu;
                    datosGenerales.ActividadEconomica2 = sdt_bsgrupo_infgen_dto.ActividadEconomica2;
                    datosGenerales.Ciiu2 = sdt_bsgrupo_infgen_dto.Ciiu2;
                    datosGenerales.ActividadEconomica3 = sdt_bsgrupo_infgen_dto.ActividadEconomica3;
                    datosGenerales.Ciiu3 = sdt_bsgrupo_infgen_dto.Ciiu3;
                    datosGenerales.FechaActividad = sdt_bsgrupo_infgen_dto.FechaActividad;
                    datosGenerales.Direccion = sdt_bsgrupo_infgen_dto.Direccion;
                    datosGenerales.Referencia = sdt_bsgrupo_infgen_dto.Referencia;
                    datosGenerales.Departamento = sdt_bsgrupo_infgen_dto.Departamento;
                    datosGenerales.Provincia = sdt_bsgrupo_infgen_dto.Provincia;
                    datosGenerales.Distrito = sdt_bsgrupo_infgen_dto.Distrito;
                    datosGenerales.Ubigeo = sdt_bsgrupo_infgen_dto.Ubigeo;
                    datosGenerales.FechaConstitucion = sdt_bsgrupo_infgen_dto.FechaConstitucion;
                    //rpta.FechaConstitucion = Convert.ToDateTime(entity.FConstitucion);
                    datosGenerales.ActvidadComercioExterior = sdt_bsgrupo_infgen_dto.ActvidadComercioExterior;
                    datosGenerales.CodigoActividadComerExt = sdt_bsgrupo_infgen_dto.CodigoActividadComerExt;
                    datosGenerales.CodigoDependencia = sdt_bsgrupo_infgen_dto.CodigoDependencia;
                    datosGenerales.Dependencia = sdt_bsgrupo_infgen_dto.Dependencia;
                    datosGenerales.Folio = sdt_bsgrupo_infgen_dto.Folio;
                    datosGenerales.Asiento = sdt_bsgrupo_infgen_dto.Asiento;
                    datosGenerales.Tomo = sdt_bsgrupo_infgen_dto.Tomo;
                    datosGenerales.PartidaReg = sdt_bsgrupo_infgen_dto.PartidaReg;
                    datosGenerales.Patron = sdt_bsgrupo_infgen_dto.Patron;
                    datosGenerales.FechaCreacion = DateTime.Now;
                    datosGenerales.FechaModificacion = DateTime.Now;
                    datosGenerales.UsuarioCreacion = usuario;
                    datosGenerales.UsuarioModificacion = usuario;
                }
                else
                {
                    datosGenerales = null;
                }

                foreach (var entity in sdt_bsgrupo_repleg_dtos)
                {
                    SentinelRepLegItemDTO rpta = new SentinelRepLegItemDTO();
                    rpta.TipoDocumento = entity.TipoDocumento;
                    rpta.NumeroDocumento = entity.NumeroDocumento;
                    rpta.Nombres = entity.Nombres;
                    rpta.ApellidoPaterno = entity.ApellidoPaterno;
                    rpta.ApellidoMaterno = entity.ApellidoMaterno;
                    rpta.RazonSocial = entity.RazonSocial;
                    rpta.Cargo = entity.Cargo;
                    rpta.SemaforoActual = entity.SemaforoActual;
                    rpta.FechaCreacion = DateTime.Now;
                    rpta.FechaModificacion = DateTime.Now;
                    rpta.UsuarioCreacion = usuario;
                    rpta.UsuarioModificacion = usuario;

                    cargo.Add(rpta);
                }

                foreach (var entity in sdt_bsgrupo_poshis_dtos)
                {
                    SentinelSdtPoshisItemDTO rpta = new SentinelSdtPoshisItemDTO();
                    rpta.TipoDocumento = entity.TipoDocumento;
                    rpta.NumeroDocumento = entity.NumeroDocumento;
                    rpta.FechaProceso = entity.FechaProceso;
                    rpta.SemanaActual = entity.SemanaActual;
                    rpta.DescripcionSemaforo = entity.DescripcionSemaforo;
                    rpta.Score = entity.Score;
                    rpta.CodigoVariacion = entity.CodigoVariacion;
                    rpta.DescripcionVariacion = entity.DescripcionVariacion;
                    rpta.NumeroEntidades = entity.NumeroEntidades;
                    rpta.DeudaTotal = entity.DeudaTotal;
                    rpta.PorcentajeCalificacion = entity.PorcentajeCalificacion;
                    rpta.PeorCalificacion = entity.PeorCalificacion;
                    rpta.PeroCalificacionDescripcion = entity.PeroCalificacionDescripcion;
                    rpta.MontoSbs = entity.MontoSbs;
                    rpta.ProgresoRegistro = entity.ProgresoRegistro;
                    rpta.DocImpuesto = entity.DocImpuesto;
                    rpta.DeudaTributaria = entity.DeudaTributaria;
                    rpta.Afp = entity.Afp;
                    rpta.TarjetaCredito = entity.TarjetaCredito;
                    rpta.CuentaCorriente = entity.CuentaCorriente;
                    rpta.ReporteNegativo = entity.ReporteNegativo;
                    rpta.DeudaDirecta = entity.DeudaDirecta;
                    rpta.DeudaIndirecta = entity.DeudaIndirecta;
                    rpta.LineaCreditoNoUtilizada = entity.LineaCreditoNoUtilizada;
                    rpta.DeudaCastigada = entity.DeudaCastigada;
                    rpta.FechaCreacion = DateTime.Now;
                    rpta.FechaModificacion = DateTime.Now;
                    rpta.UsuarioCreacion = usuario;
                    rpta.UsuarioModificacion = usuario;

                    posicionHistoria.Add(rpta);
                }
                var resultadoDTO = new ActualizarSentinelResultadoDTO();
                resultadoDTO.DniRuc = dniRuc;
                resultadoDTO.Deuda = deuda;
                resultadoDTO.LineaCredito = lineaCredito;
                resultadoDTO.DatosVencidas = datosVencidas;
                resultadoDTO.DatosGenerales = datosGenerales;
                resultadoDTO.Cargo = cargo;
                resultadoDTO.PosicionHistoria = posicionHistoria;

                return resultadoDTO;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Mapea desde SentinelDTO a Sentinel
        /// </summary>
        /// <param name="dto">SentinelDTO</param>
        /// <returns> Sentinel </returns>
        public Sentinel MapeoEntidadDesdeDTO(SentinelDTO dto)
        {
            try
            {
                var entidad = _mapper.Map<Sentinel>(dto);
                if (entidad != null) entidad.Estado = true;
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los Datos Sentinel asociado a un Alumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> SentinelDatosContactoDTO </returns>
        public SentinelDatosContactoDTO ObtenerDatosAlumnoSentinel(int idAlumno)
        {
            try
            {
                return _unitOfWork.SentinelRepository.ObtenerDatosAlumnoSentinel(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la cabecera para la agenda.
        /// </summary>
        /// <param name="idSentinel">Id de sentinel</param>
        /// <returns>SentinelDatosCabeceraDTO</returns>
        public SentinelDatosCabeceraDTO ObtenerCabeceraSentinel(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelRepository.ObtenerCabeceraSentinel(idSentinel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 20/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Sentinel para mostrar los detalles.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelDetalleDTO> </returns>
        public SentinelDetalleDTO ObtenerDetalleSentinel(int idSentinel)
        {
            try
            {
                var sentinelSdtEstandarItemService = new SentinelSdtEstandarItemService(_unitOfWork);
                var sentinelSdtInfGenService = new SentinelSdtInfGenService(_unitOfWork);
                var sentinelSdtRepSbsitemService = new SentinelSdtRepSbsitemService(_unitOfWork);
                var sentinelSdtLincreItemService = new SentinelSdtLincreItemService(_unitOfWork);
                var sentinelSdtPoshisItemService = new SentinelSdtPoshisItemService(_unitOfWork);
                var sentinelSdtResVenItemService = new SentinelSdtResVenItemService(_unitOfWork);

                var sentinelDetalle = new SentinelDetalleDTO();

                sentinelDetalle.DniRuc = sentinelSdtEstandarItemService.ObtenerDniRucSentinel(idSentinel);
                sentinelDetalle.DatosGenerales = sentinelSdtInfGenService.ObtenerDatosGenerales(idSentinel);
                sentinelDetalle.DatosVencidas = sentinelSdtResVenItemService.ObtenerDatosVencidos(idSentinel);
                sentinelDetalle.LineaCredito = sentinelSdtLincreItemService.ObtenerLineaDeCredito(idSentinel);
                sentinelDetalle.Deuda = sentinelSdtRepSbsitemService.ObtenerLineaDeudaSentinel(idSentinel);
                sentinelDetalle.PosicionHistoria = sentinelSdtPoshisItemService.ObtenerPosicionHistoria(idSentinel);

                return sentinelDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/11/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza sentinel del Alumno por medio de dni, idContacto y usuario
        /// </summary>
        /// <param name="dni"></param>
        /// <param name="idContacto"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public SentinelRespuestaDTO ActualizarSentinelAlumno(string dni, int idContacto, string usuario)
        {
            try
            {
                var respuestaTotal = new SentinelRespuestaDTO();
                var servicioSentinel = new SentinelService(_unitOfWork);
                var servicioEstandarItem = new SentinelSdtEstandarItemService(_unitOfWork);
                var servicioInfGen = new SentinelSdtInfGenService(_unitOfWork);
                var servicioLincreItem = new SentinelSdtLincreItemService(_unitOfWork);
                var servicioPoshis = new SentinelSdtPoshisItemService(_unitOfWork);
                var servicioRepLeg = new SentinelRepLegItemService(_unitOfWork);
                var servicioRepSBS = new SentinelSdtRepSbsitemService(_unitOfWork);
                var servicioResVen = new SentinelSdtResVenItemService(_unitOfWork);
                var servicioAlumno = new AlumnoService(_unitOfWork);

                var resultadoSentinel = servicioSentinel.ObtenerIdSentinelPorDni(dni);
                var idSentinel = 0;
                if (resultadoSentinel != null && resultadoSentinel.Valor != null)
                {
                    idSentinel = resultadoSentinel.Valor.Value;
                }
                var alumno = servicioAlumno.ObtenerPorId(idContacto);
                SentinelDTO sentinel = new SentinelDTO();

                bool respuesta = false;
                bool estado = true;
                if (idSentinel != null && idSentinel > 0)
                {
                    sentinel = servicioSentinel.ObtenerSentinelPorDni(dni);
                    var sentinelSdtEstandarItem = servicioEstandarItem.ObtenerPorIdSentinel(idSentinel);
                    var sentinelSdtInfGen = servicioInfGen.ObtenerPorIdSentinel(idSentinel);
                    var sentinelSdtLincreItem = servicioLincreItem.ObtenerPorIdSentinel(idSentinel);
                    var sentinelSdtPoshisItem = servicioPoshis.ObtenerPorIdSentinel(idSentinel);
                    var sentinelRepLegItem = servicioRepLeg.ObtenerPorIdSentinel(idSentinel);
                    var sentinelSdtRepSBSItem = servicioRepSBS.ObtenerPorIdSentinel(idSentinel);
                    var sentinelSdtResVenItem = servicioResVen.ObtenerPorIdSentinel(idSentinel);

                    if (sentinelSdtEstandarItem != null && sentinelSdtEstandarItem.Count() > 0) { servicioEstandarItem.Delete(sentinelSdtEstandarItem.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelSdtInfGen != null && sentinelSdtInfGen.Count() > 0) { servicioInfGen.Delete(sentinelSdtInfGen.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelSdtLincreItem != null && sentinelSdtLincreItem.Count() > 0) { servicioLincreItem.Delete(sentinelSdtLincreItem.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelSdtPoshisItem != null && sentinelSdtPoshisItem.Count() > 0) { servicioPoshis.Delete(sentinelSdtPoshisItem.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelRepLegItem != null && sentinelRepLegItem.Count() > 0) { servicioRepLeg.Delete(sentinelRepLegItem.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelSdtRepSBSItem != null && sentinelSdtRepSBSItem.Count() > 0) { servicioRepSBS.Delete(sentinelSdtRepSBSItem.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelSdtResVenItem != null && sentinelSdtResVenItem.Count() > 0) { servicioResVen.Delete(sentinelSdtResVenItem.Select(p => p.Id).ToList(), usuario); }
                    /* Estado = 1 */
                    var resultadoActualizar = servicioSentinel.ActualizarSentinelAlumno(dni, usuario);
                    if (resultadoActualizar.DatosGenerales.Dni == null || resultadoActualizar.DatosGenerales.Dni == "")
                    {
                        estado = false;
                    }
                    if (estado)
                    {
                        alumno.Dni = dni;
                        alumno = servicioAlumno.ValidarEstadoContactoWhatsAppTemporalAlterno(alumno);
                        if (resultadoActualizar.DatosGenerales != null)
                        {
                            alumno.FechaNacimiento = resultadoActualizar.DatosGenerales.FechaNacimiento;
                        }
                        var entidadAlumno = alumno;
                        //var entidadAlumno = servicioAlumno.MapeoEntidadDesdeDTO(alumno);
                        alumno.IdEmpresa = (entidadAlumno.IdEmpresa == 0 || entidadAlumno.IdEmpresa == -1) ? null : entidadAlumno.IdEmpresa;
                        servicioAlumno.Update(entidadAlumno);

                        var entidadSentinel = servicioSentinel.MapeoEntidadDesdeDTO(sentinel);
                        entidadSentinel.SentinelRepLegItems = servicioRepLeg.MapeoEntidadesDesdeListaDTO(resultadoActualizar.Cargo);
                        entidadSentinel.SentinelSdtEstandarItems = servicioEstandarItem.MapeoEntidadesDesdeListaDTO(resultadoActualizar.DniRuc);
                        entidadSentinel.SentinelSdtInfGens = servicioInfGen.MapeoEntidadesDesdeListaDTO(new SentinelSdtInfGenDTO[] { resultadoActualizar.DatosGenerales }.ToList());
                        entidadSentinel.SentinelSdtLincreItems = servicioLincreItem.MapeoEntidadesDesdeListaDTO(resultadoActualizar.LineaCredito);
                        entidadSentinel.SentinelSdtPoshisItems = servicioPoshis.MapeoEntidadesDesdeListaDTO(resultadoActualizar.PosicionHistoria);
                        entidadSentinel.SentinelSdtRepSbsitems = servicioRepSBS.MapeoEntidadesDesdeListaDTO(resultadoActualizar.Deuda);
                        entidadSentinel.SentinelSdtResVenItems = servicioResVen.MapeoEntidadesDesdeListaDTO(resultadoActualizar.DatosVencidas);

                        entidadSentinel.Dni = dni;
                        entidadSentinel.UsuarioModificacion = usuario;
                        entidadSentinel.FechaModificacion = DateTime.Now;
                        servicioSentinel.Update(entidadSentinel);
                        respuesta = true;
                    }
                }
                else
                {
                    sentinel.Dni = dni;
                    sentinel.Estado = true;
                    sentinel.UsuarioCreacion = usuario;
                    sentinel.UsuarioModificacion = usuario;
                    sentinel.FechaCreacion = DateTime.Now;
                    sentinel.FechaModificacion = DateTime.Now;
                    var resultadoActualizar = servicioSentinel.ActualizarSentinelAlumno(dni, usuario);
                    if (resultadoActualizar.DatosGenerales.Dni == "")
                    {
                        estado = false;
                        //return BadRequest("El numero de DNI a consultar es invalido o no esta registrado en sentinel");
                    }
                    if (estado)
                    {
                        alumno = servicioAlumno.ValidarEstadoContactoWhatsAppTemporalAlterno(alumno);
                        if (resultadoActualizar.DatosGenerales != null)
                        {
                            alumno.FechaNacimiento = resultadoActualizar.DatosGenerales.FechaNacimiento;
                            alumno.UsuarioModificacion = usuario;
                            alumno.FechaModificacion = DateTime.Now;
                        }
                        var entidadAlumno = alumno;
                        entidadAlumno.Dni = dni;
                        servicioAlumno.Update(entidadAlumno);
                        var entidadSentinel = servicioSentinel.MapeoEntidadDesdeDTO(sentinel);
                        entidadSentinel.SentinelRepLegItems = servicioRepLeg.MapeoEntidadesDesdeListaDTO(resultadoActualizar.Cargo);
                        entidadSentinel.SentinelSdtEstandarItems = servicioEstandarItem.MapeoEntidadesDesdeListaDTO(resultadoActualizar.DniRuc);
                        entidadSentinel.SentinelSdtInfGens = servicioInfGen.MapeoEntidadesDesdeListaDTO(new SentinelSdtInfGenDTO[] { resultadoActualizar.DatosGenerales }.ToList());
                        entidadSentinel.SentinelSdtLincreItems = servicioLincreItem.MapeoEntidadesDesdeListaDTO(resultadoActualizar.LineaCredito);
                        entidadSentinel.SentinelSdtPoshisItems = servicioPoshis.MapeoEntidadesDesdeListaDTO(resultadoActualizar.PosicionHistoria);
                        entidadSentinel.SentinelSdtRepSbsitems = servicioRepSBS.MapeoEntidadesDesdeListaDTO(resultadoActualizar.Deuda);
                        entidadSentinel.SentinelSdtResVenItems = servicioResVen.MapeoEntidadesDesdeListaDTO(resultadoActualizar.DatosVencidas);
                        servicioSentinel.Add(entidadSentinel);
                        respuesta = true;
                    }
                }
                respuestaTotal.Respuesta = respuesta;
                respuestaTotal.IdSentinel = sentinel.Id;
                respuestaTotal.Estado = estado;
                return respuestaTotal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtiene codigo de matricula
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        /// <summary>
        public string ObtenerCodigoMatricula(int idOportunidad)
        {
            try
            {
                var codigoMat = _unitOfWork.MatriculaCabeceraRepository.ObtenerMatriculaPorOportunidadOperaciones(idOportunidad);
                if (codigoMat != null)
                {
                    return codigoMat.CodigoMatricula;
                }
                else
                {
                    return "No tiene Codigo Matricula";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Obtiene El Sueldo Promedio de un Contacto
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="dni"></param>
        /// <param name="idCargo"></param>
        /// <param name="idIndustria"></param>
        /// <returns></returns>
        public SueldosDescripcionDTO ObtenerPromedioSueldo(int? idEmpresa, string dni, int? idCargo, int? idIndustria)
        {
            SueldosDescripcionDTO promedio = new SueldosDescripcionDTO();

            promedio.valor = null;
            promedio.descripcion = "SD";
            //// si tiene DNI 
            var sentinelDNI = _unitOfWork.SentinelSueldoIndividualRepository.ObtenerSueldoPromedioPorDni(dni);
            if (sentinelDNI.Valor != null)
            {//si existe en los sueldos individuales
                int? pro = (int?)sentinelDNI.Valor;

                promedio.valor = pro;
                promedio.descripcion = "SP";
                return promedio;
            }
            /////////////////////////////// si no tiene DNI se aplica la logica//////////////////////////////////////////////////////////////////////////////
            if (idIndustria == 1)
            {
                var proProm = _unitOfWork.SentinelSueldoPorIndustriaDataTotalRepository.ObtenerSueldoPorCargoIndustria(idCargo.Value, idIndustria.Value);
                if (proProm.Valor != null)
                {
                    promedio.valor = proProm.Valor;
                    promedio.descripcion = "PM";
                }
                return promedio;//PM
            }

            var entidad = _unitOfWork.SentinelSueldoPorIndustriaRepository.ObtenerTipoSueldoIndustria(idCargo.Value, idIndustria.Value);

            if (entidad.Valor != null)
            {
                switch (entidad.Valor)
                {
                    case 1://solo mercado
                        var promMercado = _unitOfWork.SentinelSueldoPorIndustriaDataTotalRepository.ObtenerSueldoPorCargoIndustria(idCargo.Value, idIndustria.Value);
                        if (promMercado.Valor != null)
                        {
                            //promedio = promMercado.valor;//SM
                            promedio.valor = promMercado.Valor;
                            promedio.descripcion = "SM";

                            //return promedio;
                        }
                        break;
                    case 2://solo industria

                        var solomercado = _unitOfWork.SentinelSueldoPorIndustriaDataTotalRepository.ObtenerSueldoPorCargoIndustria(idCargo.Value, idIndustria.Value);
                        if (solomercado.Valor != null)
                        {
                            // promedio = solomercado.valor;//SI
                            promedio.valor = solomercado.Valor;
                            promedio.descripcion = "SI";
                        }
                        break;
                    case 3:// MERCADO por categoria
                        if (idEmpresa == null)
                        {
                            //si no tiene empresa se imprime promedio del mercado
                            var promIndustria = _unitOfWork.SentinelSueldoPorIndustriaDataTotalRepository.ObtenerSueldoPorCargoIndustria(idCargo.Value, idIndustria.Value);
                            if (promIndustria.Valor != null)
                            {
                                // promedio = promIndustria.valor;//PM
                                promedio.valor = promIndustria.Valor;
                                promedio.descripcion = "PM";
                            }
                        }
                        else
                        {//tiene empresa imprime el mercado de acuerdo a su categoria
                            var empresa = _unitOfWork.EmpresaRepository.ObtenerIdTamanioEmpresaPorIdEmpresa(idEmpresa.Value);
                            if (empresa != null && empresa.Valor != null)
                            {
                                var industria = _unitOfWork.SentinelSueldoPorIndustriaDataDinamicoRepository.ObtenerValorSueldoIndustria(idCargo.Value, idIndustria.Value, empresa.Valor == null ? 0 : empresa.Valor.Value);
                                if (industria.Valor != null)
                                {
                                    // promedio = industria.valor; //MC
                                    promedio.valor = industria.Valor;
                                    promedio.descripcion = "MC";
                                }
                            }
                            else
                            { //si no tiene categoria ,debe imprimir promedio de mercado
                                var promIndustria = _unitOfWork.SentinelSueldoPorIndustriaDataTotalRepository.ObtenerSueldoPorCargoIndustria(idCargo.Value, idIndustria.Value);
                                if (promIndustria.Valor != null)
                                {
                                    // promedio = promIndustria.valor;//PM
                                    promedio.valor = promIndustria.Valor;
                                    promedio.descripcion = "PM";
                                }
                            }
                        }
                        break;

                    case 4://INDUSTRIA por categoria
                        if (idEmpresa == null)
                        {//si no tiene empresa se imprime promedio de la industria
                            var industria = _unitOfWork.SentinelSueldoPorIndustriaDataDinamicoRepository.ObtenerValorSueldoIndustria(idCargo.Value, idIndustria.Value, idIndustria.Value);
                            if (industria.Valor != null)
                            {
                                //promedio = industria.valor;//PI
                                promedio.valor = industria.Valor;
                                promedio.descripcion = "PI";
                            }
                        }
                        else
                        {//tiene empresa imprime de acuerdo a su categoria de la industria
                            var empresa = _unitOfWork.EmpresaRepository.ObtenerIdTamanioEmpresaPorIdEmpresa(idEmpresa.Value);
                            if (empresa != null && empresa.Valor != null)
                            {
                                var industria = _unitOfWork.SentinelSueldoPorIndustriaDataDinamicoRepository.ObtenerValorSueldoIndustria(idCargo.Value, idIndustria.Value, empresa.Valor == null ? 0 : empresa.Valor.Value);
                                if (industria.Valor != null)
                                {
                                    //promedio = industria.valor; //IC
                                    promedio.valor = industria.Valor;
                                    promedio.descripcion = "IC";
                                }
                            }
                            else
                            { //debe imprimir promedio de la industria
                                var industria = _unitOfWork.SentinelSueldoPorIndustriaDataDinamicoRepository.ObtenerValorSueldoIndustria(idCargo.Value, idIndustria.Value, empresa.Valor == null ? 0 : empresa.Valor.Value);
                                if (industria != null)
                                {
                                    //promedio = industria.valor; //PI
                                    promedio.valor = industria.Valor;
                                    promedio.descripcion = "PI";
                                }
                            }
                        }
                        break;
                }
            }
            else
            {//caso que solo tenga cargo no industria 
                var promMercado = _unitOfWork.SentinelSueldoPorIndustriaDataTotalRepository.ObtenerSueldoPorCargoIndustria(idCargo.Value, idIndustria.Value);
                if (promMercado.Valor != null)
                {
                    //promedio = promMercado.valor;//PM
                    promedio.valor = promMercado.Valor;
                    promedio.descripcion = "PM";
                }
            }
            return promedio;
        }
    }
}
