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
    /// Repositorio: ProcedenciaFormularioRepository
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_ProcedenciaFormulario
    /// </summary>
    public class ProcedenciaFormularioRepository : GenericRepository<TProcedenciaFormulario>, IProcedenciaFormularioRepository
    {
        private Mapper _mapper;

        public ProcedenciaFormularioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProcedenciaFormulario, ProcedenciaFormulario>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TProcedenciaFormulario MapeoEntidad(ProcedenciaFormulario entidad)
        {
            try
            {
                //crea la entidad padre
                TProcedenciaFormulario modelo = _mapper.Map<TProcedenciaFormulario>(entidad);

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

        public TProcedenciaFormulario Add(ProcedenciaFormulario entidad)
        {
            try
            {
                var ProcedenciaFormulario = MapeoEntidad(entidad);
                base.Insert(ProcedenciaFormulario);
                return ProcedenciaFormulario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProcedenciaFormulario Update(ProcedenciaFormulario entidad)
        {
            try
            {
                var ProcedenciaFormulario = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProcedenciaFormulario.RowVersion = entidadExistente.RowVersion;

                base.Update(ProcedenciaFormulario);
                return ProcedenciaFormulario;
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


        public IEnumerable<TProcedenciaFormulario> Add(IEnumerable<ProcedenciaFormulario> listadoEntidad)
        {
            try
            {
                List<TProcedenciaFormulario> listado = new List<TProcedenciaFormulario>();
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

        public IEnumerable<TProcedenciaFormulario> Update(IEnumerable<ProcedenciaFormulario> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProcedenciaFormulario> listado = new List<TProcedenciaFormulario>();
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
        /// Obtiene registros de T_ProcedenciaFormulario para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_ProcedenciaFormulario WHERE Estado=1";
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
        /// Obtiene todos los registros de T_ProcedenciaFormulario
        /// </summary>
        /// <returns> List<ProcedenciaFormularioDTO> </returns>
        public IEnumerable<ProcedenciaFormularioDTO> ObtenerProcedenciaFormulario()
        {
            try
            {
                List<ProcedenciaFormularioDTO> rpta = new List<ProcedenciaFormularioDTO>();
                var query = @"SELECT Id, Nombre, Descripcion, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion
                            FROM mkt.T_ProcedenciaFormulario 
                            WHERE Estado=1 order by FechaModificacion desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProcedenciaFormularioDTO>>(resultado);
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
        /// Obtiene todos los registros de T_ProcedenciaFormulario
        /// </summary>
        /// <returns> List<ProcedenciaFormularioDTO> </returns>
        public IEnumerable<ProcedenciaFormularioFiltroDTO> ObtenerProcedenciaFormularioFiltro()
        {
            try
            {
                List<ProcedenciaFormularioFiltroDTO> rpta = new List<ProcedenciaFormularioFiltroDTO>();
                var query = @"SELECT Id, Nombre, Descripcion FROM mkt.T_ProcedenciaFormulario
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProcedenciaFormularioFiltroDTO>>(resultado);
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
        /// Obtiene todos los registros de T_ProcedenciaFormulario de acuerdo a los vlaores asignados.
        /// </summary>
        /// <returns> List<ProcedenciaFormularioDTO> </returns>

        public IEnumerable<ProcedenciaFormularioFiltroDTO> ObtenerProcedenciaFormularioTodo()
        {
            try
            {
                List<ProcedenciaFormularioFiltroDTO> rpta = new List<ProcedenciaFormularioFiltroDTO>();
                var query = @"SELECT Id, Nombre, Descripcion FROM mkt.T_ProcedenciaFormulario
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProcedenciaFormularioFiltroDTO>>(resultado);
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
