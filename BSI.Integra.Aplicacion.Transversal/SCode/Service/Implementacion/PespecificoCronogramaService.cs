using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PespecificoCronogramaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 27/07/2022
    /// <summary>
    /// Gestión general de T_PespecificoCronograma
    /// </summary>
    public class PespecificoCronogramaService : IPespecificoCronogramaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PespecificoCronogramaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TPespecificoCronograma, PespecificoCronograma>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 23/05/2023
        /// Version: 1.0
        /// <summary>
        /// Devulve todas las sesiones totalizado por IdPEspecifico
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <returns>Objeto</returns>
        public IEnumerable<PEspecificoCronogramaGrupalGrupoDTO> ObtenerPEspecificoCronogramaGrupal(int idPEspecifico)
        {
            try
            {
                var cronogramaGrupal = _unitOfWork.PespecificoCronogramaRepository.ObtenerPEspecificoCronogramaGrupalPorIdPEspecifico(idPEspecifico);

                var resultadoAgrupado = cronogramaGrupal.GroupBy(x => new { x.Grupo })
                    .Select(y => new PEspecificoCronogramaGrupalGrupoDTO
                    {
                        Grupo = y.Key.Grupo,
                        Lista = y.Select(w => new PEspecificoCronogramaGrupalDTO()
                        {
                            Id = w.Id,
                            IdPEspecifico = w.IdPEspecifico,
                            FechaHoraInicio = w.FechaHoraInicio,
                            Duracion = w.Duracion,
                            DuracionTotal = w.DuracionTotal,
                            Curso = w.Curso,
                            Tipo = w.Tipo,
                            Grupo = w.Grupo
                        }).OrderBy(x => x.FechaHoraInicio)
                    });
                return resultadoAgrupado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 23/05/2023
        /// Version: 1.0
        /// <summary>
        /// Calcular Sesiones Crongorama Completo Grupo Desde Grupo
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="grupo"> grupo</param>
        /// <returns>sesiones</returns>
        public IEnumerable<PEspecificoCronogramaGrupalDTO> CalcularSesionesCronogramaGrupoCompletoDesdeGrupo(int idPespecifico, int grupo)
        {
            IEnumerable<PEspecificoCronogramaGrupalDTO> listado = new List<PEspecificoCronogramaGrupalDTO>();
            if (grupo > 0)
            {
                var sesionesPEspecifico = _unitOfWork.PEspecificoSesionRepository.ObtenerSesionesPorPEspecificoGrupo(idPespecifico, grupo);

                if (sesionesPEspecifico != null && sesionesPEspecifico.Count() > 0)
                {
                    listado = sesionesPEspecifico.Select(s => new PEspecificoCronogramaGrupalDTO()
                    {
                        IdPEspecifico = s.IdPEspecifico,
                        FechaHoraInicio = s.FechaHoraInicio,
                        Duracion = s.Duracion,
                        DuracionTotal = s.DuracionTotal,
                        Curso = s.Curso,
                        Tipo = s.ModalidadSesion,
                        Grupo = s.Grupo
                    });
                }
            }
            return listado;
        }
    }
}
