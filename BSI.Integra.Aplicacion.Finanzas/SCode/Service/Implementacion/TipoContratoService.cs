using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: TipoContratoService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_TipoContrato
    /// </summary>
    public class TipoContratoService : ITipoContratoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoContratoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoContrato, TipoContrato>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoContratoDTO, TipoContrato>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        

        #region Metodos Base
        public TipoContrato Add(TipoContratoDTO data,string Usuario)
        {
            try
            {
                TipoContrato entidad = _mapper.Map<TipoContrato>(data);
                entidad.Id = 0;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = _unitOfWork.TipoContratoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoContrato>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoContrato Update(TipoContratoDTO data, string Usuario)
        {
            try
            {
                var repositorioTipoContrato = _unitOfWork.TipoContratoRepository;
                var entidad = _mapper.Map<TipoContrato>(repositorioTipoContrato.FirstById(data.Id));
                entidad.Comentario = data.Comentario;
                entidad.Nombre = data.Nombre;
                entidad.IdPais = data.IdPais;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;

                var modelo = _unitOfWork.TipoContratoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoContrato>(modelo);
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
                _unitOfWork.TipoContratoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoContrato> Add(List<TipoContrato> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoContratoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoContrato>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoContrato> Update(List<TipoContrato> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoContratoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoContrato>>(modelo);
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
                _unitOfWork.TipoContratoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoContrato
        /// </summary>
        /// <returns> List<TipoContratoDTO> </returns>
        public IEnumerable<TipoContratoDTO> ObtenerTipoContrato()
        {
            try
            {
                return _unitOfWork.TipoContratoRepository.ObtenerTipoContrato();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoContrato para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoContratoComboDTO> </returns>
        public IEnumerable<TipoContratoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TipoContratoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
