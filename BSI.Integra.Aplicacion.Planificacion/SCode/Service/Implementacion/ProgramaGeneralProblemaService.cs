using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ProgramaGeneralProblemaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralProblema
    /// </summary>
    public class ProgramaGeneralProblemaService : IProgramaGeneralProblemaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ProgramaGeneralProblemaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TProgramaGeneralProblema, ProgramaGeneralProblema>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ProgramaGeneralProblemaDTO, ProgramaGeneralProblemaComboDTO>(MemberList.None);
                    cfg.CreateMap<ProgramaGeneralProblemaAgendaDTO, ProgramaGeneralProblemaDetalleAgendaDTO>(MemberList.None);
                    cfg.CreateMap<ProgramaGeneralProblemaAgendaDTO, ProgramaGeneralProblemaDetalleAgendaNuevaAgendaDTO>(MemberList.None);

                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public ProgramaGeneralProblema Add(ProgramaGeneralProblema entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralProblema>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralProblema Update(ProgramaGeneralProblema entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralProblema>(modelo);
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
                _unitOfWork.ProgramaGeneralProblemaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralProblema> Add(List<ProgramaGeneralProblema> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralProblema>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralProblema> Update(List<ProgramaGeneralProblema> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralProblema>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.ProgramaGeneralProblemaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralProblema
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDTO> ObtenerProgramaGeneralProblema()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralProblemaRepository.ObtenerProgramaGeneralProblema();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProgramaGeneralProblema para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaComboDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralProblemaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Problemas de Programa General asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralProblemaAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaAgendaDTO> ObtenerProgramaGeneralProblemaParaAgendaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralProblemaRepository.ObtenerProgramaGeneralProblemaParaAgendaPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Problemas de Programa General asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralProblemaAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaAgendaDTO> ObtenerProgramaGeneralProblemaParaAgendaPorIdOportunidadNuevaAgenda(int idOportunidad)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralProblemaRepository.ObtenerProgramaGeneralProblemaParaAgendaPorIdOportunidadNuevaAgenda(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Problemas de Programa General y sus Argumentos asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralProblemaDetalleAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDetalleAgendaDTO> ObtenerProgramaGeneralProblemaDetalleParaAgendaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var problemas = ObtenerProgramaGeneralProblemaParaAgendaPorIdOportunidad(idOportunidad);


                var problemasDetalle = _mapper.Map<List<ProgramaGeneralProblemaDetalleAgendaDTO>>(problemas);

                var problemaDetalleSolucionService = new ProgramaGeneralProblemaDetalleSolucionService(_unitOfWork);
                problemasDetalle.ForEach(
                    p => p.Argumentos = problemaDetalleSolucionService.ObtenerProgramaGeneralProblemaDetalleSolucionParaAgenda(p.IdProblema, idOportunidad).ToList()
                );
                return problemasDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Carlos Crispin R.
        /// Fecha: 14/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Problemas de Programa General y sus Argumentos asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralProblemaDetalleAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDetalleAgendaNuevaAgendaDTO> ObtenerProgramaGeneralProblemaDetalleParaAgendaPorIdOportunidadNuevaAgenda(int idOportunidad)
        {
            try
            {
                var problemas = ObtenerProgramaGeneralProblemaParaAgendaPorIdOportunidadNuevaAgenda(idOportunidad);


                var problemasDetalle = _mapper.Map<List<ProgramaGeneralProblemaDetalleAgendaNuevaAgendaDTO>>(problemas);

                var problemaDetalleSolucionService = new ProgramaGeneralProblemaDetalleSolucionService(_unitOfWork);
                problemasDetalle.ForEach(
                    p => p.Argumentos = problemaDetalleSolucionService.ObtenerProgramaGeneralProblemaDetalleSolucionParaAgendaNuevaAgenda(p.IdProblema, idOportunidad).ToList()
                );
                return problemasDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Problemas de Programa General junto con sus Argumentos y Modalidades de forma Agrupada.
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaArgumentoModalidadDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaArgumentoModalidadDTO> ObtenerProblemaArgumentoModalidad()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralProblemaRepository.ObtenerProblemaArgumentoModalidad();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Problemas de Programa General junto con sus Argumentos y Modalidades.
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaArgumentoModalidadDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaArgumentoModalidadDetalleDTO> ObtenerProblemaArgumentoModalidadDetalle()
        {
            try
            {
                var problemas = _unitOfWork.ProgramaGeneralProblemaRepository.ObtenerProblemaArgumentoModalidad();
                var problemasDetalle =
                    (from p in problemas
                     group p by new
                     {
                         p.IdPGeneral,
                         p.IdProblema,
                         p.NombreProblema
                     } into g
                     select new ProgramaGeneralProblemaArgumentoModalidadDetalleDTO
                     {
                         IdProblema = g.Key.IdProblema,
                         IdPGeneral = g.Key.IdPGeneral,
                         NombreProblema = g.Key.NombreProblema,
                         Argumentos = g.Select(o => new ProgramaGeneralProblemaArgumentoDetalleSolucionDTO
                         {
                             Id = o.IdArgumentoProblema,
                             Detalle = o.DetalleArgumentoProblema,
                             Solucion = o.SolucionArgumentoProblema
                         }).Where(a => a.Id != null).GroupBy(a => a.Id).Select(a => a.First()).ToList(),
                         Modalidades = g.Select(o => new ProblemaModalidadDTO
                         {
                             Id = o.IdModalidadProblema,
                             Nombre = o.NombreModalidad,
                             IdModalidadCurso = o.IdModalidadCurso
                         }).GroupBy(i => i.Id).Select(i => i.First()).ToList()
                     }).ToList();
                return problemasDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Existe problema asociado al Id.
        /// </summary>
        /// <param name="idProgramaGeneralProblema">Id de ProgramaGeneralProblema</param>
        /// <returns> bool </returns>
        public bool ExistePoblemaPorId(int idProgramaGeneralProblema)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralProblemaRepository.Exist(idProgramaGeneralProblema);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos de ProgramaGeneralProblema asociados a un Id
        /// </summary>
        /// <param name="idProgramaGeneralProblema">Id de ProgramaGeneralProblema</param>
        /// <returns> bool </returns>
        public ProgramaGeneralProblema ObtenerEntidadPorId(int idProgramaGeneralProblema)
        {
            try
            {
                return _mapper.Map<ProgramaGeneralProblema>(_unitOfWork.ProgramaGeneralProblemaRepository.FirstById(idProgramaGeneralProblema));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }


}
