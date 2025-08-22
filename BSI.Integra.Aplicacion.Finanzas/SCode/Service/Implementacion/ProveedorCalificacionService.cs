using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ProveedorCalificacionService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_ProveedorCalificacion
    /// </summary>
    public class ProveedorCalificacionService : IProveedorCalificacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProveedorCalificacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TProveedorCalificacion, ProveedorCalificacion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProveedorCalificacion Add(ProveedorCalificacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorCalificacionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProveedorCalificacion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProveedorCalificacion Update(ProveedorCalificacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorCalificacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProveedorCalificacion>(modelo);
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
                _unitOfWork.ProveedorCalificacionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProveedorCalificacion> Add(List<ProveedorCalificacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorCalificacionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProveedorCalificacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProveedorCalificacion> Update(List<ProveedorCalificacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorCalificacionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProveedorCalificacion>>(modelo);
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
                _unitOfWork.ProveedorCalificacionRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_ProveedorCalificacion
        /// </summary>
        /// <returns> List<ProveedorCalificacionDTO> </returns>
        public IEnumerable<ProveedorCalificacionDTO> ObtenerProveedorCalificacion()
        {
            try
            {
                return _unitOfWork.ProveedorCalificacionRepository.ObtenerProveedorCalificacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertarCriterioCalificacion(FiltroProveedorCalificacionDTO Calificacion)
        {
            try
            {
                var _repProveedorRep = _unitOfWork.ProveedorRepository;
                var _repProveedorCalificacionRep = _unitOfWork.ProveedorCalificacionRepository;
                var objProveedor = _repProveedorRep.FirstById(Calificacion.IdProveedor);

                if (objProveedor == null)
                    throw new Exception("No se encontro el registro de 'Proveedor' que se quiere calificar");
                objProveedor.IdPrestacionRegistro = Calificacion.IdPrestacionRegistro;
                objProveedor.FechaModificacion = DateTime.Now;
                objProveedor.UsuarioModificacion = Calificacion.UsuarioModificacion;

                _repProveedorRep.Update(objProveedor);
                _unitOfWork.Commit();
                var listaIdCriterio = _repProveedorCalificacionRep.GetBy(x => x.Estado == true && x.IdProveedor == Calificacion.IdProveedor, x => new { Id = x.Id }).ToList();
                foreach (var CriterioCalificacion in listaIdCriterio)
                {
                    if (_repProveedorCalificacionRep.Exist(CriterioCalificacion.Id))
                    {
                        _repProveedorCalificacionRep.Delete(CriterioCalificacion.Id, Calificacion.UsuarioModificacion);
                    }
                    else
                    {
                        return false;
                    }
                }

                for (int i = 0; i < Calificacion.ListaIdSubCriterioCalificacion.Length; i++)
                {
                    ProveedorCalificacion objProveedorCalificacion = new ProveedorCalificacion();
                    objProveedorCalificacion.IdProveedor = Calificacion.IdProveedor;
                    objProveedorCalificacion.IdProveedorSubCriterioCalificacion = Calificacion.ListaIdSubCriterioCalificacion[i];
                    objProveedorCalificacion.Estado = true;
                    objProveedorCalificacion.FechaCreacion = DateTime.Now;
                    objProveedorCalificacion.FechaModificacion = DateTime.Now;
                    objProveedorCalificacion.UsuarioCreacion = Calificacion.UsuarioModificacion;
                    objProveedorCalificacion.UsuarioModificacion = Calificacion.UsuarioModificacion;

                    _repProveedorCalificacionRep.Add(objProveedorCalificacion);
                    _unitOfWork.Commit();
                }


                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

}
