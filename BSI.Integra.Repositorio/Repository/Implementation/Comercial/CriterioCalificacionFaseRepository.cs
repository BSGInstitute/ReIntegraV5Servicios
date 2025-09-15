using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CriterioCalificacionFaseRepository
    /// Autor: José Vega
    /// Fecha: 20/09/2023
    /// <summary>
    /// Gestión general de T_CriterioCalificacionFaseOportunidad
    /// </summary>
    public class CriterioCalificacionFaseRepository : GenericRepository<TCriterioCalificacionFaseOportunidad>, ICriterioCalificacionFaseRepository
    {
        private Mapper _mapper;
        public CriterioCalificacionFaseRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioCalificacionFaseOportunidad, CriterioCalificacionFase>(MemberList.None).ReverseMap();
                cfg.CreateMap<TLineamientoCalificacionFase, LineamientoCalificacionFase>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCriterioCalificacionFaseOportunidad MapeoEntidad(CriterioCalificacionFase entidad)
        {
            try
            {
                TCriterioCalificacionFaseOportunidad criterioCalificacionFaseOportunidad = _mapper.Map<TCriterioCalificacionFaseOportunidad>(entidad);
                return criterioCalificacionFaseOportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCriterioCalificacionFaseOportunidad Add(CriterioCalificacionFase entidad)
        {
            try
            {
                var criterioCalificacionFase = MapeoEntidad(entidad);
                base.Insert(criterioCalificacionFase);
                return criterioCalificacionFase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCriterioCalificacionFaseOportunidad Update(CriterioCalificacionFase entidad)
        {
            try
            {
                var criterioCalificacionFase = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                criterioCalificacionFase.RowVersion = entidadExistente.RowVersion;

                base.Update(criterioCalificacionFase);
                return criterioCalificacionFase;
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

        public IEnumerable<TCriterioCalificacionFaseOportunidad> Add(IEnumerable<CriterioCalificacionFase> listadoEntidad)
        {
            try
            {
                List<TCriterioCalificacionFaseOportunidad> listado = new List<TCriterioCalificacionFaseOportunidad>();
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

        public IEnumerable<TCriterioCalificacionFaseOportunidad> Update(IEnumerable<CriterioCalificacionFase> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCriterioCalificacionFaseOportunidad> listado = new List<TCriterioCalificacionFaseOportunidad>();
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

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina de manera lógica los registros de tablas detalle de T_CriterioCalificacionFaseOportunidad
        /// </summary>
        /// <param name="id"> PK de T_CriterioCalificacionFaseOportunidad </param> 
        /// <returns> bool </returns>
        public bool EliminarDetalles(int id)
        {
            try
            {
                string spReporte = "[com].[SP_EliminarCriterioCalificacionFaseDetalles]";
                string resultadoReporte = _dapperRepository.QuerySPDapper(spReporte, new { IdCriterioCalificacionFase = id });
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros con los detalles de T_CriterioCalificacionFaseOportunidad
        /// </summary>
        /// <returns> List<CriterioCalificacionFaseDTO> </returns>
        public List<CriterioCalificacionFaseDTO> ObtenerCriteriosCalificacionFase()
        {
            try
            {
                List<CriterioCalificacionFaseDTO> criteriosFiltro = new();
                var query = @"SELECT Id,
                        IdTransicionCalificacionFase,
                        Orden,
                        Nombre,
                        Descripcion
                    FROM com.T_CriterioCalificacionFaseOportunidad
                    WHERE Estado = 1 
                    ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    criteriosFiltro = JsonConvert.DeserializeObject<List<CriterioCalificacionFaseDTO>>(resultado)!;
                }
                return criteriosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los criterios asociados a una transición específica
        /// </summary>
        /// <param name="idTransicionCalificacionFase">ID de la transición</param>
        /// <returns> List<CriterioCalificacionFaseDTO> </returns>
        public List<CriterioCalificacionFaseDTO> ObtenerCriteriosPorTransicion(int idTransicionCalificacionFase)
        {
            try
            {
                List<CriterioCalificacionFaseDTO> criteriosFiltro = new();
                var query = @"SELECT Id,
                        IdTransicionCalificacionFase,
                        Orden,
                        Nombre,
                        Descripcion
                    FROM com.T_CriterioCalificacionFaseOportunidad
                    WHERE Estado = 1 AND IdTransicionCalificacionFase = @idTransicionCalificacionFase
                    ORDER BY Orden";
                var resultado = _dapperRepository.QueryDapper(query, new { idTransicionCalificacionFase });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    criteriosFiltro = JsonConvert.DeserializeObject<List<CriterioCalificacionFaseDTO>>(resultado)!;
                }
                return criteriosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros con los detalles de T_CriterioCalificacionFaseOportunidad
        /// </summary>
        /// <returns> CriterioCalificacionFaseDTO </returns>
        public CriterioCalificacionFase ObtenerCriterioCalificacionFasePorId(int idCriterioCalificacionFase)
        {
            try
            {
                var rpta = new CriterioCalificacionFase();
                var query = @"SELECT Id,
                           Orden,
                           Nombre,
                           Descripcion,
                           UsuarioCreacion,
                           FechaCreacion,
                           IdMigracion
                    FROM com.T_CriterioCalificacionFaseOportunidad
                    WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = idCriterioCalificacionFase });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                  rpta = JsonConvert.DeserializeObject<CriterioCalificacionFase>(resultado)!;
                  return rpta;
                }
                return null;


            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de un registro por el PK
        /// </summary>
        /// <param name="id"> PK de T_CriterioCalificacionFaseOportunidad </param> 
        /// <returns> CriterioCalificacionFase </returns>
        public CriterioCalificacionFase ObtenerPorId(int id)
        {
            try
            {
                string query = @"SELECT Id,
                                       Orden,
                                       Nombre,
                                       Descripcion,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion
                                FROM com.T_CriterioCalificacionFaseOportunidad
                                WHERE Estado = 1
                                      AND Id =  @Id;";
                string resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<CriterioCalificacionFase>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CCFR-OPI-001@Error en ObtenerPorId: {ex.Message}", ex);
            }
        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todo el combo de la tabla T_CriterioCalificacionFaseOportunidad
        /// </summary>
        /// <returns> Lista DTO - List<ComboDTO>() </returns>
        public IEnumerable<ComboDTO> ListaCriterios()
        {
            try
            {
                var query = @"
                    SELECT 
                        Id, Nombre
                    FROM 
                        com.T_CriterioCalificacionFaseOportunidad
                    WHERE 
                        Estado = 1
                    ORDER BY
                        Nombre";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#CCFR-OC-001@Error en ObtenerCombo: {ex.Message}", ex);
            }
        }
    }
}