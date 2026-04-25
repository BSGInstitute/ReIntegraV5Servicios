using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using CsvHelper;
using CsvHelper.Configuration;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Transactions;
using System.Text;
using System.Security.Cryptography;
using BSI.Integra.Aplicacion.Base.BO;
using System.Net.Mail;
using DocumentFormat.OpenXml.Office2010.Excel;
using BSI.Integra.Aplicacion.Transversal.SCode.Service.Implementacion;
using Google.Rpc;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PersonalService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 08/06/2022
    /// <summary>
    /// Gestión general de T_Personal
    /// </summary>
    public class PersonalService : IPersonalService
    {
        private IUnitOfWork _unitOfWork;
        private IPersonaService _personaService;


        private Mapper _mapper;

        public PersonalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _personaService = new PersonaService(unitOfWork);


            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<TPersonal, Personal>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalArchivoDTO, PersonalArchivo>(MemberList.None).ReverseMap();
            });

            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Personal Add(Personal entidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonalRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Personal>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Personal Update(Personal entidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonalRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Personal>(modelo);
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
                _unitOfWork.PersonalRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Personal> Add(List<Personal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonalRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Personal>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Personal> Update(List<Personal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonalRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Personal>>(modelo);
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
                _unitOfWork.PersonalRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Personal para mostrarse en combo.
        /// </summary>
        /// <returns> List<PersonalComboDTO> </returns>
        public IEnumerable<PersonalComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Personal
        /// </summary>
        /// <returns> List<PersonalDTO> </returns>
        public IEnumerable<PersonalDTO> ObtenerPersonal()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Junior Llerena
        /// Fecha: 16/07/25
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las areas de trabajo del personal
        /// </summary>
        /// <returns> List<PersonalDTO> </returns>
        public IEnumerable<PersonalComboAreaDTO> ObtenerPersonaAreaTrabajo()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalAreaTrabajo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el primer nombre y apellido paterno por nombre de usuario
        /// </summary>
        /// <returns> List<PersonalDTO> </returns>
        public StringDTO ObtenerPrimerNombreApellidoPaternoPorUserName(string usuario)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPrimerNombreApellidoPaternoPorUserName(usuario);
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
        /// Determina si existe algun registro de T_Personal asociado al correo
        /// </summary>
        /// <returns> BoolDTO </returns>
        public BoolDTO ExistePersonalPorCorreo(string email)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ExistePersonalPorCorreo(email);
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
        /// Obtiene el registro de T_Personal asociado al identificador.
        /// </summary>
        /// <param name="idPersonal">Id del asesor</param>
        /// <returns> List<PersonalDTO> </returns>
        public Personal ObtenerPorId(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPorId(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 24/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id, Nombre y Apellido a trav�s del email del Asesor.
        /// </summary>
        /// <param name="email"></param>
        /// <returns> Información de nombres, apellidos por email de Asesor : PersonalInformacionCorreoDTO </returns>
        public PersonalInformacionCorreoDTO ObtenerNombreApellido(string email)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerNombreApellido(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<PersonalAsignadoDTO> ObtenerPersonalAsignado(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalAsignado(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el personal por el Id
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns>Personal</returns>
        public Personal ObtenerPersonalPorId(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalPorId(idPersonal);
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
        /// Obtiene todos los asesores sin ningun  tipo de restriccion
        /// </summary>
        /// <returns> Lista de Objeto DTO : List<ReportePersonalDTO> </returns>
        public List<ReportePersonalDTO> ObtenerAsesoresVentasOficial()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerAsesoresVentasOficial();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Carlos Crispin.
        /// Fecha: 17/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los asesores sin ningun  tipo de restriccion para el reporte de ingresos
        /// </summary>
        /// <returns> Lista de Objeto DTO : List<ReportePersonalDTO> </returns>
        public List<ReportePersonalDTO> ObtenerAsesoresVentasOficialRI(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerAsesoresVentasOficialRI(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Victor Hinojosa.
        /// Fecha: 18/09/2024
        /// Version: 1.0
        /// Obtiene los asesores filtrando por id solo asignados subordinador para reporte Cambio de fase
        /// <param name="idPersonal"></param>
        /// <returns></returns>

        public List<ReportePersonalDTO> ObtenerAsesoresVentasOficial_CF(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerAsesoresVentasOficial_CF(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Victor Hinojosa.
        /// Fecha: 18/09/2024
        /// Version: 1.0
        /// Obtiene los asesores filtrando por id solo asignados subordinador para reporte Contactabilidad
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<ReportePersonalDTO> ObtenerAsesoresVentasOficial_CONT(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerAsesoresVentasOficial_CONT(idPersonal);
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
        /// Obtiene todos los asesores sin ningun  tipo de restriccion
        /// </summary>
        /// <returns> Lista de Objeto DTO : List<ReportePersonalDTO> </returns>
        public (List<ReportePersonalDTO> asesores, List<ReportePersonalDTO> coordinadores) ObtenerAsesorCoordinadorVentasCombo()
        {
            try
            {
                var asesores = _unitOfWork.PersonalRepository.ObtenerAsesoresVentasOficial();
                var coordinadores = _unitOfWork.PersonalRepository.ObtenerCoordinadoresVentasOficial();
                return (asesores, coordinadores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 22/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registro de Personal para T_TInteraccionChatIntegra
        /// </summary>
        /// <returns> Lista de Objeto DTO : List<PersonalAutocompleteDTO> </returns>

        public IEnumerable<PersonalAutocompleteDTO> CargarPersonalParaFiltro()
        {
            try
            {
                return _unitOfWork.PersonalRepository.CargarPersonalParaFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 27/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Todos los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns>List<PersonalAsignadoDTO></returns>
        public List<PersonalAsignadoDTO> PersonalAsignadoOperacionesTotal(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalAsignadoOperacionesTotal(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los asesores de ventas para el reporte de seguimiento por el tipoPersonal=coordinador
        /// </summary>
        /// <returns>Lista de objetos de clase PersonalAsignadoDTO</returns>
        public List<PersonalAsignadoDTO> AsesoresVentasOficialReporteSeguimiento()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerAsesoresVentasOficialReporteSeguimiento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns>Lista de ObjetosDTO: List(PersonalAsignadoDTO)</returns>
        public List<PersonalAsignadoDTO> PersonalAsignadoVentas(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalAsignadoVentas(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de Personal (activos) del Área de Ventas
        /// </summary>
        /// <returns>Lista de ObjetosDTO: List(AsesorFiltroDTO)</returns>
        public List<AsesorFiltroDTO> ObtenerPersonalAsesoresFiltro()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalAsesoresFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ;
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 04/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de Coordinadores (activos) del Área de Ventas
        /// </summary>
        /// <returns>Lista de ObjetosDTO: List(CoordinadorFiltroDTO)</returns>
        public List<CoordinadorFiltroDTO> ObtenerPersonalCoordinadoresFiltro()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalCoordinadoresFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del personal
        /// </summary>
        /// <param name="idAsesor"> Id de Personal </param>
        /// <returns> PersonalMinReasignacionDTO </returns>
        public PersonalMinReasignacionDTO ObtenerPersonalReasignacion(int idAsesor)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalReasignacion(idAsesor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todo el personal de tipo coordinador 
        /// </summary>
        /// <returns> List<ReportePersonalDTO> </returns>
        public List<ReportePersonalDTO> ObtenerCoordinadoresVentasOficial()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerCoordinadoresVentasOficial();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Carlos Crispin.
        /// Fecha: 17/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todo el personal de tipo coordinador para el reporte de ingresos por asesor 
        /// </summary>
        /// <returns> List<ReportePersonalDTO> </returns>
        public List<ReportePersonalDTO> ObtenerCoordinadoresVentasOficialRI(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerCoordinadoresVentasOficialRI(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion necesaria del Personal para la  Agenda.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns>retorna un objeto tipo PersonalDatosAgendaDTO</returns>
        public PersonalDatosAgendaDTO ObtenerDatosPersonalAgenda(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerDatosPersonalAgenda(idPersonal);
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
        /// Obtiene el personal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DatoCompletoPersonalDTO ObtenerDatoPersonal(int id)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerDatoPersonal(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Personal ObtenerListaPersonalPorEmail(string email, int id)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerListaPersonalPorEmail(email, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int? ObtenerPersonalEliminadoEmailRepetido(string email)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalEliminadoEmailRepetido(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Margiory Ramirez Neyra.
        /// Fecha: 09/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registro  del Personal para GrupoFiltro.
        /// </summary>
        /// <param name="idGrupo"></param>
        /// <returns>retorna un objeto tipo PersonalDatosAgendaDTO</returns>
        public List<DatosPersonalAsesorPorGrupoIdDTO> ObtenerAsesoresPorGrupoId(int idGrupo)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerAsesoresPorGrupoId(idGrupo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el horario de trabajo del personal
        /// </summary>
        /// <param name="Id">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <returns>Obtener el horario de trabajo de un personal en formato HTML</returns>
        public string ObtenerHorarioTrabajo(int id)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerHorarioTrabajo(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la firma del personal en imagen
        /// </summary>
        /// <param name="idCodigoPais">Codigo del pais</param>
        /// <param name="idCiudad">Id de la ciudad (PK de la tabla conf.T_Ciudad)</param>
        /// <returns>Cadena formateada de la imagen de la firma de correo</returns>
        public string ObtenerFirmaCorreoImagen(string urlFoto, int? idCodigoPais = 0, int? idCiudad = 0)
        {
            try
            {
                string urlInicial = urlFoto;
                if (idCodigoPais == Pais.CodigoPeru && idCiudad == 4)//4: Arequipa
                    urlInicial += "pa";
                else if (idCodigoPais == Pais.CodigoPeru && idCiudad == 14)//14: Lima Y Callao
                    urlInicial += "pl";
                else if (idCodigoPais == Pais.CodigoColombia)
                    urlInicial += "c";
                else if (idCodigoPais == Pais.CodigoBolivia)
                    urlInicial += "b";
                else
                    urlInicial += "pl";

                return urlInicial += ".png";
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/11/2021
        /// <summary>
        /// Obtener información de Personal por nombre AutoComplete
        /// </summary>
        /// <param name="nombre"> nombre de búsqueda </param>
        /// <returns> Lista de Personal por nombre Registrados : List<PersonalAutocompleteDTO> </returns>
        public List<PersonalAutocompleteDTO> CargarPersonalAutoComplete(string nombre)
        {
            try
            {
                return _unitOfWork.PersonalRepository.CargarPersonalAutoComplete(nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Coordinadores Para Filtro
        /// </summary>
        /// <param></param>
        /// <returns>Lista Objeto: List<FiltroDTO></returns>
        public List<FiltroDTO> ObtenerCoordinadoresParaFiltro()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerCoordinadoresParaFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns>Lista de ObjetosDTO: List(PersonalAsignadoDTO)</returns>
        public List<PersonalAsignadoDTO> ObtenerPersonalAsignadoVentas(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalAsignadoVentas(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los asesores de ventas para el reporte de seguimiento por el tipoPersonal=coordinador
        /// </summary>
        /// <returns>Lista de objetos de clase PersonalAsignadoDTO</returns>
        public List<PersonalAsignadoDTO> ObtenerAsesoresVentasOficialReporteSeguimiento()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerAsesoresVentasOficialReporteSeguimiento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 08/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener configuracion de open vox por el id del Personal
        /// </summary>
        /// <param name="idPersonal"> id del personal </param>
        /// <returns> Lista Objeto: List<PersonalConfiguracionOpenVoxDTO> </returns>
        public List<PersonalConfiguracionOpenVoxDTO> ObtenerConfiguracionOpenVoxPorIdPersonal(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerConfiguracionOpenVoxPorIdPersonal(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros del personal activo
        /// </summary>
        /// <param></param>
        /// <returns> List<PersonalActivoEmailDTO> </returns>
        public List<PersonalActivoEmailDTO> ObtenerTodoPersonalActivoParaFiltro()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerTodoPersonalActivoParaFiltro();
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
        /// Obtiene todos los asesores y sus coordinadores para filtros (id, nombres)
        /// </summary>
        /// <param></param>
        /// <returns> List<AsesorNombreFiltroDTO> </returns>
        public List<AsesorNombreFiltroDTO> ObtenerTodoAsesorCoordinadorVentas()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerTodoAsesorCoordinadorVentas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<PersonalComboDTO> ObtenerPersonalPorMarketing()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalPorMarketing();
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
                return _unitOfWork.PersonalRepository.Exist(idPlantilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Personal Asignado Operaciones
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns> List<PersonalAsignadoDTO> </returns>
        public List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperacionesTotalV2(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalAsignadoOperacionesTotalV2(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Miguel Quiñones
        /// Fecha: 228/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Personal Asignado Operaciones
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns> List<PersonalAsignadoDTO> </returns>
        public List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperacionesTotal(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalAsignadoOperacionesTotal(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Todos los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns> Lista de DTO: List<PersonalAsignadoDTO> </returns>
        public List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperacionesUsuarioTotal(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalAsignadoOperacionesUsuarioTotal(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Todos los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns> DTO: PersonalAsignadoReportePendienteDTO </returns>
        public PersonalAsignadoReportePendienteDTO ObtenerDatosUsuariosReportePendiente(string usuario)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerDatosUsuariosReportePendiente(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/01/2023
        /// Version: 1.0
        /// <summary>Original solo activos
        /// Obtiene los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns> Lista DTO: List<PersonalAsignadoDTO> </returns>
        public List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperaciones(int idPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalAsignadoOperaciones(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los asesores de operaciones activos
        /// </summary>
        /// <returns> Lista DTO: List<AsesorFiltroDTO> - listaPersonal </returns>
        public List<AsesorFiltroDTO> ObtenerPersonalAsesoresOperacionesActivos()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalAsesoresOperacionesActivos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Retorna el presonal responsable de seleccion
        /// </summary>
        /// <returns> Lista DTO: List<FiltroCombosDTO> - listaPersonal </returns>
        public List<FiltroCombosDTO> ObtenerComboPersonalGestionPersonas()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerComboPersonalGestionPersonas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Retorna el presonal Ventas V4
        /// </summary>
        /// <returns> Lista DTO: List<FiltroCombosDTO> - listaPersonal </returns>
        public List<AsesorNombreFiltroDTO> ObtenerPersonalVentasV4()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalVentasV4();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 14/09/2023
        /// Version: 1.0
        /// <summary>
        /// Retorna pais de sede del personal
        /// </summary>
        /// <returns> Id Pais Sede </returns>
        public int? ObtenerPaisSedePersonal(int idPersonal)
        {
            return _unitOfWork.PersonalRepository.ObtenerPaisSedePersonal(idPersonal);
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Retorna el presonal para autocomplete
        /// </summary>
        /// <returns> Lista DTO: List<FiltroCombosDTO> - listaPersonal </returns>
        public List<PersonalAutocompleteDTO> ObtenerNombresFiltroAutoComplete(string valor)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerNombresFiltroAutoComplete(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene personal Asistente Coordinador/Cobranza Matricula.
        /// </summary>
        /// <returns> Lista DTO: List<FiltroCombosDTO> - listaPersonal </returns>
        public List<PersonalAutocompleteDTO> ObtenerAsistenteAcademicoMatricula(string valor)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerAsistenteAcademicoMatricula(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 13/12/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene asesores que deben tener agenda liberada
        /// </summary>
        /// <returns> Lista DTO: List<PersonalAutocompleteDTO> - listaPersonal </returns>
        public List<PersonalAutocompleteDTO> ObtenerPersonalAgendaLiberadaOperaciones()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalAgendaLiberadaOperaciones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 12/04/2024
        /// Version: 1.0
        /// <summary>
        /// Procesa el Excel para subir a T_RegistroMarcacion
        /// </summary>
        /// <returns> Lista DTO: List<PersonalAutocompleteDTO> - listaPersonal </returns>
        public IEnumerable<RegistrosMarcacionPersonal> ProcesarExcelRegistroMarcacion(IFormFile ArchivoExcel)
        {
            try
            {
                var ListaMarcacion = new List<RegistrosMarcacionPersonal>();
                int index = 0;
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    NewLine = Environment.NewLine,
                    Delimiter = ";",
                    MissingFieldFound = null,
                    BadDataFound = null,
                };
                using (var reader = new StreamReader(ArchivoExcel.OpenReadStream()))
                using (var cvs = new CsvReader(reader, config))
                {
                    cvs.Read();
                    cvs.ReadHeader();
                    while (cvs.Read())
                    {
                        index++;

                        var fechaMarcacionString = cvs.GetField<string>("fechaMarcacion");
                        var dniString = cvs.GetField<string>("dni");

                        if (string.IsNullOrWhiteSpace(fechaMarcacionString) || string.IsNullOrWhiteSpace(dniString))
                        { throw new Exception("Uno/varios valores o Campos son incorrectos en Dni o FechaMarcación!"); }

                        var datos = new RegistrosMarcacionPersonal
                        {
                            dni = dniString,
                            m1 = string.IsNullOrWhiteSpace(cvs.GetField<string>("m1")) ? null : cvs.GetField<string>("m1"),
                            m2 = string.IsNullOrWhiteSpace(cvs.GetField<string>("m2")) ? null : cvs.GetField<string>("m2"),
                            m3 = string.IsNullOrWhiteSpace(cvs.GetField<string>("m3")) ? null : cvs.GetField<string>("m3"),
                            m4 = string.IsNullOrWhiteSpace(cvs.GetField<string>("m4")) ? null : cvs.GetField<string>("m4"),
                            m5 = string.IsNullOrWhiteSpace(cvs.GetField<string>("m5")) ? null : cvs.GetField<string>("m5"),
                            m6 = string.IsNullOrWhiteSpace(cvs.GetField<string>("m6")) ? null : cvs.GetField<string>("m6")
                        };

                        datos.fechaMarcacion = string.IsNullOrWhiteSpace(fechaMarcacionString) ? null :
                            DateTime.ParseExact(fechaMarcacionString, new string[] { "d/MM/yyyy", "dd/MM/yyyy", "d/M/yyyy" }, CultureInfo.InvariantCulture);

                        ListaMarcacion.Add(datos);
                    }
                }
                var Nregistros = index;
                return ListaMarcacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 28/03/2023
        /// Version: 1.0
        /// <summary>
        /// Inserta la Marcacion del Personal
        /// </summary>
        /// <returns> true or False </returns>
        public bool InsertarMarcacionPersonal(string data, string usuario)
        {
            try
            {
                return _unitOfWork.PersonalRepository.InsertarMarcacionPersonal(data, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Max Mantilla
        /// Fecha: 03/06/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los nombres de personal para autocomplete
        /// </summary>
        /// <returns> int </returns>
        public int ObtenerIdPersonalPorUserName(string UserName)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerIdPersonalPorUserName(UserName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<PersonalFichaDatosDTO> ObtenerFichaDatosPersonal()
        {
            return _unitOfWork.PersonalRepository.ObtenerFichaDatosPersonal();
        }

        public ComboFichaDatosPersonalDTO ObtenerCombosFichaDatosPersonal()
        {
            try
            {
                ComboFichaDatosPersonalDTO respuesta = new ComboFichaDatosPersonalDTO()
                {
                    listaCiudad = _unitOfWork.CiudadRepository.ObtenerCiudadesPorPaisByFichaDato(),
                    listaPais = _unitOfWork.PaisRepository.ObtenerComboConMoneda(),
                    listaEstadoCivil = _unitOfWork.EstadoCivilRepository.Obtener(),
                    listaSexo = _unitOfWork.SexoRepository.ObtenerCombo(),
                    listaSistemaPensionario = _unitOfWork.SistemaPensionarioRepository.Obtener(),
                    listaEntidad = _unitOfWork.EntidadSistemaPensionarioRepository.Obtener(),
                    listaTipoDocumento = _unitOfWork.TipoDocumentoPersonalRepository.Obtener(),
                    listaMotivoCese = _unitOfWork.MotivoCeseRepository.Obtener(),
                    listaEntidadSeguroSalud = _unitOfWork.EntidadSeguroSaludRepository.Obtener(),
                    listaCentroEstudio = _unitOfWork.CentroEstudioRepository.Obtener(),
                    listaTipoEstudio = _unitOfWork.TipoEstudioRepository.Obtener(),
                    listaAreaFormacion = _unitOfWork.AreaFormacionRepository.ObtenerAreaFormacionFiltro(),
                    listaEstadoEstudio = _unitOfWork.GradoEstudioRepository.ObtenerEstadoEstudio(),
                    listaNivelEstudio = _unitOfWork.NivelEstudioRepository.Obtener(),
                    listaIdioma = _unitOfWork.IdiomaRepository.ObtenerCombo(),
                    listaNivelIdioma = _unitOfWork.NivelIdiomaRepository.Obtener(),
                    listaEmpresa = _unitOfWork.EmpresaRepository.ObtenerCombo(),
                    listaAreaTrabajo = _unitOfWork.AreaTrabajoRepository.ObtenerTodoAreaTrabajoFiltro(),
                    listaCargo = _unitOfWork.CargoRepository.ObtenerCargoFiltro(),
                    listaParentesco = _unitOfWork.ParentescoPersonalRepository.Obtener(),
                    listaTipoSangre = _unitOfWork.TipoSangreRepository.Obtener(),
                    listaPuestoTrabajo = _unitOfWork.PuestoTrabajoRepository.ObtenerCombo(),
                    listaSedeTrabajo = _unitOfWork.SedeTrabajoRepository.ObtenerCombo(),
                    listaPersonalAreaTrabajo = _unitOfWork.PersonalAreaTrabajoRepository.Obtener(),
                    listaPersonal = _unitOfWork.PersonalRepository.ObtenerComboNombre(),
                    listaPersonalAsesorAsociado = _unitOfWork.PersonalRepository.ObtenerAsesorCerrador(),
                    listaTipoPagoRemuneracion = _unitOfWork.TipoPagoRemuneracionRepository.Obtener(),
                    listaEntidadFinanciera = _unitOfWork.EntidadFinancieraRepository.ObtenerEntidadesFinancieras(),
                    listaContratoEstado = _unitOfWork.ContratoEstadoRepository.Obtener(),
                    listaMotivoInactividad = _unitOfWork.MotivoInactividadRepository.Obtener(),
                    listaPuestoTrabajoNivel = _unitOfWork.MaestroNivelPuestoTrabajoRepository.ObtenerListaParaFiltro(),
                    listaCategoriaAsesor = _unitOfWork.TableroComercialCategoriaAsesorRepository.ObtenerCombo(),
                    listaNivelCompetenciaTecnica = _unitOfWork.NivelCompetenciaTecnicaRepository.Obtener()

                };
                return _mapper.Map<ComboFichaDatosPersonalDTO>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public accesoPortalDTO ObtenerPEspecificoPersonalAccesoTemporalCombo()
        {
            try
            {
                // List<PEspecificoNuevoAulaVirtualDTO>
                var resultado = _unitOfWork.PEspecificoRepository.ObtenerPEspecificoPersonalNuevoAulaVirtualTipo();
                var programasAsignados = resultado
                                          .GroupBy(x => new { x.IdPEspecificoPadre, x.NombrePEspecificoPadre })
                                          .Select(s => new ProgramasAsignadosDTO
                                          {
                                              IdPEspecificoPadre = s.Key.IdPEspecificoPadre,
                                              NombrePEspecificoPadre = s.Key.NombrePEspecificoPadre
                                          })
                                          .ToList();
                var cursosAsignados = resultado
                                         .Select(s => new CursosAsignadosDTO
                                         {
                                             IdPEspecificoPadre = s.IdPEspecificoPadre,
                                             IdPEspecifico = s.IdPEspecifico,
                                             NombrePEspecifico = s.NombrePEspecifico
                                         })
                                         .ToList();

                accesoPortalDTO respuesta = new accesoPortalDTO()
                {
                    ProgramasAsignados = programasAsignados,
                    CursosAsignados = cursosAsignados

                };
                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public FichaDatosPersonalDTO ObtenerInformacionPersonal(int idPersonal)
        {
            try
            {

                var puestoTrabajoNivel = _unitOfWork.MaestroNivelPuestoTrabajoRepository.GetBy(x => x.Estado == true).ToList();
                var personal = _unitOfWork.PersonalRepository.ObtenerInformacionPersonalPuestoSede(idPersonal);

                if (personal.IdPuestoTrabajoNivel == null)
                {
                    if (personal.TipoPersonal != null && personal.TipoPersonal.Length > 0)
                    {
                        var validacion = puestoTrabajoNivel.Where(x => x.Nombre.Contains(personal.TipoPersonal)).FirstOrDefault();
                        if (validacion != null)
                        {
                            personal.IdPuestoTrabajoNivel = validacion.Id;
                        }
                    }
                }
                var personalCese = _unitOfWork.PersonalCeseRepository.ObtenerMotivoFechaUltimo(idPersonal);
                var personalRemuneracion = _unitOfWork.PersonalRemuneracionRepository.Obtener(idPersonal);
                var personalDireccion = _unitOfWork.PersonalRepository.ObtenerPersonalDireccionDomiciliaria(idPersonal);
                var listaFormacion = _unitOfWork.PersonalFormacionRepository.Obtener(idPersonal);
                var listaComputo = _unitOfWork.PersonalComputoRepository.ObtenerPersonalComputo(idPersonal);

                var listaIdioma = _unitOfWork.PersonalIdiomaRepository.ObtenerPersonalIdioma(idPersonal);
                var listaCertificacion = _unitOfWork.PersonalCertificacionRepository.ObtenerPersonalCertificacion(idPersonal);
                var listaExperiencia = _unitOfWork.PersonalExperienciaRepository.ObtenerPersonalExperiencia(idPersonal);
                var listaInformacionMedica = _unitOfWork.PersonalInformacionMedicaRepository.ObtenerPersonalInformacionMedica(idPersonal);
                var listaHistorialMedico = _unitOfWork.PersonalHistorialMedicoRepository.ObtenerPersonalHistorialMedico(idPersonal);
                var listaSistemaPensionario = _unitOfWork.PersonalSistemaPensionarioRepository.ObtenerPersonalSistemaPensionario(idPersonal);
                var listaSeguroSalud = _unitOfWork.PersonalSeguroSaludRepository.ObtenerPersonalSeguroSalud(idPersonal);
                var listaDatosPersonalFamiliar = _unitOfWork.DatoFamiliarPersonalRepository.ObtenerListaFamiliarPersonal(idPersonal);
                var listaAccesoTemporal = ObtenerListaAccesoTemporal(idPersonal);
                var datoContratoPersonal = _unitOfWork.DatoContratoPersonalRepository.ObtenerByIdPersonal(idPersonal);
                var listaPuestoTrabajo = _unitOfWork.PersonalLogRepository.GetBy(x => x.EstadoRol == true && x.IdPersonal == idPersonal, x => new PersonalPuestoTrabajoDTO { Id = x.Id, Rol = x.Rol, FechaInicio = x.FechaInicio, FechaFin = x.FechaFin }).ToList();
                if (listaPuestoTrabajo != null)
                {
                    var ultimo = listaPuestoTrabajo.Count;
                    listaPuestoTrabajo[ultimo - 1].FechaFin = null;
                }
                var listaTipoAsesorHistorico = _unitOfWork.PersonalLogRepository.ObtenerTipoAsesorHistorico(idPersonal);
                var listaJefeInmediatoHistorico = _unitOfWork.PersonalLogRepository.ObtenerJefeInmediatoHistorico(idPersonal);
                var listaPeriodoInactivoHistorico = _unitOfWork.PersonalMotivoTiempoInactividadRepository.ObtenerPeriodoInactivoHistorico(idPersonal);
                var ultimoPeriodoInactivo = listaPeriodoInactivoHistorico.OrderByDescending(x => x.Id).FirstOrDefault();
                if (personal.Activo == true)
                {
                    personalCese = null;
                    ultimoPeriodoInactivo = null;
                }
                FichaDatosPersonalDTO resultado = new FichaDatosPersonalDTO()
                {
                    DatosPersonal = personal,
                    DatosPersonalCese = personalCese,
                    PersonalRemuneracion = personalRemuneracion,
                    Formacion = listaFormacion,
                    Computo = listaComputo,
                    Idioma = listaIdioma,
                    Certificacion = listaCertificacion,
                    Experiencia = listaExperiencia,
                    DatoFamiliar = listaDatosPersonalFamiliar,
                    InformacionMedica = listaInformacionMedica,
                    HistorialMedico = listaHistorialMedico,
                    SistemaPensionario = listaSistemaPensionario,
                    SeguroSalud = listaSeguroSalud,
                    DatoContratoPersonal = datoContratoPersonal,
                    ListaAccesoTemporal = listaAccesoTemporal,
                    listaPuestoTrabajo = listaPuestoTrabajo,
                    PersonalDireccion = personalDireccion,
                    listaTipoAsesorHistorico = listaTipoAsesorHistorico,
                    listaJefeInmediatoHistorico = listaJefeInmediatoHistorico,
                    listaPeriodoInactivoHistorico = listaPeriodoInactivoHistorico,
                    DatoPersonalDescanso = ultimoPeriodoInactivo
                };

                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<MaestroPersonalGrupoAccesoTemporalDTO> ObtenerListaAccesoTemporal(int idPersonal)
        {
            try
            {
                List<MaestroPersonalGrupoAccesoTemporalDTO> listaAccesoTemporal = new List<MaestroPersonalGrupoAccesoTemporalDTO>();

                var accesoTempAuxiliar = _unitOfWork.PersonalAccesoTemporalAulaVirtualRepository.ObtenerListaAccesoTemporal(idPersonal);

                listaAccesoTemporal = accesoTempAuxiliar
                    .GroupBy(x => new { x.IdPersonal, x.IdPEspecificoPadre, x.NombreProgramaPadre, x.FechaInicio, x.FechaFin, x.EvaluacionHabilitada })
                    .Select(g =>
                    new MaestroPersonalGrupoAccesoTemporalDTO
                    {
                        IdPersonal = g.Key.IdPersonal,
                        IdPEspecificoPadre = g.Key.IdPEspecificoPadre,
                        NombreProgramaPadre = g.Key.NombreProgramaPadre,
                        EvaluacionHabilitada = g.Key.EvaluacionHabilitada,
                        IdPEspecificoHijo = accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.IdPEspecificoHijo).ToList(),
                        CantidadPreguntaConfigurada = accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.CantidadPreguntaConfigurada).ToList().Sum(),
                        CantidadCrucigramaConfigurado = accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.CantidadCrucigramaConfigurado).ToList().Sum(),
                        CantidadPreguntaResuelta = accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.CantidadPreguntaResuelta).ToList().Sum(),
                        CantidadCrucigramaResuelta = accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.CantidadCrucigramaResuelta).ToList().Sum(),
                        Avance = Math.Round(accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.Avance).ToList().Sum() / accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.Avance).ToList().Count(), 2),
                        Nota = Math.Round(accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.Nota).ToList().Sum() / accesoTempAuxiliar.Where(y => y.IdPEspecificoPadre == g.Key.IdPEspecificoPadre && y.FechaInicio == g.Key.FechaInicio && y.FechaFin == g.Key.FechaFin).Select(z => z.Nota).ToList().Count(), 2),
                        FechaInicio = g.Key.FechaInicio,
                        FechaFin = g.Key.FechaFin
                    }).ToList();

                return listaAccesoTemporal;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Insertar(MaestroPersonalCompuestoDTO dto, string usuarioR)
        {
            try
            {
                //Validacion de Id Jefe null
                dto.Personal.IdJefe = dto.Personal.IdJefe != null ? dto.Personal.IdJefe : 0;
                //Validacion de Nivel de Puesto de Trabajo
                var auxiliarNivelPuestoTrabajo = "otro";
                var idPersonalGlobal = 0;
                if (dto.Personal.IdPuestoTrabajoNivel > 0 && dto.Personal.IdPuestoTrabajoNivel != null)
                {
                    auxiliarNivelPuestoTrabajo = _unitOfWork.MaestroNivelPuestoTrabajoRepository.GetBy(x => x.Id == dto.Personal.IdPuestoTrabajoNivel.GetValueOrDefault()).Select(x => x.NivelVisualizacionAgenda).FirstOrDefault();
                }
                else if (dto.Personal.TipoPersonal != null && dto.Personal.TipoPersonal.Length > 0)
                {
                    auxiliarNivelPuestoTrabajo = _unitOfWork.MaestroNivelPuestoTrabajoRepository.GetBy(x => x.NivelVisualizacionAgenda.ToUpper().Contains(dto.Personal.TipoPersonal.ToUpper())).Select(x => x.NivelVisualizacionAgenda).FirstOrDefault();
                }
                Persona persona = new Persona();
                Personal personal;
                int? IdPersonaClasificacion = null;
                var ListEmailRepetidoValido = _unitOfWork.PersonalRepository.GetBy(x => x.Estado == true && x.Email.Equals(dto.Personal.Email), x => new { x.Email, x.Id }).ToList();
                var IdPersonalEmailRepetido = _unitOfWork.PersonalRepository.ObtenerPersonalEliminadoEmailRepetido(dto.Personal.Email);

                if (ListEmailRepetidoValido.Count == 0 && (IdPersonalEmailRepetido == null || IdPersonalEmailRepetido == 0))
                {
                    personal = new Personal()
                    {
                        Apellidos = dto.Personal.Apellidos,
                        Rol = dto.Personal.Area,
                        AreaAbrev = dto.Personal.AreaAbrev,
                        DistritoDireccion = dto.Personal.DistritoDireccion,
                        EmailReferencia = dto.Personal.EmailReferencia,
                        FechaNacimiento = dto.Personal.FechaNacimiento,
                        IdCiudad = dto.Personal.IdCiudadNacimiento,
                        IdRegionDireccion = dto.Personal.IdCiudadReferencia,
                        IdEstadocivil = dto.Personal.IdEstadocivil,
                        IdPaisNacimiento = dto.Personal.IdPaisNacimiento,
                        IdPaisDireccion = dto.Personal.IdPaisReferencia,
                        IdSexo = dto.Personal.IdSexo,
                        IdTipoDocumento = dto.Personal.IdTipoDocumento,
                        NombreDireccion = dto.Personal.NombreDireccion,
                        Nombres = dto.Personal.Nombres,
                        NumeroDocumento = dto.Personal.NumeroDocumento,
                        FijoReferencia = dto.Personal.TelefonoFijo,
                        MovilReferencia = dto.Personal.TelefonoMovil,
                        Estado = true,
                        UsuarioCreacion = usuarioR,
                        UsuarioModificacion = usuarioR,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Email = dto.Personal.Email,
                        TipoPersonal = auxiliarNivelPuestoTrabajo,
                        IdJefe = dto.Personal.IdJefe,
                        Central = dto.Personal.Central,
                        Anexo3Cx = dto.Personal.Anexo3CX,
                        UrlFirmaCorreos = dto.Personal.UrlFirmaCorreos,
                        Activo = dto.Personal.Activo,
                        IdSistemaPensionario = dto.PersonalSistemaPensionario.IdSistemaPensionario,
                        IdEntidadSistemaPensionario = dto.PersonalSistemaPensionario.IdEntidadSistemaPensionario,
                        NombreCuspp = dto.PersonalSistemaPensionario.CodigoAfiliado,
                        ConEssalud = dto.PersonalSeguroSalud.IdEntidadSeguroSalud.HasValue ? true : false,
                        IdTipoSangre = dto.Personal.IdTipoSangre,
                        EsCerrador = dto.Personal.EsCerrador,
                        IdCerrador = dto.Personal.IdAsesorAsociado,
                        IdPuestoTrabajoNivel = dto.Personal.IdPuestoTrabajoNivel,
                        IdPersonalArchivo = dto.Personal.IdPersonalArchivo,
                        IdPersonalAreaTrabajo = dto.Personal.IdPersonalAreaTrabajo,
                        IdTableroComercialCategoriaAsesor = dto.Personal.IdTableroComercialCategoriaAsesor
                    };
                    var PersonalNew = _unitOfWork.PersonalRepository.Add(personal);
                    _unitOfWork.Commit();
                    idPersonalGlobal = PersonalNew.Id;


                    PersonalPuestoSedeHistorico agregar = new PersonalPuestoSedeHistorico()
                    {
                        IdPersonal = idPersonalGlobal,
                        IdPuestoTrabajo = dto.Personal.IdPuestoTrabajo.GetValueOrDefault(),
                        IdSedeTrabajo = dto.Personal.IdSede.GetValueOrDefault(),
                        Actual = true,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuarioR,
                        UsuarioModificacion = usuarioR
                    };
                    var resInsertar = _unitOfWork.PersonalPuestoSedeHistoricoRepository.Add(agregar);
                    _unitOfWork.Commit();

                }
                else if (ListEmailRepetidoValido.Count == 0 && (IdPersonalEmailRepetido != null || IdPersonalEmailRepetido != 0))
                {
                    _unitOfWork.PersonalRepository.ActivarPersonal(IdPersonalEmailRepetido.Value);
                    personal = _unitOfWork.PersonalRepository.ObtenerPorId(IdPersonalEmailRepetido.Value);
                    personal.Apellidos = dto.Personal.Apellidos;
                    personal.Rol = dto.Personal.Area;
                    personal.AreaAbrev = dto.Personal.AreaAbrev;
                    personal.DistritoDireccion = dto.Personal.DistritoDireccion;
                    personal.EmailReferencia = dto.Personal.EmailReferencia;
                    personal.FechaNacimiento = dto.Personal.FechaNacimiento;
                    personal.IdCiudad = dto.Personal.IdCiudadNacimiento;
                    personal.IdRegionDireccion = dto.Personal.IdCiudadReferencia;
                    personal.IdEstadocivil = dto.Personal.IdEstadocivil;
                    personal.IdPaisNacimiento = dto.Personal.IdPaisNacimiento;
                    personal.IdPaisDireccion = dto.Personal.IdPaisReferencia;
                    personal.IdSexo = dto.Personal.IdSexo;
                    personal.IdTipoDocumento = dto.Personal.IdTipoDocumento;
                    personal.NombreDireccion = dto.Personal.NombreDireccion;
                    personal.Nombres = dto.Personal.Nombres;
                    personal.NumeroDocumento = dto.Personal.NumeroDocumento;
                    personal.FijoReferencia = dto.Personal.TelefonoFijo;
                    personal.MovilReferencia = dto.Personal.TelefonoMovil;
                    personal.UsuarioModificacion = usuarioR;
                    personal.FechaModificacion = DateTime.Now;
                    personal.Email = dto.Personal.Email;
                    personal.TipoPersonal = auxiliarNivelPuestoTrabajo;
                    personal.IdJefe = dto.Personal.IdJefe;
                    personal.Central = dto.Personal.Central;
                    personal.Anexo3Cx = dto.Personal.Anexo3CX;
                    personal.UrlFirmaCorreos = dto.Personal.UrlFirmaCorreos;
                    personal.Activo = dto.Personal.Activo;
                    personal.IdSistemaPensionario = dto.PersonalSistemaPensionario.IdSistemaPensionario;
                    personal.IdEntidadSistemaPensionario = dto.PersonalSistemaPensionario.IdEntidadSistemaPensionario;
                    personal.NombreCuspp = dto.PersonalSistemaPensionario.CodigoAfiliado;
                    personal.ConEssalud = dto.PersonalSeguroSalud.IdEntidadSeguroSalud.HasValue ? true : false;
                    personal.IdTipoSangre = dto.Personal.IdTipoSangre;
                    personal.EsCerrador = dto.Personal.EsCerrador;
                    personal.IdCerrador = dto.Personal.IdAsesorAsociado;
                    personal.IdPuestoTrabajoNivel = dto.Personal.IdPuestoTrabajoNivel;
                    personal.IdTableroComercialCategoriaAsesor = dto.Personal.IdTableroComercialCategoriaAsesor;
                    var PersonalNew = _unitOfWork.PersonalRepository.Update(personal);
                    _unitOfWork.Commit();
                    idPersonalGlobal = PersonalNew.Id;

                    var personalPuestoTrabajoSede = _unitOfWork.PersonalPuestoSedeHistoricoRepository.GetBy(x => x.IdPersonal == idPersonalGlobal).FirstOrDefault();

                    if (personalPuestoTrabajoSede == null)
                    {
                        PersonalPuestoSedeHistorico agregar = new PersonalPuestoSedeHistorico()
                        {
                            IdPersonal = idPersonalGlobal,
                            IdPuestoTrabajo = dto.Personal.IdPuestoTrabajo.GetValueOrDefault(),
                            IdSedeTrabajo = dto.Personal.IdSede.GetValueOrDefault(),
                            Actual = true,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuarioR,
                            UsuarioModificacion = usuarioR
                        };
                        var resInsertar = _unitOfWork.PersonalPuestoSedeHistoricoRepository.Add(agregar);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        personalPuestoTrabajoSede.FechaModificacion = DateTime.Now;
                        personalPuestoTrabajoSede.UsuarioModificacion = usuarioR;
                        personalPuestoTrabajoSede.Actual = false;
                        var res = _unitOfWork.PersonalPuestoSedeHistoricoRepository.Update(personalPuestoTrabajoSede);
                        _unitOfWork.Commit();
                        if (res)
                        {
                            PersonalPuestoSedeHistorico agregar = new PersonalPuestoSedeHistorico()
                            {
                                IdPersonal = idPersonalGlobal,
                                IdPuestoTrabajo = dto.Personal.IdPuestoTrabajo.GetValueOrDefault(),
                                IdSedeTrabajo = dto.Personal.IdSede.GetValueOrDefault(),
                                Actual = true,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = usuarioR,
                                UsuarioModificacion = usuarioR
                            };
                            var resInsertar = _unitOfWork.PersonalPuestoSedeHistoricoRepository.Add(agregar);
                            _unitOfWork.Commit();
                        }
                    }
                    //Asignacion de módulos
                    var usuario = _unitOfWork.UsuarioRepository.GetBy(x => x.IdPersonal == dto.Personal.Id).FirstOrDefault();
                    if (usuario != null)
                    {
                        var listaModuloAnterior = _unitOfWork.ModuloSistemaAccesoRepository.GetBy(x => x.IdUsuario == usuario.Id).ToList();
                        var listaModuloNuevo = _unitOfWork.ModuloSistemaPuestoTrabajoRepository.GetBy(x => x.IdPuestoTrabajo == dto.Personal.IdPuestoTrabajo).ToList();
                        if (listaModuloAnterior.Count > 0)
                        {
                            foreach (var moduloAnterior in listaModuloAnterior)
                            {
                                _unitOfWork.ModuloSistemaAccesoRepository.Delete(moduloAnterior.Id, usuarioR);
                                _unitOfWork.Commit();
                            }
                        }
                        if (listaModuloNuevo.Count > 0)
                        {
                            ModuloSistemaAccesoV5 agregarModulo;
                            foreach (var moduloNuevo in listaModuloNuevo)
                            {
                                agregarModulo = new ModuloSistemaAccesoV5()
                                {
                                    IdUsuarioRol = usuario.IdUsuarioRol,
                                    IdUsuario = usuario.Id,
                                    IdModuloSistema = moduloNuevo.IdModuloSistema,
                                    Estado = true,
                                    UsuarioCreacion = usuarioR,
                                    UsuarioModificacion = usuarioR,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                _unitOfWork.ModuloSistemaAccesoRepository.Add(agregarModulo);
                                _unitOfWork.Commit();
                            }
                        }
                    }
                }
                else
                {
                    personal = new Personal();
                }
                IdPersonaClasificacion = _personaService.InsertarPersona(idPersonalGlobal, Aplicacion.Base.Enums.Enums.TipoPersona.Personal, usuarioR);

                if (IdPersonaClasificacion == null)
                {
                    throw new Exception("Error al insertar el Tipo Persona Clasificacion");
                }
                if (personal != null)
                {
                    PersonalLog personalLogBO = new PersonalLog();
                    personalLogBO.IdPersonal = idPersonalGlobal;
                    personalLogBO.Rol = dto.Personal.Area;
                    personalLogBO.TipoPersonal = personal.TipoPersonal;
                    personalLogBO.IdPuestoTrabajoNivel = personal.IdPuestoTrabajoNivel;
                    personalLogBO.IdJefe = personal.IdJefe;
                    personalLogBO.EstadoCerrador = false;
                    personalLogBO.EstadoRol = true;
                    personalLogBO.EstadoTipoPersonal = true;
                    personalLogBO.EstadoIdJefe = true;
                    personalLogBO.FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                    personalLogBO.FechaFin = null;
                    personalLogBO.Estado = true;
                    personalLogBO.UsuarioCreacion = usuarioR;
                    personalLogBO.UsuarioModificacion = usuarioR;
                    personalLogBO.FechaCreacion = DateTime.Now;
                    personalLogBO.FechaModificacion = DateTime.Now;
                    _unitOfWork.PersonalLogRepository.Add(personalLogBO);
                    _unitOfWork.Commit();

                    var personalLogActualizar = _unitOfWork.PersonalLogRepository.GetBy(x => x.IdPersonal == idPersonalGlobal).OrderByDescending(x => x.Id).FirstOrDefault();
                    PersonalLog personalLogTipoAsesorBO = new PersonalLog();
                    personalLogTipoAsesorBO.IdPersonal = personalLogActualizar.IdPersonal;
                    personalLogTipoAsesorBO.Rol = personalLogActualizar.Rol;
                    personalLogTipoAsesorBO.TipoPersonal = personalLogActualizar.TipoPersonal;
                    personalLogTipoAsesorBO.IdPuestoTrabajoNivel = personalLogActualizar.IdPuestoTrabajoNivel;
                    personalLogTipoAsesorBO.IdJefe = personalLogActualizar.IdJefe;
                    personalLogTipoAsesorBO.IdCerrador = personal.IdCerrador;
                    personalLogTipoAsesorBO.EsCerrador = personal.EsCerrador;
                    personalLogTipoAsesorBO.EstadoCerrador = true;
                    personalLogTipoAsesorBO.EstadoRol = false;
                    personalLogTipoAsesorBO.EstadoTipoPersonal = false;
                    personalLogTipoAsesorBO.EstadoIdJefe = false;
                    personalLogTipoAsesorBO.FechaInicio = DateTime.Now.Date;
                    personalLogTipoAsesorBO.FechaFin = null;
                    personalLogTipoAsesorBO.Estado = true;
                    personalLogTipoAsesorBO.UsuarioCreacion = personalLogActualizar.UsuarioCreacion;
                    personalLogTipoAsesorBO.UsuarioModificacion = personalLogActualizar.UsuarioModificacion;
                    personalLogTipoAsesorBO.FechaCreacion = personalLogActualizar.FechaCreacion;
                    personalLogTipoAsesorBO.FechaModificacion = personalLogActualizar.FechaModificacion;
                    _unitOfWork.PersonalLogRepository.Add(personalLogTipoAsesorBO);
                    _unitOfWork.Commit();
                }
                if (dto.PersonalDireccion.IdPais.HasValue)
                {
                    PersonalDireccion personalDireccion = new PersonalDireccion
                    {
                        IdPersonal = idPersonalGlobal,
                        IdPais = dto.PersonalDireccion.IdPais,
                        IdCiudad = dto.PersonalDireccion.IdCiudad,
                        Distrito = dto.PersonalDireccion.Distrito == "" ? null : dto.PersonalDireccion.Distrito,
                        TipoVia = dto.PersonalDireccion.TipoVia == "" ? null : dto.PersonalDireccion.TipoVia,
                        TipoZonaUrbana = dto.PersonalDireccion.TipoZonaUrbana == "" ? null : dto.PersonalDireccion.TipoZonaUrbana,
                        NombreVia = dto.PersonalDireccion.NombreVia == "" ? null : dto.PersonalDireccion.NombreVia,
                        NombreZonaUrbana = dto.PersonalDireccion.NombreZonaUrbana == "" ? null : dto.PersonalDireccion.NombreZonaUrbana,
                        Manzana = dto.PersonalDireccion.Manzana == "" ? null : dto.PersonalDireccion.Manzana,
                        Lote = dto.PersonalDireccion.Lote,
                        Activo = true,
                        Estado = true,
                        UsuarioCreacion = usuarioR,
                        UsuarioModificacion = usuarioR,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _unitOfWork.PersonalDireccionRepository.Add(personalDireccion);
                    _unitOfWork.Commit();

                }
                if (dto.PersonalSistemaPensionario.IdSistemaPensionario.HasValue)
                {
                    PersonalSistemaPensionario personalSistemaPensionario = new PersonalSistemaPensionario
                    {
                        Activo = true,
                        CodigoAfiliado = dto.PersonalSistemaPensionario.CodigoAfiliado,
                        IdEntidadSistemaPensionario = dto.PersonalSistemaPensionario.IdEntidadSistemaPensionario,
                        IdSistemaPensionario = dto.PersonalSistemaPensionario.IdSistemaPensionario.Value,
                        IdPersonal = idPersonalGlobal,
                        Estado = true,
                        UsuarioCreacion = usuarioR,
                        UsuarioModificacion = usuarioR,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _unitOfWork.PersonalSistemaPensionarioRepository.Add(personalSistemaPensionario);
                    _unitOfWork.Commit();
                }
                if (dto.PersonalSeguroSalud.IdEntidadSeguroSalud.HasValue)
                {
                    PersonalSeguroSalud personalSeguroSalud = new PersonalSeguroSalud()
                    {
                        IdEntidadSeguroSalud = dto.PersonalSeguroSalud.IdEntidadSeguroSalud.Value,
                        IdPersonal = idPersonalGlobal,
                        Estado = true,
                        UsuarioCreacion = usuarioR,
                        UsuarioModificacion = usuarioR,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Activo = true,
                    };
                    _unitOfWork.PersonalSeguroSaludRepository.Add(personalSeguroSalud);
                    _unitOfWork.Commit();
                }
                if (dto.PersonalRemuneracion.IdTipoPagoRemuneracion.HasValue)
                {
                    PersonalRemuneracion personalSeguroSalud = new PersonalRemuneracion()
                    {
                        IdTipoPagoRemuneracion = dto.PersonalRemuneracion.IdTipoPagoRemuneracion.Value,
                        IdEntidadFinanciera = dto.PersonalRemuneracion.IdEntidadFinanciera,
                        IdPersonal = idPersonalGlobal,
                        NumeroCuenta = dto.PersonalRemuneracion.NumeroCuenta,
                        Estado = true,
                        UsuarioCreacion = usuarioR,
                        UsuarioModificacion = usuarioR,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Activo = true,
                    };
                    _unitOfWork.PersonalRemuneracionRepository.Add(personalSeguroSalud);
                    _unitOfWork.Commit();
                }
                foreach (var item in dto.PersonalFormacion)
                {
                    PersonalFormacion personalFormacion = new PersonalFormacion
                    {
                        AlaActualidad = item.AlaActualidad,
                        FechaFin = item.FechaFin,
                        FechaInicio = item.FechaInicio,
                        IdAreaFormacion = item.IdAreaFormacion,
                        IdCentroEstudio = item.IdCentroEstudio,
                        IdEstadoEstudio = item.IdEstadoEstudio,
                        IdTipoEstudio = item.IdTipoEstudio,
                        Logro = item.Logro,
                        IdPersonal = idPersonalGlobal,
                        Estado = true,
                        UsuarioCreacion = usuarioR,
                        UsuarioModificacion = usuarioR,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdPersonalArchivo = item.IdPersonalArchivo
                    };
                    _unitOfWork.PersonalFormacionRepository.Add(personalFormacion);
                    _unitOfWork.Commit();

                }
                foreach (var item in dto.PersonalInformatica)
                {
                    PersonalComputo personalComputo = new PersonalComputo
                    {
                        IdCentroEstudio = item.IdCentroEstudio,
                        IdNivelCompetenciaTecnica = item.IdNivelEstudio,
                        Programa = item.Programa,
                        IdPersonal = idPersonalGlobal,
                        Estado = true,
                        UsuarioCreacion = usuarioR,
                        UsuarioModificacion = usuarioR,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdPersonalArchivo = item.IdPersonalArchivo,
                    };
                    _unitOfWork.PersonalComputoRepository.Add(personalComputo);
                    _unitOfWork.Commit();
                }
                foreach (var item in dto.PersonalIdiomas)
                {
                    PersonalIdioma personalIdioma = new PersonalIdioma
                    {
                        IdCentroEstudio = item.IdCentroEstudio,
                        IdIdioma = item.IdIdioma,
                        IdNivelIdioma = item.IdNivelIdioma,
                        IdPersonal = idPersonalGlobal,
                        Estado = true,
                        UsuarioCreacion = usuarioR,
                        UsuarioModificacion = usuarioR,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdPersonalArchivo = item.IdPersonalArchivo
                    };
                    _unitOfWork.PersonalIdiomaRepository.Add(personalIdioma);
                    _unitOfWork.Commit();

                }
                foreach (var item in dto.PersonalCertificacion)
                {
                    PersonalCertificacion personalCertificacion = new PersonalCertificacion
                    {
                        Institucion = item.Institucion,
                        Programa = item.Programa,
                        FechaCertificacion = item.FechaCertificacion,
                        IdPersonal = idPersonalGlobal,
                        Estado = true,
                        UsuarioCreacion = usuarioR,
                        UsuarioModificacion = usuarioR,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdPersonalArchivo = item.IdPersonalArchivo,
                        IdCentroEstudio = item.IdCentroEstudio
                    };
                    _unitOfWork.PersonalCertificacionRepository.Add(personalCertificacion);
                    _unitOfWork.Commit();
                }
                foreach (var item in dto.PersonalExperiencia)
                {
                    PersonalExperiencia personalExperiencia = new PersonalExperiencia
                    {
                        IdPersonal = idPersonalGlobal,
                        FechaIngreso = item.FechaIngreso,
                        FechaRetiro = item.FechaRetiro,
                        IdAreaTrabajo = item.IdAreaTrabajo,
                        IdCargo = item.IdCargo,
                        IdEmpresa = item.IdEmpresa,
                        MotivoRetiro = item.MotivoRetiro,
                        NombreJefeInmediato = item.NombreJefeInmediato,
                        TelefonoJefeInmediato = item.TelefonoJefeInmediato,
                        Estado = true,
                        UsuarioCreacion = usuarioR,
                        UsuarioModificacion = usuarioR,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdPersonalArchivo = item.IdPersonalArchivo
                    };
                    _unitOfWork.PersonalExperienciaRepository.Add(personalExperiencia);
                    _unitOfWork.Commit();

                }
                foreach (var item in dto.PersonalFamiliar)
                {
                    DatoFamiliarPersonal personalFamiliar = new DatoFamiliarPersonal
                    {
                        Apellidos = item.Apellidos,
                        Nombres = item.Nombres,
                        DerechoHabiente = item.DerechoHabiente,
                        EsContactoInmediato = item.EsContactoInmediato,
                        FechaNacimiento = item.FechaNacimiento,
                        IdParentescoPersonal = item.IdParentescoPersonal,
                        IdSexo = item.IdSexo,
                        IdTipoDocumentoPersonal = item.IdTipoDocumentoPersonal,
                        NumeroDocumento = item.NumeroDocumento,
                        NumeroReferencia1 = item.NumeroReferencia,
                        IdPersonal = idPersonalGlobal,
                        Estado = true,
                        UsuarioCreacion = usuarioR,
                        UsuarioModificacion = usuarioR,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _unitOfWork.DatoFamiliarPersonalRepository.Add(personalFamiliar);
                    _unitOfWork.Commit();

                }
                foreach (var item in dto.PersonalInformacionMedica)
                {
                    PersonalInformacionMedica personalInformacionMedica = new PersonalInformacionMedica
                    {
                        Alergia = item.Alergia,
                        IdTipoSangre = dto.Personal.IdTipoSangre,
                        Precaucion = item.Precaucion,
                        IdPersonal = idPersonalGlobal,
                        Estado = true,
                        UsuarioCreacion = usuarioR,
                        UsuarioModificacion = usuarioR,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _unitOfWork.PersonalInformacionMedicaRepository.Add(personalInformacionMedica);
                    _unitOfWork.Commit();
                }
                foreach (var item in dto.PersonalHistorialMedico)
                {
                    PersonalHistorialMedico personalHistorialMedico = new PersonalHistorialMedico
                    {
                        Enfermedad = item.Enfermedad,
                        DetalleEnfermedad = item.DetalleEnfermedad,
                        Periodo = item.Periodo,
                        IdPersonal = idPersonalGlobal,
                        Estado = true,
                        UsuarioCreacion = usuarioR,
                        UsuarioModificacion = usuarioR,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _unitOfWork.PersonalHistorialMedicoRepository.Add(personalHistorialMedico);
                    _unitOfWork.Commit();
                }


                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Actualizar(MaestroPersonalCompuestoDTO dto, string usuario)
        {
            try
            {
                //Validacion de Id Jefe null
                dto.Personal.IdJefe = dto.Personal.IdJefe != null ? dto.Personal.IdJefe : 0;
                var personal = _unitOfWork.PersonalRepository.FirstById(dto.Personal.Id);
                //Validacion de Nivel de Puesto de Trabajo
                var auxiliarNivelPuestoTrabajo = "otro";
                if (dto.Personal.IdPuestoTrabajoNivel > 0 && dto.Personal.IdPuestoTrabajoNivel != null)
                {
                    auxiliarNivelPuestoTrabajo = _unitOfWork.MaestroNivelPuestoTrabajoRepository.GetBy(x => x.Id == dto.Personal.IdPuestoTrabajoNivel.GetValueOrDefault()).Select(x => x.NivelVisualizacionAgenda).FirstOrDefault();

                }
                else if (dto.Personal.TipoPersonal != null && dto.Personal.TipoPersonal.Length > 0)
                {
                    auxiliarNivelPuestoTrabajo = _unitOfWork.MaestroNivelPuestoTrabajoRepository.GetBy(x => x.NivelVisualizacionAgenda.ToUpper().Contains(dto.Personal.TipoPersonal.ToUpper())).Select(x => x.NivelVisualizacionAgenda).FirstOrDefault();
                }
                else
                {
                    auxiliarNivelPuestoTrabajo = "otro";
                }
                var PersonalCertificacion = _unitOfWork.PersonalCertificacionRepository.GetBy(x => x.IdPersonal == dto.Personal.Id).ToList();
                var PersonalExperiencia = _unitOfWork.PersonalExperienciaRepository.GetBy(x => x.IdPersonal == dto.Personal.Id).ToList();
                var PersonalFamiliar = _unitOfWork.DatoFamiliarPersonalRepository.GetBy(x => x.IdPersonal == dto.Personal.Id).ToList();
                var PersonalFormacion = _unitOfWork.PersonalFormacionRepository.GetBy(x => x.IdPersonal == dto.Personal.Id).ToList();
                var PersonalHistorialMedico = _unitOfWork.PersonalHistorialMedicoRepository.GetBy(x => x.IdPersonal == dto.Personal.Id).ToList();
                var PersonalIdiomas = _unitOfWork.PersonalIdiomaRepository.GetBy(x => x.IdPersonal == dto.Personal.Id).ToList();
                var PersonalInformacionMedica = _unitOfWork.PersonalInformacionMedicaRepository.GetBy(x => x.IdPersonal == dto.Personal.Id).ToList();
                var PersonalInformatica = _unitOfWork.PersonalComputoRepository.GetBy(x => x.IdPersonal == dto.Personal.Id).ToList();
                var personalPuestoTrabajoSede = _unitOfWork.PersonalPuestoSedeHistoricoRepository.GetBy(x => x.IdPersonal == personal.Id && x.Actual == true).FirstOrDefault();
                var RolAnterior = personal.Rol;
                var TipoPersonalAnterior = personal.TipoPersonal == null ? "" : personal.TipoPersonal;
                int? IdJefeAnterior = personal.IdJefe;
                bool? esCerradorAnterior = personal.EsCerrador;
                int? idCerradorAnterior = personal.IdCerrador;
                var estadoCambioRolJefe = false;
                bool? estadoPersonalAnterior = personal.Activo;
                //Registro de Puesto de trabajo y Sede
                if (personal != null)
                {
                    if (personalPuestoTrabajoSede == null)
                    {
                        PersonalPuestoSedeHistorico agregar = new PersonalPuestoSedeHistorico()
                        {
                            IdPersonal = personal.Id,
                            IdPuestoTrabajo = dto.Personal.IdPuestoTrabajo.GetValueOrDefault(),
                            IdSedeTrabajo = dto.Personal.IdSede.GetValueOrDefault(),
                            Actual = true,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario
                        };
                        var resInsertar = _unitOfWork.PersonalPuestoSedeHistoricoRepository.Add(agregar);
                        _unitOfWork.Commit();

                    }
                    else
                    {
                        if (personalPuestoTrabajoSede.IdPuestoTrabajo != dto.Personal.IdPuestoTrabajo || personalPuestoTrabajoSede.IdSedeTrabajo != dto.Personal.IdSede)
                        {
                            personalPuestoTrabajoSede.FechaModificacion = DateTime.Now;
                            personalPuestoTrabajoSede.UsuarioModificacion = usuario;
                            personalPuestoTrabajoSede.Actual = false;
                            var res = _unitOfWork.PersonalPuestoSedeHistoricoRepository.Update(personalPuestoTrabajoSede);
                            _unitOfWork.Commit();

                            if (res)
                            {
                                PersonalPuestoSedeHistorico agregar = new PersonalPuestoSedeHistorico()
                                {
                                    IdPersonal = personal.Id,
                                    IdPuestoTrabajo = dto.Personal.IdPuestoTrabajo.GetValueOrDefault(),
                                    IdSedeTrabajo = dto.Personal.IdSede.GetValueOrDefault(),
                                    Actual = true,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario
                                };
                                _unitOfWork.PersonalPuestoSedeHistoricoRepository.Add(agregar);
                                _unitOfWork.Commit();
                            }
                        }
                    }
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    personal.Apellidos = dto.Personal.Apellidos;
                    personal.Rol = dto.Personal.Area;
                    personal.AreaAbrev = dto.Personal.AreaAbrev;
                    personal.TipoPersonal = auxiliarNivelPuestoTrabajo;
                    personal.IdPuestoTrabajoNivel = dto.Personal.IdPuestoTrabajoNivel;
                    personal.DistritoDireccion = dto.Personal.DistritoDireccion;
                    personal.EmailReferencia = dto.Personal.EmailReferencia;
                    personal.FechaNacimiento = dto.Personal.FechaNacimiento;
                    personal.IdCiudad = dto.Personal.IdCiudadNacimiento;
                    personal.IdRegionDireccion = dto.Personal.IdCiudadReferencia;
                    personal.IdEstadocivil = dto.Personal.IdEstadocivil;
                    personal.IdPaisNacimiento = dto.Personal.IdPaisNacimiento;
                    personal.IdPaisDireccion = dto.Personal.IdPaisReferencia;
                    personal.IdSexo = dto.Personal.IdSexo;
                    personal.IdTipoDocumento = dto.Personal.IdTipoDocumento;
                    personal.NombreDireccion = dto.Personal.NombreDireccion;
                    personal.Nombres = dto.Personal.Nombres;
                    personal.NumeroDocumento = dto.Personal.NumeroDocumento;
                    personal.FijoReferencia = dto.Personal.TelefonoFijo;
                    personal.MovilReferencia = dto.Personal.TelefonoMovil;
                    personal.UsuarioModificacion = usuario;
                    personal.FechaModificacion = DateTime.Now;
                    personal.Email = dto.Personal.Email;
                    personal.TipoPersonal = auxiliarNivelPuestoTrabajo;
                    personal.IdJefe = dto.Personal.IdJefe;
                    personal.Central = dto.Personal.Central;
                    personal.Anexo3Cx = dto.Personal.Anexo3CX;
                    personal.UrlFirmaCorreos = dto.Personal.UrlFirmaCorreos;
                    personal.Activo = dto.Personal.Activo;
                    personal.IdSistemaPensionario = dto.PersonalSistemaPensionario.IdSistemaPensionario;
                    personal.IdEntidadSistemaPensionario = dto.PersonalSistemaPensionario.IdEntidadSistemaPensionario;
                    personal.NombreCuspp = dto.PersonalSistemaPensionario.CodigoAfiliado;
                    personal.ConEssalud = dto.PersonalSeguroSalud.IdEntidadSeguroSalud.HasValue ? true : personal.ConEssalud;
                    personal.IdTipoSangre = dto.Personal.IdTipoSangre;
                    personal.EsCerrador = dto.Personal.EsCerrador;
                    personal.IdCerrador = dto.Personal.IdAsesorAsociado;
                    personal.IdPersonalArchivo = dto.Personal.IdPersonalArchivo;
                    personal.IdPersonalAreaTrabajo = dto.Personal.IdPersonalAreaTrabajo;
                    personal.IdTableroComercialCategoriaAsesor = dto.Personal.IdTableroComercialCategoriaAsesor;
                    _unitOfWork.PersonalRepository.Update(personal);
                    _unitOfWork.Commit();

                    //Inicio de insert or update in T_PersonalLog 
                    if (!(RolAnterior.ToUpper().Equals(personal.Rol.ToUpper())) || !(TipoPersonalAnterior.ToUpper().Equals(personal.TipoPersonal.ToUpper())))
                    {
                        var personalLogUpdate = _unitOfWork.PersonalLogRepository.FirstBy(x => x.IdPersonal == personal.Id && (x.EstadoRol == true || x.EstadoTipoPersonal == true) && x.FechaFin == null);
                        var personalCambioJefe = _unitOfWork.PersonalLogRepository.GetBy(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true && x.EstadoRol == false && x.EstadoTipoPersonal == false) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                        estadoCambioRolJefe = personalLogUpdate.EstadoIdJefe == true && personalLogUpdate.EstadoRol == true && personalLogUpdate.EstadoTipoPersonal == true;
                        if (estadoCambioRolJefe && personalCambioJefe == null)
                        {
                            PersonalLog personalLog = new PersonalLog();
                            personalLog.IdPersonal = personal.Id;
                            personalLog.Rol = personal.Rol;
                            personalLog.TipoPersonal = personal.TipoPersonal;
                            personalLog.IdPuestoTrabajoNivel = personal.IdPuestoTrabajoNivel;
                            personalLog.IdJefe = IdJefeAnterior;
                            personalLog.EstadoRol = false;
                            personalLog.EstadoTipoPersonal = false;
                            personalLog.EstadoIdJefe = true;
                            personalLog.FechaInicio = personalLogUpdate.FechaInicio;
                            personalLog.FechaFin = null;
                            personalLog.Estado = true;
                            personalLog.UsuarioModificacion = usuario;
                            personalLog.UsuarioCreacion = usuario;
                            personalLog.FechaCreacion = DateTime.Now;
                            personalLog.FechaModificacion = DateTime.Now;
                            _unitOfWork.PersonalLogRepository.Add(personalLog);
                            _unitOfWork.Commit();
                        }
                        personalLogUpdate.FechaFin = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 23, 59, 59);
                        personalLogUpdate.UsuarioModificacion = usuario;
                        personalLogUpdate.FechaModificacion = DateTime.Now;
                        _unitOfWork.PersonalLogRepository.Update(personalLogUpdate);
                        _unitOfWork.Commit();

                        PersonalLog personalLogBO = new PersonalLog();
                        personalLogBO.IdPersonal = personal.Id;
                        personalLogBO.Rol = personal.Rol;
                        personalLogBO.TipoPersonal = personal.TipoPersonal;
                        personalLogBO.IdPuestoTrabajoNivel = personal.IdPuestoTrabajoNivel;
                        personalLogBO.IdJefe = personal.IdJefe;
                        personalLogBO.EstadoRol = RolAnterior != personal.Rol;
                        personalLogBO.EstadoTipoPersonal = TipoPersonalAnterior != personal.TipoPersonal;
                        personalLogBO.EstadoIdJefe = false;
                        personalLogBO.FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0); ;
                        personalLogBO.FechaFin = null;
                        personalLogBO.Estado = true;
                        personalLogBO.UsuarioModificacion = usuario;
                        personalLogBO.UsuarioCreacion = usuario;
                        personalLogBO.FechaCreacion = DateTime.Now;
                        personalLogBO.FechaModificacion = DateTime.Now;

                        _unitOfWork.PersonalLogRepository.Add(personalLogBO);
                        _unitOfWork.Commit();
                    }
                    if (IdJefeAnterior != personal.IdJefe)
                    {
                        if (estadoCambioRolJefe == false)
                        {
                            var personalLogUpdate = _unitOfWork.PersonalLogRepository.GetBy(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                            var personalCambioJefe = _unitOfWork.PersonalLogRepository.GetBy(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true && x.EstadoRol == false && x.EstadoTipoPersonal == false) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                            estadoCambioRolJefe = personalLogUpdate.EstadoIdJefe == true && personalLogUpdate.EstadoRol == true && personalLogUpdate.EstadoTipoPersonal == true;
                            if (estadoCambioRolJefe && personalCambioJefe == null)
                            {
                                PersonalLog personalLog = new PersonalLog();
                                personalLog.IdPersonal = personal.Id;
                                personalLog.Rol = personal.Rol;
                                personalLog.TipoPersonal = personal.TipoPersonal;
                                personalLog.IdPuestoTrabajoNivel = personal.IdPuestoTrabajoNivel;
                                personalLog.IdJefe = IdJefeAnterior;
                                personalLog.EstadoRol = false;
                                personalLog.EstadoTipoPersonal = false;
                                personalLog.EstadoIdJefe = true;
                                personalLog.FechaInicio = personalLogUpdate.FechaInicio;
                                personalLog.FechaFin = null;
                                personalLog.Estado = true;
                                personalLog.UsuarioModificacion = usuario;
                                personalLog.UsuarioCreacion = usuario;
                                personalLog.FechaCreacion = DateTime.Now;
                                personalLog.FechaModificacion = DateTime.Now;
                                _unitOfWork.PersonalLogRepository.Add(personalLog);
                                _unitOfWork.Commit();
                            }
                        }
                        var personalLogUpdate2 = _unitOfWork.PersonalLogRepository.GetBy(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                        personalLogUpdate2.FechaFin = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 23, 59, 59);
                        personalLogUpdate2.UsuarioModificacion = usuario;
                        personalLogUpdate2.FechaModificacion = DateTime.Now;
                        _unitOfWork.PersonalLogRepository.Update(personalLogUpdate2);
                        _unitOfWork.Commit();

                        PersonalLog personalLogBO = new PersonalLog();
                        personalLogBO.IdPersonal = personal.Id;
                        personalLogBO.Rol = personal.Rol;
                        personalLogBO.TipoPersonal = personal.TipoPersonal;
                        personalLogBO.IdPuestoTrabajoNivel = personal.IdPuestoTrabajoNivel;
                        personalLogBO.IdJefe = personal.IdJefe;
                        personalLogBO.EstadoRol = false;
                        personalLogBO.EstadoTipoPersonal = false;
                        personalLogBO.EstadoIdJefe = IdJefeAnterior != personal.IdJefe;
                        personalLogBO.FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                        personalLogBO.FechaFin = null;
                        personalLogBO.Estado = true;
                        personalLogBO.UsuarioModificacion = usuario;
                        personalLogBO.UsuarioCreacion = usuario;
                        personalLogBO.FechaCreacion = DateTime.Now;
                        personalLogBO.FechaModificacion = DateTime.Now;
                        _unitOfWork.PersonalLogRepository.Add(personalLogBO);
                        _unitOfWork.Commit();
                    }
                    if (esCerradorAnterior != personal.EsCerrador || idCerradorAnterior != personal.IdCerrador)
                    {
                        var actualizarFechaAnteriorTipoAsesor = _unitOfWork.PersonalLogRepository.GetBy(x => x.IdPersonal == personal.Id && x.EstadoCerrador == true).OrderByDescending(x => x.Id).FirstOrDefault();
                        if (actualizarFechaAnteriorTipoAsesor != null)
                        {
                            actualizarFechaAnteriorTipoAsesor.FechaFin = DateTime.Now.Date;
                            _unitOfWork.PersonalLogRepository.Update(actualizarFechaAnteriorTipoAsesor);
                            _unitOfWork.Commit();
                        }
                        var personalLogActualizar = _unitOfWork.PersonalLogRepository.GetBy(x => x.IdPersonal == personal.Id).OrderByDescending(x => x.Id).FirstOrDefault();
                        if (personalLogActualizar != null)
                        {
                            PersonalLog personalLogBO = new PersonalLog();
                            personalLogBO.IdPersonal = personalLogActualizar.IdPersonal;
                            personalLogBO.Rol = personalLogActualizar.Rol;
                            personalLogBO.TipoPersonal = personalLogActualizar.TipoPersonal;
                            personalLogBO.IdPuestoTrabajoNivel = personalLogActualizar.IdPuestoTrabajoNivel;
                            personalLogBO.IdJefe = personalLogActualizar.IdJefe;
                            personalLogBO.IdCerrador = personal.IdCerrador;
                            personalLogBO.EsCerrador = personal.EsCerrador;
                            personalLogBO.EstadoCerrador = true;
                            personalLogBO.EstadoRol = false;
                            personalLogBO.EstadoTipoPersonal = false;
                            personalLogBO.EstadoIdJefe = false;
                            personalLogBO.FechaInicio = DateTime.Now.Date;
                            personalLogBO.FechaFin = null;
                            personalLogBO.Estado = personalLogActualizar.Estado;
                            personalLogBO.UsuarioCreacion = personalLogActualizar.UsuarioCreacion;
                            personalLogBO.UsuarioModificacion = personalLogActualizar.UsuarioModificacion;
                            personalLogBO.FechaCreacion = personalLogActualizar.FechaCreacion;
                            personalLogBO.FechaModificacion = personalLogActualizar.FechaModificacion;
                            _unitOfWork.PersonalLogRepository.Add(personalLogBO);
                            _unitOfWork.Commit();
                        }
                    }
                    //Fin de T_Personal Log 
                    if (dto.PersonalDireccion.EsModificado)
                    {
                        var listaPersonalDireccion = _unitOfWork.PersonalDireccionRepository.GetBy(x => x.IdPersonal == personal.Id && x.Activo == true).ToList();
                        foreach (var item in listaPersonalDireccion)
                        {
                            item.Activo = false;
                            item.UsuarioModificacion = usuario;
                            item.FechaModificacion = DateTime.Now;
                            _unitOfWork.PersonalDireccionRepository.Update(item);
                            _unitOfWork.Commit();
                        }
                        if (dto.PersonalDireccion.IdPais.HasValue)
                        {
                            PersonalDireccion personalDireccion = new PersonalDireccion
                            {
                                IdPersonal = personal.Id,
                                IdPais = dto.PersonalDireccion.IdPais,
                                IdCiudad = dto.PersonalDireccion.IdCiudad,
                                Distrito = dto.PersonalDireccion.Distrito == "" ? null : dto.PersonalDireccion.Distrito,
                                TipoVia = dto.PersonalDireccion.TipoVia == "" ? null : dto.PersonalDireccion.TipoVia,
                                TipoZonaUrbana = dto.PersonalDireccion.TipoZonaUrbana == "" ? null : dto.PersonalDireccion.TipoZonaUrbana,
                                NombreVia = dto.PersonalDireccion.NombreVia == "" ? null : dto.PersonalDireccion.NombreVia,
                                NombreZonaUrbana = dto.PersonalDireccion.NombreZonaUrbana == "" ? null : dto.PersonalDireccion.NombreZonaUrbana,
                                Manzana = dto.PersonalDireccion.Manzana == "" ? null : dto.PersonalDireccion.Manzana,
                                Lote = dto.PersonalDireccion.Lote,
                                Activo = true,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _unitOfWork.PersonalDireccionRepository.Add(personalDireccion);
                            _unitOfWork.Commit();
                        }
                    }
                    if (dto.PersonalCese.EsModificado)
                    {
                        var listaPersonalCese = _unitOfWork.PersonalCeseRepository.GetBy(x => x.IdPersonal == personal.Id);
                        foreach (var item in listaPersonalCese)
                        {
                            _unitOfWork.PersonalCeseRepository.Delete(item.Id, usuario);
                            _unitOfWork.Commit();
                        }
                        if (dto.PersonalCese.IdMotivoCese.HasValue && dto.PersonalCese.FechaCese.HasValue)
                        {
                            var ultimoContrato = _unitOfWork.DatoContratoPersonalRepository.GetBy(x => x.IdPersonal == personal.Id && x.EstadoContrato == true).OrderByDescending(x => x.FechaCreacion).FirstOrDefault();
                            if (ultimoContrato != null)
                            {
                                ultimoContrato.EstadoContrato = false;
                                ultimoContrato.IdContratoEstado = dto.PersonalCese.IdContratoEstado;
                                ultimoContrato.UsuarioModificacion = usuario;
                                ultimoContrato.FechaModificacion = DateTime.Now;
                                _unitOfWork.DatoContratoPersonalRepository.Update(ultimoContrato);
                                _unitOfWork.Commit();
                            }
                            PersonalCese personalCese = new PersonalCese
                            {
                                IdMotivoCese = dto.PersonalCese.IdMotivoCese.Value,
                                FechaCese = dto.PersonalCese.FechaCese.Value,
                                IdPersonal = personal.Id,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _unitOfWork.PersonalCeseRepository.Add(personalCese);
                            _unitOfWork.Commit();

                            var personalLogActualizarTipoAsesor = _unitOfWork.PersonalLogRepository.GetBy(x => x.IdPersonal == personal.Id && x.EstadoCerrador == true).OrderByDescending(x => x.Id).FirstOrDefault();
                            if (personalLogActualizarTipoAsesor != null && personalLogActualizarTipoAsesor.FechaFin == null)
                            {
                                personalLogActualizarTipoAsesor.FechaFin = DateTime.Now.Date;
                                _unitOfWork.PersonalLogRepository.Update(personalLogActualizarTipoAsesor);
                                _unitOfWork.Commit();
                            }
                        }
                    }
                    if (dto.PersonalDescanso.EsModificado)
                    {
                        var banderaDescansoNuevo = false;
                        var listaPersonalDescanso = _unitOfWork.PersonalMotivoTiempoInactividadRepository.GetBy(x => x.IdPersonal == personal.Id).FirstOrDefault();
                        if (listaPersonalDescanso != null)
                        {
                            if (listaPersonalDescanso.IdMotivoInactividad == dto.PersonalDescanso.IdMotivoInactividad && listaPersonalDescanso.FechaInicio == dto.PersonalDescanso.FechaInicioDescanso.GetValueOrDefault().Date && listaPersonalDescanso.FechaFin == dto.PersonalDescanso.FechaFinDescanso.GetValueOrDefault().Date && dto.PersonalDescanso.FechaInicioDescanso != null && dto.PersonalDescanso.FechaFinDescanso != null)
                            {
                                banderaDescansoNuevo = true;
                            }
                            if (!banderaDescansoNuevo)
                            {
                                _unitOfWork.PersonalMotivoTiempoInactividadRepository.Delete(listaPersonalDescanso.Id, usuario);
                                _unitOfWork.Commit();
                                PersonalMotivoTiempoInactividad personalDescanso = new PersonalMotivoTiempoInactividad
                                {
                                    IdPersonal = personal.Id,
                                    IdMotivoInactividad = dto.PersonalDescanso.IdMotivoInactividad.GetValueOrDefault(),
                                    FechaInicio = dto.PersonalDescanso.FechaInicioDescanso,
                                    FechaFin = dto.PersonalDescanso.FechaFinDescanso,
                                    Estado = true,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                _unitOfWork.PersonalMotivoTiempoInactividadRepository.Add(personalDescanso);
                                _unitOfWork.Commit();
                            }
                        }
                        else
                        {
                            PersonalMotivoTiempoInactividad personalDescanso = new PersonalMotivoTiempoInactividad
                            {
                                IdPersonal = personal.Id,
                                IdMotivoInactividad = dto.PersonalDescanso.IdMotivoInactividad.GetValueOrDefault(),
                                FechaInicio = dto.PersonalDescanso.FechaInicioDescanso,
                                FechaFin = dto.PersonalDescanso.FechaFinDescanso,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _unitOfWork.PersonalMotivoTiempoInactividadRepository.Add(personalDescanso);
                            _unitOfWork.Commit();
                        }
                    }
                    // En caso de pasar a estado Activo se elimina tiempo de inactividad de personal
                    if (estadoPersonalAnterior != null)
                    {
                        if (personal.Activo != estadoPersonalAnterior && personal.Activo == true)
                        {
                            var listaPersonalDescanso = _unitOfWork.PersonalMotivoTiempoInactividadRepository.GetBy(x => x.IdPersonal == personal.Id).FirstOrDefault();
                            if (listaPersonalDescanso != null)
                            {
                                _unitOfWork.PersonalMotivoTiempoInactividadRepository.Delete(listaPersonalDescanso.Id, usuario);
                                _unitOfWork.Commit();
                            }
                        }
                    }
                    if (dto.PersonalSistemaPensionario.EsModificado)
                    {
                        var listaSistemaPensionario = _unitOfWork.PersonalSistemaPensionarioRepository.GetBy(x => x.IdPersonal == personal.Id && x.Activo == true).ToList();
                        foreach (var item in listaSistemaPensionario)
                        {
                            item.Activo = false;
                            item.UsuarioModificacion = usuario;
                            item.FechaModificacion = DateTime.Now;
                            _unitOfWork.PersonalSistemaPensionarioRepository.Update(item);
                            _unitOfWork.Commit();
                        }
                        if (dto.PersonalSistemaPensionario.IdSistemaPensionario.HasValue)
                        {
                            PersonalSistemaPensionario personalSistemaPensionario = new PersonalSistemaPensionario
                            {
                                CodigoAfiliado = dto.PersonalSistemaPensionario.CodigoAfiliado,
                                IdEntidadSistemaPensionario = dto.PersonalSistemaPensionario.IdEntidadSistemaPensionario,
                                IdSistemaPensionario = dto.PersonalSistemaPensionario.IdSistemaPensionario.Value,
                                IdPersonal = personal.Id,
                                Activo = true,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _unitOfWork.PersonalSistemaPensionarioRepository.Add(personalSistemaPensionario);
                            _unitOfWork.Commit();
                        }
                    }
                    if (dto.PersonalSeguroSalud.EsModificado)
                    {
                        var listaSeguroSalud = _unitOfWork.PersonalSeguroSaludRepository.GetBy(x => x.IdPersonal == personal.Id && x.Activo == true).ToList();
                        foreach (var item in listaSeguroSalud)
                        {
                            item.Activo = false;
                            item.UsuarioModificacion = usuario;
                            item.FechaModificacion = DateTime.Now;
                            _unitOfWork.PersonalSeguroSaludRepository.Update(item);
                            _unitOfWork.Commit();
                        }
                        if (dto.PersonalSeguroSalud.IdEntidadSeguroSalud.HasValue)
                        {
                            PersonalSeguroSalud personalSeguroSalud = new PersonalSeguroSalud()
                            {
                                IdEntidadSeguroSalud = dto.PersonalSeguroSalud.IdEntidadSeguroSalud.Value,
                                IdPersonal = personal.Id,
                                Activo = true,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _unitOfWork.PersonalSeguroSaludRepository.Add(personalSeguroSalud);
                            _unitOfWork.Commit();
                        }
                    }
                    if (dto.PersonalRemuneracion.EsModificado)
                    {
                        var listaPersonalRemuneracion = _unitOfWork.PersonalRemuneracionRepository.GetBy(x => x.IdPersonal == personal.Id && x.Activo == true).ToList();
                        foreach (var item in listaPersonalRemuneracion)
                        {
                            item.Activo = false;
                            item.UsuarioModificacion = usuario;
                            item.FechaModificacion = DateTime.Now;
                            _unitOfWork.PersonalRemuneracionRepository.Update(item);
                            _unitOfWork.Commit();
                        }
                        if (dto.PersonalRemuneracion.IdTipoPagoRemuneracion.HasValue)
                        {
                            PersonalRemuneracion personalSeguroSalud = new PersonalRemuneracion()
                            {
                                IdTipoPagoRemuneracion = dto.PersonalRemuneracion.IdTipoPagoRemuneracion.Value,
                                IdEntidadFinanciera = dto.PersonalRemuneracion.IdEntidadFinanciera,
                                IdPersonal = personal.Id,
                                NumeroCuenta = dto.PersonalRemuneracion.NumeroCuenta,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Activo = true,
                            };
                            _unitOfWork.PersonalRemuneracionRepository.Add(personalSeguroSalud);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var item in PersonalCertificacion)
                    {
                        if (!dto.PersonalCertificacion.Any(x => x.Id == item.Id))
                        {
                            _unitOfWork.PersonalCertificacionRepository.Delete(item.Id, usuario);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var item in dto.PersonalCertificacion)
                    {
                        PersonalCertificacion personalCertificacion;
                        if (item.Id > 0)
                        {
                            personalCertificacion = _unitOfWork.PersonalCertificacionRepository.ObtenerPorId(item.Id);
                            personalCertificacion.Institucion = "";
                            personalCertificacion.Programa = item.Programa;
                            personalCertificacion.FechaCertificacion = item.FechaCertificacion;
                            personalCertificacion.UsuarioModificacion = usuario;
                            personalCertificacion.FechaModificacion = DateTime.Now;
                            personalCertificacion.IdPersonalArchivo = item.IdPersonalArchivo;
                            personalCertificacion.IdCentroEstudio = item.IdCentroEstudio;
                            _unitOfWork.PersonalCertificacionRepository.Update(personalCertificacion);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            personalCertificacion = new PersonalCertificacion
                            {
                                Institucion = "",
                                Programa = item.Programa,
                                FechaCertificacion = item.FechaCertificacion,
                                IdPersonal = dto.Personal.Id,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                IdPersonalArchivo = item.IdPersonalArchivo,
                                IdCentroEstudio = item.IdCentroEstudio,
                            };
                            _unitOfWork.PersonalCertificacionRepository.Add(personalCertificacion);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var item in PersonalExperiencia)
                    {
                        if (!dto.PersonalExperiencia.Any(x => x.Id == item.Id))
                        {
                            _unitOfWork.PersonalExperienciaRepository.Delete(item.Id, usuario);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var item in dto.PersonalExperiencia)
                    {
                        PersonalExperiencia personalExperiencia;
                        if (item.Id > 0)
                        {
                            personalExperiencia = _unitOfWork.PersonalExperienciaRepository.ObtenerPorId(item.Id);
                            personalExperiencia.FechaIngreso = item.FechaIngreso;
                            personalExperiencia.FechaRetiro = item.FechaRetiro;
                            personalExperiencia.IdAreaTrabajo = item.IdAreaTrabajo;
                            personalExperiencia.IdCargo = item.IdCargo;
                            personalExperiencia.IdEmpresa = item.IdEmpresa;
                            personalExperiencia.MotivoRetiro = item.MotivoRetiro;
                            personalExperiencia.NombreJefeInmediato = item.NombreJefeInmediato;
                            personalExperiencia.TelefonoJefeInmediato = item.TelefonoJefeInmediato;
                            personalExperiencia.UsuarioModificacion = usuario;
                            personalExperiencia.FechaModificacion = DateTime.Now;
                            personalExperiencia.IdPersonalArchivo = item.IdPersonalArchivo;
                            _unitOfWork.PersonalExperienciaRepository.Update(personalExperiencia);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            personalExperiencia = new PersonalExperiencia
                            {
                                IdPersonal = dto.Personal.Id,
                                FechaIngreso = item.FechaIngreso,
                                FechaRetiro = item.FechaRetiro,
                                IdAreaTrabajo = item.IdAreaTrabajo,
                                IdCargo = item.IdCargo,
                                IdEmpresa = item.IdEmpresa,
                                MotivoRetiro = item.MotivoRetiro,
                                NombreJefeInmediato = item.NombreJefeInmediato,
                                TelefonoJefeInmediato = item.TelefonoJefeInmediato,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                IdPersonalArchivo = item.IdPersonalArchivo,
                            };
                            _unitOfWork.PersonalExperienciaRepository.Add(personalExperiencia);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var item in PersonalFamiliar)
                    {
                        if (!dto.PersonalFamiliar.Any(x => x.Id == item.Id))
                        {
                            _unitOfWork.DatoFamiliarPersonalRepository.Delete(item.Id, usuario);
                            _unitOfWork.Commit();

                        }
                    }
                    foreach (var item in dto.PersonalFamiliar)
                    {
                        DatoFamiliarPersonal datoFamiliarPersonal;
                        if (item.Id > 0)
                        {
                            datoFamiliarPersonal = _unitOfWork.DatoFamiliarPersonalRepository.ObtenerPorId(item.Id);
                            datoFamiliarPersonal.Apellidos = item.Apellidos;
                            datoFamiliarPersonal.Nombres = item.Nombres;
                            datoFamiliarPersonal.DerechoHabiente = item.DerechoHabiente;
                            datoFamiliarPersonal.EsContactoInmediato = item.EsContactoInmediato;
                            datoFamiliarPersonal.FechaNacimiento = item.FechaNacimiento;
                            datoFamiliarPersonal.IdParentescoPersonal = item.IdParentescoPersonal;
                            datoFamiliarPersonal.IdSexo = item.IdSexo;
                            datoFamiliarPersonal.IdTipoDocumentoPersonal = item.IdTipoDocumentoPersonal;
                            datoFamiliarPersonal.NumeroDocumento = item.NumeroDocumento;
                            datoFamiliarPersonal.NumeroReferencia1 = item.NumeroReferencia;
                            datoFamiliarPersonal.UsuarioModificacion = usuario;
                            datoFamiliarPersonal.FechaModificacion = DateTime.Now;
                            _unitOfWork.DatoFamiliarPersonalRepository.Update(datoFamiliarPersonal);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            datoFamiliarPersonal = new DatoFamiliarPersonal
                            {
                                Apellidos = item.Apellidos,
                                Nombres = item.Nombres,
                                DerechoHabiente = item.DerechoHabiente,
                                EsContactoInmediato = item.EsContactoInmediato,
                                FechaNacimiento = item.FechaNacimiento,
                                IdParentescoPersonal = item.IdParentescoPersonal,
                                IdSexo = item.IdSexo,
                                IdTipoDocumentoPersonal = item.IdTipoDocumentoPersonal,
                                NumeroDocumento = item.NumeroDocumento,
                                NumeroReferencia1 = item.NumeroReferencia,
                                IdPersonal = dto.Personal.Id,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _unitOfWork.DatoFamiliarPersonalRepository.Add(datoFamiliarPersonal);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var item in PersonalFormacion)
                    {
                        if (!dto.PersonalFormacion.Any(x => x.Id == item.Id))
                        {
                            _unitOfWork.PersonalFormacionRepository.Delete(item.Id, usuario);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var item in dto.PersonalFormacion)
                    {
                        PersonalFormacion personalFormacion;
                        if (item.Id > 0)
                        {
                            personalFormacion = _unitOfWork.PersonalFormacionRepository.ObtenerPorId(item.Id);
                            personalFormacion.AlaActualidad = item.AlaActualidad;
                            personalFormacion.FechaFin = item.FechaFin;
                            personalFormacion.FechaInicio = item.FechaInicio;
                            personalFormacion.IdAreaFormacion = item.IdAreaFormacion;
                            personalFormacion.IdCentroEstudio = item.IdCentroEstudio;
                            personalFormacion.IdEstadoEstudio = item.IdEstadoEstudio;
                            personalFormacion.IdTipoEstudio = item.IdTipoEstudio;
                            personalFormacion.Logro = item.Logro;
                            personalFormacion.UsuarioModificacion = usuario;
                            personalFormacion.FechaModificacion = DateTime.Now;
                            personalFormacion.IdPersonalArchivo = item.IdPersonalArchivo;
                            _unitOfWork.PersonalFormacionRepository.Update(personalFormacion);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            personalFormacion = new PersonalFormacion
                            {
                                AlaActualidad = item.AlaActualidad,
                                FechaFin = item.FechaFin,
                                FechaInicio = item.FechaInicio,
                                IdAreaFormacion = item.IdAreaFormacion,
                                IdCentroEstudio = item.IdCentroEstudio,
                                IdEstadoEstudio = item.IdEstadoEstudio,
                                IdTipoEstudio = item.IdTipoEstudio,
                                Logro = item.Logro,
                                IdPersonal = dto.Personal.Id,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                IdPersonalArchivo = item.IdPersonalArchivo,
                            };
                            _unitOfWork.PersonalFormacionRepository.Add(personalFormacion);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var item in PersonalHistorialMedico)
                    {
                        if (!dto.PersonalHistorialMedico.Any(x => x.Id == item.Id))
                        {
                            _unitOfWork.PersonalHistorialMedicoRepository.Delete(item.Id, usuario);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var item in dto.PersonalHistorialMedico)
                    {
                        PersonalHistorialMedico personalHistorialMedico;
                        if (item.Id > 0)
                        {
                            personalHistorialMedico = _unitOfWork.PersonalHistorialMedicoRepository.ObtenerPorId(item.Id);
                            personalHistorialMedico.Enfermedad = item.Enfermedad;
                            personalHistorialMedico.DetalleEnfermedad = item.DetalleEnfermedad;
                            personalHistorialMedico.Periodo = item.Periodo;
                            personalHistorialMedico.UsuarioModificacion = usuario;
                            personalHistorialMedico.FechaModificacion = DateTime.Now;
                            _unitOfWork.PersonalHistorialMedicoRepository.Update(personalHistorialMedico);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            personalHistorialMedico = new PersonalHistorialMedico
                            {
                                Enfermedad = item.Enfermedad,
                                DetalleEnfermedad = item.DetalleEnfermedad,
                                Periodo = " ",
                                IdPersonal = dto.Personal.Id,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _unitOfWork.PersonalHistorialMedicoRepository.Add(personalHistorialMedico);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var item in PersonalIdiomas)
                    {
                        if (!dto.PersonalIdiomas.Any(x => x.Id == item.Id))
                        {
                            _unitOfWork.PersonalIdiomaRepository.Delete(item.Id, usuario);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var item in dto.PersonalIdiomas)
                    {
                        PersonalIdioma personalIdioma;
                        if (item.Id > 0)
                        {
                            personalIdioma = _unitOfWork.PersonalIdiomaRepository.ObtenerPorId(item.Id);
                            personalIdioma.IdCentroEstudio = item.IdCentroEstudio;
                            personalIdioma.IdIdioma = item.IdIdioma;
                            personalIdioma.IdNivelIdioma = item.IdNivelIdioma;
                            personalIdioma.UsuarioModificacion = usuario;
                            personalIdioma.FechaModificacion = DateTime.Now;
                            personalIdioma.IdPersonalArchivo = item.IdPersonalArchivo;
                            _unitOfWork.PersonalIdiomaRepository.Update(personalIdioma);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            personalIdioma = new PersonalIdioma
                            {
                                IdCentroEstudio = item.IdCentroEstudio,
                                IdIdioma = item.IdIdioma,
                                IdNivelIdioma = item.IdNivelIdioma,
                                IdPersonal = dto.Personal.Id,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                IdPersonalArchivo = item.IdPersonalArchivo,
                            };
                            _unitOfWork.PersonalIdiomaRepository.Add(personalIdioma);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var item in PersonalInformacionMedica)
                    {
                        if (!dto.PersonalInformacionMedica.Any(x => x.Id == item.Id))
                        {
                            _unitOfWork.PersonalInformacionMedicaRepository.Delete(item.Id, usuario);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var item in dto.PersonalInformacionMedica)
                    {
                        PersonalInformacionMedica personalInformacionMedica;
                        if (item.Id > 0)
                        {
                            personalInformacionMedica = _unitOfWork.PersonalInformacionMedicaRepository.ObtenerPorId(item.Id);
                            personalInformacionMedica.Alergia = item.Alergia;
                            personalInformacionMedica.IdTipoSangre = dto.Personal.IdTipoSangre;
                            personalInformacionMedica.Precaucion = item.Precaucion;
                            personalInformacionMedica.UsuarioModificacion = usuario;
                            personalInformacionMedica.FechaModificacion = DateTime.Now;
                            _unitOfWork.PersonalInformacionMedicaRepository.Update(personalInformacionMedica);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            personalInformacionMedica = new PersonalInformacionMedica
                            {
                                Alergia = item.Alergia,
                                IdTipoSangre = dto.Personal.IdTipoSangre,
                                Precaucion = item.Precaucion,
                                IdPersonal = dto.Personal.Id,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _unitOfWork.PersonalInformacionMedicaRepository.Add(personalInformacionMedica);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var item in PersonalInformatica)
                    {
                        if (!dto.PersonalInformatica.Any(x => x.Id == item.Id))
                        {
                            _unitOfWork.PersonalComputoRepository.Delete(item.Id, usuario);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var item in dto.PersonalInformatica)
                    {
                        PersonalComputo personalComputo;
                        if (item.Id > 0)
                        {
                            personalComputo = _unitOfWork.PersonalComputoRepository.ObtenerPorId(item.Id);
                            personalComputo.IdCentroEstudio = item.IdCentroEstudio;
                            personalComputo.IdNivelCompetenciaTecnica = item.IdNivelEstudio;
                            personalComputo.Programa = item.Programa;
                            personalComputo.UsuarioModificacion = usuario;
                            personalComputo.FechaModificacion = DateTime.Now;
                            personalComputo.IdPersonalArchivo = item.IdPersonalArchivo;
                            _unitOfWork.PersonalComputoRepository.Update(personalComputo);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            personalComputo = new PersonalComputo
                            {
                                IdCentroEstudio = item.IdCentroEstudio,
                                IdNivelCompetenciaTecnica = item.IdNivelEstudio,
                                Programa = item.Programa,
                                IdPersonal = dto.Personal.Id,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                IdPersonalArchivo = item.IdPersonalArchivo,
                            };
                            _unitOfWork.PersonalComputoRepository.Add(personalComputo);
                            _unitOfWork.Commit();
                        }
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ArchivoDTO ObtenerArchivoPersonal(int idPersonalArchivo)
        {
            try
            {

                string htmlArchivo = string.Empty;
                var informacionArchivo = _unitOfWork.PersonalArchivoRepository.ObtenerPorId(idPersonalArchivo);
                PersonalArchivoDTO personalArchivoDTO = _mapper.Map<PersonalArchivoDTO>(informacionArchivo);
                if (personalArchivoDTO != null)
                {
                    if (personalArchivoDTO.EsImagen.GetValueOrDefault())
                    {
                        htmlArchivo = "<img src='" + personalArchivoDTO.RutaArchivo + "'style='max-width:500px'>";
                    }
                    else
                    {
                        htmlArchivo = "<a href='" + personalArchivoDTO.RutaArchivo + "' target='_blank'>Descargar</a>";
                    }
                    ArchivoDTO resultado = new ArchivoDTO()
                    {
                        Respuesta = true,
                        Datos = personalArchivoDTO,
                        Html = htmlArchivo,
                        Mensaje = "Información cargada correctamente"
                    };
                    return resultado;
                }
                else
                {
                    ArchivoDTO resultado = new ArchivoDTO()
                    {
                        Respuesta = false,
                        Datos = null,
                        Html = "",
                        Mensaje = "Error: No se encontró el archivo solicitado"
                    };
                    return resultado;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DescargarArchivoDTO DescargarArchivoPersonal(int idPersonalArchivo)
        {
            try
            {

                var informacionArchivo = _unitOfWork.PersonalArchivoRepository.ObtenerPorId(idPersonalArchivo);
                PersonalArchivoDTO personalArchivoDTO = _mapper.Map<PersonalArchivoDTO>(informacionArchivo);
                if (personalArchivoDTO != null)
                {
                    string base64String = string.Empty;
                    if (personalArchivoDTO.EsImagen.GetValueOrDefault())
                    {
                        using (WebClient webClient = new WebClient())
                        {
                            Image image;
                            using (Stream stream = webClient.OpenRead(personalArchivoDTO.RutaArchivo))
                            {
                                image = Image.FromStream(stream);
                            }
                            using (MemoryStream m = new MemoryStream())
                            {
                                image.Save(m, image.RawFormat);
                                byte[] imageBytes = m.ToArray();
                                base64String = Convert.ToBase64String(imageBytes);
                            }
                            DescargarArchivoDTO resultado = new DescargarArchivoDTO()
                            {
                                Respuesta = true,
                                EsImagen = true,
                                RutaArchivo = base64String,
                                Datos = personalArchivoDTO,
                                Mensaje = "Información cargada correctamente"
                            };
                            return resultado;

                        }
                    }
                    else
                    {
                        using (WebClient client = new WebClient())
                        {
                            var bytes = client.DownloadData(personalArchivoDTO.RutaArchivo);
                            base64String = Convert.ToBase64String(bytes);
                        }
                        DescargarArchivoDTO resultado = new DescargarArchivoDTO()
                        {
                            Respuesta = true,
                            EsImagen = false,
                            RutaArchivo = base64String,
                            Datos = personalArchivoDTO,
                            Mensaje = "Información cargada correctamente"
                        };
                        return resultado;

                    }
                }
                else
                {
                    DescargarArchivoDTO resultado = new DescargarArchivoDTO()
                    {
                        Respuesta = false,
                        EsImagen = false,
                        RutaArchivo = null,
                        Datos = null,
                        Mensaje = "Error: No se encontró el archivo solicitado"
                    };
                    return resultado;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (_unitOfWork.PersonalRepository.Exist(id))
                {
                    var personalCertificacion = _unitOfWork.PersonalCertificacionRepository.GetBy(x => x.IdPersonal == id).ToList();
                    var personalExperiencia = _unitOfWork.PersonalExperienciaRepository.GetBy(x => x.IdPersonal == id).ToList();
                    var personalFamiliar = _unitOfWork.DatoFamiliarPersonalRepository.GetBy(x => x.IdPersonal == id).ToList();
                    var personalFormacion = _unitOfWork.PersonalFormacionRepository.GetBy(x => x.IdPersonal == id).ToList();
                    var personalHistorialMedico = _unitOfWork.PersonalHistorialMedicoRepository.GetBy(x => x.IdPersonal == id).ToList();
                    var personalIdiomas = _unitOfWork.PersonalIdiomaRepository.GetBy(x => x.IdPersonal == id).ToList();
                    var personalInformacionMedica = _unitOfWork.PersonalInformacionMedicaRepository.GetBy(x => x.IdPersonal == id).ToList();
                    var personalInformatica = _unitOfWork.PersonalComputoRepository.GetBy(x => x.IdPersonal == id).ToList();

                    var personalSeguroSalud = _unitOfWork.PersonalSeguroSaludRepository.FirstBy(x => x.IdPersonal == id && x.Activo == true);
                    var personalSistemaPensionario = _unitOfWork.PersonalSistemaPensionarioRepository.FirstBy(x => x.IdPersonal == id && x.Activo == true);

                    var personalRemuneracion = _unitOfWork.PersonalRemuneracionRepository.FirstBy(x => x.IdPersonal == id && x.Activo == true);

                    if (personalRemuneracion != null)
                    {
                        _unitOfWork.PersonalRemuneracionRepository.Delete(personalRemuneracion.Id, usuario);
                        _unitOfWork.Commit();
                    }

                    if (personalSeguroSalud != null)
                    {
                        _unitOfWork.PersonalSeguroSaludRepository.Delete(personalSeguroSalud.Id, usuario);
                        _unitOfWork.Commit();
                    }

                    if (personalSistemaPensionario != null)
                    {
                        _unitOfWork.PersonalSistemaPensionarioRepository.Delete(personalSistemaPensionario.Id, usuario);
                        _unitOfWork.Commit();
                    }

                    foreach (var item in personalCertificacion)
                    {
                        _unitOfWork.PersonalCertificacionRepository.Delete(item.Id, usuario);
                        _unitOfWork.Commit();
                    }
                    foreach (var item in personalExperiencia)
                    {
                        _unitOfWork.PersonalExperienciaRepository.Delete(item.Id, usuario);
                        _unitOfWork.Commit();
                    }
                    foreach (var item in personalFamiliar)
                    {
                        _unitOfWork.DatoFamiliarPersonalRepository.Delete(item.Id, usuario);
                        _unitOfWork.Commit();
                    }
                    foreach (var item in personalFormacion)
                    {
                        _unitOfWork.PersonalFormacionRepository.Delete(item.Id, usuario);
                        _unitOfWork.Commit();
                    }
                    foreach (var item in personalHistorialMedico)
                    {
                        _unitOfWork.PersonalHistorialMedicoRepository.Delete(item.Id, usuario);
                        _unitOfWork.Commit();
                    }
                    foreach (var item in personalIdiomas)
                    {
                        _unitOfWork.PersonalIdiomaRepository.Delete(item.Id, usuario);
                        _unitOfWork.Commit();
                    }
                    foreach (var item in personalInformacionMedica)
                    {
                        _unitOfWork.PersonalInformacionMedicaRepository.Delete(item.Id, usuario);
                        _unitOfWork.Commit();
                    }
                    foreach (var item in personalInformatica)
                    {
                        _unitOfWork.PersonalComputoRepository.Delete(item.Id, usuario);
                        _unitOfWork.Commit();
                    }

                    try
                    {
                        _unitOfWork.PersonalAccesoTemporalAulaVirtualRepository.EliminarAccesoTemporalPorIdPersonal(id, usuario);
                        _unitOfWork.Commit();

                    }
                    catch (Exception e)
                    {
                    }

                    var respuesta = _unitOfWork.PersonalRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<MaestroPersonalGrupoAccesoTemporalDTO> ActualizarAccesoTemporal(ActualizarAccesoTemporalDTO dto, string usuario)
        {
            try
            {
                if (dto.FechaInicio > dto.FechaFin)
                {
                    throw new BadRequestException($"La fecha de fin del acceso temporal debe ser mayor o igual a la fecha de inicio");
                }
                string emailPersonalSolicitado = string.Empty;

                var personalSolicitado = _unitOfWork.PersonalRepository.FirstBy(x => x.Id == dto.IdPersonal);
                if (personalSolicitado == null)
                    throw new BadRequestException($"El personal al que se desea dar acceso temporal no existe");
                else
                    emailPersonalSolicitado = personalSolicitado.Email;

                var pEspecificoPadre = _unitOfWork.PEspecificoRepository.FirstBy(x => x.Id == dto.IdPEspecificoPadre);
                if (pEspecificoPadre == null)
                    throw new BadRequestException($"No existe el programa especifico");

                if (!_unitOfWork.IntegraAspNetUserRepository.ExistePorNombreUsuario(usuario))
                    throw new BadRequestException($"El usuario no existe");

                List<MaestroPersonalGrupoAccesoTemporalDTO> listaAccesoTemporal = new List<MaestroPersonalGrupoAccesoTemporalDTO>();
                ReemplazoEtiquetaPlantillaDTO reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaDTO();
                // Email del usuario que realiza la modificacion
                string usuarioResponsable = _unitOfWork.IntegraAspNetUserRepository.ObtenerEmailPorNombreUsuario(usuario);

                bool resultadoActualizacion = ActualizarAccesosTemporalesIntegra(dto, usuario);

                if (!resultadoActualizacion)
                    throw new BadRequestException($"Hubo un fallo en la actualizacion de los accesos temporales");
                else
                {
                    var alumnoPersonal = _unitOfWork.AlumnoRepository.FirstBy(x => x.Email1 == personalSolicitado.Email);

                    if (alumnoPersonal == null)
                        throw new BadRequestException($"No se ha encontrado un alumno con el correo del personal");

                    EtiquetaParametroAlumnoSinOportunidadDTO parametrosEtiquetas = new EtiquetaParametroAlumnoSinOportunidadDTO
                    {
                        IdAlumno = alumnoPersonal.Id,
                        IdPGeneral = pEspecificoPadre.IdProgramaGeneral
                    };

                    reemplazoEtiquetaPlantilla.IdPlantilla = ValorEstatico.IdPlantillaAccesoTemporalPersonalMailing;


                    ReemplazarEtiquetasAlumnoSinOportunidad(parametrosEtiquetas, reemplazoEtiquetaPlantilla);



                    PlantillaEmailMandrillDTO emailFinalEnvio = reemplazoEtiquetaPlantilla.EmailReemplazado;





                    List<string> correosPersonalizados = new List<string>
                    {
                        alumnoPersonal.Email1
                    };

                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = "matriculas@bsginstitute.com",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = emailFinalEnvio.Asunto,
                        Message = emailFinalEnvio.CuerpoHTML,
                        Cc = usuarioResponsable,
                        Bcc = "gmiranda@bsginstitute.com",
                        AttachedFiles = null
                    };

                    MailerService mailService = new MailerService();
                    mailService.SetData(mailDataPersonalizado);

                    List<TMKMensajeIdDTO> listaIdsMailChimp = mailService.SendMessageTask();
                    List<MandrilEnvioCorreo> listaMandrilEnvioCorreoBO = new List<MandrilEnvioCorreo>();

                    foreach (var mensaje in listaIdsMailChimp)
                    {
                        var mandrilEnvioCorreoBO = new MandrilEnvioCorreo
                        {
                            IdOportunidad = null,
                            IdPersonal = personalSolicitado.Id,
                            IdAlumno = alumnoPersonal.Id,
                            IdCentroCosto = pEspecificoPadre.IdCentroCosto,
                            IdMandrilTipoAsignacion = 1, //Correos enviados automaticos
                            EstadoEnvio = 1,
                            IdMandrilTipoEnvio = 1, // Correo enviado automaticamente
                            FechaEnvio = DateTime.Now,
                            Asunto = emailFinalEnvio.Asunto,
                            FkMandril = mensaje.MensajeId,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = "EnvioAutomaticoGP",
                            UsuarioModificacion = "EnvioAutomaticoGP",
                            EsEnvioMasivo = false
                        };
                        listaMandrilEnvioCorreoBO.Add(mandrilEnvioCorreoBO);
                        _unitOfWork.Commit();
                    }

                    _unitOfWork.MandrilEnvioCorreoRepository.Add(listaMandrilEnvioCorreoBO);
                    _unitOfWork.Commit();
                }

                listaAccesoTemporal = ObtenerListaAccesoTemporal(dto.IdPersonal);


                return listaAccesoTemporal;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool ActualizarAccesosTemporalesIntegra(ActualizarAccesoTemporalDTO datosAccesoTemporal, string usuario)
        {
            try
            {
                bool resultado = false;
                Alumno Alumno = new Alumno();
                var IdPortalWeb = "";
                TAlumno resultadoAlumno;
                resultado = _unitOfWork.PersonalAccesoTemporalAulaVirtualRepository.ActualizarAccesosTemporalesIntegra(datosAccesoTemporal);


                if (!resultado)
                {
                    return resultado;
                }

                Personal personal = new Personal();
                personal = _unitOfWork.PersonalRepository.ObtenerPorId(datosAccesoTemporal.IdPersonal);



                // Verificacion y/o creacion de persona
                Persona Persona = _unitOfWork.PersonaRepository.ObtenerPorEmail(personal.Email);

                Persona PersonaEntidad = new Persona();
                ClasificacionPersona clasificacionPersonatmp;
                bool tmpindicador = false;
                if (Persona != null)
                {
                    PersonaEntidad = Persona;
                    tmpindicador = true;


                }
                if (Persona == null)
                {
                    PersonaEntidad.Email1 = personal.Email;
                    PersonaEntidad.Estado = true;
                    PersonaEntidad.UsuarioCreacion = "AccesoTemporalGP";
                    PersonaEntidad.UsuarioModificacion = "AccesoTemporalGP";
                    PersonaEntidad.FechaCreacion = DateTime.Now;
                    PersonaEntidad.FechaModificacion = DateTime.Now;

                    PersonaEntidad = _mapper.Map<Persona>(_unitOfWork.PersonaRepository.Add(PersonaEntidad)); ;
                    tmpindicador = false;
                    _unitOfWork.Commit();

                }

                clasificacionPersonatmp = _unitOfWork.ClasificacionPersonaRepository.ObtenerPorPersonalYTipoPersona(PersonaEntidad.Id, 1);


                if (clasificacionPersonatmp == null)
                {
                    clasificacionPersonatmp = new ClasificacionPersona();

                    Alumno = _unitOfWork.AlumnoRepository.ObtenerPorEmail1(personal.Email);

                    if (Alumno != null)
                    {
                        clasificacionPersonatmp.IdPersona = PersonaEntidad.Id;
                        clasificacionPersonatmp.IdTipoPersona = 1;/*Tipo Alumno*/
                        clasificacionPersonatmp.IdTablaOriginal = Alumno.Id;
                        clasificacionPersonatmp.Estado = true;
                        clasificacionPersonatmp.UsuarioCreacion = "AccesoTemporalGP";
                        clasificacionPersonatmp.UsuarioModificacion = "AccesoTemporalGP";
                        clasificacionPersonatmp.FechaCreacion = DateTime.Now;
                        clasificacionPersonatmp.FechaModificacion = DateTime.Now;
                        TClasificacionPersona resultClasificacion;
                        try
                        {
                            resultClasificacion = _unitOfWork.ClasificacionPersonaRepository.Add(clasificacionPersonatmp);
                            _unitOfWork.Commit();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Comunicarse con sistemas para validar los datos del alumno");
                        }

                        clasificacionPersonatmp.IdMigracion = resultClasificacion.Id;

                        // Otra consulta por el cambio en el campo RowVersion
                        clasificacionPersonatmp = _unitOfWork.ClasificacionPersonaRepository.ObtenerPorId(resultClasificacion.Id);


                    }
                }

                Alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(clasificacionPersonatmp.IdTablaOriginal);

                if (Alumno == null)
                {
                    Alumno = new Alumno();

                    //Nombres
                    var nombres = personal.Nombres.Split(new char[] { ' ' }).ToList().Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();

                    if (nombres.Count == 1)
                    {
                        Alumno.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                        Alumno.Nombre2 = string.Empty;
                    }
                    else if (nombres.Count == 2)
                    {
                        Alumno.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                        Alumno.Nombre2 = nombres[1].Length >= 100 ? nombres[1].Substring(0, 100) : nombres[1];
                    }
                    else if (nombres.Count > 2)
                    {
                        Alumno.Nombre1 = string.Join(" ", nombres.ToArray()).Length >= 100 ? String.Join(" ", nombres.ToArray()).Substring(0, 100) : String.Join(" ", nombres.ToArray());
                        Alumno.Nombre2 = string.Empty;
                    }

                    //Apellidos
                    personal.Apellidos = personal.Apellidos ?? string.Empty;

                    var apellidos = personal.Apellidos.Split(new char[] { ' ' }).ToList().Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();

                    if (apellidos.Count == 1)
                    {
                        Alumno.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                        Alumno.ApellidoMaterno = string.Empty;
                    }
                    else if (apellidos.Count == 2)
                    {
                        Alumno.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                        Alumno.ApellidoMaterno = apellidos[1].Length >= 100 ? apellidos[1].Substring(0, 100) : apellidos[1];
                    }
                    else if (apellidos.Count > 2)
                    {
                        Alumno.ApellidoPaterno = String.Join(" ", apellidos.ToArray()).Length >= 100 ? String.Join(" ", apellidos.ToArray()).Substring(0, 100) : String.Join(" ", apellidos.ToArray());
                        Alumno.ApellidoMaterno = string.Empty;
                    }
                    else
                    {
                        Alumno.ApellidoPaterno = string.Empty;
                        Alumno.ApellidoMaterno = string.Empty;
                    }

                    Alumno.IdAformacion = 3/*Sin area de formacion*/;
                    Alumno.IdAtrabajo = 3/*Sin area de trabajo*/;
                    Alumno.IdCargo = 11/*Sin cargo*/;
                    Alumno.IdIndustria = 48/*Sin industria*/;
                    Alumno.Celular = "955456433";
                    Alumno.IdCodigoRegionCiudad = null;
                    Alumno.IdCodigoPais = 51/*Peru*/;
                    Alumno.Telefono = string.Empty;
                    Alumno.Email1 = personal.Email;
                    Alumno.Email2 = personal.Email;
                    Alumno.Estado = true;
                    Alumno.UsuarioCreacion = "GestionPersonal";
                    Alumno.UsuarioModificacion = "SYSTEM";
                    Alumno.FechaModificacion = DateTime.Now;
                    Alumno.FechaCreacion = DateTime.Now;
                    Alumno.IdEstadoContactoWhatsApp = 3;
                    resultadoAlumno = _unitOfWork.AlumnoRepository.Add(Alumno);
                    _unitOfWork.Commit();

                    if (resultadoAlumno == null)
                        return false;

                    clasificacionPersonatmp.IdPersona = PersonaEntidad.Id;
                    clasificacionPersonatmp.IdTipoPersona = 1;/*Tipo Alumno*/
                    clasificacionPersonatmp.IdTablaOriginal = resultadoAlumno.Id;
                    clasificacionPersonatmp.Estado = true;
                    clasificacionPersonatmp.UsuarioCreacion = "AccesoTemporalGP";
                    clasificacionPersonatmp.UsuarioModificacion = "AccesoTemporalGP";
                    clasificacionPersonatmp.FechaCreacion = DateTime.Now;
                    clasificacionPersonatmp.FechaModificacion = DateTime.Now;

                    _unitOfWork.ClasificacionPersonaRepository.Add(clasificacionPersonatmp);
                    _unitOfWork.Commit();

                    clasificacionPersonatmp.IdMigracion = clasificacionPersonatmp.Id;

                    // Otra consulta por el cambio en el campo RowVersion
                    clasificacionPersonatmp = _unitOfWork.ClasificacionPersonaRepository.ObtenerPorId(clasificacionPersonatmp.Id);

                    try
                    {
                        _unitOfWork.ClasificacionPersonaRepository.Update(clasificacionPersonatmp);
                        _unitOfWork.Commit();
                    }
                    catch (Exception)
                    {
                        throw new Exception("Comunicarse con sistemas para validar los datos del alumno");
                    }
                }

                if (Alumno.Email1 != PersonaEntidad.Email1)
                    throw new Exception("Comunicarse con sistemas para validar los datos del alumno");

                var registroPortalWeb = _unitOfWork.PersonalAccesoTemporalAulaVirtualRepository.ObtenerDatosBasicosPortalWebUsername(personal.Email);

                if (registroPortalWeb != null)
                {
                    IdPortalWeb = registroPortalWeb.IdUsuarioPortalWeb;

                    if (Alumno.Id != registroPortalWeb.IdAlumno)
                        _unitOfWork.PersonalAccesoTemporalAulaVirtualRepository.ActualizarIdAlumnoUsuarioPortalWeb(registroPortalWeb.IdUsuarioPortalWeb, Alumno.Id);
                }

                if (string.IsNullOrEmpty(IdPortalWeb))
                {
                    /*Logica para crear el contacto*/
                    if (string.IsNullOrEmpty(IdPortalWeb))
                    {
                        var credencialesIntegra = _unitOfWork.IntegraAspNetUserRepository.ObtenerPorId(personal.Id);

                        string claveIntegra = credencialesIntegra.UsClave;
                        string claveHash = string.Empty;
                        claveHash = CryptoService.HashPassword(claveIntegra);

                        var resultadoAspNetUsers = _unitOfWork.MontoPagoCronogramaRepository.CrearUsuarioClavePortalWeb(Alumno.Id, personal.Email, claveIntegra, claveHash, personal.Nombres, personal.Apellidos, Alumno.Telefono, Alumno.Celular, Alumno.IdCodigoRegionCiudad, Alumno.IdCodigoPais, DateTime.Now);

                        IdPortalWeb = _unitOfWork.PersonalAccesoTemporalAulaVirtualRepository.ObtenerIdUsuarioPortalWebCorreo(personal.Email);
                    }
                }

                bool resultadoPortalWeb = _unitOfWork.PersonalAccesoTemporalAulaVirtualRepository.ActualizarAccesosTemporalesPortalWeb(personal.Id, IdPortalWeb, Alumno.Id);

                if (!resultadoPortalWeb)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        public void ReemplazarEtiquetasAlumnoSinOportunidad(EtiquetaParametroAlumnoSinOportunidadDTO parametrosEtiquetasOpcionales, ReemplazoEtiquetaPlantillaDTO reemplazoEtiquetaPlantilla)
        {
            try
            {
                int IdPlantillaBase = 0;
                // Declaracion de parametros e inicializaciones
                var alumno = _unitOfWork.AlumnoRepository.FirstBy(x => x.Id == parametrosEtiquetasOpcionales.IdAlumno);

                if (alumno == null)
                    throw new Exception("Alumno no existente");

                var plantilla = _unitOfWork.PlantillaRepository.FirstBy(x => x.Id == reemplazoEtiquetaPlantilla.IdPlantilla);

                if (plantilla == null)
                    throw new Exception("Plantilla no existente");

                if (_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                    IdPlantillaBase = plantilla.IdPlantillaBase;
                else
                    throw new Exception("Plantilla base no existente");

                List<DatoPlantillaWhatsAppDTO> listaObjetoWhasApp = new List<DatoPlantillaWhatsAppDTO>();
                PlantillaAsuntoCuerpoDTO plantillaBase = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(reemplazoEtiquetaPlantilla.IdPlantilla);

                List<string> listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();
                foreach (string etiqueta in listaEtiqueta)
                {
                    listaObjetoWhasApp.Add(new DatoPlantillaWhatsAppDTO() { Codigo = string.Concat("{", etiqueta, "}"), Texto = string.Empty });
                }

                // Inicio logica intercambio
                #region DatosPGeneral
                if (parametrosEtiquetasOpcionales.IdPGeneral.HasValue)
                {
                    var pGeneral = _unitOfWork.PGeneralRepository.FirstBy(x => x.Id == parametrosEtiquetasOpcionales.IdPGeneral);

                    if (pGeneral == null)
                    {
                        throw new Exception("El programa general no existe");
                    }

                    if (plantillaBase.Cuerpo.Contains("{tPgeneral.Nombre}"))
                    {
                        var valor = pGeneral.Nombre;

                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPgeneral.Nombre}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.Codigo.Equals("{tPgeneral.Nombre}")).FirstOrDefault().Texto = valor;
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.Nombre}"))
                    {
                        var valor = pGeneral.Nombre;

                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.Nombre}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.Codigo.Equals("{tPLA_PGeneral.Nombre}")).FirstOrDefault().Texto = valor;
                        }
                    }
                }
                #endregion

                #region DatosAlumno
                if (plantillaBase.Cuerpo.Contains("{tAlumno.nombre1}"))
                {
                    var valorFormateado = alumno.Nombre1;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumno.nombre1}", valorFormateado);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{tAlumno.nombre1}")).FirstOrDefault().Texto = valorFormateado;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.nombre1}"))
                {
                    var valorFormateado = alumno.Nombre1;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.nombre1}", valorFormateado);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().Texto = valorFormateado;
                    }
                }

                var listaAccesoPortalWeb = new List<string>()
                {
                    "{T_Alumno.UsuarioPortalWeb}",
                    "{T_Alumno.ClavePortalWeb}"
                };

                if (listaAccesoPortalWeb.Any(plantillaBase.Cuerpo.Contains))
                {
                    CredencialesPortalWebAlumnoDTO accesoPortalWeb = _unitOfWork.AlumnoRepository.ObtenerCredencialesPortalWebPorIdAlumno(alumno.Id);

                    if (plantillaBase.Cuerpo.Contains("{T_Alumno.UsuarioPortalWeb}"))
                    {
                        var valor = accesoPortalWeb.PortalWebUsuario;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.UsuarioPortalWeb}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_Alumno.UsuarioPortalWeb}")).FirstOrDefault().Texto = valor;
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_Alumno.ClavePortalWeb}"))
                    {
                        var valor = accesoPortalWeb.PortalWebClave;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.ClavePortalWeb}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_Alumno.ClavePortalWeb}")).FirstOrDefault().Texto = valor;
                        }
                    }
                }
                #endregion
                PlantillaEmailMandrillDTO plantilladto;
                if (reemplazoEtiquetaPlantilla == null)
                {
                    throw new Exception("Error: reemplazoEtiquetaPlantilla es NULL");
                }
                if (reemplazoEtiquetaPlantilla.EmailReemplazado == null)
                {
                    reemplazoEtiquetaPlantilla.EmailReemplazado = new PlantillaEmailMandrillDTO();

                }
                reemplazoEtiquetaPlantilla.EmailReemplazado.Asunto = " ";
                reemplazoEtiquetaPlantilla.EmailReemplazado.CuerpoHTML = " ";

                // Intercambio final de la plantilla a sus valores respectivos
                if (IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    reemplazoEtiquetaPlantilla.EmailReemplazado.Asunto = plantillaBase.Asunto;
                    reemplazoEtiquetaPlantilla.EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                else if (IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    reemplazoEtiquetaPlantilla.WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    reemplazoEtiquetaPlantilla.WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in reemplazoEtiquetaPlantilla.WhatsAppReemplazado.ListaEtiquetas)
                    {
                        reemplazoEtiquetaPlantilla.WhatsAppReemplazado.Plantilla = reemplazoEtiquetaPlantilla.WhatsAppReemplazado.Plantilla.Replace(item.Codigo, item.Texto);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool EliminarAccesoTemporal(EliminarAccesoTemporalDTO AccesoTemporal, string usuario)
        {
            try
            {
                List<MaestroPersonalGrupoAccesoTemporalDTO> listaAccesoTemporal = new List<MaestroPersonalGrupoAccesoTemporalDTO>();

                if (_unitOfWork.PersonalRepository.Exist(AccesoTemporal.IdPersonal))
                {
                    bool resultado = _unitOfWork.PersonalAccesoTemporalAulaVirtualRepository.EliminarAccesoTemporalPorIdPEspecificoPadre(AccesoTemporal.IdPersonal, AccesoTemporal.IdPEspecificoPadre, AccesoTemporal.FechaInicio, AccesoTemporal.FechaFin, usuario);

                    var personal = _unitOfWork.PersonalRepository.FirstById(AccesoTemporal.IdPersonal);
                    var idPortalWeb = _unitOfWork.PersonalAccesoTemporalAulaVirtualRepository.ObtenerIdUsuarioPortalWebCorreo(personal.Email);
                    var alumno = _unitOfWork.AlumnoRepository.FirstBy(x => x.Email1 == personal.Email);

                    if (alumno == null)
                    {
                        throw new Exception("El alumno no existe");

                    }

                    bool resultadoPortalWeb = _unitOfWork.PersonalAccesoTemporalAulaVirtualRepository.ActualizarAccesosTemporalesPortalWeb(AccesoTemporal.IdPersonal, idPortalWeb, alumno.Id);

                    if (!resultado || !resultadoPortalWeb)
                    {
                        throw new Exception("No se pudo eliminar el registro");
                    }
                }
                else
                {
                    throw new Exception("El elemento no existe o ya fue eliminado");
                }

                listaAccesoTemporal = ObtenerListaAccesoTemporal(AccesoTemporal.IdPersonal);

                if (listaAccesoTemporal != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }



        public bool RegistrarArchivoFotoExpositor(ArchivoPersonalDTO ArchivoPersonal, string usuario)
        {
            try
            {

                string nombreArchivo = "";
                string nombreArchivotemp = "";
                string contentType = "";
                var urlArchivoRepositorio = "";
                if (ArchivoPersonal.File != null)
                {
                    UtilService utilBO = new UtilService();
                    contentType = ArchivoPersonal.File.ContentType;
                    nombreArchivo = ArchivoPersonal.File.FileName;
                    nombreArchivotemp = ArchivoPersonal.File.FileName;
                    nombreArchivotemp = DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" + utilBO.SlugNombreArchivo(nombreArchivotemp);
                    urlArchivoRepositorio = _unitOfWork.PersonalArchivoRepository.SubirDocumentosPersonal(ArchivoPersonal.File.ConvertToByte(), ArchivoPersonal.File.ContentType, nombreArchivotemp);
                }
                else
                {
                    throw new Exception("No se subió ningún archivo.");
                }

                if (string.IsNullOrEmpty(urlArchivoRepositorio))
                {
                    throw new Exception("Ocurrió un problema al subir el archivo.");
                }
                bool esImagen = false;
                if (ArchivoPersonal.File.ContentType.Contains("image"))
                {
                    esImagen = true;
                }
                var agregarArchivo = new PersonalArchivo
                {
                    NombreArchivo = nombreArchivo,
                    RutaArchivo = urlArchivoRepositorio,
                    MimeType = ArchivoPersonal.File.ContentType,
                    EsImagen = esImagen,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario
                };
                var resultado = _unitOfWork.PersonalArchivoRepository.Add(agregarArchivo);
                _unitOfWork.Commit();
                if (ArchivoPersonal.Id != null)
                {
                    var personal = _unitOfWork.PersonalRepository.FirstById((int)ArchivoPersonal.Id);
                    if (personal != null)
                    {
                        personal.IdPersonalArchivo = agregarArchivo.Id;
                        personal.UsuarioModificacion = usuario;
                        personal.FechaModificacion = DateTime.Now;
                        _unitOfWork.PersonalRepository.Update(personal);
                    }
                }
                if (resultado != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarHorario(HorarioDTO dto, string usuario)
        {
            try
            {

                List<PersonalHorario> listaHorario = _unitOfWork.PersonalHorarioRepository.ObtenerPorIdPersonal(dto.IdPersonal);
                var i = 1;
                foreach (var item in listaHorario)
                {
                    if (i == 1)
                    {
                        item.FechaFin = DateTime.Now;
                    }
                    item.Activo = false;
                    _unitOfWork.PersonalHorarioRepository.Update(item);
                    _unitOfWork.Commit();
                    i = i + 1;
                }
                PersonalHorario personalHorarioBO = new PersonalHorario();
                personalHorarioBO.IdPersonal = dto.IdPersonal;
                personalHorarioBO.Estado = true;
                personalHorarioBO.Activo = true;
                personalHorarioBO.FechaCreacion = DateTime.Now;
                personalHorarioBO.UsuarioCreacion = usuario;

                personalHorarioBO.Lunes1 = dto.Lunes1;
                personalHorarioBO.Lunes2 = dto.Lunes2;
                personalHorarioBO.Lunes3 = dto.Lunes3;
                personalHorarioBO.Lunes4 = dto.Lunes4;
                personalHorarioBO.Martes1 = dto.Martes1;
                personalHorarioBO.Martes2 = dto.Martes2;
                personalHorarioBO.Martes3 = dto.Martes3;
                personalHorarioBO.Martes4 = dto.Martes4;
                personalHorarioBO.Miercoles1 = dto.Miercoles1;
                personalHorarioBO.Miercoles2 = dto.Miercoles2;
                personalHorarioBO.Miercoles3 = dto.Miercoles3;
                personalHorarioBO.Miercoles4 = dto.Miercoles4;
                personalHorarioBO.Jueves1 = dto.Jueves1;
                personalHorarioBO.Jueves2 = dto.Jueves2;
                personalHorarioBO.Jueves3 = dto.Jueves3;
                personalHorarioBO.Jueves4 = dto.Jueves4;
                personalHorarioBO.Viernes1 = dto.Viernes1;
                personalHorarioBO.Viernes2 = dto.Viernes2;
                personalHorarioBO.Viernes3 = dto.Viernes3;
                personalHorarioBO.Viernes4 = dto.Viernes4;
                personalHorarioBO.Sabado1 = dto.Sabado1;
                personalHorarioBO.Sabado2 = dto.Sabado2;
                personalHorarioBO.Sabado3 = dto.Sabado3;
                personalHorarioBO.Sabado4 = dto.Sabado4;
                personalHorarioBO.Domingo1 = dto.Domingo1;
                personalHorarioBO.Domingo2 = dto.Domingo2;
                personalHorarioBO.Domingo3 = dto.Domingo3;
                personalHorarioBO.Domingo4 = dto.Domingo4;
                personalHorarioBO.FechaInicio = DateTime.Now;
                personalHorarioBO.FechaFin = null;
                personalHorarioBO.FechaModificacion = DateTime.Now;
                personalHorarioBO.UsuarioModificacion = usuario;

                //if (personalHorarioBO.Id == 0)
                _unitOfWork.PersonalHorarioRepository.Add(personalHorarioBO);
                _unitOfWork.Commit();
                //else _repPersonalHorario.Update(personalHorarioBO);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }




        public IEnumerable<FiltroPersonalJefaturaFiltroDTO> ObtenerReporteTodoPersonal(FiltroPersonalJefaturaDTO filtro)
        {
            try
            {
                string condicion = string.Empty;
                var filtros = new
                {
                    ListaPersonal = filtro.ListaPersonal == null ? "" : string.Join(",", filtro.ListaPersonal.Select(x => x).Distinct()),
                    ListaAreaTrabajo = filtro.ListaAreaTrabajo == null ? "" : string.Join(",", filtro.ListaAreaTrabajo),
                    Estado = filtro.Estado == null ? "" : string.Join(",", filtro.Estado),
                };
                if (filtros.ListaPersonal.Length > 0)
                {
                    if (condicion.Length > 0)
                    {
                        condicion = condicion + " AND IdPersonal IN (" + filtros.ListaPersonal + ")";
                    }
                    else
                    {
                        condicion = condicion + " IdPersonal IN (" + filtros.ListaPersonal + ")";
                    }
                }
                if (filtros.ListaAreaTrabajo.Length > 0)
                {
                    if (condicion.Length > 0)
                    {
                        condicion = condicion + " AND IdPersonalAreaTrabajo IN (" + filtros.ListaAreaTrabajo + ")";
                    }
                    else
                    {
                        condicion = condicion + " IdPersonalAreaTrabajo IN (" + filtros.ListaAreaTrabajo + ")";
                    }
                }
                if (filtros.Estado.Length > 0)
                {
                    if (condicion.Length > 0)
                    {
                        if (filtros.Estado == "1")
                        {
                            condicion = condicion + " AND Estado = 'Activo' ";
                        }
                        else
                        {
                            condicion = condicion + " AND Estado = 'Inactivo' ";
                        }
                    }
                    else
                    {
                        if (filtros.Estado == "1")
                        {
                            condicion = condicion + " Estado = 'Activo' ";
                        }
                        else
                        {
                            condicion = condicion + " Estado = 'Inactivo' ";
                        }
                    }
                }
                return _unitOfWork.PersonalRepository.ObtenerPersonalJefaturaFiltro(condicion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ComboDTO> ObtenerCombosJefatura()
        {
            return _unitOfWork.PersonalAreaTrabajoRepository.ObtenerTodoFiltroAreaTrabajo();
        }


        public PersonalJefaturaIteradorDTO ObtenerPersonalEncargadoJefatura()
        {

            try
            {
                PersonalJefaturaIteradorDTO resultadoFinalBSGrupo = new PersonalJefaturaIteradorDTO();
                PersonalJefaturaIteradorDTO resultadoFinal = new PersonalJefaturaIteradorDTO();
                List<PersonalJefaturaIteradorDTO> respuesta = new List<PersonalJefaturaIteradorDTO>();
                PersonalJefaturaIteradorDTO agregar;
                var listaPersonalJefatura = _unitOfWork.PersonalRepository.ObtenerPersonalJefatura();
                //Lista Adaptación para Gerencia
                var gerenciaPrincipal = listaPersonalJefatura.Where(x => x.IdPersonal == 213).FirstOrDefault();//Información de Gerente
                var listaJefaturaPrincipal = listaPersonalJefatura.Where(x => x.IdJefeInmediato == 213 && x.IdPersonal != 213).ToList();
                var casoParticular = listaPersonalJefatura.Where(x => x.IdPersonal == 13).FirstOrDefault();
                casoParticular.IdJefeInmediato = 213;
                listaJefaturaPrincipal.Add(casoParticular);

                resultadoFinal.IdPersonal = gerenciaPrincipal.IdPersonal;
                resultadoFinal.Personal = gerenciaPrincipal.Personal;
                resultadoFinal.PuestoTrabajo = gerenciaPrincipal.PuestoTrabajo;
                var grupoJefaturaPrincipal = listaJefaturaPrincipal.GroupBy(x => new { x.IdJefeInmediato }).Select(x => new PersonalJefaturaAgrupadoDTO
                {
                    IdJefeInmediato = x.Key.IdJefeInmediato,
                    PersonalACargo = x.GroupBy(y => new { y.IdPersonal, y.Personal, y.PuestoTrabajo }).Select(y => new PersonalJefaturaAsociadoDTO
                    {
                        IdPersonal = y.Key.IdPersonal,
                        Personal = y.Key.Personal,
                        PuestoTrabajo = y.Key.PuestoTrabajo
                    }).ToList(),
                }).FirstOrDefault();
                foreach (var jefe in grupoJefaturaPrincipal.PersonalACargo)
                {
                    agregar = new PersonalJefaturaIteradorDTO()
                    {
                        IdPersonal = jefe.IdPersonal.GetValueOrDefault(),
                        Personal = jefe.Personal,
                        PuestoTrabajo = jefe.PuestoTrabajo,
                        PersonalACargo = ObtenerSubordinado(jefe.IdPersonal.GetValueOrDefault(), listaPersonalJefatura, 0),
                    };
                    respuesta.Add(agregar);
                }
                respuesta = respuesta.OrderBy(x => x.Personal).ToList();
                resultadoFinal.PersonalACargo = respuesta;

                //Caso de Personal sin Jefe
                PersonalJefaturaIteradorDTO resultadoFinalSinJefe = new PersonalJefaturaIteradorDTO();
                List<PersonalJefaturaIteradorDTO> respuestaSinJefe = new List<PersonalJefaturaIteradorDTO>();
                var listaPersonalSinJefe = listaPersonalJefatura.Where(x => x.IdJefeInmediato == 0).ToList();
                var casoParticularQuitar = listaPersonalSinJefe.Where(x => x.IdPersonal == 13).FirstOrDefault();
                var casoGerenciQuitar = listaPersonalSinJefe.Where(x => x.IdPersonal == 213).FirstOrDefault();
                if (casoParticularQuitar != null)
                {
                    listaPersonalSinJefe.Remove(casoParticular);
                }
                if (casoGerenciQuitar != null)
                {
                    listaPersonalSinJefe.Remove(casoGerenciQuitar);
                }
                resultadoFinalSinJefe.IdPersonal = 0;
                resultadoFinalSinJefe.Personal = "Sin Jefe";
                resultadoFinalSinJefe.PuestoTrabajo = " ";
                var grupoPersonalSinJefe = listaPersonalSinJefe.GroupBy(x => new { x.IdJefeInmediato }).Select(x => new PersonalJefaturaAgrupadoDTO
                {
                    IdJefeInmediato = x.Key.IdJefeInmediato,
                    PersonalACargo = x.GroupBy(y => new { y.IdPersonal, y.Personal, y.PuestoTrabajo }).Select(y => new PersonalJefaturaAsociadoDTO
                    {
                        IdPersonal = y.Key.IdPersonal,
                        Personal = y.Key.Personal,
                        PuestoTrabajo = y.Key.PuestoTrabajo
                    }).ToList(),
                }).FirstOrDefault();
                foreach (var jefe in grupoPersonalSinJefe.PersonalACargo)
                {
                    agregar = new PersonalJefaturaIteradorDTO()
                    {
                        IdPersonal = jefe.IdPersonal.GetValueOrDefault(),
                        Personal = jefe.Personal,
                        PuestoTrabajo = jefe.PuestoTrabajo,
                        PersonalACargo = ObtenerSubordinado(jefe.IdPersonal.GetValueOrDefault(), listaPersonalJefatura, 0),
                    };
                    respuestaSinJefe.Add(agregar);
                }
                respuestaSinJefe = respuestaSinJefe.OrderBy(x => x.Personal).ToList();
                resultadoFinalSinJefe.PersonalACargo = respuestaSinJefe;

                List<PersonalJefaturaIteradorDTO> auxiliar = new List<PersonalJefaturaIteradorDTO>();
                auxiliar.Add(resultadoFinal);
                auxiliar.Add(resultadoFinalSinJefe);
                resultadoFinalBSGrupo.IdPersonal = 0;
                resultadoFinalBSGrupo.Personal = "BS_GRUPO_ADAPTAR_JERARQUIA";
                resultadoFinalBSGrupo.PuestoTrabajo = "BS_GRUPO_ADAPTAR_JERARQUIA";
                resultadoFinalBSGrupo.PersonalACargo = auxiliar;
                return resultadoFinalBSGrupo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<PersonalJefaturaIteradorDTO> ObtenerSubordinado(int idJefe, List<PersonalJefaturaDTO> lista, int iterador)
        {
            try
            {
                List<PersonalJefaturaIteradorDTO> listaRetornada = new List<PersonalJefaturaIteradorDTO>();
                if (iterador <= 10)
                {
                    var listaPersonalJefatura = lista;
                    var listaJefatura = listaPersonalJefatura.Where(x => x.IdJefeInmediato == idJefe).ToList();
                    var grupoJefatura = listaJefatura.GroupBy(x => new { x.IdJefeInmediato }).Select(x => new PersonalJefaturaAgrupadoDTO
                    {
                        IdJefeInmediato = x.Key.IdJefeInmediato,
                        PersonalACargo = x.GroupBy(y => new { y.IdPersonal, y.Personal, y.PuestoTrabajo }).Select(y => new PersonalJefaturaAsociadoDTO
                        {
                            IdPersonal = y.Key.IdPersonal,
                            Personal = y.Key.Personal,
                            PuestoTrabajo = y.Key.PuestoTrabajo
                        }).ToList(),
                    }).FirstOrDefault();
                    if (grupoJefatura != null)
                    {
                        foreach (var jefe in grupoJefatura.PersonalACargo)
                        {
                            PersonalJefaturaIteradorDTO agregar = new PersonalJefaturaIteradorDTO()
                            {
                                IdPersonal = jefe.IdPersonal.GetValueOrDefault(),
                                Personal = jefe.Personal,
                                PuestoTrabajo = jefe.PuestoTrabajo,
                                PersonalACargo = this.ObtenerSubordinado(jefe.IdPersonal.GetValueOrDefault(), lista, iterador + 1)
                            };
                            listaRetornada.Add(agregar);
                        }
                        listaRetornada = listaRetornada.OrderBy(x => x.Personal).ToList();
                    }
                }
                return listaRetornada;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public List<PersonalAutocompleteDTO> CargarPersonalAutoCompleteContrato(string nombre)
        {
            return _unitOfWork.PersonalRepository.CargarPersonalAutoCompleteContrato(nombre);
        }
        /// Autor: Eliot Arias F.
        /// Fecha: 10/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del personal de GP para contacto de whatsapp
        /// </summary>
        /// <returns> int </returns>
        public PersonalWhatsAppDTO ObtenerDatosPersonalPorID(int IdPersonal)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerDatosPersonalPorID(IdPersonal);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 26/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la vista V_ReporteInduccionPersonal
        /// </summary>
        /// <returns>Objetos agrupados</returns>|
        public List<InduccionPersonalCalificacionAgrupadaDTO> ObtenerReportePersonal()
        {
            try
            {
                var listaPersonalInduccion = _unitOfWork.PersonalRepository.ObtenerReportePersonal();
                var listaCursosCalificadosAgrupada = listaPersonalInduccion
                    .GroupBy(x => new
                    {
                        FechaIncoorporacion = x.FechaIncoorporacion,
                        FechaRealizado = x.FechaRealizado,
                        IdSede = x.IdSede,
                        NombreSede = x.NombreSede,
                        IdArea = x.IdArea,
                        NombreArea = x.NombreArea,
                        IdPuestoTrabajo = x.IdPuestoTrabajo,
                        NombrePuestoTrabajo = x.NombrePuestoTrabajo,
                        IdProcesoSeleccion = x.IdProcesoSeleccion,
                        NroDocumento = x.NroDocumento,
                        IdPostulante = x.IdPostulante,
                        NombrePostulante = x.NombrePostulante
                    }).Select(g => new InduccionPersonalCalificacionAgrupadaDTO
                    {
                        FechaIncoorporacion = g.Key.FechaIncoorporacion,
                        FechaRealizado = g.Key.FechaRealizado,
                        IdSede = g.Key.IdSede,
                        NombreSede = g.Key.NombreSede,
                        IdArea = g.Key.IdArea,
                        NombreArea = g.Key.NombreArea,
                        IdPuestoTrabajo = g.Key.IdPuestoTrabajo,
                        NombrePuestoTrabajo = g.Key.NombrePuestoTrabajo,
                        IdProcesoSeleccion = g.Key.IdProcesoSeleccion,
                        NroDocumento = g.Key.NroDocumento,
                        IdPostulante = g.Key.IdPostulante,
                        NombrePostulante = g.Key.NombrePostulante,
                        IdCursoCalificacion = g.Select(e => new CursoCalificacion
                        {
                            OrdenFilaSesion = e.OrdenFilaSesion,
                            Calificacion = e.Calificacion
                        }).ToList(),
                        PromedioGeneral = Math.Round(g.Sum(p => p.Calificacion) / 4)
                    }).ToList();
                return listaCursosCalificadosAgrupada;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 26/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la vista V_ReporteInduccionPersonal filtrados mediante un objeto FiltroInduccionPersonalDTO
        /// </summary>
        /// <returns>Objetos agrupados</returns>|
        public List<InduccionPersonalCalificacionAgrupadaDTO> ObtenerReportePersonalFiltro(FiltroInduccionPersonalDTO Filtro)
        {
            try
            {
                List<InduccionPersonalDTO> listaPersonalInduccion = new List<InduccionPersonalDTO>();
                if (Filtro.IdProceso.Count > 0 || Filtro.IdSede.Count > 0 || Filtro.IdArea.Count > 0 || Filtro.FechaInicio != null || Filtro.FechaFin != null)
                {
                    listaPersonalInduccion = _unitOfWork.PersonalRepository.ObtenerReportePersonalFiltro(Filtro);
                }
                else
                {
                    listaPersonalInduccion = _unitOfWork.PersonalRepository.ObtenerReportePersonal();
                }

                var listaCursosCalificadosAgrupada = listaPersonalInduccion
                    .GroupBy(x => new
                    {
                        FechaIncoorporacion = x.FechaIncoorporacion,
                        FechaRealizado = x.FechaRealizado,
                        IdSede = x.IdSede,
                        NombreSede = x.NombreSede,
                        IdArea = x.IdArea,
                        NombreArea = x.NombreArea,
                        IdPuestoTrabajo = x.IdPuestoTrabajo,
                        NombrePuestoTrabajo = x.NombrePuestoTrabajo,
                        IdProcesoSeleccion = x.IdProcesoSeleccion,
                        NroDocumento = x.NroDocumento,
                        IdPostulante = x.IdPostulante,
                        NombrePostulante = x.NombrePostulante
                    }).Select(g => new InduccionPersonalCalificacionAgrupadaDTO
                    {
                        FechaIncoorporacion = g.Key.FechaIncoorporacion,
                        FechaRealizado = g.Key.FechaRealizado,
                        IdSede = g.Key.IdSede,
                        NombreSede = g.Key.NombreSede,
                        IdArea = g.Key.IdArea,
                        NombreArea = g.Key.NombreArea,
                        IdPuestoTrabajo = g.Key.IdPuestoTrabajo,
                        NombrePuestoTrabajo = g.Key.NombrePuestoTrabajo,
                        IdProcesoSeleccion = g.Key.IdProcesoSeleccion,
                        NroDocumento = g.Key.NroDocumento,
                        IdPostulante = g.Key.IdPostulante,
                        NombrePostulante = g.Key.NombrePostulante,
                        IdCursoCalificacion = g.Select(e => new CursoCalificacion
                        {
                            OrdenFilaSesion = e.OrdenFilaSesion,
                            Calificacion = e.Calificacion
                        }).ToList(),
                        PromedioGeneral = Math.Round(g.Sum(p => p.Calificacion) / 4)
                    }).ToList();
                return listaCursosCalificadosAgrupada;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 26/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene y junta los registros de las tablas de area, sede trabajo, procesos
        /// </summary>
        /// <returns>Objetos agrupados</returns>
        public Object ObtenerCombosInduccion()
        {
            try
            {
                var areas = _unitOfWork.PersonalAreaTrabajoRepository.ObtenerCombo();
                var sedes = _unitOfWork.SedeTrabajoRepository.ObtenerCombo();
                var procesos = _unitOfWork.ProcesoSeleccionRepository.ObtenerCodigoNombre();
                return new { listaAreas = areas, listaSedes = sedes, listaProcesos = procesos };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        //// parte de victor
        ///

        /// Autor: Victor Hinojosa
        /// Fecha: 20/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todo el personal
        /// </summary>
        /// <returns>List<PersonalDetalleDTO></PersonalDetalleDTO></returns>
        public IEnumerable<PersonalDetalleDTO> ObtenerTodoPersonal()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerTodoPersonal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActivarPersonal(int id)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ActivarPersonal(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboDTO> ObtenerPersonalActivo()
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerPersonalActivo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Tipo Función: POST
        /// Autor: Victor Hinojosa
        /// Fecha: 18/10/2024
        /// Versión: 2
        /// <summary>
        /// Guarda el horario por personal
        /// </summary>
        /// <param name="Json">Objeto de clase HorarioDTO para la asignacion de horarios por personal</param>
        /// <returns>Response 200 o response 400 con mensaje de error</returns>
        public bool GuardarHorario(PersonalHorarioDTO Json, string usuarioIntegra)
        {
            try
            {
                PersonalHorario personalHorarioBO = new PersonalHorario(); ;
                List<PersonalHorario> listaHorario = new List<PersonalHorario>();

                listaHorario = _unitOfWork.PersonalHorarioRepository.ObtenerPorIdPersonal(Json.IdPersonal.Value).OrderByDescending(x => x.FechaCreacion).ToList();
                var i = 1;
                foreach (var item in listaHorario)
                {
                    if (i == 1)
                    {
                        item.FechaFin = DateTime.Now;
                    }
                    item.Activo = false;
                    _unitOfWork.PersonalHorarioRepository.Update(item);
                    _unitOfWork.Commit();
                    i = i + 1;
                }

                personalHorarioBO.IdPersonal = Json.IdPersonal ?? 0;
                personalHorarioBO.Estado = true;
                personalHorarioBO.Activo = true;
                personalHorarioBO.FechaCreacion = DateTime.Now;
                personalHorarioBO.UsuarioCreacion = usuarioIntegra;

                personalHorarioBO.Lunes1 = Json.Lunes1;
                personalHorarioBO.Lunes2 = Json.Lunes2;
                personalHorarioBO.Lunes3 = Json.Lunes3;
                personalHorarioBO.Lunes4 = Json.Lunes4;
                personalHorarioBO.Martes1 = Json.Martes1;
                personalHorarioBO.Martes2 = Json.Martes2;
                personalHorarioBO.Martes3 = Json.Martes3;
                personalHorarioBO.Martes4 = Json.Martes4;
                personalHorarioBO.Miercoles1 = Json.Miercoles1;
                personalHorarioBO.Miercoles2 = Json.Miercoles2;
                personalHorarioBO.Miercoles3 = Json.Miercoles3;
                personalHorarioBO.Miercoles4 = Json.Miercoles4;
                personalHorarioBO.Jueves1 = Json.Jueves1;
                personalHorarioBO.Jueves2 = Json.Jueves2;
                personalHorarioBO.Jueves3 = Json.Jueves3;
                personalHorarioBO.Jueves4 = Json.Jueves4;
                personalHorarioBO.Viernes1 = Json.Viernes1;
                personalHorarioBO.Viernes2 = Json.Viernes2;
                personalHorarioBO.Viernes3 = Json.Viernes3;
                personalHorarioBO.Viernes4 = Json.Viernes4;
                personalHorarioBO.Sabado1 = Json.Sabado1;
                personalHorarioBO.Sabado2 = Json.Sabado2;
                personalHorarioBO.Sabado3 = Json.Sabado3;
                personalHorarioBO.Sabado4 = Json.Sabado4;
                personalHorarioBO.Domingo1 = Json.Domingo1;
                personalHorarioBO.Domingo2 = Json.Domingo2;
                personalHorarioBO.Domingo3 = Json.Domingo3;
                personalHorarioBO.Domingo4 = Json.Domingo4;
                personalHorarioBO.FechaInicio = DateTime.Now;
                personalHorarioBO.FechaFin = null;
                personalHorarioBO.FechaModificacion = DateTime.Now;
                personalHorarioBO.UsuarioModificacion = usuarioIntegra;


                _unitOfWork.PersonalHorarioRepository.Add(personalHorarioBO);
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// TipoFuncion: POST
        /// Autor: Victor Hinojosa
        /// Fecha: 28/09/2024
        /// Versión: 1.0
        /// <summary>
        /// Inserta un Personal
        /// </summary>
        /// <param name="Json">Información Compuesta de Personal</param>
        /// <returns>PersonalDetalleDTO</returns>

        public ResultadoDTOv2 InsertarPersonal(PersonalDetalleDTO Json, string usuarioIntegra)
        {

            try
            {
                List<PersonalDTO> anexoActivo = _unitOfWork.PersonalRepository.ObtenerValidacionAnexo(Json.Id.Value, Json.Anexo);
                if (!anexoActivo.Any())
                {
                    var servicioPersonal = new PersonalService(_unitOfWork);
                    var servicioPersonalLog = new PersonalLogService(_unitOfWork);
                    var servicioPais = new PaisService(_unitOfWork);
                    PaisDiferenciaHorariaDTO horario = new PaisDiferenciaHorariaDTO();
                    DominioPbxDTO dominio = new DominioPbxDTO();

                    //revisar validacion de pais
                    if (Json.IdPais != 0)
                    {
                        var horarioTemp = servicioPais.ObtenerPais().FirstOrDefault(x => x.EstadoVisualizacion == 1 && x.Id == Json.IdPais);
                        if (horarioTemp != null)
                        {
                            horario.Id = horarioTemp.Id;
                            horario.DiferenciaHoraria = Convert.ToInt32(horarioTemp.ZonaHoraria);
                        }
                    }

                    else
                    {
                        horario.Id = 0;
                        horario.DiferenciaHoraria = 0;
                    }
                    if (Json.IdDominioPbx != null)
                    {
                        var dominioTemp = _unitOfWork.CentralLlamadaDireccionRepository.ObtenerComboDominioPbx().FirstOrDefault(x => x.Id == Json.IdDominioPbx);
                        dominio.Id = dominioTemp.Id;
                        dominio.Nombre = dominioTemp.Nombre;
                    }
                    var conincidenciasEmail = servicioPersonal.ObtenerListaPersonalPorEmail(Json.Email, (int)Json.Id);

                    var idPersonalEmailRepetido = servicioPersonal.ObtenerPersonalEliminadoEmailRepetido(Json.Email);

                    int? idClasificacionPersona = null;

                    Personal personal = new Personal();
                    PersonaService persona = new PersonaService(_unitOfWork);
                    var tmp = Json;
                    Json.Dominio = dominio.Nombre;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (conincidenciasEmail == null && (idPersonalEmailRepetido == null || idPersonalEmailRepetido == 0))
                        {
                            personal.Nombres = Json.Nombres;
                            personal.Apellidos = Json.Apellidos;
                            personal.Rol = Json.Area;
                            personal.AreaAbrev = Json.AreaAbrev;
                            personal.IdPersonalAreaTrabajo = Json.IdPersonalAreaTrabajo;
                            personal.IdJefe = Json.IdJefe;
                            personal.IdDominioPbx = Json.IdDominioPbx;
                            personal.DiferenciaHoraria = horario.DiferenciaHoraria;
                            personal.CodigoPaisDiferenciaHoraria = horario.Id;
                            personal.Central = Json.Central;
                            personal.TipoPersonal = Json.AsesorCoordinador;
                            personal.Email = Json.Email;
                            personal.Anexo = Json.Anexo;
                            personal.Anexo3Cx = Json.Anexo;
                            personal.UsuarioCreacion = usuarioIntegra;
                            personal.UsuarioModificacion = usuarioIntegra;
                            personal.FechaCreacion = DateTime.Now;
                            personal.FechaModificacion = DateTime.Now;
                            personal.Activo = Json.Activo;
                            personal.UsuarioAsterisk = Json.UsuarioAsterisk;
                            personal.ContrasenaAsterisk = Json.ContrasenaAsterisk;
                            personal.IdDominioPbx = dominio.Id;
                            personal.Dominio = dominio.Nombre;
                            personal.Estado = true;
                            personal.Ip1 = Json.Ip1;
                            personal.Ip2 = Json.Ip2;

                            Json.Estado = true;
                            var personatemporal = servicioPersonal.Add(personal);
                            _unitOfWork.Commit();

                            Json.Id = personatemporal.Id;
                            personal.Id = personatemporal.Id;
                            idClasificacionPersona = persona.InsertarPersona(Json.Id.Value, Aplicacion.Base.Enums.Enums.TipoPersona.Personal, usuarioIntegra);
                        }
                        else if (conincidenciasEmail == null && (idPersonalEmailRepetido != null || idPersonalEmailRepetido != 0))
                        {
                            servicioPersonal.ActivarPersonal(idPersonalEmailRepetido.Value);
                            personal = servicioPersonal.ObtenerPorId(idPersonalEmailRepetido.Value);
                            personal.Nombres = Json.Nombres;
                            personal.Apellidos = Json.Apellidos;
                            personal.Rol = Json.Area;
                            personal.AreaAbrev = Json.AreaAbrev;
                            personal.IdPersonalAreaTrabajo = Json.IdPersonalAreaTrabajo;
                            personal.IdJefe = Json.IdJefe;
                            personal.IdDominioPbx = Json.IdDominioPbx;
                            personal.DiferenciaHoraria = horario.DiferenciaHoraria;
                            personal.CodigoPaisDiferenciaHoraria = horario.Id;
                            personal.Central = Json.Central;
                            personal.TipoPersonal = Json.AsesorCoordinador;
                            personal.Email = Json.Email;
                            personal.Anexo = Json.Anexo;
                            personal.Anexo3Cx = Json.Anexo;
                            personal.UsuarioCreacion = usuarioIntegra;
                            personal.UsuarioModificacion = usuarioIntegra;
                            personal.FechaCreacion = DateTime.Now;
                            personal.FechaModificacion = DateTime.Now;
                            personal.IdDominioPbx = dominio.Id;
                            personal.Dominio = dominio.Nombre;
                            personal.Estado = true;
                            personal.Activo = Json.Activo;
                            personal.UsuarioAsterisk = Json.UsuarioAsterisk;
                            personal.ContrasenaAsterisk = Json.ContrasenaAsterisk;
                            personal.Ip1 = Json.Ip1;
                            personal.Ip2 = Json.Ip2;
                            personal = servicioPersonal.Update(personal);
                            idClasificacionPersona = persona.InsertarPersona(Json.Id.Value, Aplicacion.Base.Enums.Enums.TipoPersona.Personal, usuarioIntegra);
                        }
                        else
                        {
                            Json = null;
                            throw new Exception("El correo electrónico ya se encuentra registrado en el sistema.");

                        }

                        if (idClasificacionPersona == null && Json != null)
                        {
                            throw new Exception("Error al insertar el Tipo Persona Clasificacion");
                        }
                        if (Json != null)
                        {
                            PersonalLog personalLogDTO = new PersonalLog();
                            personalLogDTO.IdPersonal = personal.Id;
                            personalLogDTO.Rol = personal.Rol;
                            personalLogDTO.TipoPersonal = personal.TipoPersonal;
                            personalLogDTO.IdJefe = personal.IdJefe;
                            personalLogDTO.EstadoRol = true;
                            personalLogDTO.EstadoTipoPersonal = true;
                            personalLogDTO.EstadoIdJefe = true;
                            personalLogDTO.FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                            personalLogDTO.FechaFin = null;
                            personalLogDTO.Estado = true;
                            personalLogDTO.UsuarioModificacion = usuarioIntegra;
                            personalLogDTO.UsuarioCreacion = usuarioIntegra;
                            personalLogDTO.FechaCreacion = DateTime.Now;
                            personalLogDTO.FechaModificacion = DateTime.Now;
                            personalLogDTO.IdPersonal = Json.Id == null ? 0 : Json.Id.Value;

                            servicioPersonalLog.Add(personalLogDTO);
                            _unitOfWork.Commit();


                            GmailCliente gmailClienteBO;
                            gmailClienteBO = _unitOfWork.GmailClienteRepository.ObtenerPorIdAsesor(personal.Id);
                            if (gmailClienteBO == null)
                            {
                                gmailClienteBO = new GmailCliente();
                                gmailClienteBO.IdAsesor = personal.Id;
                                gmailClienteBO.EmailAsesor = personal.Email;
                                gmailClienteBO.PasswordCorreo = tmp.PasswordCorreo;
                                gmailClienteBO.NombreAsesor = string.Concat(personal.Nombres, " ", personal.Apellidos);
                                gmailClienteBO.IdClient = "-";
                                gmailClienteBO.ClientSecret = tmp.PasswordCorreo;

                                gmailClienteBO.Estado = true;
                                gmailClienteBO.UsuarioCreacion = usuarioIntegra;
                                gmailClienteBO.UsuarioModificacion = usuarioIntegra;
                                gmailClienteBO.FechaCreacion = DateTime.Now;
                                gmailClienteBO.FechaModificacion = DateTime.Now;

                                _unitOfWork.GmailClienteRepository.Add(gmailClienteBO);
                                _unitOfWork.Commit();

                            }
                            else
                            {
                                gmailClienteBO.IdAsesor = personal.Id;
                                gmailClienteBO.EmailAsesor = personal.Email;
                                gmailClienteBO.PasswordCorreo = tmp.PasswordCorreo;
                                gmailClienteBO.NombreAsesor = string.Concat(personal.Nombres, " ", personal.Apellidos);
                                gmailClienteBO.IdClient = "-";
                                gmailClienteBO.ClientSecret = tmp.PasswordCorreo;
                                gmailClienteBO.UsuarioModificacion = personal.UsuarioModificacion;
                                gmailClienteBO.FechaModificacion = DateTime.Now;

                                _unitOfWork.GmailClienteRepository.Update(gmailClienteBO);
                                _unitOfWork.Commit();
                            }
                            PersonalHorario personalHorario;
                            personalHorario = _unitOfWork.PersonalHorarioRepository.ObtenerPorIdPersonal(personal.Id).FirstOrDefault();
                            if (personalHorario == null)
                            {
                                personalHorario = new PersonalHorario();
                                personalHorario.IdPersonal = personal.Id;
                                personalHorario.Lunes1 = Json.PersonalHorario.Lunes1;
                                personalHorario.Lunes2 = Json.PersonalHorario.Lunes2;
                                personalHorario.Lunes3 = Json.PersonalHorario.Lunes3;
                                personalHorario.Lunes4 = Json.PersonalHorario.Lunes4;
                                personalHorario.Martes1 = Json.PersonalHorario.Martes1;
                                personalHorario.Martes2 = Json.PersonalHorario.Martes2;
                                personalHorario.Martes3 = Json.PersonalHorario.Martes3;
                                personalHorario.Martes4 = Json.PersonalHorario.Martes4;
                                personalHorario.Miercoles1 = Json.PersonalHorario.Miercoles1;
                                personalHorario.Miercoles2 = Json.PersonalHorario.Miercoles2;
                                personalHorario.Miercoles3 = Json.PersonalHorario.Miercoles3;
                                personalHorario.Miercoles4 = Json.PersonalHorario.Miercoles4;
                                personalHorario.Jueves1 = Json.PersonalHorario.Jueves1;
                                personalHorario.Jueves2 = Json.PersonalHorario.Jueves2;
                                personalHorario.Jueves3 = Json.PersonalHorario.Jueves3;
                                personalHorario.Jueves4 = Json.PersonalHorario.Jueves4;
                                personalHorario.Viernes1 = Json.PersonalHorario.Viernes1;
                                personalHorario.Viernes2 = Json.PersonalHorario.Viernes2;
                                personalHorario.Viernes3 = Json.PersonalHorario.Viernes3;
                                personalHorario.Viernes4 = Json.PersonalHorario.Viernes4;
                                personalHorario.Sabado1 = Json.PersonalHorario.Sabado1;
                                personalHorario.Sabado2 = Json.PersonalHorario.Sabado2;
                                personalHorario.Sabado3 = Json.PersonalHorario.Sabado3;
                                personalHorario.Sabado4 = Json.PersonalHorario.Sabado4;
                                personalHorario.Domingo1 = Json.PersonalHorario.Domingo1;
                                personalHorario.Domingo2 = Json.PersonalHorario.Domingo2;
                                personalHorario.Domingo3 = Json.PersonalHorario.Domingo3;
                                personalHorario.Domingo4 = Json.PersonalHorario.Domingo4;
                                personalHorario.Activo = true;
                                personalHorario.UsuarioCreacion = usuarioIntegra;
                                personalHorario.UsuarioModificacion = usuarioIntegra;
                                personalHorario.FechaCreacion = DateTime.Now;
                                personalHorario.FechaModificacion = DateTime.Now;

                                _unitOfWork.PersonalHorarioRepository.Add(personalHorario);
                                _unitOfWork.Commit();

                            }
                            else
                            {
                                gmailClienteBO.IdAsesor = personal.Id;
                                gmailClienteBO.EmailAsesor = personal.Email;
                                gmailClienteBO.PasswordCorreo = tmp.PasswordCorreo;
                                gmailClienteBO.NombreAsesor = string.Concat(personal.Nombres, " ", personal.Apellidos);
                                gmailClienteBO.IdClient = "-";
                                gmailClienteBO.ClientSecret = tmp.PasswordCorreo;
                                gmailClienteBO.UsuarioModificacion = personal.UsuarioModificacion;
                                gmailClienteBO.FechaModificacion = DateTime.Now;

                                _unitOfWork.GmailClienteRepository.Update(gmailClienteBO);
                                _unitOfWork.Commit();
                            }
                            _unitOfWork.Commit();
                            scope.Complete();
                        }
                    }
                    return new ResultadoDTOv2
                    {
                        Exito = true,
                        Mensaje = "El Personal se ha insertado correctamente",
                    };
                }
                else
                {
                    return new ResultadoDTOv2
                    {
                        Exito = false,
                        Mensaje = "El Anexo ya se encuentra en uso por otro personal"
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResultadoDTOv2
                {
                    Exito = false,
                    Mensaje = ex.Message
                };
            }
        }
        /// TipoFuncion: POST
        /// Autor: Victor Hinojosa
        /// Fecha: 04/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualiza datos del personal 
        /// </summary>
        /// <param name="Json">Información Compuesta de Personal</param>
        /// <returns>PersonalDetalleDTO</returns>

        public ResultadoDTOv2 ActualizarPersonal(PersonalDetalleDTO Json, string usuarioIntegra)
        {

            try
            {
                List<PersonalDTO> anexoActivo = _unitOfWork.PersonalRepository.ObtenerValidacionAnexo(Json.Id.Value, Json.Anexo);
                if (!anexoActivo.Any())
                {
                    var servicioPersonal = new PersonalService(_unitOfWork);
                    Personal personal = new Personal();
                    var servicioPais = new PaisService(_unitOfWork);
                    PaisDiferenciaHorariaDTO horario = new PaisDiferenciaHorariaDTO();
                    DominioPbxDTO dominio = new DominioPbxDTO();
                    if (Json.IdPais != null && Json.IdPais != 0)
                    {
                        var horarioTemp = servicioPais.ObtenerPais().FirstOrDefault(x => x.EstadoVisualizacion == 1 && x.Id == Json.IdPais);
                        horario.Id = horarioTemp.Id;
                        horario.DiferenciaHoraria = Convert.ToInt32(horarioTemp.ZonaHoraria);
                    }
                    else
                    {
                        horario.Id = 51;
                        horario.DiferenciaHoraria = 0;
                    }
                    if (Json.IdDominioPbx != 0)
                    {
                        var dominioTemp = _unitOfWork.CentralLlamadaDireccionRepository.ObtenerComboDominioPbx().FirstOrDefault(x => x.Id == Json.IdDominioPbx);
                        dominio.Id = dominioTemp.Id;
                        dominio.Nombre = dominioTemp.Nombre;
                    }
                    personal = servicioPersonal.ObtenerPorId(Json.Id.Value);
                    var tmp = Json;

                    var conincidenciasEmail = servicioPersonal.ObtenerListaPersonalPorEmail(Json.Email, (int)Json.Id);
                    var idPersonalEmailRepetido = servicioPersonal.ObtenerPersonalEliminadoEmailRepetido(Json.Email);



                    var rolAnterior = personal.Rol;
                    var tipoPersonalAnterior = personal.TipoPersonal == null ? "" : personal.TipoPersonal;
                    int? idJefeAnterior = personal.IdJefe;
                    var estadoCambioRolJefe = false;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (conincidenciasEmail == null && (idPersonalEmailRepetido == null || idPersonalEmailRepetido == 0))
                        {
                        personal.Nombres = Json.Nombres;
                        personal.Apellidos = Json.Apellidos;
                        personal.Rol = Json.Area;
                        personal.AreaAbrev = Json.AreaAbrev;
                        personal.IdJefe = Json.IdJefe;
                        personal.IdDominioPbx = Json.IdDominioPbx;
                        personal.DiferenciaHoraria = horario.DiferenciaHoraria;
                        personal.CodigoPaisDiferenciaHoraria = horario.Id;
                        personal.Dominio = dominio.Nombre;
                        personal.IdDominioPbx = dominio.Id;
                        personal.Central = Json.Central;
                        personal.TipoPersonal = Json.AsesorCoordinador;
                        personal.Email = Json.Email;
                        personal.Anexo = Json.Anexo;
                        personal.Anexo3Cx = Json.Anexo;
                        personal.UsuarioAsterisk = Json.UsuarioAsterisk;
                        personal.ContrasenaAsterisk = Json.ContrasenaAsterisk;
                        personal.UsuarioModificacion = usuarioIntegra;
                        personal.FechaModificacion = DateTime.Now;
                        personal.Estado = true;
                        personal.Activo = Json.Activo;
                        personal.Ip1 = Json.Ip1;
                        personal.Ip2 = Json.Ip2;
                        personal.IdPersonalAreaTrabajo = Json.IdPersonalAreaTrabajo;
                        servicioPersonal.Update(personal);

                            if (!(rolAnterior.ToUpper().Equals(personal.Rol.ToUpper())) || !(tipoPersonalAnterior.ToUpper().Equals(personal.TipoPersonal?.ToUpper() ?? "")))
                            {
                                var personalLogUpdate = _unitOfWork.PersonalLogRepository.ObtenerPorIdPersonal(personal.Id).ToList().Where(x => x.IdPersonal == personal.Id && (x.EstadoRol == true || x.EstadoTipoPersonal == true) && x.FechaFin == null).FirstOrDefault();
                                var personalCambioJefe = _unitOfWork.PersonalLogRepository.ObtenerPorIdPersonal(personal.Id).ToList().Where(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true && x.EstadoRol == false && x.EstadoTipoPersonal == false) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                                if (personalLogUpdate != null)
                                {
                                    estadoCambioRolJefe = personalLogUpdate.EstadoIdJefe == true && personalLogUpdate.EstadoRol == true && personalLogUpdate.EstadoTipoPersonal == true;
                                    if (estadoCambioRolJefe && personalCambioJefe == null)
                                    {
                                        PersonalLog personalLog = new PersonalLog();
                                        personalLog.IdPersonal = personal.Id;
                                        personalLog.Rol = personal.Rol;
                                        personalLog.TipoPersonal = personal.TipoPersonal;
                                        personalLog.IdJefe = idJefeAnterior;
                                        personalLog.EstadoRol = false;
                                        personalLog.EstadoTipoPersonal = false;
                                        personalLog.EstadoIdJefe = true;
                                        personalLog.FechaInicio = personalLogUpdate.FechaInicio;
                                        personalLog.FechaFin = null;
                                        personalLog.Estado = true;
                                        personalLog.UsuarioModificacion = usuarioIntegra;
                                        personalLog.UsuarioCreacion = usuarioIntegra;
                                        personalLog.FechaCreacion = DateTime.Now;
                                        personalLog.FechaModificacion = DateTime.Now;
                                        _unitOfWork.PersonalLogRepository.Add(personalLog);
                                    }
                                    personalLogUpdate.FechaFin = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 23, 59, 59);
                                    personalLogUpdate.UsuarioModificacion = usuarioIntegra;
                                    personalLogUpdate.FechaModificacion = DateTime.Now;
                                    _unitOfWork.PersonalLogRepository.Update(personalLogUpdate);

                                    PersonalLog personalLogBO = new PersonalLog();
                                    personalLogBO.IdPersonal = personal.Id;
                                    personalLogBO.Rol = personal.Rol;
                                    personalLogBO.TipoPersonal = personal.TipoPersonal;
                                    personalLogBO.IdJefe = personal.IdJefe;
                                    personalLogBO.EstadoRol = rolAnterior != personal.Rol;
                                    personalLogBO.EstadoTipoPersonal = tipoPersonalAnterior != personal.TipoPersonal;
                                    personalLogBO.EstadoIdJefe = false;
                                    personalLogBO.FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0); ;
                                    personalLogBO.FechaFin = null;
                                    personalLogBO.Estado = true;
                                    personalLogBO.UsuarioModificacion = usuarioIntegra;
                                    personalLogBO.UsuarioCreacion = usuarioIntegra;
                                    personalLogBO.FechaCreacion = DateTime.Now;
                                    personalLogBO.FechaModificacion = DateTime.Now;

                                    _unitOfWork.PersonalLogRepository.Add(personalLogBO);
                                }
                               
                            }
                            if (idJefeAnterior != personal.IdJefe)
                            {
                                if (estadoCambioRolJefe == false)
                                {
                                    var personalLogUpdate = _unitOfWork.PersonalLogRepository.ObtenerPorIdPersonal(personal.Id).ToList().Where(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                                    var personalCambioJefe = _unitOfWork.PersonalLogRepository.ObtenerPorIdPersonal(personal.Id).ToList().Where(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true && x.EstadoRol == false && x.EstadoTipoPersonal == false) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                                    estadoCambioRolJefe = personalLogUpdate.EstadoIdJefe == true && personalLogUpdate.EstadoRol == true && personalLogUpdate.EstadoTipoPersonal == true;
                                    if (estadoCambioRolJefe && personalCambioJefe == null)
                                    {

                                    PersonalLog personalLog = new PersonalLog();
                                    personalLog.IdPersonal = personal.Id;
                                    personalLog.Rol = personal.Rol;
                                    personalLog.TipoPersonal = personal.TipoPersonal;
                                    personalLog.IdJefe = idJefeAnterior;
                                    personalLog.EstadoRol = false;
                                    personalLog.EstadoTipoPersonal = false;
                                    personalLog.EstadoIdJefe = true;
                                    personalLog.FechaInicio = personalLogUpdate.FechaInicio;
                                    personalLog.FechaFin = null;
                                    personalLog.Estado = true;
                                    personalLog.UsuarioModificacion = usuarioIntegra;
                                    personalLog.UsuarioCreacion = usuarioIntegra;
                                    personalLog.FechaCreacion = DateTime.Now;
                                    personalLog.FechaModificacion = DateTime.Now;
                                    _unitOfWork.PersonalLogRepository.Add(personalLog);
                                }
                            }

                            var personalLogUpdate2 = _unitOfWork.PersonalLogRepository.ObtenerPorIdPersonal(personal.Id).ToList().Where(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                            personalLogUpdate2.FechaFin = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 23, 59, 59);
                            personalLogUpdate2.UsuarioModificacion = usuarioIntegra;
                            personalLogUpdate2.FechaModificacion = DateTime.Now;
                            _unitOfWork.PersonalLogRepository.Update(personalLogUpdate2);

                            PersonalLog personalLogBO = new PersonalLog();
                            personalLogBO.IdPersonal = personal.Id;
                            personalLogBO.Rol = personal.Rol;
                            personalLogBO.TipoPersonal = personal.TipoPersonal;
                            personalLogBO.IdJefe = personal.IdJefe;
                            personalLogBO.EstadoRol = false;
                            personalLogBO.EstadoTipoPersonal = false;
                            personalLogBO.EstadoIdJefe = idJefeAnterior != personal.IdJefe;
                            personalLogBO.FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                            personalLogBO.FechaFin = null;
                            personalLogBO.Estado = true;
                            personalLogBO.UsuarioModificacion = usuarioIntegra;
                            personalLogBO.UsuarioCreacion = usuarioIntegra;
                            personalLogBO.FechaCreacion = DateTime.Now;
                            personalLogBO.FechaModificacion = DateTime.Now;
                            _unitOfWork.PersonalLogRepository.Add(personalLogBO);
                        }
                        GmailCliente gmailClienteBO;
                        gmailClienteBO = _unitOfWork.GmailClienteRepository.ObtenerPorIdAsesor(personal.Id);
                        if (gmailClienteBO == null)
                        {
                            gmailClienteBO = new GmailCliente();
                            gmailClienteBO.IdAsesor = personal.Id;
                            gmailClienteBO.EmailAsesor = personal.Email;
                            gmailClienteBO.PasswordCorreo = tmp.PasswordCorreo;
                            gmailClienteBO.NombreAsesor = string.Concat(personal.Nombres, " ", personal.Apellidos);
                            gmailClienteBO.IdClient = "-";
                            gmailClienteBO.ClientSecret = tmp.PasswordCorreo;
                            gmailClienteBO.Estado = true;
                            gmailClienteBO.UsuarioCreacion = usuarioIntegra;
                            gmailClienteBO.UsuarioModificacion = usuarioIntegra;
                            gmailClienteBO.FechaCreacion = DateTime.Now;
                            gmailClienteBO.FechaModificacion = DateTime.Now;
                            _unitOfWork.GmailClienteRepository.Add(gmailClienteBO);
                        }
                        else
                        {
                            gmailClienteBO.IdAsesor = personal.Id;
                            gmailClienteBO.EmailAsesor = personal.Email;
                            gmailClienteBO.PasswordCorreo = tmp.PasswordCorreo;
                            gmailClienteBO.NombreAsesor = string.Concat(personal.Nombres, " ", personal.Apellidos);
                            gmailClienteBO.IdClient = "-";
                            gmailClienteBO.ClientSecret = tmp.PasswordCorreo;
                            gmailClienteBO.UsuarioModificacion = usuarioIntegra;
                            gmailClienteBO.FechaModificacion = DateTime.Now;

                            _unitOfWork.GmailClienteRepository.Update(gmailClienteBO);

                        }
                        }
                        else
                        {
                            throw new Exception("El correo electrónico ya se encuentra registrado en el sistema.");

                        }
                        _unitOfWork.Commit();
                        scope.Complete();
                    }
                    return new ResultadoDTOv2
                    {
                        Exito = true,
                        Mensaje = "El Personal se ha actualizado correctamente",
                    };
                }
                else
                {
                    throw new Exception("AnexoEnUso: El Anexo se encuentra en uso");
                }
            }
            catch (Exception ex)
            {
                return new ResultadoDTOv2
            {
                    Exito = false,
                    Mensaje = ex.Message
                };
            }

        }
        /// TipoFuncion: POST
        /// Autor: Victor Hinojosa
        /// Fecha: 15/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza el envio de correo por medio de SMTP
        /// </summary>
        /// <returns> Obtiene confirmación de envio: Bool </returns>
        public bool EnviarCorreoGmail(string emailDestinatario, string emailRemitente, string personal, string clave, string mensaje, string asunto)
        {
            string host = "smtp.gmail.com";
            int port = 587;

            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    //CONFIGURACION DEL MENSAJE
                    MailMessage mail = new MailMessage();
                    mail.To.Add(emailDestinatario);
                    mail.From = new MailAddress(emailRemitente, personal, System.Text.Encoding.UTF8);
                    mail.Subject = asunto;
                    mail.Body = mensaje;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    //CONFIGURACIÓN DEL STMP

                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(emailRemitente, clave);
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                    mail.Dispose();
                    smtp.Dispose();

                    return true;
                }
                catch (Exception ex)
                {
                    smtp.Dispose();
                    return false;
                }
            }
        }
        /// TipoFuncion: POST
        /// Autor: Victor Hinojosa
        /// Fecha: 14/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza validaciones de Acceso
        /// </summary>
        /// <returns> Obtiene confirmación de envio: Bool </returns>
        public bool EnviarMensajeValidacionAcceso(EnvioCorreoValidacionAccesoDTO Json)
        {
            try
            {
                var emailDestinatario = _unitOfWork.IntegraAspNetUserRepository.ObtenerEmailPorNombreUsuario(Json.Usuario);
                var mensaje = "Envio exitoso, la clave de aplicación es correcta!";
                var asunto = "Validación clave de aplicación " + Json.EmailRemitente;

                bool envioCorreo = EnviarCorreoGmail(emailDestinatario, Json.EmailRemitente, Json.PersonalRemitente, Json.PasswordCorreo, mensaje, asunto);
                return envioCorreo;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// TipoFuncion: POST
        /// Autor: Victor Hinojosa
        /// Fecha: 11/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Resetea ips de un personal
        /// </summary>
        /// <param name="Json">Información Compuesta de Personal</param>
        /// <returns>MacDTO</returns>
        public bool ResetarIp(MacDTO json)
        {
            try
            {
                if (json == null || string.IsNullOrEmpty(json.Usuario))
                {
                    throw new ArgumentException("El objeto json no es válido o el usuario está vacío.");
                }
                var personal = _unitOfWork.PersonalRepository.ObtenerPorId(json.Id);
                if (personal == null)
                {
                    throw new Exception("Personal no encontrado");
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    personal.Ip1 = null;
                    personal.Ip2 = null;
                    personal.FechaModificacion = DateTime.Now;
                    personal.UsuarioModificacion = json.Usuario;

                    var personaprueba = _unitOfWork.PersonalRepository.Update(personal);
                    _unitOfWork.Commit();
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }






        /// TipoFuncion: GET
        /// Autor: Junior Llerena
        /// Fecha: 11/12/2025
        /// Versión: 1.0
        /// <summary>
        /// marcacion personal obtener personal por id
        /// </summary>
        public Personal FirstById(int id)
        {
            try
            {
                var personal = _unitOfWork.PersonalRepository.ObtenerPorId(id);

                if (personal == null)
                {
                    throw new Exception($"No se encontró el personal con Id: {id}");
                }

                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Junior Llerena
        /// Fecha: 11/12/2025
        /// Versión: 1.0
        /// <summary>
        /// marcacion personal obtener registro marcacion por filtro
        /// </summary>
        public RegistroMarcadorFechaBO ObtenerRegistroMarcacionPersonalDNI(int idPersonal, DateTime fechaActual, string Dni)
        {
            try
            {
                return _unitOfWork.PersonalRepository.ObtenerRegistroMarcacionPorFiltro(idPersonal, fechaActual, Dni);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Junior Llerena
        /// Fecha: 11/12/2025
        /// Versión: 1.0
        /// <summary>
        /// marcacion personal actualizar
        /// </summary>
        public bool Update(RegistroMarcadorFechaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                return _unitOfWork.PersonalRepository.ActualizarRegistroMarcacion(objetoBO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Junior Llerena
        /// Fecha: 11/12/2025
        /// Versión: 1.0
        /// <summary>
        /// marcacion personal insertar
        /// </summary>
        public bool Insert(RegistroMarcadorFechaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                return _unitOfWork.PersonalRepository.InsertarRegistroMarcacion(objetoBO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// TipoFuncion: GET
        /// Autor: Junior Llerena
        /// Fecha: 11/12/2025
        /// Versión: 1.0
        /// <summary>
        /// marcacion personal
        /// </summary>
        public ResultadoDTOv2 InsertarMarcacionPersonalV2(string Usuario, int TipoBoton, string DNI)
        {
            try
            {
                var user = _unitOfWork.PersonalRepository.ObtenerIdentidadUsusarioDNI(Usuario, DNI);
                var personal = FirstById(user.Id);
                var fechaActual = DateTime.Now;
                bool rpta = false;
                bool yaMarcado = false;
                bool noCumpleTiempoMinimoAlmuerzo = false;
                var registroMarcacionPersonal = ObtenerRegistroMarcacionPersonalDNI(personal.Id, fechaActual, DNI);
                switch (TipoBoton)
                {
                    case 1:
                        if (registroMarcacionPersonal == null)
                        {
                            registroMarcacionPersonal = new RegistroMarcadorFechaBO()
                            {
                                Pin = personal.NumeroDocumento,
                                Fecha = fechaActual.Date,
                                IdCiudad = personal.IdCiudad == null ? 0 : personal.IdCiudad.Value,
                                IdPersonal = personal.Id,
                                M1 = fechaActual.TimeOfDay,
                                Estado = true,
                                UsuarioCreacion = Usuario,
                                UsuarioModificacion = Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            rpta = Insert(registroMarcacionPersonal);
                        }
                        else
                        {
                            if (registroMarcacionPersonal.M1 != null)
                            {
                                yaMarcado = true;
                            }
                            else
                            {
                                registroMarcacionPersonal.M1 = fechaActual.TimeOfDay;
                                registroMarcacionPersonal.UsuarioModificacion = Usuario;
                                registroMarcacionPersonal.FechaModificacion = DateTime.Now;
                                rpta = Update(registroMarcacionPersonal);
                            }
                        }
                        break;
                    case 2: 
                        if (registroMarcacionPersonal != null)
                        {
                            if (registroMarcacionPersonal.M1 == null)
                            {
                                return new ResultadoDTOv2
                                {
                                    Exito = false,
                                    Mensaje = "Debe marcar entrada (M1) antes de marcar salida a refrigerio"
                                };
                            }

                            if (registroMarcacionPersonal.M4 != null)
                            {
                                return new ResultadoDTOv2
                                {
                                    Exito = false,
                                    Mensaje = "Ya marcó salida (M4), no puede marcar salida a refrigerio"
                                };
                            }

                            if (registroMarcacionPersonal.M2 == null)
                            {
                                registroMarcacionPersonal.M2 = fechaActual.TimeOfDay;
                                registroMarcacionPersonal.UsuarioModificacion = Usuario;
                                registroMarcacionPersonal.FechaModificacion = DateTime.Now;
                                rpta = Update(registroMarcacionPersonal);
                            }
                            else
                            {
                                yaMarcado = true;
                            }
                        }
                        else
                        {
                            return new ResultadoDTOv2
                            {
                                Exito = false,
                                Mensaje = "Debe marcar entrada (M1) antes de marcar salida a refrigerio"
                            };
                        }
                        break;
                    case 3:
                        if (registroMarcacionPersonal != null)
                        {
                            if (registroMarcacionPersonal.M2 == null)
                            {
                                return new ResultadoDTOv2
                                {
                                    Exito = false,
                                    Mensaje = "Debe marcar salida a refrigerio (M2) antes de marcar regreso de refrigerio"
                                };
                            }

                            if (registroMarcacionPersonal.M4 != null)
                            {
                                return new ResultadoDTOv2
                                {
                                    Exito = false,
                                    Mensaje = "Ya marcó salida (M4), no puede marcar regreso de refrigerio"
                                };
                            }

                            if (registroMarcacionPersonal.M3 == null)
                            {
                                var diferencia = fechaActual.TimeOfDay - registroMarcacionPersonal.M2.Value;
                                var horasdiferencia = diferencia.Hours * 60;
                                var minutosdiferencia = diferencia.Minutes + horasdiferencia;

                                if (minutosdiferencia < 45)
                                {
                                    noCumpleTiempoMinimoAlmuerzo = true;
                                    break;
                                }

                                registroMarcacionPersonal.M3 = fechaActual.TimeOfDay;
                                registroMarcacionPersonal.UsuarioModificacion = Usuario;
                                registroMarcacionPersonal.FechaModificacion = DateTime.Now;
                                rpta = Update(registroMarcacionPersonal);
                            }
                            else
                            {
                                yaMarcado = true;
                            }
                        }
                        else
                        {
                            return new ResultadoDTOv2
                            {
                                Exito = false,
                                Mensaje = "Debe marcar salida a refrigerio (M2) antes de marcar regreso de refrigerio"
                            };
                        }
                        break;
                    case 4: 
                        if (fechaActual.Hour >= 0 && fechaActual.Hour <= 6)
                        {
                           
                            var temp = fechaActual.AddDays(-1);
                            var newFecha = new DateTime(temp.Year, temp.Month, temp.Day, 23, 59, 59);
                            var registroMarcacionPersonalTemp = ObtenerRegistroMarcacionPersonalDNI(personal.Id, temp, DNI);

                            if (registroMarcacionPersonalTemp == null)
                            {
                                return new ResultadoDTOv2
                                {
                                    Exito = false,
                                    Mensaje = "No existe registro de entrada del día anterior. Debe marcar entrada (M1) antes de marcar salida"
                                };
                            }

                            if (registroMarcacionPersonalTemp.M1 == null)
                            {
                                return new ResultadoDTOv2
                                {
                                    Exito = false,
                                    Mensaje = "Debe marcar entrada (M1) antes de marcar salida"
                                };
                            }

                            if (registroMarcacionPersonalTemp.M2 != null && registroMarcacionPersonalTemp.M3 == null)
                            {
                                return new ResultadoDTOv2
                                {
                                    Exito = false,
                                    Mensaje = "Debe marcar regreso de refrigerio (M3) antes de marcar salida"
                                };
                            }

                            registroMarcacionPersonalTemp.M4 = newFecha.TimeOfDay;
                            registroMarcacionPersonalTemp.M5 = fechaActual.Date.TimeOfDay;
                            registroMarcacionPersonalTemp.M6 = fechaActual.TimeOfDay;
                            registroMarcacionPersonalTemp.UsuarioModificacion = Usuario;
                            registroMarcacionPersonalTemp.FechaModificacion = DateTime.Now;
                            rpta = Update(registroMarcacionPersonalTemp);
                        }
                        else
                        {
                            if (registroMarcacionPersonal != null)
                            {
                                if (registroMarcacionPersonal.M1 == null)
                                {
                                    return new ResultadoDTOv2
                                    {
                                        Exito = false,
                                        Mensaje = "Debe marcar entrada (M1) antes de marcar salida"
                                    };
                                }

                                if (registroMarcacionPersonal.M2 != null && registroMarcacionPersonal.M3 == null)
                                {
                                    return new ResultadoDTOv2
                                    {
                                        Exito = false,
                                        Mensaje = "Debe marcar regreso de refrigerio (M3) antes de marcar salida"
                                    };
                                }

                                if (registroMarcacionPersonal.M4 == null)
                                {
                                    registroMarcacionPersonal.M4 = fechaActual.TimeOfDay;
                                    registroMarcacionPersonal.UsuarioModificacion = Usuario;
                                    registroMarcacionPersonal.FechaModificacion = DateTime.Now;
                                    rpta = Update(registroMarcacionPersonal);
                                }
                                else
                                {
                                    yaMarcado = true;
                                }
                            }
                            else
                            {
                                return new ResultadoDTOv2
                                {
                                    Exito = false,
                                    Mensaje = "Debe marcar entrada (M1) antes de marcar salida"
                                };
                            }
                        }
                        break;
                }

                if (yaMarcado)
                {
                    return new ResultadoDTOv2
                    {
                        Exito = false,
                        Mensaje = "Ya ha marcado en este horario"
                    };
                }

                if (noCumpleTiempoMinimoAlmuerzo)
                {
                    return new ResultadoDTOv2
                    {
                        Exito = false,
                        Mensaje = "No cumple el tiempo mínimo de almuerzo (45 minutos)"
                    };
                }

                return new ResultadoDTOv2
                {
                    Exito = rpta,
                    Mensaje = rpta ? "Marcación registrada correctamente" : "Error al registrar la marcación"
                };
            }
            catch (Exception e)
            {
                return new ResultadoDTOv2
                {
                    Exito = false,
                    Mensaje = e.Message
                };
            }
        }
    }
}