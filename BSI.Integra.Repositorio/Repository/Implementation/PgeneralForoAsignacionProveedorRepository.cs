using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PgeneralForoAsignacionProveedorRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PgeneralForoAsignacionProveedor
    /// </summary>
    public class PgeneralForoAsignacionProveedorRepository : GenericRepository<TPgeneralForoAsignacionProveedor>, IPgeneralForoAsignacionProveedorRepository
    {
        private Mapper _mapper;

        public PgeneralForoAsignacionProveedorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralForoAsignacionProveedor, PgeneralForoAsignacionProveedor>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPgeneralForoAsignacionProveedor MapeoEntidad(PgeneralForoAsignacionProveedor entidad)
        {
            try
            {
                //crea la entidad padre
                TPgeneralForoAsignacionProveedor modelo = _mapper.Map<TPgeneralForoAsignacionProveedor>(entidad);

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

        public TPgeneralForoAsignacionProveedor Add(PgeneralForoAsignacionProveedor entidad)
        {
            try
            {
                var PGeneralForoAsignacionProveedor = MapeoEntidad(entidad);
                base.Insert(PGeneralForoAsignacionProveedor);
                return PGeneralForoAsignacionProveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralForoAsignacionProveedor Update(PgeneralForoAsignacionProveedor entidad)
        {
            try
            {
                var PGeneralForoAsignacionProveedor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PGeneralForoAsignacionProveedor.RowVersion = entidadExistente.RowVersion;

                base.Update(PGeneralForoAsignacionProveedor);
                return PGeneralForoAsignacionProveedor;
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


        public IEnumerable<TPgeneralForoAsignacionProveedor> Add(IEnumerable<PgeneralForoAsignacionProveedor> listadoEntidad)
        {
            try
            {
                List<TPgeneralForoAsignacionProveedor> listado = new List<TPgeneralForoAsignacionProveedor>();
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

        public IEnumerable<TPgeneralForoAsignacionProveedor> Update(IEnumerable<PgeneralForoAsignacionProveedor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralForoAsignacionProveedor> listado = new List<TPgeneralForoAsignacionProveedor>();
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
        /// Autor: Gilmer Qm
        /// Fecha: 20/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros asociados al IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <returns> IEnumerable<PGeneralForoAsignacionProveedor> </returns>
        public IEnumerable<PgeneralForoAsignacionProveedor> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                string query = @"SELECT Id,
                                       IdPGeneral,
                                       IdModalidadCurso,
                                       IdProveedor
                                FROM pla.T_PGeneralForoAsignacionProveedor
                                WHERE Estado = 1
                                      AND IdPGeneral = @IdPGeneral;";
                string queryResultado = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(queryResultado) && !queryResultado.Contains("null"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PgeneralForoAsignacionProveedor>>(queryResultado)!;
                }
                return new List<PgeneralForoAsignacionProveedor>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
