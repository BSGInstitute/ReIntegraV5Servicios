using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DetalleOportunidadCompetidorRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 04/07/2022
    /// <summary>
    /// Gestión general de T_DetalleOportunidadCompetidor
    /// </summary>
    public class DetalleOportunidadCompetidorRepository : GenericRepository<TDetalleOportunidadCompetidor>, IDetalleOportunidadCompetidorRepository
    {
        private Mapper _mapper;

        public DetalleOportunidadCompetidorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDetalleOportunidadCompetidor, DetalleOportunidadCompetidor>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TDetalleOportunidadCompetidor MapeoEntidad(DetalleOportunidadCompetidor entidad)
        {
            try
            {
                //crea la entidad padre
                TDetalleOportunidadCompetidor modelo = _mapper.Map<TDetalleOportunidadCompetidor>(entidad);

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

        public TDetalleOportunidadCompetidor Add(DetalleOportunidadCompetidor entidad)
        {
            try
            {
                var DetalleOportunidadCompetidor = MapeoEntidad(entidad);
                base.Insert(DetalleOportunidadCompetidor);
                return DetalleOportunidadCompetidor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDetalleOportunidadCompetidor Update(DetalleOportunidadCompetidor entidad)
        {
            try
            {
                var DetalleOportunidadCompetidor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DetalleOportunidadCompetidor.RowVersion = entidadExistente.RowVersion;

                base.Update(DetalleOportunidadCompetidor);
                return DetalleOportunidadCompetidor;
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


        public IEnumerable<TDetalleOportunidadCompetidor> Add(IEnumerable<DetalleOportunidadCompetidor> listadoEntidad)
        {
            try
            {
                List<TDetalleOportunidadCompetidor> listado = new List<TDetalleOportunidadCompetidor>();
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

        public IEnumerable<TDetalleOportunidadCompetidor> Update(IEnumerable<DetalleOportunidadCompetidor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDetalleOportunidadCompetidor> listado = new List<TDetalleOportunidadCompetidor>();
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
        /// Fecha: 04/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DetalleOportunidadCompetidor.
        /// </summary>
        /// <returns> List<DetalleOportunidadCompetidorDTO> </returns>
        public IEnumerable<DetalleOportunidadCompetidorDTO> ObtenerDetalleOportunidadCompetidor()
        {
            try
            {
                List<DetalleOportunidadCompetidorDTO> rpta = new List<DetalleOportunidadCompetidorDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdOportunidadCompetidor,
	                    IdCompetidor,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_DetalleOportunidadCompetidor
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DetalleOportunidadCompetidorDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DetalleOportunidadCompetidor para mostrarse en combo.
        /// </summary>
        /// <returns> List<DetalleOportunidadCompetidorComboDTO> </returns>
        public IEnumerable<DetalleOportunidadCompetidorComboDTO> ObtenerCombo()
        {
            try
            {
                List<DetalleOportunidadCompetidorComboDTO> rpta = new List<DetalleOportunidadCompetidorComboDTO>();
                var query = @"SELECT
	                            DOC.Id,
	                            C.Nombre AS Competidor,
	                            OC.Id AS IdOportunidad
                            FROM com.T_DetalleOportunidadCompetidor AS DOC
                            INNER JOIN pla.T_Competidor AS C
	                            ON DOC.IdCompetidor = C.Id
	                            AND C.Estado = 1
                            INNER JOIN com.T_OportunidadCompetidor AS OC
	                            ON DOC.IdOportunidadCompetidor = OC.Id
	                            AND OC.Estado = 1
                            WHERE DOC.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DetalleOportunidadCompetidorComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id y Nombre de las Empresas Competidoras asociadas a una OportunidadCompetidor.
        /// </summary>
        /// <param name="idOportunidadCompetidor">Id de Oportunidad Competidor</param>
        /// <returns> List<DetalleOportunidadCompetidorEmpresaDTO> </returns>
        public IEnumerable<DetalleOportunidadCompetidorEmpresaDTO> ObtenerEmpresaCompetidoraPorIdOportunidadCompetidor(int idOportunidadCompetidor)
        {
            try
            {
                List<DetalleOportunidadCompetidorEmpresaDTO> empresas = new List<DetalleOportunidadCompetidorEmpresaDTO>();
                var query = @"SELECT Id,Nombre FROM com.V_EmpresaDetalleOportunidadCompetidor WHERE IdOportunidadCompetidor = @idOportunidadCompetidor";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idOportunidadCompetidor });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    empresas = JsonConvert.DeserializeObject<List<DetalleOportunidadCompetidorEmpresaDTO>>(resultadoQuery);
                }
                return empresas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_DetalleOportunidadCompetidor asociados a OportunidadCompetidor.
        /// </summary>
        /// <param name="idOportunidadCompetidor">Id de Oportunidad Competidor</param>
        /// <returns> List<DetalleOportunidadCompetidorDTO> </returns>
        public IEnumerable<DetalleOportunidadCompetidorDTO> ObtenerDetallePorIdOportunidadCompetidor(int idOportunidadCompetidor)
        {
            try
            {
                List<DetalleOportunidadCompetidorDTO> rpta = new List<DetalleOportunidadCompetidorDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdOportunidadCompetidor,
	                    IdCompetidor,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_DetalleOportunidadCompetidor
                    WHERE Estado = 1
                        AND IdOportunidadCompetidor = @idOportunidadCompetidor";
                var resultado = _dapperRepository.QueryDapper(query, new { idOportunidadCompetidor });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DetalleOportunidadCompetidorDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: F
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_DetalleOportunidadCompetidor asociados a OportunidadCompetidor.
        /// </summary>
        /// <param name="idOportunidadCompetidor">Id de Oportunidad Competidor</param>
        /// <returns> List<DetalleOportunidadCompetidorDTO> </returns>
        public async Task<IEnumerable<DetalleOportunidadCompetidorDTO>> ObtenerDetallePorIdOportunidadCompetidorAsync(int idOportunidadCompetidor)
        {
            try
            {
                List<DetalleOportunidadCompetidorDTO> rpta = new List<DetalleOportunidadCompetidorDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdOportunidadCompetidor,
	                    IdCompetidor,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_DetalleOportunidadCompetidor
                    WHERE Estado = 1
                        AND IdOportunidadCompetidor = @idOportunidadCompetidor";
                var resultado = await _dapperRepository.QueryDapperAsync(query, new { idOportunidadCompetidor });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DetalleOportunidadCompetidorDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_DetalleOportunidadCompetidor asociado a una OportunidadCompetidor y un Competidor.
        /// </summary>
        /// <param name="idOportunidadCompetidor">Id de Oportunidad Competidor</param>
        /// <param name="idCompetidor">Id de Competidor</param>
        /// <returns> DetalleOportunidadCompetidorDTO </returns>
        public DetalleOportunidadCompetidorDTO ObtenerDetallePorDatosCompetidor(int idOportunidadCompetidor, int idCompetidor)
        {
            try
            {
                DetalleOportunidadCompetidorDTO rpta = new DetalleOportunidadCompetidorDTO();
                var query = @"
                    SELECT
	                    Id,
	                    IdOportunidadCompetidor,
	                    IdCompetidor,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_DetalleOportunidadCompetidor
                    WHERE Estado = 1
                        AND IdOportunidadCompetidor = @idOportunidadCompetidor
                        AND IdCompetidor = @idCompetidor";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidadCompetidor, idCompetidor });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<DetalleOportunidadCompetidorDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DetalleOportunidadCompetidor ObtenerPorIdOportunidaCompetidorIdCompetidor(int idOportunidadCompetidor, int idCompetidor)
        {
            try
            {
                DetalleOportunidadCompetidor rpta = new DetalleOportunidadCompetidor();
                var query = @"
                            SELECT
	                            Id,
                                IdOportunidadCompetidor,
                                IdCompetidor,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion
                            FROM com.T_DetalleOportunidadCompetidor
                            WHERE Estado = 1
                                AND IdOportunidadCompetidor = @idOportunidadCompetidor
                                AND IdCompetidor = @idCompetidor";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidadCompetidor, idCompetidor });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<DetalleOportunidadCompetidor>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_DetalleOportunidadCompetidor asociado a una OportunidadCompetidor y un Competidor.
        /// </summary>
        /// <param name="idOportunidadCompetidor">Id de Oportunidad Competidor</param>
        /// <param name="idCompetidor">Id de Competidor</param>
        /// <returns> DetalleOportunidadCompetidorDTO </returns>
        public async Task<DetalleOportunidadCompetidor> ObtenerPorIdOportunidaCompetidorIdCompetidorAsync(int idOportunidadCompetidor, int idCompetidor)
        {
            try
            {
                DetalleOportunidadCompetidor rpta = new DetalleOportunidadCompetidor();
                var query = @"
                            SELECT
	                            Id,
                                IdOportunidadCompetidor,
                                IdCompetidor,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion
                            FROM com.T_DetalleOportunidadCompetidor
                            WHERE Estado = 1
                                AND IdOportunidadCompetidor = @idOportunidadCompetidor
                                AND IdCompetidor = @idCompetidor";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { idOportunidadCompetidor, idCompetidor });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<DetalleOportunidadCompetidor>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_DetalleOportunidadCompetidor asociado a una OportunidadCompetidor.
        /// </summary>
        /// <param name="idOportunidadCompetidor">Id de Oportunidad Competidor</param>
        /// <param name="idCompetidor">Id de Competidor</param>
        /// <returns> DetalleOportunidadCompetidorDTO </returns>
        public async Task<List<DetalleOportunidadCompetidor>> ObtenerPorIdOportunidaCompetidorIdsCompetidorAsync(int idOportunidadCompetidor, List<int> idCompetidor)
        {
            try
            {
                List<DetalleOportunidadCompetidor> rpta = new List<DetalleOportunidadCompetidor>();
                var query = @"
                            SELECT
	                            Id,
                                IdOportunidadCompetidor,
                                IdCompetidor,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion
                            FROM com.T_DetalleOportunidadCompetidor
                            WHERE Estado = 1
                                AND IdOportunidadCompetidor = @idOportunidadCompetidor
                                AND IdCompetidor IN @idCompetidor";
                var resultado = await _dapperRepository.QueryDapperAsync(query, new { idOportunidadCompetidor, idCompetidor });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DetalleOportunidadCompetidor>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
