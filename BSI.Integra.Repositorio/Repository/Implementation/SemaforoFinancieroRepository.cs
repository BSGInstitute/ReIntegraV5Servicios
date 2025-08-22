using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SemaforoFinancieroRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_SemaforoFinanciero
    /// </summary>
    public class SemaforoFinancieroRepository : GenericRepository<TSemaforoFinanciero>, ISemaforoFinancieroRepository
    {
        private Mapper _mapper;

        public SemaforoFinancieroRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSemaforoFinanciero, SemaforoFinanciero>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSemaforoFinanciero MapeoEntidad(SemaforoFinanciero entidad)
        {
            try
            {
                //crea la entidad padre
                TSemaforoFinanciero modelo = _mapper.Map<TSemaforoFinanciero>(entidad);

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

        public TSemaforoFinanciero Add(SemaforoFinanciero entidad)
        {
            try
            {
                var SemaforoFinanciero = MapeoEntidad(entidad);
                base.Insert(SemaforoFinanciero);
                return SemaforoFinanciero;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSemaforoFinanciero Update(SemaforoFinanciero entidad)
        {
            try
            {
                var SemaforoFinanciero = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SemaforoFinanciero.RowVersion = entidadExistente.RowVersion;

                base.Update(SemaforoFinanciero);
                return SemaforoFinanciero;
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


        public IEnumerable<TSemaforoFinanciero> Add(IEnumerable<SemaforoFinanciero> listadoEntidad)
        {
            try
            {
                List<TSemaforoFinanciero> listado = new List<TSemaforoFinanciero>();
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

        public IEnumerable<TSemaforoFinanciero> Update(IEnumerable<SemaforoFinanciero> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSemaforoFinanciero> listado = new List<TSemaforoFinanciero>();
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
        /// Obtiene todos los registros de T_SemaforoFinanciero.
        /// </summary>
        /// <returns> List<SemaforoFinancieroDTO> </returns>
        public IEnumerable<SemaforoFinancieroDTO> ObtenerSemaforoFinanciero()
        {
            try
            {
                List<SemaforoFinancieroDTO> rpta = new List<SemaforoFinancieroDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPais,
	                    Activo,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion
                    FROM com.T_SemaforoFinanciero
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SemaforoFinancieroDTO>>(resultado);
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
        /// Obtiene registros de T_SemaforoFinanciero para mostrarse en combo.
        /// </summary>
        /// <returns> List<SemaforoFinancieroComboDTO> </returns>
        public IEnumerable<SemaforoFinancieroComboDTO> ObtenerCombo()
        {
            try
            {
                List<SemaforoFinancieroComboDTO> rpta = new List<SemaforoFinancieroComboDTO>();
                var query = @"
                    SELECT
	                    SF.Id,
	                    P.NombrePais,SF.IdPais,SF.Activo,SF.Estado,SF.UsuarioCreacion,SF.UsuarioModificacion,SF.FechaCreacion,
                        SF.FechaModificacion,SF.RowVersion,SF.IdMigracion 
                    FROM com.T_SemaforoFinanciero AS SF
                    INNER JOIN conf.T_Pais AS P
	                    ON SF.IdPais = P.Id
                    WHERE
	                    SF.Estado = 1
	                    AND P.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SemaforoFinancieroComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/09/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta nuevo registro a la tabla T_SemaforoFinanciero
        /// </summary>
        public bool InsertarNuevoSemaforo(SemaforoFinanciero objeto)
        {
            try
            {
                //mapeo de la entidad
                TSemaforoFinanciero entidad = MapeoEntidad(objeto);

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
        private void AsignacionId(TSemaforoFinanciero entidad, SemaforoFinanciero objetoBO)
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
        /// Obtiene el registro de la tabla T_SemaforoFinancieropor el id
        /// </summary>
        ///<param name="id">id del T_SemaforoFinancieroDetalle/param>
        /// <returns> SemaforoFinanciero </returns>
        public SemaforoFinanciero ObtenerSemaforoPorId(int id)
        {
            try
            {
                SemaforoFinanciero rpta = new SemaforoFinanciero();
                var query = @"SELECT Id,IdPais,Estado, UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion 
                            FROM com.T_SemaforoFinanciero 
                            WHERE Id = " + id + " AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<SemaforoFinanciero>(resultado);

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
