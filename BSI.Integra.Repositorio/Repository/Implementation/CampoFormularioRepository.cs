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
    /// Repositorio: CampoFormularioRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_CampoFormulario
    /// </summary>
    public class CampoFormularioRepository : GenericRepository<TCampoFormulario>, ICampoFormularioRepository
    {
        private Mapper _mapper;

        public CampoFormularioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCampoFormulario, CampoFormulario>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TCampoFormulario MapeoEntidad(CampoFormulario entidad)
        {
            try
            {
                //crea la entidad padre
                TCampoFormulario modelo = _mapper.Map<TCampoFormulario>(entidad);

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

        public TCampoFormulario Add(CampoFormulario entidad)
        {
            try
            {
                var CampoFormulario = MapeoEntidad(entidad);
                base.Insert(CampoFormulario);
                return CampoFormulario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCampoFormulario Update(CampoFormulario entidad)
        {
            try
            {
                var CampoFormulario = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CampoFormulario.RowVersion = entidadExistente.RowVersion;

                base.Update(CampoFormulario);
                return CampoFormulario;
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


        public IEnumerable<TCampoFormulario> Add(IEnumerable<CampoFormulario> listadoEntidad)
        {
            try
            {
                List<TCampoFormulario> listado = new List<TCampoFormulario>();
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

        public IEnumerable<TCampoFormulario> Update(IEnumerable<CampoFormulario> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCampoFormulario> listado = new List<TCampoFormulario>();
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CampoFormulario para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_CampoFormulario WHERE Estado=1";
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CampoFormulario
        /// </summary>
        /// <returns> List<CampoFormularioDTO> </returns>
        public IEnumerable<CampoFormularioDTO> ObtenerCampoFormulario()
        {
            try
            {
                List<CampoFormularioDTO> rpta = new List<CampoFormularioDTO>();
                var query = @"SELECT Id, Nombre, Descripcion, Prioridad, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion
                            FROM mkt.T_CampoFormulario
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CampoFormularioDTO>>(resultado);
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
        /// Obtiene todos los registros de T_CampoFormulario
        /// </summary>
        /// <returns> List<CampoFormularioDTO> </returns>
        /// 
        public IEnumerable<CampoFormularioSeleccionadoDTO> ObtenerCampoFormularioPorIdFormularioSolicitud(int idFormularioSolicitud)
        {
            try
            {
                string _SPName = "[mkt].[SP_TCampoFormulario_ObtenerDetalles]";
                var jsonResult = _dapperRepository.QuerySPDapper(_SPName, new { IdFormularioSolicitud = idFormularioSolicitud });

                if (string.IsNullOrEmpty(jsonResult))
                {
                    return new List<CampoFormularioSeleccionadoDTO>();
                }

                List<CampoFormularioSeleccionadoDTO> result = JsonConvert.DeserializeObject<List<CampoFormularioSeleccionadoDTO>>(jsonResult);

                return result ?? new List<CampoFormularioSeleccionadoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
