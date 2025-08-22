using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.WhatsApp;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.WhatsApp
{
    public class FiltradoDeDatosPorPrioridadWhatsAppRepository: GenericRepository<TFiltradoDeDatosPorPrioridadWhatsApp>, IFiltradoDeDatosPorPrioridadWhatsAppRepository
    {
        public Mapper _mapper;
        public FiltradoDeDatosPorPrioridadWhatsAppRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFiltradoDeDatosPorPrioridadWhatsApp, FiltradoDeDatosPorPrioridadWhatsAppDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        private TFiltradoDeDatosPorPrioridadWhatsApp MapeoEntidad(AgendaTab entidad)
        {
            try
            {
                TFiltradoDeDatosPorPrioridadWhatsApp modelo = _mapper.Map<TFiltradoDeDatosPorPrioridadWhatsApp>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TFiltradoDeDatosPorPrioridadWhatsApp> ObtenerFiltradoPorCampaniaGeneralAndPrioridad(int campaniaGeneral, int prioridad) 
        {
            try
            {
                return base.GetBy(x=>x.IdCampaniaGeneral==campaniaGeneral && x.Prioridad==prioridad).ToList();
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<TFiltradoDeDatosPorPrioridadWhatsApp> ObtenerFiltradoPorCampaniaGeneral(int campaniaGeneral)
        {
            try
            {
                return base.GetBy(x => x.IdCampaniaGeneral == campaniaGeneral).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
