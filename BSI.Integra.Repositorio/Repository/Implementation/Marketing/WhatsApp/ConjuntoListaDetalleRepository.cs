using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConjuntoListaDetalleRepository
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ConjuntoListaDetalle
    /// </summary>
    public class ConjuntoListaDetalleRepository : GenericRepository<TConjuntoListaDetalle>, IConjuntoListaDetalleRepository
    {
        private Mapper _mapper;

        public ConjuntoListaDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConjuntoListaDetalle, ConjuntoListaDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TConjuntoListaDetalle MapeoEntidad(ConjuntoListaDetalleDTO entidad)
        {
            try
            {
     
                TConjuntoListaDetalle modelo = _mapper.Map<TConjuntoListaDetalle>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConjuntoListaDetalle Add(ConjuntoListaDetalleDTO entidad)
        {
            try
            {
                var ConjuntoListaDetalle = MapeoEntidad(entidad);
                base.Insert(ConjuntoListaDetalle);
                return ConjuntoListaDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public TConjuntoListaDetalle Update(ConjuntoListaDetalleDTO entidad)
        {
            try
            {
                var ConjuntoListaDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConjuntoListaDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(ConjuntoListaDetalle);
                return ConjuntoListaDetalle;
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


        public IEnumerable<TConjuntoListaDetalle> Add(IEnumerable<ConjuntoListaDetalleDTO> listadoEntidad)
        {
            try
            {
                List<TConjuntoListaDetalle> listado = new List<TConjuntoListaDetalle>();
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

        public IEnumerable<TConjuntoListaDetalle> Update(IEnumerable<ConjuntoListaDetalleDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConjuntoListaDetalle> listado = new List<TConjuntoListaDetalle>();
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



        public List<ConjuntoListaDetalleDTO> Obtener(int idConjuntoLista)
        {
            try
            {
                List<ConjuntoListaDetalleDTO> conjuntoListaDetalle = new List<ConjuntoListaDetalleDTO>();

                var _query = @"
                            SELECT IdConjuntoLista, 
                                    Id, 
                                    Nombre, 
                                    Descripcion, 
                                    Prioridad, 
                                    UsuarioCreacion, 
                                    UsuarioModificacion, 
                                    FechaCreacion, 
                                    FechaModificacion
                            FROM mkt.V_ObtenerConjuntoListaDetalle
                            WHERE EstadoConjuntoLista = 1
                                    AND EstadoConjuntoListaDetalle = 1
                                    AND IdConjuntoLista = @idConjuntoLista;
                            ";
                var query = _dapperRepository.QueryDapper(_query, new { idConjuntoLista });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    conjuntoListaDetalle = JsonConvert.DeserializeObject<List<ConjuntoListaDetalleDTO>>(query);
                }
                return conjuntoListaDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<ConjuntoListaDetalleMailingMasivoDTO> ObtenerListasMailingMasivo(int idConjuntoLista)
        {
            try
            {
                List<ConjuntoListaDetalleMailingMasivoDTO> conjuntoListaDetalleMasivo = new List<ConjuntoListaDetalleMailingMasivoDTO>();
                var _query = @"
                            SELECT ConfiguracionEnvioMailig.Id AS Id, 
                                   ISNULL(ConfiguracionEnvioMailig.Nombre, ConjuntoListaDetalle.Nombre) AS Nombre, 
                                   ConfiguracionEnvioMailig.Descripcion AS Descripcion, 
                                   ConjuntoListaDetalle.Id AS IdConjuntoListaDetalle, 
                                   ConfiguracionEnvioMailig.IdPlantilla AS IdPlantilla
                            FROM mkt.T_ConjuntoLista AS ConjuntoLista
                                 INNER JOIN mkt.T_ConjuntoListaDetalle AS ConjuntoListaDetalle ON ConjuntoLista.Id = ConjuntoListaDetalle.IdConjuntoLista
                                 LEFT JOIN mkt.T_ConfiguracionEnvioMailing AS ConfiguracionEnvioMailig ON ConfiguracionEnvioMailig.IdConjuntoListaDetalle = ConjuntoListaDetalle.Id
                            WHERE ConjuntoLista.Estado = 1
                                  AND ConjuntoListaDetalle.Estado = 1
                                  AND ConjuntoLista.Id = @idConjuntoLista
                                  AND (ConfiguracionEnvioMailig.Activo = 1
                                       OR ConfiguracionEnvioMailig.Activo IS NULL);
                            ";

                var query = _dapperRepository.QueryDapper(_query, new { idConjuntoLista });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    conjuntoListaDetalleMasivo = JsonConvert.DeserializeObject<List<ConjuntoListaDetalleMailingMasivoDTO>>(query);
                }
                return conjuntoListaDetalleMasivo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



    }
}
