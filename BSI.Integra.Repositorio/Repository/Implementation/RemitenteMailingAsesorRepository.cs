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
    /// Repositorio: RemitenteMailingRepository
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 09/11/2022
    /// <summary>
    /// Gestión general de T_RemitenteMailing
    /// </summary>
    public class RemitenteMailingAsesorRepository : GenericRepository<TRemitenteMailingAsesor>, IRemitenteMailingAsesorRepository
    {
        private Mapper _mapper;

        public RemitenteMailingAsesorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRemitenteMailingAsesor, RemitenteMailingAsesor>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TRemitenteMailingAsesor MapeoEntidad(RemitenteMailingAsesor entidad)
        {
            try
            {
                //crea la entidad padre
                TRemitenteMailingAsesor modelo = _mapper.Map<TRemitenteMailingAsesor>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRemitenteMailingAsesor Add(RemitenteMailingAsesor entidad)
        {
            try
            {
                var RemitenteMailingAsesor = MapeoEntidad(entidad);
                base.Insert(RemitenteMailingAsesor);
                return RemitenteMailingAsesor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRemitenteMailingAsesor Update(RemitenteMailingAsesor entidad)
        {
            try
            {
                var RemitenteMailingAsesor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RemitenteMailingAsesor.RowVersion = entidadExistente.RowVersion;

                base.Update(RemitenteMailingAsesor);
                return RemitenteMailingAsesor;
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


        public IEnumerable<TRemitenteMailingAsesor> Add(IEnumerable<RemitenteMailingAsesor> listadoEntidad)
        {
            try
            {
                List<TRemitenteMailingAsesor> listado = new List<TRemitenteMailingAsesor>();
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

        public IEnumerable<TRemitenteMailingAsesor> Update(IEnumerable<RemitenteMailingAsesor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRemitenteMailingAsesor> listado = new List<TRemitenteMailingAsesor>();
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
