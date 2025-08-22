using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PreguntaIntentoDetalleRepository
    /// Autor: Gilmer qm.
    /// Fecha: 21/07/2023
    /// <summary>
    /// Gestión general de T_PreguntaIntentoDetalle
    /// </summary>
    public class PreguntaIntentoDetalleRepository : GenericRepository<TPreguntaIntentoDetalle>, IPreguntaIntentoDetalleRepository
    {
        private Mapper _mapper;

        public PreguntaIntentoDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaIntentoDetalle, PreguntaIntentoDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPreguntaIntentoDetalle MapeoEntidad(PreguntaIntentoDetalle entidad)
        {
            try
            {
                //Mapea la entidad padre
                TPreguntaIntentoDetalle modelo = _mapper.Map<TPreguntaIntentoDetalle>(entidad);

                //if (entidad.PGeneralPreguntaIntentoDetalles != null && entidad.PGeneralPreguntaIntentoDetalles.Count > 0)
                //    modelo.TPgeneralPreguntaIntentoDetalles = _mapper.Map<List<TPgeneralPreguntaIntentoDetalle>>(entidad.PGeneralPreguntaIntentoDetalles);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntaIntentoDetalle Add(PreguntaIntentoDetalle entidad)
        {
            try
            {
                var PreguntaIntentoDetalle = MapeoEntidad(entidad);
                base.Insert(PreguntaIntentoDetalle);
                return PreguntaIntentoDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntaIntentoDetalle Update(PreguntaIntentoDetalle entidad)
        {
            try
            {
                var PreguntaIntentoDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PreguntaIntentoDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(PreguntaIntentoDetalle);
                return PreguntaIntentoDetalle;
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


        public IEnumerable<TPreguntaIntentoDetalle> Add(IEnumerable<PreguntaIntentoDetalle> listadoEntidad)
        {
            try
            {
                List<TPreguntaIntentoDetalle> listado = new List<TPreguntaIntentoDetalle>();
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

        public IEnumerable<TPreguntaIntentoDetalle> Update(IEnumerable<PreguntaIntentoDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPreguntaIntentoDetalle> listado = new List<TPreguntaIntentoDetalle>();
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
        /// Autor: Gilmer qm.
        /// Fecha: 21/07/2023
        /// Versión: 1
        /// <summary>
        /// Obtiene información de los intentos para el reporte de preguntas interactivas para su exportación en excel
        /// </summary>
        /// <returns>List<ListadoPreguntaIntentoDetalleDTO></returns>
        public async Task<IEnumerable<PreguntaIntentoDetalleOrdenDTO>> ObtenerListadoPreguntaIntentoDetallado()
        {
            try
            {
                var query = "SELECT IdPreguntaIntento, ROW_NUMBER() OVER(PARTITION BY IdPreguntaIntento ORDER BY IdPreguntaIntento ASC) AS Orden, PorcentajeCalificacion FROM gp.T_PreguntaIntentoDetalle WHERE Estado=1";
                var res = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]") && res != "null" && res != null)
                    return JsonConvert.DeserializeObject<IEnumerable<PreguntaIntentoDetalleOrdenDTO>>(res);
                return new List<PreguntaIntentoDetalleOrdenDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer qm.
        /// Fecha: 21/07/2023
        /// <summary>
        /// Obtiene los intentos registrados por pregunta
        /// </summary>
        /// <param name="idPreguntaIntento">Id del intento de pregunta(PK de la tabla gp.T_PreguntaIntento)</param>
        /// <returns>Lista de objeto de tipo PreguntaIntentoDetalleDTO</returns>
        public IEnumerable<PreguntaIntentoDetalle> ObtenerPorIdPreguntaIntento(int idPreguntaIntento)
        {
            try
            {
                var query = @"SELECT Id,
                                       IdPreguntaIntento,
                                       PorcentajeCalificacion,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion
                                FROM gp.T_PreguntaIntentoDetalle
                                WHERE Estado = 1
                                      AND IdPreguntaIntento = @IdPreguntaIntento;";
                var res = _dapperRepository.QueryDapper(query, new { IdPreguntaIntento = idPreguntaIntento });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]") && res != "null" && res != null)
                    return JsonConvert.DeserializeObject<IEnumerable<PreguntaIntentoDetalle>>(res);
                return new List<PreguntaIntentoDetalle>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer qm.
        /// Fecha: 21/07/2023
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <param name="id"> (PK) Primary Key </param>
        /// <returns> PreguntaIntentoDetalle </returns>
        public PreguntaIntentoDetalle ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                                       IdPreguntaIntento,
                                       PorcentajeCalificacion,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion
                                FROM gp.T_PreguntaIntentoDetalle
                                WHERE Estado = 1
                                      AND Id = @Id;";
                var res = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(res) && res != "null" && res != null)
                    return JsonConvert.DeserializeObject<PreguntaIntentoDetalle>(res);
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
