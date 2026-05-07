using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Routing.Trie;
using System;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CentroCostoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/06/2022
    /// <summary>
    /// Gestión general de T_CentroCosto
    /// </summary>
    public class CentroCostoService : ICentroCostoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CentroCostoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<CentroCostoDTO, CentroCosto>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCentroCosto, CentroCosto>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCentroCosto, CentroCostoDTO>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }

        /// Autor: Gretel Canasa
        /// Fecha: 25/04/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Centro Costo
        public CentroCostoDTO Insertar(CentroCostoDTO dto, string UserName)
        {
            try
            {
                if (dto != null)
                {
                    CentroCosto centroCosto = new();
                    centroCosto.IdArea = dto.IdArea;
                    centroCosto.IdSubArea = dto.IdSubArea;
                    centroCosto.IdPgeneral = dto.IdPgeneral;
                    centroCosto.Nombre = dto.Nombre;
                    centroCosto.Codigo = dto.Codigo;
                    centroCosto.IdAreaCc = dto.IdAreaCc;
                    centroCosto.Ismtotales = dto.Ismtotales;
                    centroCosto.Icpftotales = dto.Icpftotales;
                    centroCosto.Estado = true;
                    centroCosto.FechaCreacion = DateTime.Now;
                    centroCosto.FechaModificacion = DateTime.Now;
                    centroCosto.UsuarioCreacion = UserName;
                    centroCosto.UsuarioModificacion = UserName;

                    var respuesta = _unitOfWork.CentroCostoRepository.Add(centroCosto);
                    _unitOfWork.Commit();
                    return _mapper.Map<CentroCostoDTO>(respuesta);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Eliminar(int idCentroCosto, string usuario)
        {
            try
            {
                _unitOfWork.CentroCostoRepository.Delete(idCentroCosto, usuario);
                var pespecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(idCentroCosto);
                if (pespecifico != null && pespecifico.Id != 0)
                {
                    _unitOfWork.PEspecificoRepository.Delete(pespecifico.Id, usuario);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CentroCosto para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gretel Canasa
        /// Fecha: 12/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los combos
        /// </summary>
        /// <returns> List<ComboDTO> </returns> 
        public async Task<CentroCostoCombosPadreDTO> ObtenerCombosModulo()
        {
            try
            {
                var task_areaCc = _unitOfWork.AreaCentroCostoRepository.ObtenerAreaCCAsync();
                var task_subNivelCc = _unitOfWork.SubNivelCcRepository.ObtenerSubNivelCCAsync();
                var task_ciudad = _unitOfWork.TroncalPgeneralRepository.ObtenerTroncalCiudadAsync();
                var task_area = _unitOfWork.AreaRepository.ObtenerComboAsync();
                var task_subArea = _unitOfWork.SubAreaRepository.ObtenerSubAreaAsync();
                var task_pGeneral = _unitOfWork.TroncalPgeneralRepository.ObtenerTroncalPGeneral();

                CentroCostoCombosPadreDTO resultado = new CentroCostoCombosPadreDTO()
                {
                    AreaCc = await task_areaCc,
                    SubNivelCc = await task_subNivelCc,
                    Ciudad = await task_ciudad,
                    //TroncalCiudad = await task_troncalCiudad,
                    Area = await task_area,
                    SubArea = await task_subArea,
                    PGeneral = await task_pGeneral
                };
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombos(): {ex.Message}");
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 07/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de Centros de Costo basado en un Nombre Parcial.
        /// </summary>
        /// <param name="valor">Nombre Parcial de Centro de Costo</param>
        /// <returns> List<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerSubNivelCCPorAreaCCAsync(int idAreaCC)
        {
            return await _unitOfWork.SubNivelCcRepository.ObtenerSubNivelCCPorAreaCCAsync(idAreaCC);
        }
        /// Autor: Gretel Canasa
        /// Fecha: 07/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de Centros de Costo basado en un Nombre Parcial.
        /// </summary>
        /// <param name="valor">Nombre Parcial de Centro de Costo</param>
        /// <returns> List<ComboDTO> </returns>
        public async Task<IEnumerable<TroncalPGeneralSubAreaCodigoDTO>> ObtenerPGeneralPorIdSubAreaAsync(int idSubArea)
        {
            return await _unitOfWork.TroncalPgeneralRepository.ObtenerPGeneralPorIdSubArea(idSubArea);
        }
        /// Autor: Gretel Canasa
        /// Fecha: 07/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de Centros de Costo basado en un Nombre Parcial.
        /// </summary>
        /// <param name="valor">Nombre Parcial de Centro de Costo</param>
        /// <returns> List<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerSubAreaPorIdAreaAsync(int idArea)
        {
            return await _unitOfWork.SubAreaRepository.ObtenerSubAreaPorIdAreaAsync(idArea);
        }

        public IEnumerable<ComboDTO> ObtenerRecientesAutocomplete(string nombreParcial)
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.ObtenerRecientesAutocomplete(nombreParcial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de Centros de Costo basado en un Nombre Parcial.
        /// </summary>
        /// <param name="valor">Nombre Parcial de Centro de Costo</param>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO>ObtenerFiltroAutocomplete(string valor)
        {
            return _unitOfWork.CentroCostoRepository.ObtenerFiltroAutocomplete(valor);
        }

        /// Autor: Flavio R. Mamani Fabian.
        /// Fecha: 14/03/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de Centros de Costo basado en un Nombre Parcial.
        /// </summary>
        /// <param name="valor">Nombre parcial de centro de costo</param>
        /// <param name="usuario">Tipo programa carrera</param>
        /// <returns> Lista ComboDTO de Centro Costos </returns>
        public IEnumerable<ComboDTO> ObtenerAutocompleteV2(string valor, string usuario)
        {
            if (usuario == "AdminInst")
            {
                return _unitOfWork.CentroCostoRepository.ObtenerAutocompletePorTipoProgramaCarrera(valor, 2);
            }
            else
            {
                return _unitOfWork.CentroCostoRepository.ObtenerAutocompletePorTipoProgramaCarrera(valor, null);
            }
        }
        public IEnumerable<ComboDTO> ObtenerAutocompleteV3(string valor, string usuario)
        {
            if (usuario == "AdminInst")
            {
                return _unitOfWork.CentroCostoRepository.ObtenerAutocompletePorTipoProgramaCarreraV3(valor, 2);
            }
            else
            {
                return _unitOfWork.CentroCostoRepository.ObtenerAutocompletePorTipoProgramaCarreraV3(valor, null);
            }
        }
        /// Autor: Daniel Huaita
        /// Fecha: 16/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de Centros de Costo basado en un Nombre Parcial que estan en estado lanzamiento y ejecucion.
        /// </summary>
        /// <param name="nombreParcial">Nombre Parcial de Centro de Costo</param>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerAutocompleteCentroCosto(string nombreParcial)
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.ObtenerAutocompleteCentroCosto(nombreParcial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener información de centro de Costo AutoComplete
        /// </summary>
        /// <param name="nombreParcial">Nombre Parcial de Centro de Costo</param>
        /// <returns> List<ComboDTO> </returns>		
        public IEnumerable<ComboDTO> ObtenerAutocompleteConPGeneral(string nombreParcial)
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.ObtenerAutocompleteConPGeneral(nombreParcial);
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
        /// Obtiene Valor para etiquetas po centro costo
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <returns> PlantillaCentroCostoDTO </returns>
        public PlantillaCentroCostoDTO ObtenerCentroCostoParaPEspecifico(int idCentroCosto)
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.ObtenerCentroCostoParaPEspecifico(idCentroCosto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 22/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de centro de costos para filtro por Asesores
        /// </summary>
        /// <param name="listaAsesores">Ids del, o de los, asesor(es)</param>
        /// <returns> Lista objeto DTO : List<ComboDTO> </returns>
        public List<ComboDTO> ObtenerCentroCostoPorAsesores(List<int> listaAsesores)
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.ObtenerCentroCostoPorAsesores(listaAsesores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 13/10/2022
        /// Version: 1.0
        /// <summary>
		/// Obtiene Valor para etiquetas po centro costo
		/// </summary>
		/// <param name="idCentroCosto"></param>
		/// <returns></returns>
        public PlantillaCentroCostoDTO ObtenerCentroCostoParaPlantillaWhatsApp(int idCentroCosto)
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.ObtenerCentroCostoParaPlantillaWhatsApp(idCentroCosto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<object> ObtenerCentroCostoAutoComplete(string Valor)
        {
            try
            {

                return _unitOfWork.CentroCostoRepository.
                    GetBy(x => x.Estado == true && x.Nombre.Contains(Valor), x => new { x.Id, x.Nombre }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
		/// Fecha: 04/11/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene centro de costo padre y centro de costo individual
		/// </summary>
		/// <param></param>
		/// <returns>List<CentroCostoPadreCentroCostoIndividualDTO></returns>
        public List<CentroCostoPadreCentroCostoIndividualDTO> ObtenerCentroCostoPadreCentroCostoIndividual()
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.ObtenerCentroCostoPadreCentroCostoIndividual();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 11/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del centro de costo por IdProgramaEspecifico
        /// </summary>
        /// <param name="idPEspecifico"> Id de  PEspecifico </param>
        /// <returns> List<DatosCentroCostoDTO> </returns>
        public List<DatosCentroCostoDTO> ObtenerDatosCentroCostos(int idPEspecifico)
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.ObtenerDatosCentroCostos(idPEspecifico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gretel Canasa
        /// Fecha: 04/17/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del centro de costo 
        /// </summary>
        /// <param name="idPEspecifico"> Id de  PEspecifico </param>
        /// <returns> List<DatosCentroCostoDTO> </returns>

        public List<CentroCostoDTO> Obtener()
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.Obtener();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gretel Canasa
        /// Fecha: 04/17/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del centro de costo 
        /// </summary>
        /// <param name="idPEspecifico"> Id de  PEspecifico </param>
        /// <returns> List<DatosCentroCostoDTO> </returns>

        public List<CentroCostoUsuariosDTO> ObtenerCcDatosUsuarios()
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.ObtenerCcDatosUsuarios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 04/17/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del centro de costo 
        /// </summary>
        /// <param name="idPEspecifico"> Id de  PEspecifico </param>
        /// <returns> List<DatosCentroCostoDTO> </returns>

        public CentroCostoMasAdicionalesDTO ObtenerMasAdicionales(int id)
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.ObtenerMasAdicionales(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huamanc
        /// Fecha: 09/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del centro de costo por IdProgramaEspecifico y los documnetos segun el tipo
        /// </summary>
        /// <param name="idPEspecifico"> Id de  PEspecifico </param>
        /// <returns> List<DatosCentroCostoDTO> </returns>
        public object ObtenerDatosDelCentrodeCosto(int idPEspecifico)
        {
            try
            {
                var _repCriterioDoc = _unitOfWork.CriterioDocRepository;
                var _repPEspecifico = _unitOfWork.PEspecificoRepository;
                var listaRpta = _unitOfWork.CentroCostoRepository.ObtenerDatosCentroCostos(idPEspecifico); ;

                var modalidad = _repPEspecifico.GetBy(x => x.Id == idPEspecifico, x => new { x.Tipo }).FirstOrDefault();

                List<CriterioDocDTO> listaDocumentos = new List<CriterioDocDTO>();

                if (modalidad.Tipo == "Presencial")
                {
                    var tempData = _repCriterioDoc.GetBy(x => x.ModalidadPresencial == true, x => new { IdCriterioDocs = x.Id, NombreDocumento = x.Nombre });
                    foreach (var item in tempData)
                    {
                        var temp = new CriterioDocDTO()
                        {
                            IdCriterioDocs = item.IdCriterioDocs,
                            NombreDocumento = item.NombreDocumento
                        };
                        listaDocumentos.Add(temp);
                    }
                }
                if (modalidad.Tipo == "Online Asincronica")
                {
                    var tempData = _repCriterioDoc.GetBy(x => x.ModalidadAonline == true, x => new { IdCriterioDocs = x.Id, NombreDocumento = x.Nombre });
                    foreach (var item in tempData)
                    {
                        var temp = new CriterioDocDTO()
                        {
                            IdCriterioDocs = item.IdCriterioDocs,
                            NombreDocumento = item.NombreDocumento
                        };
                        listaDocumentos.Add(temp);
                    }
                }
                if (modalidad.Tipo == "Online Sincronica")
                {
                    var tempData = _repCriterioDoc.GetBy(x => x.ModalidadOnline == true, x => new { IdCriterioDocs = x.Id, NombreDocumento = x.Nombre });
                    foreach (var item in tempData)
                    {
                        var temp = new CriterioDocDTO()
                        {
                            IdCriterioDocs = item.IdCriterioDocs,
                            NombreDocumento = item.NombreDocumento
                        };
                        listaDocumentos.Add(temp);
                    }
                }
                var listadoDocumentos = listaDocumentos;
                var listaCursos = _repPEspecifico.ObtenerCursosCentroCosto(idPEspecifico);

                return new { listaRpta, listaDocumentos, listaCursos };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Determina si una Plantilla Existe basado en su identificador
        /// </summary>
        /// <param name="idPlantilla">Id de la Plantilla</param>
        /// <returns> bool </returns>
        public bool ExistePorId(int idPlantilla)
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.Exist(idPlantilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Margiory Ramirez.
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Lista  de Centro Costo
        /// </summary>
        /// <param name="nombreCentroCosto"></param>
        /// <returns> bool </returns>
        public List<CentroCostoPEspecificoDTO> ObtenerListaCentrosCostoPorNombre(string nombreCentroCosto)
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.ObtenerListaCentrosCostoPorNombre(nombreCentroCosto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica Centro Costo
        /// </summary>
        /// <param name="dto">Centro de costo</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>centrodeCostoDTO</returns>
        public CentroCostoDTO Actualizar(CentroCostoDTO dto, string UserName)
        {
            try
            {
                CentroCosto entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.CentroCostoRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.IdArea = dto.IdArea;
                            entidad.IdSubArea = dto.IdSubArea;
                            entidad.IdPgeneral = dto.IdPgeneral;
                            entidad.Nombre = dto.Nombre;
                            entidad.Codigo = dto.Codigo;
                            entidad.IdAreaCc = dto.IdAreaCc;
                            entidad.FechaModificacion = DateTime.Now;
                            entidad.UsuarioModificacion = UserName;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");
                var respuesta = _unitOfWork.CentroCostoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CentroCostoDTO>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Max Mantilla
        /// Fecha: 20/07/2023
        /// Version: 1.0
        /// <summary>
		/// Obtiene lista de centro de costos padre
		/// </summary>
		/// <returns></returns>
		public List<CentroCostoProgramaEspecificoFiltroDTO> ObtenerCentroCostoPadres(int? tipo)
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.ObtenerCentroCostoPadres(tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 20/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de centro de costos 
        /// </summary>
        /// <returns></returns>
         public List<CentroCostoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoComplete(string valor)
         {
            try
            {
                return _unitOfWork.CentroCostoRepository.ObtenerTodoFiltroAutoComplete(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 20/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de centro de costos 
        /// </summary>
        /// <returns></returns>
        public List<CentroCostoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoCompleteInstituto(string valor)
        {
            try
            {
                return _unitOfWork.CentroCostoRepository.ObtenerTodoFiltroAutoCompleteInstituto(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
