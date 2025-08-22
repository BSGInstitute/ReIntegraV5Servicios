using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SemaforoFinancieroDetalleRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_SemaforoFinancieroDetalle
    /// </summary>
    public class SemaforoFinancieroDetalleRepository : GenericRepository<TSemaforoFinancieroDetalle>, ISemaforoFinancieroDetalleRepository
    {
        private Mapper _mapper;

        public SemaforoFinancieroDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSemaforoFinancieroDetalle, SemaforoFinancieroDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSemaforoFinancieroDetalle MapeoEntidad(SemaforoFinancieroDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TSemaforoFinancieroDetalle modelo = _mapper.Map<TSemaforoFinancieroDetalle>(entidad);

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

        public TSemaforoFinancieroDetalle Add(SemaforoFinancieroDetalle entidad)
        {
            try
            {
                var SemaforoFinancieroDetalle = MapeoEntidad(entidad);
                base.Insert(SemaforoFinancieroDetalle);
                return SemaforoFinancieroDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSemaforoFinancieroDetalle Update(SemaforoFinancieroDetalle entidad)
        {
            try
            {
                var SemaforoFinancieroDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SemaforoFinancieroDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(SemaforoFinancieroDetalle);
                return SemaforoFinancieroDetalle;
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


        public IEnumerable<TSemaforoFinancieroDetalle> Add(IEnumerable<SemaforoFinancieroDetalle> listadoEntidad)
        {
            try
            {
                List<TSemaforoFinancieroDetalle> listado = new List<TSemaforoFinancieroDetalle>();
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

        public IEnumerable<TSemaforoFinancieroDetalle> Update(IEnumerable<SemaforoFinancieroDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSemaforoFinancieroDetalle> listado = new List<TSemaforoFinancieroDetalle>();
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
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SemaforoFinancieroDetalle.
        /// </summary>
        /// <returns> List<SemaforoFinancieroDetalleDTO> </returns>
        public IEnumerable<SemaforoFinancieroDetalleDTO> ObtenerSemaforoFinancieroDetalle()
        {
            try
            {
                List<SemaforoFinancieroDetalleDTO> rpta = new List<SemaforoFinancieroDetalleDTO>();
                var query = $@"
                    SELECT
	                    Id,
	                    IdSemaforoFinanciero,
	                    Nombre,
	                    Mensaje,
	                    Color,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion
                    FROM com.T_SemaforoFinancieroDetalle
                    WHERE
	                    Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SemaforoFinancieroDetalleDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SemaforoFinancieroDetalle para mostrarse en combo.
        /// </summary>
        /// <returns> List<SemaforoFinancieroDetalleComboDTO> </returns>
        public IEnumerable<SemaforoFinancieroDetalleComboDTO> ObtenerCombo()
        {
            try
            {
                List<SemaforoFinancieroDetalleComboDTO> rpta = new List<SemaforoFinancieroDetalleComboDTO>();
                var query = $@"
                    SELECT
	                    Id,
	                    Nombre
                    FROM com.T_SemaforoFinancieroDetalle
                    WHERE
	                    Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SemaforoFinancieroDetalleComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_SemaforoFinancieroDetalle asociados a un SemaforoFinanciero.
        /// </summary>
        /// <param name="idSemaforoFinanciero">Id del semaforo financiero</param>
        /// <returns> List<SemaforoFinancieroDetalleDTO> </returns>
        public IEnumerable<SemaforoFinancieroDetalleDTO> ObtenerSemaforoFinancieroDetallePorIdSemaforoFinanciero(int idSemaforoFinanciero)
        {
            try
            {
                List<SemaforoFinancieroDetalleDTO> rpta = new List<SemaforoFinancieroDetalleDTO>();
                var query = $@"
                    SELECT
	                    Id,
	                    IdSemaforoFinanciero,
	                    Nombre,
	                    Mensaje,
	                    Color,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        Estado,
						RowVersion
                    FROM com.T_SemaforoFinancieroDetalle
                    WHERE
	                    Estado = 1
	                    AND IdSemaforoFinanciero = @idSemaforoFinanciero";
                var resultado = _dapperRepository.QueryDapper(query, new { idSemaforoFinanciero });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SemaforoFinancieroDetalleDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 02/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las variables segun el IdSemaforoFinancieroDetalle
        /// </summary>
        ///<param name="IdSemaforoFinancieroDetalle">Id del semaforo financiero detalle</param>
        /// <returns> SemaforoFinancieroDetalleVariableInformacionDetalladaDTO </returns>
        public List<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO> ObtenerVariables(int IdSemaforoFinancieroDetalle)
        {
            try
            {
                List<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO> respuesta = new List<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO>();
                var query = "com.SP_ObtenerSemaforoFinancieroDetalleVariable";
                var respuestaBD = _dapperRepository.QuerySPDapper(query, new { semaforoDetalle = IdSemaforoFinancieroDetalle });

                if (!string.IsNullOrEmpty(respuestaBD) && !respuestaBD.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO>>(respuestaBD);
                }
                return respuesta;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/09/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta nuevo registro a la tabla T_SemaforoFinancieroDetalle.
        /// </summary>
        ///<param name="objeto">Objeto a insertarse</param>
        public bool InsertarNuevoSemaforoDetalle(SemaforoFinancieroDetalle objeto)
        {
            try
            {
                //mapeo de la entidad
                TSemaforoFinancieroDetalle entidad = MapeoEntidad(objeto);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objeto);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 02/09/2022
        /// Version: 1.0
        /// <summary>
        /// Asigna un nuevo Id al objeto
        /// </summary>
        ///<param name="entidad">Tabla Entidad</param>
        ///<param name="objetoBO">Entidad/param>
        /// <returns>  </returns>
        private void AsignacionId(TSemaforoFinancieroDetalle entidad, SemaforoFinancieroDetalle objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 02/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de la tabla T_SemaforoFinancieroDetalle por el id
        /// </summary>
        ///<param name="id">id del T_SemaforoFinancieroDetalle/param>
        /// <returns> SemaforoFinancieroDetalle </returns>
        public SemaforoFinancieroDetalle ObtenerSemaforoFinancieroDetallePorId(int id)
        {
            try
            {
                SemaforoFinancieroDetalle rpta = new SemaforoFinancieroDetalle();
                var query = @"SELECT 
                                Id,
                                IdSemaforoFinanciero,
                                Nombre,
                                Mensaje,
                                Color,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                IdMigracion
                            FROM com.T_SemaforoFinancieroDetalle
                            WHERE Id = " + id + " AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<SemaforoFinancieroDetalle>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
