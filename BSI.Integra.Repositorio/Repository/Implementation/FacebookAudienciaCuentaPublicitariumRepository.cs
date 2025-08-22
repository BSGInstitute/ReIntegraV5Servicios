using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FacebookAudienciaCuentaPublicitariumRepository
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenSector
    /// </summary>
    public class FacebookAudienciaCuentaPublicitariumRepository : GenericRepository<TFacebookAudienciaCuentaPublicitarium>, IFacebookAudienciaCuentaPublicitariumRepository
    {
        private Mapper _mapper;

        public FacebookAudienciaCuentaPublicitariumRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFacebookAudienciaCuentaPublicitarium, FacebookAudienciaCuentaPublicitarium>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TFacebookAudienciaCuentaPublicitarium MapeoEntidad(FacebookAudienciaCuentaPublicitarium entidad)
        {
            try
            {
                //crea la entidad padre
                TFacebookAudienciaCuentaPublicitarium modelo = _mapper.Map<TFacebookAudienciaCuentaPublicitarium>(entidad);

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

        public TFacebookAudienciaCuentaPublicitarium Add(FacebookAudienciaCuentaPublicitarium entidad)
        {
            try
            {
                var FacebookAudienciaCuentaPublicitarium = MapeoEntidad(entidad);
                base.Insert(FacebookAudienciaCuentaPublicitarium);
                return FacebookAudienciaCuentaPublicitarium;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFacebookAudienciaCuentaPublicitarium Update(FacebookAudienciaCuentaPublicitarium entidad)
        {
            try
            {
                var FacebookAudienciaCuentaPublicitarium = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FacebookAudienciaCuentaPublicitarium.RowVersion = entidadExistente.RowVersion;

                base.Update(FacebookAudienciaCuentaPublicitarium);
                return FacebookAudienciaCuentaPublicitarium;
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


        public IEnumerable<TFacebookAudienciaCuentaPublicitarium> Add(IEnumerable<FacebookAudienciaCuentaPublicitarium> listadoEntidad)
        {
            try
            {
                List<TFacebookAudienciaCuentaPublicitarium> listado = new List<TFacebookAudienciaCuentaPublicitarium>();
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

        public IEnumerable<TFacebookAudienciaCuentaPublicitarium> Update(IEnumerable<FacebookAudienciaCuentaPublicitarium> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFacebookAudienciaCuentaPublicitarium> listado = new List<TFacebookAudienciaCuentaPublicitarium>();
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

       

    }
}




