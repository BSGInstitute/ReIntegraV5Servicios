using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: LlamadaWebphoneAsteriskRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 19/10/2022
    /// <summary>
    /// Gestión general de T_LlamadaWebphoneAsterisk
    /// </summary>
    public class LlamadaWebphoneAsteriskRepository : GenericRepository<TLlamadaWebphoneAsterisk>, ILlamadaWebphoneAsteriskRepository
    {
        private Mapper _mapper;

        public LlamadaWebphoneAsteriskRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TLlamadaWebphoneAsterisk, LlamadaWebphoneAsterisk>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TLlamadaWebphoneAsterisk MapeoEntidad(LlamadaWebphoneAsterisk entidad)
        {
            try
            {
                //crea la entidad padre
                TLlamadaWebphoneAsterisk modelo = _mapper.Map<TLlamadaWebphoneAsterisk>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TLlamadaWebphoneAsterisk Add(LlamadaWebphoneAsterisk entidad)
        {
            try
            {
                var LlamadaWebphoneAsterisk = MapeoEntidad(entidad);
                Insert(LlamadaWebphoneAsterisk);
                return LlamadaWebphoneAsterisk;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TLlamadaWebphoneAsterisk Update(LlamadaWebphoneAsterisk entidad)
        {
            try
            {
                var LlamadaWebphoneAsterisk = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                LlamadaWebphoneAsterisk.RowVersion = entidadExistente.RowVersion;

                Update(LlamadaWebphoneAsterisk);
                return LlamadaWebphoneAsterisk;
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


        public IEnumerable<TLlamadaWebphoneAsterisk> Add(IEnumerable<LlamadaWebphoneAsterisk> listadoEntidad)
        {
            try
            {
                List<TLlamadaWebphoneAsterisk> listado = new List<TLlamadaWebphoneAsterisk>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TLlamadaWebphoneAsterisk> Update(IEnumerable<LlamadaWebphoneAsterisk> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TLlamadaWebphoneAsterisk> listado = new List<TLlamadaWebphoneAsterisk>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                Update(listado);
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

        /// Autor: Jonathan Caipo
        /// Fecha: 19/10/2022
        /// Version: 1.0
        /// <summary>
        /// Modifica una llamada del repositorio
        /// </summary>
        /// <param name="idLlamada"></param>
        /// <param name="url"></param>
        /// <param name="nombreUsuario"></param>
        /// <param name="duracionContesto"></param>
        /// <param name="nroBytes"></param>
        /// <returns>ValorIntDTO</returns>
        public ValorIntDTO ModificarLlamadaWebphone(int idLlamada, string url, string nombreUsuario, int duracionContesto, int nroBytes)
        {
            try
            {
                ValorIntDTO respuesta = new ValorIntDTO();
                string query = "com.SP_ModificarLlamadaWebphoneAsterisk";
                string queryRespuesta = _dapperRepository.QuerySPFirstOrDefault(query, new { IdLlamada = idLlamada, Url = url, Usuario = nombreUsuario, DuracionContestado = duracionContesto, Bytes = nroBytes });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<ValorIntDTO>(queryRespuesta)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: --/--/--
        /// Version: 1.0
        /// <summary>
        /// Obtiene el ultimo cdrId generado
        /// </summary>
        /// <returns>ValorIntDTO</returns>
        public IntDTO ObtenerCdrIdRegularizacion()
        {
            try
            {
                const int cdrIdRegularizacion = 20000000;
                IntDTO respuesta = new IntDTO()
                {
                    Valor = cdrIdRegularizacion
                };
                string query = "SELECT TOP 1 CdrId + 1 AS Valor FROM com.T_LlamadaWebphoneAsterisk WHERE CdrId >= @CdrId ORDER BY CdrId DESC";
                string queryRespuesta = _dapperRepository.FirstOrDefault(query, new { CdrId = cdrIdRegularizacion });

                if (!string.IsNullOrEmpty(queryRespuesta) && queryRespuesta != "null")
                {
                    return JsonConvert.DeserializeObject<IntDTO>(queryRespuesta)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 17/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas los registros con cdrid mayor a 20000000
        /// </summary>
        /// <returns>ValorIntDTO</returns>
        public List<LlamadaWebphoneAsterisk> ObtenerRegistrosCdrId()
        {
            try
            {
                const int cdrIdRegularizacion = 20000000;
                string query = @"SELECT Id,
		                FechaInicio,
		                FechaFin,
		                Anexo,
		                TelefonoDestino,
		                IdActividadDetalle,
		                IdLlamadaWebphoneTipo,
		                CdrId,
		                DuracionTimbrado,
		                DuracionContesto,
		                NombreGrabacion,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                IdProveedorNube,
		                Url,
		                NroBytes,
		                FechaSubida,
		                EsEliminado,
		                FechaEliminacion,
		                GrabacionContrato,
		                IdServidorAsterisk 
	                FROM com.T_LlamadaWebphoneAsterisk 
                    WHERE CdrId >= @CdrId AND Estado=1 ORDER BY CdrId ASC";
                string queryRespuesta = _dapperRepository.QueryDapper(query, new { CdrId = cdrIdRegularizacion });

                if (!string.IsNullOrEmpty(queryRespuesta) && queryRespuesta != "[]")
                {
                    return JsonConvert.DeserializeObject<List<LlamadaWebphoneAsterisk>>(queryRespuesta)!;
                }
                return new List<LlamadaWebphoneAsterisk>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
