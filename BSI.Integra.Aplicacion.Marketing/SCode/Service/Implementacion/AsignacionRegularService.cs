using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Google.Api;
using Nancy.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsAppMensajeEnviadoApiComercialDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: AsignacionRegularService
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenSector
    /// </summary>
    public class AsignacionRegularService : IAsignacionRegularService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;


        public AsignacionRegularService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsignacionRegular, AsignacionRegular>(MemberList.None).ReverseMap();
                cfg.CreateMap<ObtenerAsesorConfiguracionPorPaisDTO, PaisConfiguracionAsignacionRegularDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public AsignacionRegular Add(AsignacionRegular entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionRegularRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionRegular>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AsignacionRegular Update(AsignacionRegular entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionRegularRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionRegular>(modelo);
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
                _unitOfWork.AsignacionRegularRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionRegular> Add(List<AsignacionRegular> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionRegularRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionRegular>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionRegular> Update(List<AsignacionRegular> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionRegularRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionRegular>>(modelo);
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
                _unitOfWork.AsignacionRegularRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los proveedores de origenes asignados y no asignados
        /// </summary>
        /// <returns>List<ActividadAgendaDTO></returns>
        public bool RegularizarConfiguracionAsignacionRegular()
        {
            try
            {
                List<ValidacionPaisConfiguracionAsignacionRegularDTO> ListaAsignacionRegular = new List<ValidacionPaisConfiguracionAsignacionRegularDTO>();
                ListaAsignacionRegular = _unitOfWork.AsignacionRegularRepository.ObtenerListaDeAsignacionRegular();
                bool Validacion = false;
                foreach (var item in ListaAsignacionRegular)
                {
                    Validacion = _unitOfWork.AsignacionRegularRepository.VerificarConfiguracionPorPais(item.Id);
                }
                return Validacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los proveedores de origenes asignados y no asignados
        /// </summary>
        /// <returns>List<ActividadAgendaDTO></returns>
        public List<ConfiguracionPrincipalDTO> ObtenerConfiguracionAsignacionRegular(int IdGrupoFiltroProgramaCritico)
        {
            try
            {
                bool ActualizacionCorrecta1 = false;
                ActualizacionCorrecta1 = RegularizarConfiguracionAsignacionRegular();

                List<ConfiguracionPrincipalDTO> AsignacionRegularPrincipalConfiguracion = new List<ConfiguracionPrincipalDTO>();
                List<RecibirConfiguracionPrincipalDTO> ListaAsignacionRegularConfPrincipal = new List<RecibirConfiguracionPrincipalDTO>();
                ListaAsignacionRegularConfPrincipal = _unitOfWork.AsignacionRegularRepository.ObtenerCOnfiguracionPrincipal(IdGrupoFiltroProgramaCritico);


                foreach (var item in ListaAsignacionRegularConfPrincipal)
                {
                    ConfiguracionPrincipalDTO VarAsignacionRegularPrincipalConfiguracion = new ConfiguracionPrincipalDTO();
                    RecibirConfiguracionPrincipalPorPaisDTO VarRecibirConfiguracionPrincipalPorPais = new RecibirConfiguracionPrincipalPorPaisDTO();
                    VarAsignacionRegularPrincipalConfiguracion.Id = item.Id;
                    VarAsignacionRegularPrincipalConfiguracion.IdGrupoFiltroProgramaCritico = item.IdGrupoFiltroProgramaCritico;
                    VarAsignacionRegularPrincipalConfiguracion.Codigo = item.Codigo;
                    VarAsignacionRegularPrincipalConfiguracion.Prioridad = item.Prioridad;
                    VarAsignacionRegularPrincipalConfiguracion.Coordinador = item.Coordinador;
                    VarAsignacionRegularPrincipalConfiguracion.Asesor = item.Asesor;
                    VarAsignacionRegularPrincipalConfiguracion.DatoCalidad = item.DatoCalidad;
                    VarAsignacionRegularPrincipalConfiguracion.AplicaProporcionPorPais = item.AplicaProporcionPorPais;
                    VarAsignacionRegularPrincipalConfiguracion.EsLimiteCola = item.EsLimiteCola;
                    VarAsignacionRegularPrincipalConfiguracion.LimiteCola = item.LimiteCola;
                    VarAsignacionRegularPrincipalConfiguracion.PorcentajeTolerancia = item.PorcentajeTolerancia;

                    VarRecibirConfiguracionPrincipalPorPais = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionesPorPais(item.Id, 51);
                    VarAsignacionRegularPrincipalConfiguracion.IdPeru = VarRecibirConfiguracionPrincipalPorPais.Id;
                    VarAsignacionRegularPrincipalConfiguracion.EsProporcionManualPeru = VarRecibirConfiguracionPrincipalPorPais.EsProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionManualPeru = VarRecibirConfiguracionPrincipalPorPais.ProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionPorPaisPeru = VarRecibirConfiguracionPrincipalPorPais.ProporcionPorPais;

                    VarRecibirConfiguracionPrincipalPorPais = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionesPorPais(item.Id, 57);
                    VarAsignacionRegularPrincipalConfiguracion.IdColombia = VarRecibirConfiguracionPrincipalPorPais.Id;
                    VarAsignacionRegularPrincipalConfiguracion.EsProporcionManualColombia = VarRecibirConfiguracionPrincipalPorPais.EsProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionManualColombia = VarRecibirConfiguracionPrincipalPorPais.ProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionPorPaisColombia = VarRecibirConfiguracionPrincipalPorPais.ProporcionPorPais;

                    VarRecibirConfiguracionPrincipalPorPais = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionesPorPais(item.Id, 591);
                    VarAsignacionRegularPrincipalConfiguracion.IdBolivia = VarRecibirConfiguracionPrincipalPorPais.Id;
                    VarAsignacionRegularPrincipalConfiguracion.EsProporcionManualBolivia = VarRecibirConfiguracionPrincipalPorPais.EsProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionManualBolivia = VarRecibirConfiguracionPrincipalPorPais.ProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionPorPaisBolivia = VarRecibirConfiguracionPrincipalPorPais.ProporcionPorPais;

                    VarRecibirConfiguracionPrincipalPorPais = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionesPorPais(item.Id, 52);
                    VarAsignacionRegularPrincipalConfiguracion.IdMexico = VarRecibirConfiguracionPrincipalPorPais.Id;
                    VarAsignacionRegularPrincipalConfiguracion.EsProporcionManualMexico = VarRecibirConfiguracionPrincipalPorPais.EsProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionManualMexico = VarRecibirConfiguracionPrincipalPorPais.ProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionPorPaisMexico = VarRecibirConfiguracionPrincipalPorPais.ProporcionPorPais;

                    VarRecibirConfiguracionPrincipalPorPais = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionesPorPais(item.Id, 0);
                    VarAsignacionRegularPrincipalConfiguracion.IdInternacional = VarRecibirConfiguracionPrincipalPorPais.Id;
                    VarAsignacionRegularPrincipalConfiguracion.EsProporcionManualInternacional = VarRecibirConfiguracionPrincipalPorPais.EsProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionManualInternacional = VarRecibirConfiguracionPrincipalPorPais.ProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionPorPaisInternacional = VarRecibirConfiguracionPrincipalPorPais.ProporcionPorPais;

                    AsignacionRegularPrincipalConfiguracion.Add(VarAsignacionRegularPrincipalConfiguracion);
                }



                //List<ObtenerConfiguracionProgramasOtrasAreasDTO> ListaProgramasOtrasAreas = new List<ObtenerConfiguracionProgramasOtrasAreasDTO>();
                //ListaProgramasOtrasAreas = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionProgramasOtrasAreas(IdGrupoFiltroProgramaCritico);
                //List<ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO> resultadoAgrupado = new List<ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO>();

                //resultadoAgrupado = ListaProgramasOtrasAreas.GroupBy(x => new { x.IdProgramaOtraArea, x.IdGrupoFiltroProgramaCritico, x.IdAsignacionRegular, x.Coordinador, x.Asesor, x.PGventa, x.BaseHistorica, x.DatoCalidad, x.EsLimitePeru, x.LimitePeru, x.EsLimiteColombia, x.LimiteColombia, x.EsLimiteMexico, x.LimiteMexico, x.EsLimiteBolivia, x.LimiteBolivia, x.EsLimiteInternacional, x.LimiteInternacional }).Select(x => new ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO
                //{
                //    IdProgramaOtraArea = x.Key.IdProgramaOtraArea,
                //    IdGrupoFiltroProgramaCritico = x.Key.IdGrupoFiltroProgramaCritico,
                //    IdAsignacionRegular = x.Key.IdAsignacionRegular,
                //    Coordinador = x.Key.Coordinador,
                //    Asesor = x.Key.Asesor,
                //    PGventa = x.Key.PGventa,
                //    BaseHistorica = x.Key.BaseHistorica,
                //    DatoCalidad = x.Key.DatoCalidad,
                //    EsLimitePeru = x.Key.EsLimitePeru,
                //    LimitePeru = x.Key.LimitePeru,
                //    EsLimiteColombia = x.Key.EsLimiteColombia,
                //    LimiteColombia = x.Key.LimiteColombia,
                //    EsLimiteMexico = x.Key.EsLimiteMexico,
                //    LimiteMexico = x.Key.LimiteMexico,
                //    EsLimiteBolivia = x.Key.EsLimiteBolivia,
                //    LimiteBolivia = x.Key.LimiteBolivia,

                //    EsLimiteInternacional = x.Key.EsLimiteInternacional,
                //    LimiteInternacional = x.Key.LimiteInternacional,
                //    ListaProgramasGenerales = x.GroupBy(y => new { y.IdProgramaGeneral, y.Codigo }).Select(y => new ListaProgramasGeneralesDTO
                //    {

                //        IdProgramaGeneral = y.Key.IdProgramaGeneral,
                //        Codigo = y.Key.Codigo,
                //    }).ToList(),
                //}).ToList();

                return AsignacionRegularPrincipalConfiguracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna Los Programa General Que tienen una configuración
        /// </summary>
        /// <returns>List<ActividadAgendaDTO></returns>
        public List<ObtenerBloquePorProgramaCriticoDTO> ObtenerBloquePorProgramaCritico()
        {
            try
            {

                bool ActualizacionCorrecta1 = false;
                ActualizacionCorrecta1 = RegularizarConfiguracionAsignacionRegular();
                List<ObtenerBloquePorProgramaCriticoDTO> ListaBloquePorProgramaCritico = new List<ObtenerBloquePorProgramaCriticoDTO>();
                ListaBloquePorProgramaCritico = _unitOfWork.AsignacionRegularRepository.ObtenerBloquePorProgramaCritico();
                return ListaBloquePorProgramaCritico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna Los Programa General Que tienen una configuración
        /// </summary>
        /// <returns>List<ActividadAgendaDTO></returns>
        public int ActualizarAsignacionRegular(List<ConfiguracionPrincipalDTO> ListaActualizar)
        {
            try
            {
                int ActualizacionCorrecta = 0;
                bool ActualizacionCorrecta1 = false;

                if (ListaActualizar.Count > 0)
                {
                    foreach (var item in ListaActualizar)
                    {
                        ActualizacionCorrecta = _unitOfWork.AsignacionRegularRepository.ActualizarAsignacionRegular(item.Id, item.DatoCalidad, item.AplicaProporcionPorPais, item.EsLimiteCola, item.LimiteCola, item.Prioridad, item.PorcentajeTolerancia, "ActualizarAsignacionRegular");

                        ActualizacionCorrecta = _unitOfWork.AsignacionRegularRepository.ActualizarPaisAsignacionRegular(item.IdPeru, item.EsProporcionManualPeru, item.ProporcionManualPeru, item.ProporcionPorPaisPeru, "ActualizarAsignacionRegular");
                        ActualizacionCorrecta = _unitOfWork.AsignacionRegularRepository.ActualizarPaisAsignacionRegular(item.IdColombia, item.EsProporcionManualColombia, item.ProporcionManualColombia, item.ProporcionPorPaisColombia, "ActualizarAsignacionRegular");
                        ActualizacionCorrecta = _unitOfWork.AsignacionRegularRepository.ActualizarPaisAsignacionRegular(item.IdMexico, item.EsProporcionManualMexico, item.ProporcionManualMexico, item.ProporcionPorPaisMexico, "ActualizarAsignacionRegular");
                        ActualizacionCorrecta = _unitOfWork.AsignacionRegularRepository.ActualizarPaisAsignacionRegular(item.IdBolivia, item.EsProporcionManualBolivia, item.ProporcionManualBolivia, item.ProporcionPorPaisBolivia, "ActualizarAsignacionRegular");
                        ActualizacionCorrecta = _unitOfWork.AsignacionRegularRepository.ActualizarPaisAsignacionRegular(item.IdBolivia, item.EsProporcionManualChile, item.ProporcionManualChile, item.ProporcionPorPaisChile, "ActualizarAsignacionRegular");
                        ActualizacionCorrecta = _unitOfWork.AsignacionRegularRepository.ActualizarPaisAsignacionRegular(item.IdInternacional, item.EsProporcionManualInternacional, item.ProporcionManualInternacional, item.ProporcionPorPaisInternacional, "ActualizarAsignacionRegular");
                    }
                }
                List<VerificarSiAplicaProporcionAsignacionRegularDTO> ListaProgramaGeneralVerificado = new List<VerificarSiAplicaProporcionAsignacionRegularDTO>();

                ActualizacionCorrecta1 = RegularizarConfiguracionAsignacionRegular();
                _unitOfWork.AsignacionRegularRepository.RegularizarConfiguracionTemporalAsignacionRegular();
                ListaProgramaGeneralVerificado = _unitOfWork.AsignacionRegularRepository.VerificarSiAplicaProporcionAsignacionRegular();



                if (ListaProgramaGeneralVerificado.Count > 0 && ListaProgramaGeneralVerificado is not null)

                {

                    string ValidacionPrograma = "";

                    foreach (var item in ListaProgramaGeneralVerificado)

                    {

                        ValidacionPrograma = "Programa general: " + item.Nombre + " --- Estado depués de la validacion : " + item.MensajeValidacion + "<br></br>" + ValidacionPrograma;

                    }

                    EnvioCorreo("Asignacion Automática", "Validación de configuraciones de asignación por paises", ValidacionPrograma, _unitOfWork.AsignacionRegularRepository.ObtenerAddressee());

                }



                return ActualizacionCorrecta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 16/10/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna las configuraciones del tab de programas otras areas
        /// </summary>
        /// <returns>List<ActividadAgendaDTO></returns>
        public int ActualizarConfiguracionProgramasOtrasAreas(List<ConfiguracionPrincipalDTO> ListaActualizar)
        {
            try
            {
                int ActualizacionCorrecta = 0;
                if (ListaActualizar.Count > 0)
                {
                    foreach (var item in ListaActualizar)
                    {
                        ActualizacionCorrecta = _unitOfWork.AsignacionRegularRepository.ActualizarAsignacionRegular(item.Id, item.DatoCalidad, item.AplicaProporcionPorPais, item.EsLimiteCola, item.LimiteCola, item.Prioridad, item.PorcentajeTolerancia, "ActualizarAsignacionRegular");

                        ActualizacionCorrecta = _unitOfWork.AsignacionRegularRepository.ActualizarPaisAsignacionRegular(item.IdPeru, item.EsProporcionManualPeru, item.ProporcionManualPeru, item.ProporcionPorPaisPeru, "ActualizarAsignacionRegular");
                        ActualizacionCorrecta = _unitOfWork.AsignacionRegularRepository.ActualizarPaisAsignacionRegular(item.IdColombia, item.EsProporcionManualColombia, item.ProporcionManualColombia, item.ProporcionPorPaisColombia, "ActualizarAsignacionRegular");
                        ActualizacionCorrecta = _unitOfWork.AsignacionRegularRepository.ActualizarPaisAsignacionRegular(item.IdMexico, item.EsProporcionManualMexico, item.ProporcionManualMexico, item.ProporcionPorPaisMexico, "ActualizarAsignacionRegular");
                        ActualizacionCorrecta = _unitOfWork.AsignacionRegularRepository.ActualizarPaisAsignacionRegular(item.IdBolivia, item.EsProporcionManualBolivia, item.ProporcionManualBolivia, item.ProporcionPorPaisBolivia, "ActualizarAsignacionRegular");
                        ActualizacionCorrecta = _unitOfWork.AsignacionRegularRepository.ActualizarPaisAsignacionRegular(item.IdInternacional, item.EsProporcionManualInternacional, item.ProporcionManualInternacional, item.ProporcionPorPaisInternacional, "ActualizarAsignacionRegular");
                    }
                }
                return ActualizacionCorrecta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los proveedores de origenes asignados y no asignados
        /// </summary>
        /// <returns>List<ActividadAgendaDTO></returns>
        public List<ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO> ObtenerConfiguracionProgramasOtrasAreas(int IdGrupoFiltroProgramaCritico)
        {
            try
            {
                List<ObtenerConfiguracionProgramasOtrasAreasDTO> ListaProgramasOtrasAreas = new List<ObtenerConfiguracionProgramasOtrasAreasDTO>();
                ListaProgramasOtrasAreas = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionProgramasOtrasAreas(IdGrupoFiltroProgramaCritico);
                List<ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO> resultadoAgrupado = new List<ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO>();


                resultadoAgrupado = ListaProgramasOtrasAreas.GroupBy(x => new { x.IdProgramaOtraArea, x.IdGrupoFiltroProgramaCritico, x.IdAsignacionRegular, x.Coordinador, x.Asesor, x.PGventa, x.BaseHistorica, x.DatoCalidad, x.EsLimitePeru, x.LimitePeru, x.EsLimiteColombia, x.LimiteColombia, x.EsLimiteMexico, x.LimiteMexico, x.EsLimiteBolivia, x.LimiteBolivia, x.EsLimiteInternacional, x.LimiteInternacional }).Select(x => new ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO
                {
                    IdProgramaOtraArea = x.Key.IdProgramaOtraArea,
                    IdGrupoFiltroProgramaCritico = x.Key.IdGrupoFiltroProgramaCritico,
                    IdAsignacionRegular = x.Key.IdAsignacionRegular,
                    Coordinador = x.Key.Coordinador,
                    Asesor = x.Key.Asesor,
                    PGventa = x.Key.PGventa,
                    BaseHistorica = x.Key.BaseHistorica,
                    DatoCalidad = x.Key.DatoCalidad,
                    EsLimitePeru = x.Key.EsLimitePeru,
                    LimitePeru = x.Key.LimitePeru,
                    EsLimiteColombia = x.Key.EsLimiteColombia,
                    LimiteColombia = x.Key.LimiteColombia,
                    EsLimiteMexico = x.Key.EsLimiteMexico,
                    LimiteMexico = x.Key.LimiteMexico,
                    EsLimiteBolivia = x.Key.EsLimiteBolivia,
                    LimiteBolivia = x.Key.LimiteBolivia,

                    EsLimiteInternacional = x.Key.EsLimiteInternacional,
                    LimiteInternacional = x.Key.LimiteInternacional,
                    ListaProgramasGenerales = x.GroupBy(y => new { y.IdProgramaGeneral, y.Codigo }).Select(y => new ListaProgramasGeneralesDTO
                    {

                        IdProgramaGeneral = y.Key.IdProgramaGeneral,
                        Codigo = y.Key.Codigo,

                    }).ToList(),
                }).ToList();
                return resultadoAgrupado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna Los Programa General Que tienen una configuración
        /// </summary>
        /// <returns>List<ActividadAgendaDTO></returns>
        public List<ListaProgramasGeneralesDTO> ObtenerComboListaProgramasGenerales()
        {
            try
            {
                List<ListaProgramasGeneralesDTO> ListaProgramasGenerales = new List<ListaProgramasGeneralesDTO>();
                ListaProgramasGenerales = _unitOfWork.AsignacionRegularRepository.ObtenerComboListaProgramasGenerales();
                return ListaProgramasGenerales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna Los Programa General Que tienen una configuración
        /// </summary>
        /// <returns>List<ActividadAgendaDTO></returns>
        public int ActualizarProgramasOtrasAreas(List<ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO> ListaProgramasOtrasAreas)
        {
            try
            {
                int resultado = 0;
                int Actualizar = new int();
                foreach (var item in ListaProgramasOtrasAreas)
                {
                    resultado = _unitOfWork.AsignacionRegularRepository.ActualizarProgramaOtrasAreas(item.IdProgramaOtraArea, item.IdGrupoFiltroProgramaCritico, item.IdAsignacionRegular, item.Coordinador, item.Asesor, item.PGventa, item.BaseHistorica, item.DatoCalidad, item.EsLimitePeru, item.LimitePeru, item.EsLimiteColombia, item.LimiteColombia, item.EsLimiteMexico, item.LimiteMexico, item.EsLimiteBolivia, item.LimiteBolivia, item.EsLimiteInternacional, item.LimiteInternacional, "ProcesoRegulador");
                    List<IdProgramaGeneralDTO>? ListaProgramasGeneralesParaEliminar = new List<IdProgramaGeneralDTO>();
                    List<IdProgramaGeneralDTO>? ListaProgramasGeneralesParaEliminarRec = new List<IdProgramaGeneralDTO>();

                    List<ListaProgramasGeneralesDTO>? ListaProgramasGeneralesParaActualizar = item.ListaProgramasGenerales;
                    ListaProgramasGeneralesParaEliminarRec = _unitOfWork.AsignacionRegularRepository.ObtenerListaProgramas(item.IdProgramaOtraArea);
                    ListaProgramasGeneralesParaEliminar.AddRange(ListaProgramasGeneralesParaEliminarRec);
                    foreach (var x in ListaProgramasGeneralesParaEliminarRec.ToList())
                    {
                        foreach (var y in item.ListaProgramasGenerales)
                        {
                            if (x.IdProgramaGeneral == y.IdProgramaGeneral)
                            {
                                ListaProgramasGeneralesParaEliminar.Remove(x);
                                break;
                            }
                        }

                    }
                    foreach (var x2 in item.ListaProgramasGenerales.ToList())
                    {
                        foreach (var y2 in ListaProgramasGeneralesParaEliminarRec)
                        {
                            if (x2.IdProgramaGeneral == y2.IdProgramaGeneral)
                            {
                                ListaProgramasGeneralesParaActualizar.Remove(x2);
                                break;
                            }
                        }
                    }


                    if (ListaProgramasGeneralesParaActualizar.Count() > 0)
                    {
                        foreach (var x in ListaProgramasGeneralesParaActualizar)
                        {
                            resultado = _unitOfWork.AsignacionRegularRepository.AgregarPaisProgramaOtrasAreas(item.IdProgramaOtraArea, x.IdProgramaGeneral);
                        }
                    }
                    if (ListaProgramasGeneralesParaEliminar.Count() > 0)
                    {
                        foreach (var x in ListaProgramasGeneralesParaEliminar)
                        {
                            resultado = _unitOfWork.AsignacionRegularRepository.EliminarPaisProgramaOtrasAreas(item.IdProgramaOtraArea, x.IdProgramaGeneral);
                        }
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ComboBusquedaDTO ObtenerComboBusqueda()
        {
            try
            {

                ComboBusquedaDTO ListaDeCombos = new ComboBusquedaDTO();

                ListaDeCombos.ComboProgramaCritico = _unitOfWork.AsignacionRegularRepository.ComboGrupoVenta();
                ListaDeCombos.ComboProgramaGeneral = _unitOfWork.AsignacionRegularRepository.ComboProgramaGeneral();
                ListaDeCombos.ComboAsesor = _unitOfWork.AsignacionRegularRepository.ComboAsesor();
                ListaDeCombos.ComboCoordinador = _unitOfWork.AsignacionRegularRepository.ComboPersonalJefe();

                return ListaDeCombos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO> BuscarPorComboSeleccionadosProgramasOtrasAreas(int? IdProgramasGeneral, int? IdGrupoFiltroProgramaCritico, int? IdAsesor, int? IdCoordinador)
        {
            try
            {
                if (IdProgramasGeneral == 0) { IdProgramasGeneral = null; }
                if (IdGrupoFiltroProgramaCritico == 0) { IdGrupoFiltroProgramaCritico = null; }
                if (IdAsesor == 0) { IdAsesor = null; }
                if (IdCoordinador == 0) { IdCoordinador = null; }
                List<ListaIdAsignacionRegularDTO> ListaAsignacionRegular = new List<ListaIdAsignacionRegularDTO>();
                ListaAsignacionRegular = _unitOfWork.AsignacionRegularRepository.BuscarPorComboSeleccionados(IdProgramasGeneral, IdGrupoFiltroProgramaCritico, IdAsesor, IdCoordinador);


                List<ObtenerConfiguracionProgramasOtrasAreasDTO> ListaProgramasOtrasAreas = new List<ObtenerConfiguracionProgramasOtrasAreasDTO>();
                List<ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO> resultadoAgrupado = new List<ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO>();


                foreach (var x in ListaAsignacionRegular)
                {

                    ListaProgramasOtrasAreas.AddRange(_unitOfWork.AsignacionRegularRepository.ObtenerListaConfiguracionesSeleccionadas(x.Id));
                }


                resultadoAgrupado = ListaProgramasOtrasAreas.GroupBy(x => new { x.IdProgramaOtraArea, x.IdGrupoFiltroProgramaCritico, x.IdAsignacionRegular, x.Coordinador, x.Asesor, x.PGventa, x.BaseHistorica, x.DatoCalidad, x.EsLimitePeru, x.LimitePeru, x.EsLimiteColombia, x.LimiteColombia, x.EsLimiteMexico, x.LimiteMexico, x.EsLimiteBolivia, x.LimiteBolivia, x.EsLimiteInternacional, x.LimiteInternacional }).Select(x => new ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO
                {
                    IdProgramaOtraArea = x.Key.IdProgramaOtraArea,
                    IdGrupoFiltroProgramaCritico = x.Key.IdGrupoFiltroProgramaCritico,
                    IdAsignacionRegular = x.Key.IdAsignacionRegular,
                    Coordinador = x.Key.Coordinador,
                    Asesor = x.Key.Asesor,
                    PGventa = x.Key.PGventa,
                    BaseHistorica = x.Key.BaseHistorica,
                    DatoCalidad = x.Key.DatoCalidad,
                    EsLimitePeru = x.Key.EsLimitePeru,
                    LimitePeru = x.Key.LimitePeru,
                    EsLimiteColombia = x.Key.EsLimiteColombia,
                    LimiteColombia = x.Key.LimiteColombia,
                    EsLimiteMexico = x.Key.EsLimiteMexico,
                    LimiteMexico = x.Key.LimiteMexico,
                    EsLimiteBolivia = x.Key.EsLimiteBolivia,
                    LimiteBolivia = x.Key.LimiteBolivia,

                    EsLimiteInternacional = x.Key.EsLimiteInternacional,
                    LimiteInternacional = x.Key.LimiteInternacional,
                    ListaProgramasGenerales = x.GroupBy(y => new { y.IdProgramaGeneral, y.Codigo }).Select(y => new ListaProgramasGeneralesDTO
                    {

                        IdProgramaGeneral = y.Key.IdProgramaGeneral,
                        Codigo = y.Key.Codigo,

                    }).ToList(),
                }).ToList();
                return resultadoAgrupado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionPrincipalDTO> BuscarPorComboSeleccionadosProgramasCriticos(int? IdProgramasGeneral, int? IdGrupoFiltroProgramaCritico, int? IdAsesor, int? IdCoordinador)
        {
            try
            {

                if (IdProgramasGeneral == 0) { IdProgramasGeneral = null; }
                if (IdGrupoFiltroProgramaCritico == 0) { IdGrupoFiltroProgramaCritico = null; }
                if (IdAsesor == 0) { IdAsesor = null; }
                if (IdCoordinador == 0) { IdCoordinador = null; }
                List<ListaIdAsignacionRegularDTO> ListaAsignacionRegular = new List<ListaIdAsignacionRegularDTO>();
                ListaAsignacionRegular = _unitOfWork.AsignacionRegularRepository.BuscarPorComboSeleccionados(IdProgramasGeneral, IdGrupoFiltroProgramaCritico, IdAsesor, IdCoordinador);



                List<ConfiguracionPrincipalDTO> AsignacionRegularPrincipalConfiguracion = new List<ConfiguracionPrincipalDTO>();
                List<RecibirConfiguracionPrincipalDTO> ListaAsignacionRegularConfPrincipal = new List<RecibirConfiguracionPrincipalDTO>();


                foreach (var x in ListaAsignacionRegular)
                {
                    ListaAsignacionRegularConfPrincipal.AddRange(_unitOfWork.AsignacionRegularRepository.ObtenerCOnfiguracionPrincipalCombo(x.Id));
                }

                foreach (var item in ListaAsignacionRegularConfPrincipal)
                {
                    ConfiguracionPrincipalDTO VarAsignacionRegularPrincipalConfiguracion = new ConfiguracionPrincipalDTO();
                    RecibirConfiguracionPrincipalPorPaisDTO VarRecibirConfiguracionPrincipalPorPais = new RecibirConfiguracionPrincipalPorPaisDTO();
                    VarAsignacionRegularPrincipalConfiguracion.Id = item.Id;
                    VarAsignacionRegularPrincipalConfiguracion.IdGrupoFiltroProgramaCritico = item.IdGrupoFiltroProgramaCritico;
                    VarAsignacionRegularPrincipalConfiguracion.Codigo = item.Codigo;
                    VarAsignacionRegularPrincipalConfiguracion.Prioridad = item.Prioridad;
                    VarAsignacionRegularPrincipalConfiguracion.Coordinador = item.Coordinador;
                    VarAsignacionRegularPrincipalConfiguracion.Asesor = item.Asesor;
                    VarAsignacionRegularPrincipalConfiguracion.DatoCalidad = item.DatoCalidad;
                    VarAsignacionRegularPrincipalConfiguracion.AplicaProporcionPorPais = item.AplicaProporcionPorPais;
                    VarAsignacionRegularPrincipalConfiguracion.EsLimiteCola = item.EsLimiteCola;
                    VarAsignacionRegularPrincipalConfiguracion.LimiteCola = item.LimiteCola;
                    VarAsignacionRegularPrincipalConfiguracion.PorcentajeTolerancia = item.PorcentajeTolerancia;

                    VarRecibirConfiguracionPrincipalPorPais = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionesPorPais(item.Id, 51);
                    VarAsignacionRegularPrincipalConfiguracion.IdPeru = VarRecibirConfiguracionPrincipalPorPais.Id;
                    VarAsignacionRegularPrincipalConfiguracion.EsProporcionManualPeru = VarRecibirConfiguracionPrincipalPorPais.EsProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionManualPeru = VarRecibirConfiguracionPrincipalPorPais.ProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionPorPaisPeru = VarRecibirConfiguracionPrincipalPorPais.ProporcionPorPais;

                    VarRecibirConfiguracionPrincipalPorPais = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionesPorPais(item.Id, 57);
                    VarAsignacionRegularPrincipalConfiguracion.IdColombia = VarRecibirConfiguracionPrincipalPorPais.Id;
                    VarAsignacionRegularPrincipalConfiguracion.EsProporcionManualColombia = VarRecibirConfiguracionPrincipalPorPais.EsProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionManualColombia = VarRecibirConfiguracionPrincipalPorPais.ProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionPorPaisColombia = VarRecibirConfiguracionPrincipalPorPais.ProporcionPorPais;

                    VarRecibirConfiguracionPrincipalPorPais = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionesPorPais(item.Id, 591);
                    VarAsignacionRegularPrincipalConfiguracion.IdBolivia = VarRecibirConfiguracionPrincipalPorPais.Id;
                    VarAsignacionRegularPrincipalConfiguracion.EsProporcionManualBolivia = VarRecibirConfiguracionPrincipalPorPais.EsProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionManualBolivia = VarRecibirConfiguracionPrincipalPorPais.ProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionPorPaisBolivia = VarRecibirConfiguracionPrincipalPorPais.ProporcionPorPais;

                    VarRecibirConfiguracionPrincipalPorPais = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionesPorPais(item.Id, 52);
                    VarAsignacionRegularPrincipalConfiguracion.IdMexico = VarRecibirConfiguracionPrincipalPorPais.Id;
                    VarAsignacionRegularPrincipalConfiguracion.EsProporcionManualMexico = VarRecibirConfiguracionPrincipalPorPais.EsProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionManualMexico = VarRecibirConfiguracionPrincipalPorPais.ProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionPorPaisMexico = VarRecibirConfiguracionPrincipalPorPais.ProporcionPorPais;

                    VarRecibirConfiguracionPrincipalPorPais = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionesPorPais(item.Id, 56);
                    VarAsignacionRegularPrincipalConfiguracion.IdChile = VarRecibirConfiguracionPrincipalPorPais.Id;
                    VarAsignacionRegularPrincipalConfiguracion.EsProporcionManualMexico = VarRecibirConfiguracionPrincipalPorPais.EsProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionManualMexico = VarRecibirConfiguracionPrincipalPorPais.ProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionPorPaisMexico = VarRecibirConfiguracionPrincipalPorPais.ProporcionPorPais;

                    VarRecibirConfiguracionPrincipalPorPais = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionesPorPais(item.Id, 0);
                    VarAsignacionRegularPrincipalConfiguracion.IdInternacional = VarRecibirConfiguracionPrincipalPorPais.Id;
                    VarAsignacionRegularPrincipalConfiguracion.EsProporcionManualInternacional = VarRecibirConfiguracionPrincipalPorPais.EsProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionManualInternacional = VarRecibirConfiguracionPrincipalPorPais.ProporcionManual;
                    VarAsignacionRegularPrincipalConfiguracion.ProporcionPorPaisInternacional = VarRecibirConfiguracionPrincipalPorPais.ProporcionPorPais;

                    AsignacionRegularPrincipalConfiguracion.Add(VarAsignacionRegularPrincipalConfiguracion);
                }
                return AsignacionRegularPrincipalConfiguracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// El metodo saca las oportunidades de asignación automatica, obtiene sus configuraciones para comparar con los asesores activos que vendan su programa y hacer la asignación, es importante regularizar todas las tablas de configuración 
        /// </summary>
        /// <returns>bool</returns>
        public bool AsignacionAutomatizadaAsesor(string Usuario)
        {
            try
            {

                EnvioCorreoAsignacion("Inicio de la asignacion de datos");

                bool? EstadoActualizacion = false;
                List<ObtenerOportunidadConfiguradaV2DTO> ListaOportunidadesConfiguradas = new List<ObtenerOportunidadConfiguradaV2DTO>();
                ListaOportunidadesConfiguradas = _unitOfWork.AsignacionRegularRepository.ObtenerOportunidadConfigurada();

                foreach (ObtenerOportunidadConfiguradaV2DTO Opor in ListaOportunidadesConfiguradas)
                {
                    try
                    {
                        List<ObtenerAsesoresPorOportunidadDTO> ListaAsesoresPorCofiguracion = new List<ObtenerAsesoresPorOportunidadDTO>();
                        ListaAsesoresPorCofiguracion = _unitOfWork.AsignacionRegularRepository.ObtenerAsesoresPorOportunidad(Opor.IdPGeneral);
                        foreach (ObtenerAsesoresPorOportunidadDTO asesor in ListaAsesoresPorCofiguracion)
                        {
                            if (asesor.ActivarAsignacionPaisConfiguracion == true && asesor.ActivarAsignacionAutomatica == true && asesor.IdPGeneral == Opor.IdPGeneral)
                            {
                                if (asesor.CantidadTotal < asesor.TopeOportunidad || (Opor.AsignacionDirecta == true || Opor.AsignacionDirectaWhatsapp == true || Opor.AsigancionDirectaMailing == true))
                                {
                                    AsignarAsesorManualDTO? data = new AsignarAsesorManualDTO();
                                    data.IdOportunidades = new int?[] { Opor.Id };
                                    data.IdAsesor = asesor.IdPersonal;
                                    data.FechaProgramada = null;
                                    data.IdCentroCosto = new int();
                                    data.SegunMejorPro = false;
                                    var ListaDetalle = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionDetalle(asesor.Id.Value);
                                    var estadoAsignacion = false;
                                    foreach (var detalleConfiguracionPais in ListaDetalle)
                                    {
                                        if (detalleConfiguracionPais.IdPais == Opor.IdPais)
                                        {
                                            if (Opor.AsignacionDirecta == true && detalleConfiguracionPais.DatoCalidad == true)
                                            {
                                                data.SegunMejorPro = true;
                                                data.envioWhats = true;

                                                AsignacionManualService whats = new AsignacionManualService(_unitOfWork);
                                                whats.AsignarAsesor(data, Usuario);
                                                estadoAsignacion = true;
                                                //EnvioCorreoAsignacion("Fin Asignacion de datos");
                                                break;
                                            }
                                            if (Opor.AsignacionDirectaWhatsapp == true && detalleConfiguracionPais.DatoCalidadWhatsapp == true)
                                            {
                                                data.SegunMejorPro = true;
                                                data.envioWhats = true;

                                                AsignacionManualService whats = new AsignacionManualService(_unitOfWork);

                                                whats.AsignarAsesor(data, Usuario);
                                                //EnvioCorreoAsignacion("Fin Asignacion de datos");
                                                estadoAsignacion = true;
                                                break;
                                            }
                                            if (Opor.AsigancionDirectaMailing == true && detalleConfiguracionPais.DatoCalidadMailing == true)
                                            {
                                                data.SegunMejorPro = true;
                                                data.envioWhats = true;

                                                AsignacionManualService whats = new AsignacionManualService(_unitOfWork);
                                                whats.AsignarAsesor(data, Usuario);
                                                //EnvioCorreoAsignacion("Fin Asignacion de datos");
                                                estadoAsignacion = true;
                                                break;
                                            }

                                            if (Opor.AsignacionRegular == true && Opor.AsignacionDirecta == false && Opor.AsignacionDirectaWhatsapp == false && Opor.AsigancionDirectaMailing == false)
                                            {

                                                var cantidad = 0;

                                                switch (detalleConfiguracionPais.IdPais)
                                                {
                                                    case 51:
                                                        cantidad = asesor.CantidadTotalPeru.GetValueOrDefault();
                                                        break;
                                                    case 52:
                                                        cantidad = asesor.CantidadTotalMexico.GetValueOrDefault();
                                                        break;
                                                    case 56:
                                                        cantidad = asesor.CantidadTotalChile.GetValueOrDefault();
                                                        break;
                                                    case 57:
                                                        cantidad = asesor.CantidadTotalColombia.GetValueOrDefault();
                                                        break;
                                                    case 591:
                                                        cantidad = asesor.CantidadTotalBolivia.GetValueOrDefault();
                                                        break;
                                                    case 0:
                                                        cantidad = asesor.CantidadTotalInternacional.GetValueOrDefault();
                                                        break;
                                                }

                                                if (cantidad < detalleConfiguracionPais.Distribucion && detalleConfiguracionPais.Distribucion > 0)
                                                {
                                                    data.envioWhats = true;

                                                    AsignacionManualService whats = new AsignacionManualService(_unitOfWork);
                                                    whats.AsignarAsesor(data, Usuario);
                                                    estadoAsignacion = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    if(estadoAsignacion == true)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                }
                EnvioCorreoAsignacion("Fin Asignacion de datos");
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Ramirez
        /// Fecha: 09/07/2024
        /// Version: 1.0
        /// <summary>
        /// El metodo saca las oportunidades de asignación automatica, obtiene sus configuraciones para comparar con los asesores activos que vendan su programa y hacer la asignación, es importante regularizar todas las tablas de configuración 
        /// </summary>
        /// <returns>bool</returns>

        public bool AsignacionAutomatizadaAsesorWhatsapp(string Usuario)
        {
            try
            {
                EnvioCorreoAsignacion("Inicio de la asignacion de datos");

                List<ObtenerOportunidadConfiguradaV2DTO> ListaOportunidadesConfiguradas = _unitOfWork.AsignacionRegularRepository.ObtenerOportunidadConfigurada();

                // Inicializar el índice del asesor
                int indiceAsesor = 0;
                var asesoresActivosPorConfiguracion = new Dictionary<int, List<ObtenerAsesoresPorOportunidadDTO>>();

                foreach (var oportunidad in ListaOportunidadesConfiguradas)
                {
                    try
                    {
                        if (!asesoresActivosPorConfiguracion.ContainsKey(oportunidad.IdPGeneral.Value))
                        {
                            List<ObtenerAsesoresPorOportunidadDTO> asesores = _unitOfWork.AsignacionRegularRepository.ObtenerAsesoresPorOportunidad(oportunidad.IdPGeneral);
                            asesoresActivosPorConfiguracion[oportunidad.IdPGeneral.Value] = asesores.Where(asesor =>
                                asesor.ActivarAsignacionPaisConfiguracion == true && asesor.ActivarAsignacionAutomatica == true && asesor.IdPGeneral == oportunidad.IdPGeneral).ToList();
                        }

                        var asesoresActivos = asesoresActivosPorConfiguracion[oportunidad.IdPGeneral.Value];

                        if (asesoresActivos.Count == 0)
                        {
                            continue;
                        }

                        // Obtener el asesor actual para asignar
                        var asesor = asesoresActivos[indiceAsesor % asesoresActivos.Count];

                        if (asesor.CantidadTotal < asesor.TopeOportunidad || oportunidad.AsignacionDirecta == true || oportunidad.AsignacionDirectaWhatsapp == true || oportunidad.AsigancionDirectaMailing == true)
                        {
                            AsignarAsesorManualDTO data = new AsignarAsesorManualDTO
                            {
                                IdOportunidades = new int?[] { oportunidad.Id },
                                IdAsesor = asesor.IdPersonal,
                                FechaProgramada = null,
                                IdCentroCosto = new int(),
                                SegunMejorPro = false
                            };

                            var ListaDetalle = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionDetalle(asesor.Id.Value);
                            bool estadoAsignacion = false;

                            foreach (var detalleConfiguracionPais in ListaDetalle)
                            {
                                if (detalleConfiguracionPais.IdPais == oportunidad.IdPais)
                                {

                                    if (oportunidad.AsignacionDirectaWhatsapp == true && detalleConfiguracionPais.DatoCalidadWhatsapp == true)
                                    {
                                        data.SegunMejorPro = true;
                                        data.envioWhats = true;

                                        AsignacionManualService whats = new AsignacionManualService(_unitOfWork);
                                        whats.AsignarAsesor(data, Usuario);
                                        estadoAsignacion = true;
                                        break;
                                    }

                                }
                            }

                            if (estadoAsignacion)
                            {
                                // Aumentar el índice del asesor solo si se asignó una oportunidad
                                indiceAsesor++;

                            }
                        }
                    }
                    catch (Exception exAsesores)
                    {
                        Console.WriteLine($"Error al obtener asesores para la oportunidad {oportunidad.Id}: {exAsesores.Message}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// El metodo saca las oportunidades de asignación automatica, obtiene sus configuraciones para comparar con los asesores activos que vendan su programa y hacer la asignación, es importante regularizar todas las tablas de configuración 
        /// </summary>
        /// <returns>bool</returns>
        public bool? AsignarAsesor(int? IdOportunidadPreAsignada, int? IdasignacionRegular)
        {
            try
            {
                bool? EstadoActualizacion = false;
                EstadoActualizacion = _unitOfWork.AsignacionRegularRepository.ActualizarRegistroPorInsertarAsesor(IdOportunidadPreAsignada, IdasignacionRegular);
                return EstadoActualizacion;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// método que funciona el de asignación, manda los datos a V4 para que el dato se asigne
        /// </summary>
        /// <returns>bool</returns>
        public bool UrlPost(string UrlBase, string jsonStringResult)
        {

            try
            {
                var http = (HttpWebRequest)WebRequest.Create(new Uri(UrlBase));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";

                string parsedContent = jsonStringResult;
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                var response = http.GetResponse();

                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// método que manda correos por sender
        /// </summary>
        /// <returns>bool</returns>
        public bool EnvioCorreo(string displayname, string subject, string mensaje, List<AddresseeDTO> listaReceptores)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    SenderDTO Sender = new SenderDTO();
                    Sender = _unitOfWork.AsignacionRegularRepository.ObtenerSender();


                    //CONFIGURACION DEL MENSAJE
                    MailMessage mail = new MailMessage();
                    mail.To.Add("emayta@bsginstitute.com");

                    if (listaReceptores != null && listaReceptores.Count > 0)
                    {
                        foreach (var copia in listaReceptores)
                        {
                            mail.Bcc.Add(copia.Email);
                        }
                    }
                    mail.From = new MailAddress("emayta@bsginstitute.com", displayname, System.Text.Encoding.UTF8);
                    mail.Subject = subject;
                    mail.Body = mensaje;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    //CONFIGURACIÓN DEL STMP

                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;

                    smtp.Credentials = new System.Net.NetworkCredential(Sender.Email, Sender.Contrasenia);// Enter seders   User name and password
                    smtp.EnableSsl = true;
                    smtp.Send(mail)
;
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
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de asesores
        /// </summary>
        /// <returns>List<ObtenerListaAsesorDTO></returns>
        public List<ObtenerListaAsesorDTO> ObtenerListaAsesor()
        {
            try
            {
                List<ObtenerListaAsesorDTO> ListaAsignacionRegular = new List<ObtenerListaAsesorDTO>();
                ListaAsignacionRegular = _unitOfWork.AsignacionRegularRepository.ObtenerListaAsesor();
                return ListaAsignacionRegular;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los asesores disponibles para la asignacion
        /// </summary>
        /// <returns>ObtenerListaAsesorDTO</returns>
        public ObtenerListaAsesorDTO ObtenerAsesorConfiguracion(int id)
        {
            try
            {
                ObtenerListaAsesorDTO AsesorConfiguracion = new ObtenerListaAsesorDTO();
                AsesorConfiguracion = _unitOfWork.AsignacionRegularRepository.ObtenerAsesorConfiguracion(id);
                return AsesorConfiguracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de asesores
        /// </summary>
        /// <returns>List<ObtenerAsesorConfiguracionPorPaisDTO></returns>



        public List<ObtenerAsesorConfiguracionPorPaisDTO> ObtenerAsesorConfiguracionPorPais(int id)
        {
            try
            {
                List<ObtenerAsesorConfiguracionPorPaisDTO> configuracionPais = _unitOfWork.AsignacionRegularRepository.ObtenerAsesorConfiguracionPorPais(id);


                List<ObtenerAsesorConfiguracionPorPaisDTO> resultado = new List<ObtenerAsesorConfiguracionPorPaisDTO>();


                foreach (var config in configuracionPais)
                {
                    ObtenerAsesorConfiguracionPorPaisDTO padre = new ObtenerAsesorConfiguracionPorPaisDTO
                    {
                        Id = config.Id,
                        IdAsignacionRegular = config.IdAsignacionRegular,
                        Codigo = config.Codigo,
                        CantidadTotal = config.CantidadTotal,
                        ActivarAsignacionPaisConfiguracion = config.ActivarAsignacionPaisConfiguracion,
                        CantidadTotalPeru = config.CantidadTotalPeru,
                        CantidadTotalChile = config.CantidadTotalChile,
                        CantidadTotalMexico = config.CantidadTotalMexico,
                        CantidadTotalColombia = config.CantidadTotalColombia,
                        CantidadTotalBolivia = config.CantidadTotalBolivia,
                        CantidadTotalInternacional = config.CantidadTotalInternacional,

                    };


                    var detalles = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionDetallePais(config.Id);

                    foreach (var detalle in detalles)
                    {
                        switch (detalle.IdPais)
                        {
                            case 51: // Perú
                                padre.DatoCalidadPeru = detalle.DatoCalidad;
                                padre.DatoCalidadMailingPeru = detalle.DatoCalidadMailing;
                                padre.DatoCalidadWhatsappPeru = detalle.DatoCalidadWhatsapp;
                                padre.DistribucionPeru = detalle.Distribucion;
                                break;
                            case 56: // Chile
                                padre.DatoCalidadChile = detalle.DatoCalidad;
                                padre.DatoCalidadMailingChile = detalle.DatoCalidadMailing;
                                padre.DatoCalidadWhatsappChile = detalle.DatoCalidadWhatsapp;
                                padre.DistribucionChile = detalle.Distribucion;
                                break;
                            case 52: // Mexico
                                padre.DatoCalidadMexico = detalle.DatoCalidad;
                                padre.DatoCalidadMailingMexico = detalle.DatoCalidadMailing;
                                padre.DatoCalidadWhatsappMexico = detalle.DatoCalidadWhatsapp;
                                padre.DistribucionMexico = detalle.Distribucion;
                                break;
                            case 57: // Colombia
                                padre.DatoCalidadColombia = detalle.DatoCalidad;
                                padre.DatoCalidadMailingColombia = detalle.DatoCalidadMailing;
                                padre.DatoCalidadWhatsappColombia = detalle.DatoCalidadWhatsapp;
                                padre.DistribucionColombia = detalle.Distribucion;
                                break;
                            case 591: // Bolivia
                                padre.DatoCalidadBolivia = detalle.DatoCalidad;
                                padre.DatoCalidadMailingBolivia = detalle.DatoCalidadMailing;
                                padre.DatoCalidadWhatsappBolivia = detalle.DatoCalidadWhatsapp;
                                padre.DistribucionBolivia = detalle.Distribucion;
                                break;
                            default:  // Internacional
                                padre.DatoCalidadInternacional = detalle.DatoCalidad;
                                padre.DatoCalidadMailingInternacional = detalle.DatoCalidadMailing;
                                padre.DatoCalidadWhatsappInternacional = detalle.DatoCalidadWhatsapp;
                                padre.DistribucionInternacional = detalle.Distribucion;
                                break;
                        }
                    }

                    resultado.Add(padre);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los combos de asesores 
        /// </summary>
        /// <returns>List<ComboAsesoresDTO></returns>
        public List<ComboAsesoresDTO> ObtenerComboAsesores()
        {
            try
            {
                List<ComboAsesoresDTO> ListaAsignacionRegular = new List<ComboAsesoresDTO>();
                ListaAsignacionRegular = _unitOfWork.AsignacionRegularRepository.ComboAsesores();
                return ListaAsignacionRegular;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta un nuvo asesor a la configuración de asignación automática
        /// </summary>
        /// <returns>bool</returns>
        public bool? InsertarAsignacionRegular(InsertarAsignacionRegularDTO ListaIdAsignacionRegular, String UsuarioCreacion)
        {
            try
            {
                var listaOportunidadActualFaseActual = string.Join(",", ListaIdAsignacionRegular.Id.Select(x => ((uint)x)));
                bool? RespuestaBool = new bool();
                RespuestaBool = _unitOfWork.AsignacionRegularRepository.InsertarAsignacionRegular(listaOportunidadActualFaseActual, UsuarioCreacion);
                return RespuestaBool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta una nueva categoria origen al sector
        /// </summary>
        /// <returns>bool</returns>
        public bool? InsertarCategoriaOrigenPorSector(int IdOrigenSector, ListaCategoriaOrigenDTO ListaCategoriaOrigen, String UsuarioCreacion)
        {
            try
            {
                var ListaCategoriaOrigenConcatenado = string.Join(",", ListaCategoriaOrigen.Id.Select(x => ((uint)x)));
                bool? RespuestaBool = new bool();
                RespuestaBool = _unitOfWork.AsignacionRegularRepository.InsertarCategoriaOrigenPorSector(ListaCategoriaOrigenConcatenado, IdOrigenSector, UsuarioCreacion);
                return RespuestaBool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Por medio de un boleano se agrupa o desagrupa la configuración para las categoria origen
        /// </summary>
        /// <returns>bool</returns>
        public bool? AgruparCategoriaOrigen(bool Agrupar, int id, String UsuarioModificacion)
        {
            try
            {
                bool? RespuestaBool = new bool();
                RespuestaBool = _unitOfWork.AsignacionRegularRepository.AgruparCategoriaOrigen(Agrupar, id, UsuarioModificacion);
                return RespuestaBool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina la configuración de una categoria origen para un sector
        /// </summary>
        /// <returns>bool</returns>
        public bool? EliminarConfiguracionCategoriaOrigen(int Id, String UsuarioModificacion)
        {
            try
            {
                bool? RespuestaBool = new bool();
                RespuestaBool = _unitOfWork.AsignacionRegularRepository.EliminarConfiguracionCategoriaOrigen(Id, UsuarioModificacion);
                return RespuestaBool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta un nuevo sector a la configuracion
        /// </summary>
        /// <returns>bool</returns>
        public bool? InsertarOrigenSector(InsertarOrigenSectorDTO OrigenSector, string UsuarioCreacion)
        {
            try
            {
                bool? RespuestaBool = new bool();
                RespuestaBool = _unitOfWork.AsignacionRegularRepository.InsertarOrigenSector(OrigenSector.Nombre, OrigenSector.Descripcion, UsuarioCreacion);
                return RespuestaBool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina un registro de sector con todo y sus configuraciones de categoria origen
        /// </summary>
        /// <returns>bool</returns>
        public bool? EliminarOrigenSector(int Id, string UsuarioModificacion)
        {
            try
            {
                bool? RespuestaBool = new bool();
                RespuestaBool = _unitOfWork.AsignacionRegularRepository.EliminarOrigenSector(Id, UsuarioModificacion);
                return RespuestaBool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta una nueva configuracion para el asesor
        /// </summary>
        /// <returns>bool</returns>
        public bool? InsertarConfiguracionAsignacionRegular(int IdAsignacionRegular, InsertarProgramaGeneralAsignacionRegularDTO ListaIdAsignacionRegular, String UsuarioCreacion)
        {
            try
            {
                bool? RespuestaBool = new bool();
                foreach (int id in ListaIdAsignacionRegular.idProgramaGeneral)
                {
                    RespuestaBool = _unitOfWork.AsignacionRegularRepository.InsertarConfiguracionAsignacionRegular(IdAsignacionRegular, id, UsuarioCreacion);
                }
                return RespuestaBool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las categoria origen disponibles para asignación
        /// </summary>
        /// <returns>bool</returns>
        public List<ListaCategoriaOrigenNoConfigurada> ObtenerCategoriaOrigen()
        {
            try
            {
                List<ListaCategoriaOrigenNoConfigurada> ListaCategoriaOrigenNoConfigurada = new List<ListaCategoriaOrigenNoConfigurada>();
                ListaCategoriaOrigenNoConfigurada = _unitOfWork.AsignacionRegularRepository.ObtenerCategoriaOrigen();

                return ListaCategoriaOrigenNoConfigurada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la configuracion por los asesores disponibles para la asignacion
        /// </summary>
        /// <returns>bool</returns>
        public List<CategoriaOrigenPorSectorDTO> ObtenerCategoriaOrigenPorSector(int IdOrigenSector)
        {
            try
            {
                List<CategoriaOrigenPorSectorDTO> ListaCategoriaOrigenPorSector = new List<CategoriaOrigenPorSectorDTO>();
                ListaCategoriaOrigenPorSector = _unitOfWork.AsignacionRegularRepository.ObtenerCategoriaOrigenPorSector(IdOrigenSector);

                return ListaCategoriaOrigenPorSector;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza por lista la configuración de un asesor
        /// </summary>
        /// <returns>bool</returns>
        /// 

        public bool? ActualizarConfiguracionAsignacionRegular(List<ObtenerAsesorConfiguracionPorPaisDTO> listaConfiguracionAsignacionRegular, string usuarioModificacion)
        {
            try
            {
                bool? respuestaBool = false;
                foreach (var configuracion in listaConfiguracionAsignacionRegular)
                {
                    var asesorConfiguracion = _mapper.Map<PaisConfiguracionAsignacionRegularDTO>(configuracion);
                    respuestaBool = _unitOfWork.AsignacionRegularRepository.ActualizarConfiguracionAsignacionRegular(asesorConfiguracion, usuarioModificacion);

                    if (respuestaBool == true)
                    {
                        List<int> listaPaises = new List<int> { 51, 52, 56, 591, 57, 0 };

                        foreach (var idPais in listaPaises)
                        {
                            var existeConfiguracionDetalle = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionDetallePorIdPais(configuracion.Id, idPais);

                            if (existeConfiguracionDetalle == null)
                            {
                                if (TieneDatosParaInsertar(configuracion, idPais))
                                {
                                    DetallePaisConfiguracionAsignacionRegularDTO nuevoDetalle = new DetallePaisConfiguracionAsignacionRegularDTO
                                    {
                                        IdPaisConfiguracionAsignacionRegular = configuracion.Id,
                                        DatoCalidad = ObtenerDatoCalidad(configuracion, idPais),
                                        DatoCalidadWhatsapp = ObtenerDatoCalidadWhatsapp(configuracion, idPais),
                                        DatoCalidadMailing = ObtenerDatoCalidadMailing(configuracion, idPais),
                                        Distribucion = ObtenerDistribucion(configuracion, idPais),
                                        IdPais = idPais,

                                    };

                                    _unitOfWork.AsignacionRegularRepository.InsertarConfiguracionAsignacioDetalle(nuevoDetalle, usuarioModificacion);
                                }
                            }
                            else
                            {

                                existeConfiguracionDetalle.DatoCalidad = ObtenerDatoCalidad(configuracion, idPais);
                                existeConfiguracionDetalle.DatoCalidadWhatsapp = ObtenerDatoCalidadWhatsapp(configuracion, idPais);
                                existeConfiguracionDetalle.DatoCalidadMailing = ObtenerDatoCalidadMailing(configuracion, idPais);
                                existeConfiguracionDetalle.Distribucion = ObtenerDistribucion(configuracion, idPais);
                                _unitOfWork.AsignacionRegularRepository.ActualizarConfiguracionDetalle(existeConfiguracionDetalle, usuarioModificacion);
                                _unitOfWork.Commit();
                            }
                        }
                    }
                }
                return respuestaBool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool TieneDatosParaInsertar(ObtenerAsesorConfiguracionPorPaisDTO configuracion, int idPais)
        {
            bool idValido = configuracion.IdAsignacionRegular > 0;
            switch (idPais)
            {
                case 51:

                    return configuracion.CantidadTotalPeru > 0 || configuracion.DatoCalidadPeru || configuracion.DatoCalidadWhatsappPeru || configuracion.DatoCalidadMailingPeru || idValido;

                case 57:

                    return configuracion.CantidadTotalColombia > 0 || configuracion.DatoCalidadColombia || configuracion.DatoCalidadWhatsappColombia || configuracion.DatoCalidadMailingColombia || idValido;
                case 56:
                    return configuracion.CantidadTotalChile > 0 || configuracion.DatoCalidadChile || configuracion.DatoCalidadWhatsappChile || configuracion.DatoCalidadMailingChile || idValido;
                case 591:

                    return configuracion.CantidadTotalBolivia > 0 || configuracion.DatoCalidadBolivia || configuracion.DatoCalidadWhatsappBolivia || configuracion.DatoCalidadMailingBolivia || idValido;
                case 52:

                    return configuracion.CantidadTotalMexico > 0 || configuracion.DatoCalidadMexico || configuracion.DatoCalidadWhatsappMexico || configuracion.DatoCalidadMailingMexico || idValido;
                case 0:

                    return configuracion.CantidadTotalInternacional > 0 || configuracion.DatoCalidadInternacional || configuracion.DatoCalidadWhatsappInternacional || configuracion.DatoCalidadMailingInternacional || idValido;

                default:
                    return false;
            }
        }



        private bool ObtenerDatoCalidadWhatsapp(ObtenerAsesorConfiguracionPorPaisDTO configuracion, int idPais)
        {
            switch (idPais)
            {
                case 51: return configuracion.DatoCalidadWhatsappPeru;
                case 52: return configuracion.DatoCalidadWhatsappMexico;
                case 56: return configuracion.DatoCalidadWhatsappChile;
                case 591: return configuracion.DatoCalidadWhatsappBolivia;
                case 57: return configuracion.DatoCalidadWhatsappColombia;
                case 0: return configuracion.DatoCalidadWhatsappInternacional;
                default: return false;
            }
        }
        private bool ObtenerDatoCalidad(ObtenerAsesorConfiguracionPorPaisDTO configuracion, int idPais)
        {
            switch (idPais)
            {
                case 51: return configuracion.DatoCalidadPeru;
                case 52: return configuracion.DatoCalidadMexico;
                case 56: return configuracion.DatoCalidadChile;
                case 591: return configuracion.DatoCalidadBolivia;
                case 57: return configuracion.DatoCalidadColombia;
                case 0: return configuracion.DatoCalidadInternacional;
                default: return false;
            }
        }

        private bool ObtenerDatoCalidadMailing(ObtenerAsesorConfiguracionPorPaisDTO configuracion, int idPais)
        {
            switch (idPais)
            {
                case 51: return configuracion.DatoCalidadMailingPeru;
                case 52: return configuracion.DatoCalidadMailingMexico;
                case 56: return configuracion.DatoCalidadMailingChile;
                case 591: return configuracion.DatoCalidadMailingBolivia;
                case 57: return configuracion.DatoCalidadMailingColombia;
                case 0: return configuracion.DatoCalidadMailingInternacional;
                default: return false;
            }
        }

        private int ObtenerDistribucion(ObtenerAsesorConfiguracionPorPaisDTO configuracion, int idPais)
        {
            switch (idPais)
            {
                case 51:
                    return configuracion.DistribucionPeru;
                case 52: return configuracion.DistribucionMexico;
                case 56: return configuracion.DistribucionChile;
                case 591: return configuracion.DistribucionBolivia;
                case 57: return configuracion.DistribucionColombia;
                case 0: return configuracion.DistribucionInternacional;

                default:
                    return 0;
            }
        }

        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el tope de oportundiades que puede recibir un asesor
        /// </summary>
        /// <returns>bool</returns>
        public bool? ActualizarTopeOportunidad(int idAsignacionRegular, int TopeOportunidad, String UsuarioModificacion)
        {
            try
            {
                bool? RespuestaBool = new bool();
                RespuestaBool = _unitOfWork.AsignacionRegularRepository.ActualizarTopeOportunidad(idAsignacionRegular, TopeOportunidad, UsuarioModificacion);
                return RespuestaBool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /// <summary>
        /// Autor: Miguel Valdivia
        /// Fecha: 27/08/2025
        /// Version: 1.0
        /// Actualiza el tope de asignación diaria
        /// </summary>
        /// <param name="idAsignacionRegular">ID de la asignación regular</param>
        /// <param name="TopeAsignacionDiaria">Nuevo tope de asignación diaria</param>
        /// <param name="UsuarioModificacion">Usuario que realiza la modificación</param>
        /// <returns>bool? - True si se actualizó correctamente</returns>
        public bool? ActualizarTopeAsignacionDiaria(int idAsignacionRegular, int TopeAsignacionDiaria, String UsuarioModificacion)
        {
            try
            {
                bool? RespuestaBool = new bool();
                RespuestaBool = _unitOfWork.AsignacionRegularRepository.ActualizarTopeAsignacionDiaria(idAsignacionRegular, TopeAsignacionDiaria, UsuarioModificacion);
                return RespuestaBool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 20/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza la prioridad del asesor
        /// </summary>
        /// <returns>bool</returns>
        public bool? ActualizarPrioridad(int idAsignacionRegular, int Prioridad, String UsuarioModificacion)
        {
            try
            {
                bool? RespuestaBool = new bool();
                RespuestaBool = _unitOfWork.AsignacionRegularRepository.ActualizarPrioridad(idAsignacionRegular, Prioridad, UsuarioModificacion);
                return RespuestaBool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina un asesor de la asignación
        /// </summary>
        /// <returns>bool</returns>
        public bool? EliminarAsignacionRegular(int IdAsignacionRegular, String UsuarioCreacion)
        {
            try
            {
                bool? RespuestaBool = new bool();
                RespuestaBool = _unitOfWork.AsignacionRegularRepository.EliminarAsignacionRegular(IdAsignacionRegular, UsuarioCreacion);
                return RespuestaBool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina el registro de configuracion de un programa general por asesor
        /// </summary>
        /// <returns>bool</returns>
        public bool? EliminarPaisConfiguracionAsignacionRegular(int id, String UsuarioModificacion)
        {
            try
            {
                bool? RespuestaBool = new bool();
                RespuestaBool = _unitOfWork.AsignacionRegularRepository.EliminarPaisConfiguracionAsignacionRegular(id, UsuarioModificacion);
                return RespuestaBool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Activa la asignacion por asesor
        /// </summary>
        /// <returns>bool</returns>
        public bool? ActivarAsignacionAutomatica(int IdAsignacionRegular, bool activar, String UsuarioCreacion)
        {
            try
            {
                bool? RespuestaBool = new bool();
                RespuestaBool = _unitOfWork.AsignacionRegularRepository.ActivarAsignacionAutomatica(IdAsignacionRegular, activar, UsuarioCreacion);
                return RespuestaBool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool EnvioCorreoAsignacion(string mensaje)
        {
            try
            {
                List<string> correosAlerta = new List<string>();
                correosAlerta.Add("balmanzam@bsginstitute.com");
                correosAlerta.Add("mvaldiviac@bsginstitute.com");
                correosAlerta.Add("drevilla@bsginstitute.com");

                var mailServiceAlerta = new TMK_MailService();
                TMKMailDataDTO mailDataAlerta = new TMKMailDataDTO();
                mailDataAlerta.Sender = "ccrispin@bsginstitute.com";
                mailDataAlerta.Recipient = string.Join(",", correosAlerta);
                mailDataAlerta.Subject = "Asignacion de datos";
                mailDataAlerta.Message = mensaje;
                mailDataAlerta.Bcc = string.Empty;
                mailDataAlerta.AttachedFiles = null;
                mailServiceAlerta.SetData(mailDataAlerta);
                mailServiceAlerta.SendMessageTask();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EnvioCorreoAsignacion: {ex.Message}");
                return false;
            }
        }






    // Logical de esignal envio de whatsapp alumnos asignados- falta impelntar Cristian

    //public bool EnvioWhats(int idOportunidad, int idPais, int idPersonal, int IdCategoriaOrigen)
    //{
    //    try
    //    {

    //        DateTime currentTime = DateTime.Now;

    //        PlantillaAsig plantilla = new PlantillaAsig();

    //        var alumno = _unitOfWork.AsignacionRegularRepository.ObtenerAlumnoPorOportunidad(idOportunidad);
    //        var contador = _unitOfWork.AsignacionRegularRepository.ObtenerContadorBic(idOportunidad);
    //        var pgeneral = _unitOfWork.AsignacionRegularRepository.ObtenerPGeneralPorOportunidad(idOportunidad);
    //        var idplantilla = 0;

    //        List<DatosPlantillaWhatsAppDTO> objetoPlantilla = new List<DatosPlantillaWhatsAppDTO>();
    //        objetoPlantilla.Add(new DatosPlantillaWhatsAppDTO());

    //        objetoPlantilla[0].codigo = "{tAlumnos.nombre1}";
    //        objetoPlantilla[0].texto = alumno.Count > 0 ? alumno[0].Nombre : string.Empty;

    //        if ((contador.DiasSinContactoManhana > 3) || (contador.DiasSinContactoTarde > 3))
    //        {
    //            if (currentTime.Hour >= 9 && currentTime.Hour < 19)
    //            {
    //                plantilla = ReemplazarEtiquetas(1702, IdCategoriaOrigen, idPersonal, pgeneral.IdPGeneral);
    //                idplantilla = 1702;
    //            }
    //        }
    //        else if (currentTime.Hour >= 9 && currentTime.Hour < 19)
    //        {
    //            plantilla = ReemplazarEtiquetas(1701, IdCategoriaOrigen, idPersonal, pgeneral.IdPGeneral);
    //            idplantilla = 1701;
    //        }


    //        WhatsAppMensajePlantillaComDTO envioWhats = new WhatsAppMensajePlantillaComDTO();

    //        envioWhats.WaTo = alumno[0].Celular;
    //        envioWhats.WaTypeMensaje = 8;
    //        envioWhats.WaBody = plantilla.Descripcion;
    //        envioWhats.IdPlantilla = idplantilla;
    //        envioWhats.WaCaption = plantilla.Texto;
    //        envioWhats.IdPais = idPais;
    //        envioWhats.IdAlumno = alumno[0].Id;
    //        envioWhats.DatosPlantillaWhatsApp = objetoPlantilla;


    //        WhatsAppMensajeEnviadoApiComercialService whatsAppMensajesService = new WhatsAppMensajeEnviadoApiComercialService(_unitOfWork);

    //        whatsAppMensajesService.EnvioMensajePorPlantilla(envioWhats, "WhatsappAsesor", idPersonal);

    //        return true;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public PlantillaAsig ReemplazarEtiquetas(int idPlantilla, int idCategoria, int idPersonal, int idPGeneral)
    //{
    //    try
    //    {
    //        var categoria = _unitOfWork.CategoriaOrigenRepository.ObtenerPorId(idCategoria);
    //        var plantilla = _unitOfWork.PlantillaRepository.ObtenerPlantillaClaveValor(idPlantilla);
    //        var pgeneral = _unitOfWork.PGeneralRepository.ObtenerPorId(idPGeneral);
    //        var personal = _unitOfWork.PersonalRepository.ObtenerPorId(idPersonal);

    //        if (plantilla.Texto.Contains("{tPersonal.proveedor}"))
    //        {
    //            if (categoria.Descripcion == "Datos provenientes de envios Masivos de Mensajes de Texto" ||
    //                categoria.Descripcion == "Formulario Version de Prueba - Sitio Web" ||
    //                categoria.Descripcion == "Formulario a partir de aviso en Google" ||
    //                categoria.Descripcion == "Formulario de Contactenos - Sitio Web" ||
    //                categoria.Descripcion == "Formulario de Registro - Sitio Web" ||
    //                categoria.Descripcion == "Fomulario - Sitio Web" ||
    //                categoria.Descripcion == "Formulario a partir de aviso de Google" ||
    //                categoria.Descripcion == "Formulario - Sitio Web" ||
    //                categoria.Descripcion == "Formulario Contactenos - Sitio Web" ||
    //                categoria.Descripcion == "Formulario Versión Prueba - Sitio Web" ||
    //                categoria.Descripcion == "Formularios de Clientes Potenciales de Google" ||
    //                categoria.Descripcion == "Mensaje de Texto")
    //            {
    //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Sitio Web");
    //            }
    //            else if (
    //                categoria.Descripcion == "Marcador Predictivo Bases Propias" ||
    //                categoria.Descripcion == "Llamada Oficina")
    //            {
    //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Lllamada Telefonica");
    //            }
    //            else if (
    //                categoria.Descripcion == "Datos ingresados de facebook redirigidos a whatsapp - Remarketing" ||
    //                categoria.Descripcion == "Datos ingresados de facebook redirigidos a whatsapp - Publico abierto" ||
    //                categoria.Descripcion == "Respuesta por Whatsapp de correos Mailing" ||
    //                categoria.Descripcion == "Whatsapp")
    //            {
    //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Whatsapp");
    //            }
    //            else if (
    //                categoria.Descripcion == "Chat - Sitio Web")
    //            {
    //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Chat de Nuestro Sitio Web");
    //            }
    //            else if (
    //                categoria.Descripcion == "Mailing Rpta - Marketing" ||
    //                categoria.Descripcion == "Formulario a partir de correo electronico" ||
    //                categoria.Descripcion == "Mailing")
    //            {
    //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Correo Electronico");
    //            }
    //            else if (
    //                categoria.Descripcion == "Datos de Inbox Comentarios - Instangram" ||
    //                categoria.Descripcion == "Comentarios de Instagram" ||
    //                categoria.Descripcion == "Mensajes de Facebook" ||
    //                categoria.Descripcion == "Comentarios Chat")
    //            {
    //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Inbox o Comentarios");
    //            }
    //            else if (
    //                categoria.Descripcion == "Formulario a partir de aviso en Facebook" ||
    //                categoria.Descripcion == "Comentarios de Facebook")
    //            {
    //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Facebook");
    //            }
    //            else if (
    //                categoria.Descripcion == "Twitter")
    //            {
    //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Twitter");
    //            }
    //            else if (
    //                categoria.Descripcion == "Formulario a partir de aviso en Instagram")
    //            {
    //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Instagram");
    //            }
    //        }
    //        if (plantilla.Texto.Contains("{tPegeneral.Nombre}"))
    //        {
    //            plantilla.Texto = plantilla.Texto.Replace("{tPegeneral.Nombre}", pgeneral.Nombre);
    //        }
    //        if (plantilla.Texto.Contains("{tPersonal.NombreCompleto}"))
    //        {
    //            plantilla.Texto = plantilla.Texto.Replace("{tPersonal.NombreCompleto}", $"{personal.Nombres} {personal.ApellidoPaterno} {personal.ApellidoMaterno}");
    //        }


    //        PlantillaAsig plantillaDatos = new PlantillaAsig();

    //        plantillaDatos.Nombre = plantilla.Nombre;
    //        plantillaDatos.Texto = plantilla.Texto;
    //        plantillaDatos.Descripcion = plantilla.Descripcion;

    //        return plantillaDatos;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


        public bool EnvioCorreoFacebook(string mensaje)
        {
            try
            {
                List<string> correosAlerta = new List<string>();

                correosAlerta.Add("mramirez@bsginstitute.com");
                ;

                var mailServiceAlerta = new TMK_MailService();
                TMKMailDataDTO mailDataAlerta = new TMKMailDataDTO();
                mailDataAlerta.Sender = "mramirez@bsginstitute.com";
                mailDataAlerta.Recipient = string.Join(",", correosAlerta);
                mailDataAlerta.Subject = "Conexión Hook Complementos";
                mailDataAlerta.Message = mensaje;
                mailDataAlerta.Bcc = string.Empty;
                mailDataAlerta.AttachedFiles = null;
                mailServiceAlerta.SetData(mailDataAlerta);
                mailServiceAlerta.SendMessageTask();

                return true;
}
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EnvioCorreoAsignacion: {ex.Message}");
                return false;
            }
        }

    }


}

