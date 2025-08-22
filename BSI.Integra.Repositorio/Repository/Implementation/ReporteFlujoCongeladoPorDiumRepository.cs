using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReporteFlujoCongeladoPorDiumRepository
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_ReporteFlujoCongeladoPorDium
    /// </summary>
    public class ReporteFlujoCongeladoPorDiumRepository : GenericRepository<TReporteFlujoCongeladoPorDium>, IReporteFlujoCongeladoPorDiumRepository
    {
        private Mapper _mapper;

        public ReporteFlujoCongeladoPorDiumRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TReporteFlujoCongeladoPorDium, ReporteFlujoCongeladoPorDium>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TReporteFlujoCongeladoPorDium MapeoEntidad(ReporteFlujoCongeladoPorDium entidad)
        {
            try
            {
                //crea la entidad padre
                TReporteFlujoCongeladoPorDium modelo = _mapper.Map<TReporteFlujoCongeladoPorDium>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TReporteFlujoCongeladoPorDium Add(ReporteFlujoCongeladoPorDium entidad)
        {
            try
            {
                var ReporteFlujoCongeladoPorDium = MapeoEntidad(entidad);
                base.Insert(ReporteFlujoCongeladoPorDium);
                return ReporteFlujoCongeladoPorDium;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TReporteFlujoCongeladoPorDium Update(ReporteFlujoCongeladoPorDium entidad)
        {
            try
            {
                var ReporteFlujoCongeladoPorDium = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ReporteFlujoCongeladoPorDium.RowVersion = entidadExistente.RowVersion;

                base.Update(ReporteFlujoCongeladoPorDium);
                return ReporteFlujoCongeladoPorDium;
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


        public IEnumerable<TReporteFlujoCongeladoPorDium> Add(IEnumerable<ReporteFlujoCongeladoPorDium> listadoEntidad)
        {
            try
            {
                List<TReporteFlujoCongeladoPorDium> listado = new List<TReporteFlujoCongeladoPorDium>();
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

        public IEnumerable<TReporteFlujoCongeladoPorDium> Update(IEnumerable<ReporteFlujoCongeladoPorDium> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TReporteFlujoCongeladoPorDium> listado = new List<TReporteFlujoCongeladoPorDium>();
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
