using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
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
    public class DatoContratoComisionBonoRepository :  GenericRepository<TDatoContratoComisionBono>, IDatoContratoComisionBonoRepository
    {
        private Mapper _mapper;
        public DatoContratoComisionBonoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDatoContratoComisionBono, DatoContratoComisionBono>(MemberList.None).ReverseMap();
                cfg.CreateMap<DatoContratoComisionBono, DatoContratoComisionBonoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TDatoContratoComisionBono, DatoContratoComisionBonoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TDatoContratoComisionBono MapeoEntidad(DatoContratoComisionBono entidad)
        {
            try
            {
                TDatoContratoComisionBono modelo = _mapper.Map<TDatoContratoComisionBono>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TDatoContratoComisionBono Add(DatoContratoComisionBono entidad)
        {
            try
            {
                var DatoContratoComisionBono = MapeoEntidad(entidad);
                base.Insert(DatoContratoComisionBono);
                return DatoContratoComisionBono;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TDatoContratoComisionBono Update(DatoContratoComisionBono entidad)
        {
            try
            {
                var DatoContratoComisionBono = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DatoContratoComisionBono.RowVersion = entidadExistente.RowVersion;

                base.Update(DatoContratoComisionBono);
                return DatoContratoComisionBono;
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
        public IEnumerable<TDatoContratoComisionBono> Add(IEnumerable<DatoContratoComisionBono> listadoEntidad)
        {
            try
            {
                List<TDatoContratoComisionBono> listado = new List<TDatoContratoComisionBono>();
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
        public IEnumerable<TDatoContratoComisionBono> Update(IEnumerable<DatoContratoComisionBono> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDatoContratoComisionBono> listado = new List<TDatoContratoComisionBono>();
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
