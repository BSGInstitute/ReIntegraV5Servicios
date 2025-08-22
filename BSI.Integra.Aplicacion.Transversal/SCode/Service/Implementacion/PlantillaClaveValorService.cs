using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PlantillaClaveValorService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/06/2022
    /// <summary>
    /// Gestión general de T_PlantillaClaveValor
    /// </summary>
    public class PlantillaClaveValorService : IPlantillaClaveValorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PlantillaClaveValorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPlantillaClaveValor, PlantillaClaveValor>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public PlantillaClaveValor Add(PlantillaClaveValor entidad)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaClaveValorRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PlantillaClaveValor>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PlantillaClaveValor Update(PlantillaClaveValor entidad)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaClaveValorRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PlantillaClaveValor>(modelo);
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
                _unitOfWork.PlantillaClaveValorRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlantillaClaveValor> Add(List<PlantillaClaveValor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaClaveValorRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PlantillaClaveValor>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlantillaClaveValor> Update(List<PlantillaClaveValor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaClaveValorRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PlantillaClaveValor>>(modelo);
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
                _unitOfWork.PlantillaClaveValorRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 13/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PlantillaClaveValor
        /// </summary>
        /// <returns> List<PlantillaClaveValorDTO> </returns>
        public IEnumerable<PlantillaClaveValorDTO> ObtenerPlantillaClaveValor()
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerPlantillaClaveValor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PlantillaClaveValor para mostrarse en combo.
        /// </summary>
        /// <returns> List<PlantillaClaveValorComboDTO> </returns>
        public IEnumerable<PlantillaClaveValorComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el valor de plantillas relacionadas a una Plantilla Base en especifico.
        /// </summary>
        /// <param name="nombrePlantillaBase">Nombre de Plantilla Base</param>
        /// <returns> List<PlantillaValorDTO> </returns>
        public IEnumerable<PlantillaValorDTO> ObtenerPlantillaPorNombrePlantillaBase(string nombrePlantillaBase)
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerPlantillaPorNombrePlantillaBase(nombrePlantillaBase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el contenido de plantillas con ciertos filtros por defecto.
        /// </summary>
        /// <returns> List<PlantillaMailingAgendaDTO> </returns>
        public IEnumerable<PlantillaMailingAgendaDTO> ObtenerPlantillasMailing()
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerPlantillasMailing();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Valor de las Plantillas relacionadas a una Fase Oportunidad
        /// </summary>
        /// <param name="idFaseOportunidad">Id de la Fase Oportunidad</param>
        /// <returns> List<PlantillaClaveValorAreaEtiquetaDTO> </returns>
        public IEnumerable<PlantillaClaveValorAreaEtiquetaDTO> ObtenerPlantillasPorIdFaseOportunidad(int idFaseOportunidad)
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerPlantillasPorIdFaseOportunidad(idFaseOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos principales de Plantillas asociadas a WhatsApp para la Agenda.
        /// </summary>
        /// <returns> List<PlantillaMailingAgendaDTO> </returns>
        public IEnumerable<PlantillaWhatsAppAgendaDTO> ObtenerPlantillaWhatsAppAgenda()
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerPlantillaWhatsAppAgenda();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos principales de Plantillas asociadas a WhatsApp para la Agenda.
        /// </summary>
        /// <returns> List<PlantillaMailingAgendaDTO> </returns>
        public IEnumerable<PlantillaWhatsAppAgendaDTO> ObtenerPlantillaWhatsAppAgendaComercial()
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerPlantillaWhatsAppAgendaComercial();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Problema y Causa asociados a una Oportunidad.
        /// </summary>
        /// <returns> List<ProblemaCausaDTO> </returns>
        public IEnumerable<ProblemaCausaDTO> ObtenerCausaProblemaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerCausaProblemaPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los URL de Programas relacionados a un Centro de Costo.
        /// </summary>
        /// <returns> List<PGeneralCursoRelacionadoDTO> </returns>
        public IEnumerable<PGeneralCursoRelacionadoDTO> ObtenerCursosRelacionadosPorIdCentroCosto(int idCentroCosto)
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerCursosRelacionadosPorIdCentroCosto(idCentroCosto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las plantillas disponibles por fase
        /// </summary>
        /// <param name="idFaseOportunidad"> Id de Fase de Oportunidad</param>
        /// <returns>List<ComboDTO></returns>
        public IEnumerable<FiltroDTO> ObtenerPlantillaGenerarMensaje(int idFaseOportunidad)
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerPlantillaGenerarMensaje(idFaseOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el monto de los cursos relacionados
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="idEtiqueta">Id de la lista curso area(PK de la tabla mkt.T_ListaCursoAreaEtiqueta)</param>
        /// <returns>List<CursosRelacionadosDTO></returns>
        public List<CursosRelacionadosDTO> ObtenerMontosCursosRelacionados(int idOportunidad, int idEtiqueta)
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerMontosCursosRelacionados(idOportunidad, idEtiqueta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 31/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_PlantillaClaveValor relacionado al identificador.
        /// </summary>
        /// <param name="idPlantilla">Id del T_PlantillaClaveValor</param>
        /// <returns> PlantillaClaveValor </returns>
        public IEnumerable<PlantillaClaveValor> ObtenerPorIdPlantilla(int idPlantilla)
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerPorIdPlantilla(idPlantilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 12/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una plantlilla por cada plantilla
        /// </summary>
        /// <param name="nombrePlantillaBase"></param>
        /// <returns></returns>
        public List<PlantillaValorDTO> ObtenerPlantillaPorPlantillaBase(string nombrePlantillaBase)
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerPlantillaPorPlantillaBase(nombrePlantillaBase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Todo Plantilla Mailing
        /// </summary>
        /// <returns></returns>
        public List<ContenidoPlantillaDTO> ObtenerTodoPlantillasMailing()
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerTodoPlantillasMailing();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene plantilla de whatsapp operaciones
        /// </summary>
        /// <returns> List<PlantillaWhatsAppDTO>  </returns>
        public List<PlantillaWhatsAppDTO> ObtenterPlantillaWhatsAppOperaciones()
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenterPlantillaWhatsAppOperaciones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las plantillas mailing disponibles para operaciones
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerPlantillaGenerarMensajeOperaciones()
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerPlantillaGenerarMensajeOperaciones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ContenidoPlantillaDTO> ObtenerTodoPlantillaPorPersonalAreaTrabajo(int idPersonalAreaTrabajo)
        {
            try
            {
                PlantillaClaveValorService plantillaClaveValorService = new PlantillaClaveValorService(_unitOfWork);
                var listaPlantillasDisponibles = plantillaClaveValorService.ObtenerTodoPlantillasMailing();
                if (idPersonalAreaTrabajo == 3) //ValorEstatico.IdPersonalAreaTrabajoOperaciones
                {
                    listaPlantillasDisponibles.AddRange(plantillaClaveValorService.ObtenerPlantillaGenerarMensajeOperaciones().Select(
                            x => new ContenidoPlantillaDTO
                            {
                                Id = x.Id,
                                Nombre = x.Nombre,
                                Clave = "",
                                IdAreaEtiqueta = 0,
                                IdPlantillaClaveValor = 0,
                                Valor = ""
                            }
                        ));
                }
                return listaPlantillasDisponibles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Daniel Huaita Carpio
        /// Fecha: 03/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las plantillas mailing disponibles para la agenda de operaciones
        /// </summary>
        /// <returns></returns>
        public List<ContenidoPlantillaDTO> ObtenerPlantillasModuloAgenda()
        {
             try
            {
                var listaPlantillasDisponible = _unitOfWork.PlantillaClaveValorRepository.ObtenerPlantillasModuloAgenda().Select(
                            x => new ContenidoPlantillaDTO
                            {
                                Id = x.Id,
                                Nombre = x.Nombre,
                                Clave = "",
                                IdAreaEtiqueta = 0,
                                IdPlantillaClaveValor = 0,
                                Valor = ""
                            }
                        ).ToList();

                return listaPlantillasDisponible;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<PlantillaClaveValor> ObtenerPorIdPlantillaTodos(int idPlantilla)
        {
            try
            {
                return _unitOfWork.PlantillaClaveValorRepository.ObtenerPorIdPlantillaTodos(idPlantilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<PlantillaAsociacionModuloSistema> ObtenerPlantillaAsociacionModuloSistemaPorIdPlantilla(int idPlantilla)
        {
            try
            {
                return _unitOfWork.PlantillaAsociacionModuloSistemaRepository.ObtenerPlantillaAsociacionModuloSistemaPorIdPlantilla(idPlantilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
