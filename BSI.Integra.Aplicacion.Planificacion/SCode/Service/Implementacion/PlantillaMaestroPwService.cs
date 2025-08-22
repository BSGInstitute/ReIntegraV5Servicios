using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: PlantillaMaestroPwService
    /// Autor: Jonathan Caipo
    /// Fecha: 18/10/2023
    /// <summary>
    /// Gestión general de PlantillaMestroPw
    /// </summary>
    public class PlantillaMaestroPwService : IPlantillaMaestroPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PlantillaMaestroPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    //cfg.CreateMap<PlantillaMaestroPw, PlantillaMaestroPwlDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las plantilla por cada IdPlantilla
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns> Lista DTO - List<SeccionPwFiltroPlantillaPwListaSubSeccionesDTO> </returns>
        public List<SeccionPwFiltroPlantillaPwListaSubSeccionesDTO> ObtenerPlantillaSeccionMaestraPorIdPlantilla(int idPlantilla)
        {
            try
            {
                List<SeccionPwFiltroPlantillaPwListaSubSeccionesDTO> listaSeccionSubSeccion = new List<SeccionPwFiltroPlantillaPwListaSubSeccionesDTO>();
                var listaSeccion = _unitOfWork.SeccionPwRepository.ObtenerPlantillaSeccionesPorIdPlantillaPW(idPlantilla);

                listaSeccionSubSeccion = listaSeccion.GroupBy(u => (u.IdPlantilla, u.Id, u.Nombre))
                                    .Select(group =>
                                    new SeccionPwFiltroPlantillaPwListaSubSeccionesDTO
                                    {
                                        Id = group.Key.Id,
                                        IdPlantilla = group.Key.IdPlantilla,
                                        IdPlantillaPw = group.Key.IdPlantilla,
                                        Nombre = group.Key.Nombre,
                                        Descripcion = group.Select(x => x.Descripcion).FirstOrDefault(),
                                        Contenido = group.Select(x => x.Contenido).FirstOrDefault(),
                                        VisibleWeb = group.Select(x => x.VisibleWeb).FirstOrDefault(),
                                        ZonaWeb = group.Select(x => x.ZonaWeb).FirstOrDefault(),
                                        OrdenEeb = group.Select(x => x.OrdenEeb).FirstOrDefault(),
                                        Titulo = group.Select(x => x.Titulo).FirstOrDefault(),
                                        Posicion = group.Select(x => x.Posicion).FirstOrDefault(),
                                        Tipo = group.Select(x => x.Tipo).FirstOrDefault(),
                                        IdSeccionMaestraPw = group.Select(x => x.IdSeccionMaestraPw).FirstOrDefault(),
                                        IdSeccionTipoContenido = group.Select(x => x.IdSeccionTipoContenido).FirstOrDefault(),
                                        NombreSeccionTipoContenido = group.Select(x => x.NombreSeccionTipoContenido).FirstOrDefault(),
                                        ListaSubSeccionesPw = group.Select(x => new SubSeccionTipoDetallePwDTO { 
                                            IdSeccionTipoDetallePw = x.IdSeccionTipoDetallePw, 
                                            NombreSubSeccion = x.NombreSubSeccion, 
                                            IdSubSeccionTipoContenido = x.IdSubSeccionTipoContenido 
                                        }).ToList()
                                    }).ToList();

                return listaSeccionSubSeccion;
            }
            catch
            {
                throw;
            }
        }
    }
}
