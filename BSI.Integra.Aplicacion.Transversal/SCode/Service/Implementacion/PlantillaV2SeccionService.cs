using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PlantillaV2SeccionService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>s
    /// Gestión general de T_PlantillaV2Seccion
    /// </summary>
    public class PlantillaV2SeccionService : IPlantillaV2SeccionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PlantillaV2SeccionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPlantillaV2seccion, PlantillaV2Seccion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public List<PSSeccion> ObtenerTodo(int id)
        {
            try
            {
                var todo = _unitOfWork.PlantillaV2SeccionRepository.ObtenerTodo(id);
                var respuesta = todo.GroupBy(x => new
                {
                    x.IdPlantillaSeccion,
                    x.IdPlantillaV2,
                    x.IdSeccion,
                    x.NombreSeccion,
                    x.EstadoTexto
                }).Select(g => new PSSeccion
                {
                    IdPlantillaSeccion = g.Key.IdPlantillaSeccion,
                    IdPlantillaV2 = g.Key.IdPlantillaV2,
                    IdSeccion = g.Key.IdSeccion,
                    NombreSeccion = g.Key.NombreSeccion,
                    EstadoTexto = g.Key.EstadoTexto,
                    PSEstilo = todo.Where(x => x.IdPlantillaSeccion == g.Key.IdPlantillaSeccion && x.IdEstilo != null).Select(x => new PSEstilo
                    {
                        IdEstilo = (int)x.IdEstilo,
                        IdPlantillaSeccionEstilo = (int)x.IdPlantillaSeccionEstilo,
                        valor = x.valor,
                        NombreEstilo = x.NombreEstilo,
                        NombreTipo = x.NombreTipo,
                        CodigoCss = x.CodigoCss

                    }).ToList()
                }).ToList();
                return respuesta;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<PlantillaSeccionDTO> ActualizacionMasiva(List<PlantillaSeccionDTO> data)
        {
            try
            {

                var servicio = new PlantillaV2SeccionEstiloService(_unitOfWork);
                foreach (var item in data)
                {
                    if (item.Id == 0)
                    {
                        if (item.Eliminado == false)
                        {

                            var entidad = new PlantillaV2SeccionEnvio();
                            entidad.IdPlantillaV2 = item.IdPlantillaV2;
                            entidad.IdSeccion = item.IdSeccion;
                            entidad.Usuario = item.Usuario;

                            var nuevo = Add(entidad);

                            foreach (var s in item.Estilos)
                            {
                                s.IdPlantillav2Seccion = nuevo.Id;
                            }
                            servicio.ActualizacionMasiva(item.Estilos, item.Usuario);

                        }
                    }
                    else
                    {
                        if (item.Eliminado == false)
                        {
                            var entidad = new PlantillaV2SeccionEnvio();
                            entidad.Id = item.Id;
                            entidad.IdPlantillaV2 = item.IdPlantillaV2;
                            entidad.IdSeccion = item.IdSeccion;
                            entidad.Usuario = item.Usuario;
                            Update(entidad);
                            servicio.ActualizacionMasiva(item.Estilos, item.Usuario);

                        }
                        else
                        {

                            Delete(item.Id, item.Usuario);
                            foreach (var s in item.Estilos)
                            {
                                s.Eliminado = true;
                            }
                            servicio.ActualizacionMasiva(item.Estilos, item.Usuario);

                        }
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public PlantillaV2Seccion Add(PlantillaV2SeccionEnvio data)
        {
            try
            {
                var repPlantillaV2Seccion = _unitOfWork.PlantillaV2SeccionRepository;
                PlantillaV2Seccion entidad = new PlantillaV2Seccion();
                entidad.IdPlantillaV2 = data.IdPlantillaV2;
                entidad.IdSeccion = data.IdSeccion;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;


                var modelo = _unitOfWork.PlantillaV2SeccionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PlantillaV2Seccion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PlantillaV2Seccion Update(PlantillaV2SeccionEnvio data)
        {
            try
            {
                var repPlantillaV2Seccion = _unitOfWork.PlantillaV2SeccionRepository;
                PlantillaV2Seccion entidad = new PlantillaV2Seccion();
                entidad = _mapper.Map<PlantillaV2Seccion>(repPlantillaV2Seccion.FirstById(data.Id));
                entidad.IdPlantillaV2 = data.IdPlantillaV2;
                entidad.IdSeccion = data.IdSeccion;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;

                var modelo = _unitOfWork.PlantillaV2SeccionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PlantillaV2Seccion>(modelo);
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
                _unitOfWork.PlantillaV2SeccionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlantillaV2Seccion> Add(List<PlantillaV2Seccion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaV2SeccionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PlantillaV2Seccion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlantillaV2Seccion> Update(List<PlantillaV2Seccion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaV2SeccionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PlantillaV2Seccion>>(modelo);
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
                _unitOfWork.PlantillaV2SeccionRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_PlantillaV2Seccion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<PlantillaV2SeccionCombo> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.PlantillaV2SeccionRepository.ObtenerCombo();
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
        /// Obtiene todos los registros de T_PlantillaV2Seccion
        /// </summary>
        /// <returns> List<PlantillaV2SeccionDTO> </returns>
        public IEnumerable<PlantillaV2Seccion> ObtenerPlantillaV2Seccion()
        {
            try
            {
                return _unitOfWork.PlantillaV2SeccionRepository.ObtenerPlantillaV2Seccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
