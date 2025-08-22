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
    /// Repositorio: FacebookCuentaPublicitariaRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_FacebookCuentaPublicitaria
    /// </summary>
    public class FacebookCuentaPublicitariaRepository : GenericRepository<TFacebookCuentaPublicitarium>, IFacebookCuentaPublicitariaRepository
    {
        private Mapper _mapper;

        public FacebookCuentaPublicitariaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFacebookCuentaPublicitarium, FacebookCuentaPublicitarium>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TFacebookCuentaPublicitarium MapeoEntidad(FacebookCuentaPublicitarium entidad)
        {
            try
            {
                //crea la entidad padre
                TFacebookCuentaPublicitarium modelo = _mapper.Map<TFacebookCuentaPublicitarium>(entidad);

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

        public TFacebookCuentaPublicitarium Add(FacebookCuentaPublicitarium entidad)
        {
            try
            {
                var FacebookCuentaPublicitaria = MapeoEntidad(entidad);
                base.Insert(FacebookCuentaPublicitaria);
                return FacebookCuentaPublicitaria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFacebookCuentaPublicitarium Update(FacebookCuentaPublicitarium entidad)
        {
            try
            {
                var FacebookCuentaPublicitaria = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FacebookCuentaPublicitaria.RowVersion = entidadExistente.RowVersion;

                base.Update(FacebookCuentaPublicitaria);
                return FacebookCuentaPublicitaria;
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


        public IEnumerable<TFacebookCuentaPublicitarium> Add(IEnumerable<FacebookCuentaPublicitarium> listadoEntidad)
        {
            try
            {
                List<TFacebookCuentaPublicitarium> listado = new List<TFacebookCuentaPublicitarium>();
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

        public IEnumerable<TFacebookCuentaPublicitarium> Update(IEnumerable<FacebookCuentaPublicitarium> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFacebookCuentaPublicitarium> listado = new List<TFacebookCuentaPublicitarium>();
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

        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                var lista = new List<ComboDTO>();
                string _query = @"SELECT Id,Nombre FROM mkt.T_FacebookCuentaPublicitaria Where Estado=1";
                var queryRespuesta = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ComboDTO>>(queryRespuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los registros para combos.
        /// </summary>
        /// <returns></returns>
        public List<FacebookCuentaPublicitariaDTO> ObtenerComboFacebookCuentaPublicitaria()
        {
            try
            {
                List<FacebookCuentaPublicitariaDTO> facebookCuentaPublicitariaDTOs = new List<FacebookCuentaPublicitariaDTO>();
                facebookCuentaPublicitariaDTOs = GetBy(x => true, y => new FacebookCuentaPublicitariaDTO
                {
                    Id = y.Id,
                    FacebookIdCuentaPublicitaria = y.FacebookIdCuentaPublicitaria,
                    Nombre = y.Nombre
                }).ToList();
                return facebookCuentaPublicitariaDTOs;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
