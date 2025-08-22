using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PgeneralCodigoPartnerRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PgeneralCodigoPartner
    /// </summary>
    public class PgeneralCodigoPartnerRepository : GenericRepository<TPgeneralCodigoPartner>, IPgeneralCodigoPartnerRepository
    {
        private Mapper _mapper;

        public PgeneralCodigoPartnerRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralCodigoPartner, PGeneralCodigoPartner>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralCodigoPartnerModalidadCurso, PgeneralCodigoPartnerModalidadCurso>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralCodigoPartnerVersionPrograma, PgeneralCodigoPartnerVersionPrograma>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPgeneralCodigoPartner MapeoEntidad(PGeneralCodigoPartner entidad)
        {
            try
            {
                //crea la entidad padre
                TPgeneralCodigoPartner modelo = _mapper.Map<TPgeneralCodigoPartner>(entidad);
                //mapea los hijos
                if (entidad.PgeneralCodigoPartnerModalidadCurso != null && entidad.PgeneralCodigoPartnerModalidadCurso.Count > 0)
                    modelo.TPgeneralCodigoPartnerModalidadCursos = _mapper.Map<List<TPgeneralCodigoPartnerModalidadCurso>>(entidad.PgeneralCodigoPartnerModalidadCurso);

                if (entidad.PgeneralCodigoPartnerVersionPrograma != null && entidad.PgeneralCodigoPartnerVersionPrograma.Count > 0)
                    modelo.TPgeneralCodigoPartnerVersionProgramas = _mapper.Map<List<TPgeneralCodigoPartnerVersionPrograma>>(entidad.PgeneralCodigoPartnerVersionPrograma);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralCodigoPartner Add(PGeneralCodigoPartner entidad)
        {
            try
            {
                var PGeneralCodigoPartner = MapeoEntidad(entidad);
                base.Insert(PGeneralCodigoPartner);
                return PGeneralCodigoPartner;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralCodigoPartner Update(PGeneralCodigoPartner entidad)
        {
            try
            {
                var PGeneralCodigoPartner = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PGeneralCodigoPartner.RowVersion = entidadExistente.RowVersion;

                base.Update(PGeneralCodigoPartner);
                return PGeneralCodigoPartner;
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


        public IEnumerable<TPgeneralCodigoPartner> Add(IEnumerable<PGeneralCodigoPartner> listadoEntidad)
        {
            try
            {
                List<TPgeneralCodigoPartner> listado = new List<TPgeneralCodigoPartner>();
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

        public IEnumerable<TPgeneralCodigoPartner> Update(IEnumerable<PGeneralCodigoPartner> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralCodigoPartner> listado = new List<TPgeneralCodigoPartner>();
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
        /// Autor: Gilmer Quispe.
        /// Fecha: 19/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de PgeneralCodigoPartneror asociados por el IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> IEnumerable<PgeneralCodigoPartnerDTO> </returns>
        public IEnumerable<PgeneralCodigoPartnerAlternoDTO> ObtenerPgeneralCodigoPartnerPorIdPGeneral(int idPGeneral)
        {
            IEnumerable<PgeneralCodigoPartnerAlternoDTO> rpta = new List<PgeneralCodigoPartnerAlternoDTO>();
            string query = "SELECT Id,Codigo,Pdu FROM pla.V_ObtenerCodigoPartnerPgeneral WHERE Estado=1 AND IdPgeneral=@IdPgeneral";
            string resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<IEnumerable<PgeneralCodigoPartnerAlternoDTO>>(resultado)!;
                foreach (var item in rpta)
                {
                    item.VersionesPrograma = new List<int>();
                    item.ModalidadesCurso = new List<int>();

                    string queryVersionPrograma = "SELECT ISNULL(IdVersionPrograma, 4) AS Valor FROM pla.T_PgeneralCodigoPartnerVersionPrograma WHERE Estado=1 AND IdPgeneralCodigoPartner=@Id";
                    string queryModalidadCurso = "SELECT IdModalidadCurso AS Valor FROM pla.T_PgeneralCodigoPartnerModalidadCurso WHERE Estado=1 AND IdPgeneralCodigoPartner=@Id";

                    string resultadoVersionPrograma = _dapperRepository.QueryDapper(queryVersionPrograma, new { item.Id });
                    if (!string.IsNullOrEmpty(resultadoVersionPrograma) && !resultadoVersionPrograma.Contains("[]"))
                    {
                        var rptaVersionPrograma = JsonConvert.DeserializeObject<List<IntDTO>>(resultadoVersionPrograma)!;
                        item.VersionesPrograma = rptaVersionPrograma.Select(x => x.Valor!.Value).ToList();
                    }
                    string resultadoModalidadCurso = _dapperRepository.QueryDapper(queryModalidadCurso, new { item.Id });
                    if (!string.IsNullOrEmpty(resultadoModalidadCurso) && !resultadoModalidadCurso.Contains("[]"))
                    {
                        var rptaModalidadCurso = JsonConvert.DeserializeObject<List<IntDTO>>(resultadoModalidadCurso)!;
                        item.ModalidadesCurso = rptaModalidadCurso.Select(x => x.Valor!.Value).ToList();
                    }
                }
            }
            return rpta;
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene los registros asociados al IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <returns> IEnumerable<PGeneralCodigoPartner> </returns>
        public IEnumerable<PGeneralCodigoPartner> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPgeneral,
                                   Codigo,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PgeneralCodigoPartner
                            WHERE Estado = 1
                                  AND IdPgeneral = @IdPgeneral;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PGeneralCodigoPartner>>(respuestaDapper);
                }
                return new List<PGeneralCodigoPartner>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/08/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="id"> </param>
        /// <returns></returns>
        public PGeneralCodigoPartner? ObtenerPorId(int id)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPgeneral,
                                   Codigo,
                                   Estado,
                                   Pdu,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PgeneralCodigoPartner
                            WHERE Estado = 1
                                  AND Id = @id;";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { id });
                if (!string.IsNullOrEmpty(respuestaDapper) && respuestaDapper != "null")
                {
                    return JsonConvert.DeserializeObject<PGeneralCodigoPartner>(respuestaDapper);
                }
                return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}