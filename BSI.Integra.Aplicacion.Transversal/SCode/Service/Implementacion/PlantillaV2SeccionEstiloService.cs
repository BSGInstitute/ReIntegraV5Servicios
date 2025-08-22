using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PlantillaV2SeccionEstiloService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>s
    /// Gestión general de T_PlantillaV2SeccionEstilo
    /// </summary>
    public class PlantillaV2SeccionEstiloService : IPlantillaV2SeccionEstiloService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PlantillaV2SeccionEstiloService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPlantillaV2seccionEstilo, PlantillaV2SeccionEstilo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public bool ActualizacionMasiva(List<PlantillaSeccionEstilo> data, string usuario)
        {
            try
            {

                foreach (var item in data)
                {
                    if (item.Id == 0)
                    {
                        if (item.Eliminado == false)
                        {

                            var entidad = new PlantillaV2SeccionEstiloEnvio();
                            entidad.IdPlanitillav2Seccion = item.IdPlantillav2Seccion;
                            entidad.IdEstilo = item.IdEstilo;
                            entidad.Valor = item.Valor;
                            entidad.Usuario = usuario;
                            Add(entidad);
                        }
                    }
                    else
                    {
                        if (item.Eliminado == false)
                        {
                            var entidad = new PlantillaV2SeccionEstiloEnvio();
                            entidad.Id = item.Id;
                            entidad.IdPlanitillav2Seccion = item.IdPlantillav2Seccion;
                            entidad.IdEstilo = item.IdEstilo;
                            entidad.Valor = item.Valor;
                            entidad.Usuario = usuario;
                            Update(entidad);
                        }
                        else
                        {

                            Delete(item.Id, usuario);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public PlantillaV2SeccionEstilo Add(PlantillaV2SeccionEstiloEnvio data)
        {
            try
            {
                var repPlantillaV2SeccionEstilo = _unitOfWork.PlantillaV2SeccionEstiloRepository;
                PlantillaV2SeccionEstilo entidad = new PlantillaV2SeccionEstilo();
                entidad.IdPlanitillav2Seccion = data.IdPlanitillav2Seccion;
                entidad.IdEstilo = data.IdEstilo;
                entidad.Valor = data.Valor;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;


                var modelo = _unitOfWork.PlantillaV2SeccionEstiloRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PlantillaV2SeccionEstilo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PlantillaV2SeccionEstilo Update(PlantillaV2SeccionEstiloEnvio data)
        {
            try
            {
                var repPlantillaV2SeccionEstilo = _unitOfWork.PlantillaV2SeccionEstiloRepository;
                PlantillaV2SeccionEstilo entidad = new PlantillaV2SeccionEstilo();
                entidad = _mapper.Map<PlantillaV2SeccionEstilo>(repPlantillaV2SeccionEstilo.FirstById(data.Id));
                entidad.IdEstilo = data.IdEstilo;
                entidad.Valor = data.Valor;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;

                var modelo = _unitOfWork.PlantillaV2SeccionEstiloRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PlantillaV2SeccionEstilo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        #region Metodos Base

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.PlantillaV2SeccionEstiloRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlantillaV2SeccionEstilo> Add(List<PlantillaV2SeccionEstilo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaV2SeccionEstiloRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PlantillaV2SeccionEstilo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlantillaV2SeccionEstilo> Update(List<PlantillaV2SeccionEstilo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaV2SeccionEstiloRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PlantillaV2SeccionEstilo>>(modelo);
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
                _unitOfWork.PlantillaV2SeccionEstiloRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_PlantillaV2SeccionEstilo para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<PlantillaV2SeccionEstiloCombo> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.PlantillaV2SeccionEstiloRepository.ObtenerCombo();
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
        /// Obtiene todos los registros de T_PlantillaV2SeccionEstilo
        /// </summary>
        /// <returns> List<PlantillaV2SeccionEstiloDTO> </returns>
        public IEnumerable<PlantillaV2SeccionEstilo> ObtenerPlantillaV2SeccionEstilo()
        {
            try
            {
                return _unitOfWork.PlantillaV2SeccionEstiloRepository.ObtenerPlantillaV2SeccionEstilo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
