using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: OrigenIngresoCajaService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_OrigenIngresoCaja
    /// </summary>
    public class OrigenIngresoCajaService : IOrigenIngresoCajaService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OrigenIngresoCajaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOrigenIngresoCaja, OrigenIngresoCaja>(MemberList.None).ReverseMap();
                cfg.CreateMap<OrigenIngresoCajaDTO, OrigenIngresoCaja>(MemberList.None).ReverseMap();
            }
          );

           
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public OrigenIngresoCaja Add(OrigenIngresoCajaDTO data,string Usuario)
        {
            try
            {
                OrigenIngresoCaja entidad = new OrigenIngresoCaja();
                entidad.Id = 0;
                entidad.Nombre = data.Nombre;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.Estado = true;
                var modelo = _unitOfWork.OrigenIngresoCajaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OrigenIngresoCaja>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OrigenIngresoCaja Update(OrigenIngresoCajaDTO data,string Usuario)
        {
            try
            {
                var repOrigenIngresoCaja = _unitOfWork.OrigenIngresoCajaRepository;
                OrigenIngresoCaja entidad = new OrigenIngresoCaja();
                entidad = _mapper.Map<OrigenIngresoCaja>(repOrigenIngresoCaja.FirstById(data.Id));
                entidad.Nombre = data.Nombre;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                var modelo = repOrigenIngresoCaja.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OrigenIngresoCaja>(modelo);
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
                _unitOfWork.OrigenIngresoCajaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrigenIngresoCaja> Add(List<OrigenIngresoCaja> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenIngresoCajaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OrigenIngresoCaja>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrigenIngresoCaja> Update(List<OrigenIngresoCaja> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenIngresoCajaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OrigenIngresoCaja>>(modelo);
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
                _unitOfWork.OrigenIngresoCajaRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_OrigenIngresoCaja para mostrarse en combo.
        /// </summary>
        /// <returns> List<OrigenIngresoCajaComboDTO> </returns>
        public IEnumerable<OrigenIngresoCajaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.OrigenIngresoCajaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }

}
