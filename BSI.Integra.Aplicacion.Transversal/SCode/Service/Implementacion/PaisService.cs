using AutoMapper;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PaisService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_Pais
    /// </summary>
    public class PaisService : IPaisService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PaisService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPai, Pais>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPai, PaisDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<Pais, PaisDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Pais
        /// </summary>
        /// <returns> List<PaisDTO> </returns>
        public IEnumerable<PaisDTO> ObtenerPais()
        {
            try
            {
                return _unitOfWork.PaisRepository.ObtenerPais();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Pais para mostrarse en combo.
        /// </summary>
        /// <returns> List<PaisComboDTO> </returns>
        public IEnumerable<PaisComboDTO> ObtenerPaisCombo()
        {
            try
            {
                return _unitOfWork.PaisRepository.ObtenerPaisCombo();
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
        /// Obtiene los datos basicos de los paises junto con su Zona Horaria.
        /// </summary>
        /// <returns> List<PaisZonaHorariaDTO> </returns>
        public IEnumerable<PaisZonaHorariaDTO> ObtenerPaisZonaHoraria()
        {
            try
            {
                return _unitOfWork.PaisRepository.ObtenerPaisZonaHoraria();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene url de ruta Bandera
        /// </summary>
        /// <param name="idPais">Id del Pais</param>
        /// <returns> ValorStringDTO </returns>
        public UrlBlockStoragePais ObtenerRutaUrlBandera()
        {
            try
            {
                UrlBlockStoragePais ruta = new UrlBlockStoragePais();
                ruta.RutaCompleta = "https://repositorioweb.blob.core.windows.net/repositorioweb/flags/";
                ruta.RutaBlob = "repositorioweb/flags/";
                return ruta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene url de ruta Icono
        /// </summary>
        /// <param name="idPais">Id del Pais</param>
        /// <returns> ValorStringDTO </returns>
        public UrlBlockStoragePais ObtenerRutaUrlIcono()
        {
            try
            {
                UrlBlockStoragePais ruta = new UrlBlockStoragePais();
                ruta.RutaCompleta = "https://repositorioweb.blob.core.windows.net/repositorioweb/FlagIcons/";
                ruta.RutaBlob = "repositorioweb/FlagIcons/";
                return ruta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra. 
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Regiatro Pais Ruta Bandera y Ruta Icono
        /// </summary>
        /// <param name="idPais">Id del Pais</param>
        /// <returns> ValorStringDTO </returns>
        public PaisDTO RegistrarPais(RegistroPaisDTO registroPais, string usuario)
        {
            try
            {
                IRegistroArchivoStorageService registroArchivoStorageService = new RegistroArchivoStorageService(_unitOfWork);

                string? rutaBandera = null;
                string? rutaIcono = null;

                if (registroPais.Bandera != null)
                {
                    rutaBandera = registroArchivoStorageService.SubirArchivo(
                        registroPais.Bandera.ConvertToByte(),
                        registroPais.Bandera.ContentType,
                        registroPais.Bandera.FileName,
                        registroPais.RutaCompletaBandera,
                        registroPais.RutaBlobBandera);
                }
                if (registroPais.Icono != null)
                {
                    rutaIcono = registroArchivoStorageService.SubirArchivo(
                        registroPais.Icono.ConvertToByte(),
                        registroPais.Icono.ContentType,
                        registroPais.Icono.FileName,
                        registroPais.RutaCompletaIcono,
                        registroPais.RutaBlobIcono);
                }

                Pais pais = new Pais();
                pais.Id = registroPais.Id;
                pais.CodigoPais = registroPais.CodigoPais;
                pais.CodigoIso = registroPais.CodigoIso;
                pais.NombrePais = registroPais.NombrePais;
                pais.Moneda = registroPais.Moneda;
                pais.ZonaHoraria = registroPais.ZonaHoraria;
                pais.EstadoPublicacion = registroPais.EstadoPublicacion;
                pais.CodigoGoogleId = null;
                pais.CodigoPaisMoodle = null;
                pais.RutaIcono = rutaIcono;
                pais.RutaBandera = rutaBandera;

                if (pais.Id != 0)
                {
                    var paisTemp = _unitOfWork.PaisRepository.ObtenerPorId(pais.Id)!;
                    if (pais.RutaIcono == null || pais.RutaIcono == "")
                    {
                        pais.RutaIcono = paisTemp.RutaIcono;
                    }
                    if (pais.RutaBandera == null || pais.RutaBandera == "")
                    {
                        pais.RutaBandera = paisTemp.RutaBandera;
                    }
                    pais.CodigoGoogleId = paisTemp.CodigoGoogleId;
                    pais.CodigoPaisMoodle = paisTemp.CodigoPaisMoodle;
                    pais.Estado = true;
                    pais.UsuarioCreacion = pais.UsuarioCreacion;
                    pais.UsuarioModificacion = usuario;
                    pais.FechaCreacion = pais.FechaCreacion;
                    pais.FechaModificacion = DateTime.Now;
                    _unitOfWork.PaisRepository.Update(pais);
                    _unitOfWork.Commit();
                }
                else
                {
                    pais.Estado = true;
                    pais.UsuarioCreacion = usuario;
                    pais.UsuarioModificacion = usuario;
                    pais.FechaCreacion = DateTime.Now;
                    pais.FechaModificacion = DateTime.Now;
                    var res = _unitOfWork.PaisRepository.Add(pais);
                    _unitOfWork.Commit();
                    pais.Id = res.Id;
                }
                return _mapper.Map<PaisDTO>(pais);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/08/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina un registro por id
        /// </summary>
        /// <returns> List<PaisDTO> </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var res = _unitOfWork.PaisRepository.Delete(id, usuario);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Pais para Combo
        /// </summary>
        /// <returns> List<PaisDTO> </returns>PaisRepository
        public IEnumerable<PaisMonedaComboDTO> ObtenerComboConMoneda()
        {
            try
            {
                return _unitOfWork.PaisRepository.ObtenerComboConMoneda();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Pais para Obtener todo Filtro.
        /// </summary>
        /// <returns> List<PaisDTO> </returns>PaisRepository
        public IEnumerable<PaisZonaHorariaComboDTO> ObtenerComboConZonaHoraria()
        {
            try
            {
                return _unitOfWork.PaisRepository.ObtenerComboConZonaHoraria();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor:Junior Llerena
        /// Fecha: 16/07/2025
        /// <summary>
        /// Obtiene todos los paises con estad 1 y estado de visualizacion 1
        /// </summary>
        /// <returns>Lista de objetos de tipo <PaisZonaHorariaComboDTO></returns>
        public IEnumerable<PaisZonaHorariaComboDTO> ObtenerComboZonaHorarioActivo()
        {
            try
            {
                return _unitOfWork.PaisRepository.ObtenerComboZonaHorarioActivo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 07/11/2022
        /// <summary>
        /// Obtiene lista de paises para lista desplagable
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.PaisRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los codigo pais con estado 1
        /// </summary>
        /// <returns>Lista de codigo de paises</returns>
        public List<int> ObtenerTodoCodigoPais()
        {
            try
            {
                return _unitOfWork.PaisRepository.ObtenerTodoCodigoPais();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Eliot Arias Flores
        /// Fecha: 21/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los paises
        /// </summary>
        /// <returns>Lista de paises</returns>
        public IEnumerable<ComboDTO> ObtenerListaPais()
        {
            try
            {
                return _unitOfWork.PaisRepository.ObtenerListaPais();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Victor Hinojosa
        /// Fecha: 26/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Pais
        /// </summary>
        /// <returns> List<PaisDTO> </returns>
        public IEnumerable<PaisDTO> ObtenerPaisConEstadoVisualizacion()
        {
            try
            {
                return _unitOfWork.PaisRepository.ObtenerPais().ToList().Where(x => x.EstadoVisualizacion == 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

