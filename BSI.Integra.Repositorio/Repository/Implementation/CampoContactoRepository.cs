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
    /// Repositorio: CampoContactoRepository
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_CampoContacto
    /// </summary>
    public class CampoContactoRepository : GenericRepository<TCampoContacto>, ICampoContactoRepository
    {
        private Mapper _mapper;

        public CampoContactoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCampoContacto, CampoContacto>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TCampoContacto MapeoEntidad(CampoContacto entidad)
        {
            try
            {
                //crea la entidad padre
                TCampoContacto modelo = _mapper.Map<TCampoContacto>(entidad);

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

        public TCampoContacto Add(CampoContacto entidad)
        {
            try
            {
                var CampoContacto = MapeoEntidad(entidad);
                base.Insert(CampoContacto);
                return CampoContacto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCampoContacto Update(CampoContacto entidad)
        {
            try
            {
                var CampoContacto = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CampoContacto.RowVersion = entidadExistente.RowVersion;

                base.Update(CampoContacto);
                return CampoContacto;
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


        public IEnumerable<TCampoContacto> Add(IEnumerable<CampoContacto> listadoEntidad)
        {
            try
            {
                List<TCampoContacto> listado = new List<TCampoContacto>();
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

        public IEnumerable<TCampoContacto> Update(IEnumerable<CampoContacto> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCampoContacto> listado = new List<TCampoContacto>();
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
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CampoContacto para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_CampoContacto WHERE Estado=1";
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
        /// Fecha: 31/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CampoContacto
        /// </summary>
        /// <returns> List<CampoContactoDTO> </returns>
        public IEnumerable<CampoContactoDTO> ObtenerCampoContacto()
        {
            try
            {
                List<CampoContactoDTO> rpta = new List<CampoContactoDTO>();
                var query = @"SELECT Id, Nombre, TipoControl, ValoresPreEstablecidos,Procedimiento, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion
                            FROM mkt.T_CampoContacto
                            WHERE Estado=1 order by id desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CampoContactoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 31/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros mediante filtros  de T_CampoContacto
        /// </summary>
        /// <returns> List<CampoContactoDTO> </returns>

        public IEnumerable<CampoContactoFiltroDTO> ObtenerFiltroCampoContacto()
        {
            try
            {
                List<CampoContactoFiltroDTO> rpta = new List<CampoContactoFiltroDTO>();
                var query = @"SELECT Id, Nombre FROM mkt.T_CampoContacto
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CampoContactoFiltroDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 31/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros mediante los valores  asignaddos T_CampoContacto
        /// </summary>
        /// <returns> List<CampoContactoDTO> </returns>

        public IEnumerable<CampoContactoTodoDTO> ObtenerFiltroCampoContactoTodo()
        {
            try
            {
                List<CampoContactoTodoDTO> rpta = new List<CampoContactoTodoDTO>();
                var query = @"SELECT Id, Nombre, TipoControl, ValoresPreEstablecidos,Procedimiento
                            FROM mkt.T_CampoContacto
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CampoContactoTodoDTO>>(resultado);
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

