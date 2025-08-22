using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PgeneralProyectoAplicacionProveedorRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PgeneralProyectoAplicacionProveedor
    /// </summary>
    public class PgeneralProyectoAplicacionProveedorRepository : GenericRepository<TPgeneralProyectoAplicacionProveedor>, IPgeneralProyectoAplicacionProveedorRepository
    {
        private Mapper _mapper;

        public PgeneralProyectoAplicacionProveedorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralProyectoAplicacionProveedor, PgeneralProyectoAplicacionProveedor>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPgeneralProyectoAplicacionProveedor MapeoEntidad(PgeneralProyectoAplicacionProveedor entidad)
        {
            try
            {
                //crea la entidad padre
                TPgeneralProyectoAplicacionProveedor modelo = _mapper.Map<TPgeneralProyectoAplicacionProveedor>(entidad);

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

        public TPgeneralProyectoAplicacionProveedor Add(PgeneralProyectoAplicacionProveedor entidad)
        {
            try
            {
                var PgeneralProyectoAplicacionProveedor = MapeoEntidad(entidad);
                base.Insert(PgeneralProyectoAplicacionProveedor);
                return PgeneralProyectoAplicacionProveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralProyectoAplicacionProveedor Update(PgeneralProyectoAplicacionProveedor entidad)
        {
            try
            {
                var PgeneralProyectoAplicacionProveedor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralProyectoAplicacionProveedor.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralProyectoAplicacionProveedor);
                return PgeneralProyectoAplicacionProveedor;
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


        public IEnumerable<TPgeneralProyectoAplicacionProveedor> Add(IEnumerable<PgeneralProyectoAplicacionProveedor> listadoEntidad)
        {
            try
            {
                List<TPgeneralProyectoAplicacionProveedor> listado = new List<TPgeneralProyectoAplicacionProveedor>();
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

        public IEnumerable<TPgeneralProyectoAplicacionProveedor> Update(IEnumerable<PgeneralProyectoAplicacionProveedor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralProyectoAplicacionProveedor> listado = new List<TPgeneralProyectoAplicacionProveedor>();
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/08/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene los registros asociados al IdPGeneral
        /// </summary>
        /// <param name="id"> (PK) de T_PGeneral </param>
        /// <returns> IEnumerable<PGeneralCodigoPartner> </returns>
        public PgeneralProyectoAplicacionProveedor? ObtenerPorIdProveedorIdPgeneralProyectoAplicacion(int idProveedor, int idPgeneralProyectoAplicacion)
        {
            try
            {
                var query = @"
                        SELECT 
	                        Id,
	                        IdPgeneralProyectoAplicacion,
	                        IdProveedor,
	                        Estado,
	                        UsuarioCreacion,
	                        UsuarioModificacion,
	                        FechaCreacion,
	                        FechaModificacion,
	                        RowVersion,
	                        IdMigracion
                        FROM pla.T_PgeneralProyectoAplicacionProveedor
                        WHERE Estado = 1 AND IdProveedor = @idProveedor AND IdPgeneralProyectoAplicacion=@idPgeneralProyectoAplicacion;";
                var respuestaDapper = _dapperRepository.FirstOrDefault(query, new { idProveedor, idPgeneralProyectoAplicacion });
                if (!string.IsNullOrEmpty(respuestaDapper) && respuestaDapper != "null")
                {
                    return JsonConvert.DeserializeObject<PgeneralProyectoAplicacionProveedor>(respuestaDapper)!;
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
