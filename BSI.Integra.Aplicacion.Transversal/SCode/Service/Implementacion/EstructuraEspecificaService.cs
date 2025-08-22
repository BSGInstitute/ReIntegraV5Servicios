using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: EstructuraEspecificaService
    /// Autor: Gilmer Quispe.
    /// Fecha: 23/08/2022
    /// <summary>
    /// Gestión general de programas y cronogramas 
    /// </summary>
    public class EstructuraEspecificaService : IEstructuraEspecificaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public EstructuraEspecificaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEstructuraEspecifica, EstructuraEspecifica>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/08/2022
        /// Version: 1.0
        /// <summary>
        /// Congela el Cronograma de alumno por IdCronograma y Usuario.
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="usuario">Usuario</param>
        /// <returns> bool </returns>
        public bool CongelarEstructuraAlumno(object datos, string usuario)
        {
            try
            {
                return _unitOfWork.EstructuraEspecificaRepository.CongelarEstructuraAlumno(datos, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 23/08/2022
        /// Version: 1.0
        /// <summary>
        /// Congela el Cronograma de alumno por IdCronograma y Usuario.
        /// </summary>
        /// <param name="IdCronograma">Id del cronograma</param>
        /// <param name="Usuario">Usuario</param>
        /// <returns> List<DatosEstructuraCurricularDTO> </returns>
        public DatosEstructuraCurricularDTO CongelarEstructuraEspecifica(int IdCronograma, string Usuario)
        {
            var listaDatosEstructura = new DatosEstructuraCurricularDTO();
            listaDatosEstructura.DatosEstructura = new List<DatosEstructuraEspecificaDTO>();

            var servicioMatriculaCabecera = new MatriculaCabeceraService(_unitOfWork);
            var servicioEstructuraEspecifica = new EstructuraEspecificaService(_unitOfWork);

            listaDatosEstructura.Usuario = Usuario;
            //Con el IdCronograma se consigue el IdMatriculaCabecera
            var datosMatriculaCabecera = servicioMatriculaCabecera.ObtenerInformacionMatriculaCabeceraPorIdCronograma(IdCronograma);
            var idProgramaEspecifico = servicioMatriculaCabecera.ObtenerAlumnoProgramaEspecifico(datosMatriculaCabecera.CodigoMatricula);
            //Verificar si es padre o hijo
            //var listaPrograma = _PespecificoPadrePespecificoHijoRepositorio.ObtenerPespecificosHijos(idProgramaEspecifico);
            var listaPrograma = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerCursosCongelamientoEstrucuraCurricular(datosMatriculaCabecera.Id);
            if (listaPrograma != null)
            {
                if (listaPrograma.Count() > 0)//Significa que tiene hijos
                {
                    var IdProgramaGeneralPadre = servicioMatriculaCabecera.ObtenerProgramaGeneral(idProgramaEspecifico);
                    foreach (var lp in listaPrograma)
                    {
                        //Conseguimos el programa GeneralHijo
                        //var idProgramaGeneral = _repMatriculaCabecera.ObtenerProgramaGeneral(lp.PEspecificoHijoId);
                        DatosEstructuraEspecificaDTO estructura = new DatosEstructuraEspecificaDTO();
                        estructura.IdMatriculaCabecera = datosMatriculaCabecera.Id;
                        estructura.IdPGeneralPadre = IdProgramaGeneralPadre;
                        estructura.IdPGeneralHijo = lp.Id;
                        estructura.IdPEspecificoHijo = lp.IdPEspecifico.Value;
                        //listaDatosEstructura.Insert(estructuraBO);
                        //var IdEstructuraEspecifica = this.Id;
                        estructura.Encuesta = new List<DatosEstructuraEspecificaEncuestaDTO>();
                        estructura.Tarea = new List<DatosEstructuraEspecificaTareaDTO>();
                        estructura.Capitulo = new List<DatosEstructuraEspecificaCapituloDTO>();

                        List<DatosEstructuraEspecificaCapituloDTO> listaCapitulos = new List<DatosEstructuraEspecificaCapituloDTO>();

                        var respuesta = this.ConseguirEstructuraPorPrograma(lp.Id);
                        if (respuesta != null)
                        {

                            foreach (var r in respuesta)
                            {

                                if (r.Nombre.ToUpper().Contains("ENCUESTA"))
                                {

                                    DatosEstructuraEspecificaEncuestaDTO encuestaDTO = new DatosEstructuraEspecificaEncuestaDTO();
                                    encuestaDTO.IdEncuesta = r.Id;
                                    encuestaDTO.NombreEncuesta = r.Capitulo;
                                    encuestaDTO.OrdenCapitulo = r.OrdenFila;
                                    encuestaDTO.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                    estructura.Encuesta.Add(encuestaDTO);

                                }
                                else
                                {
                                    if (r.Nombre.ToUpper().Contains("TAREA"))
                                    {
                                        DatosEstructuraEspecificaTareaDTO tarea = new DatosEstructuraEspecificaTareaDTO();
                                        // tarea.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                        tarea.NombreTarea = r.Capitulo;
                                        tarea.OrdenCapitulo = r.OrdenFila;
                                        tarea.IdTarea = r.Id;
                                        tarea.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                        estructura.Tarea.Add(tarea);

                                    }
                                    else
                                    {
                                        DatosEstructuraEspecificaCapituloDTO capituloDTO = new DatosEstructuraEspecificaCapituloDTO();
                                        //capituloBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                        capituloDTO.Numero = r.OrdenFila;
                                        capituloDTO.Capitulo = r.Capitulo;
                                        if (r.ListaSesiones.Count() > 0)
                                        {
                                            List<DatosEstructuraEspecificaSesionDTO> Sesion = new List<DatosEstructuraEspecificaSesionDTO>();

                                            var sesiones = r.ListaSesiones.Where(x => x.Sesion != null).Select(x => x.Sesion).Distinct().ToList();
                                            foreach (var se in sesiones)
                                            {
                                                var sesion = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).FirstOrDefault();
                                                DatosEstructuraEspecificaSesionDTO sesionDTO = new DatosEstructuraEspecificaSesionDTO();
                                                //sesionBO.IdEstructuraEspecificaCapitulo = IdCapitulo;
                                                sesionDTO.Numero = sesion.OrdenSeccion;
                                                sesionDTO.Sesion = sesion.Sesion;
                                                sesionDTO.OrdenSesion = sesion.OrdenFila;
                                                var subSesiones = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).ToList();
                                                List<DatosEstructuraEspecificaSubSesionDTO> listaSubSesionBO = new List<DatosEstructuraEspecificaSubSesionDTO>();
                                                foreach (var sub in subSesiones)
                                                {
                                                    if (sub.SubSesion != null)
                                                    {
                                                        if (sub.SubSesion.Trim() != "")
                                                        {
                                                            DatosEstructuraEspecificaSubSesionDTO subSesionBO = new DatosEstructuraEspecificaSubSesionDTO();
                                                            //subSesionBO.IdEstructuraEspecificaSesion = IdSesion;
                                                            subSesionBO.Numero = sub.OrdenFila;
                                                            subSesionBO.SubSesion = sub.SubSesion;
                                                            //subSesisubSesionBOonBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                                            listaSubSesionBO.Add(subSesionBO);
                                                        }
                                                    }
                                                }
                                                sesionDTO.SubSesion = listaSubSesionBO;
                                                Sesion.Add(sesionDTO);
                                            }
                                            capituloDTO.Sesion = Sesion;
                                        }
                                        listaCapitulos.Add(capituloDTO);

                                    }
                                }


                            }
                            estructura.Capitulo = listaCapitulos;
                            listaDatosEstructura.DatosEstructura.Add(estructura);
                        }
                    }
                }
                else //No tiene hijos
                {
                    //Conseguimos el programa General
                    var idProgramaGeneral = servicioMatriculaCabecera.ObtenerProgramaGeneral(idProgramaEspecifico);
                    DatosEstructuraEspecificaDTO estructura = new DatosEstructuraEspecificaDTO();
                    estructura.IdMatriculaCabecera = datosMatriculaCabecera.Id;
                    estructura.IdPGeneralPadre = idProgramaGeneral;
                    estructura.IdPGeneralHijo = idProgramaGeneral;

                    var respuesta = this.ConseguirEstructuraPorPrograma(idProgramaGeneral);
                    estructura.Encuesta = new List<DatosEstructuraEspecificaEncuestaDTO>();
                    estructura.Tarea = new List<DatosEstructuraEspecificaTareaDTO>();
                    estructura.Capitulo = new List<DatosEstructuraEspecificaCapituloDTO>();

                    List<DatosEstructuraEspecificaCapituloDTO> listaCapitulos = new List<DatosEstructuraEspecificaCapituloDTO>();
                    if (respuesta != null)
                    {
                        foreach (var r in respuesta)
                        {

                            if (r.Nombre.ToUpper().Contains("ENCUESTA"))
                            {

                                DatosEstructuraEspecificaEncuestaDTO encuestaDTO = new DatosEstructuraEspecificaEncuestaDTO();
                                encuestaDTO.IdEncuesta = r.Id;
                                encuestaDTO.NombreEncuesta = r.Nombre;
                                encuestaDTO.OrdenCapitulo = r.OrdenFila;
                                encuestaDTO.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                estructura.Encuesta.Add(encuestaDTO);

                            }
                            else
                            {
                                if (r.Nombre.ToUpper().Contains("TAREA"))
                                {
                                    DatosEstructuraEspecificaTareaDTO tarea = new DatosEstructuraEspecificaTareaDTO();
                                    // tarea.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                    tarea.NombreTarea = r.Nombre;
                                    tarea.OrdenCapitulo = r.OrdenFila;
                                    tarea.IdTarea = r.Id;
                                    tarea.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                    estructura.Tarea.Add(tarea);

                                }
                                else
                                {
                                    DatosEstructuraEspecificaCapituloDTO capituloDTO = new DatosEstructuraEspecificaCapituloDTO();
                                    //capituloBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                    capituloDTO.Numero = r.OrdenFila;
                                    capituloDTO.Capitulo = r.Capitulo;
                                    if (r.ListaSesiones.Count() > 0)
                                    {
                                        List<DatosEstructuraEspecificaSesionDTO> Sesion = new List<DatosEstructuraEspecificaSesionDTO>();

                                        var sesiones = r.ListaSesiones.Where(x => x.Sesion != null).Select(x => x.Sesion).Distinct().ToList();
                                        foreach (var se in sesiones)
                                        {
                                            var sesion = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).FirstOrDefault();
                                            DatosEstructuraEspecificaSesionDTO sesionDTO = new DatosEstructuraEspecificaSesionDTO();
                                            //sesionBO.IdEstructuraEspecificaCapitulo = IdCapitulo;
                                            sesionDTO.Numero = sesion.OrdenSeccion;
                                            sesionDTO.Sesion = sesion.Sesion;
                                            sesionDTO.OrdenSesion = sesion.OrdenFila;
                                            var subSesiones = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).ToList();
                                            List<DatosEstructuraEspecificaSubSesionDTO> listaSubSesionBO = new List<DatosEstructuraEspecificaSubSesionDTO>();
                                            foreach (var sub in subSesiones)
                                            {
                                                if (sub.SubSesion != null)
                                                {
                                                    if (sub.SubSesion.Trim() != "")
                                                    {
                                                        DatosEstructuraEspecificaSubSesionDTO subSesionBO = new DatosEstructuraEspecificaSubSesionDTO();
                                                        //subSesionBO.IdEstructuraEspecificaSesion = IdSesion;
                                                        subSesionBO.Numero = sesion.OrdenFila;
                                                        subSesionBO.SubSesion = sub.SubSesion;
                                                        //subSesisubSesionBOonBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                                        listaSubSesionBO.Add(subSesionBO);
                                                    }

                                                }
                                            }
                                            sesionDTO.SubSesion = listaSubSesionBO;
                                            Sesion.Add(sesionDTO);
                                        }
                                        capituloDTO.Sesion = Sesion;
                                    }
                                    listaCapitulos.Add(capituloDTO);

                                }
                            }


                        }
                        estructura.Capitulo = listaCapitulos;
                        listaDatosEstructura.DatosEstructura.Add(estructura);
                    }
                }

            }
            servicioEstructuraEspecifica.CongelarEstructuraAlumno(listaDatosEstructura.DatosEstructura, listaDatosEstructura.Usuario);
            return listaDatosEstructura;


        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/09/2022
        /// Version: 1.0
        /// <summary>
        /// Consigue la estructura de un programa por si Id
        /// </summary>
        /// <param name="idProgramaGeneral">Id del programa general</param>
        /// <returns> List<CapitulosSesionesProgramaDTO> </returns>
        public List<CapitulosSesionesProgramaDTO> ConseguirEstructuraPorPrograma(int idProgramaGeneral)
        { 
            List<EstructuraCapituloProgramaAlternoDTO> lista = new List<EstructuraCapituloProgramaAlternoDTO>();

            var respuesta = _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerPreConfigurarVideoPrograma(idProgramaGeneral);
            if (respuesta != null)
            {
                var listadoEstructura = (from x in respuesta
                                         group x by x.NumeroFila into newGroup
                                         select newGroup).ToList();
                foreach (var item in listadoEstructura)
                {
                    EstructuraCapituloProgramaAlternoDTO objeto = new EstructuraCapituloProgramaAlternoDTO();
                    objeto.OrdenFila = item.Key;
                    foreach (var itemRegistros in item)
                    {
                        switch (itemRegistros.NombreTitulo)
                        {
                            case "Capitulo":
                                objeto.Nombre = itemRegistros.Nombre;
                                objeto.Capitulo = itemRegistros.Contenido;
                                objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                objeto.IdPgeneral = itemRegistros.IdPgeneral;
                                break;
                            case "Sesion":
                                objeto.Sesion = itemRegistros.Contenido;
                                objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                objeto.IdDocumentoSeccionPw = itemRegistros.Id;
                                break;
                            case "SubSeccion":
                                objeto.SubSesion = itemRegistros.Contenido;
                                if (!string.IsNullOrEmpty(objeto.SubSesion))
                                {
                                    objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                    objeto.IdDocumentoSeccionPw = itemRegistros.Id;
                                }
                                break;
                            default:
                                objeto.OrdenCapitulo = !Regex.IsMatch(itemRegistros.Contenido, @"^[0-9]+$") ? 1 : Convert.ToInt32(itemRegistros.Contenido);
                                objeto.TotalSegundos = itemRegistros.TotalSegundos;
                                break;
                        }
                    }
                    lista.Add(objeto);
                }

                var rpta = lista.OrderBy(x => x.OrdenFila).ToList();

                var listas = rpta.GroupBy(x => new { x.IdPgeneral, x.Nombre, x.Capitulo, x.OrdenCapitulo });

                List<CapitulosSesionesProgramaDTO> listaRegistro = new List<CapitulosSesionesProgramaDTO>();

                foreach (var capitulo in listas)
                {
                    CapitulosSesionesProgramaDTO registro = new CapitulosSesionesProgramaDTO();
                    registro.IdPgeneral = capitulo.Key.IdPgeneral;
                    registro.Nombre = capitulo.Key.Nombre;
                    registro.Capitulo = capitulo.Key.Capitulo;
                    registro.OrdenFila = capitulo.Key.OrdenCapitulo;

                    registro.ListaSesiones = new List<EstructuraCapituloProgramaAlternoDTO>();

                    foreach (var sesiones in capitulo)
                    {
                        EstructuraCapituloProgramaAlternoDTO sesion = new EstructuraCapituloProgramaAlternoDTO();
                        sesion.Sesion = sesiones.Sesion;
                        sesion.OrdenFila = sesiones.OrdenFila;
                        sesion.OrdenCapitulo = sesiones.OrdenCapitulo;
                        sesion.OrdenSeccion = sesiones.OrdenSeccion;
                        sesion.TotalSegundos = sesiones.TotalSegundos;
                        sesion.SubSesion = sesiones.SubSesion;

                        registro.ListaSesiones.Add(sesion);
                    }
                    listaRegistro.Add(registro);


                }

                var respuestaPreFinal = listaRegistro.OrderBy(x => x.Capitulo).ToList();

                foreach (var item in respuestaPreFinal)
                {
                    var rptaEvaluacion = _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerPreConfigurarVideoProgramaEvaluaciones(item.IdPgeneral, item.OrdenFila);
                    var rptaExamenes = _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerPreConfigurarVideoProgramaEncuestas(item.IdPgeneral, item.OrdenFila);

                    if (rptaEvaluacion != null && rptaEvaluacion.Count > 0)
                    {
                        foreach (var itemEvaluacion in rptaEvaluacion)
                        {
                            CapitulosSesionesProgramaDTO registroEvaluacion = new CapitulosSesionesProgramaDTO();
                            registroEvaluacion.Id = itemEvaluacion.Id;
                            registroEvaluacion.IdDocumentoSeccionPw = itemEvaluacion.IdSeccionTipoDetalle_PW;
                            registroEvaluacion.IdPgeneral = itemEvaluacion.IdPgeneral;
                            registroEvaluacion.Nombre = itemEvaluacion.Nombre;
                            registroEvaluacion.Capitulo = itemEvaluacion.Contenido;
                            registroEvaluacion.OrdenFila = itemEvaluacion.NumeroFila;

                            registroEvaluacion.ListaSesiones = new List<EstructuraCapituloProgramaAlternoDTO>();
                            listaRegistro.Add(registroEvaluacion);
                        }

                    }

                    if (rptaExamenes != null && rptaExamenes.Count > 0)
                    {
                        foreach (var itemEvaluacion in rptaExamenes)
                        {
                            CapitulosSesionesProgramaDTO registroEvaluacion = new CapitulosSesionesProgramaDTO();
                            registroEvaluacion.Id = itemEvaluacion.Id;
                            registroEvaluacion.IdDocumentoSeccionPw = itemEvaluacion.IdSeccionTipoDetalle_PW;
                            registroEvaluacion.IdPgeneral = itemEvaluacion.IdPgeneral;
                            registroEvaluacion.Nombre = itemEvaluacion.Nombre;
                            registroEvaluacion.Capitulo = itemEvaluacion.Contenido;
                            registroEvaluacion.OrdenFila = itemEvaluacion.NumeroFila;

                            registroEvaluacion.ListaSesiones = new List<EstructuraCapituloProgramaAlternoDTO>();
                            listaRegistro.Add(registroEvaluacion);
                        }

                    }
                }

                var listaFinal = listaRegistro.OrderBy(x => x.Capitulo).ToList();
                return listaFinal;
            }
            else
            {
                return null;
            }
        }
    }
}
