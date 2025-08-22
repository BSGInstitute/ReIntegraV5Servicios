using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PlantillaV2Service
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PlantillaV2
    /// </summary>
    public class PlantillaV2Service : IPlantillaV2Service
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PlantillaV2Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPlantillaV2, PlantillaV2>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public PlantillaV2 Add(PlantillaV2Envio data)
        {
            try
            {
                var repPlantillaV2 = _unitOfWork.PlantillaV2Repository;
                PlantillaV2 entidad = new PlantillaV2();
                entidad.Nombre = data.Nombre;
                entidad.Codigo = data.Codigo;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;


                var modelo = _unitOfWork.PlantillaV2Repository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PlantillaV2>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PlantillaV2 Update(PlantillaV2Envio data)
        {
            try
            {
                var repPlantillaV2 = _unitOfWork.PlantillaV2Repository;
                PlantillaV2 entidad = new PlantillaV2();
                entidad = _mapper.Map<PlantillaV2>(repPlantillaV2.FirstById(data.Id));
                entidad.Nombre = data.Nombre;
                entidad.Codigo = data.Codigo;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;

                var modelo = _unitOfWork.PlantillaV2Repository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PlantillaV2>(modelo);
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
                _unitOfWork.PlantillaV2Repository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlantillaV2> Add(List<PlantillaV2> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaV2Repository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PlantillaV2>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlantillaV2> Update(List<PlantillaV2> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaV2Repository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PlantillaV2>>(modelo);
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
                _unitOfWork.PlantillaV2Repository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_PlantillaV2 para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<PlantillaV2Combo> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.PlantillaV2Repository.ObtenerCombo();
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
        /// Obtiene todos los registros de T_PlantillaV2
        /// </summary>
        /// <returns> List<PlantillaV2DTO> </returns>
        public IEnumerable<PlantillaV2> ObtenerPlantillaV2()
        {
            try
            {
                return _unitOfWork.PlantillaV2Repository.ObtenerPlantillaV2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
