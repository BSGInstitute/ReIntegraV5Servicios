using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FacebookFormularioLeadgenLogRepository
    /// Autor: Max Mantilla Rodriguez.
    /// Fecha: 09/10/2025
    /// <summary>
    /// Gestión general de T_FacebookFormularioLeadgenLog
    /// </summary>
    public class FacebookFormularioLeadgenLogRepository : GenericRepository<TFacebookFormularioLeadgenLog>, IFacebookFormularioLeadgenLogRepository
    {
        private Mapper _mapper;

        public FacebookFormularioLeadgenLogRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFacebookFormularioLeadgenLog, FacebookFormularioLeadgenLog>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TFacebookFormularioLeadgenLog MapeoEntidad(FacebookFormularioLeadgenLog entidad)
        {
            try
            {
                //crea la entidad padre
                TFacebookFormularioLeadgenLog modelo = _mapper.Map<TFacebookFormularioLeadgenLog>(entidad);

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

        public TFacebookFormularioLeadgenLog Add(FacebookFormularioLeadgenLog entidad)
        {
            try
            {
                var FacebookFormularioLeadgenLog = MapeoEntidad(entidad);
                base.Insert(FacebookFormularioLeadgenLog);
                return FacebookFormularioLeadgenLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFacebookFormularioLeadgenLog Update(FacebookFormularioLeadgenLog entidad)
        {
            try
            {
                var FacebookFormularioLeadgenLog = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FacebookFormularioLeadgenLog.RowVersion = entidadExistente.RowVersion;

                base.Update(FacebookFormularioLeadgenLog);
                return FacebookFormularioLeadgenLog;
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


        public IEnumerable<TFacebookFormularioLeadgenLog> Add(IEnumerable<FacebookFormularioLeadgenLog> listadoEntidad)
        {
            try
            {
                List<TFacebookFormularioLeadgenLog> listado = new List<TFacebookFormularioLeadgenLog>();
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

        public IEnumerable<TFacebookFormularioLeadgenLog> Update(IEnumerable<FacebookFormularioLeadgenLog> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFacebookFormularioLeadgenLog> listado = new List<TFacebookFormularioLeadgenLog>();
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
