using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: MontoPagoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 27/07/2022
    /// <summary>
    /// Gestión general de T_MontoPago
    /// </summary>
    public class MontoPagoRepository : GenericRepository<TMontoPago>, IMontoPagoRepository
    {
        private Mapper _mapper;

        public MontoPagoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMontoPago, MontoPago>(MemberList.None).ReverseMap();
                cfg.CreateMap<TMontoPagoPlataforma, MontoPagoPlataforma>(MemberList.None).ReverseMap();
                cfg.CreateMap<TMontoPagoSuscripcion, MontoPagoSuscripcion>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMontoPago MapeoEntidad(MontoPago entidad)
        {
            try
            {
                TMontoPago modelo = _mapper.Map<TMontoPago>(entidad);

                if (entidad.MontoPagoPlataforma != null && entidad.MontoPagoPlataforma.Count > 0)
                    modelo.TMontoPagoPlataformas = _mapper.Map<List<TMontoPagoPlataforma>>(entidad.MontoPagoPlataforma);

                if (entidad.MontoPagoSuscripcion != null && entidad.MontoPagoSuscripcion.Count > 0)
                    modelo.TMontoPagoSuscripcions = _mapper.Map<List<TMontoPagoSuscripcion>>(entidad.MontoPagoSuscripcion);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMontoPago Add(MontoPago entidad)
        {
            try
            {
                var MontoPago = MapeoEntidad(entidad);
                base.Insert(MontoPago);
                return MontoPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMontoPago Update(MontoPago entidad)
        {
            try
            {
                var MontoPago = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MontoPago.RowVersion = entidadExistente.RowVersion;

                base.Update(MontoPago);
                return MontoPago;
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


        public IEnumerable<TMontoPago> Add(IEnumerable<MontoPago> listadoEntidad)
        {
            try
            {
                List<TMontoPago> listado = new List<TMontoPago>();
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

        public IEnumerable<TMontoPago> Update(IEnumerable<MontoPago> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMontoPago> listado = new List<TMontoPago>();
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
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MontoPago.
        /// </summary>
        /// <returns> List<MontoPagoDTO> </returns>
        public IEnumerable<MontoPagoDTO> ObtenerMontoPago()
        {
            try
            {
                List<MontoPagoDTO> rpta = new List<MontoPagoDTO>();
                var query = @"
                    SELECT
	                    Id,Precio,PrecioLetras,IdMoneda,Matricula,Cuotas,NroCuotas,IdTipoDescuento,IdPrograma,IdTipoPago,IdPais,Vencimiento,PrimeraCuota,
	                    CuotaDoble,Descripcion,VisibleWeb,Paquete,PorDefecto,MontoDescontado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM pla.T_MontoPago
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MontoPagoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MontoPago para mostrarse en combo.
        /// </summary>
        /// <returns> List<MontoPagoComboDTO> </returns>
        public IEnumerable<MontoPagoComboDTO> ObtenerCombo()
        {
            try
            {
                List<MontoPagoComboDTO> rpta = new List<MontoPagoComboDTO>();
                var query = @"SELECT Precio,IdMoneda,IdPrograma,IdTipoPago,IdPais,Descripcion FROM pla.T_MontoPago WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MontoPagoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Montos de Pago asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<MontoPagoDTO> </returns>
        public IEnumerable<MontoPagoOportunidadDTO> ObtenerMontoPagoPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<MontoPagoOportunidadDTO> montosPago = new List<MontoPagoOportunidadDTO>();
                var query = @"
                    SELECT Id,Precio,PrecioLetras,IdMoneda,Matricula,Cuotas,NroCuotas,IdPrograma,IdTipoPago,IdPais,Vencimiento,PrimeraCuota,CuotaDoble,
	                    IdTipoDescuento,Formula,PorcentajeGeneral,PorcentajeMatricula,FraccionesMatricula,PorcentajeCuotas,CuotasAdicionales,NombrePlural,
	                    CuotasTipoPago,Paquete,Nombre,VisibleWeb,MontoDescontado
                    FROM mkt.V_MontosPagos
                    WHERE IdOportunidad = @idOportunidad";
                var resultado = _dapperRepository.QueryDapper(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    montosPago = JsonConvert.DeserializeObject<List<MontoPagoOportunidadDTO>>(resultado)!;
                }
                return montosPago;
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
        /// Obtiene el Paquete relacionado a un Monto Pago especifico.
        /// </summary>
        /// <param name="idMontoPago">Id del Monto Pago</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO ObtenerPaquetePorIdMontoPago(int idMontoPago)
        {
            try
            {
                StringDTO montosPago = new StringDTO();
                var query = @"SELECT Paquete AS Valor FROM pla.T_MontoPago WHERE Id = @idMontoPago AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idMontoPago });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    montosPago = JsonConvert.DeserializeObject<StringDTO>(resultado)!;
                }
                return montosPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Beneficios asociados a un Programa y a un Pais para el Anexo 03 de DocumentoAgenda.
        /// </summary>
        /// <param name="idProgramaGeneral">Id del Programa General</param>
        /// <param name="idPais">Id del Pais</param>
        /// <returns> List<MontoPagoBeneficiosDTO> </returns>
        public IEnumerable<MontoPagoBeneficiosDTO> ObtenerBeneficiosAnexo03(int idProgramaGeneral, int idPais)
        {
            try
            {
                var query = "SELECT DISTINCT IdProgramaGeneral, Paquete, NombrePaquete, Beneficios, IdPais, IdMoneda FROM com.VObtenerBeneficiosAnexo03 WHERE IdProgramaGeneral = @idProgramaGeneral AND IdPais = @idPais and Beneficios is not null";
                var res = _dapperRepository.QueryDapper(query, new { idProgramaGeneral, idPais });
                return JsonConvert.DeserializeObject<List<MontoPagoBeneficiosDTO>>(res);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Probabilidad de Sueldo asociado a la Oportunidad y Pais.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idPais">Id del Pais</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO ObtenerProbabilidadSueldoOportunidad(int idOportunidad, int idPais)
        {
            try
            {
                StringDTO montosPago = new StringDTO();
                var resultado = _dapperRepository.QuerySPFirstOrDefault("com.SP_TAlumnoGetMontoPago", new { idOportunidad, idPais });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    var montoPagoDB = JsonConvert.DeserializeObject<JToken>(resultado);
                    montosPago.Valor = Convert.ToString(montoPagoDB["CuotaPago"]);
                }
                return montosPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Versiones de Monto Pago asociado a una Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<MontoPagoVersionDTO> </returns>
        public IEnumerable<MontoPagoVersionDTO> ObtenerVersionMontoPagoPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<MontoPagoVersionDTO> versiones = new List<MontoPagoVersionDTO>();
                var resultado = _dapperRepository.QuerySPDapper("pla.SP_GetMontoPagoContadoByOportunidadEtiquetasV2", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    versiones = JsonConvert.DeserializeObject<List<MontoPagoVersionDTO>>(resultado);
                }
                return versiones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Versiones de Monto Pago asociado a una Oportunidad junto a los Beneficios
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<MontoPagoVersionBeneficiosDTO> </returns>
        public IEnumerable<MontoPagoVersionBeneficiosDTO> ObtenerVersionMontoPagoBeneficiosPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<MontoPagoVersionBeneficiosDTO> versiones = new List<MontoPagoVersionBeneficiosDTO>();
                var resultado = _dapperRepository.QuerySPFirstOrDefault("pla.SP_GetMontoPagoContadoByOportunidadEtiquetas", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    versiones = JsonConvert.DeserializeObject<List<MontoPagoVersionBeneficiosDTO>>(resultado);
                }
                return versiones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Monto Pago Contado por Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> MontoPagoCompuestoDTO </returns>
        public MontoPagoCompuestoDTO ObtenerMontoPagoContadoPorIdOportunidad(int idOportunidad)
        {
            try
            {
                MontoPagoCompuestoDTO pagoContado = new MontoPagoCompuestoDTO();
                var resultado = _dapperRepository.QuerySPFirstOrDefault("pla.SP_GetMontoPagoContadoByOportunidadId", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    pagoContado = JsonConvert.DeserializeObject<MontoPagoCompuestoDTO>(resultado);
                else
                    return null;
                return pagoContado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Monto Pago Contado por Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> MontoPagoCompuestoDTO </returns>
        public async Task<MontoPagoCompuestoDTO> ObtenerMontoPagoContadoPorIdOportunidadAsync(int idOportunidad)
        {
            try
            {
                MontoPagoCompuestoDTO pagoContado = new MontoPagoCompuestoDTO();
                var resultado = await _dapperRepository.QuerySPFirstOrDefaultAsync("pla.SP_GetMontoPagoContadoByOportunidadId", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    pagoContado = JsonConvert.DeserializeObject<MontoPagoCompuestoDTO>(resultado);
                else
                    return null;
                return pagoContado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera una tabla para Precio en Cuotas de un Programa
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> MontoPagoCompuestoDTO </returns>
        public MontoPagoCompuestoDTO ObtenerMontoPagoPorIdOportunidadParaTabla(int idOportunidad)
        {
            try
            {
                MontoPagoCompuestoDTO pagoContado = new MontoPagoCompuestoDTO();
                var resultado = _dapperRepository.QuerySPFirstOrDefault("pla.SP_GetMontoPagoByIdOportunidad", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    pagoContado = JsonConvert.DeserializeObject<MontoPagoCompuestoDTO>(resultado);
                }
                return pagoContado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Monto Pago por Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<MontoPagoCronogramaCompuestoDTO> </returns>
        public IEnumerable<MontoPagoCronogramaCompuestoDTO> ObtenerPorIdOportunidadV2(int idOportunidad)
        {
            try
            {
                List<MontoPagoCronogramaCompuestoDTO> pagoContado = new List<MontoPagoCronogramaCompuestoDTO>();
                var resultado = _dapperRepository.QuerySPDapper("pla.SP_GetMontoPagoByIdOportunidad", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    pagoContado = JsonConvert.DeserializeObject<List<MontoPagoCronogramaCompuestoDTO>>(resultado);
                }
                return pagoContado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Monto Pago por Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<MontoPagoCronogramaCompuestoDTO> </returns>
        public async Task<IEnumerable<MontoPagoCronogramaCompuestoDTO>> ObtenerPorIdOportunidadV2Async(int idOportunidad)
        {
            try
            {
                List<MontoPagoCronogramaCompuestoDTO> pagoContado = new List<MontoPagoCronogramaCompuestoDTO>();
                var resultado = await _dapperRepository.QuerySPDapperAsync("pla.SP_GetMontoPagoByIdOportunidad", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    pagoContado = JsonConvert.DeserializeObject<List<MontoPagoCronogramaCompuestoDTO>>(resultado);
                }
                return pagoContado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna los paquetes, nombre paquete,precio y pais de un programa general
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> List<MontoPagoModalidadDTO> </returns>
        public List<MontoPagoModalidadDTO> ObtenerMontosPorId(int idPGeneral)
        {
            try
            {
                List<MontoPagoModalidadDTO> montos = new List<MontoPagoModalidadDTO>();
                var resultado = _dapperRepository.QuerySPDapper("pla.SP_MontoPago", new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    montos = JsonConvert.DeserializeObject<List<MontoPagoModalidadDTO>>(resultado);
                }
                return montos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/09/2025
        /// Version: 1.0
        /// <summary>
        /// Retorna los paquetes, nombre paquete,precio y pais de un programa general
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> List<MontoPagoModalidadDTO> </returns>
        public async Task<List<MontoPagoModalidadDTO>> ObtenerMontosPorIdAsync(int idPGeneral)
        {
            try
            {
                List<MontoPagoModalidadDTO> montos = new List<MontoPagoModalidadDTO>();
                var resultado = await _dapperRepository.QuerySPDapperAsync("pla.SP_MontoPago", new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    montos = JsonConvert.DeserializeObject<List<MontoPagoModalidadDTO>>(resultado) ?? new List<MontoPagoModalidadDTO>();
                }
                return montos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        /// Autor: Gilmer Quispe.
        /// Fecha: 10/11/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna los datos de la tabla MontoPago asociado al Id
        /// </summary>
        /// <param name="id">Id de T_MontoPago </param>
        /// <returns> MontoPagoDTO </returns>
        public MontoPago ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                                   Precio,
                                   PrecioLetras,
                                   IdMoneda,
                                   Matricula,
                                   Cuotas,
                                   NroCuotas,
                                   IdTipoDescuento,
                                   IdPrograma,
                                   IdTipoPago,
                                   IdPais,
                                   Vencimiento,
                                   PrimeraCuota,
                                   CuotaDoble,
                                   Descripcion,
                                   VisibleWeb,
                                   Paquete,
                                   PorDefecto,
                                   MontoDescontado,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
	                           FROM pla.T_MontoPago WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<MontoPago>(resultado);
                }
                return new MontoPago(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Versiones por monto de Pago
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> List<MontoPagoEtiquetaDTO> </returns>
        public List<MontoPagoEtiquetaDTO> ObtenerVersionesMontoPago(int idOportunidad)
        {
            try
            {
                string queryVersiones = "pla.SP_GetMontoPagoContadoByOportunidadEtiquetas";
                var resultado = _dapperRepository.QuerySPDapper(queryVersiones, new { idOportunidad });
                return JsonConvert.DeserializeObject<List<MontoPagoEtiquetaDTO>>(resultado)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Versiones de Monto Pago v2
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> List<MontoPagoEtiquetaDTO> </returns>
        public List<MontoPagoEtiquetaDTO> ObtenerVersionesMontoPagoV2(int idOportunidad)
        {
            try
            {
                string queryVersiones = "pla.SP_GetMontoPagoContadoByOportunidadEtiquetasV2";
                var respuesta = _dapperRepository.QuerySPDapper(queryVersiones, new { idOportunidad });
                return JsonConvert.DeserializeObject<List<MontoPagoEtiquetaDTO>>(respuesta)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Monto de Pago Por Id de Oportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> MontoPagoCompuestoDTO </returns>
        public MontoPagoCompuestoDTO ObtenerMontoPagoPorIdOportunidadSP(int idOportunidad)
        {
            try
            {
                string queryMontopadgo = "pla.SP_GetMontoPagoByIdOportunidad";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(queryMontopadgo, new { idOportunidad });
                return JsonConvert.DeserializeObject<MontoPagoCompuestoDTO>(resultado)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene paquete por medio del id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> MontoPagoPaqueteDTO </returns>
        public MontoPagoPaqueteDTO ObtenerPaquete(int id)
        {
            try
            {
                string query = @"SELECT 
                                    Id, Paquete 
                                FROM 
                                    pla.V_TMontoPago_Obtenerpaquete 
                                WHERE 
                                    Id = @Id AND Estado = 1";
                var queryMontoPago = _dapperRepository.FirstOrDefault(query, new { Id = id });
                return JsonConvert.DeserializeObject<MontoPagoPaqueteDTO>(queryMontoPago)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los paquetes completos por IdCentroCosto
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Lista: List<PaqueteCentroCostoDTO </returns>
        public List<PaqueteCentroCostoDTO> ObtenerPaquetesIdCentroCosto(int id)
        {
            try
            {
                string query = @"
                                SELECT 
                                    IdPaquete, Paquete, IdCentroCosto 
                                FROM 
                                    pla.V_TCentrocosto_Obtenerpaquete 
                                WHERE 
                                    IdCentroCosto = @Id";
                var queryMontoPago = _dapperRepository.QueryDapper(query, new { Id = id });
                return JsonConvert.DeserializeObject<List<PaqueteCentroCostoDTO>>(queryMontoPago)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los Montos de Pago Por Programa
        /// </summary>
        /// <param name="idPrograma"></param>
        /// <returns></returns>
        public IEnumerable<MontoPagoDTO> ObtenerPorIdPrograma(int idPrograma)
        {
            try
            {
                var query = @"
                            SELECT 
                                Id, Precio, PrecioLetras, IdMoneda, Matricula, Cuotas, NroCuotas, IdTipoDescuento, IdPrograma, IdTipoPago, 
                                IdPais, Vencimiento, PrimeraCuota, CuotaDoble, Descripcion, VisibleWeb, Paquete, PorDefecto, MontoDescontado 
                            FROM pla.V_TMontoPagoPrograma
                            WHERE 
                                Estado = 1 
                                AND IdPrograma = @idPrograma";
                var resultado = _dapperRepository.QueryDapper(query, new { idPrograma });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<MontoPagoDTO>>(resultado)!;
                }
                return new List<MontoPagoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPR-OMPPP-001@Error en ObtenerMontoPagoPorPrograma() {ex.Message}", ex);
            }
        }
    }
}
