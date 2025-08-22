using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CronogramaPagoDetalleFinalRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_CronogramaPagoDetalleFinal
    /// </summary>
    public class CronogramaPagoDetalleFinalRepository : GenericRepository<TCronogramaPagoDetalleFinal>, ICronogramaPagoDetalleFinalRepository
    {
        private Mapper _mapper;

        public CronogramaPagoDetalleFinalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCronogramaPagoDetalleFinal, CronogramaPagoDetalleFinal>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCronogramaPagoDetalleFinal MapeoEntidad(CronogramaPagoDetalleFinal entidad)
        {
            try
            {
                //crea la entidad padre
                TCronogramaPagoDetalleFinal modelo = _mapper.Map<TCronogramaPagoDetalleFinal>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCronogramaPagoDetalleFinal Add(CronogramaPagoDetalleFinal entidad)
        {
            try
            {
                var CronogramaPagoDetalleFinal = MapeoEntidad(entidad);
                base.Insert(CronogramaPagoDetalleFinal);
                return CronogramaPagoDetalleFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCronogramaPagoDetalleFinal Update(CronogramaPagoDetalleFinal entidad)
        {
            try
            {
                var CronogramaPagoDetalleFinal = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CronogramaPagoDetalleFinal.RowVersion = entidadExistente.RowVersion;

                base.Update(CronogramaPagoDetalleFinal);
                return CronogramaPagoDetalleFinal;
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
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TCronogramaPagoDetalleFinal> Add(IEnumerable<CronogramaPagoDetalleFinal> listadoEntidad)
        {
            try
            {
                List<TCronogramaPagoDetalleFinal> listado = new List<TCronogramaPagoDetalleFinal>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TCronogramaPagoDetalleFinal> Update(IEnumerable<CronogramaPagoDetalleFinal> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCronogramaPagoDetalleFinal> listado = new List<TCronogramaPagoDetalleFinal>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CronogramaPagoDetalleFinal.
        /// </summary>
        /// <returns> List<CronogramaPagoDetalleFinalDTO> </returns>
        public IEnumerable<CronogramaPagoDetalleFinalDTO> ObtenerCronogramaPagoDetalleFinal()
        {
            try
            {
                List<CronogramaPagoDetalleFinalDTO> rpta = new List<CronogramaPagoDetalleFinalDTO>();
                var query = @"
                    SELECT Id,IdMatriculaCabecera,NroCuota,NroSubCuota,FechaVencimiento,TotalPagar,Cuota,Saldo,Mora,MontoPagado,
	                    Cancelado,TipoCuota,Moneda,FechaPago,IdFormaPago,IdCuenta,FechaPagoBanco,Enviado,Observaciones,IdDocumentoPago,
	                    NroDocumento,MonedaPago,TipoCambio,CuotaDolares,FechaProcesoPago,Version,Aprobado,FechaDeposito,UsuarioCreacion,
	                    UsuarioModificacion,FechaCreacion,FechaModificacion,FechaProcesoPagoReal,FechaIngresoEnCuenta,FechaEfectivoDisponible,
	                    MoraTarifario,FechaCompromiso1,FechaCompromiso2,FechaCompromiso3,FechaGeneracionCompromiso1,FechaGeneracionCompromiso2,
	                    FechaGeneracionCompromiso3,UsuarioCoordinadorAcademico,IdPersonal_CoordinadorCobranza,MonedaMoraTarifario
                    FROM fin.T_CronogramaPagoDetalleFinal
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CronogramaPagoDetalleFinalDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CronogramaPagoDetalleFinal para mostrarse en combo.
        /// </summary>
        /// <returns> List<CronogramaPagoDetalleFinalComboDTO> </returns>
        public IEnumerable<CronogramaPagoDetalleFinalComboDTO> ObtenerCombo()
        {
            try
            {
                List<CronogramaPagoDetalleFinalComboDTO> rpta = new List<CronogramaPagoDetalleFinalComboDTO>();
                var query = @"SELECT
	                    Id,IdMatriculaCabecera,NroCuota,NroSubCuota
                    FROM fin.T_CronogramaPagoDetalleFinal
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CronogramaPagoDetalleFinalComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las cuotas ordenadas asociadas a una Matricula Cabecera.
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matriula Cabecera</param>
        /// <returns> List<CronogramaPagoDetalleFinalCuotaDTO> </returns>
        public IEnumerable<CronogramaPagoDetalleFinalCuotaDTO> ObtenerListaCuotaPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                List<CronogramaPagoDetalleFinalCuotaDTO> cuotas = new List<CronogramaPagoDetalleFinalCuotaDTO>();
                var query = @"
                    SELECT FechaVencimiento,Cuota,Mora,NroCuota,Moneda,Cancelado,MontoCuotaDescuento
                    FROM fin.V_TCronogramaPagoDetalleFinal_CuotasVentas
                    WHERE IdMatriCulaCabecera = @idMatriculaCabecera AND Estado = 1
                    ORDER BY NroCuota";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    cuotas = JsonConvert.DeserializeObject<List<CronogramaPagoDetalleFinalCuotaDTO>>(resultadoQuery);
                }
                return cuotas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// utor: Gilmer Quispe
        /// Fecha: 27/09/2022
        /// <summary>
        /// Obtiene el Cronograma Finanzas por la version y el IdMatriculaCabecera
        /// </summary>
        /// <param name="version"> Version </param>
        /// <param name="idMatriculaCabecera"> Id de la Matricula </param>
        /// <returns> Lista Cronograma del Alumno : List<CronogramaPagoDetalleFinalDTO> </returns>
        public List<CronogramaPagoDetalleFinalFinanzasDTO> ObtenerCronogramaFinanzasPorVersionYMCabecera(int version, int idMatriculaCabecera)
        {
            try
            {
                var cronogramaPagoDetalleFinals = new List<CronogramaPagoDetalleFinalFinanzasDTO>();
                string _query = $"EXECUTE FIN.SP_ObtenerCronogramaFinanzas @Version = {version}, @IdMatriculaCabecera = {idMatriculaCabecera}";
                var registroDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(registroDB) && !registroDB.Contains("[]"))
                {
                    cronogramaPagoDetalleFinals = JsonConvert.DeserializeObject<List<CronogramaPagoDetalleFinalFinanzasDTO>>(registroDB);
                }
                return cronogramaPagoDetalleFinals;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 16/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CronogramaPagoDetalleFinal por IdMatriculaCabecera
        /// </summary>
        /// <returns> List<CronogramaPagoDetalleFinalDTO> </returns>
        public IEnumerable<CronogramaPagoDetalleFinalDTO> ObtenerCronograma(int idMatriculaCabecera)
        {
            try
            {
                List<CronogramaPagoDetalleFinalDTO> cronograma = new List<CronogramaPagoDetalleFinalDTO>();
                var query = @"
                    SELECT 
                        Id, IdMatriculaCabecera, NroCuota, NroSubCuota, FechaVencimiento, TotalPagar, Cuota, Saldo, Mora, MontoPagado, Cancelado, 
                        TipoCuota, Moneda, FechaPago, IdFormaPago, IdCuenta, FechaPagoBanco, Enviado, Observaciones, IdDocumentoPago, NroDocumento, 
                        MonedaPago, TipoCambio,CuotaDolares, FechaProcesoPago, Version, Aprobado, FechaDeposito, UsuarioCreacion, UsuarioModificacion, 
                        FechaCreacion, FechaModificacion, FechaProcesoPagoReal, FechaIngresoEnCuenta, FechaEfectivoDisponible, MoraTarifario, 
                        FechaCompromiso1, FechaCompromiso2, FechaCompromiso3, FechaGeneracionCompromiso1, FechaGeneracionCompromiso2,
	                    FechaGeneracionCompromiso3, UsuarioCoordinadorAcademico, IdPersonal_CoordinadorCobranza, MonedaMoraTarifario
                    FROM 
                        fin.T_CronogramaPagoDetalleFinal
                    WHERE 
                        IdMatriculaCabecera = @IdMatriculaCabecera AND Aprobado = 1 ORDER BY Version DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    cronograma = JsonConvert.DeserializeObject<List<CronogramaPagoDetalleFinalDTO>>(resultado)!;
                }
                return cronograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Lista del Programa de cuotas por medio del idMatricula
        /// </summary>
        /// <param name="idMatricula"></param>
        /// <returns> List<ProgramaListaCuotaDTO> </returns>
        public List<ProgramaListaCuotaDTO> ObtenerListaCuotaPrograma(int idMatricula)
        {
            try
            {
                string query = @"SELECT 
                                    FechaVencimiento, Cuota, Mora, NroCuota, Moneda, Cancelado, MontoCuotaDescuento 
                                 FROM 
                                    fin.V_TCronogramaPagoDetalleFinal_CuotasVentas 
                                 WHERE 
                                    IdMatriCulaCabecera = @IdMatricula AND Estado = 1 ORDER BY NroCuota";
                var queryCronogramaPagoDetalleMod = _dapperRepository.QueryDapper(query, new { IdMatricula = idMatricula });
                return JsonConvert.DeserializeObject<List<ProgramaListaCuotaDTO>>(queryCronogramaPagoDetalleMod)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Autor: Gkilmer Quispe
        ///Fecha: 02/12/2023
        /// <summary>
        /// Obtiene versiones de Fecha de Compromiso
        /// </summary>
        /// <param name="IdCuota"> Id de la cuota </param>
        /// <returns> Lista de Personal por nombre Registrados : List<ResultadoFechaCompromiso> </returns>
        public List<ResultadoFechaCompromiso> ObtenerVersionesFechaCompromiso(int idCuota)
        {
            try
            {
                var registroDB = _dapperRepository.QuerySPDapper("fin.SP_ObtenerVersionesFechaCompromiso", new { IdCuota = idCuota });
                var valor = JsonConvert.DeserializeObject<List<ResultadoFechaCompromiso>>(registroDB);
                return valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Autor: Juan D. Huanaco Quispe
        ///Fecha: 03/05/2024
        /// <summary>
        /// Obtiene los compromisos de una cuota
        /// </summary>
        /// <param name="IdCuota"> Id de la cuota </param>
        /// <returns> Lista de Personal por nombre Registrados : List<ResultadoFechaCompromiso> </returns>
        public List<AgendaAtcCompromiso> ObtenerAgendaAtcCompromiso(int idCuota)
        {
            try
            {
                var registroDB = _dapperRepository.QuerySPDapper("fin.SP_AgendaAtcObtenerVersionesFechaCompromiso", new { IdCronogramaPagoDetalleFinal = idCuota });
                var valor = JsonConvert.DeserializeObject<List<AgendaAtcCompromiso>>(registroDB);
                return valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 20/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtener Cronograma Finanzas del Alumno
        /// </summary>
        /// <param name="version"> Version </param>
        /// <param name="idMatriculaCabecera"> Id de la Matricula </param>
        /// <returns> Lista Cronograma del Alumno : List<CronogramaPagoDetalleFinalDTO> </returns>
        public List<CronogramaPagoDetalleFinalDTO> ObtenerCronogramaFinanzas(int version, int idMatriculaCabecera)
        {
            try
            {
                List<CronogramaPagoDetalleFinalDTO> cronogramaPagoDetalleFinals = new List<CronogramaPagoDetalleFinalDTO>();
                string query = $"EXECUTE FIN.SP_ObtenerCronogramaFinanzas @Version = {version}, @IdMatriculaCabecera = {idMatriculaCabecera}";
                var registroDB = _dapperRepository.QueryDapper(query, null);
                if (!registroDB.Contains("[]"))
                {
                    cronogramaPagoDetalleFinals = JsonConvert.DeserializeObject<List<CronogramaPagoDetalleFinalDTO>>(registroDB)!;
                }
                return cronogramaPagoDetalleFinals;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Juan Diego Huanaco Quispe
        /// Fecha: 22/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtener las moras calculadas de las cuotas del alumno.
        /// </summary>
        /// <param name="idMatriculaCabecera"> Id de la Matricula </param>
        /// <returns> Lista Cronograma del Alumno : List<CronogramaPagoDetalleFinalDTO> </returns>
        public List<CuotaDataAdicionalDTO> ObtenerCuotaDataAdicional(int idMatriculaCabecera)
        {
            try
            {
                List<CuotaDataAdicionalDTO> morasCalculadas = new List<CuotaDataAdicionalDTO>();
                string query = $"SELECT IdCuota, Cuota, MoraCalculada FROM pw.V_CronogramaCuotaMatriculadoAlumno WHERE IdMatriculaCabecera = @IdMatriculaCabecera";
                var registroDB = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!registroDB.Contains("[]"))
                    morasCalculadas = JsonConvert.DeserializeObject<List<CuotaDataAdicionalDTO>>(registroDB)!;
                
                return morasCalculadas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Adriana Chipana 



        /// <summary>
        /// Obtiene una lista de cuotas crep
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public List<ListadoCuotaCrepDTO> ObtenerCuotasCrepPorCodigoMatricula(int idMatriculaCabecera, int? version)
        {
            try
            {
                List<ListadoCuotaCrepDTO> listadoCuotaCreps = new List<ListadoCuotaCrepDTO>();
                var _query = "SELECT Id, NroCuota, NroSubCuota,FechaVencimiento,moneda,Cuota,Mora, Total, Enviado,fin.F_ObtenerFechaAnteriorCuota(@idMatriculaCabecera, Nrocuota, NroSubCuota) AS FechaAnterior, Adicional,Cancelado FROM fin.V_TCronogramaPagoDetalleFinal_ObtenerTodo WHERE IdMatriculaCabecera = @idMatriculaCabecera AND version = @version AND  ( Cancelado is not null  OR IdFormaPago IN ( 1, 2, 6, 7 ) ) ORDER  BY NroCuota, NroSubCuota";
                var listadoCuotaCrepsDB = _dapperRepository.QueryDapper(_query, new { idMatriculaCabecera, version });

                if (!string.IsNullOrEmpty(listadoCuotaCrepsDB) && !listadoCuotaCrepsDB.Contains("[]"))
                {
                    listadoCuotaCreps = JsonConvert.DeserializeObject<List<ListadoCuotaCrepDTO>>(listadoCuotaCrepsDB);
                }
                return listadoCuotaCreps;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public List<ListadoCuotasModificadasDTO> ObtenerCuotas(int idMatriculaCabecera, int? version)
        {
            try
            {
                List<ListadoCuotasModificadasDTO> listadoCuotaCreps = new List<ListadoCuotasModificadasDTO>();
                var _query = "SELECT case(mora) when 0.00 then '1' + right('0' + convert(varchar,NroCuota),2) + right('0' + convert(varchar,NroSubCuota),2) + " +
                    "'XXXXXX'  else  '2' + right('0' + convert(varchar,NroCuota),2) + right('0' + convert(varchar,NroSubCuota),2) + right('000000' + replace(convert(varchar,Mora),'.',''),6) " +
                    "end CodigoEspecial,NroCuota,NroSubCuota,round(Cuota,2) + ROUND(Mora, 2) Cuota, convert(varchar,FechaVencimiento,103) FechaVencimiento, fin.F_ObtenerFechaAnteriorCuota(@idMatriculaCabecera, Nrocuota, NroSubCuota)" +
                    " AS FechaAnterior, Enviado FROM fin.V_TCronogramaPagoDetalleFinal_ObtenerTodo WHERE IdMatriculaCabecera = @idMatriculaCabecera AND version = @version AND ( Cancelado = 0  OR IdFormaPago IN ( 1, 2, 6, 7 ) )";
                var listadoCuotaCrepsDB = _dapperRepository.QueryDapper(_query, new { idMatriculaCabecera, version });

                if (!string.IsNullOrEmpty(listadoCuotaCrepsDB) && !listadoCuotaCrepsDB.Contains("[]"))
                {
                    listadoCuotaCreps = JsonConvert.DeserializeObject<List<ListadoCuotasModificadasDTO>>(listadoCuotaCrepsDB);
                }
                return listadoCuotaCreps;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Actuliza los datos enviados
        /// </summary>
        /// <param name="CodigoMatricula"></param>
        /// <param name="NroCuota"></param>
        /// <param name="NroSubCuota"></param>
        /// <returns></returns>
        public int ActualizarEnviado(string CodigoMatricula, int NroCuota, int NroSubCuota)
        {
            try
            {
                var registroDB = _dapperRepository.QuerySPFirstOrDefault("fin.SP_ActualizarEnviadoCronograma", new { CodigoMatricula, NroCuota, NroSubCuota });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Actuliza los datos enviados
        /// </summary>
        /// <param name="CodigoMatricula"></param>
        /// <param name="NroCuota"></param>
        /// <param name="NroSubCuota"></param>
        /// <returns></returns>
        public int ActualizarUltimo(string CodigoMatricula, int NroCuota, int NroSubCuota)
        {
            try
            {
                var registroDB = _dapperRepository.QuerySPFirstOrDefault("fin.SP_ActualizarUltimoCronograma", new { CodigoMatricula, NroCuota, NroSubCuota });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public int PagarCuotaCDPG_CtoFinal(string idMat, int NroCuota, int NroSubCuota, DateTime FechaPago, double MontoPagado, double MoraBanco, string MonedaPago, string NroDoc, int IdPeriodo, string uaurio, ref string Excepcion, int? IdTipoComprobante, string? NroDocumentoComprobante, string? NombreRazonSocial)
        {
            try
            {
                var registroDB = _dapperRepository.
                    QuerySPFirstOrDefault("fin.SP_PagarCuotaCDPGCtoFinalV5", new
                    {
                        IdMat = idMat,
                        NroCuota,
                        NroSubCuota,
                        FechaPago,
                        MontoPagado,
                        MoraBanco,
                        MonedaPago,
                        NroDoc,
                        IdPeriodo,
                        UsuarioMod = uaurio,
                        IdTipoComprobante,
                        NroDocumentoComprobante,
                        NombreRazonSocial
                    });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                Excepcion = registroDB;
                return valor.Resultado;
            }
            catch (Exception e)
            {
                Excepcion = JsonConvert.SerializeObject(e);

                return -1;
            }
        }

        public List<MatriculaControlDocumentoDTO> ObtenerDocumentosFiltrado(FiltroControlDocumentoDTO filtro)
        {
            try
            {
                List<MatriculaControlDocumentoDTO> matriculasControlDocumentos = new List<MatriculaControlDocumentoDTO>();
                var matriculasControlDocumentosDB = _dapperRepository.QuerySPDapper("fin.SP_ControlDocumentosAlumnosFiltro", new { filtro.IdAlumno, filtro.IdPEspecifico, filtro.IdAsesor, filtro.IdCoordinador, filtro.IdMatriculaCabecera, filtro.Estado });
                if (!matriculasControlDocumentosDB.Contains("[]") && !string.IsNullOrEmpty(matriculasControlDocumentosDB))
                {
                    matriculasControlDocumentos = JsonConvert.DeserializeObject<List<MatriculaControlDocumentoDTO>>(matriculasControlDocumentosDB);
                }
                return matriculasControlDocumentos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor : griselberto Huaman
        /// fecha creacion : 12-02-2023
        /// <summary>
        /// Obtiene la maxima version del cronograma
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public int ObtenerMaximaVersionCronograma(int idMatriculaCabecera)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"fin.SP_ObtenerVersionMaximaCronograma";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return _resultado.Valor.Value;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha creación: 12/08/2024
        /// <summary>
        /// Obtiene detalle de cuota
        /// </summary>
        /// <param name="IdMatriculaCabecera"></param>
        /// <param name="NroCuota"></param>
        /// <param name="NroSubCuota"></param>
        /// <param name="IdFormaPago"></param>
        /// <returns> Detalle de transacción de cuotas </returns>
        public IEnumerable<DetalleCuotasTransaccionAuditoriaDTO> ObtenerDetalleCuotasTransaccionAuditoria(FiltroDetalleCuotasTransaccionAuditoriaDTO filtroDetalle)
        {
            try
            {
                var rpta = new List<DetalleCuotasTransaccionAuditoriaDTO>();
                var query = "SELECT * FROM ope.V_ObtenerDetalleCuotasTransaccionAuditoria WHERE IdMatriculaCabecera = @IdMatricula AND NroCuota = @NroCuota AND NroSubCuota = @NroSubCuota AND FechaPago = @Fecha";
                var resultado = _dapperRepository.QueryDapper(query, new { IdMatricula = filtroDetalle.IdMatriculaCabecera, NroCuota = filtroDetalle.NroCuota, NroSubCuota = filtroDetalle.NroSubCuota, Fecha = filtroDetalle.FechaPagoFormateado });
                if (!string.IsNullOrEmpty(resultado))
                {
                    rpta = JsonConvert.DeserializeObject<List<DetalleCuotasTransaccionAuditoriaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha creación: 12/08/2024
        /// <summary>
        /// Obtiene detalle de matrícula
        /// </summary>
        /// <param name="IdMontoPagoCronograma"></param>
        /// <param name="NroCuota"></param>
        /// <returns> Detalle de transacción de matrícula </returns>
        public IEnumerable<DetalleMatriculaTransaccionAuditoriaDTO> ObtenerDetalleMatriculaTransaccionAuditoria(FiltroDetalleMatriculaTransaccionAuditoriaDTO filtroDetalle)
        {
            try
            {
                var rpta = new List<DetalleMatriculaTransaccionAuditoriaDTO>();
                var query = "SELECT * FROM com.V_ObtenerDetalleMatriculaTransaccionAuditoria WHERE idMontoPagoCronograma = @IdMonto AND NroCuota = @NroCuota";
                var resultado = _dapperRepository.QueryDapper(query, new { IdMonto = filtroDetalle.IdMontoPagoCronograma, NroCuota = filtroDetalle.NroCuota });
                if (!string.IsNullOrEmpty(resultado))
                {
                    rpta = JsonConvert.DeserializeObject<List<DetalleMatriculaTransaccionAuditoriaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha creación: 25/09/2024
        /// <summary>
        /// Actualiza tabla fin.T_CronogramaPagoDetalleFinal columna EnviadoSiigo por SP
        /// </summary>
        /// <param name="Id" ></param>
        /// <returns>  </returns>
        public bool ActualizaEnviadoSiigo(int id)
        {
            try
            {
                _dapperRepository.QuerySPFirstOrDefault("fin.SP_ActualizarCronogramaPagoDetalleFinalEnviadoSiigo", new { id });
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
