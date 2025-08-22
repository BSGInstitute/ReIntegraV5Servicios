using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TipoDatoService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class TipoDatoService : ITipoDatoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoDatoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoDato, TipoDato>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoDatosDTO, TipoDato>(MemberList.None).ReverseMap();
            }
           );


            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TipoDato Add(TipoDato entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoDatoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoDato>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoDato Update(TipoDato entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoDatoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoDato>(modelo);
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
                _unitOfWork.TipoDatoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoDato> Add(List<TipoDato> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoDatoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoDato>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoDato> Update(List<TipoDato> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoDatoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoDato>>(modelo);
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
                _unitOfWork.TipoDatoRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoDato para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TipoDatoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoDato
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        public IEnumerable<TipoDatoDTO> ObtenerTipoDato()
        {
            try
            {
                return _unitOfWork.TipoDatoRepository.ObtenerTipoDato();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoDato
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        public IEnumerable<TipoDatoFiltroDTO> ObtenerFiltroTipoDato()
        {
            try
            {
                return _unitOfWork.TipoDatoRepository.ObtenerFiltroTipoDato();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 12/10/2022
        /// Version: 1.0
        /// <summary>
		/// Obtiene Lista de Tipo de Datos para filtro en formularios
		/// </summary>
		/// <returns></returns>
        public IEnumerable<ComboDTO> CargarTipoDatoChat()
        {
            try
            {
                return _unitOfWork.TipoDatoRepository.CargarTipoDatoChat();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TipoDato InsertarTipoDato(TipoDatosDTO entidad, string Usuario)
        {
            try
            {
                TipoDato data = _mapper.Map<TipoDato>(entidad);
                data.Id = 0;
                data.UsuarioModificacion = Usuario;
                data.UsuarioCreacion = Usuario;
                data.FechaCreacion = DateTime.Now;
                data.FechaModificacion = DateTime.Now;
                data.Estado = true;
                var modelo = _unitOfWork.TipoDatoRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<TipoDato>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TipoDato ActualizarTipoDato(TipoDatosDTO entidad, string Usuario)
        {
            try
            {
                var rep = _unitOfWork.TipoDatoRepository;
                var entidadActual = _mapper.Map<TipoDato>(rep.FirstById(entidad.Id));
                entidadActual.UsuarioModificacion = Usuario;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.Nombre = entidad.Nombre;
                entidadActual.Descripcion = entidad.Descripcion;
                entidadActual.Prioridad = entidad.Prioridad;


                var modelo = _unitOfWork.TipoDatoRepository.Update(entidadActual);
                _unitOfWork.Commit();
                return _mapper.Map<TipoDato>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
