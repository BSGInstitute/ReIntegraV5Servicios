using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: OportunidadInformacionService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 23/07/2022
    /// <summary>
    /// Gestión general de Informacion de Oportunidades
    /// </summary>
    public class OportunidadInformacionService : IOportunidadInformacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public OportunidadInformacionBoDTO oportunidadInformacionBoDTO;
        public OportunidadInformacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, Entity>(MemberList.None).ReverseMap());
            //_mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion relacionada a Oportunidades basado en IdClasificacionPersona y IdAlumno* (No usado por el momento)
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> List<OportunidadInformacionDTO> </returns>
        public OportunidadInformacionDTO ObtenerOportunidadInformacion(int idAlumno, int idClasificacionPersona)
        {
            try
            {
                var oportunidadInformacion = new OportunidadInformacionDTO()
                {
                    IdAlumno = idAlumno,
                    IdClasificacionPersona = idClasificacionPersona
                };
                var oportunidadService = new OportunidadService(_unitOfWork);
                oportunidadInformacion.ListaVentaCruzada =
                    oportunidadService.ObtenerVentaCruzadaParaAgendaPorIdClasificacionPersona(oportunidadInformacion.IdClasificacionPersona).ToList();

                oportunidadInformacion.ListaHistorial =
                    oportunidadService.ObtenerHistorialOportunidadesParaAgendaPorIdClasificacionPersona(oportunidadInformacion.IdClasificacionPersona).ToList();

                return oportunidadInformacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Información de Oportunidad por IdAlumno e IdClasificacionPersona
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idClasificacionPersona"></param>
        /// <returns> InformacionOportunidadDTO </returns>
        public InformacionOportunidadDTO ObtenerInformacionOportunidad(int idAlumno, int idClasificacionPersona)
        {
            try
            {
                var oportunidadInformacion = new InformacionOportunidadDTO()
                {
                    IdAlumno = idAlumno,
                    IdClasificacionPersona = idClasificacionPersona
                };
                var oportunidadService = new OportunidadService(_unitOfWork);
                oportunidadInformacion.HistorialOportunidades =
                    oportunidadService.CargarOportunidadHistorial(oportunidadInformacion.IdAlumno, oportunidadInformacion.IdClasificacionPersona);

                oportunidadInformacion.VentasCruzadas =
                    oportunidadService.ObtenerHistorialOportunidades(oportunidadInformacion.IdAlumno, oportunidadInformacion.IdClasificacionPersona);

                return oportunidadInformacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los prerequisitos, beneficios y empresa competidora para un Programa General
        /// </summary>
        /// <param name="idOportunidad"></param>
        public void CargarPrerequisitosBeneficios(int idOportunidad)
        {
            int i = 0;
            ProgramaGeneralPreBenCompuestoDTO programaGeneralPreBen = new ProgramaGeneralPreBenCompuestoDTO();
            programaGeneralPreBen.ListaPreGeneral = new List<ProgramaGeneralPrerequisitoOportunidadDTO>();
            programaGeneralPreBen.ListaPreEspecifico = new List<ProgramaGeneralPrerequisitoOportunidadDTO>();
            programaGeneralPreBen.ListaBeneficios = new List<ProgramaGeneralBeneficioOportunidadDTO>();
            programaGeneralPreBen.ListaCompetidores = new List<ComboDTO>();
            programaGeneralPreBen.OportunidadCompetidor = new OportunidadCompetidorFinalizarActividadDTO();
            programaGeneralPreBen.ListaPreGeneral = _unitOfWork.ProgramaGeneralPrerequisitoRepository.ObtenerProgramaGeneralPrerequisitoPorIdOportunidad(idOportunidad).ToList();
            programaGeneralPreBen.ListaPreEspecifico = _unitOfWork.ProgramaGeneralPrerequisitoRepository.ObtenerProgramaGeneralPrerequisitoEspecificoPorIdOportunidad(idOportunidad).ToList();
            programaGeneralPreBen.ListaBeneficios = _unitOfWork.ProgramaGeneralBeneficioRepository.ObtenerProgramaGeneralBeneficioPorIdOportunidad(idOportunidad).ToList();

            foreach (var item in programaGeneralPreBen.ListaBeneficios)
            {
                programaGeneralPreBen.ListaBeneficios[i].Argumentos = _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.ObtenerProgramaGeneralBeneficioArgumentoPorIdBeneficio(item.IdBeneficio).ToList();
                i++;
            }

            var oportunidadCompetidores = _unitOfWork.OportunidadCompetidorRepository.ObtenerOportunidadCompetidorPorIdOportunidad(idOportunidad);
            var oportunidadCompetidorDTO = (from t1 in oportunidadCompetidores
                                            select new OportunidadCompetidorFinalizarActividadDTO
                                            {
                                                Id = t1.Id,
                                                IdOportunidad = t1.IdOportunidad.Value,
                                                Completado = t1.Completado,
                                                OtroBeneficio = t1.OtroBeneficio,
                                                Respuesta = t1.Respuesta,
                                            }).FirstOrDefault();
            if (oportunidadCompetidorDTO != null)
            {
                var listaCompetidorDto = _unitOfWork.DetalleOportunidadCompetidorRepository.ObtenerDetallePorIdOportunidadCompetidor(oportunidadCompetidorDTO.Id);
                programaGeneralPreBen.OportunidadCompetidor = oportunidadCompetidorDTO;

                foreach (var item in listaCompetidorDto)
                {
                    var empresa = _unitOfWork.EmpresaRepository.ObtenerPorId(item.IdCompetidor);
                    if (empresa.Nombre != null)
                    {
                        var competidor = new ComboDTO
                        {
                            Id = item.IdCompetidor,
                            Nombre = empresa.Nombre
                        };
                        programaGeneralPreBen.ListaCompetidores.Add(competidor);
                    }
                }
            }
            oportunidadInformacionBoDTO.ProgramaGeneralPreBen = programaGeneralPreBen;
        }
    }
}
