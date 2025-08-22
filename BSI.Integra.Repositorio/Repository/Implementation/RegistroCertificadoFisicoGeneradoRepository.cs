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
    public class RegistroCertificadoFisicoGeneradoRepository : GenericRepository<TRegistroCertificadoFisicoGenerado>, IRegistroCertificadoFisicoGeneradoRepository
    {
        private Mapper _mapper;

        public RegistroCertificadoFisicoGeneradoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRegistroCertificadoFisicoGenerado, RegistroCertificadoFisicoGenerado>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TRegistroCertificadoFisicoGenerado MapeoEntidad(RegistroCertificadoFisicoGenerado entidad)
        {
            try
            {
                TRegistroCertificadoFisicoGenerado modelo = _mapper.Map<TRegistroCertificadoFisicoGenerado>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRegistroCertificadoFisicoGenerado Add(RegistroCertificadoFisicoGenerado entidad)
        {
            try
            {
                var registroCertificadoFisicoGenerado = MapeoEntidad(entidad);
                base.Insert(registroCertificadoFisicoGenerado);
                return registroCertificadoFisicoGenerado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRegistroCertificadoFisicoGenerado Update(RegistroCertificadoFisicoGenerado entidad)
        {
            try
            {
                var RegistroCertificadoFisicoGenerado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RegistroCertificadoFisicoGenerado.RowVersion = entidadExistente.RowVersion;

                base.Update(RegistroCertificadoFisicoGenerado);
                return RegistroCertificadoFisicoGenerado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRegistroCertificadoFisicoGenerado UpdateAlterno(RegistroCertificadoFisicoGenerado entidad)
        {
            try
            {
                var RegistroCertificadoFisicoGenerado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RegistroCertificadoFisicoGenerado.RowVersion = entidadExistente.RowVersion;

                base.UpdateAlterno(RegistroCertificadoFisicoGenerado);
                return RegistroCertificadoFisicoGenerado;
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


        public IEnumerable<TRegistroCertificadoFisicoGenerado> Add(IEnumerable<RegistroCertificadoFisicoGenerado> listadoEntidad)
        {
            try
            {
                List<TRegistroCertificadoFisicoGenerado> listado = new List<TRegistroCertificadoFisicoGenerado>();
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

        public IEnumerable<TRegistroCertificadoFisicoGenerado> Update(IEnumerable<RegistroCertificadoFisicoGenerado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRegistroCertificadoFisicoGenerado> listado = new List<TRegistroCertificadoFisicoGenerado>();
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
