using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProveedorCalificacionRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ProveedorCalificacion
    /// </summary>
    public class ProveedorCalificacionRepository : GenericRepository<TProveedorCalificacion>, IProveedorCalificacionRepository
    {
        private Mapper _mapper;

        public ProveedorCalificacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProveedorCalificacion, ProveedorCalificacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProveedorCalificacion MapeoEntidad(ProveedorCalificacion entidad)
        {
            try
            {
                //crea la entidad padre
                TProveedorCalificacion modelo = _mapper.Map<TProveedorCalificacion>(entidad);

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

        public TProveedorCalificacion Add(ProveedorCalificacion entidad)
        {
            try
            {
                var ProveedorCalificacion = MapeoEntidad(entidad);
                base.Insert(ProveedorCalificacion);
                return ProveedorCalificacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProveedorCalificacion Update(ProveedorCalificacion entidad)
        {
            try
            {
                var ProveedorCalificacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProveedorCalificacion.RowVersion = entidadExistente.RowVersion;

                base.Update(ProveedorCalificacion);
                return ProveedorCalificacion;
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


        public IEnumerable<TProveedorCalificacion> Add(IEnumerable<ProveedorCalificacion> listadoEntidad)
        {
            try
            {
                List<TProveedorCalificacion> listado = new List<TProveedorCalificacion>();
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

        public IEnumerable<TProveedorCalificacion> Update(IEnumerable<ProveedorCalificacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProveedorCalificacion> listado = new List<TProveedorCalificacion>();
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
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProveedorCalificacion.
        /// </summary>
        /// <returns> List<ProveedorCalificacionDTO> </returns>
        public IEnumerable<ProveedorCalificacionDTO> ObtenerProveedorCalificacion()
        {
            try
            {
                List<ProveedorCalificacionDTO> rpta = new List<ProveedorCalificacionDTO>();
                var query = @"
                SELECT [Id]
                      ,[IdProveedor]
                      ,[IdProveedorSubCriterioCalificacion]
                      ,[Estado]
                      ,[UsuarioCreacion]
                      ,[UsuarioModificacion]
                      ,[FechaCreacion]
                      ,[FechaModificacion]
                FROM [fin].[T_ProveedorCalificacion]
                WHERE Estado =1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProveedorCalificacionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
