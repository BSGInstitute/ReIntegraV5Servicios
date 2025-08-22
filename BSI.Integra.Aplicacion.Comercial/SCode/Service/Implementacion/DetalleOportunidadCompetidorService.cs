using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: DetalleOportunidadCompetidorService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 04/07/2022
    /// <summary>
    /// Gestión general de T_DetalleOportunidadCompetidor
    /// </summary>
    public class DetalleOportunidadCompetidorService : IDetalleOportunidadCompetidorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DetalleOportunidadCompetidorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDetalleOportunidadCompetidor, DetalleOportunidadCompetidor>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public DetalleOportunidadCompetidor Add(DetalleOportunidadCompetidor entidad)
        {
            try
            {
                var modelo = _unitOfWork.DetalleOportunidadCompetidorRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DetalleOportunidadCompetidor>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DetalleOportunidadCompetidor Update(DetalleOportunidadCompetidor entidad)
        {
            try
            {
                var modelo = _unitOfWork.DetalleOportunidadCompetidorRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DetalleOportunidadCompetidor>(modelo);
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
                _unitOfWork.DetalleOportunidadCompetidorRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DetalleOportunidadCompetidor> Add(List<DetalleOportunidadCompetidor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DetalleOportunidadCompetidorRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DetalleOportunidadCompetidor>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DetalleOportunidadCompetidor> Update(List<DetalleOportunidadCompetidor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DetalleOportunidadCompetidorRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DetalleOportunidadCompetidor>>(modelo);
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
                _unitOfWork.DetalleOportunidadCompetidorRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 04/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DetalleOportunidadCompetidor
        /// </summary>
        /// <returns> List<DetalleOportunidadCompetidorDTO> </returns>
        public IEnumerable<DetalleOportunidadCompetidorDTO> ObtenerDetalleOportunidadCompetidor()
        {
            try
            {
                return _unitOfWork.DetalleOportunidadCompetidorRepository.ObtenerDetalleOportunidadCompetidor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DetalleOportunidadCompetidor para mostrarse en combo.
        /// </summary>
        /// <returns> List<DetalleOportunidadCompetidorComboDTO> </returns>
        public IEnumerable<DetalleOportunidadCompetidorComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.DetalleOportunidadCompetidorRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DetalleOportunidadCompetidor para mostrarse en combo.
        /// </summary>
        /// <param name="idOportunidadCompetidor">Id de Oportunidad Competidor</param>
        /// <returns> List<DetalleOportunidadCompetidorEmpresaDTO> </returns>
        public IEnumerable<DetalleOportunidadCompetidorEmpresaDTO> ObtenerEmpresaCompetidoraPorIdOportunidadCompetidor(int idOportunidadCompetidor)
        {
            try
            {
                return _unitOfWork.DetalleOportunidadCompetidorRepository.ObtenerEmpresaCompetidoraPorIdOportunidadCompetidor(idOportunidadCompetidor);
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
        /// Elimina (Actualiza estado a false ) los competidores que ya no estan en la lista nueva.
        /// </summary>
        /// <param name="idOportunidadCompetidor">Id de Oportunidad Competidor</param>
        /// <param name="usuario">Usuario responsable de la eliminacion</param>
        /// <param name="nuevos">Lista de Identificadores</param>
        /// <returns> List<DetalleOportunidadCompetidorEmpresaDTO> </returns>
        public void EliminarPorOportunidadCompetidor(int idOportunidadCompetidor, string usuario, List<int> nuevos)
        {
            try
            {
                var listaEliminar = _unitOfWork.DetalleOportunidadCompetidorRepository.ObtenerDetallePorIdOportunidadCompetidor(idOportunidadCompetidor).ToList();
                listaEliminar.RemoveAll(x => nuevos.Any(y => y == x.IdCompetidor));
                Delete(listaEliminar.Select(p => p.Id).ToList(), usuario);
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
        /// Elimina (Actualiza estado a false ) los competidores que ya no estan en la lista nueva.
        /// </summary>
        /// <param name="idOportunidadCompetidor">Id de Oportunidad Competidor</param>
        /// <param name="usuario">Usuario responsable de la eliminacion</param>
        /// <param name="nuevos">Lista de Identificadores</param>
        /// <returns> List<DetalleOportunidadCompetidorEmpresaDTO> </returns>
        public async Task EliminarPorOportunidadCompetidorAsync(int idOportunidadCompetidor, string usuario, List<int> nuevos)
        {
            try
            {
                var listaEliminar = await _unitOfWork.DetalleOportunidadCompetidorRepository.ObtenerDetallePorIdOportunidadCompetidorAsync(idOportunidadCompetidor);
                listaEliminar.ToList().RemoveAll(x => nuevos.Any(y => y == x.IdCompetidor));
                _unitOfWork.DetalleOportunidadCompetidorRepository.Delete(listaEliminar.Select(p => p.Id).ToList(), usuario);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_DetalleOportunidadCompetidor asociados a OportunidadCompetidor.
        /// </summary>
        /// <param name="idOportunidadCompetidor">Id de Oportunidad Competidor</param>
        /// <returns> List<DetalleOportunidadCompetidorDTO> </returns>
        public IEnumerable<DetalleOportunidadCompetidorDTO> ObtenerDetallePorIdOportunidadCompetidor(int idOportunidadCompetidor)
        {
            try
            {
                return _unitOfWork.DetalleOportunidadCompetidorRepository.ObtenerDetallePorIdOportunidadCompetidor(idOportunidadCompetidor);
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
        /// Obtiene el registro de T_DetalleOportunidadCompetidor asociado a una OportunidadCompetidor y un Competidor.
        /// </summary>
        /// <param name="idOportunidadCompetidor">Id de Oportunidad Competidor</param>
        /// <param name="idCompetidor">Id de Competidor</param>
        /// <returns> DetalleOportunidadCompetidorDTO </returns>
        public DetalleOportunidadCompetidorDTO ObtenerDetallePorDatosCompetidor(int idOportunidadCompetidor, int idCompetidor)
        {
            try
            {
                return _unitOfWork.DetalleOportunidadCompetidorRepository.ObtenerDetallePorDatosCompetidor(idOportunidadCompetidor, idCompetidor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DetalleOportunidadCompetidor ObtenerPorIdOportunidaCompetidorIdCompetidor(int idOportunidadCompetidor, int idCompetidor)
        {
            try
            {
                return _unitOfWork.DetalleOportunidadCompetidorRepository.ObtenerPorIdOportunidaCompetidorIdCompetidor(idOportunidadCompetidor, idCompetidor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
