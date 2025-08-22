using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoDatoRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class ConfiguracionDatoRemarketingTipoDatoRepository : GenericRepository<TConfiguracionDatoRemarketingTipoDato>, IConfiguracionDatoRemarketingTipoDatoRepository
    {
        private Mapper _mapper;

        public ConfiguracionDatoRemarketingTipoDatoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionDatoRemarketingTipoDato, ConfiguracionDatoRemarketingTipoDatoRepository>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TConfiguracionDatoRemarketingTipoDato MapeoEntidad(ConfiguracionDatoRemarketingTipoDato entidad)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionDatoRemarketingTipoDato modelo = _mapper.Map<TConfiguracionDatoRemarketingTipoDato>(entidad);

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

        public TConfiguracionDatoRemarketingTipoDato Add(ConfiguracionDatoRemarketingTipoDato entidad)
        {
            try
            {
                var ConfiguracionDatoRemarketingTipoDato = MapeoEntidad(entidad);
                base.Insert(ConfiguracionDatoRemarketingTipoDato);
                return ConfiguracionDatoRemarketingTipoDato;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionDatoRemarketingTipoDato Update(ConfiguracionDatoRemarketingTipoDato entidad)
        {
            try
            {
                var ConfiguracionDatoRemarketingTipoDato = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionDatoRemarketingTipoDato.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionDatoRemarketingTipoDato);
                return ConfiguracionDatoRemarketingTipoDato;
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


        public IEnumerable<TConfiguracionDatoRemarketingTipoDato> Add(IEnumerable<ConfiguracionDatoRemarketingTipoDato> listadoEntidad)
        {
            try
            {
                List<TConfiguracionDatoRemarketingTipoDato> listado = new List<TConfiguracionDatoRemarketingTipoDato>();
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

        public IEnumerable<TConfiguracionDatoRemarketingTipoDato> Update(IEnumerable<ConfiguracionDatoRemarketingTipoDato> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionDatoRemarketingTipoDato> listado = new List<TConfiguracionDatoRemarketingTipoDato>();
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
        /// Obtiene registros de T_TipoDato para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_ConfiguracionDatoRemarketingTipoDato WHERE Estado=1";
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
