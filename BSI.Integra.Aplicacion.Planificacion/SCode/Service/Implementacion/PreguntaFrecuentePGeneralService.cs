using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: PreguntaFrecuentePGeneralService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_PreguntaFrecuentePGeneral
    /// </summary>
    public class PreguntaFrecuentePGeneralService : IPreguntaFrecuentePGeneralService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PreguntaFrecuentePGeneralService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPreguntaFrecuentePgeneral, PreguntaFrecuentePGeneral>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public PreguntaFrecuentePGeneral Add(PreguntaFrecuentePGeneral entidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaFrecuentePGeneralRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PreguntaFrecuentePGeneral>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PreguntaFrecuentePGeneral Update(PreguntaFrecuentePGeneral entidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaFrecuentePGeneralRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PreguntaFrecuentePGeneral>(modelo);
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
                _unitOfWork.PreguntaFrecuentePGeneralRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreguntaFrecuentePGeneral> Add(List<PreguntaFrecuentePGeneral> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaFrecuentePGeneralRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PreguntaFrecuentePGeneral>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreguntaFrecuentePGeneral> Update(List<PreguntaFrecuentePGeneral> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntaFrecuentePGeneralRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PreguntaFrecuentePGeneral>>(modelo);
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
                _unitOfWork.PreguntaFrecuentePGeneralRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PreguntaFrecuentePGeneral para mostrarse en combo.
        /// </summary>
        /// <returns> List<PreguntaFrecuentePGeneralDTO> </returns>
        public IEnumerable<PreguntaFrecuentePGeneralDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.PreguntaFrecuentePGeneralRepository.Obtener();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Preguntas Frecuentes asociadas a un Centro de Costo
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> List<PreguntaFrecuentePorCentroCostoDTO> </returns>
        public IEnumerable<PreguntaFrecuentePorCentroCostoDTO> ObtenerPreguntaFrecuentePorIdCentroCosto(int idCentroCosto)
        {
            try
            {
                return _unitOfWork.PreguntaFrecuentePGeneralRepository.ObtenerPreguntaFrecuentePorIdCentroCosto(idCentroCosto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el detalle agrupado de Preguntas Frecuentes asociadas a un Centro de Costo
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> List<PreguntaFrecuentePorCentroCostoDTO> </returns>
        public IEnumerable<PreguntaFrecuenteDetallePorCentroCostoDTO> ObtenerPreguntaFrecuenteDetallePorIdCentroCosto(int idCentroCosto)
        {
            try
            {
                var preguntas = _unitOfWork.PreguntaFrecuentePGeneralRepository.ObtenerPreguntaFrecuentePorIdCentroCosto(idCentroCosto);
                var preguntaDetalle = new List<PreguntaFrecuenteDetallePorCentroCostoDTO>();
                if (preguntas != null)
                {
                    preguntaDetalle =
                        (from p in preguntas
                         group p by new
                         {
                             p.IdPrograma,
                             p.IdSeccion,
                             p.Nombre
                         } into g
                         select new PreguntaFrecuenteDetallePorCentroCostoDTO
                         {
                             IdPrograma = g.Key.IdPrograma,
                             IdSeccion = g.Key.IdSeccion,
                             Nombre = g.Key.Nombre,
                             Detalle = g.Select(o => new PreguntaFrecuentePreguntaRespuestaDTO
                             {
                                 Pregunta = o.Pregunta,
                                 Respuesta = o.Respuesta
                             }).ToList()
                         }).ToList();
                }
                return preguntaDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Preguntas Frecuentes por ProgramaCentroCostoDTO data
        /// </summary>
        /// <param name="data"></param>
        /// <returns>List<PreguntaFrecuentePGeneralDTO2></returns>
        public List<PreguntaFrecuentePGeneralRespuestaDTO> ObtenerPreguntaFrecuente(ProgramaCentroCostoDTO data)
        {
            try
            {
                return _unitOfWork.PreguntaFrecuentePGeneralRepository.ObtenerPreguntaFrecuente(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
