using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
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
    /// Repositorio: PreguntumRepository
    /// Autor : Gilmer Qm.
    /// Fecha: 03/08/2023
    /// <summary>
    /// Gestión general de T_Preguntum
    /// </summary>
    public class PreguntumRepository : GenericRepository<TPreguntum>, IPreguntumRepository
    {
        private Mapper _mapper;
        public PreguntumRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntum, Preguntum>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPreguntum MapeoEntidad(Preguntum entidad)
        {
            try
            {
                //crea la entidad padre
                TPreguntum modelo = _mapper.Map<TPreguntum>(entidad);

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

        public TPreguntum Add(Preguntum entidad)
        {
            try
            {
                var Preguntum = MapeoEntidad(entidad);
                base.Insert(Preguntum);
                return Preguntum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntum Update(Preguntum entidad)
        {
            try
            {
                var Preguntum = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Preguntum.RowVersion = entidadExistente.RowVersion;

                base.Update(Preguntum);
                return Preguntum;
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


        public IEnumerable<TPreguntum> Add(IEnumerable<Preguntum> listadoEntidad)
        {
            try
            {
                List<TPreguntum> listado = new List<TPreguntum>();
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

        public IEnumerable<TPreguntum> Update(IEnumerable<Preguntum> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPreguntum> listado = new List<TPreguntum>();
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
        /// Fecha: 03/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por el PK
        /// </summary>
        /// <returns> Preguntum </returns>
        public Preguntum ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                                       IdTipoRespuesta,
                                       IdPreguntaEscalaValor,
                                       EnunciadoPregunta,
                                       ConparacionValor,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion,
                                       RequiereTiempo,
                                       MinutosPorPregunta,
                                       RespuestaAleatoria,
                                       ActivarFeedBackRespuestaCorrecta,
                                       ActivarFeedBackRespuestaIncorrecta,
                                       MostrarFeedbackInmediato,
                                       MostrarFeedbackPorPregunta,
                                       IdPreguntaIntento,
                                       IdPreguntaTipo,
                                       IdTipoRespuestaCalificacion,
                                       FactorRespuesta,
                                       IdPreguntaCategoria
                                FROM gp.T_Pregunta
                                WHERE Estado = 1
                                 AND Id = @Id;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<Preguntum>(resultado); 
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 14/05/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de V_PreguntaEncuestaAsincronica
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> BancoPreguntumDTO </returns>
        public List<BancoPreguntumDTO> ObtenerPreguntaEncuestaAsincronica()
        {
            try
            {
                var rpta = new List<BancoPreguntumDTO>();
                var query = @"SELECT IdPregunta
		                        ,IdTipoRespuesta
		                        ,IdPreguntaTipo
		                        ,EnunciadoPregunta
		                        ,NombreTipoRespuesta
		                        ,NombrePreguntaTipo
		                        ,ActivarDescripcion
                                ,Descripcion
		                        ,PreguntaObligatoria
		                        ,PreguntaActiva
                            FROM [gp].[V_PreguntaEncuestaAsincronica]";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<BancoPreguntumDTO>>(resultado);
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
