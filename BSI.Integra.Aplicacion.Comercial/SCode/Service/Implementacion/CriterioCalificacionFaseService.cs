using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Servicios.Implementacion
{
    /// Servicio: CriterioCalificacionFaseService
    /// Autor: José Vega
    /// Fecha: 20/09/2023
    /// <summary>
    /// Servicio para gestionar los criterios de calificación de fase
    /// </summary>
    public class CriterioCalificacionFaseService : ICriterioCalificacionFaseService
    {
        private IUnitOfWork _unitOfWork;
        private ICriterioCalificacionFaseRepository _repCriterioCalificacionFase;
        private Mapper _mapper;

        public ICriterioCalificacionFaseRepository CriterioCalificacionFaseRepository { get => _repCriterioCalificacionFase; set => _repCriterioCalificacionFase = value; }

        public CriterioCalificacionFaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CriterioCalificacionFaseRepository = _unitOfWork.CriterioFaseRepository;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioCalificacionFaseOportunidad, CriterioCalificacionFase>().ReverseMap();
                cfg.CreateMap<CriterioCalificacionFase, CriterioCalificacionFaseCreateDTO>().ReverseMap();
            });
            _mapper = new Mapper(mapperConfig);

        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Inserta un nuevo criterio de calificación de fase y sus lineamientos asociados
        /// </summary>
        /// <param name="criterioCalificacionFaseDTO">Datos del criterio a insertar</param>
        /// <param name="usuario">Usuario que realiza la operación</param>
        /// <returns>bool</returns>
      /*  public bool InsertarCriterio(CriterioCalificacionFaseDTO criterioCalificacionFaseDTO, string usuario)
        {
            try
            {
                // Mapeo de DTO a Entidad para el criterio
                CriterioCalificacionFase criterioCalificacionFase = new CriterioCalificacionFase
                {
                    IdTransicionCalificacionFase = criterioCalificacionFaseDTO.IdTransicionCalificacionFase,
                    Orden = criterioCalificacionFaseDTO.Orden,
                    Nombre = criterioCalificacionFaseDTO.Nombre,
                    Descripcion = criterioCalificacionFaseDTO.Descripcion,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                };

                // Si hay lineamientos, los agregamos
                if (criterioCalificacionFaseDTO.LineamientoCalificacionFase != null && criterioCalificacionFaseDTO.LineamientoCalificacionFase.Count > 0)
                {
                    criterioCalificacionFase.LineamientoCalificacionFase = new List<LineamientoCalificacionFase>();

                    foreach (var lineamientoDTO in criterioCalificacionFaseDTO.LineamientoCalificacionFase)
                    {
                        var lineamiento = new LineamientoCalificacionFase
                        {
                            Orden = lineamientoDTO.Orden,
                            IdCriticidadCalificacion = lineamientoDTO.IdCriticidadCalificacion,
                            Nombre = lineamientoDTO.Nombre,
                            Descripcion = lineamientoDTO.Descripcion,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        criterioCalificacionFase.LineamientoCalificacionFase.Add(lineamiento);
                    }
                }

                // Insertar criterio con sus lineamientos
                var resultado = _repCriterioCalificacionFase.Add(criterioCalificacionFase);
                return resultado != null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }*/

        public CriterioCalificacionFase Add(CriterioCalificacionFase entidad)
        {
            try
            {
                var modelo = _unitOfWork.CriterioFaseRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CriterioCalificacionFase>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza un criterio de calificación de fase existente y sus lineamientos
        /// </summary>
        /// <param name="criterioCalificacionFaseDTO">Datos del criterio a actualizar</param>
        /// <param name="usuario">Usuario que realiza la operación</param>
        /// <returns>bool</returns>
        public CriterioCalificacionFase ActualizarCriterio(CriterioCalificacionFase entidad)
   {

            try
            {
                var modelo = _unitOfWork.CriterioFaseRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CriterioCalificacionFase>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*   public bool ActualizarCriterio(CriterioCalificacionFaseDTO criterioCalificacionFaseDTO, string usuario)
           {
               try
               {
                   if (criterioCalificacionFaseDTO.Id == null)
                       throw new ArgumentException("El ID del criterio no puede ser nulo");

                   // Obtener el criterio existente
                   CriterioCalificacionFase criterioCalificacionFase = _repCriterioCalificacionFase.ObtenerPorId(criterioCalificacionFaseDTO.Id.Value);
                   if (criterioCalificacionFase == null)
                       throw new ArgumentException($"No se encontró el criterio con ID {criterioCalificacionFaseDTO.Id}");

                   // Actualizar propiedades del criterio
                   criterioCalificacionFase.IdTransicionCalificacionFase = criterioCalificacionFaseDTO.IdTransicionCalificacionFase;
                   criterioCalificacionFase.Orden = criterioCalificacionFaseDTO.Orden;
                   criterioCalificacionFase.Nombre = criterioCalificacionFaseDTO.Nombre;
                   criterioCalificacionFase.Descripcion = criterioCalificacionFaseDTO.Descripcion;
                   criterioCalificacionFase.UsuarioModificacion = usuario;
                   criterioCalificacionFase.FechaModificacion = DateTime.Now;

                   // Primero eliminamos todos los lineamientos existentes (baja lógica)
                   var lineamientos = _repLineamientoCalificacionFase.GetBy(x => x.IdCriterioCalificacionFaseOportunidad == criterioCalificacionFase.Id && x.Estado == true);
                   foreach (var lineamiento in lineamientos)
                   {
                       _repLineamientoCalificacionFase.Delete(lineamiento.Id, usuario);
                   }

                   // Ahora agregamos los nuevos lineamientos
                   if (criterioCalificacionFaseDTO.LineamientoCalificacionFase != null && criterioCalificacionFaseDTO.LineamientoCalificacionFase.Count > 0)
                   {
                       criterioCalificacionFase.LineamientoCalificacionFase = new List<LineamientoCalificacionFase>();

                       foreach (var lineamientoDTO in criterioCalificacionFaseDTO.LineamientoCalificacionFase)
                       {
                           var lineamiento = new LineamientoCalificacionFase
                           {
                               IdCriterioCalificacionFaseOportunidad = criterioCalificacionFase.Id,
                               Orden = lineamientoDTO.Orden,
                               IdCriticidadCalificacion = lineamientoDTO.IdCriticidadCalificacion,
                               Nombre = lineamientoDTO.Nombre,
                               Descripcion = lineamientoDTO.Descripcion,
                               UsuarioCreacion = usuario,
                               UsuarioModificacion = usuario,
                               FechaCreacion = DateTime.Now,
                               FechaModificacion = DateTime.Now,
                               Estado = true
                           };
                           criterioCalificacionFase.LineamientoCalificacionFase.Add(lineamiento);
                       }
                   }

                   // Actualizar el criterio con sus nuevos lineamientos
                   var resultado = _repCriterioCalificacionFase.Update(criterioCalificacionFase);
                   return resultado != null;
               }
               catch (Exception ex)
               {
                   throw new Exception(ex.Message);
               }
           } */

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina de manera lógica un criterio de calificación de fase y sus lineamientos
        /// </summary>
        /// <param name="id">ID del criterio a eliminar</param>
        /// <param name="usuario">Usuario que realiza la operación</param>
        /// <returns>bool</returns>
        public bool EliminarCriterio(int id, string usuario)
        {
            try
            {
                // Eliminar detalles (lineamientos) primero
                _repCriterioCalificacionFase.EliminarDetalles(id);

                // Luego eliminar el criterio principal
                return _repCriterioCalificacionFase.Delete(id, usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los criterios de calificación de fase
        /// </summary>
        /// <returns>Lista de criterios</returns>
        public List<CriterioCalificacionFaseDTO> ObtenerCriteriosCalificacionFase()
        {
            try
            {
                return _repCriterioCalificacionFase.ObtenerCriteriosCalificacionFase();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los criterios asociados a una transición específica
        /// </summary>
        /// <param name="idTransicionCalificacionFase">ID de la transición</param>
        /// <returns>Lista de criterios</returns>
        public List<CriterioCalificacionFaseDTO> ObtenerCriteriosPorTransicion(int idTransicionCalificacionFase)
        {
            try
            {
                return _repCriterioCalificacionFase.ObtenerCriteriosPorTransicion(idTransicionCalificacionFase);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene un criterio de calificación de fase por su ID
        /// </summary>
        /// <param name="idCriterioCalificacionFase">ID del criterio</param>
        /// <returns>Detalles del criterio</returns>
        public CriterioCalificacionFase ObtenerCriterioCalificacionFasePorId(int idCriterioCalificacionFase)
        {
            try
            {
                return _unitOfWork.CriterioFaseRepository.ObtenerCriterioCalificacionFasePorId(idCriterioCalificacionFase);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los combos necesarios para los formularios del módulo
        /// </summary>
        /// <returns>Diccionario con los combos</returns>
        /*public async Task<Dictionary<string, List<ComboDTO>>> ObtenerCriticidad()
        {
            try
            {
                var combos = new Dictionary<string, List<ComboDTO>>();

                // Obtener lista de criticidades para los lineamientos
                var criticidades = _repCriticidadCalificacion.GetBy(x => x.Estado == true)
                    .Select(x => new ComboDTO { Id = x.Id, Nombre = x.NombreCriticidad })
                    .ToList();

                combos.Add("Criticidades", criticidades);

                return combos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }*/

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene un combo con todos los criterios de calificación de fase
        /// </summary>
        /// <returns>Lista para combo</returns>
        public IEnumerable<ComboDTO> ListaCriterios()
        {
            try
            {
                return _repCriterioCalificacionFase.ListaCriterios();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}