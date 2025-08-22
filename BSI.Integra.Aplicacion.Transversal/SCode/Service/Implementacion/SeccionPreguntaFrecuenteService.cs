using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: SeccionPreguntaFrecuenteService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_SeccionPreguntaFrecuente
    /// </summary>
    public class SeccionPreguntaFrecuenteService : ISeccionPreguntaFrecuenteService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SeccionPreguntaFrecuenteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSeccionPreguntaFrecuente, SeccionPreguntaFrecuente>(MemberList.None).ReverseMap();
               
            }
           );

         
            _mapper = new Mapper(config);
        }
         
    }
     
}
