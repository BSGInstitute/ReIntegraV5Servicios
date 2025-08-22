using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConfiguracionDatoRemarketingCategoriaOrigenRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionDatoRemarketingCategoriaOrigen
    /// </summary>
    public class ConfiguracionDatoRemarketingCategoriaOrigenRepository : GenericRepository<TConfiguracionDatoRemarketingCategoriaOrigen>, IConfiguracionDatoRemarketingCategoriaOrigenRepository
    {
        private Mapper _mapper;

        public ConfiguracionDatoRemarketingCategoriaOrigenRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionDatoRemarketingCategoriaOrigen, ConfiguracionDatoRemarketingCategoriaOrigen>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TConfiguracionDatoRemarketingCategoriaOrigen MapeoEntidad(ConfiguracionDatoRemarketingCategoriaOrigen entidad)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionDatoRemarketingCategoriaOrigen modelo = _mapper.Map<TConfiguracionDatoRemarketingCategoriaOrigen>(entidad);

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

        public TConfiguracionDatoRemarketingCategoriaOrigen Add(ConfiguracionDatoRemarketingCategoriaOrigen entidad)
        {
            try
            {
                var ConfiguracionDatoRemarketingCategoriaOrigen = MapeoEntidad(entidad);
                base.Insert(ConfiguracionDatoRemarketingCategoriaOrigen);
                return ConfiguracionDatoRemarketingCategoriaOrigen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionDatoRemarketingCategoriaOrigen Update(ConfiguracionDatoRemarketingCategoriaOrigen entidad)
        {
            try
            {
                var ConfiguracionDatoRemarketingCategoriaOrigen = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionDatoRemarketingCategoriaOrigen.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionDatoRemarketingCategoriaOrigen);
                return ConfiguracionDatoRemarketingCategoriaOrigen;
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


        public IEnumerable<TConfiguracionDatoRemarketingCategoriaOrigen> Add(IEnumerable<ConfiguracionDatoRemarketingCategoriaOrigen> listadoEntidad)
        {
            try
            {
                List<TConfiguracionDatoRemarketingCategoriaOrigen> listado = new List<TConfiguracionDatoRemarketingCategoriaOrigen>();
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

        public IEnumerable<TConfiguracionDatoRemarketingCategoriaOrigen> Update(IEnumerable<ConfiguracionDatoRemarketingCategoriaOrigen> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionDatoRemarketingCategoriaOrigen> listado = new List<TConfiguracionDatoRemarketingCategoriaOrigen>();
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
        /// Obtiene registros de T_ConfiguracionDatoRemarketingCategoriaOrigen para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_ConfiguracionDatoRemarketingCategoriaOrigen WHERE Estado=1";
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

        
    }
}
