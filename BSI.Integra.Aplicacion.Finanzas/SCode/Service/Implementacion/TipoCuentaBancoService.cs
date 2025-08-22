using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: TipoCuentaBancoService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_TipoCuentaBanco
    /// </summary>
    public class TipoCuentaBancoService : ITipoCuentaBancoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoCuentaBancoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoCuentaBanco, TipoCuentaBanco>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoCuentaBancoDTO, TipoCuentaBanco>(MemberList.None).ReverseMap();
            }
           );

          

            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TipoCuentaBanco Add(TipoCuentaBancoDTO entidad, string Usuario)
        {
            try
            {

                TipoCuentaBanco data = _mapper.Map<TipoCuentaBanco>(entidad);
                data.Id = 0;
                data.UsuarioModificacion = Usuario;
                data.UsuarioCreacion = Usuario;
                data.FechaCreacion = DateTime.Now;
                data.FechaModificacion = DateTime.Now;
                data.Estado = true;
                var modelo = _unitOfWork.TipoCuentaBancoRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<TipoCuentaBanco>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoCuentaBanco Update(TipoCuentaBancoDTO entidad, string Usuario)
        {
            try
            {


                var rep = _unitOfWork.TipoCuentaBancoRepository;
                var entidadActual = _mapper.Map<TipoCuentaBanco>(rep.FirstById(entidad.Id));
                entidadActual.UsuarioModificacion = Usuario;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.Nombre = entidad.Nombre;
                var modelo = _unitOfWork.TipoCuentaBancoRepository.Update(entidadActual);
                _unitOfWork.Commit();
                return _mapper.Map<TipoCuentaBanco>(modelo);
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
                _unitOfWork.TipoCuentaBancoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoCuentaBanco> Add(List<TipoCuentaBanco> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCuentaBancoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoCuentaBanco>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoCuentaBanco> Update(List<TipoCuentaBanco> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCuentaBancoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoCuentaBanco>>(modelo);
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
                _unitOfWork.TipoCuentaBancoRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_TipoCuentaBanco
        /// </summary>
        /// <returns> List<TipoCuentaBancoDTO> </returns>
        public IEnumerable<TipoCuentaBancoDTO> ObtenerTipoCuentaBanco()
        {
            try
            {
                return _unitOfWork.TipoCuentaBancoRepository.ObtenerTipoCuentaBanco();
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
        /// Obtiene registros de T_TipoCuentaBanco para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoCuentaBancoComboDTO> </returns>
        public IEnumerable<TipoCuentaBancoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TipoCuentaBancoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
