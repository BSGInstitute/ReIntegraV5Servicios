using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CertificadoSolicitudRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 06/01/2023
    /// <summary>
    /// Gestión general de T_CertificadoSolicitud
    /// </summary>
    public class CertificadoSolicitudRepository : GenericRepository<TCertificadoSolicitud>, ICertificadoSolicitudRepository
    {
        private Mapper _mapper;

        public CertificadoSolicitudRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCertificadoSolicitud, CertificadoSolicitud>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        } 
        #region Metodos Base
        private TCertificadoSolicitud MapeoEntidad(CertificadoSolicitud entidad)
        {
            try
            {
                //crea la entidad padre
                TCertificadoSolicitud modelo = _mapper.Map<TCertificadoSolicitud>(entidad); 
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public TCertificadoSolicitud Add(CertificadoSolicitud entidad)
        {
            try
            {
                var CertificadoSolicitud = MapeoEntidad(entidad);
                base.Insert(CertificadoSolicitud);
                return CertificadoSolicitud;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCertificadoSolicitud Update(CertificadoSolicitud entidad)
        {
            try
            {
                var CertificadoSolicitud = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CertificadoSolicitud.RowVersion = entidadExistente.RowVersion;

                base.Update(CertificadoSolicitud);
                return CertificadoSolicitud;
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


        public IEnumerable<TCertificadoSolicitud> Add(IEnumerable<CertificadoSolicitud> listadoEntidad)
        {
            try
            {
                List<TCertificadoSolicitud> listado = new List<TCertificadoSolicitud>();
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

        public IEnumerable<TCertificadoSolicitud> Update(IEnumerable<CertificadoSolicitud> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCertificadoSolicitud> listado = new List<TCertificadoSolicitud>();
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
