using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AdworkCredencialApiRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_AdworkCredencialApi
    /// </summary>
    public class AdworkCredencialApiRepository : GenericRepository<TAdworkCredencialApi>, IAdworkCredencialApiRepository
    {
        private Mapper _mapper;

        public AdworkCredencialApiRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAdworkCredencialApi, AdworkCredencialApi>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TAdworkCredencialApi MapeoEntidad(AdworkCredencialApi entidad)
        {
            try
            {
                //crea la entidad padre
                TAdworkCredencialApi modelo = _mapper.Map<TAdworkCredencialApi>(entidad);

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

        public TAdworkCredencialApi Add(AdworkCredencialApi entidad)
        {
            try
            {
                var AdworkCredencialApi = MapeoEntidad(entidad);
                base.Insert(AdworkCredencialApi);
                return AdworkCredencialApi;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAdworkCredencialApi Update(AdworkCredencialApi entidad)
        {
            try
            {
                var AdworkCredencialApi = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AdworkCredencialApi.RowVersion = entidadExistente.RowVersion;

                base.Update(AdworkCredencialApi);
                return AdworkCredencialApi;
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


        public IEnumerable<TAdworkCredencialApi> Add(IEnumerable<AdworkCredencialApi> listadoEntidad)
        {
            try
            {
                List<TAdworkCredencialApi> listado = new List<TAdworkCredencialApi>();
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

        public IEnumerable<TAdworkCredencialApi> Update(IEnumerable<AdworkCredencialApi> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAdworkCredencialApi> listado = new List<TAdworkCredencialApi>();
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



        public AdworkCredencialApiDTO ObtenerCredencial()
        {
            try
            {
                AdworkCredencialApiDTO rpta = new AdworkCredencialApiDTO();
                var query = @"SELECT Id, DeveloperToken, ClientCustomerId,Oauth2ClientId,Oauth2ClientSecret,Oauth2RefreshToken, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion
                            FROM mkt.T_AdworkCredencialApi
                            WHERE Estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<AdworkCredencialApiDTO>(resultado);
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
