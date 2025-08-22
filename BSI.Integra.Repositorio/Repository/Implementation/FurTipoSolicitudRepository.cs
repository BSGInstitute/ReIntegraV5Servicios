using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueCarpetaDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class FurTipoSolicitudRepository : GenericRepository<TFurTipoSolicitud>, IFurTipoSolicitudRepository
    {
        private Mapper _mapper;
        public FurTipoSolicitudRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFurTipoSolicitud, TFurTipoSolicitudDTO>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFurTipoSolicitud MapeoEntidad(TFurTipoSolicitudDTO entidad)
        {
            try
            {
                TFurTipoSolicitud modelo = _mapper.Map<TFurTipoSolicitud>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFurTipoSolicitud Add(TFurTipoSolicitudDTO entidad)
        {
            try
            {
                entidad.Id = 0;
                var LandingPage = MapeoEntidad(entidad);
                base.Insert(LandingPage);
                return LandingPage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFurTipoSolicitud Update(TFurTipoSolicitudDTO entidad)
        {
            try
            {
                var entidadOriginal = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                entidadOriginal.RowVersion = entidadExistente.RowVersion;
                base.Update(entidadOriginal);
                return entidadOriginal;
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


        public IEnumerable<TFurTipoSolicitud> Add(IEnumerable<TFurTipoSolicitudDTO> listadoEntidad)
        {
            try
            {
                List<TFurTipoSolicitud> listado = new List<TFurTipoSolicitud>();
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

        public IEnumerable<TFurTipoSolicitud> Update(IEnumerable<TFurTipoSolicitudDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFurTipoSolicitud> listado = new List<TFurTipoSolicitud>();
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

                public TFurTipoSolicitud ObtenerPorId(int id)
        {
            try
            {
                return base.FirstById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TFurTipoSolicitud> ObtenerTodos()
        {
            try
            {
                return base.GetBy(x => x.Estado == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TFurTipoSolicitud> ObtenerPorTexto(string texto)
        {
            try
            {
                return base.GetBy(x => x.Nombre.Contains(texto)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
