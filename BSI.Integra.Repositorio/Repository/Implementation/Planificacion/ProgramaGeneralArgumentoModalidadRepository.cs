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
    public class ProgramaGeneralArgumentoModalidadRepository : GenericRepository<TProgramaGeneralArgumentoModalidad>, IProgramaGeneralArgumentoModalidadRepository
    {
        private Mapper _mapper;
        public ProgramaGeneralArgumentoModalidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralArgumentoModalidad, ProgramaGeneralArgumentoModalidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralArgumentoModalidad MapeoEntidad(ProgramaGeneralArgumentoModalidad entidad)
        {
            try
            {
                TProgramaGeneralArgumentoModalidad modelo = _mapper.Map<TProgramaGeneralArgumentoModalidad>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralArgumentoModalidad Add(ProgramaGeneralArgumentoModalidad entidad)
        {
            try
            {
                var ProgramaGeneralArgumentoModalidad = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralArgumentoModalidad);
                return ProgramaGeneralArgumentoModalidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralArgumentoModalidad Update(ProgramaGeneralArgumentoModalidad entidad)
        {
            try
            {
                var ProgramaGeneralArgumentoModalidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralArgumentoModalidad.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralArgumentoModalidad);
                return ProgramaGeneralArgumentoModalidad;
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

        public IEnumerable<TProgramaGeneralArgumentoModalidad> AddList(IEnumerable<ProgramaGeneralArgumentoModalidad> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralArgumentoModalidad> listado = new List<TProgramaGeneralArgumentoModalidad>();
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

        public IEnumerable<TProgramaGeneralArgumentoModalidad> Update(IEnumerable<ProgramaGeneralArgumentoModalidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralArgumentoModalidad> listado = new List<TProgramaGeneralArgumentoModalidad>();
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
