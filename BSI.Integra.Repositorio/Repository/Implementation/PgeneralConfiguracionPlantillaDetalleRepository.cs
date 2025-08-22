using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PgeneralConfiguracionPlantillaDetalleRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PgeneralConfiguracionPlantillaDetalle
    /// </summary>
    public class PgeneralConfiguracionPlantillaDetalleRepository : GenericRepository<TPgeneralConfiguracionPlantillaDetalle>, IPgeneralConfiguracionPlantillaDetalleRepository
    {
        private Mapper _mapper;

        public PgeneralConfiguracionPlantillaDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralConfiguracionPlantillaDetalle, PGeneralConfiguracionPlantillaDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralConfiguracionPlantillaEstadoMatricula, PgeneralConfiguracionPlantillaEstadoMatricula>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralConfiguracionPlantillaSubEstadoMatricula, PgeneralConfiguracionPlantillaSubEstadoMatricula>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPgeneralConfiguracionPlantillaDetalle MapeoEntidad(PGeneralConfiguracionPlantillaDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TPgeneralConfiguracionPlantillaDetalle modelo = _mapper.Map<TPgeneralConfiguracionPlantillaDetalle>(entidad);

                //mapea los hijos
                if (entidad.PgeneralConfiguracionPlantillaEstadoMatriculas != null && entidad.PgeneralConfiguracionPlantillaEstadoMatriculas.Count > 0)
                    modelo.TPgeneralConfiguracionPlantillaEstadoMatriculas = _mapper.Map<List<TPgeneralConfiguracionPlantillaEstadoMatricula>>(entidad.PgeneralConfiguracionPlantillaEstadoMatriculas);

                if (entidad.PgeneralConfiguracionPlantillaSubEstadoMatriculas != null && entidad.PgeneralConfiguracionPlantillaSubEstadoMatriculas.Count > 0)
                    modelo.TPgeneralConfiguracionPlantillaSubEstadoMatriculas = _mapper.Map<List<TPgeneralConfiguracionPlantillaSubEstadoMatricula>>(entidad.PgeneralConfiguracionPlantillaSubEstadoMatriculas);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPgeneralConfiguracionPlantillaDetalle Add(PGeneralConfiguracionPlantillaDetalle entidad)
        {
            try
            {
                var PGeneralConfiguracionPlantillaDetalle = MapeoEntidad(entidad);
                base.Insert(PGeneralConfiguracionPlantillaDetalle);
                return PGeneralConfiguracionPlantillaDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPgeneralConfiguracionPlantillaDetalle Update(PGeneralConfiguracionPlantillaDetalle entidad)
        {
            try
            {
                var PGeneralConfiguracionPlantillaDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PGeneralConfiguracionPlantillaDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(PGeneralConfiguracionPlantillaDetalle);
                return PGeneralConfiguracionPlantillaDetalle;
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
        public IEnumerable<TPgeneralConfiguracionPlantillaDetalle> Add(IEnumerable<PGeneralConfiguracionPlantillaDetalle> listadoEntidad)
        {
            try
            {
                List<TPgeneralConfiguracionPlantillaDetalle> listado = new List<TPgeneralConfiguracionPlantillaDetalle>();
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
        public IEnumerable<TPgeneralConfiguracionPlantillaDetalle> Update(IEnumerable<PGeneralConfiguracionPlantillaDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralConfiguracionPlantillaDetalle> listado = new List<TPgeneralConfiguracionPlantillaDetalle>();
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
        /// Fecha: 17/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el registor por la primary Key
        /// </summary>
        /// <param name="idPgeneralConfiguracionPlantilla"> (PK) de  </param>
        /// <returns>PGeneralConfiguracionPlantillaDetalle</returns>
        public IEnumerable<PGeneralConfiguracionPlantillaDetalle> ObtenerPorIdPgeneralConfiguracionPlantilla(int idPgeneralConfiguracionPlantilla)
        {
            string _query = @"SELECT Id,
                                   IdPgeneralConfiguracionPlantilla,
                                   IdModalidadCurso,
                                   IdOperadorComparacion,
                                   NotaAprobatoria,
                                   DeudaPendiente,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PGeneralConfiguracionPlantillaDetalle
                            WHERE Estado = 1
                                  AND IdPgeneralConfiguracionPlantilla = @IdPgeneralConfiguracionPlantilla;";
            string query = _dapperRepository.QueryDapper(_query, new { IdPgeneralConfiguracionPlantilla = idPgeneralConfiguracionPlantilla });
            if (!string.IsNullOrEmpty(query) && query != "[]")
                return JsonConvert.DeserializeObject<IEnumerable<PGeneralConfiguracionPlantillaDetalle>>(query)!;
            return new List<PGeneralConfiguracionPlantillaDetalle>();

        }

    }
}

