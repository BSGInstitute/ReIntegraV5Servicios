using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class ProgramaGeneralArgumentoDetalleRepository : GenericRepository<TProgramaGeneralArgumentoDetalle>, IProgramaGeneralArgumentoDetalleRepository
    {
        private Mapper _mapper;
        public ProgramaGeneralArgumentoDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralArgumentoDetalle, ProgramaGeneralArgumentoDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralArgumentoDetalle MapeoEntidad(ProgramaGeneralArgumentoDetalle entidad)
        {
            try
            {
                TProgramaGeneralArgumentoDetalle modelo = _mapper.Map<TProgramaGeneralArgumentoDetalle>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralArgumentoDetalle Add(ProgramaGeneralArgumentoDetalle entidad)
        {
            try
            {
                var ProgramaGeneralArgumentoDetalle = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralArgumentoDetalle);
                return ProgramaGeneralArgumentoDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralArgumentoDetalle Update(ProgramaGeneralArgumentoDetalle entidad)
        {
            try
            {
                var ProgramaGeneralArgumentoDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralArgumentoDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralArgumentoDetalle);
                return ProgramaGeneralArgumentoDetalle;
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

        public IEnumerable<TProgramaGeneralArgumentoDetalle> AddList(IEnumerable<ProgramaGeneralArgumentoDetalle> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralArgumentoDetalle> listado = new List<TProgramaGeneralArgumentoDetalle>();
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

        public IEnumerable<TProgramaGeneralArgumentoDetalle> UpdateList(IEnumerable<ProgramaGeneralArgumentoDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralArgumentoDetalle> listado = new List<TProgramaGeneralArgumentoDetalle>();
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
