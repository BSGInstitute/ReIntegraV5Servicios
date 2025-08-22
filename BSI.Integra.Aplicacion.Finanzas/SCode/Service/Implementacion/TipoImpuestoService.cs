using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: TipoImpuestoService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_TipoImpuesto
    /// </summary>
    public class TipoImpuestoService : ITipoImpuestoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoImpuestoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
                { 
                    cfg.CreateMap<TTipoImpuesto, TipoImpuesto>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TipoImpuestoRecibidoDTO, TipoImpuesto>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TipoImpuesto Add(TipoImpuestoRecibidoDTO data,string Usuario)
        {
            try
            {
                TipoImpuesto entidad = _mapper.Map<TipoImpuesto>(data);
                entidad.Id = 0;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = _unitOfWork.TipoImpuestoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoImpuesto>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoImpuesto Update(TipoImpuestoRecibidoDTO data, string Usuario)
        {
            try
            {
                var repositorio = _unitOfWork.TipoImpuestoRepository;
                var entidad = _mapper.Map<TipoImpuesto>(repositorio.FirstById(data.Id));
                entidad.Valor = data.Valor;
                entidad.Descripcion = data.Descripcion;
                entidad.Nombre = data.Nombre;
                entidad.IdPais = data.IdPais;
                entidad.Activo = data.Activo;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                var modelo = repositorio.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoImpuesto>(modelo);
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
                _unitOfWork.TipoImpuestoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoImpuesto> Add(List<TipoImpuesto> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoImpuestoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoImpuesto>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoImpuesto> Update(List<TipoImpuesto> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoImpuestoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoImpuesto>>(modelo);
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
                _unitOfWork.TipoImpuestoRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoImpuesto
        /// </summary>
        /// <returns> List<TipoImpuestoDTO> </returns>
        public IEnumerable<TipoImpuestoDTO> ObtenerTipoImpuesto()
        {
            try
            {
                return _unitOfWork.TipoImpuestoRepository.ObtenerTipoImpuesto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoImpuesto para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoImpuestoComboDTO> </returns>
        public IEnumerable<TipoImpuestoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TipoImpuestoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
