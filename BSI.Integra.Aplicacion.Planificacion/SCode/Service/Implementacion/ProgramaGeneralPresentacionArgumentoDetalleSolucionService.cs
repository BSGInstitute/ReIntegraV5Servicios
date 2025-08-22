using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class ProgramaGeneralPresentacionArgumentoDetalleSolucionService : IProgramaGeneralPresentacionArgumentoDetalleSolucionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ProgramaGeneralPresentacionArgumentoDetalleSolucionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TProgramaGeneralPresentacionArgumentoDetalleSolucion, ProgramaGeneralPresentacionArgumentoDetalleSolucion>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ProgramaGeneralPresentacionArgumentoDetalleSolucionDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }

        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 01/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene DetalleSolucion de Problemas basado en Id PresentacionArgumento y Id Oportunidad.
        /// </summary>
        /// <param name="idProblema">Id del PresentacionArgumento</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralPresentacionArgumentoDetalleSolucionAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralPresentacionArgumentoDetalleSolucionAgendaDTO> ObtenerProgramaGeneralPresentacionArgumentoDetalleSolucionParaAgenda(int idPresentacionArgumento, int idOportunidad)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralPresentacionArgumentoDetalleSolucionRepository.ObtenerProgramaGeneralPresentacionArgumentoDetalleSolucionParaAgenda(idPresentacionArgumento, idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
