using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class RecuperacionAutomaticoModuloSistemaRepository : GenericRepository<TRecuperacionAutomaticoModuloSistema>, IRecuperacionAutomaticoModuloSistemaRepository
    {
        private Mapper _mapper;
        public RecuperacionAutomaticoModuloSistemaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRecuperacionAutomaticoModuloSistema, RecuperacionAutomaticoModuloSistemaDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TRecuperacionAutomaticoModuloSistema MapeoEntidad(RecuperacionAutomaticoModuloSistemaDTO entidad)
        {
            try
            {
                TRecuperacionAutomaticoModuloSistema modelo = _mapper.Map<TRecuperacionAutomaticoModuloSistema>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRecuperacionAutomaticoModuloSistema Add(RecuperacionAutomaticoModuloSistemaDTO entidad)
        {
            try
            {
                var PgeneralConfiguracionPlantilla = MapeoEntidad(entidad);
                base.Insert(PgeneralConfiguracionPlantilla);
                return PgeneralConfiguracionPlantilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRecuperacionAutomaticoModuloSistema Update(RecuperacionAutomaticoModuloSistemaDTO entidad)
        {
            try
            {
                var PgeneralConfiguracionPlantilla = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralConfiguracionPlantilla.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralConfiguracionPlantilla);
                return PgeneralConfiguracionPlantilla;
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


        public IEnumerable<TRecuperacionAutomaticoModuloSistema> Add(IEnumerable<RecuperacionAutomaticoModuloSistemaDTO> listadoEntidad)
        {
            try
            {
                List<TRecuperacionAutomaticoModuloSistema> listado = new List<TRecuperacionAutomaticoModuloSistema>();
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

        public IEnumerable<TRecuperacionAutomaticoModuloSistema> Update(IEnumerable<RecuperacionAutomaticoModuloSistemaDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRecuperacionAutomaticoModuloSistema> listado = new List<TRecuperacionAutomaticoModuloSistema>();
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
        public IEnumerable<TRecuperacionAutomaticoModuloSistema> GetBy(Expression<Func<TRecuperacionAutomaticoModuloSistema, bool>> filter)
        {
            try
            {
                return base.GetBy(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TRecuperacionAutomaticoModuloSistema FirstBy(Expression<Func<TRecuperacionAutomaticoModuloSistema, bool>> filter)
        {
            try
            {
                return base.FirstBy(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
