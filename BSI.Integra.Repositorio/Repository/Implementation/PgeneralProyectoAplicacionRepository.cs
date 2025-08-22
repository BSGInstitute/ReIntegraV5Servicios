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
    /// Repositorio: PgeneralProyectoAplicacionRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PgeneralProyectoAplicacion
    /// </summary>
    public class PgeneralProyectoAplicacionRepository : GenericRepository<TPgeneralProyectoAplicacion>, IPgeneralProyectoAplicacionRepository
    {
        private Mapper _mapper;

        public PgeneralProyectoAplicacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralProyectoAplicacion, PgeneralProyectoAplicacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralProyectoAplicacionProveedor, PgeneralProyectoAplicacionProveedor>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralProyectoAplicacionModalidad, PgeneralProyectoAplicacionModalidad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPgeneralProyectoAplicacion MapeoEntidad(PgeneralProyectoAplicacion entidad)
        {
            try
            {
                //crea la entidad padre
                TPgeneralProyectoAplicacion modelo = _mapper.Map<TPgeneralProyectoAplicacion>(entidad);

                //mapea los hijos
                if (entidad.PgeneralProyectoAplicacionProveedor != null && entidad.PgeneralProyectoAplicacionProveedor.Count > 0)
                    modelo.TPgeneralProyectoAplicacionProveedors = _mapper.Map<List<TPgeneralProyectoAplicacionProveedor>>(entidad.PgeneralProyectoAplicacionProveedor);

                if (entidad.PgeneralProyectoAplicacionModalidad != null && entidad.PgeneralProyectoAplicacionModalidad.Count > 0)
                    modelo.TPgeneralProyectoAplicacionModalidads = _mapper.Map<List<TPgeneralProyectoAplicacionModalidad>>(entidad.PgeneralProyectoAplicacionModalidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralProyectoAplicacion Add(PgeneralProyectoAplicacion entidad)
        {
            try
            {
                var PgeneralProyectoAplicacion = MapeoEntidad(entidad);
                base.Insert(PgeneralProyectoAplicacion);
                return PgeneralProyectoAplicacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralProyectoAplicacion Update(PgeneralProyectoAplicacion entidad)
        {
            try
            {
                var PgeneralProyectoAplicacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralProyectoAplicacion.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralProyectoAplicacion);
                return PgeneralProyectoAplicacion;
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


        public IEnumerable<TPgeneralProyectoAplicacion> Add(IEnumerable<PgeneralProyectoAplicacion> listadoEntidad)
        {
            try
            {
                List<TPgeneralProyectoAplicacion> listado = new List<TPgeneralProyectoAplicacion>();
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

        public IEnumerable<TPgeneralProyectoAplicacion> Update(IEnumerable<PgeneralProyectoAplicacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralProyectoAplicacion> listado = new List<TPgeneralProyectoAplicacion>();
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
        /// Obtiene la lista de PgeneralProyectoAplicacion asociados por el IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> IEnumerable<PgeneralProyectoAplicacionDTO> </returns>
        public IEnumerable<PgeneralProyectoAplicacionAlternoDTO> ObtenerPgeneralProyectoAplicacionPorIdPGeneral(int idPGeneral)
        {
            IEnumerable<PgeneralProyectoAplicacionAlternoDTO> rpta = new List<PgeneralProyectoAplicacionAlternoDTO>();
            string query = "SELECT Id FROM pla.V_ObtenerPgeneralProyectoAplicacion WHERE Estado=1 AND IdPgeneral=@idPGeneral";
            string resultado = _dapperRepository.QueryDapper(query, new { IdPgeneral = idPGeneral });
            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<IEnumerable<PgeneralProyectoAplicacionAlternoDTO>>(resultado)!;
                foreach (var item in rpta)
                {
                    item.Modalidades = new List<int>();
                    item.Proveedores = new List<int>();

                    string queryModalidad = "SELECT IdModalidadCurso AS Valor FROM pla.T_PgeneralProyectoAplicacionModalidad WHERE Estado=1 AND IdPgeneralProyectoAplicacion=@Id";
                    string resultadoModalidad = _dapperRepository.QueryDapper(queryModalidad, new { item.Id });
                    if (!string.IsNullOrEmpty(resultadoModalidad) && !resultadoModalidad.Contains("[]"))
                    {
                        var rptaModalidad = JsonConvert.DeserializeObject<List<IntDTO>>(resultadoModalidad)!;
                        item.Modalidades = rptaModalidad.Select(x => x.Valor ?? 4).ToList();
                    }
                    string queryProveedor = "SELECT IdProveedor AS Valor From pla.T_PgeneralProyectoAplicacionProveedor WHERE Estado=1 AND IdPgeneralProyectoAplicacion=@Id";
                    string resultadoProveedor = _dapperRepository.QueryDapper(queryProveedor, new { item.Id });
                    if (!string.IsNullOrEmpty(resultadoProveedor) && !resultadoProveedor.Contains("[]"))
                    {
                        var rptaProveedor = JsonConvert.DeserializeObject<List<IntDTO>>(resultadoProveedor)!;
                        item.Proveedores = rptaProveedor.Select(x => x.Valor!.Value).ToList(); ;
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
        public IEnumerable<PgeneralProyectoAplicacion> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPgeneral,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PgeneralProyectoAplicacion
                            WHERE Estado = 1
                                  AND IdPgeneral = @IdPgeneral;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PgeneralProyectoAplicacion>>(respuestaDapper);
                }
                return new List<PgeneralProyectoAplicacion>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/08/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene los registros asociados al IdPGeneral
        /// </summary>
        /// <param name="id"> (PK) de T_PGeneral </param>
        /// <returns> IEnumerable<PGeneralCodigoPartner> </returns>
        public PgeneralProyectoAplicacion? ObtenerPorId(int id)
        {
            try
            {
                var _query = @"SELECT
                                    Id,
                                   IdPgeneral,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PgeneralProyectoAplicacion
                            WHERE Estado = 1
                                  AND Id = @id;";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { id });
                if (!string.IsNullOrEmpty(respuestaDapper) && respuestaDapper != "null")
                {
                    return JsonConvert.DeserializeObject<PgeneralProyectoAplicacion>(respuestaDapper)!;
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
