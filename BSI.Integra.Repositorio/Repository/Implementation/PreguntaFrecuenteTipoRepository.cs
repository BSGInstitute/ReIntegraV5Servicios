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
    /// Repositorio: PreguntaFrecuenteTipoRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PreguntaFrecuenteTipo
    /// </summary>
    public class PreguntaFrecuenteTipoRepository : GenericRepository<TPreguntaFrecuenteTipo>, IPreguntaFrecuenteTipoRepository
    {
        private Mapper _mapper;

        public PreguntaFrecuenteTipoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaFrecuenteTipo, PreguntaFrecuenteTipo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TPreguntaFrecuenteTipo MapeoEntidad(PreguntaFrecuenteTipo entidad)
        {
            try
            {
                //crea la entidad padre
                TPreguntaFrecuenteTipo modelo = _mapper.Map<TPreguntaFrecuenteTipo>(entidad);

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

        public TPreguntaFrecuenteTipo Add(PreguntaFrecuenteTipo entidad)
        {
            try
            {
                var PreguntaFrecuenteTipo = MapeoEntidad(entidad);
                base.Insert(PreguntaFrecuenteTipo);
                return PreguntaFrecuenteTipo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntaFrecuenteTipo Update(PreguntaFrecuenteTipo entidad)
        {
            try
            {
                var PreguntaFrecuenteTipo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PreguntaFrecuenteTipo.RowVersion = entidadExistente.RowVersion;

                base.Update(PreguntaFrecuenteTipo);
                return PreguntaFrecuenteTipo;
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


        public IEnumerable<TPreguntaFrecuenteTipo> Add(IEnumerable<PreguntaFrecuenteTipo> listadoEntidad)
        {
            try
            {
                List<TPreguntaFrecuenteTipo> listado = new List<TPreguntaFrecuenteTipo>();
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

        public IEnumerable<TPreguntaFrecuenteTipo> Update(IEnumerable<PreguntaFrecuenteTipo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPreguntaFrecuenteTipo> listado = new List<TPreguntaFrecuenteTipo>();
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
        /// Obtiene todo los registros con Estado=1 de T_PreguntaFrecuenteTipo
        /// </summary>
        /// <returns> List<PreguntaFrecuenteTipoDTO> </returns>
        public IEnumerable<PreguntaFrecuenteTipoDTO> Obtener()
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPreguntaFrecuente,
                                   IdTipo
                            FROM pla.T_PreguntaFrecuenteTipo
                            WHERE Estado = 1
                            ORDER BY Id DESC;";
                var preguntaFrecuente = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(preguntaFrecuente) && !preguntaFrecuente.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PreguntaFrecuenteTipoDTO>>(preguntaFrecuente);
                }
                return new List<PreguntaFrecuenteTipoDTO>();
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
        public IEnumerable<PreguntaFrecuenteTipo> ObtenerPorIdPreguntaFrecuente(int idPreguntaFrecuente)
        {
            try
            {
                var query = @"SELECT  Id,
                                    IdPreguntaFrecuente,
                                    IdTipo,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion
                            FROM pla.T_PreguntaFrecuenteTipo
                            WHERE Estado = 1
                                  AND IdPreguntaFrecuente = @IdPreguntaFrecuente;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPreguntaFrecuente = idPreguntaFrecuente });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PreguntaFrecuenteTipo>>(resultado);
                }
                return new List<PreguntaFrecuenteTipo>();
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
        /// <param name="idTipo"> (PK) de T_ModalidadCurso </param>
        /// <returns> PreguntaFrecuenteTipo </returns>
        public PreguntaFrecuenteTipo ObtenerPorIdPreguntaFrecuenteYIdTipo(int idPreguntaFrecuente, int idTipo)
        {
            try
            {
                var query = @"SELECT  Id,
                                    IdPreguntaFrecuente,
                                    IdTipo,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion
                            FROM pla.T_PreguntaFrecuenteTipo
                            WHERE Estado = 1
                                  AND IdPreguntaFrecuente = @IdPreguntaFrecuente
                                  AND IdTipo=@IdTipo";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPreguntaFrecuente = idPreguntaFrecuente, IdTipo = idTipo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<PreguntaFrecuenteTipo>(resultado);
                }
                return new PreguntaFrecuenteTipo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
