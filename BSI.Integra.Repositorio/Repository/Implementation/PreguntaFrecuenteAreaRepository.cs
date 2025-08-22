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
    /// Repositorio: PreguntaFrecuenteAreaRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PreguntaFrecuenteArea
    /// </summary>
    public class PreguntaFrecuenteAreaRepository : GenericRepository<TPreguntaFrecuenteArea>, IPreguntaFrecuenteAreaRepository
    {
        private Mapper _mapper;

        public PreguntaFrecuenteAreaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaFrecuenteArea, PreguntaFrecuenteArea>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPreguntaFrecuenteArea MapeoEntidad(PreguntaFrecuenteArea entidad)
        {
            try
            {
                //crea la entidad padre
                TPreguntaFrecuenteArea modelo = _mapper.Map<TPreguntaFrecuenteArea>(entidad);

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

        public TPreguntaFrecuenteArea Add(PreguntaFrecuenteArea entidad)
        {
            try
            {
                var PreguntaFrecuenteArea = MapeoEntidad(entidad);
                base.Insert(PreguntaFrecuenteArea);
                return PreguntaFrecuenteArea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntaFrecuenteArea Update(PreguntaFrecuenteArea entidad)
        {
            try
            {
                var PreguntaFrecuenteArea = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PreguntaFrecuenteArea.RowVersion = entidadExistente.RowVersion;

                base.Update(PreguntaFrecuenteArea);
                return PreguntaFrecuenteArea;
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


        public IEnumerable<TPreguntaFrecuenteArea> Add(IEnumerable<PreguntaFrecuenteArea> listadoEntidad)
        {
            try
            {
                List<TPreguntaFrecuenteArea> listado = new List<TPreguntaFrecuenteArea>();
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

        public IEnumerable<TPreguntaFrecuenteArea> Update(IEnumerable<PreguntaFrecuenteArea> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPreguntaFrecuenteArea> listado = new List<TPreguntaFrecuenteArea>();
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
        /// Obtiene todo los registros con Estado=1 de T_PreguntaFrecuenteArea
        /// </summary>
        /// <returns> List<PreguntaFrecuenteAreaDTO> </returns>
        public IEnumerable<PreguntaFrecuenteAreaDTO> Obtener()
        {
            try
            {
                var _query = @"SELECT  Id,
                                    IdPreguntaFrecuente,
                                    IdArea 
                            FROM pla.T_PreguntaFrecuenteArea
                            WHERE Estado = 1 order by Id desc;";
                var preguntaFrecuente = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(preguntaFrecuente) && !preguntaFrecuente.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PreguntaFrecuenteAreaDTO>>(preguntaFrecuente);
                }
                return new List<PreguntaFrecuenteAreaDTO>();
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
        /// <returns> IEnumerable<PreguntaFrecuentePGeneral> </returns>
        public IEnumerable<PreguntaFrecuenteArea> ObtenerPorIdPreguntaFrecuente(int idPreguntaFrecuente)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdPreguntaFrecuente,
                                   IdArea,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PreguntaFrecuenteArea
                            WHERE Estado = 1
                                  AND IdPreguntaFrecuente = @IdPreguntaFrecuente;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPreguntaFrecuente = idPreguntaFrecuente });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PreguntaFrecuenteArea>>(resultado);
                }
                return new List<PreguntaFrecuenteArea>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 22/06/2023
        /// Version: 1.0
        /// Obtiene los registros por el IdPreguntaFrecuente y IdPGeneral
        /// <summary>
        /// <param name="idPreguntaFrecuente"> (PK) de T_PreguntaFrecuente </param>
        /// <param name="idArea"> (PK) de T_AreaCapacitacion </param>
        /// </summary>
        /// <returns> PreguntaFrecuenteArea </returns>
        public PreguntaFrecuenteArea ObtenerPorIdPreguntaFrecuenteYIdArea(int idPreguntaFrecuente, int idArea)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdPreguntaFrecuente,
                                   IdArea,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PreguntaFrecuenteArea
                            WHERE Estado = 1
                                  AND IdPreguntaFrecuente = @IdPreguntaFrecuente
                                  AND IdArea = @IdArea;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPreguntaFrecuente = idPreguntaFrecuente, IdArea = idArea });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<PreguntaFrecuenteArea>(resultado);
                }
                return new PreguntaFrecuenteArea();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}