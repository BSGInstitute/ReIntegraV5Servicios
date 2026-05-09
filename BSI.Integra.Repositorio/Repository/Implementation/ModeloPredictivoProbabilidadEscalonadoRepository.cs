using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ModeloPredictivoProbabilidadEscalonadoRepository
    /// Autor: Jose Vega
    /// Fecha: 22/04/2026
    /// Version: 1.0
    /// <summary>
    /// Gestión de mkt.T_ModeloPredictivoProbabilidadEscalonado.
    /// </summary>
    public class ModeloPredictivoProbabilidadEscalonadoRepository : GenericRepository<TModeloPredictivoProbabilidadEscalonado>, IModeloPredictivoProbabilidadEscalonadoRepository
    {
        private Mapper _mapper;

        public ModeloPredictivoProbabilidadEscalonadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModeloPredictivoProbabilidadEscalonado, ModeloPredictivoProbabilidadEscalonado>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TModeloPredictivoProbabilidadEscalonado MapeoEntidad(ModeloPredictivoProbabilidadEscalonado entidad)
        {
            try
            {
                TModeloPredictivoProbabilidadEscalonado modelo = _mapper.Map<TModeloPredictivoProbabilidadEscalonado>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModeloPredictivoProbabilidadEscalonado Add(ModeloPredictivoProbabilidadEscalonado entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                base.Insert(modelo);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModeloPredictivoProbabilidadEscalonado Update(ModeloPredictivoProbabilidadEscalonado entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                modelo.RowVersion = entidadExistente.RowVersion;

                base.Update(modelo);
                return modelo;
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

        public IEnumerable<TModeloPredictivoProbabilidadEscalonado> Add(IEnumerable<ModeloPredictivoProbabilidadEscalonado> listadoEntidad)
        {
            try
            {
                List<TModeloPredictivoProbabilidadEscalonado> listado = new List<TModeloPredictivoProbabilidadEscalonado>();
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

        public IEnumerable<TModeloPredictivoProbabilidadEscalonado> Update(IEnumerable<ModeloPredictivoProbabilidadEscalonado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TModeloPredictivoProbabilidadEscalonado> listado = new List<TModeloPredictivoProbabilidadEscalonado>();
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

        public ScorePrimeraOportunidadDTO ObtenerP0PorIdOportunidad(int idOportunidad)
        {
            try
            {
                var resultado = new ScorePrimeraOportunidadDTO
                {
                    IdOportunidad = idOportunidad,
                    P0 = 0.0m
                };

                var query = @"
                    SELECT TOP 1 e.ProbabilidadPerfil AS P0
                    FROM mkt.T_ModeloPredictivoProbabilidad p WITH (NOLOCK)
                    INNER JOIN mkt.T_ModeloPredictivoProbabilidadEscalonado e WITH (NOLOCK)
                        ON e.IdModeloPredictivoProbabilidad = p.Id
                    WHERE p.IdOportunidad = @idOportunidad
                      AND p.IdModeloPredictivoTipo = 10
                      AND p.Estado = 1
                      AND e.Estado = 1
                    ORDER BY p.FechaCreacion DESC";

                var respuesta = _dapperRepository.FirstOrDefault(query, new { idOportunidad });

                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null" && !respuesta.Contains("[]"))
                {
                    var fila = JsonConvert.DeserializeObject<ScorePrimeraOportunidadDTO>(respuesta);
                    if (fila != null)
                    {
                        resultado.P0 = fila.P0;
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
