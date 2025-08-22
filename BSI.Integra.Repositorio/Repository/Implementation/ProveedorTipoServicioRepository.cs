



using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProveedorTipoServicioRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 07/07/2022
    /// <summary>
    /// Gestión general de T_ProveedorTipoServicio
    /// </summary>
    public class ProveedorTipoServicioRepository : GenericRepository<TProveedorTipoServicio>, IProveedorTipoServicioRepository
    {
        private Mapper _mapper;

        public ProveedorTipoServicioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProveedorTipoServicio, ProveedorTipoServicio>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProveedorTipoServicio MapeoEntidad(ProveedorTipoServicio entidad)
        {
            try
            {
                //crea la entidad padre
                TProveedorTipoServicio modelo = _mapper.Map<TProveedorTipoServicio>(entidad);

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

        public TProveedorTipoServicio Add(ProveedorTipoServicio entidad)
        {
            try
            {
                var ProveedorTipoServicio = MapeoEntidad(entidad);
                base.Insert(ProveedorTipoServicio);
                return ProveedorTipoServicio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProveedorTipoServicio Update(ProveedorTipoServicio entidad)
        {
            try
            {
                var ProveedorTipoServicio = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProveedorTipoServicio.RowVersion = entidadExistente.RowVersion;

                base.Update(ProveedorTipoServicio);
                return ProveedorTipoServicio;
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


        public IEnumerable<TProveedorTipoServicio> Add(IEnumerable<ProveedorTipoServicio> listadoEntidad)
        {
            try
            {
                List<TProveedorTipoServicio> listado = new List<TProveedorTipoServicio>();
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

        public IEnumerable<TProveedorTipoServicio> Update(IEnumerable<ProveedorTipoServicio> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProveedorTipoServicio> listado = new List<TProveedorTipoServicio>();
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
        /// Autor: Griselberto Huaman.
        /// Fecha: 07/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProveedorTipoServicio.
        /// </summary>
        /// /// <param name="listaIdProveedor"></param>
        /// <returns> List<ProveedorTipoServicioDTO> </returns>
        public IEnumerable<ProveedorTipoServicioDTO> ObtenerProveedorTipoServicio(List<int> listaIdProveedor)
        {
            try
            {
                List<ProveedorTipoServicioDTO> rpta = new List<ProveedorTipoServicioDTO>();
                var query = @"
                                SELECT Id, 
                                       IdProveedor, 
                                       IdTipoServicio
                                FROM mkt.V_ObtenerProveedorTipoServicio
                                WHERE EstadoProveedorTipoServicio = 1
                                      AND EstadoProveedor = 1
                                      AND EstadoTipoServicio = 1
	                                  AND IdProveedor IN @listaIdProveedor ";
                var resultado = _dapperRepository.QueryDapper(query, new { listaIdProveedor = listaIdProveedor.ToArray() });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProveedorTipoServicioDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 07/07/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina de forma fisica los registros asociados al proveedor
        /// </summary>
        /// <param name="idProveedor">Identificador del Proveedor</param>
        /// <param name="usuario">Usuario que llamo al End Point</param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorPlantilla(int idProveedor, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdProveedor == idProveedor && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdProveedor));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}