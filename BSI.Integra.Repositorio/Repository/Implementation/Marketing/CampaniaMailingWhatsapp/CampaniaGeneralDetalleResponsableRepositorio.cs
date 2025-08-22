using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp;
using Google.Api.Ads.AdWords.v201809;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.CampaniaMailingWhatsapp
{
    public class CampaniaGeneralDetalleResponsableRepositorio : GenericRepository<TCampaniaGeneralDetalleResponsable>, ICampaniaGeneralDetalleResponsableRepositorio
    {
        private Mapper _mapper;

        public CampaniaGeneralDetalleResponsableRepositorio(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCampaniaGeneralDetalleResponsable, CampaniaGeneralDetalleResponsableDTO>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TCampaniaGeneralDetalleResponsable MapeoEntidad(CampaniaGeneralDetalleResponsableDTO entidad)
        {
            try
            {
                TCampaniaGeneralDetalleResponsable modelo = _mapper.Map<TCampaniaGeneralDetalleResponsable>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCampaniaGeneralDetalleResponsable Add(CampaniaGeneralDetalleResponsableDTO entidad)
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
        public TCampaniaGeneralDetalleResponsable Add(TCampaniaGeneralDetalleResponsable entidad)
        {
            try
            {
                base.Insert(entidad);
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCampaniaGeneralDetalleResponsable Update(CampaniaGeneralDetalleResponsableDTO entidad)
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
        public TCampaniaGeneralDetalleResponsable UpdateByEntity(TCampaniaGeneralDetalleResponsable entidad)
        {
            try
            {
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                entidad.RowVersion = entidadExistente.RowVersion;

                base.Update(entidad);
                return entidad;
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


        public IEnumerable<TCampaniaGeneralDetalleResponsable> Add(IEnumerable<CampaniaGeneralDetalleResponsableDTO> listadoEntidad)
        {
            try
            {
                List<TCampaniaGeneralDetalleResponsable> listado = new List<TCampaniaGeneralDetalleResponsable>();
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
        public IEnumerable<TCampaniaGeneralDetalleResponsable> Add(IEnumerable<TCampaniaGeneralDetalleResponsable> listadoEntidad)
        {
            try
            {

                base.Insert(listadoEntidad);
                return listadoEntidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TCampaniaGeneralDetalleResponsable> Update(IEnumerable<CampaniaGeneralDetalleResponsableDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCampaniaGeneralDetalleResponsable> listado = new List<TCampaniaGeneralDetalleResponsable>();
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
        public IEnumerable<TCampaniaGeneralDetalleResponsable> UpdateByEntity(IEnumerable<TCampaniaGeneralDetalleResponsable> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCampaniaGeneralDetalleResponsable> listado = new List<TCampaniaGeneralDetalleResponsable>();

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
        public bool ExistFunction(int? item, int id)
        {
            if (item == null) return false;
            return base.Exist(x => x.Id == item && x.IdCampaniaGeneralDetalle == id);

        }
        public TCampaniaGeneralDetalleResponsable FirstById(int id)
        {
            try
            {
                TCampaniaGeneralDetalleResponsable entidad = base.FirstById(id);

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TCampaniaGeneralDetalleResponsable FirstBy(int Id ,int detalleId)
        {
            try
            {
                TCampaniaGeneralDetalleResponsable entidad = base.FirstBy(x => x.Id == Id && x.IdCampaniaGeneralDetalle == detalleId);
                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
