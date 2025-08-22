using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConfiguracionDatoRemarketingTipoCategoriaOrigenRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionDatoRemarketingTipoCategoriaOrigen
    /// </summary>
    public class ConfiguracionDatoRemarketingTipoCategoriaOrigenRepository : GenericRepository<TConfiguracionDatoRemarketingTipoCategoriaOrigen>, IConfiguracionDatoRemarketingTipoCategoriaOrigenRepository
    {
        private Mapper _mapper;

        public ConfiguracionDatoRemarketingTipoCategoriaOrigenRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionDatoRemarketingTipoCategoriaOrigen, ConfiguracionDatoRemarketingTipoCategoriaOrigen>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TConfiguracionDatoRemarketingTipoCategoriaOrigen MapeoEntidad(ConfiguracionDatoRemarketingTipoCategoriaOrigen entidad)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionDatoRemarketingTipoCategoriaOrigen modelo = _mapper.Map<TConfiguracionDatoRemarketingTipoCategoriaOrigen>(entidad);

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

        public TConfiguracionDatoRemarketingTipoCategoriaOrigen Add(ConfiguracionDatoRemarketingTipoCategoriaOrigen entidad)
        {
            try
            {
                var ConfiguracionDatoRemarketingTipoCategoriaOrigen = MapeoEntidad(entidad);
                base.Insert(ConfiguracionDatoRemarketingTipoCategoriaOrigen);
                return ConfiguracionDatoRemarketingTipoCategoriaOrigen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionDatoRemarketingTipoCategoriaOrigen Update(ConfiguracionDatoRemarketingTipoCategoriaOrigen entidad)
        {
            try
            {
                var ConfiguracionDatoRemarketingTipoCategoriaOrigen = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionDatoRemarketingTipoCategoriaOrigen.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionDatoRemarketingTipoCategoriaOrigen);
                return ConfiguracionDatoRemarketingTipoCategoriaOrigen;
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


        public IEnumerable<TConfiguracionDatoRemarketingTipoCategoriaOrigen> Add(IEnumerable<ConfiguracionDatoRemarketingTipoCategoriaOrigen> listadoEntidad)
        {
            try
            {
                List<TConfiguracionDatoRemarketingTipoCategoriaOrigen> listado = new List<TConfiguracionDatoRemarketingTipoCategoriaOrigen>();
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

        public IEnumerable<TConfiguracionDatoRemarketingTipoCategoriaOrigen> Update(IEnumerable<ConfiguracionDatoRemarketingTipoCategoriaOrigen> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionDatoRemarketingTipoCategoriaOrigen> listado = new List<TConfiguracionDatoRemarketingTipoCategoriaOrigen>();
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
        /// Obtiene registros de T_ConfiguracionDatoRemarketingTipoCategoriaOrigen para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_ConfiguracionDatoRemarketingTipoCategoriaOrigen WHERE Estado=1";
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
