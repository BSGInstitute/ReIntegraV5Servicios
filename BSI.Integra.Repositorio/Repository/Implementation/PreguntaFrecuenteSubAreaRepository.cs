using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PreguntaFrecuenteSubAreaRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PreguntaFrecuenteSubArea
    /// </summary>
    public class PreguntaFrecuenteSubAreaRepository : GenericRepository<TPreguntaFrecuenteSubArea>, IPreguntaFrecuenteSubAreaRepository
    {
        private Mapper _mapper;

        public PreguntaFrecuenteSubAreaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaFrecuenteSubArea, PreguntaFrecuenteSubArea>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPreguntaFrecuenteSubArea MapeoEntidad(PreguntaFrecuenteSubArea entidad)
        {
            try
            {
                //crea la entidad padre
                TPreguntaFrecuenteSubArea modelo = _mapper.Map<TPreguntaFrecuenteSubArea>(entidad);

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

        public TPreguntaFrecuenteSubArea Add(PreguntaFrecuenteSubArea entidad)
        {
            try
            {
                var PreguntaFrecuenteSubArea = MapeoEntidad(entidad);
                base.Insert(PreguntaFrecuenteSubArea);
                return PreguntaFrecuenteSubArea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntaFrecuenteSubArea Update(PreguntaFrecuenteSubArea entidad)
        {
            try
            {
                var PreguntaFrecuenteSubArea = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PreguntaFrecuenteSubArea.RowVersion = entidadExistente.RowVersion;

                base.Update(PreguntaFrecuenteSubArea);
                return PreguntaFrecuenteSubArea;
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


        public IEnumerable<TPreguntaFrecuenteSubArea> Add(IEnumerable<PreguntaFrecuenteSubArea> listadoEntidad)
        {
            try
            {
                List<TPreguntaFrecuenteSubArea> listado = new List<TPreguntaFrecuenteSubArea>();
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

        public IEnumerable<TPreguntaFrecuenteSubArea> Update(IEnumerable<PreguntaFrecuenteSubArea> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPreguntaFrecuenteSubArea> listado = new List<TPreguntaFrecuenteSubArea>();
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
        /// Autor: Gilmer Qm.
        /// Fecha: 20/06/2022
        /// <summary>
        /// Obtiene todo los registros con Estado=1 de T_PreguntaFrecuenteSubArea
        /// </summary>
        /// <returns> List<PreguntaFrecuenteSubAreaDTO> </returns>
        public IEnumerable<PreguntaFrecuenteSubAreaDTO> Obtener()
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPreguntaFrecuente,
                                   IdSubArea
                            FROM pla.T_PreguntaFrecuenteSubArea
                            WHERE Estado = 1
                            ORDER BY Id DESC;";
                var preguntaFrecuente = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(preguntaFrecuente) && !preguntaFrecuente.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PreguntaFrecuenteSubAreaDTO>>(preguntaFrecuente);
                }
                return new List<PreguntaFrecuenteSubAreaDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 22/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros por el IdPreguntaFrecuente
        /// </summary>
        /// <param name="idPreguntaFrecuente"> (PK) de T_PreguntaFrecuente </param>
        /// <returns> IEnumerable<PreguntaFrecuenteSubArea> </returns>
        public IEnumerable<PreguntaFrecuenteSubArea> ObtenerPorIdPreguntaFrecuente(int idPreguntaFrecuente)
        {
            try
            {
                var query = @"SELECT  Id,
                                    IdPreguntaFrecuente,
                                    IdSubArea,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion
                            FROM pla.T_PreguntaFrecuenteSubArea
                            WHERE Estado = 1
                                  AND IdPreguntaFrecuente = @IdPreguntaFrecuente;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPreguntaFrecuente = idPreguntaFrecuente });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PreguntaFrecuenteSubArea>>(resultado);
                }
                return new List<PreguntaFrecuenteSubArea>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 22/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros por el IdPreguntaFrecuente
        /// </summary>
        /// <param name="idPreguntaFrecuente"> (PK) de T_PreguntaFrecuente </param>
        /// <param name="idSubArea"> (PK) de T_SubAreaCapacitacion </param>
        /// <returns> IEnumerable<PreguntaFrecuenteSubArea> </returns>
        public PreguntaFrecuenteSubArea ObtenerPorIdPreguntaFrecuenteYIdSubArea(int idPreguntaFrecuente, int idSubArea)
        {
            try
            {
                var query = @"SELECT  Id,
                                    IdPreguntaFrecuente,
                                    IdSubArea,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion
                            FROM pla.T_PreguntaFrecuenteSubArea
                            WHERE Estado = 1
                                  AND IdPreguntaFrecuente = @IdPreguntaFrecuente
                                  AND IdSubArea = @IdSubArea;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPreguntaFrecuente = idPreguntaFrecuente, IdSubArea=idSubArea });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<PreguntaFrecuenteSubArea>(resultado);
                }
                return new PreguntaFrecuenteSubArea();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
