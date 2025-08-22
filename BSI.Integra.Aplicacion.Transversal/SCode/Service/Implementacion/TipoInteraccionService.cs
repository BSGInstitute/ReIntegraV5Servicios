using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TipoInteraccionService
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_TipoInteraccion
    /// </summary>
    public class TipoInteraccionService : ITipoInteraccionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoInteraccionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TTipoInteracccion, TipoInteraccion>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TipoInteraccionesDTO, TipoInteraccion>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }

        #region Metodos Base
       
        public TipoInteraccion Add(TipoInteraccionesDTO entidad, string Usuario )
        {
            try
            {
                TipoInteraccion data = _mapper.Map<TipoInteraccion>(entidad);
                data.Id = 0;
                data.UsuarioModificacion=Usuario;
                data.UsuarioCreacion=Usuario;
                data.FechaCreacion= DateTime.Now;
                data.FechaModificacion = DateTime.Now;
                data.Estado = true;
                var modelo = _unitOfWork.TipoInteraccionRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<TipoInteraccion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoInteraccion Update(TipoInteraccionesDTO entidad,string Usuario)
        {
            try
            {
                var rep = _unitOfWork.TipoInteraccionRepository;
                var entidadActual = _mapper.Map<TipoInteraccion>(rep.FirstById(entidad.Id));
                entidadActual.UsuarioModificacion = Usuario;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.Nombre = entidad.Nombre;
                entidadActual.Canal = entidad.Canal;
                var modelo = _unitOfWork.TipoInteraccionRepository.Update(entidadActual);
                _unitOfWork.Commit();
                return _mapper.Map<TipoInteraccion>(modelo);
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
                _unitOfWork.TipoInteraccionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoInteraccion> Add(List<TipoInteraccion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoInteraccionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoInteraccion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoInteraccion> Update(List<TipoInteraccion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoInteraccionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoInteraccion>>(modelo);
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
                _unitOfWork.TipoInteraccionRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoInteraccion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TipoInteraccionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoInteraccion
        /// </summary>
        /// <returns> List<TipoInteraccionDTO> </returns>
        public IEnumerable<TipoInteraccionDTO> ObtenerTipoInteraccion()
        {
            try
            {
                return _unitOfWork.TipoInteraccionRepository.ObtenerTipoInteraccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoInteraccion para llenoo de combo
        /// </summary>
        /// <returns> List<TipoInteraccionDTO> </returns>

        public IEnumerable<TipoInteraccionCanalDTO> ObtenerTipoInteraccionCanalCombo()
        {
            try
            {
                return _unitOfWork.TipoInteraccionRepository.ObtenerTipoInteraccionCanalCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<FiltroDTO> ObtenerPorTipoInteraccionGeneralFormulario()
        {
            try
            {
                return _unitOfWork.TipoInteraccionRepository.ObtenerPorTipoInteraccionGeneralFormulario();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
