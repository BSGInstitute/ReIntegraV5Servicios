using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: FaseOportunidadService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_FaseOportunidad
    /// </summary>
    public class FaseOportunidadService : IFaseOportunidadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FaseOportunidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFaseOportunidad, FaseOportunidad>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public FaseOportunidad Add(FaseOportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.FaseOportunidadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FaseOportunidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FaseOportunidad Update(FaseOportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.FaseOportunidadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FaseOportunidad>(modelo);
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
                _unitOfWork.FaseOportunidadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FaseOportunidad> Add(List<FaseOportunidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FaseOportunidadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FaseOportunidad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FaseOportunidad> Update(List<FaseOportunidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FaseOportunidadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FaseOportunidad>>(modelo);
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
                _unitOfWork.FaseOportunidadRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_FaseOportunidad para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<FaseOportunidadComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.FaseOportunidadRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_FaseOportunidad
        /// </summary>
        /// <returns> List<FaseOportunidadDTO> </returns>
        public IEnumerable<FaseOportunidadDTO> ObtenerFaseOportunidad()
        {
            try
            {
                return _unitOfWork.FaseOportunidadRepository.ObtenerFaseOportunidad();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Valida si la Oportunidad se encuentra en Fase de Cierre
        /// </summary>
        /// <param name="idFase">Id de la Fase Oportunidad</param>
        /// <returns> bool </returns>
        public bool ValidarFaseCierreOportunidad(int idFase)
        {
            try
            {
                return _unitOfWork.FaseOportunidadRepository.ValidarFaseCierreOportunidad(idFase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna si la Fase como parametro es una Fase IS.
        /// </summary>
        /// <param name="idFase">Id de la Fase Oportunidad</param>
        /// <returns> bool </returns>
        public bool ValidarFaseIS(int idFase)
        {
            try
            {
                return _unitOfWork.FaseOportunidadRepository.ValidarFaseIS(idFase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna el Id de la Fase Maxima segun dos fases entregadas.
        /// </summary>
        /// <param name="faseUno"> Fase Uno </param>
        /// <param name="faseDos"> Fase Dos </param>
        /// <returns> Retorna el Id de la Fase Maxima segun dos fases entregadas : int en caso de no entregar id's aptops se retorna 2  </returns>
        public int ObternerFaseMaximaHistoria(int faseUno, int faseDos)
        {
            try
            {
                return _unitOfWork.FaseOportunidadRepository.ObternerFaseMaximaHistoria(faseUno, faseDos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_FaseOportunidad
        /// </summary>
        /// <returns> List<FaseOportunidadComboDTO> </returns>
        public List<FaseOportunidadComboDTO> ObtenerFaseOportunidadTodoFiltro()
        {
            try
            {
                return _unitOfWork.FaseOportunidadRepository.ObtenerFaseOportunidadTodoFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fase oportunidad mediante Id interaccionChat
        /// </summary>
        /// <param name="idInteraccionChat"></param>
        /// <returns> FaseOportunidadInteraccionDTO </returns>
        public FaseOportunidadInteraccionDTO ObtenerFaseOportunidadPorInteraccionId(int idInteraccionChat)
        {
            try
            {
                return _unitOfWork.FaseOportunidadRepository.ObtenerFaseOportunidadPorInteraccionId(idInteraccionChat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
		/// Obtiene datos de la Oportunidad mediante IdFaseOportunidad
		/// </summary>
		/// <param name="idFaseOportunidadPortal"></param>
		/// <returns> OportunidadDatosChatDTO </returns>
        public OportunidadDatosChatDTO ObtenerOportunidadDatosChatPorIdFaseOportunidadPortal(string idFaseOportunidadPortal)
        {
            try
            {
                return _unitOfWork.FaseOportunidadRepository.ObtenerOportunidadDatosChatPorIdFaseOportunidadPortal(idFaseOportunidadPortal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
		/// Obtiene datos de la oportunidad mediante IdFaseOportunidad
		/// </summary>
		/// <param name="idFaseOportunidadPortal"></param>
		/// <returns> OportunidadDatosChatDTO </returns>
        public OportunidadDatosChatDTO ObtenerOportunidadDatosChatPorIdFaseOportunidadPortalAA(string idFaseOportunidadPortal)
        {
            try
            {
                return _unitOfWork.FaseOportunidadRepository.ObtenerOportunidadDatosChatPorIdFaseOportunidadPortalAA(idFaseOportunidadPortal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<FaseOportunidadComboDTO> ObtenerComboFiltroSegmento()
        {
            try
            {
                return _unitOfWork.FaseOportunidadRepository.ObtenerComboFiltroSegmento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
