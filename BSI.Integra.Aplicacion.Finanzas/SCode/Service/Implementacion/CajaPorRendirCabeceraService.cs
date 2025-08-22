using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: CajaPorRendirCabeceraService
    /// Autor Modificacion: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_CajaPorRendirCabecera
    /// </summary>
    public class CajaPorRendirCabeceraService : ICajaPorRendirCabeceraService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CajaPorRendirCabeceraService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCajaPorRendirCabecera, CajaPorRendirCabecera>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CajaPorRendirCabecera Add(CajaPorRendirCabeceraDTO data)
        {
            try
            {
                CajaPorRendirCabecera entidad = new CajaPorRendirCabecera();
                entidad.Codigo = data.Codigo;
                entidad.IdCaja = data.IdCaja;
                entidad.Anho = DateTime.Now.Year;
                entidad.IdPersonalAprobacion = data.IdPersonalAprobacion;
                entidad.IdPersonalSolicitante = data.IdPersonalSolicitante;
                entidad.Descripcion = data.Descripcion;
                entidad.Observacion = data.Observacion;
                entidad.EsRendido = data.EsRendido;
                entidad.MontoDevolucion = data.MontoDevolucion;
                entidad.Estado = true;
                entidad.UsuarioCreacion = data.UsuarioModificacion;
                entidad.UsuarioModificacion = data.UsuarioModificacion;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                var modelo = _unitOfWork.CajaPorRendirCabeceraRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CajaPorRendirCabecera>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CajaPorRendirCabecera Update(CajaPorRendirCabecera entidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaPorRendirCabeceraRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CajaPorRendirCabecera>(modelo);
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
                _unitOfWork.CajaPorRendirCabeceraRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CajaPorRendirCabecera> Add(List<CajaPorRendirCabecera> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaPorRendirCabeceraRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CajaPorRendirCabecera>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CajaPorRendirCabecera> Update(List<CajaPorRendirCabecera> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaPorRendirCabeceraRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CajaPorRendirCabecera>>(modelo);
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
                _unitOfWork.CajaPorRendirCabeceraRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public IEnumerable<CajaPorRendirCabeceraComboDTO> ObtenerComboCabeceraPR()
        {
            try
            {
                return _unitOfWork.CajaPorRendirCabeceraRepository.ObtenerComboCabeceraPR();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
