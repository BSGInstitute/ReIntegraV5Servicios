using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProveedorCampaniaIntegraRepository
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha:22/08/2022
    /// <summary>
    /// Gestión general de T_ProveedorCampaniaIntegra
    /// </summary>
    public class ProveedorCampaniaIntegraRepository : GenericRepository<TProveedorCampaniaIntegra>, IProveedorCampaniaIntegraRepository
    {
        private Mapper _mapper;

        public ProveedorCampaniaIntegraRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProveedorCampaniaIntegra, ProveedorCampaniaIntegra>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TProveedorCampaniaIntegra MapeoEntidad(ProveedorCampaniaIntegra entidad)
        {
            try
            {
                //crea la entidad padre
                TProveedorCampaniaIntegra modelo = _mapper.Map<TProveedorCampaniaIntegra>(entidad);

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

        public TProveedorCampaniaIntegra Add(ProveedorCampaniaIntegra entidad)
        {
            try
            {
                var ProveedorCampaniaIntegra = MapeoEntidad(entidad);
                base.Insert(ProveedorCampaniaIntegra);
                return ProveedorCampaniaIntegra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProveedorCampaniaIntegra Update(ProveedorCampaniaIntegra entidad)
        {
            try
            {
                var ProveedorCampaniaIntegra = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProveedorCampaniaIntegra.RowVersion = entidadExistente.RowVersion;

                base.Update(ProveedorCampaniaIntegra);
                return ProveedorCampaniaIntegra;
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


        public IEnumerable<TProveedorCampaniaIntegra> Add(IEnumerable<ProveedorCampaniaIntegra> listadoEntidad)
        {
            try
            {
                List<TProveedorCampaniaIntegra> listado = new List<TProveedorCampaniaIntegra>();
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

        public IEnumerable<TProveedorCampaniaIntegra> Update(IEnumerable<ProveedorCampaniaIntegra> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProveedorCampaniaIntegra> listado = new List<TProveedorCampaniaIntegra>();
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
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha:22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProveedorCampaniaIntegra para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_ProveedorCampaniaIntegra WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha:22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProveedorCampaniaIntegra
        /// </summary>
        /// <returns> List<ProveedorCampaniaIntegraDTO> </returns>
        public IEnumerable<ProveedorCampaniaIntegraDTO> ObtenerProveedorCampaniaIntegra()
        {
            try
            {
                List<ProveedorCampaniaIntegraDTO> rpta = new List<ProveedorCampaniaIntegraDTO>();
                var query = @"SELECT Id, Nombre, PorDefecto, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion 
                            FROM mkt.T_ProveedorCampaniaIntegra
                            WHERE Estado=1 order by id desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProveedorCampaniaIntegraDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Meiss.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProveedorCampaniaIntegra
        /// </summary>
        /// <returns> List<ProveedorCampaniaIntegraDTO> </returns>
        public IEnumerable<ProveedorCampaniaIntegraFiltroDTO> ObtenerProveedorCampaniaIntegraFiltro()
        {
            try
            {
                List<ProveedorCampaniaIntegraFiltroDTO> rpta = new List<ProveedorCampaniaIntegraFiltroDTO>();
                var query = @"SELECT Id, Nombre, PorDefecto FROM mkt.T_ProveedorCampaniaIntegra
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProveedorCampaniaIntegraFiltroDTO>>(resultado);
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
