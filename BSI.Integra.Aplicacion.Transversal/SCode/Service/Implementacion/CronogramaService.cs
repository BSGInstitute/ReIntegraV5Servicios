using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CronogramaService
    /// Autor: Margiory Ramirez..
    /// Fecha: 17/01/2023
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    /// 


    public class CronogramaService : ICronogramaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CronogramaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
            var config = new MapperConfiguration(
                cfg => {
                    cfg.CreateMap<EstadoMatricula, EstadoMatricula>(MemberList.None).ReverseMap();
                    cfg.CreateMap<CronogramaPagoDetalleFinal, TCronogramaPagoDetalleFinal>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TCronogramaPago, CronogramaPago>(MemberList.None).ReverseMap();
                });


            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TipoDato Add(TipoDato entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoDatoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoDato>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoDato Update(TipoDato entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoDatoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoDato>(modelo);
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
                _unitOfWork.TipoDatoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoDato> Add(List<TipoDato> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoDatoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoDato>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoDato> Update(List<TipoDato> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoDatoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoDato>>(modelo);
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
                _unitOfWork.TipoDatoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene todos los datos necesarios para un pago
        /// </summary>
        /// <returns>Json/returns>


        public object ObtenerDatosPago()
        {
            try
            {
                var _repFormaPago = _unitOfWork.FormaPagoRepository;
                var listaFormaPago = _repFormaPago.GetBy(x => x.Estado == true, x => new { x.Id, x.Descripcion }).OrderBy(x => x.Descripcion);

                var _repDocumentoPago = _unitOfWork.DocumentoPagoRepository;
                var listaDocumentoPago = _repDocumentoPago.GetBy(x => x.Estado == true, x => new { x.Id, Documento = x.Nombre }).OrderBy(x => x.Documento);

                var _repCuentaCorriente = _unitOfWork.CuentaCorrienteRepository;
                var listadoCuentasCorrientes = _repCuentaCorriente.ObtenerCuentasCorrientes();
                var listadoCuentasCorrientesFinal = new List<DatosCuentaCorrienteDTO>();

                foreach (var item in listadoCuentasCorrientes)
                {
                    var tempCuentaCorriente = new DatosCuentaCorrienteDTO
                    {
                        IdCta = item.IdCta,
                        Id = string.Concat(item.NumeroCuenta, "-", item.IdCiudad),
                        Cuenta = String.Concat(item.NombreEntidadFinanciera, " ", item.NumeroCuenta)
                    };
                    listadoCuentasCorrientesFinal.Add(tempCuentaCorriente);
                }
                return (new { listaFormaPago, listaDocumentoPago, listadoCuentasCorrientesFinal });
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene todos los datos necesarios para un pago
        /// </summary>
        /// <returns>Json/returns>



        public object ObtenerCodigoMatricula(Dictionary<string, string> Valor) //CodigoMatricula
        {
            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                //(_repMatriculaCabecera.GetBy(x => x.CodigoMatricula.Contains(Valor["filtro"]), x => new { Id = x.CodigoMatricula }));
                return (_repMatriculaCabecera.ObtenerCodigoMatricula(Valor["valor"].ToString()));

            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// retorna el nombre completo del alumno y su id , por el valor del parametro
        /// </summary>
        /// <returns>Json/returns>


        public List<AlumnoFiltroAutocompleteDTO> ObtenerAlumnoPorValor(Dictionary<string, string> Valor)
        {
            try
            {


                var _repAlumno = _unitOfWork.AlumnoRepository;
                var alumnosTemp = _repAlumno.ObtenerTodoFiltrosAutoComplete(Valor["valor"]);
                foreach (var item in alumnosTemp)
                {
                    item.NombreCompleto = string.Concat(item.NombreCompleto, " (", item.Id, ") ");
                }
                return alumnosTemp;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// Autor: Margiory Ramirez
        /// Fecha: 17/01/2023
        /// Versión: 1.0
        /// <summary>
        ///obtiene el codigo de matricula por codigo de alumno
        /// </summary>
        /// <returns>Json/returns>
        public List<CodigoMatriculaPEspecificoDTO> ObtenerCodigoMatriculaPEspecificoPorAlumnos(int idAlumno)
        {
            try
            {


                var _repCabeceraRepositorio = _unitOfWork.MatriculaCabeceraRepository;
                return (_repCabeceraRepositorio.ObtenerCodigoMatriculaPEspecificoPorAlumnos(idAlumno));
            }


            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene todos los estados de matricula 
        /// </summary>
        /// <returns>Json/returns>


        public object ObtenerTodoEstadoMatricula()
        {

            try
            {

                var _repMatricula = _unitOfWork.EstadoPagoMatriculaRepository;

                return (_repMatricula.ObtenerTodoEstadoMatricula());
            }


            catch (Exception e)
            {
                return (e.Message);
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene los datos del asesor por el apellido
        /// </summary>
        /// <returns>Json/returns>
        public object ObtenerAsesorPorApellidos()
        {
            try
            {

                var _repAsesor = _unitOfWork.PersonalRepository;

                return (_repAsesor.ObtenerAsesorPorApellidos());
            }


            catch (Exception e)
            {
                return (e.Message);
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// onbtiene los datos del coordinador por el apellido
        /// </summary>
        /// <returns>Json/returns>

        public object ObtenerCoordinadorPorApellidos()
        {
            try
            {

                var _repPersonal = _unitOfWork.PersonalRepository;

                return (_repPersonal.ObtenerCoordinadorPorApellidos());
            }


            catch (Exception e)
            {
                return (e.Message);
            }
        }


        /// Autor: Margiory Ramirez
        /// Fecha: 17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene las ccuotas no pagadas en base a la version final del cronograma del codigo de matricula
        /// </summary>
        /// <returns>Json/returns>


        public object ObtenerCuotasNoPagadas(string CodigoMatricula, int Version)
        {
            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                return (_repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == Version && x.Cancelado == false, x => new { x.Id, NroCuotaSubCuota = string.Concat(x.NroCuota, " - ", x.NroSubCuota), x.Cuota }));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Margiory Ramirez
        /// Fecha: 17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// <param name="Valor"></param>
        /// obtiene programas especificos por el nombre del centro de costo
        /// </summary>
        /// <returns>Json/returns>

        public object ObtenerPEspecificoPorCentroCosto(Dictionary<string, string> Valor)
        {
            try
            {

                {
                    var _repPespecifico = _unitOfWork.PEspecificoRepository;
                    return (_repPespecifico.ObtenerPEspecificoPorCentroCosto(Valor["filtro"]));
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// <param name="CodigoMatricula"></param>
        /// obtiene los datos del alumno en base al codigo de matricula
        /// </summary>
        /// <returns>Json/returns>


        public object ObtenerAlumnoProgramaEspecifico(string CodigoMatricula)
        {


            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                // var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                var matriculaCabeceraTemp = _repMatriculaCabecera.ObtenerIdMatriculaPorCodigo(CodigoMatricula);
                if (matriculaCabeceraTemp.EstadoMatricula.Equals("matriculado") || matriculaCabeceraTemp.EstadoMatricula.Equals("pormatricular"))
                {
                    return (_repMatriculaCabecera.ObtenerAlumnoProgramaEspecifico(matriculaCabeceraTemp.Id));
                }
                else
                {
                    List<AlumnoProgramaEspecificoDTO> lista = new List<AlumnoProgramaEspecificoDTO>();
                    return (lista);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Margiory Ramirez
        /// Fecha: 17/01/2023
        /// Versión: 1.0
        /// <summary>
        ///  /// <param name="CodigoMatricula"></param>
        /// Obtiene todos los datos de la matricula en base a su codigo
        /// </summary>
        /// <returns>Json/returns>


        public object ObtenerDatosMatriculaPorCodigoMatricula(string CodigoMatricula)
        {

            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                //     var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                var matriculaCabeceraTemp = _repMatriculaCabecera.ObtenerIdMatriculaPorCodigo(CodigoMatricula);
                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                if(versionAprobada!=null)
                {
                    var resultado = _repMatriculaCabecera.ObtenerDatosMatriculaPorCodigoMatriculaVersion(CodigoMatricula, versionAprobada.Version);

                    var beneficiosmatricula = _repMatriculaCabecera.ObtenerBeneficiosMatriculaCabecera(CodigoMatricula);
                    return (new { resultado, beneficiosmatricula });

                }
                else
                {
                    throw new Exception("No se encontro ninguna version aprobada para esta matricula");
                }

                

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// Autor: Margiory Ramirez
        /// Fecha: 18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// <param name="Json"></param>
        /// Actualiza los datos de una matricula
        /// </summary>
        /// <returns>Json/returns>

        public object ActualizarMatricula(MatriculaActualizarDTO Json)
        {


            try
            {

                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var _repCronogramaPago = _unitOfWork.CronogramaPagoRepository;

                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == Json.Codigomatricula).FirstOrDefault();

                if (matriculaCabeceraTemp != null)
                {
                    matriculaCabeceraTemp.FechaModificacion = DateTime.Now;
                    matriculaCabeceraTemp.UsuarioModificacion = Json.usuario;

                    if (Json.Estado != null)
                    {
                        if (matriculaCabeceraTemp.EstadoMatricula == "pormatricular" && Json.Estado == "matriculado")
                        {
                            matriculaCabeceraTemp.FechaPorMatricularMatriculado = DateTime.Now;
                        }

                        if (matriculaCabeceraTemp.EstadoMatricula == "pormatricular" && Json.Estado == "matriculado" && (matriculaCabeceraTemp.EmpresaPaga == "SI" || Json.EmpresaPaga == true))
                        {
                            matriculaCabeceraTemp.FechaMatricula = DateTime.Now;
                            matriculaCabeceraTemp.IdEstadoPagoMatricula = 2;
                        }
                        matriculaCabeceraTemp.EstadoMatricula = Json.Estado;
                    }

                    if (Json.Periodo != null && Json.Periodo != 0)
                        matriculaCabeceraTemp.IdPeriodo = Json.Periodo.Value;
                    if (Json.Programa != null && Json.Programa != 0)
                        matriculaCabeceraTemp.IdPespecifico = Json.Programa.Value;
                    if (Json.Asesor != null && Json.Asesor != 0)
                        matriculaCabeceraTemp.IdAsesor = Json.Asesor.Value;
                    if (Json.Coordinador != null && Json.Coordinador != 0)
                        matriculaCabeceraTemp.IdCoordinador = Json.Coordinador.Value;

                    var cronogramaPagosTemp = _repCronogramaPago.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).FirstOrDefault();
                    if (cronogramaPagosTemp != null)
                    {
                        cronogramaPagosTemp.FechaModificacion = DateTime.Now;
                        cronogramaPagosTemp.UsuarioModificacion = Json.usuario;
                        if (Json.Observaciones != null)
                            cronogramaPagosTemp.Observaciones = Json.Observaciones;
                    }

                    if (Json.EmpresaNombre != null)
                        matriculaCabeceraTemp.EmpresaNombre = Json.EmpresaNombre;

                    matriculaCabeceraTemp.EmpresaPaga = Json.EmpresaPaga == true ? "SI" : "NO";

                    //Actualizamos
                    _repMatriculaCabecera.Update(matriculaCabeceraTemp);
                    _unitOfWork.Commit();
                    _repCronogramaPago.Update(cronogramaPagosTemp);
                    _unitOfWork.Commit();

                    return (new { Message = "Se Actualizo Correctamente" });
                }
                else
                {
                    return ("Error, Matricula no Existe");
                }



            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }


        /// Autor: Margiory Ramirez
        /// Fecha: 18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// <param name="CodigoMatricula"></param>
        /// obtiene el cronograma valido de una matricula en base a su codigo
        /// </summary>
        /// <returns>Json/returns>


        public object ObtenerCronograma(string CodigoMatricula)
        {

            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                //var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                var matriculaCabeceraTemp = _repMatriculaCabecera.ObtenerIdMatriculaPorCodigo(CodigoMatricula);
                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                var _repCronogramaPagoDetalleOriginal = _unitOfWork.CronogramaPagoDetalleOriginalRepository;

                var cronogramaOriginal = _repCronogramaPagoDetalleOriginal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id, x => new { x.Id, x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.TotalPagar, x.Cuota, x.FechaVencimiento, x.Moneda });
                var cronogramaPagoDetalleFinal = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == versionAprobada.Version, x =>
                                                                                        new { x.Id, x.Cancelado, x.EnviadoSiigo,x.FacturamaEnvio, FlagCancelado = x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.FechaVencimiento,
                                                                                            x.TotalPagar, x.Cuota, x.Mora, x.Saldo, x.Moneda, x.MontoPagado, x.FechaPago, x.IdFormaPago,
                                                                                            x.IdCuenta, x.FechaPagoBanco, x.Enviado, x.Observaciones, x.IdDocumentoPago, x.NroDocumento, x.MonedaPago,
                                                                                            x.TipoCambio, x.CuotaDolares, x.FechaProcesoPago, x.Version, x.FechaDeposito,x.MoraTarifario,x.MonedaMoraTarifario }).OrderBy(x => x.NroCuota).ThenBy(x => x.NroSubCuota);

                var flagSinAprobar = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == false).Count() > 0 ? true : false;
                return new { cronogramaOriginal, cronogramaPagoDetalleFinal, flagSinAprobar };
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }

        public object ObtenerCronogramFacturacion(string CodigoMatricula)
        {

            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                //var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                var matriculaCabeceraTemp = _repMatriculaCabecera.ObtenerIdMatriculaPorCodigo(CodigoMatricula);
                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                var _repCronogramaPagoDetalleOriginal = _unitOfWork.CronogramaPagoDetalleOriginalRepository;

                var cronogramaOriginal = _repCronogramaPagoDetalleOriginal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id, x => new { x.Id, x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.TotalPagar, x.Cuota, x.FechaVencimiento, x.Moneda });
                var cronogramaPagoDetalleFinal = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == versionAprobada.Version, x =>
                                                                                        new {
                                                                                            x.Id,
                                                                                            x.Cancelado,
                                                                                            x.EnviadoSiigo,
                                                                                            x.FacturamaEnvio,
                                                                                            FlagCancelado = x.Cancelado,
                                                                                            x.NroCuota,
                                                                                            x.NroSubCuota,
                                                                                            x.TipoCuota,
                                                                                            x.FechaVencimiento,
                                                                                            x.TotalPagar,
                                                                                            x.Cuota,
                                                                                            x.Mora,
                                                                                            x.Saldo,
                                                                                            x.Moneda,
                                                                                            x.MontoPagado,
                                                                                            x.FechaPago,
                                                                                            x.IdFormaPago,
                                                                                            x.IdCuenta,
                                                                                            x.FechaPagoBanco,
                                                                                            x.Enviado,
                                                                                            x.Observaciones,
                                                                                            x.IdDocumentoPago,
                                                                                            x.NroDocumento,
                                                                                            x.MonedaPago,
                                                                                            x.TipoCambio,
                                                                                            x.CuotaDolares,
                                                                                            x.FechaProcesoPago,
                                                                                            x.Version,
                                                                                            x.FechaDeposito,
                                                                                            x.MoraTarifario,
                                                                                            x.MonedaMoraTarifario,
                                                                                            x.IdMatriculaCabecera,
                                                                                            IdAlumno = matriculaCabeceraTemp.IdAlumno ,


                                                                                        }).OrderBy(x => x.NroCuota).ThenBy(x => x.NroSubCuota);

                var flagSinAprobar = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == false).Count() > 0 ? true : false;
                return new
                {
                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                    IdAlumno = matriculaCabeceraTemp.IdAlumno, 

                    cronogramaOriginal, cronogramaPagoDetalleFinal, flagSinAprobar };
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }


        /// Autor: Margiory Ramirez
        /// Fecha: 18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// <param name="CodigoMatricula"></param>
        /// Obtiene Costos Administrativos por Codigo de Matricula
        /// </summary>
        /// <returns> Lista de Costos Administrativos </returns>
        /// <returns> Lista de Objeto DTO : List<CostosAdministrativosDTO> </returns>

        public object ObtenerCostosAdministrativosCodigoMatricula(string CodigoMatricula)
        {
            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var MatriculaTemporal = _repMatriculaCabecera.GetBy(w => w.CodigoMatricula == CodigoMatricula).FirstOrDefault();

                if (MatriculaTemporal == null)
                {
                    return ("No existe Codigo de Matricula con Costos Administrativos");
                }
                else
                {
                    var CostosAdministrativos = _repMatriculaCabecera.ObtenerCostosAdministrativos(MatriculaTemporal.Id);

                    return (CostosAdministrativos);
                }
            }
            catch (Exception e)
            {
                return (e.Message);
            }

        }


        /// Autor: Margiory Ramirez
        /// Fecha: 18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene los datos del perdonal
        /// </summary>
        /// <returns>Json/returns>

        public object ObtenerTodoPersonal()
        {

            try
            {
                var _repPersonalRepositorio = _unitOfWork.PersonalRepository;
                return (_repPersonalRepositorio.GetBy(x => x.Activo == true, x => new { x.Id, NombreCompleto = string.Concat(x.Apellidos, " ", x.Nombres), x.Rol }));
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtieneel personal vcalidado en base al apellido
        ///  <param name="Valor"></param>/summary>
        /// <returns> DTO: PersonalAsignadoReportePendienteDTO </returns>

        public object ObtenerPersonalAprobadoPorApellido(Dictionary<string, string> Valor)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalAprobadoPorApellido(Valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// <param name="IdCuota"></param>/summary>
        /// <param name="IdFormaPago"></param>/summary>
        /// <param name="Usuario"></param>/summary>
        /// Actualiza la forma de pago
        /// </summary>
        /// <returns>Json/returns>



        public object ActualizarFormaPago(int IdCuota, int? IdFormaPago, string Usuario)
        {

            try
            {
                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                var CronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinal();
                CronogramaPagoDetalleFinal = _mapper.Map<CronogramaPagoDetalleFinal>(_repCronogramaPagoDetalleFinal.FirstById(IdCuota));
                CronogramaPagoDetalleFinal.IdFormaPago = IdFormaPago == 0 ? null : IdFormaPago;
                CronogramaPagoDetalleFinal.FechaModificacion = DateTime.Now;
                CronogramaPagoDetalleFinal.UsuarioModificacion = Usuario;
                _repCronogramaPagoDetalleFinal.Update(CronogramaPagoDetalleFinal);
                _unitOfWork.Commit();

                return (new { Message = "Se Actualizo correctamente" });
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }

        }



        /// Autor: Margiory Ramirez
        /// Fecha: 18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la fecha de deposito
        /// </summary>
        /// <returns>Json/returns>
        public object ActualizarFechaDeposito(PagoActualizadoFechaDepositoDTO Json)
        {

            try
            {
                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                var CronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinal();

                CronogramaPagoDetalleFinal = _mapper.Map<CronogramaPagoDetalleFinal>(_repCronogramaPagoDetalleFinal.FirstById(Json.IdCuota));
                CronogramaPagoDetalleFinal.FechaDeposito = Json.FechaDeposito;
                CronogramaPagoDetalleFinal.FechaModificacion = DateTime.Now;
                CronogramaPagoDetalleFinal.UsuarioModificacion = Json.Usuario;
                _repCronogramaPagoDetalleFinal.Update(CronogramaPagoDetalleFinal);
                _unitOfWork.Commit();

                return (new { Message = "Se Actualizo correctamente" });
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }

        }
        /// Autor: Margiory Ramirez
        /// Fecha: 18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Guarda una pago , y modifica las tablas de cronograma a pagado
        /// </summary>
        /// <returns>Json/returns>


        public object GuardarPagoCuota(PagoCuotaCronogramaDTO Json)
        {

            try
            {

                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == Json.CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                var CronogramaActual = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == versionAprobada.Version).OrderBy(w => w.NroCuota).ThenBy(w => w.NroSubCuota).ToList();

                CronogramaPagoDetalleFinal CronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinal();
                this.GuardarPago(Json, CronogramaActual, Json.NroCuota, Json.NroSubCuota);
                return (new { Message = "Se Realizo el pago correctamente" });
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Guarda una pago , y modifica las tablas de cronograma a pagado
        /// </summary>
        /// <returns>Json/returns>
        public bool GuardarPago(PagoCuotaCronogramaDTO objeto, List<TCronogramaPagoDetalleFinal> CronogramaActual, int NroCuotaGlobal, int NroSubCuotaGlobal)
        {
            var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
            var _repCronogramaCabeceraCambio = _unitOfWork.CronogramaCabeceraCambioRepository;
            var _repCronogramaPagoDetalleModLogFinal = _unitOfWork.CronogramaPagoDetalleModLogFinalRepository;
            var _repCronogramaDetalleCambio = _unitOfWork.CronogramaDetalleCambioRepository;
            var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
            var _repCronogramaPago = _unitOfWork.CronogramaPagoRepository;
            var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(y => y.CodigoMatricula == objeto.CodigoMatricula, y => new { y.Id }).FirstOrDefault();

            try
            {
                objeto.MontoBase = objeto.MontoBase + objeto.Mora;

                decimal monto = 0;
                int tiposmoneda = 0;
                if (objeto.MonedaBase == objeto.MonedaPago)
                {
                    monto = objeto.MontoPago;
                    tiposmoneda = 1;//pagos en misma moneda
                }   
                else if (objeto.MonedaBase != "dolares" && objeto.MonedaPago == "dolares")//monto base soles y paga con dolares
                {
                    monto = objeto.MontoPago * objeto.TipoCambio;
                    tiposmoneda = 2;//pagos en dolares a moneda origen
                }
                else if (objeto.MonedaBase == "dolares" && objeto.MonedaPago != "dolares")//monto base dolares y paga con soles
                {
                    monto = objeto.MontoPago / objeto.TipoCambio;
                    tiposmoneda = 3;//pagos en moneda origen     a cuota en dolares
                }

                //traigo la cuota del cronograma actual // si es igual el monto //si es menor ya lo valida en la vista
                var cuotaapagar = _mapper.Map<CronogramaPagoDetalleFinal>(_repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.NroCuota == objeto.NroCuota && w.NroSubCuota == objeto.NroSubCuota && w.Version == CronogramaActual.FirstOrDefault().Version).FirstOrDefault()) ; //this.GetbyCuotaSubCuotaVersion(objeto.NroCuota, objeto.NroSubCuota, CronogramaActual.FirstOrDefault().Version, objeto.CodigoMatricula).FirstOrDefault();//cronogramaactual.Where(w => w.Cuota == objeto.nrocuota && w.SubCuota == objeto.nrosubcuota).FirstOrDefault();//siempre deberia traer un valor
                var comprobante = _repCronogramaPago.ObtenerComprobanteReciente(cuotaapagar.IdMatriculaCabecera.Value);
                

                //se valida con la mora mas ahora para que cuadre y no se cree otra cuota
                if ((cuotaapagar.Cuota + cuotaapagar.Mora) == monto || (cuotaapagar.Cuota + cuotaapagar.Mora) == Math.Round(monto, 2))// los montos son iguales en ese caso cancelo solo esa cuota sin crear una nueva version
                {
                    if ((cuotaapagar.Cuota + cuotaapagar.Mora) != monto)
                    {
                        monto = Math.Round(monto, 2);
                    }

                    string monedapagos = objeto.MonedaPago;
                    cuotaapagar.FechaProcesoPago = DateTime.Now;//el dia que registro el apgo en el sistema
                    cuotaapagar.TipoCambio = objeto.TipoCambio;
                    cuotaapagar.MonedaPago = monedapagos;
                    cuotaapagar.NroDocumento = objeto.NroDocumento;
                    cuotaapagar.IdDocumentoPago = objeto.Documento;
                    cuotaapagar.IdCuenta = objeto.NroCuenta;
                    cuotaapagar.IdFormaPago = objeto.FormaPago;
                    cuotaapagar.FechaPago = objeto.Fecha;
                    cuotaapagar.Cancelado = true;
                    cuotaapagar.MontoPagado = objeto.MontoPago;
                    cuotaapagar.FechaModificacion = DateTime.Now;
                    cuotaapagar.UsuarioModificacion = objeto.usuario;

                    cuotaapagar.IdTipoComprobante = comprobante.IdTipoComprobante==-1?null: comprobante.IdTipoComprobante;
                    cuotaapagar.NroDocumentoComprobante = comprobante.NroDocumentoComprobante;
                    cuotaapagar.NombreRazonSocial = comprobante.NombreRazonSocial;


                    cuotaapagar = new CronogramaPagoDetalleFinalService(_unitOfWork).Update(cuotaapagar);//hace el pago directo en la misma version
                    //inserto en tpagosfinal
                    //objeto.formapago//esta en int y debe ser en string
                    //objeto.documento//esta en int  y debe ser string
                    //objeto.nrocuenta//esta en int y debe ser string
                    var insertado = this.InsertarPagoWebFinal(objeto.CodigoMatricula, objeto.MontoPago, monedapagos, (float)objeto.TipoCambio, objeto.FormaPago, objeto.Documento, objeto.NroDocumento, objeto.NroCuenta, objeto.NroCheque, DateTime.Now, objeto.NroDeposito, objeto.usuario);
                    //fin inserto en tpagosfinal

                    //valido si la cuota es 1-1
                    if (objeto.NroCuota == 1 && objeto.NroSubCuota == 1)// si esta pagando la primera cuota
                    {
                        var OriginalActualizado = this.ActualizarOriginal(CronogramaActual.FirstOrDefault().Version.Value, objeto.CodigoMatricula, objeto.usuario);
                    }

                    return true;
                }
                else
                {
                    var cronograma = _mapper.Map<List<CronogramaPagoDetalleFinal>>(CronogramaActual);
                    int versionactual = cronograma.FirstOrDefault().Version.Value + 1;

                    string monedapagos = objeto.MonedaPago;

                    List<CronogramaPagoDetalleFinal> nuevocronograma = new List<CronogramaPagoDetalleFinal>();
                    List<TCronogramaDetalleCambio> detallecambios = new List<TCronogramaDetalleCambio>();
                    decimal resto = 0;
                    bool flagresto = false;
                    var i=0;
                    foreach (var item in cronograma)
                    {

                        if (flagresto == true)
                        {
                            if (cronograma.Where(w => w.NroCuota == item.NroCuota && w.NroSubCuota == item.NroSubCuota + 1).FirstOrDefault() != null)// si en el cronograma hay uno con el nro de subcuota +1
                            {
                                bool exista = true;
                                int cont = 1;
                                int maximosubcuota = 0;
                                while (exista)
                                {
                                    var itemexiste = cronograma.Where(w => w.NroCuota == item.NroCuota && w.NroSubCuota == item.NroSubCuota + cont).FirstOrDefault();

                                    if (itemexiste != null)
                                    {
                                        exista = true;
                                        cont++;
                                    }
                                    else
                                    {
                                        maximosubcuota = item.NroSubCuota.Value + cont;
                                        exista = false;
                                    }

                                }
                                while (item.NroSubCuota != maximosubcuota - 1)
                                {
                                    var temp = cronograma.Where(w => w.NroCuota == item.NroCuota && w.NroSubCuota == maximosubcuota - 1).FirstOrDefault();
                                    TCronogramaDetalleCambio nuevo1 = new TCronogramaDetalleCambio()
                                    {
                                        Id = 0,
                                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                        IdCronogramaCabeceraCambio = 0,//abajo lo cambio
                                        NroCuota = temp.NroCuota.Value,
                                        NroSubcuota = maximosubcuota,
                                        FechaVencimiento = temp.FechaVencimiento.Value,
                                        Cuota = temp.Cuota.Value,
                                        Mora = temp.Mora.Value,
                                        Moneda = temp.Moneda,
                                        Version = 0//abajo lo cambios

                                    };
                                    detallecambios.Add(nuevo1);
                                    cronograma.Where(w => w.NroCuota == item.NroCuota && w.NroSubCuota == maximosubcuota - 1).FirstOrDefault().NroSubCuota = maximosubcuota;
                                    maximosubcuota--;
                                }

                                item.NroSubCuota = item.NroSubCuota + 1;
                                decimal restomonedapago = 0;
                                if (tiposmoneda == 1)//misma moneda
                                {
                                    item.Cuota = item.Cuota - resto;
                                    restomonedapago = resto;
                                }
                                if (tiposmoneda == 2)//(D)soles (P)dolares //sigue el resto en soles
                                {
                                    item.Cuota = item.Cuota - resto;
                                    restomonedapago = resto / objeto.TipoCambio;
                                }
                                if (tiposmoneda == 3)//(D)dolares (P)soles //sigue el resto en dolares
                                {
                                    item.Cuota = item.Cuota - resto;
                                    restomonedapago = resto * objeto.TipoCambio;
                                }
                                //se debe insertar en false proque es la cuota que se baja el monto
                                item.Enviado = false;


                                //////////////////////////////////////////////////////////////////////////////////
                                //se inserta en log la nueva cuota que se modifoc de monto y se le resto el resto

                                TCronogramaPagoDetalleModLogFinal log = new TCronogramaPagoDetalleModLogFinal()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    Fecha = DateTime.Now,
                                    NroCuota = item.NroCuota,
                                    NroSubCuota = item.NroSubCuota,
                                    FechaVencimiento = item.FechaVencimiento,
                                    TotalPagar = item.TotalPagar,
                                    Cuota = item.Cuota,
                                    Mora = item.Mora,
                                    MontoPagado = 0,
                                    Saldo = item.Saldo,
                                    Cancelado = item.Cancelado,
                                    TipoCuota = item.TipoCuota,
                                    Moneda = item.Moneda,
                                    FechaPago = null,
                                    IdFormaPago = null,
                                    FechaPagoBanco = null,
                                    Ultimo = false,
                                    IdDocumentoPago = null,
                                    NroDocumento = null,
                                    MonedaPago = null,
                                    TipoCambio = null,
                                    MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA  (" + item.Cuota + "," + item.NroSubCuota + ")," + item.Cuota,
                                    FechaProcesoPago = null,
                                    EstadoPrimerLog = null
                                };

                                log.Version = versionactual;
                                log.Aprobado = true;
                                log.Estado2 = true;
                                log.FechaCreacion = DateTime.Now;
                                log.FechaModificacion = DateTime.Now;
                                log.UsuarioCreacion = "SYSTEM";
                                log.UsuarioModificacion = "SYSTEM";
                                log.Estado = true;
                                var insertlog = _repCronogramaPagoDetalleModLogFinal.Insert(log);
                                _unitOfWork.Commit();
                                //////////////////////////////////////////////////////////////////////////////////

                                //item.Version = versionactual;
                                //nuevocronograma.Add(item);//añado el antiguo con subcuota+1

                                /////////////////////////////////////////// añado tmb el anterior que se volvera +1
                                TCronogramaDetalleCambio nuevo2 = new TCronogramaDetalleCambio()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    IdCronogramaCabeceraCambio = 0,//cabajo lo cambio
                                    NroCuota = item.NroCuota.Value,
                                    NroSubcuota = item.NroSubCuota.Value,
                                    FechaVencimiento = item.FechaVencimiento.Value,
                                    Cuota = item.Cuota.Value,
                                    Mora = item.Mora.Value,
                                    Moneda = item.Moneda,
                                    Version = 0//abajo lo cambios

                                };
                                detallecambios.Add(nuevo2);
                                /////////////////////////////////////////////
                                ///////////////////////////////////

                                var padre = cronograma.Where(w => w.NroCuota == objeto.NroCuota && w.NroSubCuota == objeto.NroSubCuota).FirstOrDefault();
                                //se pone la antigua como el resto
                                CronogramaPagoDetalleFinal nuevoresto = new CronogramaPagoDetalleFinal()
                                {
                                    Id = 0,
                                    NroCuota = item.NroCuota,
                                    NroSubCuota = item.NroSubCuota - 1,
                                    FechaVencimiento = padre.FechaVencimiento,
                                    //Total=
                                    Cuota = resto,
                                    Mora = 0,
                                    MontoPagado = restomonedapago,
                                    //Saldo
                                    Cancelado = true,
                                    TipoCuota = padre.TipoCuota,
                                    FechaDeposito = padre.FechaDeposito,
                                    Moneda = objeto.MonedaBase,
                                    FechaPago = objeto.Fecha,
                                    IdFormaPago = objeto.FormaPago,
                                    IdCuenta = objeto.NroCuenta,
                                    IdDocumentoPago = objeto.Documento,
                                    NroDocumento = objeto.NroDocumento,
                                    MonedaPago = objeto.MonedaPago,
                                    TipoCambio = objeto.TipoCambio,
                                    FechaProcesoPago = DateTime.Now,
                                    Version = versionactual,//una version mas
                                    Enviado = true,//200618 se manda a true la nueva con el monto del resto

                                    IdTipoComprobante = comprobante.IdTipoComprobante == -1 ? null : comprobante.IdTipoComprobante,
                                    NroDocumentoComprobante = comprobante.NroDocumentoComprobante,
                                    NombreRazonSocial = comprobante.NombreRazonSocial
                            };
                                nuevocronograma.Add(nuevoresto);//añado el nuevo resto
                                /////////////////////////////////////////////////////////////// tmb añado el resto
                                TCronogramaDetalleCambio nuevo3 = new TCronogramaDetalleCambio()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    IdCronogramaCabeceraCambio = 0,//cabajo lo cambio
                                    NroCuota = nuevoresto.NroCuota.Value,
                                    NroSubcuota = nuevoresto.NroSubCuota.Value,
                                    FechaVencimiento = nuevoresto.FechaVencimiento.Value,
                                    Cuota = nuevoresto.Cuota.Value,
                                    Mora = nuevoresto.Mora.Value,
                                    Moneda = nuevoresto.Moneda,
                                    Version = 0//abajo lo cambios

                                };
                                detallecambios.Add(nuevo3);
                                ///////////////////////////////////////////////////////////

                            }
                            else
                            {
                                //facil 
                                //la antigua pasa a ser la nueva  con la diferencia que se le resta el resto
                                decimal restomonedapago = 0;
                                item.NroSubCuota = item.NroSubCuota + 1;
                                if (tiposmoneda == 1)//misma moneda
                                {
                                    item.Cuota = item.Cuota - resto;
                                    restomonedapago = resto;
                                }
                                if (tiposmoneda == 2)//(D)soles (P)dolares //sigue el resto en soles
                                {
                                    item.Cuota = item.Cuota - resto;
                                    restomonedapago = resto / objeto.TipoCambio;
                                }
                                if (tiposmoneda == 3)//(D)dolares (P)soles //sigue el resto en dolares
                                {
                                    item.Cuota = item.Cuota - resto;
                                    restomonedapago = resto * objeto.TipoCambio;
                                }
                                //item.Version = versionactual;
                                //nuevocronograma.Add(item);//añado el antiguo con subcuota+1
                                //a la cuota que se le resta se le pone ENVIADO=0
                                item.Enviado = false;
                                //////////////////////////////////////////////////////////////////////////////////
                                //se inserta en log la nueva cuota que se modifoc de monto y se le resto el resto

                                TCronogramaPagoDetalleModLogFinal log = new TCronogramaPagoDetalleModLogFinal()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    Fecha = DateTime.Now,
                                    NroCuota = item.NroCuota,
                                    NroSubCuota = item.NroSubCuota,
                                    FechaVencimiento = item.FechaVencimiento,
                                    TotalPagar = item.TotalPagar,
                                    Cuota = item.Cuota,
                                    Mora = item.Mora,
                                    MontoPagado = 0,
                                    Saldo = item.Saldo,
                                    Cancelado = item.Cancelado,
                                    TipoCuota = item.TipoCuota,
                                    Moneda = item.Moneda,
                                    FechaPago = null,
                                    IdFormaPago = null,
                                    FechaPagoBanco = null,
                                    Ultimo = false,
                                    IdDocumentoPago = null,
                                    NroDocumento = null,
                                    MonedaPago = null,
                                    TipoCambio = null,
                                    MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA  (" + item.NroCuota + "," + item.NroSubCuota + ")," + item.Cuota,
                                    FechaProcesoPago = null,
                                    EstadoPrimerLog = null
                                };
                                log.Version = versionactual;
                                log.Aprobado = true;
                                log.Estado2 = true;
                                log.FechaCreacion = DateTime.Now;
                                log.FechaModificacion = DateTime.Now;
                                log.UsuarioCreacion = "SYSTEM";
                                log.UsuarioModificacion = "SYSTEM";
                                log.Estado = true;
                                var insertlog = _repCronogramaPagoDetalleModLogFinal.Insert(log);
                                _unitOfWork.Commit();
                                //////////////////////////////////////////////////////////////////////////////////


                                /////////////////////////////////////////// añado tmb el anterior que se volvera +1
                                TCronogramaDetalleCambio nuevo1f = new TCronogramaDetalleCambio()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    IdCronogramaCabeceraCambio = 0,//cabajo lo cambio
                                    NroCuota = item.NroCuota.Value,
                                    NroSubcuota = item.NroSubCuota.Value,
                                    FechaVencimiento = item.FechaVencimiento.Value,
                                    Cuota = item.Cuota.Value,
                                    Mora = item.Mora.Value,
                                    Moneda = item.Moneda,
                                    Version = 0//abajo lo cambios

                                };
                                detallecambios.Add(nuevo1f);
                                /////////////////////////////////////////////


                                var padre = cronograma.Where(w => w.NroCuota == objeto.NroCuota && w.NroSubCuota == objeto.NroSubCuota).FirstOrDefault();
                                //se pone la antigua como el resto
                                CronogramaPagoDetalleFinal nuevoresto = new CronogramaPagoDetalleFinal()
                                {
                                    Id = 0,
                                    NroCuota = item.NroCuota,
                                    NroSubCuota = item.NroSubCuota - 1,
                                    FechaVencimiento = padre.FechaVencimiento,
                                    //Total=
                                    Cuota = resto,
                                    Mora = 0,
                                    MontoPagado = restomonedapago,
                                    //Saldo
                                    Cancelado = true,
                                    TipoCuota = padre.TipoCuota,
                                    FechaDeposito = padre.FechaDeposito,
                                    Moneda = objeto.MonedaBase,
                                    FechaPago = objeto.Fecha,
                                    IdFormaPago = objeto.FormaPago,
                                    IdCuenta = objeto.NroCuenta,
                                    IdDocumentoPago = objeto.Documento,
                                    NroDocumento = objeto.NroDocumento,
                                    MonedaPago = objeto.MonedaPago,
                                    TipoCambio = objeto.TipoCambio,
                                    FechaProcesoPago = DateTime.Now,
                                    Version = versionactual,//una version mas
                                    Enviado = true, //a la cuota con el resto resta se le pone ENVIADO=1
                                    IdTipoComprobante = comprobante.IdTipoComprobante == -1 ? null : comprobante.IdTipoComprobante,
                                    NroDocumentoComprobante = comprobante.NroDocumentoComprobante,
                                    NombreRazonSocial = comprobante.NombreRazonSocial
                                };
                                nuevocronograma.Add(nuevoresto);//añado el nuevo resto
                                /////////////////////////////////////////////////////////////// tmb añado el resto
                                TCronogramaDetalleCambio nuevo2f = new TCronogramaDetalleCambio()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    IdCronogramaCabeceraCambio = 0,//cabajo lo cambio
                                    NroCuota = nuevoresto.NroCuota.Value,
                                    NroSubcuota = nuevoresto.NroSubCuota.Value,
                                    FechaVencimiento = nuevoresto.FechaVencimiento.Value,
                                    Cuota = nuevoresto.Cuota.Value,
                                    Mora = nuevoresto.Mora.Value,
                                    Moneda = nuevoresto.Moneda,
                                    Version = 0//abajo lo cambios

                                };
                                detallecambios.Add(nuevo2f);
                                ///////////////////////////////////////////////////////////
                            }
                            flagresto = false;
                        }

                        if (item.NroCuota == objeto.NroCuota && item.NroSubCuota == objeto.NroSubCuota)
                        {
                            //la cancelo
                            if (tiposmoneda == 1)//misma moneda
                            {
                                resto = monto - objeto.MontoBase ;
                                objeto.MontoPago = monto - resto;
                                item.MontoPagado = objeto.MontoPago;
                                item.Cancelado = true;
                                item.FechaPago = objeto.Fecha;
                                item.IdFormaPago = objeto.FormaPago;
                                item.IdCuenta = objeto.NroCuenta;
                                item.IdDocumentoPago = objeto.Documento;
                                item.NroDocumento = objeto.NroDocumento;
                                item.MonedaPago = objeto.MonedaPago;
                                item.TipoCambio = objeto.TipoCambio;
                                item.FechaProcesoPago = DateTime.Now;
                                item.Version = versionactual;//una version mas
                                item.IdTipoComprobante = comprobante.IdTipoComprobante == -1 ? null : comprobante.IdTipoComprobante;
                                item.NroDocumentoComprobante = comprobante.NroDocumentoComprobante;
                                item.NombreRazonSocial = comprobante.NombreRazonSocial;
                            }
                            //la cancelo
                            if (tiposmoneda == 2)//(D)soles (P)dolares
                            {
                                resto = monto - objeto.MontoBase;//sigue el resto en soles
                                objeto.MontoPago = monto - resto;
                                objeto.MontoPago = objeto.MontoPago / objeto.TipoCambio;

                                item.MontoPagado = objeto.MontoPago;
                                item.Cancelado = true;
                                item.FechaPago = objeto.Fecha;
                                item.IdFormaPago = objeto.FormaPago;
                                item.IdCuenta = objeto.NroCuenta;
                                item.IdDocumentoPago = objeto.Documento;
                                item.NroDocumento = objeto.NroDocumento;
                                item.MonedaPago = objeto.MonedaPago;
                                item.TipoCambio = objeto.TipoCambio;
                                item.FechaProcesoPago = DateTime.Now;
                                item.Version = versionactual;//una version mas
                                item.IdTipoComprobante = comprobante.IdTipoComprobante == -1 ? null : comprobante.IdTipoComprobante;
                                item.NroDocumentoComprobante = comprobante.NroDocumentoComprobante;
                                item.NombreRazonSocial = comprobante.NombreRazonSocial;
                            }
                            //la cancelo
                            if (tiposmoneda == 3)//(D)dolares (P)soles
                            {
                                resto = monto - objeto.MontoBase;//sigue el resto en dolares
                                objeto.MontoPago = monto - resto;
                                objeto.MontoPago = objeto.MontoPago * objeto.TipoCambio;

                                item.MontoPagado = objeto.MontoPago;
                                item.Cancelado = true;
                                item.FechaPago = objeto.Fecha;
                                item.IdFormaPago = objeto.FormaPago;
                                item.IdCuenta = objeto.NroCuenta;
                                item.IdDocumentoPago = objeto.Documento;
                                item.NroDocumento = objeto.NroDocumento;
                                item.MonedaPago = objeto.MonedaPago;
                                item.TipoCambio = objeto.TipoCambio;
                                item.FechaProcesoPago = DateTime.Now;
                                item.Version = versionactual;//una version mas
                                item.IdTipoComprobante = comprobante.IdTipoComprobante == -1 ? null : comprobante.IdTipoComprobante;
                                item.NroDocumentoComprobante = comprobante.NroDocumentoComprobante;
                                item.NombreRazonSocial = comprobante.NombreRazonSocial;
                            }
                            nuevocronograma.Add(item);

                            /////////////////////////////////////////////////////////////// tmb añado el resto
                            TCronogramaDetalleCambio nuevo2f = new TCronogramaDetalleCambio()
                            {
                                Id = 0,
                                IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                IdCronogramaCabeceraCambio = 0,//abajo lo cambio
                                NroCuota = item.NroCuota.Value,
                                NroSubcuota = item.NroSubCuota.Value,
                                FechaVencimiento = item.FechaVencimiento.Value,
                                Cuota = item.Cuota.Value,
                                Mora = item.Mora.Value,
                                Moneda = item.Moneda,
                                Version = 0//abajo lo cambios

                            };
                            detallecambios.Add(nuevo2f);
                            ///////////////////////////////////////////////////////////

                            if (Math.Round(resto, 2) > 0)//si hay resto
                            {
                                //030718 valido si cubre tmb la sgte cuota
                                if (resto >= (cronograma[i + 1].Cuota + cronograma[i + 1].Mora))
                                {
                                    objeto.NroCuota = cronograma[i + 1].NroCuota.Value;
                                    objeto.NroSubCuota = cronograma[i + 1].NroSubCuota.Value;
                                    objeto.MontoBase = cronograma[i + 1].Cuota.Value + cronograma[i + 1].Mora.Value;
                                    objeto.Mora = cronograma[i + 1].Mora.Value;
                                    monto = resto;
                                    //para que se valide con sgte y no vaya a resto
                                    flagresto = false;
                                }
                                else
                                {
                                    //La sgte cuota se divide en 2
                                    flagresto = true;
                                }
                                //fin 030718
                            }
                        }
                        else
                        {
                            item.Version = versionactual;
                            nuevocronograma.Add(item);
                        }
                        i++;
                    }


                    TCronogramaCabeceraCambio cambioagregar = new TCronogramaCabeceraCambio()
                    {
                        Id = 0,
                        IdCronogramaTipoModificacion = 7,//add cuotas by pago
                        SolicitadoPor = 1,
                        AprobadoPor = 1,
                        Aprobado = true,
                        Cancelado = false,
                        Observacion = "Se añadio cuotas por pago excesivo en la cuota:" + objeto.NroCuota + " nrosubcuota:" + objeto.NroSubCuota,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = objeto.usuario,
                        UsuarioModificacion = objeto.usuario
                    };
                    var cambioinsertadoagregado = _repCronogramaCabeceraCambio.Insert(cambioagregar);
                    _unitOfWork.Commit();
                    //insertamos el detalle
                    foreach (var detalles in detallecambios)
                    {
                        detalles.IdCronogramaCabeceraCambio = cambioagregar.Id;
                        detalles.Version = versionactual;
                        detalles.Estado = true;
                        detalles.FechaCreacion = DateTime.Now;
                        detalles.FechaModificacion = DateTime.Now;
                        detalles.UsuarioCreacion = objeto.usuario;
                        detalles.UsuarioModificacion = objeto.usuario;
                        var detalleinsert = _repCronogramaDetalleCambio.Insert(detalles);
                        _unitOfWork.Commit();
                    }
                    //insertamos la nueva version
                    decimal totalmonto = nuevocronograma.Sum(w => w.Cuota.Value);

                    foreach (var itemcronograma in nuevocronograma)
                    {

                        CronogramaPagoDetalleFinal cuota = new CronogramaPagoDetalleFinal()
                        {
                            Id = 0,
                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                            NroCuota = itemcronograma.NroCuota,
                            NroSubCuota = itemcronograma.NroSubCuota,
                            FechaVencimiento = itemcronograma.FechaVencimiento,
                            TotalPagar = totalmonto,
                            Cuota = itemcronograma.Cuota,
                            Saldo = totalmonto - itemcronograma.Cuota,
                            Mora = itemcronograma.Mora,
                            MoraTarifario = itemcronograma.MoraTarifario,
                            Cancelado = itemcronograma.Cancelado,
                            TipoCuota = itemcronograma.TipoCuota,
                            Moneda = itemcronograma.Moneda,
                            TipoCambio = itemcronograma.TipoCambio,
                            Version = versionactual,
                            FechaPago = itemcronograma.FechaPago,
                            FechaDeposito = itemcronograma.FechaDeposito,
                            IdFormaPago = itemcronograma.IdFormaPago,
                            IdCuenta = itemcronograma.IdCuenta,
                            FechaPagoBanco = itemcronograma.FechaPagoBanco,
                            Enviado = itemcronograma.Enviado,
                            Observaciones = itemcronograma.Observaciones,
                            IdDocumentoPago = itemcronograma.IdDocumentoPago,
                            NroDocumento = itemcronograma.NroDocumento,
                            MonedaPago = itemcronograma.MonedaPago,
                            CuotaDolares = itemcronograma.CuotaDolares,
                            FechaProcesoPago = itemcronograma.FechaProcesoPago,
                            MontoPagado = itemcronograma.MontoPagado,
                            Aprobado = true,//Se convierte en true cuando aprueba los cambios
                            FechaProcesoPagoReal = itemcronograma.FechaProcesoPagoReal,
                            FechaEfectivoDisponible = itemcronograma.FechaEfectivoDisponible,
                            FechaIngresoEnCuenta = itemcronograma.FechaIngresoEnCuenta,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = objeto.usuario,
                            UsuarioModificacion = objeto.usuario,
                            FechaCompromiso1 = itemcronograma.FechaCompromiso1,
                            FechaCompromiso2 = itemcronograma.FechaCompromiso2,
                            FechaCompromiso3 = itemcronograma.FechaCompromiso3
                        };
                        if (itemcronograma.Cancelado == true)
                        {
                            cuota.IdTipoComprobante = comprobante.IdTipoComprobante == -1 ? null : comprobante.IdTipoComprobante;
                            cuota.NroDocumentoComprobante = comprobante.NroDocumentoComprobante;
                            cuota.NombreRazonSocial = comprobante.NombreRazonSocial;
                        }

                        new CronogramaPagoDetalleFinalService(_unitOfWork).Add(cuota);
                        totalmonto = totalmonto - itemcronograma.Cuota.Value;//actualizo el nuevo montototal
                    }

                    _repCronogramaPago.ActualizarCompromisoPago(matriculaCabeceraTemp.Id, objeto.usuario);

                    var insertado = this.InsertarPagoWebFinal(objeto.CodigoMatricula, objeto.MontoPago + resto, monedapagos, (float)objeto.TipoCambio, objeto.FormaPago, objeto.Documento, objeto.NroDocumento, objeto.NroCuenta, objeto.NroCheque, DateTime.Now, objeto.NroDeposito, objeto.usuario);


                    //valido si la cuota es 1-1
                    if (NroCuotaGlobal == 1 && NroSubCuotaGlobal == 1)// si esta pagando la primera cuota
                    {
                        var OriginalActualizado = this.ActualizarOriginal(versionactual, objeto.CodigoMatricula, objeto.usuario);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Guarda el apgo hecho de una cuota
        /// </summary>
        /// <param name="CodigoMatricula"></param>
        /// <param name="Version"></param>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        private int ActualizarOriginal(int Version, string CodigoMatricula, string Usuario)
        {
            var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
            var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
            var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(y => y.CodigoMatricula == CodigoMatricula).FirstOrDefault();
            //Primero Eliminadmos el Actual (Original)
            var _repCronogramaPagoDetalleOriginal = _unitOfWork.CronogramaPagoDetalleOriginalRepository;
            _repCronogramaPagoDetalleOriginal.Delete(_repCronogramaPagoDetalleOriginal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id), Usuario);
            _unitOfWork.Commit();

            //Ahora Insertamos el nuevo original del actual final
            var listaActual = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Version == Version).ToList();
            List<TCronogramaPagoDetalleOriginal> listaNuevosOriginales = new List<TCronogramaPagoDetalleOriginal>();
            foreach (var item in listaActual)
            {
                TCronogramaPagoDetalleOriginal nuevoOriginal = new TCronogramaPagoDetalleOriginal();
                nuevoOriginal.IdMatriculaCabecera = item.IdMatriculaCabecera;
                nuevoOriginal.NroCuota = item.NroCuota.Value;
                nuevoOriginal.NroSubCuota = item.NroSubCuota.Value;
                nuevoOriginal.FechaVencimiento = item.FechaVencimiento.Value;
                nuevoOriginal.TotalPagar = item.TotalPagar.Value;
                nuevoOriginal.Cuota = item.Cuota.Value;
                nuevoOriginal.Saldo = item.Saldo.Value;
                nuevoOriginal.Cancelado = item.Cancelado.Value;
                nuevoOriginal.Monto = null;
                nuevoOriginal.TipoCuota = item.TipoCuota;
                nuevoOriginal.Moneda = item.Moneda;
                nuevoOriginal.TipocCambio = item.TipoCambio;
                nuevoOriginal.FechaCreacion = DateTime.Now;
                nuevoOriginal.FechaModificacion = DateTime.Now;
                nuevoOriginal.UsuarioCreacion = Usuario;
                nuevoOriginal.UsuarioModificacion = Usuario;
                nuevoOriginal.Estado = true;
                listaNuevosOriginales.Add(nuevoOriginal);
            }
            _repCronogramaPagoDetalleOriginal.Insert(listaNuevosOriginales);
            _unitOfWork.Commit();
            return 1;
        }


        /// <summary>
        /// Guarda el apgo hecho de una cuota
        /// </summary>
        /// <param name="CodigoMatricula"></param>
        /// <param name="Monto"></param>
        /// <param name="Moneda"></param>
        /// <param name="TipoCambio"></param>
        /// <param name="RUC"></param>
        /// <param name="FormaCobro"></param>
        /// <param name="Documento"></param>
        /// <param name="SerieNumero"></param>
        /// <param name="NroCta"></param>
        /// <param name="NroRefCheq"></param>
        /// <param name="FechaDocumento"></param>
        /// <param name="NroDeposito"></param>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        private int InsertarPagoWebFinal(string CodigoMatricula, decimal Monto, string Moneda, float TipoCambio , int FormaCobro, int Documento, string SerieNumero, int? NroCta, string NroRefCheq, DateTime FechaDocumento, string NroDeposito, string Usuario)
        {
            var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
            var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(y => y.CodigoMatricula == CodigoMatricula).FirstOrDefault();
            //Pagos Final Pendiente Insert
            TPagoFinal PagoFinal = new TPagoFinal();
            PagoFinal.Id = 0;
            PagoFinal.IdMatriculaCabecera = matriculaCabeceraTemp.Id;
            PagoFinal.Monto = Monto;
            PagoFinal.Moneda = Moneda;
            PagoFinal.TipoCambio = TipoCambio;
            PagoFinal.Ruc = null;
            PagoFinal.IdFormaPago = FormaCobro;
            PagoFinal.SerieNumero = SerieNumero;
            PagoFinal.IdCuentaCorriente = NroCta;
            PagoFinal.NroRefCheque = NroRefCheq;
            PagoFinal.FechaDocumento = FechaDocumento;
            PagoFinal.NroDeposito = NroDeposito;
            PagoFinal.FechaPago = DateTime.Now;
            PagoFinal.IdDocumentoPago = Documento;
            PagoFinal.EstadoPago = true;

            PagoFinal.Estado = true;
            PagoFinal.FechaCreacion = DateTime.Now;
            PagoFinal.FechaModificacion = DateTime.Now;
            PagoFinal.UsuarioCreacion = Usuario;
            PagoFinal.UsuarioModificacion = Usuario;

            var _repPagoFinal = _unitOfWork.PagoFinalRepository;
            _repPagoFinal.Insert(PagoFinal);
            _unitOfWork.Commit();

            //Actualiza Estado Matricula

            matriculaCabeceraTemp.EstadoMatricula = "matriculado";
            matriculaCabeceraTemp.Estado = true;
            matriculaCabeceraTemp.FechaModificacion = DateTime.Now;
            matriculaCabeceraTemp.UsuarioModificacion = Usuario;
            //Actualiza la fecha de matricula
            var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
            int empresaPaga, primerPago;


            primerPago = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Estado == true && w.Cancelado == true && w.Aprobado == true).Count();
            empresaPaga = _repMatriculaCabecera.GetBy(w => w.Id == matriculaCabeceraTemp.Id && w.Estado == true && w.EmpresaPaga == "SI").Count();
            if (primerPago == 0 && empresaPaga == 0)
            {
                matriculaCabeceraTemp.FechaMatricula = FechaDocumento;
            }
            _repMatriculaCabecera.Update(matriculaCabeceraTemp);
            _unitOfWork.Commit();

            return 1;
        }




        /// Autor: Margiory Ramirez
        /// Fecha: 18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Agrega las tasas academicas con los datos consignados desde la vista.
        /// </summary>
        /// <returns>Json/returns>


        public object AgregarTasasAcademicas(TasasAcademicasDetalleDTO Json)
        {
            try
            {
                if (Json != null)
                {
                    var _repTasasAcademicas = _unitOfWork.OrigenRepository;
                    return (_repTasasAcademicas.AgregarTasasAcademicasProcedimiento(
                        Json.CodigoMatricula,
                        Json.IdConcepto,
                        Json.Monto,
                        Json.Moneda,
                        Json.Usuario,
                        Json.FechaPago)
                        );
                }
                else
                {
                    return (new { Message = "Se Registro Correctamente" });
                }

            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }



        /// Autor: Margiory Ramirez
        /// Fecha: 18/01/2023
        /// <summary>
        /// Actualiza lod coucmentos de una matricula
        /// </summary>
        /// <returns>Json/returns>


        public object ActualizarEntregaControlDocs(ListaControlDocumentosDTO Json)
        {
            try
            {
                var _repControlDocAlumnoRep = _unitOfWork.ControlDocAlumnoRepository;
                var _repControlDocRep = _unitOfWork.ControlDocRepository;
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == Json.matricula, x => new { x.Id }).FirstOrDefault();
                foreach (var document in Json.ListaDocumentos)
                {
                    if (document.Ingresar)
                    {
                        var Lista = _repControlDocAlumnoRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).ToList();
                        if (Lista.Count() == 0)
                        {
                            ControlDocAlumno documentoAlumno = new ControlDocAlumno();
                            documentoAlumno.IdMatriculaCabecera = matriculaCabeceraTemp.Id;
                            documentoAlumno.IdCriterioCalificacion = 0;
                            documentoAlumno.FechaCreacion = DateTime.Now;
                            documentoAlumno.FechaModificacion = DateTime.Now;
                            documentoAlumno.UsuarioCreacion = document.usuario;
                            documentoAlumno.UsuarioModificacion = document.usuario;
                            documentoAlumno.Estado = true;
                            documentoAlumno.ComisionableEditable = "Ninguno";
                            documentoAlumno.MontoComisionable = 0;
                            documentoAlumno.PagadoComisionable = 0;
                            _repControlDocAlumnoRep.Add(documentoAlumno);
                            _unitOfWork.Commit();
                        }

                        var listaActualizarControlDocumentos = _repControlDocRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.IdCriterioDoc == document.IdCriterioDocs);

                        foreach (var item in listaActualizarControlDocumentos)
                        {
                            item.EstadoDocumento = true;
                            item.FechaModificacion = DateTime.Now;
                            item.UsuarioModificacion = document.usuario;
                            _repControlDocRep.Update(item);
                            _unitOfWork.Commit();
                        }
                    }
                    else
                    {
                        var listaEliminarControlDocumentos = _repControlDocRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.IdCriterioDoc == document.IdCriterioDocs);
                        foreach (var item in listaEliminarControlDocumentos)
                        {
                            item.EstadoDocumento = false;
                            item.FechaModificacion = DateTime.Now;
                            item.UsuarioModificacion = document.usuario;
                            _repControlDocRep.Update(item);
                            _unitOfWork.Commit();
                        }
                    };
                }

                return (new { Message = "Se Actualizo Correctamente" });
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }


        /// Autor: Margiory Ramirez
        /// Fecha: 18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// onbtiene los datos de tasas academicas con sus detalles y precios
        /// </summary>
        /// <returns>Json/returns>


        public object ObtenerDetalleTasasAcademicas(Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    var _repTasasAcademicas = _unitOfWork.OrigenRepository;
                    return (_repTasasAcademicas.ObtenerTarifariosDetallesMonto(Valor["valor"]));
                }
                else
                {
                    return (new { Message = "Exitoso" });
                }
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }



        /// Autor: Margiory Ramirez.
        /// Fecha: 21/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los documentos de una matricula en base a su codigo y su tipo de modalidad
        /// </summary>
        /// <returns>Json/returns>


        public object ObtenerDocumentosMatricula(string CodigoMatricula, int IdPEspecifico)
        {
            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                var _repControlDoc = _unitOfWork.ControlDocRepository;
                var listaDocumentoAlumno = _repControlDoc.ObtenerDocumentosPorMatriculaCabecera(CodigoMatricula);
                var _repPEspecifico = _unitOfWork.PEspecificoRepository;
                var modalidad = _repPEspecifico.GetBy(x => x.Id == IdPEspecifico, x => new { x.Tipo }).FirstOrDefault();
                var _repCriterioDoc = _unitOfWork.CriterioDocRepository;
                List<CriterioDocDTO> listaDocumentos = new List<CriterioDocDTO>();
                if (modalidad.Tipo == "Presencial")
                {
                    var tempData = _repCriterioDoc.GetBy(x => x.ModalidadPresencial == true, x => new { IdCriterioDocs = x.Id, NombreDocumento = x.Nombre });
                    foreach (var item in tempData)
                    {
                        var temp = new CriterioDocDTO()
                        {
                            IdCriterioDocs = item.IdCriterioDocs,
                            NombreDocumento = item.NombreDocumento
                        };
                        listaDocumentos.Add(temp);
                    }
                }
                if (modalidad.Tipo == "Online Asincronica")
                {
                    var tempData = _repCriterioDoc.GetBy(x => x.ModalidadAonline == true, x => new { IdCriterioDocs = x.Id, NombreDocumento = x.Nombre });
                    foreach (var item in tempData)
                    {
                        var temp = new CriterioDocDTO()
                        {
                            IdCriterioDocs = item.IdCriterioDocs,
                            NombreDocumento = item.NombreDocumento
                        };
                        listaDocumentos.Add(temp);
                    }
                }
                if (modalidad.Tipo == "Online Sincronica")
                {
                    var tempData = _repCriterioDoc.GetBy(x => x.ModalidadOnline == true, x => new { IdCriterioDocs = x.Id, NombreDocumento = x.Nombre });
                    foreach (var item in tempData)
                    {
                        var temp = new CriterioDocDTO()
                        {
                            IdCriterioDocs = item.IdCriterioDocs,
                            NombreDocumento = item.NombreDocumento
                        };
                        listaDocumentos.Add(temp);
                    }
                }
                for (int i = 0; i < listaDocumentos.Count(); i++)
                {
                    listaDocumentos[i].Estado = 0;
                    for (int j = 0; j < listaDocumentoAlumno.Count(); j++)
                    {
                        if (listaDocumentos[i].IdCriterioDocs == listaDocumentoAlumno[j].IdCriterioDocs)
                        {
                            listaDocumentos[i].Estado = 1;
                            break;
                        }
                    }
                }
                return (new { listaDocumentos, listaDocumentoAlumno });
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }
        /// Tipo Función: POST 
        /// Autor: Lisbeth, Miguel
        /// Fecha: 28/04/2021
        /// Versión: 3.0
        /// <summary>
        /// Guarda todos los cambios del cronograma asi como sus detalles y también los log de este
        /// modificacion : se agrego el campo  UsuarioCoordinadorAcademico a la tabla de cronograma de la matricula
        /// </summary>
        /// <returns>Json</returns>


        public object GuardarCronograma(CronogramaModificadoDTO Json)
        {

            try
            {
                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                var _repCronogramaDetalleCambio = _unitOfWork.CronogramaDetalleCambioRepository;
                var _repCronogramaCabeceraCambio = _unitOfWork.CronogramaCabeceraCambioRepository;
                var _repCronogramaPagoDetalleModLogFinal = _unitOfWork.CronogramaPagoDetalleModLogFinalRepository;
                var _repPersonal = _unitOfWork.PersonalRepository;
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;

                var IdMatriculaCabeceraSincronizacion = 0;

                using (TransactionScope scope = new TransactionScope())
                {
                    var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(w => w.CodigoMatricula == Json.Objeto.CodigoMatricula).FirstOrDefault();
                    var versionAprobadoTemp = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                    var cronogramaActual = _repCronogramaPagoDetalleFinal.GetBy( x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == versionAprobadoTemp.Version, x => new { x.Id, x.Cancelado, FlagCancelado = x.Cancelado, x.NroCuota, x.NroSubCuota,x.TipoCuota, x.FechaVencimiento, x.TotalPagar, x.Cuota, x.Mora, x.MoraTarifario,x.Saldo, x.Moneda, x.MontoPagado, x.FechaPago, x.IdFormaPago, x.IdCuenta,x.FechaPagoBanco, x.Enviado, x.Observaciones, x.IdDocumentoPago, x.NroDocumento,x.MonedaPago, x.TipoCambio, x.CuotaDolares, x.FechaProcesoPago, x.Version,  x.FechaDeposito, x.FechaEfectivoDisponible, x.FechaIngresoEnCuenta,x.FechaProcesoPagoReal, x.FechaCompromiso1, x.FechaCompromiso2,  x.FechaCompromiso3, x.UsuarioCoordinadorAcademico,x.NroDocumentoComprobante, x.IdTipoComprobante,x.NombreRazonSocial,x.Observacion }).OrderBy(x => x.NroCuota).ThenBy(x => x.NroSubCuota).ToList();
                    IdMatriculaCabeceraSincronizacion = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula.Equals(matriculaCabeceraTemp.CodigoMatricula)).Select(x => x.Id).FirstOrDefault();
                    var ExisteVersionNoAprobada = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Aprobado == false).ToList();
                    if (ExisteVersionNoAprobada.Count > 0)
                    {
                        return ("Existe Versiones sin Aprobar");
                    }
                    else
                    {
                        //Insert Ultima Version

                        //inserto el cronograma como quedo despues de los cambios con la version +1
                        decimal totalMonto = Json.ListaCronograma.Sum(x => x.Cuota);
                        var versionNueva = cronogramaActual.FirstOrDefault().Version + 1;
                        string _UsuarioCoordinadorAcademico = "";
                        foreach (var itemCronograma in Json.ListaCronograma)
                        {
                            var existe = cronogramaActual.Where(w => w.Id == itemCronograma.Id).FirstOrDefault();
                            Nullable<decimal> _montoPagado, _tipoCambio, _cuotaDolares, _moraTarifario;
                            Nullable<DateTime> _fechaPago, _fechaPagoBanco, _fechaProcesoPago, _fechaDeposito, _fechaEfectivoDisponible, _fechaIngresoEnCuenta, _fechaProcesoPagoReal, _fechaCompromiso1, _fechaCompromiso2, _fechaCompromiso3;
                            Nullable<int> _idFormaPago, _idCuenta, _idDocumentoPago,_idTipoComprobate;
                            Nullable<bool> _enviado;
                            string _observaciones, _nroDocumento, _monedaPago,_NroDocumentoComprobante,_NombreRazonSocial,_Observacion;

                            if (existe != null)
                            {
                                _montoPagado = existe.MontoPagado;
                                _tipoCambio = existe.TipoCambio;
                                _cuotaDolares = existe.CuotaDolares;
                                _fechaPago = existe.FechaPago;
                                _fechaPagoBanco = existe.FechaPagoBanco;
                                _fechaProcesoPago = existe.FechaProcesoPago;
                                _idFormaPago = existe.IdFormaPago;
                                _idCuenta = existe.IdCuenta;
                                _idDocumentoPago = existe.IdDocumentoPago;
                                _observaciones = existe.Observaciones;
                                _nroDocumento = existe.NroDocumento;
                                _monedaPago = existe.MonedaPago;
                                _fechaDeposito = existe.FechaDeposito;
                                _fechaEfectivoDisponible = existe.FechaEfectivoDisponible;
                                _fechaIngresoEnCuenta = existe.FechaIngresoEnCuenta;
                                _fechaProcesoPagoReal = existe.FechaProcesoPagoReal;
                                _moraTarifario = existe.MoraTarifario;
                                _fechaCompromiso1 = existe.FechaCompromiso1;
                                _fechaCompromiso2 = existe.FechaCompromiso2;
                                _fechaCompromiso3 = existe.FechaCompromiso3;
                                _UsuarioCoordinadorAcademico = existe.UsuarioCoordinadorAcademico;

                                _NroDocumentoComprobante = existe.NroDocumentoComprobante==null?"": existe.NroDocumentoComprobante;
                                _Observacion = existe.Observacion == null ? "" : existe.Observacion;
                                _NombreRazonSocial = existe.NombreRazonSocial == null ? "" : existe.NombreRazonSocial;
                                _idTipoComprobate = existe.IdTipoComprobante;


                            }
                            else
                            {

                                _montoPagado = 0;
                                _tipoCambio = null;
                                _cuotaDolares = null;
                                _fechaPago = null;
                                _fechaPagoBanco = null;
                                _fechaProcesoPago = null;
                                _idFormaPago = null;
                                _idCuenta = null;
                                _idDocumentoPago = null;
                                _enviado = false;
                                _observaciones = null;
                                _nroDocumento = null;
                                _monedaPago = null;
                                _fechaDeposito = null;
                                _fechaEfectivoDisponible = null;
                                _fechaIngresoEnCuenta = null;
                                _fechaProcesoPagoReal = null;
                                _moraTarifario = null;
                                _fechaCompromiso1 = null;
                                _fechaCompromiso2 = null;
                                _fechaCompromiso3 = null;

                                _NroDocumentoComprobante = "";
                                _Observacion = "";
                                _NombreRazonSocial = "";
                                _idTipoComprobate = null;
                            }

                            CronogramaPagoDetalleFinal cuota = new CronogramaPagoDetalleFinal()
                            {
                                Id = 0,
                                IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                NroCuota = itemCronograma.NroCuota,
                                NroSubCuota = itemCronograma.NroSubCuota,
                                FechaVencimiento = itemCronograma.FechaVencimiento,
                                TotalPagar = totalMonto,
                                Cuota = itemCronograma.Cuota,
                                Saldo = totalMonto - itemCronograma.Cuota,
                                Mora = itemCronograma.Mora,
                                MoraTarifario = _moraTarifario,
                                FechaCompromiso1 = _fechaCompromiso1,
                                FechaCompromiso2 = _fechaCompromiso2,
                                FechaCompromiso3 = _fechaCompromiso3,
                                Cancelado = itemCronograma.Cancelado,
                                TipoCuota = itemCronograma.TipoCuota,
                                Moneda = itemCronograma.Moneda,
                                TipoCambio = _tipoCambio,
                                Version = versionNueva,
                                FechaPago = _fechaPago,
                                FechaDeposito = _fechaDeposito,
                                IdFormaPago = _idFormaPago,
                                IdCuenta = _idCuenta,
                                FechaPagoBanco = _fechaPagoBanco,
                                Enviado = itemCronograma.Enviado,
                                Observaciones = _observaciones,
                                IdDocumentoPago = _idDocumentoPago,
                                NroDocumento = _nroDocumento,
                                MonedaPago = _monedaPago,
                                FechaProcesoPago = _fechaProcesoPago,
                                MontoPagado = _montoPagado,
                                Aprobado = false,//Se convierte en true cuando aprueba los cambios
                                FechaEfectivoDisponible = _fechaEfectivoDisponible,
                                FechaIngresoEnCuenta = _fechaIngresoEnCuenta,
                                FechaProcesoPagoReal = _fechaProcesoPagoReal,
                                Estado = true,
                                UsuarioCreacion = Json.Usuario + "-V5",
                                UsuarioModificacion = Json.Usuario + "-V5",
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCoordinadorAcademico = _UsuarioCoordinadorAcademico,

                                NroDocumentoComprobante = _NroDocumentoComprobante==""?null: _NroDocumentoComprobante,
                                Observacion = _Observacion == "" ? null : _Observacion,
                                NombreRazonSocial = _NombreRazonSocial == "" ? null : _NombreRazonSocial,
                                IdTipoComprobante =_idTipoComprobate
                            };
                            _repCronogramaPagoDetalleFinal.Add(cuota);
                            _unitOfWork.Commit();
                            totalMonto = totalMonto - itemCronograma.Cuota;//actualizo el nuevo montototal
                        }

                        //Insert Cambios Log
                        var listaCambiosOrdenAgrupado = (from m in Json.ListaCambiosOrden
                                                         group m by new { m.Orden } into grupo
                                                         select new { g = grupo.Key, l = grupo }).ToList();

                        List<CronogramaPagoDetalleModLogFinal> listaLog = new List<CronogramaPagoDetalleModLogFinal>();//aqui se guarda el log de los cambios
                        foreach (var orden in listaCambiosOrdenAgrupado.OrderBy(w => w.g.Orden))
                        {
                            if (orden.l.FirstOrDefault().TipoCambio == "Fraccion" || orden.l.FirstOrDefault().TipoCambio == "Fraccion Reemplazado")
                            {
                                //ttiposcambios=4E186D5A-0076-48E0-AF54-01210B243BBD
                                //creo un nuevo tcambios con tipocambio de arriba

                                CronogramaCabeceraCambio CambioFraccion = new CronogramaCabeceraCambio()
                                {
                                    Id = 0,
                                    IdCronogramaTipoModificacion = 1,
                                    SolicitadoPor = Json.Objeto.SolicitadoPorId,
                                    AprobadoPor = Json.Objeto.AprobadoPorId,
                                    Aprobado = false,
                                    Cancelado = false,
                                    Observacion = Json.Objeto.Comentario,
                                    Estado = true,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };

                                var CambioInsertadoFraccion = _repCronogramaCabeceraCambio.Add(CambioFraccion);
                                _unitOfWork.Commit();

                                //ahora insertamos los detalles
                                var idPadre = orden.l.FirstOrDefault(w => w.TipoCambio == "Fraccion").id;//el id del padre
                                var padre = Json.ListaCronograma.Where(w => w.Id == idPadre).FirstOrDefault();
                                if (padre == null)
                                {
                                    //nunca se debe dar ya esta validado por javascript
                                    //el padre fue eliminado por ende los hijos tamcpo deben existir
                                }
                                else
                                {
                                    CronogramaDetalleCambio padreTemp = new CronogramaDetalleCambio()
                                    {
                                        Id = 0,
                                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                        IdCronogramaCabeceraCambio = CambioInsertadoFraccion.Id,
                                        NroCuota = padre.NroCuota,
                                        NroSubcuota = padre.NroSubCuota,
                                        FechaVencimiento = padre.FechaVencimiento,
                                        Cuota = padre.Cuota,
                                        Mora = padre.Mora,
                                        Moneda = padre.Moneda,
                                        Version = versionNueva.Value,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = Json.Usuario,
                                        UsuarioModificacion = Json.Usuario

                                    };
                                    _repCronogramaDetalleCambio.Add(padreTemp);
                                    _unitOfWork.Commit();

                                    //INSERTO EN LOG
                                    if (listaLog.Where(w => w.NroCuota == padre.NroCuota && w.NroSubCuota == padre.NroSubCuota).FirstOrDefault() != null)//NUCNA DEBERIA ENTRAR AQUI PORQ ES AGREGADO SOLO ESE CAMBIO
                                    {
                                        var antiguo = listaLog.Where(w => w.NroCuota == padre.NroCuota && w.NroSubCuota == padre.NroSubCuota).FirstOrDefault();
                                        antiguo.MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA (" + padre.NroCuota + "," + padre.NroSubCuota + "), " + padre.Cuota;
                                    }
                                    else//es nuevo
                                    {
                                        CronogramaPagoDetalleModLogFinal log = new CronogramaPagoDetalleModLogFinal()
                                        {
                                            Id = 0,
                                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                            Fecha = DateTime.Now,
                                            NroCuota = padre.NroCuota,
                                            NroSubCuota = padre.NroSubCuota,
                                            FechaVencimiento = padre.FechaVencimiento,
                                            TotalPagar = padre.TotalPagar,
                                            Cuota = padre.Cuota,
                                            Mora = padre.Mora,
                                            MontoPagado = 0,
                                            Saldo = padre.Saldo,
                                            Cancelado = padre.Cancelado,
                                            TipoCuota = padre.TipoCuota,
                                            Moneda = padre.Moneda,
                                            FechaPago = null,
                                            IdFormaPago = null,
                                            FechaPagoBanco = null,
                                            Ultimo = false,
                                            IdDocumentoPago = null,
                                            NroDocumento = null,
                                            MonedaPago = null,
                                            TipoCambio = null,
                                            MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA  (" + padre.NroCuota + "," + padre.NroSubCuota + "), MONTO " + padre.Cuota,
                                            FechaProcesoPago = null,
                                            EstadoPrimerLog = null
                                        };
                                        listaLog.Add(log);
                                        _unitOfWork.Commit();

                                        var fechaoriginal = cronogramaActual.Where(w => w.NroCuota == padre.NroCuota && w.NroSubCuota == padre.NroSubCuota).FirstOrDefault();
                                        string fechaoriginaldate = fechaoriginal != null ? fechaoriginal.FechaVencimiento.Value.ToString("dd/MM/yyyy") : "";
                                        if (fechaoriginaldate != padre.FechaVencimiento.ToString("dd/MM/yyyy"))
                                        {
                                            log = new CronogramaPagoDetalleModLogFinal()
                                            {
                                                Id = 0,
                                                IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                                Fecha = DateTime.Now,
                                                NroCuota = padre.NroCuota,
                                                NroSubCuota = padre.NroSubCuota,
                                                FechaVencimiento = padre.FechaVencimiento,
                                                TotalPagar = padre.TotalPagar,
                                                Cuota = padre.Cuota,
                                                Mora = padre.Mora,
                                                MontoPagado = 0,
                                                Saldo = padre.Saldo,
                                                Cancelado = padre.Cancelado,
                                                TipoCuota = padre.TipoCuota,
                                                Moneda = padre.Moneda,
                                                FechaPago = null,
                                                IdFormaPago = null,
                                                FechaPagoBanco = null,
                                                Ultimo = false,
                                                IdDocumentoPago = null,
                                                NroDocumento = null,
                                                MonedaPago = null,
                                                TipoCambio = null,
                                                MensajeSistema = "FECHA DE CUOTA (" + padre.NroCuota + "," + padre.NroSubCuota + ") HA VARIADO DE " + fechaoriginaldate + " A " + padre.FechaVencimiento.ToString("dd/MM/yyyy HH:mm:ss"),
                                                FechaProcesoPago = null,
                                                EstadoPrimerLog = null
                                            };

                                            listaLog.Add(log);
                                            _unitOfWork.Commit();
                                        }
                                    }

                                    foreach (var hijos in orden.l.Where(w => w.TipoCambio == "Fraccion").OrderBy(w => w.Cuota).ThenBy(w => w.SubCuota))
                                    {
                                        var hijo = Json.ListaCronograma.Where(w => w.NroCuota == hijos.Cuota && w.NroSubCuota == hijos.SubCuota).FirstOrDefault();
                                        CronogramaDetalleCambio hijoTemp = new CronogramaDetalleCambio()
                                        {
                                            Id = 0,
                                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                            IdCronogramaCabeceraCambio = CambioInsertadoFraccion.Id,
                                            NroCuota = hijo.NroCuota,
                                            NroSubcuota = hijo.NroSubCuota,
                                            FechaVencimiento = hijo.FechaVencimiento,
                                            Cuota = hijo.Cuota,
                                            Mora = hijo.Mora,
                                            Moneda = hijo.Moneda,
                                            Version = versionNueva.Value,
                                            Estado = true,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            UsuarioCreacion = Json.Usuario,
                                            UsuarioModificacion = Json.Usuario

                                        };
                                        _repCronogramaDetalleCambio.Add(hijoTemp);
                                        _unitOfWork.Commit();

                                        //INSERTO EN LOG
                                        if (listaLog.Where(w => w.NroCuota == hijo.NroCuota && w.NroSubCuota == hijo.NroSubCuota).FirstOrDefault() != null)//NUCNA DEBERIA ENTRAR AQUI PORQ ES AGREGADO SOLO ESE CAMBIO
                                        {
                                            var antiguo = listaLog.Where(w => w.NroCuota == hijo.Cuota && w.NroSubCuota == hijo.NroSubCuota).FirstOrDefault();
                                            antiguo.MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA (" + hijo.NroCuota + "," + hijo.NroSubCuota + "),  " + hijo.Cuota;
                                        }
                                        else//es nuevo
                                        {
                                            CronogramaPagoDetalleModLogFinal log = new CronogramaPagoDetalleModLogFinal()
                                            {
                                                Id = 0,
                                                IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                                Fecha = DateTime.Now,
                                                NroCuota = hijo.NroCuota,
                                                NroSubCuota = hijo.NroSubCuota,
                                                FechaVencimiento = hijo.FechaVencimiento,
                                                TotalPagar = hijo.TotalPagar,
                                                Cuota = hijo.Cuota,
                                                Mora = hijo.Mora,
                                                MontoPagado = 0,
                                                Saldo = hijo.Saldo,
                                                Cancelado = hijo.Cancelado,
                                                TipoCuota = hijo.TipoCuota,
                                                Moneda = hijo.Moneda,
                                                FechaPago = null,
                                                IdFormaPago = null,
                                                FechaPagoBanco = null,
                                                Ultimo = false,
                                                IdDocumentoPago = null,
                                                NroDocumento = null,
                                                MonedaPago = null,
                                                TipoCambio = null,
                                                MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA  (" + hijo.NroCuota + "," + hijo.NroSubCuota + "), MONTO " + hijo.Cuota,
                                                FechaProcesoPago = null,
                                                EstadoPrimerLog = null
                                            };
                                            listaLog.Add(log);
                                            _unitOfWork.Commit();
                                            var fechaoriginal = cronogramaActual.Where(w => w.NroCuota == padre.NroCuota && w.NroSubCuota == padre.NroSubCuota).FirstOrDefault();
                                            string fechaoriginaldate = fechaoriginal != null ? fechaoriginal.FechaVencimiento.Value.ToString("dd/MM/yyyy") : "";
                                            if (fechaoriginaldate != hijo.FechaVencimiento.ToString("dd/MM/yyyy"))
                                            {
                                                log = new CronogramaPagoDetalleModLogFinal()
                                                {
                                                    Id = 0,
                                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                                    Fecha = DateTime.Now,
                                                    NroCuota = hijo.NroCuota,
                                                    NroSubCuota = hijo.NroSubCuota,
                                                    FechaVencimiento = hijo.FechaVencimiento,
                                                    TotalPagar = hijo.TotalPagar,
                                                    Cuota = hijo.Cuota,
                                                    Mora = hijo.Mora,
                                                    MontoPagado = 0,
                                                    Saldo = hijo.Saldo,
                                                    Cancelado = hijo.Cancelado,
                                                    TipoCuota = hijo.TipoCuota,
                                                    Moneda = hijo.Moneda,
                                                    FechaPago = null,
                                                    IdFormaPago = null,
                                                    FechaPagoBanco = null,
                                                    Ultimo = false,
                                                    IdDocumentoPago = null,
                                                    NroDocumento = null,
                                                    MonedaPago = null,
                                                    TipoCambio = null,
                                                    MensajeSistema = "FECHA DE CUOTA (" + hijo.NroCuota + "," + hijo.NroSubCuota + ") HA VARIADO DE " + fechaoriginaldate + " A " + hijo.FechaVencimiento.ToString("dd/MM/yyyy HH:mm:ss"),
                                                    FechaProcesoPago = null,
                                                    EstadoPrimerLog = null
                                                };

                                                listaLog.Add(log);
                                                _unitOfWork.Commit();
                                            }

                                        }


                                    }
                                    foreach (var hijos in orden.l.Where(w => w.TipoCambio == "Fraccion Reemplazado").OrderBy(w => w.Cuota).ThenBy(w => w.SubCuota))
                                    {
                                        var hijo = Json.ListaCronograma.Where(w => w.Id == hijos.id).FirstOrDefault();

                                        CronogramaDetalleCambio hijoTemp = new CronogramaDetalleCambio()
                                        {
                                            Id = 0,
                                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                            IdCronogramaCabeceraCambio = CambioInsertadoFraccion.Id,
                                            NroCuota = hijo.NroCuota,
                                            NroSubcuota = hijo.NroSubCuota,
                                            FechaVencimiento = hijo.FechaVencimiento,
                                            Cuota = hijo.Cuota,
                                            Mora = hijo.Mora,
                                            Moneda = hijo.Moneda,
                                            Version = versionNueva.Value,
                                            Estado = true,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            UsuarioCreacion = Json.Usuario,
                                            UsuarioModificacion = Json.Usuario

                                        };
                                        _repCronogramaDetalleCambio.Add(hijoTemp);
                                        _unitOfWork.Commit();

                                    }
                                }

                            }

                            if (orden.l.FirstOrDefault().TipoCambio == "Agregar")
                            {
                                //ttiposcambios=A62C8DCD-7AF1-49B1-8A23-C359E7A0D9DB
                                //creo un nuevo tcambios con tipocambio de arriba

                                CronogramaCabeceraCambio CambioAgregar = new CronogramaCabeceraCambio()
                                {
                                    Id = 0,
                                    IdCronogramaTipoModificacion = 9,
                                    SolicitadoPor = Json.Objeto.SolicitadoPorId,
                                    AprobadoPor = Json.Objeto.AprobadoPorId,
                                    Aprobado = false,
                                    Cancelado = false,
                                    Observacion = Json.Objeto.Comentario,
                                    Estado = true,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                var cambioinsertadoagregado = _repCronogramaCabeceraCambio.Add(CambioAgregar);
                                _unitOfWork.Commit();
                                var agregado = Json.ListaCronograma.Where(w => w.NroCuota == orden.l.FirstOrDefault().Cuota && w.NroSubCuota == orden.l.FirstOrDefault().SubCuota).FirstOrDefault();

                                CronogramaDetalleCambio agregadoTemp = new CronogramaDetalleCambio()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    IdCronogramaCabeceraCambio = cambioinsertadoagregado.Id,
                                    NroCuota = agregado.NroCuota,
                                    NroSubcuota = agregado.NroSubCuota,
                                    FechaVencimiento = agregado.FechaVencimiento,
                                    Cuota = agregado.Cuota,
                                    Mora = agregado.Mora,
                                    Moneda = agregado.Moneda,
                                    Version = versionNueva.Value,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario

                                };
                                _repCronogramaDetalleCambio.Add(agregadoTemp);
                                _unitOfWork.Commit();

                                //INSERTO EN LOG
                                if (listaLog.Where(w => w.NroCuota == agregado.NroCuota && w.NroSubCuota == agregado.NroSubCuota).FirstOrDefault() != null)//NUCNA DEBERIA ENTRAR AQUI PORQ ES AGREGADO SOLO ESE CAMBIO
                                {
                                    var antiguo = listaLog.Where(w => w.NroCuota == agregado.NroCuota && w.NroSubCuota == agregado.NroSubCuota).FirstOrDefault();
                                    antiguo.MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA (" + agregado.NroCuota + "," + agregado.NroSubCuota + "), MONTO " + agregado.Cuota + " Y FECHA " + agregado.FechaVencimiento;
                                }
                                else//es nuevo
                                {
                                    CronogramaPagoDetalleModLogFinal log = new CronogramaPagoDetalleModLogFinal()
                                    {
                                        Id = 0,
                                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                        Fecha = DateTime.Now,
                                        NroCuota = agregado.NroCuota,
                                        NroSubCuota = agregado.NroSubCuota,
                                        FechaVencimiento = agregado.FechaVencimiento,
                                        TotalPagar = agregado.TotalPagar,
                                        Cuota = agregado.Cuota,
                                        Mora = agregado.Mora,
                                        MontoPagado = 0,
                                        Saldo = agregado.Saldo,
                                        Cancelado = agregado.Cancelado,
                                        TipoCuota = agregado.TipoCuota,
                                        Moneda = agregado.Moneda,
                                        FechaPago = null,
                                        IdFormaPago = null,
                                        FechaPagoBanco = null,
                                        Ultimo = false,
                                        IdDocumentoPago = null,
                                        NroDocumento = null,
                                        MonedaPago = null,
                                        TipoCambio = null,
                                        MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA  (" + agregado.NroCuota + "," + agregado.NroSubCuota + "), MONTO " + agregado.Cuota + " Y FECHA " + agregado.FechaVencimiento,
                                        FechaProcesoPago = null,
                                        EstadoPrimerLog = null
                                    };
                                    listaLog.Add(log);
                                    _unitOfWork.Commit();
                                }
                            }

                            if (orden.l.FirstOrDefault().TipoCambio == "Eliminado")
                            {
                                //ttiposcambios=5DF6C4B0-23FC-4F7F-9E22-CAE3DA4F7FC2
                                //creo un nuevo tcambios con tipocambio de arriba

                                CronogramaCabeceraCambio CambioEliminar = new CronogramaCabeceraCambio()
                                {
                                    Id = 0,
                                    IdCronogramaTipoModificacion = 11,
                                    SolicitadoPor = Json.Objeto.SolicitadoPorId,
                                    AprobadoPor = Json.Objeto.AprobadoPorId,
                                    Aprobado = false,
                                    Cancelado = false,
                                    Observacion = Json.Objeto.Comentario,
                                    Estado = true,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                var cambioinsertadoeliminado = _repCronogramaCabeceraCambio.Add(CambioEliminar);
                                _unitOfWork.Commit();

                                //como no esta en listaCronograma proque fue eliminado //lo busco en el original
                                var eliminado = cronogramaActual.Where(w => w.NroCuota == orden.l.FirstOrDefault().Cuota && w.NroSubCuota == orden.l.FirstOrDefault().SubCuota).FirstOrDefault();

                                CronogramaDetalleCambio eliminadoTemp = new CronogramaDetalleCambio()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    IdCronogramaCabeceraCambio = cambioinsertadoeliminado.Id,
                                    NroCuota = eliminado.NroCuota.Value,
                                    NroSubcuota = eliminado.NroSubCuota.Value,
                                    FechaVencimiento = eliminado.FechaVencimiento.Value,
                                    Cuota = 0,
                                    Mora = eliminado.Mora.Value,
                                    Moneda = eliminado.Moneda,
                                    Version = versionNueva.Value,
                                    Estado = true,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now

                                };
                                _repCronogramaDetalleCambio.Add(eliminadoTemp);
                                _unitOfWork.Commit();

                                //INSERTO EN LOG
                                if (listaLog.Where(w => w.NroCuota == eliminado.NroCuota && w.NroSubCuota == eliminado.NroSubCuota).FirstOrDefault() != null)//ya esta en la lista
                                {
                                    var antiguo = listaLog.Where(w => w.NroCuota == eliminado.NroCuota && w.NroSubCuota == eliminado.NroSubCuota).FirstOrDefault();
                                    antiguo.MensajeSistema = "CUOTA ELIMINADA (" + eliminado.NroCuota + "," + eliminado.NroSubCuota + ")";
                                }
                                else//es nuevo
                                {
                                    CronogramaPagoDetalleModLogFinal log = new CronogramaPagoDetalleModLogFinal()
                                    {
                                        Id = 0,
                                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                        Fecha = DateTime.Now,
                                        NroCuota = eliminado.NroCuota,
                                        NroSubCuota = eliminado.NroSubCuota,
                                        FechaVencimiento = eliminado.FechaVencimiento,
                                        TotalPagar = eliminado.TotalPagar,
                                        Cuota = eliminado.Cuota,
                                        Mora = eliminado.Mora,
                                        MontoPagado = 0,
                                        Saldo = eliminado.Saldo,
                                        Cancelado = eliminado.Cancelado,
                                        TipoCuota = eliminado.TipoCuota,
                                        Moneda = eliminado.Moneda,
                                        FechaPago = null,
                                        IdFormaPago = eliminado.IdFormaPago,
                                        FechaPagoBanco = null,
                                        Ultimo = false,
                                        IdDocumentoPago = null,
                                        NroDocumento = null,
                                        MonedaPago = null,
                                        TipoCambio = null,
                                        MensajeSistema = "CUOTA ELIMINADA (" + eliminado.NroCuota + "," + eliminado.NroSubCuota + ")",
                                        FechaProcesoPago = null,
                                        EstadoPrimerLog = null
                                    };
                                    listaLog.Add(log);
                                    _unitOfWork.Commit();
                                }

                            }

                            if (orden.l.FirstOrDefault().TipoCambio == "Fecha")
                            {
                                //ttiposcambios=EFEFEA69-38BF-493D-97C8-014F8642BD37
                                //creo un nuevo tcambios con tipocambio de arriba

                                CronogramaCabeceraCambio CambioFechaTodoCronograma = new CronogramaCabeceraCambio()
                                {
                                    Id = 0,
                                    IdCronogramaTipoModificacion = 13,//una cuota
                                    SolicitadoPor = Json.Objeto.SolicitadoPorId,
                                    AprobadoPor = Json.Objeto.AprobadoPorId,
                                    Aprobado = false,
                                    Cancelado = false,
                                    Observacion = Json.Objeto.Comentario,
                                    Estado = true,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now

                                };
                                var cambiofechainsertado = _repCronogramaCabeceraCambio.Add(CambioFechaTodoCronograma);
                                _unitOfWork.Commit();

                                foreach (var detalles in orden.l.OrderBy(w => w.Cuota).ThenBy(w => w.SubCuota))//solo deberia ser 1
                                {
                                    var valoritem = Json.ListaCronograma.Where(w => w.NroCuota == detalles.Cuota && w.NroSubCuota == detalles.SubCuota).FirstOrDefault();//saca del ultimo con cambios
                                    CronogramaDetalleCambio cambioTemp = new CronogramaDetalleCambio()
                                    {
                                        Id = 0,
                                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                        IdCronogramaCabeceraCambio = cambiofechainsertado.Id,
                                        NroCuota = valoritem.NroCuota,
                                        NroSubcuota = valoritem.NroSubCuota,
                                        FechaVencimiento = valoritem.FechaVencimiento,
                                        Cuota = valoritem.Cuota,
                                        Mora = valoritem.Mora,
                                        Moneda = valoritem.Moneda,
                                        Version = versionNueva.Value,
                                        Estado = true,
                                        UsuarioCreacion = Json.Usuario,
                                        UsuarioModificacion = Json.Usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now

                                    };
                                    _repCronogramaDetalleCambio.Add(cambioTemp);
                                    _unitOfWork.Commit();

                                    //INSERTO EN LOG
                                    var fechaoriginal = cronogramaActual.Where(w => w.NroCuota == detalles.Cuota && w.NroSubCuota == detalles.SubCuota).FirstOrDefault();//deberia existir
                                    string fechaoriginaldate = fechaoriginal != null ? fechaoriginal.FechaVencimiento.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";

                                    if (listaLog.Where(w => w.NroCuota == valoritem.NroCuota && w.NroSubCuota == valoritem.NroSubCuota).FirstOrDefault() != null)//ya esta en la lista
                                    {
                                        var antiguo = listaLog.Where(w => w.NroCuota == valoritem.NroCuota && w.NroSubCuota == valoritem.NroSubCuota).FirstOrDefault();
                                        antiguo.MensajeSistema = antiguo.MensajeSistema + ", FECHA DE CUOTA (" + valoritem.NroCuota + "," + valoritem.NroSubCuota + ") HA VARIADO DE " + fechaoriginaldate + " A " + valoritem.FechaVencimiento.ToString("dd/MM/yyyy HH:mm:ss");
                                    }
                                    else//es nuevo
                                    {
                                        //traigo la fech original


                                        CronogramaPagoDetalleModLogFinal log = new CronogramaPagoDetalleModLogFinal()
                                        {
                                            Id = 0,
                                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                            Fecha = DateTime.Now,
                                            NroCuota = valoritem.NroCuota,
                                            NroSubCuota = valoritem.NroSubCuota,
                                            FechaVencimiento = valoritem.FechaVencimiento,
                                            TotalPagar = valoritem.TotalPagar,
                                            Cuota = valoritem.Cuota,
                                            Mora = valoritem.Mora,
                                            MontoPagado = 0,
                                            Saldo = valoritem.Saldo,
                                            Cancelado = valoritem.Cancelado,
                                            TipoCuota = valoritem.TipoCuota,
                                            Moneda = valoritem.Moneda,
                                            FechaPago = null,
                                            IdFormaPago = null,
                                            FechaPagoBanco = null,
                                            Ultimo = false,
                                            IdDocumentoPago = null,
                                            NroDocumento = null,
                                            MonedaPago = null,
                                            TipoCambio = null,
                                            MensajeSistema = "FECHA DE CUOTA (" + valoritem.NroCuota + "," + valoritem.NroSubCuota + ") HA VARIADO DE " + fechaoriginaldate + " A " + valoritem.FechaVencimiento.ToString("dd/MM/yyyy HH:mm:ss"),
                                            FechaProcesoPago = null,
                                            EstadoPrimerLog = null

                                        };
                                        listaLog.Add(log);
                                        _unitOfWork.Commit();

                                    }
                                }
                            }

                            if (orden.l.FirstOrDefault().TipoCambio == "Monto")
                            {
                                //ttiposcambios=E970D10D-00BF-401C-991A-32F7A32B4C52
                                //creo un nuevo tcambios con tipocambio de arriba

                                CronogramaCabeceraCambio CambioMonto = new CronogramaCabeceraCambio()
                                {
                                    Id = 0,
                                    IdCronogramaTipoModificacion = 4,//una cuota
                                    SolicitadoPor = Json.Objeto.SolicitadoPorId,
                                    AprobadoPor = Json.Objeto.AprobadoPorId,
                                    Aprobado = false,
                                    Cancelado = false,
                                    Observacion = Json.Objeto.Comentario,
                                    Estado = true,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };

                                var cambiomontoinsertado = _repCronogramaCabeceraCambio.Add(CambioMonto);
                                _unitOfWork.Commit();

                                foreach (var detalles in orden.l.OrderBy(w => w.Cuota).ThenBy(w => w.SubCuota))//solo deberia ser 1
                                {
                                    var valoritem = Json.ListaCronograma.Where(w => w.NroCuota == detalles.Cuota && w.NroSubCuota == detalles.SubCuota).FirstOrDefault();
                                    CronogramaDetalleCambio cambioTemp = new CronogramaDetalleCambio()
                                    {
                                        Id = 0,
                                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                        IdCronogramaCabeceraCambio = cambiomontoinsertado.Id,
                                        NroCuota = valoritem.NroCuota,
                                        NroSubcuota = valoritem.NroSubCuota,
                                        FechaVencimiento = valoritem.FechaVencimiento,
                                        Cuota = valoritem.Cuota,
                                        Mora = valoritem.Mora,
                                        Moneda = valoritem.Moneda,
                                        Version = versionNueva.Value,
                                        Estado = true,
                                        UsuarioCreacion = Json.Usuario,
                                        UsuarioModificacion = Json.Usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now

                                    };
                                    _repCronogramaDetalleCambio.Add(cambioTemp);
                                    _unitOfWork.Commit();

                                    //INSERTO EN LOG
                                    var montooriginal = cronogramaActual.Where(w => w.NroCuota == detalles.Cuota && w.NroSubCuota == detalles.SubCuota).FirstOrDefault();//deberia existir
                                    string montooriginaltexto = montooriginal != null ? montooriginal.Cuota.ToString() : "";

                                    if (listaLog.Where(w => w.NroCuota == valoritem.NroCuota && w.NroSubCuota == valoritem.NroSubCuota).FirstOrDefault() != null)//ya esta en la lista
                                    {
                                        var antiguo = listaLog.Where(w => w.NroCuota == valoritem.NroCuota && w.NroSubCuota == valoritem.NroSubCuota).FirstOrDefault();
                                        antiguo.MensajeSistema = antiguo.MensajeSistema + ", CUOTA HA VARIADO DE " + montooriginaltexto + " A " + valoritem.Cuota;
                                    }
                                    else//es nuevo
                                    {
                                        CronogramaPagoDetalleModLogFinal log = new CronogramaPagoDetalleModLogFinal()
                                        {
                                            Id = 0,
                                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                            Fecha = DateTime.Now,
                                            NroCuota = valoritem.NroCuota,
                                            NroSubCuota = valoritem.NroSubCuota,
                                            FechaVencimiento = valoritem.FechaVencimiento,
                                            TotalPagar = valoritem.TotalPagar,
                                            Cuota = valoritem.Cuota,
                                            Mora = valoritem.Mora,
                                            MontoPagado = 0,
                                            Saldo = valoritem.Saldo,
                                            Cancelado = valoritem.Cancelado,
                                            TipoCuota = valoritem.TipoCuota,
                                            Moneda = valoritem.Moneda,
                                            FechaPago = null,
                                            IdFormaPago = null,
                                            FechaPagoBanco = null,
                                            Ultimo = false,
                                            IdDocumentoPago = null,
                                            NroDocumento = null,
                                            MonedaPago = null,
                                            TipoCambio = null,
                                            MensajeSistema = "CUOTA (" + valoritem.NroCuota + "," + valoritem.NroSubCuota + ") HA VARIADO DE " + montooriginaltexto + " A " + valoritem.Cuota,
                                            FechaProcesoPago = null,
                                            EstadoPrimerLog = null

                                        };
                                        listaLog.Add(log);
                                        _unitOfWork.Commit();

                                    }
                                }

                            }

                            if (orden.l.FirstOrDefault().TipoCambio == "Mora")
                            {
                                //ttiposcambios=7FC068ED-F677-4D66-BEA1-5403721A851D
                                //creo un nuevo tcambios con tipocambio de arriba

                                CronogramaCabeceraCambio CambioMora = new CronogramaCabeceraCambio()
                                {
                                    Id = 0,
                                    IdCronogramaTipoModificacion = 5,//una cuota
                                    SolicitadoPor = Json.Objeto.SolicitadoPorId,
                                    AprobadoPor = Json.Objeto.AprobadoPorId,
                                    Aprobado = false,
                                    Cancelado = false,
                                    Observacion = Json.Objeto.Comentario,
                                    Estado = true,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };

                                var cambiomorainsertado = _repCronogramaCabeceraCambio.Add(CambioMora);
                                _unitOfWork.Commit();

                                foreach (var detalles in orden.l.OrderBy(w => w.Cuota).ThenBy(w => w.SubCuota))//solo deberia ser 1
                                {
                                    var valoritem = Json.ListaCronograma.Where(w => w.NroCuota == detalles.Cuota && w.NroSubCuota == detalles.SubCuota).FirstOrDefault();
                                    CronogramaDetalleCambio cambioTemp = new CronogramaDetalleCambio()
                                    {
                                        Id = 0,
                                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                        IdCronogramaCabeceraCambio = cambiomorainsertado.Id,
                                        NroCuota = valoritem.NroCuota,
                                        NroSubcuota = valoritem.NroSubCuota,
                                        FechaVencimiento = valoritem.FechaVencimiento,
                                        Cuota = valoritem.Cuota,
                                        Mora = valoritem.Mora,
                                        Moneda = valoritem.Moneda,
                                        Version = versionNueva.Value,
                                        Estado = true,
                                        UsuarioCreacion = Json.Usuario,
                                        UsuarioModificacion = Json.Usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now

                                    };
                                    _repCronogramaDetalleCambio.Add(cambioTemp);
                                    _unitOfWork.Commit();

                                    //INSERTO EN LOG
                                    var moraoriginal = cronogramaActual.Where(w => w.NroCuota == detalles.Cuota && w.NroSubCuota == detalles.SubCuota).FirstOrDefault();//deberia existir
                                    string moraoriginaltexto = moraoriginal != null ? moraoriginal.Mora.ToString() : "";

                                    if (listaLog.Where(w => w.NroCuota == valoritem.NroCuota && w.NroSubCuota == valoritem.NroSubCuota).FirstOrDefault() != null)//ya esta en la lista
                                    {
                                        var antiguo = listaLog.Where(w => w.NroCuota == valoritem.NroCuota && w.NroSubCuota == valoritem.NroSubCuota).FirstOrDefault();
                                        antiguo.MensajeSistema = antiguo.MensajeSistema + ", MORA HA VARIADO DE " + moraoriginaltexto + " A " + valoritem.Mora;
                                    }
                                    else//es nuevo
                                    {
                                        CronogramaPagoDetalleModLogFinal log = new CronogramaPagoDetalleModLogFinal()
                                        {
                                            Id = 0,
                                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                            Fecha = DateTime.Now,
                                            NroCuota = valoritem.NroCuota,
                                            NroSubCuota = valoritem.NroSubCuota,
                                            FechaVencimiento = valoritem.FechaVencimiento,
                                            TotalPagar = valoritem.TotalPagar,
                                            Cuota = valoritem.Cuota,
                                            Mora = valoritem.Mora,
                                            MontoPagado = 0,
                                            Saldo = valoritem.Saldo,
                                            Cancelado = valoritem.Cancelado,
                                            TipoCuota = valoritem.TipoCuota,
                                            Moneda = valoritem.Moneda,
                                            FechaPago = null,
                                            IdFormaPago = null,
                                            FechaPagoBanco = null,
                                            Ultimo = false,
                                            IdDocumentoPago = null,
                                            NroDocumento = null,
                                            MonedaPago = null,
                                            TipoCambio = null,
                                            MensajeSistema = "CUOTA (" + valoritem.NroCuota + "," + valoritem.NroSubCuota + ") SU MORA HA VARIADO DE " + moraoriginaltexto + " A " + valoritem.Mora,
                                            FechaProcesoPago = null,
                                            EstadoPrimerLog = null

                                        };
                                        listaLog.Add(log);
                                        _unitOfWork.Commit();

                                    }

                                }
                            }
                        }
                        foreach (var itemlog in listaLog)
                        {
                            itemlog.Version = versionNueva.Value;
                            itemlog.Aprobado = false;
                            itemlog.Estado2 = true;

                            itemlog.Estado = true;
                            itemlog.FechaCreacion = DateTime.Now;
                            itemlog.FechaModificacion = DateTime.Now;
                            itemlog.UsuarioCreacion = Json.Usuario;
                            itemlog.UsuarioModificacion = Json.Usuario;

                            if (itemlog.MensajeSistema.IndexOf("ELIMINADA") > -1)//si es de eliminada
                            {
                                //le cambio el estado2 a 0
                                itemlog.Estado2 = false;
                            }
                            var insertlog = _repCronogramaPagoDetalleModLogFinal.Add(itemlog);
                            _unitOfWork.Commit();

                        }
                    }
                    scope.Complete();
                }

                //    return true;
                //}
                //var cronogramaactual = _tcrm_CentroCostoService.GetCronogramaFinal(matricula).ToList();
                //var per = _tcrm_CentroCostoService.GetTpersonalsByUserName(User.Identity.Name);
                //var Rpta = _tCronogramaPagosDetalle_FinalService.InsertCambioMoneda(matricula, per[0].id, per[0].nombre_completo, listaCronograma, cronogramaactual);
                //var jsonResult = Json(new { Result = "OK", Records = "Prueba" }, JsonRequestBehavior.AllowGet);
                return (new { Message = "Se Guardo correctamente", IdMatriculaCabeceraSincronizacion });
                //jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }

        /// Autor:Margiory Ramirez.
        /// Fecha: 21/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la fecha de pago
        /// </summary>
        /// <returns>Json/returns>
        public object ActualizarFechaPago(PagoActualizadoFechaDTO Json)
        {

            try
            {

                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                CronogramaPagoDetalleFinal CronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinal();
                CronogramaPagoDetalleFinal = _mapper.Map<CronogramaPagoDetalleFinal>(_repCronogramaPagoDetalleFinal.FirstById(Json.IdCuota));
                CronogramaPagoDetalleFinal.FechaPago = Json.FechaPago;
                CronogramaPagoDetalleFinal.FechaModificacion = DateTime.Now;
                CronogramaPagoDetalleFinal.UsuarioModificacion = Json.Usuario;
                _repCronogramaPagoDetalleFinal.Update(CronogramaPagoDetalleFinal);
                _unitOfWork.Commit();

                return (new { Message = "Se Actualizo correctamente" });
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }


        }

   
        /// Autor:Griselberto H.
        /// Fecha: 20/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la gestion de Cobranza.
        /// </summary>
        /// <returns>Json/returns>
        public object ActualizarGestionDeCobranza(PagoActualizadoMoraTarifarioDTO Json)
        {

            try
            {

                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                CronogramaPagoDetalleFinal CronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinal();
                CronogramaPagoDetalleFinal = _mapper.Map<CronogramaPagoDetalleFinal>(_repCronogramaPagoDetalleFinal.FirstById(Json.IdCuota));
                CronogramaPagoDetalleFinal.MoraTarifario = Json.MoraTarifario;
                CronogramaPagoDetalleFinal.MonedaMoraTarifario = CronogramaPagoDetalleFinal.Moneda;
                CronogramaPagoDetalleFinal.FechaModificacion = DateTime.Now;
                CronogramaPagoDetalleFinal.UsuarioModificacion = Json.Usuario;
                _repCronogramaPagoDetalleFinal.Update(CronogramaPagoDetalleFinal);
                _unitOfWork.Commit();

                return (new { Message = "Se Actualizo correctamente" });
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }


        }



        /// Autor: Margiory Ramirez
        /// Fecha: 21/01/2023
        /// Versión: 2.0
        /// <summary>
        /// Guarda el cambio de moneda en todas las tablas respectivas
        /// modificacion : ahora modifica la moneda en la tabla T_CronogramaPago siempre 
        /// </summary>
        /// <returns>Json</returns>
        public object ActualizarMoraCAdelanto(MoraActualizadoDTO Json)
        {
            try
            {



                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                var _repCronogramaDetalleCambio = _unitOfWork.CronogramaDetalleCambioRepository;
                var _repCronogramaCabeceraCambio = _unitOfWork.CronogramaCabeceraCambioRepository;
                var _repCronogramaPagoDetalleModLogFinal = _unitOfWork.CronogramaPagoDetalleModLogFinalRepository;
                var _repPersonal = _unitOfWork.PersonalRepository;
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;

                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(w => w.CodigoMatricula == Json.Objeto.CodigoMatricula).FirstOrDefault();
                var versionAprobadoTemp = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                var cronogramaActual = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == versionAprobadoTemp.Version, x => new { x.Id, x.Cancelado, FlagCancelado = x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.FechaVencimiento, x.TotalPagar, x.Cuota, x.Mora, x.Saldo, x.Moneda, x.MontoPagado, x.FechaPago, x.IdFormaPago, x.IdCuenta, x.FechaPagoBanco, x.Enviado, x.Observaciones, x.IdDocumentoPago, x.NroDocumento, x.MonedaPago, x.TipoCambio, x.CuotaDolares, x.FechaProcesoPago, x.Version, x.FechaDeposito, x.FechaEfectivoDisponible, x.FechaIngresoEnCuenta, x.FechaProcesoPagoReal, x.UsuarioCoordinadorAcademico }).OrderBy(x => x.NroCuota).ThenBy(x => x.NroSubCuota).ToList();

                var ExisteVersionNoAprobada = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Aprobado == false).ToList();
                if (ExisteVersionNoAprobada.Count > 0)
                {
                    return ("Existe Versiones sin Aprobar");
                }
                else
                {
                    decimal totalmonto = Json.ListaCronograma.Sum(w => w.Cuota);
                    var versionnueva = cronogramaActual.FirstOrDefault().Version + 1;
                    CronogramaCabeceraCambio CambioFraccion = new CronogramaCabeceraCambio()
                    {
                        Id = 0,
                        IdCronogramaTipoModificacion = 6,
                        SolicitadoPor = 0,
                        AprobadoPor = 0,
                        Aprobado = true,
                        Cancelado = false,
                        Observacion = "",
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = Json.Usuario,
                        UsuarioModificacion = Json.Usuario
                    };
                    var cambioinsertadofraccion = _repCronogramaCabeceraCambio.Add(CambioFraccion);
                    _unitOfWork.Commit();

                    for (int i = 0; i < Json.ListaCronograma.Count(); i++)
                    {
                        CronogramaPagoDetalleFinal Cuota = new CronogramaPagoDetalleFinal()
                        {
                            Id = 0,
                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                            NroCuota = Json.ListaCronograma[i].NroCuota,
                            NroSubCuota = Json.ListaCronograma[i].NroSubCuota,
                            FechaVencimiento = Json.ListaCronograma[i].FechaVencimiento,
                            TotalPagar = totalmonto,
                            Cuota = Json.ListaCronograma[i].Cuota,
                            Saldo = totalmonto - Json.ListaCronograma[i].Cuota,
                            Mora = Json.ListaCronograma[i].Mora,
                            Cancelado = Json.ListaCronograma[i].Cancelado,
                            TipoCuota = Json.ListaCronograma[i].TipoCuota,
                            Moneda = Json.ListaCronograma[i].Moneda,
                            TipoCambio = cronogramaActual[i].TipoCambio,
                            Version = versionnueva,
                            FechaPago = cronogramaActual[i].FechaPago,
                            FechaDeposito = cronogramaActual[i].FechaDeposito,
                            IdFormaPago = cronogramaActual[i].IdFormaPago,
                            IdCuenta = cronogramaActual[i].IdCuenta,
                            FechaPagoBanco = cronogramaActual[i].FechaPagoBanco,
                            Enviado = cronogramaActual[i].Enviado,
                            Observaciones = cronogramaActual[i].Observaciones,
                            IdDocumentoPago = cronogramaActual[i].IdDocumentoPago,
                            NroDocumento = cronogramaActual[i].NroDocumento,
                            MonedaPago = cronogramaActual[i].MonedaPago,
                            FechaProcesoPago = cronogramaActual[i].FechaProcesoPago,
                            MontoPagado = cronogramaActual[i].MontoPagado,
                            Aprobado = true,//Se convierte en true cuando aprueba los cambios                          
                            FechaProcesoPagoReal = cronogramaActual[i].FechaProcesoPagoReal,
                            FechaIngresoEnCuenta = cronogramaActual[i].FechaIngresoEnCuenta,
                            FechaEfectivoDisponible = cronogramaActual[i].FechaEfectivoDisponible,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = Json.Usuario,
                            UsuarioModificacion = Json.Usuario,
                            UsuarioCoordinadorAcademico = cronogramaActual[i].UsuarioCoordinadorAcademico
                        };
                        _repCronogramaPagoDetalleFinal.Add(Cuota);
                        _unitOfWork.Commit();

                        if (Json.Objeto.NroCuota == Json.ListaCronograma[i].NroCuota && Json.Objeto.NroSubCuota == Json.ListaCronograma[i].NroSubCuota)
                        {
                            CronogramaDetalleCambio detalleCambio = new CronogramaDetalleCambio()
                            {
                                Id = 0,
                                IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                IdCronogramaCabeceraCambio = CambioFraccion.Id,
                                NroCuota = Json.ListaCronograma[i].NroCuota,
                                NroSubcuota = Json.ListaCronograma[i].NroSubCuota,
                                FechaVencimiento = Json.ListaCronograma[i].FechaVencimiento,
                                Cuota = Json.ListaCronograma[i].Cuota,
                                Mora = Json.ListaCronograma[i].Mora,
                                Moneda = Json.ListaCronograma[i].Moneda,
                                Version = versionnueva.Value,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = Json.Usuario,
                                UsuarioModificacion = Json.Usuario
                            };
                            _repCronogramaDetalleCambio.Add(detalleCambio);
                            _unitOfWork.Commit();
                        }
                        if (Json.Objeto.Id == Json.ListaCronograma[i].Id)
                        {
                            CronogramaDetalleCambio detalleCambio = new CronogramaDetalleCambio()
                            {
                                Id = 0,
                                IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                IdCronogramaCabeceraCambio = CambioFraccion.Id,
                                NroCuota = Json.ListaCronograma[i].NroCuota,
                                NroSubcuota = Json.ListaCronograma[i].NroSubCuota,
                                FechaVencimiento = Json.ListaCronograma[i].FechaVencimiento,
                                Cuota = Json.ListaCronograma[i].Cuota,
                                Mora = Json.ListaCronograma[i].Mora,
                                Moneda = Json.ListaCronograma[i].Moneda,
                                Version = versionnueva.Value,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = Json.Usuario,
                                UsuarioModificacion = Json.Usuario
                            };
                            _repCronogramaDetalleCambio.Add(detalleCambio);
                            _unitOfWork.Commit();

                            if (cronogramaActual[i].Cancelado == false)
                            {
                                CronogramaPagoDetalleModLogFinal log = new CronogramaPagoDetalleModLogFinal()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    Fecha = DateTime.Now,
                                    NroCuota = Json.ListaCronograma[i].NroCuota,
                                    NroSubCuota = Json.ListaCronograma[i].NroSubCuota,
                                    FechaVencimiento = Json.ListaCronograma[i].FechaVencimiento,
                                    TotalPagar = totalmonto,
                                    Cuota = Json.ListaCronograma[i].Cuota,
                                    Mora = Json.ListaCronograma[i].Mora,
                                    MontoPagado = 0,
                                    Saldo = totalmonto - Json.ListaCronograma[i].Cuota,
                                    Cancelado = Json.ListaCronograma[i].Cancelado,
                                    TipoCuota = Json.ListaCronograma[i].TipoCuota,
                                    Moneda = Json.ListaCronograma[i].Moneda,
                                    FechaPago = null,
                                    IdFormaPago = null,
                                    FechaPagoBanco = null,
                                    Ultimo = false,
                                    IdDocumentoPago = null,
                                    NroDocumento = null,
                                    MonedaPago = null,
                                    TipoCambio = null,
                                    MensajeSistema = "CUOTA (" + Json.ListaCronograma[i].NroCuota + "," + Json.ListaCronograma[i].NroSubCuota + ") HA VARIADO DE " + cronogramaActual[i].Cuota + " A " + Json.ListaCronograma[i].Cuota,
                                    FechaProcesoPago = null,
                                    EstadoPrimerLog = null,
                                    Version = versionnueva,
                                    Aprobado = true,
                                    Estado2 = true,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario

                                };
                                var insertLog = _repCronogramaPagoDetalleModLogFinal.Add(log);
                                _unitOfWork.Commit();
                            }
                        }

                        totalmonto = totalmonto - Json.ListaCronograma[i].Cuota;
                    }
                    return (new { Message = "Se Modifico correctamente" });
                }

            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }




        /// Autor:Margiory Ramirez.
        /// Fecha: 23/01/2023
        /// Versión: 2.0
        /// <summary>
        /// Inhabilita la matrocula y guarda todos los cambios del cronograma asi como sus detalles y también los log de este
        /// modificacion : se agrego el campo  UsuarioCoordinadorAcademico a la tabla de cronograma de la matricula
        /// </summary>
        /// <returns>Json</returns>

        public object EliminarMatricula(string CodigoMatricula, int Modoeliminacion, string Usuario)
        {
            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula).FirstOrDefault();
                var _repControlDoc = _unitOfWork.ControlDocRepository;
                var _repControlDocAlumno = _unitOfWork.ControlDocAlumnoRepository;
                var repCronogramaPagoDetalleMod = _unitOfWork.CronogramaPagoDetalleModRepository;
                var _repCronogramaPagoDetalle = _unitOfWork.CronogramaPagoRepository;
                var _repCronogramaPago = _unitOfWork.CronogramaPagoRepository;
                var _repMatriculaDetalle = _unitOfWork.MatriculaDetalleRepository;
                var _repCronogramaPagoDetalleOriginal = _unitOfWork.CronogramaPagoDetalleOriginalRepository;
                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                //var _repPagoFinal = _unitOfWork.PagoFinalRepository;
                //var _repPago = _unitOfWork.PagoRepository;
                //var _repCronogramaDetalleCambio = _unitOfWork.CronogramaDetalleCambioRepository;



                if (Modoeliminacion == 4)//con devolucion
                {
                    matriculaCabeceraTemp.EstadoMatricula = "condevolucion";
                    matriculaCabeceraTemp.FechaModificacion = DateTime.Now;
                    matriculaCabeceraTemp.UsuarioModificacion = Usuario;
                    matriculaCabeceraTemp.FechaRetiro = DateTime.Now;
                    _repMatriculaCabecera.Update(matriculaCabeceraTemp);
                    _unitOfWork.Commit();

                    var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                    var nroCuota = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true && x.Version == versionAprobada.Version, x => new { x.NroCuota }).OrderByDescending(x => x.NroCuota).FirstOrDefault();
                    var monedaOriginal = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true && x.Version == versionAprobada.Version, x => new { x.Moneda }).FirstOrDefault();
                    var montoDevolver = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true && x.Version == versionAprobada.Version && x.Cancelado == true).Sum(x => x.Cuota);
                    var cor = _repCronogramaPagoDetalleFinal.
                        GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true && x.Version == versionAprobada.Version && x.UsuarioCoordinadorAcademico != null).FirstOrDefault();

                    var UsuarioCoordinadorAcademico = cor == null ? null : cor.UsuarioCoordinadorAcademico;
                    montoDevolver = montoDevolver * -1;

                    CronogramaPagoDetalleFinal nuevaCuota = new CronogramaPagoDetalleFinal()
                    {
                        Id = 0,
                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                        NroCuota = nroCuota.NroCuota + 1,
                        NroSubCuota = 1,
                        FechaVencimiento = DateTime.Now,
                        TotalPagar = 0,
                        Cuota = montoDevolver,
                        Mora = 0,
                        MontoPagado = 0,
                        Saldo = 0,
                        Cancelado = true,
                        TipoCuota = "CUOTA",
                        Moneda = monedaOriginal.Moneda,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        UsuarioCreacion = Usuario,
                        FechaModificacion = DateTime.Now,
                        UsuarioModificacion = Usuario,
                        Enviado = false,
                        Version = versionAprobada.Version,
                        Aprobado = true,
                        UsuarioCoordinadorAcademico = UsuarioCoordinadorAcademico
                    };
                    _repCronogramaPagoDetalleFinal.Add(nuevaCuota);
                    _unitOfWork.Commit();

                }
                else if (Modoeliminacion == 3)//sin devolucion
                {
                    matriculaCabeceraTemp.EstadoMatricula = "sindevolucion";
                    matriculaCabeceraTemp.FechaModificacion = DateTime.Now;
                    matriculaCabeceraTemp.UsuarioModificacion = Usuario;
                    matriculaCabeceraTemp.FechaRetiro = DateTime.Now;
                    _repMatriculaCabecera.Update(matriculaCabeceraTemp);
                    _unitOfWork.Commit();
                }
                //else if (Modoeliminacion == 2)//eliminar
                //{
                //    matriculaCabeceraTemp.EstadoMatricula = "eliminado";
                //    matriculaCabeceraTemp.FechaModificacion = DateTime.Now;
                //    matriculaCabeceraTemp.UsuarioModificacion = Usuario;
                //    matriculaCabeceraTemp.FechaRetiro = DateTime.Now;
                //    _repMatriculaCabecera.Update(matriculaCabeceraTemp);

                //    var listaDocumentos = _repControlDoc.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                //    _repControlDoc.Delete(listaDocumentos, Usuario);
                //    var listaDocumentosAlumno = _repControlDocAlumno.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                //    _repControlDocAlumno.Delete(listaDocumentosAlumno, Usuario);
                //    var listacronogramaPagoDetalleMod = repCronogramaPagoDetalleMod.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                //    repCronogramaPagoDetalleMod.Delete(listacronogramaPagoDetalleMod, Usuario);
                //    var listacronogramaPagoDetalle = _repCronogramaPagoDetalle.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                //    _repCronogramaPagoDetalle.Delete(listacronogramaPagoDetalle, Usuario);
                //    var listaPago = _repPago.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                //    _repPago.Delete(listaPago, Usuario);
                //    var listacronogramaPago = _repCronogramaPago.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                //    _repCronogramaPago.Delete(listacronogramaPago, Usuario);
                //    var listaMatriculaDetalle = _repMatriculaDetalle.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                //    _repMatriculaDetalle.Delete(listaMatriculaDetalle, Usuario);
                //    _repMatriculaCabecera.Delete(matriculaCabeceraTemp.Id, Usuario);

                //    var _repCronogramaPagoDetalleModLogFinal = _unitOfWork.CronogramaPagoDetalleModLogFinalRepository;
                //    List<CronogramaPagoDetalleModLogFinal> listaLogs = new List<CronogramaPagoDetalleModLogFinal>();
                //    var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                //    var listaCronogramaFinal = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Version == versionAprobada.Version).ToList();
                //    foreach (var item in listaCronogramaFinal)
                //    {
                //        CronogramaPagoDetalleModLogFinal Log = new CronogramaPagoDetalleModLogFinal()
                //        {
                //            Id = 0,
                //            Fecha = DateTime.Now,
                //            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                //            NroCuota = item.NroCuota,
                //            NroSubCuota = item.NroSubCuota,
                //            FechaVencimiento = item.FechaVencimiento,
                //            TotalPagar = item.TotalPagar,
                //            Cuota = item.Cuota,
                //            Mora = item.Mora,
                //            MontoPagado = item.MontoPagado,
                //            Saldo = item.Saldo,
                //            Cancelado = item.Cancelado,
                //            TipoCuota = item.TipoCuota,
                //            Moneda = item.Moneda,
                //            FechaPago = item.FechaPago,
                //            IdFormaPago = item.IdFormaPago,
                //            Estado = item.Estado,
                //            Estado2 = item.Estado,
                //            FechaPagoBanco = item.FechaPagoBanco,
                //            Observaciones = item.Observaciones,
                //            IdDocumentoPago = item.IdDocumentoPago,
                //            NroDocumento = item.NroDocumento,
                //            MonedaPago = item.MonedaPago,
                //            TipoCambio = item.TipoCambio,
                //            FechaCreacion = DateTime.Now,
                //            UsuarioCreacion = Usuario,
                //            FechaModificacion = DateTime.Now,
                //            UsuarioModificacion = Usuario,
                //            MensajeSistema = "ESTA CUOTA SE HA ELIMINADO",
                //            FechaProcesoPago = item.FechaProcesoPago,
                //            Version = item.Version,
                //            Aprobado = true
                //        };
                //        listaLogs.Add(Log);
                //        _unitOfWork.Commit();
                //    }
                //    _repCronogramaPagoDetalleModLogFinal.Add(listaLogs);
                //    _unitOfWork.Commit();

                //    var listaCronogramaPagoDetalleOriginal = _repCronogramaPagoDetalleOriginal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                //    _repCronogramaPagoDetalleOriginal.Delete(listaCronogramaPagoDetalleOriginal, Usuario);
                //    var listaCronogramaPagoDetalleFinal = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                //    _repCronogramaPagoDetalleFinal.Delete(listaCronogramaPagoDetalleFinal, Usuario);
                //    var listaPagoFinal = _repPagoFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                //    _repPagoFinal.Delete(listaPagoFinal, Usuario);
                //    var listaCronogramaDetalleCambio = _repCronogramaDetalleCambio.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                //    _repCronogramaDetalleCambio.Delete(listaCronogramaDetalleCambio, Usuario);
                //}
                var estado = matriculaCabeceraTemp.EstadoMatricula;

                return new { Message = "Se Elimino correctamente", CodMatricula = CodigoMatricula, Estado = estado };
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }

        }


        /// Autor:Margiory Ramirez
        /// Fecha: 23/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene la lista de alumnos matriculados por pograma o por codigo de alumno o por ambos filtros
        /// </summary>
        /// <returns>Json/returns>


        public List<AlumnoMatriculaDTO> ObtenerListadoAlumnosMatricula(EscrituraCrepDTO escrituraCrepDTO)
        {

            try
            {
                var _repListadoAlumnosMatricula = _unitOfWork.MatriculaCabeceraRepository;
                var listadoAlumnoMatricula = new List<AlumnoMatriculaDTO>();
                if (escrituraCrepDTO.Tipo == 1)//por programa
                {
                    listadoAlumnoMatricula = (_repListadoAlumnosMatricula.ObtenerListadoAlumnosMatriculaCodigoPEspecifico(escrituraCrepDTO.IdPrograma.GetValueOrDefault()));
                }
                else if (escrituraCrepDTO.Tipo == 2)//por alumno
                {
                    listadoAlumnoMatricula = (_repListadoAlumnosMatricula.ObtenerListadoAlumnosMatriculaIdAlumno(escrituraCrepDTO.IdAlumno.GetValueOrDefault()));
                }
                else if (escrituraCrepDTO.Tipo == 3)//por ambos
                {
                    listadoAlumnoMatricula = (_repListadoAlumnosMatricula.ObtenerListadoAlumnosMatricula());
                }

                else if (escrituraCrepDTO.Tipo == 4)//por ambos
                {
                    listadoAlumnoMatricula = (_repListadoAlumnosMatricula.ObtenerListadoAlumnosMatriculaICodigoMatricula(escrituraCrepDTO.CodigoMatricula));
                }

                return listadoAlumnoMatricula;
            }
            catch (Exception e)
            {
                return null;
            }
        }



        /// Autor: Margiory Ramirez
        /// Fecha: 23/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene las cuotas de pago en base al codigo de matricula
        /// </summary>
        /// <returns>Json/returns>


        public object ObtenerCuotasCrepPorCodigoMatricula(string CodigoMatricula)
        {

            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                return (_repCronogramaPagoDetalleFinal.ObtenerCuotasCrepPorCodigoMatricula(matriculaCabeceraTemp.Id, versionAprobada.Version));
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }

        /// Autor:Margiory Ramirez
        /// Fecha: 23/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene las cuentas de pago
        /// </summary>
        /// <returns>Json/returns>

        public object ObtenerCuentasCorrientes()
        {

            try
            {
                var _repCuentaCorriente = _unitOfWork.CuentaCorrienteRepository;
                var listadoCuentasCorrientes = _repCuentaCorriente.ObtenerCuentasCorrientes();
                var listadoCuentasCorrientesFinal = new List<DatosCuentaCorrienteDTO>();
                foreach (var item in listadoCuentasCorrientes)
                {
                    var tempCuentaCorriente = new DatosCuentaCorrienteDTO
                    {
                        IdCta = item.IdCta,
                        Id = string.Concat(item.NumeroCuenta, "-", (item.Ciudad == "LIMA Y CALLAO" ? "LIMA" : item.Ciudad)),
                        Cuenta = String.Concat(item.NombreEntidadFinanciera, " ", item.NumeroCuenta)
                    };
                    listadoCuentasCorrientesFinal.Add(tempCuentaCorriente);
                    _unitOfWork.Commit();
                }
                return (listadoCuentasCorrientesFinal);
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }


        /// Autor:Margiory Ramirez
        /// Fecha: 23/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera un crep de pago
        /// </summary>
        /// <returns>Json/returns>


        public object GenerarCrep(ListaCrepsDTO Json, string Usuario)
        {
            ActividadCrepLog acl = new ActividadCrepLog();
            string crepString = "";
            try
            {

                bool _tienemora = false;
                Int16 tipo = 0;
                foreach (var item in Json.lista)
                {
                    if (Convert.ToDouble(item.Mora) > 0)
                    {
                        _tienemora = true;
                    }
                    if (_tienemora)
                    {
                        tipo = 2;
                    }
                    else
                    {
                        tipo = 1;
                    }
                    switch (tipo)
                    {
                        case 1:
                            item.CodigoEspecial = "1" + item.nroCuota.ToString().PadLeft(2, '0') + item.nroSubcuota.ToString().PadLeft(2, '0') + "XXXXXX";
                            break;
                        case 2:
                            item.CodigoEspecial = "2" + item.nroCuota.ToString().PadLeft(2, '0') + item.nroSubcuota.ToString().PadLeft(2, '0') + item.Mora.ToString("n2").Replace(".", "").PadLeft(6, '0');
                            break;
                        default:
                            break;
                    }
                    _tienemora = false;
                }

                var CronogramaPagDetalleFinal = new CronogramaPagoDetalleFinalService(_unitOfWork);
                var Result = CronogramaPagDetalleFinal.GenerarCrep(Json.objeto, Json.lista, Json.listaalumnos);
               
                crepString = JsonConvert.SerializeObject(new  { Cabeceara = Json.objeto, Lista =  Json.lista, DataAlumno = Json.listaalumnos });

                acl.TipoOperacion = "Exportacion " + Json.objeto.ManualAutomatico;
                acl.TipoActividad = Json.objeto.ActualizarEliminar == "A" ? "Actualizar" : "Eliminar";
                acl.EstadoOperacion = 1;// 1:Processado , 2:error
                acl.ExcepcionProceso = "";
                acl.Crep = crepString;
                acl.FechaCreacion = DateTime.Now;
                acl.FechaModificacion = DateTime.Now;
                acl.UsuarioCreacion = Usuario;
                acl.UsuarioModificacion = Usuario;
                acl.Estado = true;

                _unitOfWork.ActividadCrepLogRepository.Add(acl);
                _unitOfWork.Commit();

                return (new { Result });
            }
            catch (Exception ex)
            {
                var excepcion = JsonConvert.SerializeObject(ex);

                acl.TipoOperacion = "Exportacion " + Json.objeto.ManualAutomatico;
                acl.TipoActividad = Json.objeto.ActualizarEliminar == "A" ? "Actualizar" : "Eliminar";
                acl.EstadoOperacion = 2;//1:Processado , 2:error
                acl.ExcepcionProceso = excepcion;
                acl.Crep = crepString;
                acl.FechaCreacion = DateTime.Now;
                acl.FechaModificacion = DateTime.Now;
                acl.UsuarioCreacion = Usuario;
                acl.UsuarioModificacion = Usuario;
                acl.Estado = true;

                _unitOfWork.ActividadCrepLogRepository.Add(acl);
                _unitOfWork.Commit();

                return (ex.Message);
            }
               
        }
        

        public List<MatriculaControlDocumentoDTO> ObtenerDocumentosFiltrado(FiltroControlDocumentoDTO filtro)
        {

            try
            {
                return _unitOfWork.CronogramaPagoDetalleFinalRepository.ObtenerDocumentosFiltrado(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<DTO.ComboDTO> ObtenerComboCodigoMatricula(Dictionary<string, string> Valor) //CodigoMatricula
        {
            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                //(_repMatriculaCabecera.GetBy(x => x.CodigoMatricula.Contains(Valor["filtro"]), x => new { Id = x.CodigoMatricula }));
                return (_repMatriculaCabecera.GetBy(x => x.CodigoMatricula.Contains(Valor["valor"].ToString()), x => new ComboDTO{ Id = x.Id,Nombre=x.CodigoMatricula }).ToList());

            }
            catch (Exception e)
            {
                throw e;
            }
        }





      
        /// Autor:
        /// Fecha: 19/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la mora de adelanto
        /// </summary>
        /// <returns>Json/returns>
        public object GuardarCambioMonedaCronograma(CambioMonedaCronogramaModificadoDTO Json)
        {
            try
            {

                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                var _repCronogramaDetalleCambio = _unitOfWork.CronogramaDetalleCambioRepository;
                var _repCronogramaCabeceraCambio = _unitOfWork.CronogramaCabeceraCambioRepository;
                var _repCronogramaPagoDetalleModLogFinal = _unitOfWork.CronogramaPagoDetalleModLogFinalRepository;
                var _repCronogramaPago = _unitOfWork.CronogramaPagoRepository;
                var _repPersonal = _unitOfWork.PersonalRepository;
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var _repMoneda = _unitOfWork.MonedaRepository;


                var matricula = _repMatriculaCabecera.FirstBy(w => w.CodigoMatricula == Json.CodigoMatricula);
                if (!matricula.EstadoMatricula.Equals("matriculado") && !matricula.EstadoMatricula.Equals("pormatricular"))
                {
                    throw new Exception("No se realizo la modificacion debido a que la matricula no se encuentra en estado matriculado o por matricular.");
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(w => w.CodigoMatricula == Json.CodigoMatricula).FirstOrDefault();
                    Json.IdMatriculaCabecera = Convert.ToInt32(matriculaCabeceraTemp.Id);
                    var versionAprobadoTemp = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();

                    var cronogramaActual = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == versionAprobadoTemp.Version, x => new { x.Id, x.Cancelado, FlagCancelado = x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.FechaVencimiento, x.TotalPagar, x.Cuota, x.Mora, x.Saldo, x.Moneda, x.MontoPagado, x.FechaPago, x.IdFormaPago, x.IdCuenta, x.FechaPagoBanco, x.Enviado, x.Observaciones, x.IdDocumentoPago, x.NroDocumento, x.MonedaPago, x.TipoCambio, x.CuotaDolares, x.FechaProcesoPago, x.Version, x.FechaDeposito, x.FechaEfectivoDisponible, x.FechaIngresoEnCuenta, x.FechaProcesoPagoReal, x.UsuarioCoordinadorAcademico,x.NroDocumentoComprobante ,x.NombreRazonSocial,x.IdTipoComprobante,x.Observacion}).OrderBy(x => x.NroCuota).ThenBy(x => x.NroSubCuota).ToList();
                    //public bool InsertCambioMoneda(string matricula, int idPer, string NombreC, List<Cronograma_FinalModificadoDTO> listaCronograma, List<Cronograma_FinalDTO> cronogramaactual)
                    //{
                    decimal totalMonto = Json.ListaCronograma.Sum(x => x.Cuota);//cuota era monto   
                    /*int*/
                    var versionNueva = cronogramaActual.FirstOrDefault().Version + 1;

                    //tCambiosDTO cambiofraccion = new tCambiosDTO()
                    CronogramaCabeceraCambio cronogramaCabeceraCambio = new CronogramaCabeceraCambio()
                    {
                        IdCronogramaTipoModificacion = ValorEstatico.IdCronogramaTipoModificacionCuota,
                        SolicitadoPor = Json.IdPersonal,
                        AprobadoPor = Json.IdPersonal,
                        Aprobado = true,
                        Cancelado = false,
                        Observacion = "",
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        UsuarioCreacion = Json.UsuarioNombre,
                        FechaModificacion = DateTime.Now,
                        UsuarioModificacion = Json.UsuarioNombre
                    };


                    //var cambioinsertadofraccion = _tCambiosService.Insert(cambiofraccion);
                    var cambioinsertadofraccion = _repCronogramaCabeceraCambio.Add(cronogramaCabeceraCambio);
                    _unitOfWork.Commit();



                    for (int i = 0; i < Json.ListaCronograma.Count(); i++)
                       // foreach (var item in Json.ListaCronograma)
                        {
                        CronogramaPagoDetalleFinal cuota = new CronogramaPagoDetalleFinal()
                        {
                            //  tCronogramaPagosDetalle_FinalDTO cuota = new tCronogramaPagosDetalle_FinalDTO()
                            //{
                            //Id = "00000000-0000-0000-0000-000000000000",
                            IdMatriculaCabecera = Json.IdMatriculaCabecera,
                            NroCuota = Json.ListaCronograma[i].NroCuota,
                            NroSubCuota = Json.ListaCronograma[i].NroSubCuota,
                            FechaVencimiento = DateTime.ParseExact(Json.ListaCronograma[i].FechaVencimiento.ToString("dd/MM/yyyy HH:mm:ss"), "d/MM/yyyy HH:mm:ss", null),
                            TotalPagar = totalMonto,
                            Cuota = Json.ListaCronograma[i].Cuota,
                            Saldo = totalMonto - Json.ListaCronograma[i].Cuota,
                            Mora = Json.ListaCronograma[i].Mora,
                            Cancelado = Json.ListaCronograma[i].Cancelado,
                            TipoCuota = Json.ListaCronograma[i].TipoCuota,
                            Moneda = Json.ListaCronograma[i].Moneda,
                            TipoCambio =  Json.ListaCronograma[i].TipoCambio,
                            Version = versionNueva,
                            FechaPago = cronogramaActual[i].FechaPago != null ? cronogramaActual[i].FechaPago : null,
                            FechaDeposito = cronogramaActual[i].FechaDeposito != null ? cronogramaActual[i].FechaDeposito : null,
                            IdFormaPago = cronogramaActual[i].IdFormaPago,
                            IdCuenta = cronogramaActual[i].IdCuenta,
                            FechaPagoBanco = cronogramaActual[i].FechaPagoBanco != null ? cronogramaActual[i].FechaPagoBanco : null,
                            Enviado = cronogramaActual[i].Enviado,
                            Observaciones = cronogramaActual[i].Observaciones,
                            IdDocumentoPago = cronogramaActual[i].IdDocumentoPago,
                            NroDocumento = cronogramaActual[i].NroDocumento,
                            MonedaPago = Json.ListaCronograma[i].MonedaPago,
                            FechaProcesoPago = cronogramaActual[i].FechaProcesoPago != null ? cronogramaActual[i].FechaProcesoPago : null,
                            MontoPagado = Json.ListaCronograma[i].MontoPagado,
                            FechaEfectivoDisponible = cronogramaActual[i].FechaEfectivoDisponible,
                            FechaIngresoEnCuenta = cronogramaActual[i].FechaIngresoEnCuenta,
                            FechaProcesoPagoReal = cronogramaActual[i].FechaProcesoPagoReal,

                            NroDocumentoComprobante = cronogramaActual[i].NroDocumentoComprobante,
                            NombreRazonSocial = cronogramaActual[i].NombreRazonSocial,
                            Observacion = cronogramaActual[i].Observacion,
                            IdTipoComprobante = cronogramaActual[i].IdTipoComprobante,

                            //corregir *********** IdCambio = "00000000-0000-0000-0000-000000000000",
                            Aprobado = true,//Se convierte en true cuando aprueba los cambios
                                            //};
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = Json.UsuarioNombre,
                            FechaModificacion = DateTime.Now,
                            UsuarioModificacion = Json.UsuarioNombre,
                            UsuarioCoordinadorAcademico = cronogramaActual[i].UsuarioCoordinadorAcademico
                        };
                        _repCronogramaPagoDetalleFinal.Add(cuota);
                        _unitOfWork.Commit();

                        //tCronogramaPagosDetalle_mod_CambiosDTO DettalleCambio = new tCronogramaPagosDetalle_mod_CambiosDTO()
                        CronogramaDetalleCambio detalleCambio = new CronogramaDetalleCambio()
                        {
                            //Id = "00000000-0000-0000-0000-000000000000",
                            IdMatriculaCabecera = Json.IdMatriculaCabecera.Value,//??
                            IdCronogramaCabeceraCambio = cronogramaCabeceraCambio.Id,
                            NroCuota = Json.ListaCronograma[i].NroCuota,
                            NroSubcuota = Json.ListaCronograma[i].NroSubCuota,
                            FechaVencimiento = DateTime.ParseExact(Json.ListaCronograma[i].FechaVencimiento.ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", null),
                            Cuota = Json.ListaCronograma[i].Cuota,
                            Mora = Json.ListaCronograma[i].Mora,
                            TipoCambio = Json.ListaCronograma[i].TipoCambio,
                            Moneda = Json.ListaCronograma[i].Moneda,
                            Version = versionNueva.Value, //??   
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = Json.UsuarioNombre,
                            FechaModificacion = DateTime.Now,
                            UsuarioModificacion = Json.UsuarioNombre
                        };
                        _repCronogramaDetalleCambio.Add(detalleCambio);
                        _unitOfWork.Commit();
                        //_tCronogramaPagosDetalle_mod_CambiosService.Insert(DettalleCambio);


                        if (cronogramaActual[i].Cancelado == false)
                        {
                            CronogramaPagoDetalleModLogFinal log = new CronogramaPagoDetalleModLogFinal()
                            {
                                //tCronogramaPagosDetalle_mod_log_FinalDTO log = new tCronogramaPagosDetalle_mod_log_FinalDTO()
                                //Id = "00000000-0000-0000-0000-000000000000",
                                IdMatriculaCabecera = Json.IdMatriculaCabecera,
                                Fecha = DateTime.Now,
                                NroCuota = Json.ListaCronograma[i].NroCuota,
                                NroSubCuota = Json.ListaCronograma[i].NroSubCuota,
                                FechaVencimiento = Json.ListaCronograma[i].FechaVencimiento,
                                TotalPagar = totalMonto,
                                Cuota = Json.ListaCronograma[i].Cuota,
                                Mora = Json.ListaCronograma[i].Mora,
                                MontoPagado = 0,
                                Saldo = totalMonto - Json.ListaCronograma[i].Cuota,
                                Cancelado = Json.ListaCronograma[i].Cancelado,
                                TipoCuota = Json.ListaCronograma[i].TipoCuota,
                                Moneda = Json.ListaCronograma[i].Moneda,
                                
                                FechaPago = null,
                                IdFormaPago = null,
                                FechaPagoBanco = null,
                                Ultimo = false,
                                IdDocumentoPago = null,
                                NroDocumento = null,
                                MonedaPago = null,
                                TipoCambio = null,
                                MensajeSistema = "CUOTA (" + Json.ListaCronograma[i].NroCuota + "," + Json.ListaCronograma[i].NroSubCuota + ") HA VARIADO DE " + cronogramaActual[i].Cuota + " A " + Json.ListaCronograma[i].Cuota,
                                FechaProcesoPago = null,
                                EstadoPrimerLog = null,
                                Version = versionNueva,
                                Aprobado = true,
                                Estado2 = true,

                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                UsuarioCreacion = Json.UsuarioNombre,
                                FechaModificacion = DateTime.Now,
                                UsuarioModificacion = Json.UsuarioNombre
                            };

                            _repCronogramaPagoDetalleModLogFinal.Add(log);
                            _unitOfWork.Commit();


                            //var insertlog = _tCronogramaPagosDetalle_mod_log_FinalService.Insert(log);GuardarCambioMonedaCronograma
                        }

             

                        if (Json.ListaCronograma[i].NroCuota == 1 && Json.ListaCronograma[i].NroSubCuota == 1)
                        {
                            var moneda = _repMoneda.FirstBy(x => x.Id.Equals(Json.IdMoneda));
                            var webMoneda = moneda.DigitoFinanzas;

                            CronogramaPago cronogramaPago = new CronogramaPago();
                            cronogramaPago = _mapper.Map<CronogramaPago>(_repCronogramaPago.FirstBy(x => x.IdMatriculaCabecera == Json.IdMatriculaCabecera));

                            //Id = "00000000-0000-0000-0000-000000000000",
                            cronogramaPago.Moneda = Json.ListaCronograma[i].Moneda;
                            cronogramaPago.TipoCambio = decimal.ToDouble(Json.ListaCronograma[i].TipoCambio);
                            cronogramaPago.TotalPagar = decimal.ToDouble(totalMonto);
                            cronogramaPago.CuotaInicial = Json.ListaCronograma[i].Cuota;
                            cronogramaPago.WebMoneda = webMoneda.ToString();
                            cronogramaPago.WebTipoCambio = decimal.ToDouble(Json.ListaCronograma[i].TipoCambio);
                            cronogramaPago.WebTotalPagar = decimal.ToDouble(totalMonto);
                            cronogramaPago.WebTotalPagarConv = decimal.ToDouble(totalMonto);
                            cronogramaPago.Estado = true;
                            cronogramaPago.FechaModificacion = DateTime.Now;
                            cronogramaPago.UsuarioModificacion = Json.UsuarioNombre;

                            
                            _repCronogramaPago.Update(cronogramaPago);
                            _unitOfWork.Commit();
                        }
                        totalMonto = totalMonto - Json.ListaCronograma[i].Cuota;
                    }

                    scope.Complete();
                }
                //    return true;
                //}
                //var cronogramaactual = _tcrm_CentroCostoService.GetCronogramaFinal(matricula).ToList();
                //var per = _tcrm_CentroCostoService.GetTpersonalsByUserName(User.Identity.Name); 
                //var Rpta = _tCronogramaPagosDetalle_FinalService.InsertCambioMoneda(matricula, per[0].id, per[0].nombre_completo, listaCronograma, cronogramaactual);
                //var jsonResult = Json(new { Result = "OK", Records = "Prueba" }, JsonRequestBehavior.AllowGet);
                return (true);
                //jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }


        public Boolean GuardarPagoPostulante(int idPostulante, string Usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    //integraDBContext _integraDBContext = new integraDBContext();
                    //PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);

                    var alumno = _unitOfWork.PostulanteRepository.ObtenerDatosMatriculaIdPostulante(idPostulante);
                    PagoCuotaCronogramaDTO pagoCuotaCronograma = new PagoCuotaCronogramaDTO()
                    {
                        CodigoMatricula = alumno.CodigoMatricula,
                        NroCuota = 1,
                        NroSubCuota = 1,
                        MontoBase = 0,
                        Mora = 0,
                        MontoPago = 0,
                        MonedaBase = "soles",
                        //Moneda = 20,
                        TipoCambio = 1,
                        FormaPago = 5,
                        Documento = 1,
                        NroDocumento = alumno.IdAlumno.ToString(),
                        NroCuenta = 17,
                        NroCheque = "10594756",
                        Fecha = DateTime.Now,
                        NroDeposito = alumno.IdAlumno.ToString(),
                        usuario = Usuario,
                    };
                    pagoCuotaCronograma.Fecha = pagoCuotaCronograma.Fecha.AddHours(-5);
                    //MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                    //CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
                    var matriculaCabeceraTemp = _unitOfWork.MatriculaCabeceraRepository.GetBy(x => x.CodigoMatricula == alumno.CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                    var versionAprobada = _unitOfWork.CronogramaPagoDetalleFinalRepository.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                    var CronogramaActual = _unitOfWork.CronogramaPagoDetalleFinalRepository.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == versionAprobada.Version).OrderBy(w => w.NroCuota).ThenBy(w => w.NroSubCuota).ToList();

                    //CronogramaPagoDetalleFinalBO CronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalBO();
                    GuardarPago(pagoCuotaCronograma, CronogramaActual, pagoCuotaCronograma.NroCuota, pagoCuotaCronograma.NroSubCuota);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




    }
}





  
                                                                                                                                                                                                                            