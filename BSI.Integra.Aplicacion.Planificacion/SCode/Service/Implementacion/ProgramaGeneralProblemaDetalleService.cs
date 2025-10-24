using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
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
    public class ProgramaGeneralProblemaDetalleService : IProgramaGeneralProblemaDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralProblemaDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaDetalle, ProgramaGeneralProblemaDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralProblemaDetalle, ProgramaGeneralProblemaDetalleDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralProblemaDetalle, ProgramaGeneralProblemaDetalleDTO>(MemberList.None).ReverseMap();
     

            });
            _mapper = new Mapper(config);
        }

        public ProgramaGeneralProblemaDetalleInsertarDTO Insertar(ProgramaGeneralProblemaDetalleInsertarDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    ProgramaGeneralProblemaDetalle entidad = new()
                    {
                        IdPgeneral = dto.IdPGeneral,
                        IdProgramaGeneralProblemaFactor = dto.IdProblema,
                        IdProgramaGeneralProblemaFactorDetalle= dto.IdProblemaDetalle,
                        AplicaNombreDetalle = dto.DetalleDescripcion ,
                        AplicaTituloDetalle = dto.DetalleTitulo ,
                        AplicaPieDePagina = dto.DetallePiePagina ,
                        AplicaDescripcionSolucion = dto.SolucionDescripcion,
                        AplicaTituloSolucion = dto.SolucionTitulo,
                        AplicaSubTituloSolucion = dto.SolucionSubTitulo,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.ProgramaGeneralProblemaDetalleRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;

                    var resultado = _mapper.Map<ProgramaGeneralProblemaDetalleInsertarDTO>(respuesta);

                    if (dto.Soluciones != null && dto.Soluciones.Count() > 0)
                    {
                        var soluciones = dto.Soluciones.Select(x => new ProgramaGeneralProblemaFactorSubSolucionAsignada
                        {
                            IdProgramaGeneralProblemaDetalle = respuesta.Id,
                            IdProgramaGeneralProblemaFactorSubSolucion = x.IdProgramaGeneralProblemaFactorSubSolucion,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                        });
                        var res = _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository.Add(soluciones);
                        _unitOfWork.Commit();
                        resultado.Soluciones = _mapper.Map<List<ProgramaGeneralProblemaSubSolucionesInsertarDTO>>(res);
                    }
                    

                    return resultado;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
