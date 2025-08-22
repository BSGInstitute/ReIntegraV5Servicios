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
    /// Repositorio: TipoInteraccionRepository
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_TipoInteraccion
    /// </summary>
    public class TipoInteraccionRepository : GenericRepository<TTipoInteracccion>, ITipoInteraccionRepository
    {
        private Mapper _mapper;

        public TipoInteraccionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoInteracccion, TipoInteraccion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TTipoInteracccion MapeoEntidad(TipoInteraccion entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoInteracccion modelo = _mapper.Map<TTipoInteracccion>(entidad);

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

        public TTipoInteracccion Add(TipoInteraccion entidad)
        {
            try
            {
                var TipoInteraccion = MapeoEntidad(entidad);
                base.Insert(TipoInteraccion);
                return TipoInteraccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoInteracccion Update(TipoInteraccion entidad)
        {
            try
            {
                var TipoInteraccion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoInteraccion.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoInteraccion);
                return TipoInteraccion;
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


        public IEnumerable<TTipoInteracccion> Add(IEnumerable<TipoInteraccion> listadoEntidad)
        {
            try
            {
                List<TTipoInteracccion> listado = new List<TTipoInteracccion>();
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

        public IEnumerable<TTipoInteracccion> Update(IEnumerable<TipoInteraccion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoInteracccion> listado = new List<TTipoInteracccion>();
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
        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoInteraccion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_TipoInteracccion WHERE Estado=1";
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
        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoInteraccion
        /// </summary>
        /// <returns> List<TipoInteraccionDTO> </returns>
        public IEnumerable<TipoInteraccionDTO> ObtenerTipoInteraccion()
        {
            try
            {
                List<TipoInteraccionDTO> rpta = new List<TipoInteraccionDTO>();
                var query = @"SELECT Id, Nombre, Canal, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion
                            FROM mkt.T_TipoInteracccion
                            WHERE Estado=1 order by id desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoInteraccionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoInteraccion llenado de combo
        /// </summary>
        /// <returns> List<TipoInteraccionDTO> </returns>
        public IEnumerable<TipoInteraccionCanalDTO> ObtenerTipoInteraccionCanalCombo()
        {
            try
            {
                List<TipoInteraccionCanalDTO> rpta = new List<TipoInteraccionCanalDTO>();
                var query = @"SELECT DISTINCT Canal FROM mkt.T_TipoInteracccion WHERE Estado=1


";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoInteraccionCanalDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FiltroDTO> ObtenerPorTipoInteraccionGeneralFormulario()
        {
            try
            {
                List<FiltroDTO> rpta = new List<FiltroDTO>();
                var query = @"SELECT id, nombre FROM mkt.T_TipoInteracccion WHERE IdTipoInteraccionGeneral = 4 AND estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FiltroDTO>>(resultado);
                }
                return rpta;
            
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
