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
    /// Repositorio: ConfiguracionDatoRemarketingRepository
    /// Autor: Margiory Ramirez Neyra..
    /// Fecha: 04/10/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionDatoRemarketing
    /// </summary>
    public class ConfiguracionDatoRemarketingRepository : GenericRepository<TConfiguracionDatoRemarketing>, IConfiguracionDatoRemarketingRepository
    {
        private Mapper _mapper;

        public ConfiguracionDatoRemarketingRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionDatoRemarketing, ConfiguracionDatoRemarketing>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TConfiguracionDatoRemarketing MapeoEntidad(ConfiguracionDatoRemarketing entidad)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionDatoRemarketing modelo = _mapper.Map<TConfiguracionDatoRemarketing>(entidad);

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

        public TConfiguracionDatoRemarketing Add(ConfiguracionDatoRemarketing entidad)
        {
            try
            {
                var ConfiguracionDatoRemarketing = MapeoEntidad(entidad);
                base.Insert(ConfiguracionDatoRemarketing);
                return ConfiguracionDatoRemarketing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionDatoRemarketing Update(ConfiguracionDatoRemarketing entidad)
        {
            try
            {
                var ConfiguracionDatoRemarketing = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionDatoRemarketing.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionDatoRemarketing);
                return ConfiguracionDatoRemarketing;
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


        public IEnumerable<TConfiguracionDatoRemarketing> Add(IEnumerable<ConfiguracionDatoRemarketing> listadoEntidad)
        {
            try
            {
                List<TConfiguracionDatoRemarketing> listado = new List<TConfiguracionDatoRemarketing>();
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

        public IEnumerable<TConfiguracionDatoRemarketing> Update(IEnumerable<ConfiguracionDatoRemarketing> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionDatoRemarketing> listado = new List<TConfiguracionDatoRemarketing>();
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
        /// Autor: Margiory Ramirez Neyra..
        /// Fecha: 04/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ConfiguracionDatoRemarketing para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_ConfiguracionDatoRemarketing WHERE Estado=1";
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
        /// Fecha: 04/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConfiguracionDatoRemarketing
        /// </summary>
        /// <returns> List<ConfiguracionDatoRemarketingDTO> </returns>
        public IEnumerable<ConfiguracionDatoRemarketingDTO> ObtenerConfiguracionDatoRemarketing()
        {
            try
            {
                List<ConfiguracionDatoRemarketingDTO> rpta = new List<ConfiguracionDatoRemarketingDTO>();
                var query = @"SELECT Id, Nombre, Descripcion, Prioridad, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion
                            FROM mkt.T_ConfiguracionDatoRemarketing
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConfiguracionDatoRemarketingDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 04/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion de las configuraciones guardadas
        /// </summary>
        /// <returns>Lista de objetos de clase ConfiguracionDatoRemarketingGrillaDTO</returns>
        public List<ConfiguracionDatoRemarketingGrillaDTO> ObtenerConfiguracionesDatoRemarketing()
        {
            try
            {
                List<ConfiguracionDatoRemarketingGrillaDTO> resultadoConsulta = new List<ConfiguracionDatoRemarketingGrillaDTO>();

                string consulta = "SELECT Id, IdAgendaTab, NombreAgendaTab, FechaInicio, FechaFin, Vigente, IdTipoDato, NombreTipoDato, IdTipoCategoriaOrigen, NombreTipoCategoriaOrigen, IdCategoriaOrigen, NombreCategoriaOrigen, IdProbabilidadRegistroPw, NombreProbabilidadRegistroPw FROM mkt.V_ObtenerListaConfiguracionDatoRemarketing";
                string resultadoConsultaSinProcesar = _dapperRepository.QueryDapper(consulta, null);

                if (!string.IsNullOrEmpty(resultadoConsultaSinProcesar) && !resultadoConsultaSinProcesar.Contains("[]"))
                    resultadoConsulta = JsonConvert.DeserializeObject<List<ConfiguracionDatoRemarketingGrillaDTO>>(resultadoConsultaSinProcesar);

                return resultadoConsulta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 20/08/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion de las configuraciones guardadas
        /// </summary>
        /// <returns>Lista de objetos de clase ConfiguracionDatoRemarketingGrillaDTO</returns>
        public List<ConfiguracionDatoRemarketingAgendaTabVentasDTO> ObtenerAgendaTabVentasParaConfiguracion()
        {
            try
            {
                List<ConfiguracionDatoRemarketingAgendaTabVentasDTO> resultadoConsulta = new List<ConfiguracionDatoRemarketingAgendaTabVentasDTO>();

                string consulta = "SELECT IdAgendaTab, NombreAgendaTab FROM mkt.V_ObtenerListaAgendaTabVentas";
                string resultadoConsultaSinProcesar = _dapperRepository.QueryDapper(consulta, null);

                if (!string.IsNullOrEmpty(resultadoConsultaSinProcesar) && !resultadoConsultaSinProcesar.Contains("[]"))
                    resultadoConsulta = JsonConvert.DeserializeObject<List<ConfiguracionDatoRemarketingAgendaTabVentasDTO>>(resultadoConsultaSinProcesar);

                return resultadoConsulta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 24/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id del tab de redireccion
        /// </summary>
        /// <returns>Entero con el id del tab de redireccion</returns>
        public int ObtenerTabRedireccionRemarketing(int idTipoDato, int idSubCategoriaDato, int idProbabilidadRegistroPw)
        {
            try
            {
                ValorIntDTO idAgendaTab = new ValorIntDTO();

                string spPeticion = "[com].[SP_ObtenerTabRedireccionRemarketing]";
                string resultadoPeticion = _dapperRepository.QuerySPFirstOrDefault(spPeticion, new { IdTipoDato = idTipoDato, IdSubCategoriaDato = idSubCategoriaDato, IdProbabilidadRegistroPw = idProbabilidadRegistroPw });

                if (!string.IsNullOrEmpty(resultadoPeticion) && !resultadoPeticion.Contains("[]"))
                    idAgendaTab = JsonConvert.DeserializeObject<ValorIntDTO>(resultadoPeticion);

                return idAgendaTab != null ? idAgendaTab.Valor.Value : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
