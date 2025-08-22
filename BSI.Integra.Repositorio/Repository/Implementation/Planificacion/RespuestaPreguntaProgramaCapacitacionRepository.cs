using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: RespuestaPreguntaProgramaCapacitacionRepository
    /// Autor : Gilmer Qm.
    /// Fecha: 21/07/2023
    /// <summary>
    /// Gestión general de T_RespuestaPreguntaProgramaCapacitacion
    /// </summary>
    public class RespuestaPreguntaProgramaCapacitacionRepository : GenericRepository<TRespuestaPreguntaProgramaCapacitacion>, IRespuestaPreguntaProgramaCapacitacionRepository
    {
        private Mapper _mapper;
        public RespuestaPreguntaProgramaCapacitacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRespuestaPreguntaProgramaCapacitacion, RespuestaPreguntaProgramaCapacitacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TRespuestaPreguntaProgramaCapacitacion MapeoEntidad(RespuestaPreguntaProgramaCapacitacion entidad)
        {
            try
            {
                //crea la entidad padre
                TRespuestaPreguntaProgramaCapacitacion modelo = _mapper.Map<TRespuestaPreguntaProgramaCapacitacion>(entidad);

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

        public TRespuestaPreguntaProgramaCapacitacion Add(RespuestaPreguntaProgramaCapacitacion entidad)
        {
            try
            {
                var RespuestaPreguntaProgramaCapacitacion = MapeoEntidad(entidad);
                base.Insert(RespuestaPreguntaProgramaCapacitacion);
                return RespuestaPreguntaProgramaCapacitacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRespuestaPreguntaProgramaCapacitacion Update(RespuestaPreguntaProgramaCapacitacion entidad)
        {
            try
            {
                var RespuestaPreguntaProgramaCapacitacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RespuestaPreguntaProgramaCapacitacion.RowVersion = entidadExistente.RowVersion;

                base.Update(RespuestaPreguntaProgramaCapacitacion);
                return RespuestaPreguntaProgramaCapacitacion;
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


        public IEnumerable<TRespuestaPreguntaProgramaCapacitacion> Add(IEnumerable<RespuestaPreguntaProgramaCapacitacion> listadoEntidad)
        {
            try
            {
                List<TRespuestaPreguntaProgramaCapacitacion> listado = new List<TRespuestaPreguntaProgramaCapacitacion>();
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

        public IEnumerable<TRespuestaPreguntaProgramaCapacitacion> Update(IEnumerable<RespuestaPreguntaProgramaCapacitacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRespuestaPreguntaProgramaCapacitacion> listado = new List<TRespuestaPreguntaProgramaCapacitacion>();
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
        /// Autor : Gilmer Qm.
        /// Fecha: 21/07/2023
        /// <summary>
        /// Obtiene respuestas asociadas a una pregunta
        /// </summary>
        /// <param name="idPreguntaProgramaCapacitacion">Id de la pregunta del programa de capacitacion (PK de la tabla ope.T_PreguntaProgramaCapacitacion)</param>
        /// <returns>Lista de objetos de tipo RespuestaPreguntaProgramaCapacitacion</returns>
        public IEnumerable<RespuestaPreguntaProgramaCapacitacion> ObtenerPorIdPreguntaProgramaCapacitacion(int idPreguntaProgramaCapacitacion)
        {
            try
            {
                string query = @"SELECT Id,
                                           IdPreguntaProgramaCapacitacion,
                                           RespuestaCorrecta,
                                           NroOrden,
                                           EnunciadoRespuesta,
                                           Estado,
                                           UsuarioCreacion,
                                           UsuarioModificacion,
                                           FechaCreacion,
                                           FechaModificacion,
                                           RowVersion,
                                           IdMigracion,
                                           NroOrdenRespuesta,
                                           Puntaje,
                                           FeedbackPositivo,
                                           FeedbackNegativo,
                                           MostrarFeedBack,
                                           PuntajeTipoRespuesta
                                    FROM ope.T_RespuestaPreguntaProgramaCapacitacion
                                    WHERE Estado = 1
                                          AND IdPreguntaProgramaCapacitacion = @IdPreguntaProgramaCapacitacion;";
                var res = _dapperRepository.QueryDapper(query, new { IdPreguntaProgramaCapacitacion = idPreguntaProgramaCapacitacion });
                if (!string.IsNullOrEmpty(res) && !res.Equals("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<RespuestaPreguntaProgramaCapacitacion>>(res);
                return new List<RespuestaPreguntaProgramaCapacitacion>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor : Gilmer Qm.
        /// Fecha: 21/07/2023
        /// <summary>
        /// Obtiene el registro por el PK
        /// </summary>
        /// <param name="id"> Primary Key </param>
        /// <returns> Entidad: RespuestaPreguntaProgramaCapacitacion </returns>
        public RespuestaPreguntaProgramaCapacitacion ObtenerPorId(int id)
        {
            try
            {
                string query = @"SELECT Id,
                                           IdPreguntaProgramaCapacitacion,
                                           RespuestaCorrecta,
                                           NroOrden,
                                           EnunciadoRespuesta,
                                           Estado,
                                           UsuarioCreacion,
                                           UsuarioModificacion,
                                           FechaCreacion,
                                           FechaModificacion,
                                           RowVersion,
                                           IdMigracion,
                                           NroOrdenRespuesta,
                                           Puntaje,
                                           FeedbackPositivo,
                                           FeedbackNegativo,
                                           MostrarFeedBack,
                                           PuntajeTipoRespuesta
                                    FROM ope.T_RespuestaPreguntaProgramaCapacitacion
                                    WHERE Estado = 1
                                          AND Id = @Id;";
                var res = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(res) && !res.Equals("null"))
                    return JsonConvert.DeserializeObject<RespuestaPreguntaProgramaCapacitacion>(res);
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
