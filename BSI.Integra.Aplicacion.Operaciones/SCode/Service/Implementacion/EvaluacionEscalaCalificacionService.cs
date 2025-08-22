using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: EvaluacionEscalaCalificacionService
    /// Autor: Gilmer Quipse
    /// Fecha: 11/11/2022
    /// <summary>
    /// Gestión general de T_EvaluacionEscalaCalificacion
    /// </summary>
    public class EvaluacionEscalaCalificacionService : IEvaluacionEscalaCalificacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EvaluacionEscalaCalificacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSolicitudOperacionesAccesoTemporalDetalle, SolicitudOperacionesAccesoTemporalDetalle>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 11/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene escala por programa especifico asociados al idPEspecifico
        /// </summary>
        /// <param name="idPEspecifico"> Id de Programa especifico </param>
        /// <returns> Beneficios por CodigoMatricula </returns>
        /// <returns> Objeto: EvaluacionEscalaCalificacion </returns>
        public EvaluacionEscalaCalificacion ObtenerEscalaPorPEspecificoPresencial(int idPEspecifico)
        {
            EvaluacionEscalaCalificacion evaluacionEscalaCalificacion = null;

            var centroCostoService = new CentroCostoService(_unitOfWork);
            var respuesta = centroCostoService.ObtenerDatosCentroCostos(idPEspecifico).FirstOrDefault();

            var listado = ObtenerPorModalidadCurso(0);
            //recorre las escalas de la modalidad
            foreach (var escala_verificar in listado.OrderBy(o => o.Id))
            {
                //identifica cual coincide con el centro de costo
                if (respuesta.CentroCosto.Contains(escala_verificar.CodigoCiudad))
                    evaluacionEscalaCalificacion = escala_verificar;
            }
            return evaluacionEscalaCalificacion;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 11/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene lista de evaluacion escala calificacion filtrado por el IdModalidadCurso
        /// </summary>
        /// <param name="idModalidadCurso"> Id de Modalidad curso </param>
        /// <returns> List<EvaluacionEscalaCalificacion> </returns>
        public List<EvaluacionEscalaCalificacion> ObtenerPorModalidadCurso(int idModalidadCurso)
        {
            try
            {
                return _unitOfWork.EvaluacionEscalaCalificacionRepository.ObtenerPorModalidadCurso(idModalidadCurso);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
