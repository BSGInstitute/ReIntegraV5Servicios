using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class PgeneralTagsPwRepository : GenericRepository<TPgeneralTagsPw>, IPgeneralTagsPwRepository
    {
        private Mapper _mapper;

        public PgeneralTagsPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralTagsPw, PgeneralTagsPw>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPgeneralTagsPw MapeoEntidad(PgeneralTagsPw entidad)
        {
            try
            {
                //crea la entidad padre
                TPgeneralTagsPw modelo = _mapper.Map<TPgeneralTagsPw>(entidad);

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

        public TPgeneralTagsPw Add(PgeneralTagsPw entidad)
        {
            try
            {
                var TipoDescuento = MapeoEntidad(entidad);
                base.Insert(TipoDescuento);
                return TipoDescuento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralTagsPw Update(PgeneralTagsPw entidad)
        {
            try
            {
                var TipoDescuento = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoDescuento.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoDescuento);
                return TipoDescuento;
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


        public IEnumerable<TPgeneralTagsPw> Add(IEnumerable<PgeneralTagsPw> listadoEntidad)
        {
            try
            {
                List<TPgeneralTagsPw> listado = new List<TPgeneralTagsPw>();
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

        public IEnumerable<TPgeneralTagsPw> Update(IEnumerable<PgeneralTagsPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralTagsPw> listado = new List<TPgeneralTagsPw>();
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

        /// Autor: Giancarlo Romero Monroy
        /// Fecha: 25/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Tags por Programa General
        /// </summary>
        /// <param name="idPgeneral"></param>
        /// <returns> Lista DTO - List<TagPwDTO> </returns>
        public List<int> ObtenerIdsTagPorPGeneral(int idPgeneral)
        {
            try
            {
                string query = @"SELECT IdTagPW as Valor
                                 FROM pla.T_PGeneralTags_PW
                                 WHERE Estado = 1 AND IdPgeneral = @idPgeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPgeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    var resp = JsonConvert.DeserializeObject<IEnumerable<IntDTO>>(resultado)!;
                    return resp.Select(x => x.Valor!.Value).ToList();
                }
                return new List<int>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGTPwR-OTPw-001@Error en ObtenerTagPw() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 25/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los datos de la tabla T_PGeneralTags_PW por medio del idPGeneral y su idTag
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <param name="idTag"></param>
        /// <returns></returns>
        public PgeneralTagsPw? ObtenerPorIdPGeneralyIdTagPw(int idPGeneral, int idTag)
        {
            try
            {
                string query = @"SELECT 
                                    Id,
                                    IdPGeneral,
                                    IdTagPW,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion 
                                 FROM pla.T_PGeneralTags_PW
                                 WHERE IdPGeneral = @idPGeneral AND IdTagPW = @idTag AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPGeneral, idTag });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PgeneralTagsPw>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#TPwR-OPIPGyITPw-002@Error en ObtenerPorIdPGeneralyIdTagPw() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 25/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla por medio del IdTagPW.
        /// </summary>
        /// <param name="idTag"></param>
        /// <returns> Lista DTO - List<PgeneralTagsPw> </returns>
        public IEnumerable<PgeneralTagsPw> ObtenerPorIdTag(int idTag)
        {
            try
            {
                string query = @"SELECT 
                                    Id,
                                    IdPGeneral,
                                    IdTagPW,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion 
                                 FROM pla.T_PGeneralTags_PW
                                 WHERE IdTagPW = @idTag AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { idTag });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PgeneralTagsPw>>(resultado)!;
                }
                return new List<PgeneralTagsPw>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#TPwR-OPIT-002@Error en ObtenerPorIdTag() {ex.Message}", ex);
            }
        }
    }
}
