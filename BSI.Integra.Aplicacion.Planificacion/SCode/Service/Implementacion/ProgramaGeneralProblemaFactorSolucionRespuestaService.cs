using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    public class ProgramaGeneralProblemaFactorSolucionRespuestaService : IProgramaGeneralProblemaFactorSolucionRespuestaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralProblemaFactorSolucionRespuestaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaFactorSolucionRespuestum, ProgramaGeneralProblemaFactorSolucionRespuesta>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralProblemaFactorSolucionRespuestum, ProgramaGeneralProblemaFactorSolucionRespuestaDTO>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }

        public bool GuardarProblemaClienteSolucion(ProgramaGeneralProblemaFactorSolucionRespuestaDTO obj, string usuario)
        {
            try
            {
                ProgramaGeneralProblemaFactorSolucionRespuesta problemaRespuesta = _unitOfWork.ProgramaGeneralProblemaFactorSolucionRespuestaSolucionRespuestaRepository.ObtenerPorIdOportunidadIdProblemaFactorSolucion(obj.IdOportunidad, obj.IdProgramaGeneralProblemaFactorSolucion);

                if (problemaRespuesta != null && problemaRespuesta.Id != 0)
                {
                    problemaRespuesta.EsSolucionado = obj.EsSolucionado;
                    problemaRespuesta.UsuarioModificacion = usuario;
                    problemaRespuesta.FechaModificacion = DateTime.Now;
                    _unitOfWork.ProgramaGeneralProblemaFactorSolucionRespuestaSolucionRespuestaRepository.Update(problemaRespuesta);
                }
                else
                {
                    problemaRespuesta = new ProgramaGeneralProblemaFactorSolucionRespuesta()
                    {
                        IdOportunidad = obj.IdOportunidad,
                        IdProgramaGeneralProblemaFactorSolucion = obj.IdProgramaGeneralProblemaFactorSolucion,
                        EsSolucionado = obj.EsSolucionado,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    _unitOfWork.ProgramaGeneralProblemaFactorSolucionRespuestaSolucionRespuestaRepository.Add(problemaRespuesta);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
