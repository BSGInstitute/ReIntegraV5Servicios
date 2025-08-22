using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB; 
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: EstadoPagoMatriculaRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_EstadoPagoMatricula
    /// </summary>
    public class EstadoPagoMatriculaRepository : GenericRepository<TEstadoPagoMatricula>, IEstadoPagoMatriculaRepository
    {
        private Mapper _mapper;

        public EstadoPagoMatriculaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEstadoPagoMatricula, EstadoPagoMatricula>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TEstadoPagoMatricula MapeoEntidad(EstadoPagoMatricula entidad)
        {
            try
            {
                //crea la entidad padre
                TEstadoPagoMatricula modelo = _mapper.Map<TEstadoPagoMatricula>(entidad);

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

        public TEstadoPagoMatricula Add(EstadoPagoMatricula entidad)
        {
            try
            {
                var EstadoPagoMatricula = MapeoEntidad(entidad);
                base.Insert(EstadoPagoMatricula);
                return EstadoPagoMatricula;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEstadoPagoMatricula Update(EstadoPagoMatricula entidad)
        {
            try
            {
                var EstadoPagoMatricula = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EstadoPagoMatricula.RowVersion = entidadExistente.RowVersion;

                base.Update(EstadoPagoMatricula);
                return EstadoPagoMatricula;
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


        public IEnumerable<TEstadoPagoMatricula> Add(IEnumerable<EstadoPagoMatricula> listadoEntidad)
        {
            try
            {
                List<TEstadoPagoMatricula> listado = new List<TEstadoPagoMatricula>();
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

        public IEnumerable<TEstadoPagoMatricula> Update(IEnumerable<EstadoPagoMatricula> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEstadoPagoMatricula> listado = new List<TEstadoPagoMatricula>();
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

        /// Autor: Margiory Ramirez
        /// Fecha: 17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene todos los estados de matricula 
        /// </summary>
        /// <returns>Json/returns>

        public List<ComboDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = "SELECT id, nombre FROM fin.T_EstadoPagoMatricula WHERE estado = 1";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public object ObtenerTodoEstadoMatricula()
        {
            try
            {

                return GetBy(x => x.Estado == true, x => new { x.Id, EstadoMatricula = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto
        /// Fecha: 25/07/2023
        /// Versión: 1.0
        /// <summary>
		/// Obtiene el Id y Nombre del estado pago Matricula (pormatricular y matriculado)
		/// </summary>
		/// <returns></returns>
		public List<ComboDTO> ObtenerEstadoPagoMatriculaDevoluciones()
        {
            try
            {
                List<ComboDTO> estadoPagoMatricula = new List<ComboDTO>();
                string _queryEstadoPagoMatricula = string.Empty;
                _queryEstadoPagoMatricula = "SELECT Id,Nombre FROM fin.V_ObtenerEstadoPagoMatricula where Id > 2 and Estado = 1  ";
                var AlumnoDB = _dapperRepository.QueryDapper(_queryEstadoPagoMatricula, new { });
                if (!string.IsNullOrEmpty(AlumnoDB) && !AlumnoDB.Contains("[]"))
                {
                    estadoPagoMatricula = JsonConvert.DeserializeObject<List<ComboDTO>>(AlumnoDB);
                }
                return estadoPagoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




    }
}