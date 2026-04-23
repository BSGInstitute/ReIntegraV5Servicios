using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using System;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    /// Service: ScorePrimeraOportunidadService
    /// Autor: Jose Vega
    /// Fecha: 22/04/2026
    /// Version: 1.0
    /// <summary>
    /// Obtiene el score P0 (ProbabilidadPerfil) de una oportunidad a partir del
    /// modelo predictivo escalonado de marketing.
    /// </summary>
    public class ScorePrimeraOportunidadService : IScorePrimeraOportunidadService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScorePrimeraOportunidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ScorePrimeraOportunidadDTO ObtenerP0PorIdOportunidad(int idOportunidad)
        {
            try
            {
                if (idOportunidad <= 0)
                    throw new BadRequestException("El IdOportunidad es requerido y debe ser mayor a 0.");

                return _unitOfWork.ModeloPredictivoProbabilidadEscalonadoRepository.ObtenerP0PorIdOportunidad(idOportunidad);
            }
            catch (BadRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerP0PorIdOportunidad: {ex.Message}", ex);
            }
        }
    }
}
