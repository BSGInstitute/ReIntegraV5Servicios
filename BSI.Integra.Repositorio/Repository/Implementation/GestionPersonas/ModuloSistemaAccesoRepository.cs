using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class ModuloSistemaAccesoRepository : GenericRepository<TModuloSistemaAccesoV5>, IModuloSistemaAccesoRepository
    {
        private Mapper _mapper;
        public ModuloSistemaAccesoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModuloSistemaAccesoV5, ModuloSistemaAccesoV5>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TModuloSistemaAccesoV5 MapeoEntidad(ModuloSistemaAccesoV5 entidad)
        {
            try
            {
                TModuloSistemaAccesoV5 modelo = _mapper.Map<TModuloSistemaAccesoV5>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TModuloSistemaAccesoV5 Add(ModuloSistemaAccesoV5 entidad)
        {
            try
            {
                var ModuloSistemaAccesoV5 = MapeoEntidad(entidad);
                base.Insert(ModuloSistemaAccesoV5);
                return ModuloSistemaAccesoV5;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TModuloSistemaAccesoV5 Update(ModuloSistemaAccesoV5 entidad)
        {
            try
            {
                var ModuloSistemaAccesoV5 = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ModuloSistemaAccesoV5.RowVersion = entidadExistente.RowVersion;

                base.Update(ModuloSistemaAccesoV5);
                return ModuloSistemaAccesoV5;
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
        public IEnumerable<TModuloSistemaAccesoV5> Add(IEnumerable<ModuloSistemaAccesoV5> listadoEntidad)
        {
            try
            {
                List<TModuloSistemaAccesoV5> listado = new List<TModuloSistemaAccesoV5>();
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
        public IEnumerable<TModuloSistemaAccesoV5> Update(IEnumerable<ModuloSistemaAccesoV5> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TModuloSistemaAccesoV5> listado = new List<TModuloSistemaAccesoV5>();
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
