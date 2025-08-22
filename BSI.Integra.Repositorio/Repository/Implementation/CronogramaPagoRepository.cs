using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CronogramaPagoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 05/08/2022
    /// <summary>
    /// Gestión general de T_CronogramaPago
    /// </summary>
    public class CronogramaPagoRepository : GenericRepository<TCronogramaPago>, ICronogramaPagoRepository
    {
        private Mapper _mapper;

        public CronogramaPagoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCronogramaPago, CronogramaPago>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCronogramaPago MapeoEntidad(CronogramaPago entidad)
        {
            try
            {
                //crea la entidad padre
                TCronogramaPago modelo = _mapper.Map<TCronogramaPago>(entidad);

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

        public TCronogramaPago Add(CronogramaPago entidad)
        {
            try
            {
                var CronogramaPago = MapeoEntidad(entidad);
                base.Insert(CronogramaPago);
                return CronogramaPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCronogramaPago Update(CronogramaPago entidad)
        {
            try
            {
                var CronogramaPago = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CronogramaPago.RowVersion = entidadExistente.RowVersion;

                base.Update(CronogramaPago);
                return CronogramaPago;
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


        public IEnumerable<TCronogramaPago> Add(IEnumerable<CronogramaPago> listadoEntidad)
        {
            try
            {
                List<TCronogramaPago> listado = new List<TCronogramaPago>();
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

        public IEnumerable<TCronogramaPago> Update(IEnumerable<CronogramaPago> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCronogramaPago> listado = new List<TCronogramaPago>();
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
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CronogramaPago.
        /// </summary>
        /// <returns> List<CronogramaPagoDTO> </returns>
        public IEnumerable<CronogramaPagoDTO> ObtenerCronogramaPago()
        {
            try
            {
                List<CronogramaPagoDTO> rpta = new List<CronogramaPagoDTO>();
                var query = @"
                    SELECT Id,IdMatriculaCabecera,IdAlumno,IdPEspecifico,Periodo,Moneda,AcuerdoPago,TipoCambio,TotalPagar,NroCuotas,FechaIniPago,
	                    Enviado,Observaciones,ConCuotaInicial,CuotaInicial,CadaNDias,NDias,WebMoneda,WebTipoCambio,WebTotalPagar,WebTotalPagarConv,
	                    UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM fin.T_CronogramaPago
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CronogramaPagoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CronogramaPago para mostrarse en combo.
        /// </summary>
        /// <returns> List<CronogramaPagoComboDTO> </returns>
        public IEnumerable<CronogramaPagoComboDTO> ObtenerCombo()
        {
            try
            {
                List<CronogramaPagoComboDTO> rpta = new List<CronogramaPagoComboDTO>();
                var query = @"SELECT Id,IdMatriculaCabecera FROM fin.T_CronogramaPago WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CronogramaPagoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las cuotas e informacion de programas asociados a una Matricula Cabecera.
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de Matricula Cabecera</param>
        /// <returns> ProgramaCuotasDTO </returns>
        public ProgramaCuotasDTO ObtenerProgramaCuotasPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                ProgramaCuotasDTO cuotas = new ProgramaCuotasDTO();
                var query = @"
                    SELECT IdBusqueda,NombreCurso,IdPespecifico,NombrePespecifico,IdMatricula,CodigoMatricula,TipoPrograma,DuracionPgeneral,
	                    DuracionPespecifico,NumeroCuotas,WebMoneda,TotalPagar,WebTotalPagar,WebTipoCambio,EstadoCronogramaMod
                    FROM com.V_CuotascronogramaPorMatricula
                    WHERE IdMatricula = @idMatriculaCabecera AND EstadoCronogramaMod = 1";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    cuotas = JsonConvert.DeserializeObject<ProgramaCuotasDTO>(resultadoQuery);
                }
                return cuotas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el comprobante de Pago reciente de la matricula.
        /// </summary>
        /// <param name="idMatricula">Id de Matricula Cabecera</param>
        /// <returns> ProgramaCuotasDTO </returns>
        public ComprobanteRecienteDTO ObtenerComprobanteReciente(int idMatricula = 0 )
        {
            try
            {
                ComprobanteRecienteDTO result = new ComprobanteRecienteDTO();
                string _queryInsertar = "[fin].[SP_ObtenerComprobantePagoAlumnoReciente]";
                var queryResult = _dapperRepository.QuerySPFirstOrDefault(_queryInsertar, new
                {
                    IdMatriculaCabecera = idMatricula
                });
                if (!string.IsNullOrEmpty(queryResult) && !queryResult.Contains("[]") && queryResult != "null")
                {
                    result = JsonConvert.DeserializeObject<ComprobanteRecienteDTO>(queryResult);

                }
                return result;
            }
            catch (Exception ex)
            {
                ComprobanteRecienteDTO result = new ComprobanteRecienteDTO();
                return result;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el comprobante de Pago reciente de la matricula.
        /// </summary>
        /// <param name="idMatricula">Id de Matricula Cabecera</param>
        /// <returns> ProgramaCuotasDTO </returns>
        public bool ActualizarCompromisoPago(int IdMatriculaCabecera, string Usuario)
        {
            var respuesta = false;
            var resultado = _dapperRepository.QuerySPDapper("fin.SP_ActualizarCompromisoPago", new
            {
                IdMatriculaCabecera = IdMatriculaCabecera,
                Usuario = Usuario
            });

            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
            {
                respuesta = true;
            }
            return respuesta;
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el idmatricula por codigo de Matricula.
        /// </summary>
        /// <param name="idMatricula">Id de Matricula Cabecera</param>
        /// <returns> ProgramaCuotasDTO </returns>
        public int ObtenerIdporCodigo(string codmat)
        {
            try
            {
                int result = 0;
                var combaso = new ValorIntDTO();
                string _queryInsertar = "SELECT TOP 1 Id FROM [fin].[T_MatriculaCabecera] WHERE CodigoMatricula=@codmat";
                var queryResult = _dapperRepository.FirstOrDefault(_queryInsertar, new
                {
                    codmat = codmat
                });
                if (!string.IsNullOrEmpty(queryResult) && !queryResult.Contains("[]") && queryResult != "null")
                {
                    combaso = JsonConvert.DeserializeObject<ValorIntDTO>(queryResult);
                    result = combaso.Id;
                }
                return result;
            }
            catch (Exception ex)
            {
                int result = 0;
                return result;
            }
        }

        public PrecioFinalProgramaAlumnoDTO ObtenerPrecioFinalProgramaAlumno (string codigoMatricula)
        {
            PrecioFinalProgramaAlumnoDTO respuesta = null;
            var resultado = _dapperRepository.QuerySPFirstOrDefault("ope.SP_ObtenerPrecioFinalProgramaAlumno", new
            {
                CodigoMatricula = codigoMatricula
            });

            if (!string.IsNullOrEmpty(resultado))
            {
                respuesta = JsonConvert.DeserializeObject<PrecioFinalProgramaAlumnoDTO>(resultado);
            }
            return respuesta;
        }

    }
}
