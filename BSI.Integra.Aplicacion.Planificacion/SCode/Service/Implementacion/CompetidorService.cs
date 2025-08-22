using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: CompetidorService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 04/07/2022
    /// <summary>
    /// Gestión general de T_Competidor
    /// </summary>
    public class CompetidorService : ICompetidorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CompetidorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCompetidor, Competidor>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Competidor Add(Competidor entidad)
        {
            try
            {
                var modelo = _unitOfWork.CompetidorRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Competidor>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Competidor Update(Competidor entidad)
        {
            try
            {
                var modelo = _unitOfWork.CompetidorRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Competidor>(modelo);
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
                _unitOfWork.CompetidorRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Competidor> Add(List<Competidor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CompetidorRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Competidor>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Competidor> Update(List<Competidor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CompetidorRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Competidor>>(modelo);
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
                _unitOfWork.CompetidorRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 04/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Competidor
        /// </summary>
        /// <returns> List<CompetidorDTO> </returns>
        public IEnumerable<CompetidorDTO> ObtenerCompetidor()
        {
            try
            {
                return _unitOfWork.CompetidorRepository.ObtenerCompetidor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Competidor para mostrarse en combo.
        /// </summary>
        /// <returns> List<CompetidorComboDTO> </returns>
        public IEnumerable<CompetidorComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CompetidorRepository.ObtenerCombo();
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
        /// Obtiene Competidores relacionados a una Oportunidad para Agenda.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<CompetidorOportunidadAgendaDTO> </returns>
        public IEnumerable<CompetidorOportunidadAgendaDTO> ObtenerCompetidorParaAgendaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var competidoresAgrupados = _unitOfWork.CompetidorRepository.ObtenerCompetidorParaAgendaPorIdOportunidad(idOportunidad).GroupBy(c => c.Id);
                var competidoresProcesado = new List<CompetidorOportunidadAgendaDTO>();
                foreach (var group in competidoresAgrupados)
                {
                    var primerCompetidor = group.First();
                    primerCompetidor.IdCompetidorVentajaDesventaja = primerCompetidor.IdCompetidorVentajaDesventaja ?? 0;
                    primerCompetidor.TipoCompetidorVentajaDesventaja = primerCompetidor.TipoCompetidorVentajaDesventaja ?? 0;
                    primerCompetidor.ContenidoCompetidorVentajaDesventaja = string.Join("", group.ToList().Select(p => p.ContenidoCompetidorVentajaDesventaja ?? "Sin Desventaja"));
                    competidoresProcesado.Add(primerCompetidor);
                }
                return competidoresProcesado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
