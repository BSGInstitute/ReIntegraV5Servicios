using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConfiguracionPeriodoMatriculaRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionPeriodoMatricula
    /// </summary>
    public class ConfiguracionPeriodoMatriculaRepository : GenericRepository<TConfiguracionPeriodoMatricula>, IConfiguracionPeriodoMatriculaRepository
    {
        private Mapper _mapper;

        public ConfiguracionPeriodoMatriculaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionPeriodoMatricula, ConfiguracionPeriodoMatricula>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TConfiguracionPeriodoMatricula MapeoEntidad(ConfiguracionPeriodoMatricula entidad)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionPeriodoMatricula modelo = _mapper.Map<TConfiguracionPeriodoMatricula>(entidad);

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

        public TConfiguracionPeriodoMatricula Add(ConfiguracionPeriodoMatricula entidad)
        {
            try
            {
                var ConfiguracionPeriodoMatricula = MapeoEntidad(entidad);
                base.Insert(ConfiguracionPeriodoMatricula);
                return ConfiguracionPeriodoMatricula;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionPeriodoMatricula Update(ConfiguracionPeriodoMatricula entidad)
        {
            try
            {
                var ConfiguracionPeriodoMatricula = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionPeriodoMatricula.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionPeriodoMatricula);
                return ConfiguracionPeriodoMatricula;
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


        public IEnumerable<TConfiguracionPeriodoMatricula> Add(IEnumerable<ConfiguracionPeriodoMatricula> listadoEntidad)
        {
            try
            {
                List<TConfiguracionPeriodoMatricula> listado = new List<TConfiguracionPeriodoMatricula>();
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

        public IEnumerable<TConfiguracionPeriodoMatricula> Update(IEnumerable<ConfiguracionPeriodoMatricula> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionPeriodoMatricula> listado = new List<TConfiguracionPeriodoMatricula>();
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
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConfiguracionPeriodoMatricula.
        /// </summary>
        /// <returns> List<ConfiguracionPeriodoMatriculaDTO> </returns>
        public IEnumerable<ConfiguracionPeriodoMatriculaRecibidoDTO> ObtenerConfiguracionPeriodoMatricula()
        {
            try
            {
                List<ConfiguracionPeriodoMatriculaRecibidoDTO> rpta = new List<ConfiguracionPeriodoMatriculaRecibidoDTO>();
                var query = @"
                    SELECT [Id]
                          ,[Nombre]
                          ,[FechaInicio]
                          ,[FechaFin]
                    FROM [fin].[T_ConfiguracionPeriodoMatricula]
                    WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConfiguracionPeriodoMatriculaRecibidoDTO>>(resultado);
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
