using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: MontoPagoCronogramaDetalleRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/07/2022
    /// <summary>
    /// Gestión general de T_MontoPagoCronogramaDetalle
    /// </summary>
    public class MontoPagoCronogramaDetalleRepository : GenericRepository<TMontoPagoCronogramaDetalle>, IMontoPagoCronogramaDetalleRepository
    {
        private Mapper _mapper;

        public MontoPagoCronogramaDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMontoPagoCronogramaDetalle, MontoPagoCronogramaDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMontoPagoCronogramaDetalle MapeoEntidad(MontoPagoCronogramaDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TMontoPagoCronogramaDetalle modelo = _mapper.Map<TMontoPagoCronogramaDetalle>(entidad);

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

        public TMontoPagoCronogramaDetalle Add(MontoPagoCronogramaDetalle entidad)
        {
            try
            {
                var MontoPagoCronogramaDetalle = MapeoEntidad(entidad);
                base.Insert(MontoPagoCronogramaDetalle);
                return MontoPagoCronogramaDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMontoPagoCronogramaDetalle Update(MontoPagoCronogramaDetalle entidad)
        {
            try
            {
                var MontoPagoCronogramaDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MontoPagoCronogramaDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(MontoPagoCronogramaDetalle);
                return MontoPagoCronogramaDetalle;
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


        public IEnumerable<TMontoPagoCronogramaDetalle> Add(IEnumerable<MontoPagoCronogramaDetalle> listadoEntidad)
        {
            try
            {
                List<TMontoPagoCronogramaDetalle> listado = new List<TMontoPagoCronogramaDetalle>();
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

        public IEnumerable<TMontoPagoCronogramaDetalle> Update(IEnumerable<MontoPagoCronogramaDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMontoPagoCronogramaDetalle> listado = new List<TMontoPagoCronogramaDetalle>();
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
        /// Fecha: 09/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MontoPagoCronogramaDetalle.
        /// </summary>
        /// <returns> List<MontoPagoCronogramaDetalleDTO> </returns>
        public IEnumerable<MontoPagoCronogramaDetalleDTO> ObtenerMontoPagoCronogramaDetalle()
        {
            try
            {
                List<MontoPagoCronogramaDetalleDTO> rpta = new List<MontoPagoCronogramaDetalleDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    NumeroCuota,
	                    MontoCuota,
	                    FechaPago,
	                    CuotaDescripcion,
	                    MontoCuotaDescuento,
	                    Pagado,
	                    IdMontoPagoCronograma,
	                    Matricula,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_MontoPagoCronogramaDetalle
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MontoPagoCronogramaDetalleDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MontoPagoCronogramaDetalle para mostrarse en combo.
        /// </summary>
        /// <returns> List<MontoPagoCronogramaDetalleComboDTO> </returns>
        public IEnumerable<MontoPagoCronogramaDetalleComboDTO> ObtenerCombo()
        {
            try
            {
                List<MontoPagoCronogramaDetalleComboDTO> rpta = new List<MontoPagoCronogramaDetalleComboDTO>();
                var query = @"SELECT Id,IdMontoPagoCronograma FROM com.T_MontoPagoCronogramaDetalle WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MontoPagoCronogramaDetalleComboDTO>>(resultado);
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
        /// Obtiene registros de T_MontoPagoCronogramaDetalle relacionados a un Cronograma.
        /// </summary>
        /// <param name="idCronograma">Id de MontoPagoCronograma</param>
        /// <returns> List<MontoPagoCronogramaDetalleDTO> </returns>
        public IEnumerable<MontoPagoCronogramaDetalleDTO> ObtenerMontoPagoCronogramaDetallePorIdCronograma(int idCronograma)
        {
            try
            {
                List<MontoPagoCronogramaDetalleDTO> detalle = new List<MontoPagoCronogramaDetalleDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    NumeroCuota,
	                    MontoCuota,
	                    FechaPago,
	                    CuotaDescripcion,
	                    MontoCuotaDescuento,
	                    Pagado,
	                    IdMontoPagoCronograma,
	                    Matricula,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_MontoPagoCronogramaDetalle
                    WHERE Estado = 1 AND IdMontoPagoCronograma = @idCronograma";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idCronograma });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    detalle = JsonConvert.DeserializeObject<List<MontoPagoCronogramaDetalleDTO>>(resultadoQuery);
                }
                return detalle;
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
        /// Obtiene registros de T_MontoPagoCronogramaDetalle relacionados a un Cronograma.
        /// </summary>
        /// <param name="idCronograma">Id de MontoPagoCronograma</param>
        /// <returns> List<MontoPagoCronogramaDetalleDTO> </returns>
        public async Task<IEnumerable<MontoPagoCronogramaDetalleDTO>> ObtenerMontoPagoCronogramaDetallePorIdCronogramaAsync(int idCronograma)
        {
            try
            {
                List<MontoPagoCronogramaDetalleDTO> detalle = new List<MontoPagoCronogramaDetalleDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    NumeroCuota,
	                    MontoCuota,
	                    FechaPago,
	                    CuotaDescripcion,
	                    MontoCuotaDescuento,
	                    Pagado,
	                    IdMontoPagoCronograma,
	                    Matricula,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_MontoPagoCronogramaDetalle
                    WHERE Estado = 1 AND IdMontoPagoCronograma = @idCronograma";
                var resultadoQuery = await _dapperRepository.QueryDapperAsync(query, new { idCronograma });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    detalle = JsonConvert.DeserializeObject<List<MontoPagoCronogramaDetalleDTO>>(resultadoQuery);
                }
                return detalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 28/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_MontoPagoCronogramaDetalle asociado al IdMontoPagoCronograma.
        /// </summary>
        /// <param name="idCronograma">Id de MontoPagoCronograma</param>
        /// <returns> List<MontoPagoCronogramaDetalleDTO>  </returns>
        public IEnumerable<MontoPagoCronogramaDetalleDTO> ObtenerPorIdCronograma(int idCronograma)
        {
            try
            {
                List<MontoPagoCronogramaDetalleDTO> respuesta = new List<MontoPagoCronogramaDetalleDTO>();
                var query = @" SELECT Id,
                                         NumeroCuota,
                                         MontoCuota,
                                         FechaPago,
                                         CuotaDescripcion,
                                         MontoCuotaDescuento,
                                         Pagado,
                                         IdMontoPagoCronograma,
                                         Matricula,
                                         Estado,
                                         UsuarioCreacion,
                                         UsuarioModificacion,
                                         FechaCreacion,
                                         FechaModificacion,
                                         RowVersion,
                                         IdMigracion
                                FROM com.T_MontoPagoCronogramaDetalle
                                WHERE Estado = 1 AND IdMontoPagoCronograma = @IdCronograma";
                var resultado = _dapperRepository.QueryDapper(query, new { IdCronograma = idCronograma });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<MontoPagoCronogramaDetalleDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
