using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Office.CustomUI;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;
using System.Transactions;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    /// Service: ReportePostulanteService
    /// Autor: Flavio R.M.F.
    /// Fecha: 04/06/2024
    /// <summary>
    /// Servicio de Reporte de Postulantes
    /// </summary>
    public class EvaluacionPostulanteService : IEvaluacionPostulanteService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        private int _redondeoGeneral = 0;
        public EvaluacionPostulanteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
            });
            _mapper = new Mapper(config);
        }

        public async Task<EvaluacionPostulanteComboDTO> ObtenerCombosModulo()
        {
            try
            {
                var task_estadoEtapaProcesoSeleccion = _unitOfWork.EstadoEtapaProcesoSeleccionRepository.ObtenerComboAsync();
                var task_procesoSeleccion = _unitOfWork.ProcesoSeleccionRepository.ObtenerCodigoNombreAsync();
                var task_procesoSeleccionEtapa = _unitOfWork.ProcesoSeleccionEtapaRepository.ObtenerComboAsync();
                var task_grupoComparacion = _unitOfWork.GrupoComparacionProcesoSeleccionRepository.ObtenerComboAsync();

                var combosModulo = new EvaluacionPostulanteComboDTO();
                combosModulo.VersionesCentil = _unitOfWork.CentilRepository.ObtenerVersionesCentil();
                combosModulo.EstadoEtapas = await task_estadoEtapaProcesoSeleccion;
                combosModulo.ProcesosDeSeleccion = await task_procesoSeleccion;
                combosModulo.ProcesoSeleccionEtapas = await task_procesoSeleccionEtapa;
                combosModulo.GruposComparacion = await task_grupoComparacion;
                return combosModulo;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ResultadoReporteEvaluacionPostulante GenerarReporte(EvaluacionPostulanteFiltroReporteDTO filtroReporte)
        {
            try
            {
                var postulanteProceso = _unitOfWork.PostulanteRepository.ObtenerPostulantesUltimoProcesoSeleccion(filtroReporte);
                if (postulanteProceso == null || postulanteProceso.Count == 0)
                    throw new ConflictException("No se encontraron postulantes en el Proceso de Seleccion");

                if (filtroReporte.FiltroPorPostulante == false)
                {
                    if (filtroReporte.IdsEstadoEtapa.Count() == 0 && filtroReporte.IdsProcesoEtapa.Count() == 0)
                    {
                        if (filtroReporte.IdGrupoComparacion != null && filtroReporte.IdGrupoComparacion > 0)
                        {
                            if (filtroReporte.IdProcesoSeleccion != null)
                            {
                                var idsPostulantes = _unitOfWork.ExamenAsignadoRepository.ObtenerIdsPostulantesPorIdProcesoSeleccion(filtroReporte.IdProcesoSeleccion.Value);
                                filtroReporte.IdProcesoSeleccion = null;
                                filtroReporte.IdsPostulantes = idsPostulantes;
                            }
                        }
                    }
                    else
                    {
                        var resultado = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerIdsPostulanteEtapaProcesoSeleccionActual(filtroReporte);
                        if (resultado.Count() == 0)
                        {
                            throw new ConflictException("No se encontraron postulantes");
                        }
                        filtroReporte.IdsPostulantes = resultado;
                        postulanteProceso = postulanteProceso.Where(x => filtroReporte.IdsPostulantes.Contains(x.IdPostulante)).ToList();
                    }

                    if (filtroReporte.FechaInicio == null)
                    {
                        filtroReporte.FechaInicio = new DateTime(1900, 12, 31);
                    }
                    if (filtroReporte.FechaFin == null)
                    {
                        filtroReporte.FechaFin = DateTime.Now;
                    }
                }
                else
                {
                    filtroReporte.FechaFin = DateTime.Now;
                    filtroReporte.FechaInicio = new DateTime(1900, 12, 31);
                }
                filtroReporte.IdsPostulantes = postulanteProceso.Select(x => x.IdPostulante).ToList();

                List<ValorIntDTO> matriculaPostulantes = new List<ValorIntDTO>();
                foreach (var idPostulante in filtroReporte.IdsPostulantes)
                {
                    ValorIntDTO matriculaPostulante = new ValorIntDTO();
                    matriculaPostulante.Id = idPostulante;
                    matriculaPostulante.Valor = _unitOfWork.PostulanteRepository.ObtenerIdMatriculaPorPostulante(idPostulante);
                    matriculaPostulantes.Add(matriculaPostulante);
                }

                if (filtroReporte.IdGrupoComparacion != null && filtroReporte.IdGrupoComparacion != 0)
                {
                    filtroReporte.idsPostulanteGrupoComparacion = _unitOfWork.PostulanteComparacionRepository.ObtenerIdsPostulantesPorIdGrupoComparacion(filtroReporte.IdGrupoComparacion.Value);
                }

                var reporte = ObtenerReporteExamenesNuevaVersion(filtroReporte);

                var listaComponenteAcceso = _unitOfWork.ExamenRepository.GetBy(x => x.IdCentroCosto != null && x.CantidadDiasAcceso != null).Select(x => x.Nombre).ToList();
                var listaAgregarConfiguracion = reporte.Where(x => listaComponenteAcceso.Contains(x.Examen)).ToList();
                foreach (var agregar in listaAgregarConfiguracion)
                {
                    agregar.ConfiguracionComponenteCurso = true;
                    agregar.IdExamenAccesoTemporal = agregar.IdExamen;
                }

                //Obtenidas las calificaciones de los postulantes se ordena para que muestre en orden las evaluacion y componentes y se agrupa por cada item que exista en la lista final
                var datosAgrupado = (from p in reporte
                                     orderby p.OrdenReal
                                     group p by new { p.OrdenReal } into grupo
                                     select new DatoEvaluacionAgrupadoDTO
                                     {
                                         OrdenReal = grupo.Key.OrdenReal,
                                         Proceso = grupo.ToList()
                                     }).ToList();

                var postulantes = (from p in reporte
                                   group p by new { p.IdPostulante, p.Postulante, p.Edad } into grupo
                                   select new PostulanteEvaluacionAgrupadoDTO
                                   {
                                       IdPostulante = grupo.Key.IdPostulante,
                                       Postulante = grupo.Key.Postulante,
                                       Edad = grupo.Key.Edad
                                   }).ToList();

                var idPostulantes = postulantes.Select(x => x.IdPostulante).ToList();

                var clasificacionNEO = new List<PostulanteClasificacionNeoDTO>();
                if (idPostulantes != null && idPostulantes.Count > 0)
                {
                    clasificacionNEO = _unitOfWork.PostulanteRepository.ObtenerPostulantesUltimoProcesoSeleccion(idPostulantes);
                }
                List<int> listaPostulanteComparacion = new List<int>();

                //se elimina de los postulantes obtenidos a los potulantes que pertenezcan al grupo de comparacion en caso se haya seleccionado en el filtro
                if (listaPostulanteComparacion.Count > 0)
                {
                    reporte = reporte.Where(x => !listaPostulanteComparacion.Contains(x.IdPostulante)).ToList();
                }

                // de la lita de postulantes que hayan pasado el filtro se agrupan por proceso de seleccion los postulantes.
                var listaProcesoSeleccionAgrupado = (from p in postulanteProceso
                                                     group p by new { p.IdProcesoSeleccion, p.ProcesoSeleccion } into grupo
                                                     select new { grupo.Key.IdProcesoSeleccion, grupo.Key.ProcesoSeleccion }).ToList();

                List<ReportePruebaDetalleDTO> listaEtapas = new List<ReportePruebaDetalleDTO>();
                List<ReportePruebaDTO> listaEtapasFinal = new List<ReportePruebaDTO>();

                //Se busca todas las etapas segun el proceso de seleccion y se coloca por defecto el estado SIN RENDIR ID 9
                foreach (var item in listaProcesoSeleccionAgrupado)
                {
                    var etapasProcesoSeleccion = _unitOfWork.ProcesoSeleccionEtapaRepository.ObtenerEtapaPorIdProcesoSeleccion(item.IdProcesoSeleccion);

                    listaEtapas = listaEtapas.Concat(etapasProcesoSeleccion.Select(x => new ReportePruebaDetalleDTO
                    {
                        IdProcesoSeleccion = item.IdProcesoSeleccion,
                        ProcesoSeleccion = item.ProcesoSeleccion,
                        IdEtapa = x.Id,
                        Etapa = x.Nombre,
                        EstadoEtapa = 0,
                        IdEstadoEtapaProceso = 9,
                        NroOrden = x.NroOrden,
                        EtapaContactado = false
                    }
                    ).ToList()).ToList();
                }

                List<EtapaCalificadaPostulanteProcesoSeleccionDTO> listaEtapasOptimizacion = new List<EtapaCalificadaPostulanteProcesoSeleccionDTO>();
                if (filtroReporte.IdProcesoSeleccion != null)
                {
                    if (postulanteProceso.Count() == 0)
                    {
                        throw new ConflictException("No se encontraron postulantes");
                    }
                    else
                    {
                        List<int> idsProcesoSeleccion = new List<int>
                        {
                            filtroReporte.IdProcesoSeleccion!.Value
                        };
                        listaEtapasOptimizacion = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerPorIdsPostulanteIdsProcesoSeleccion(postulanteProceso.Select(x => x.IdPostulante).ToList(), idsProcesoSeleccion);
                    }
                }
                else
                {
                    List<int> idsProcesoSeleccion = reporte.Select(x => x.IdProcesoSeleccion).Distinct().ToList();
                    if (idsProcesoSeleccion.Count() == 0)
                    {
                        idsProcesoSeleccion = listaProcesoSeleccionAgrupado.Select(x => x.IdProcesoSeleccion).Distinct().ToList();
                    }
                    listaEtapasOptimizacion = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerPorIdsPostulanteIdsProcesoSeleccion(filtroReporte.IdsPostulantes, idsProcesoSeleccion);
                }


                List<ReportePruebaDetalleDTO> etapasList;
                ReportePruebaDetalleDTO itemEtapa;
                //Se recorre la lista de los postulantes que cumplen el filtro y cada postulante se le asigna sus etapas correspondientes segun el proceso de seleccion que se encuentre
                foreach (var item in postulanteProceso)
                {
                    etapasList = new List<ReportePruebaDetalleDTO>();
                    etapasList = listaEtapas.Where(x => x.IdProcesoSeleccion == item.IdProcesoSeleccion).OrderBy(x => x.NroOrden).ToList();
                    ReportePruebaDTO obj = new ReportePruebaDTO();
                    obj.IdPostulante = item.IdPostulante;
                    obj.Postulante = item.Postulante;
                    obj.Etapas = new List<ReportePruebaDetalleDTO>();

                    foreach (var item2 in etapasList)
                    {
                        itemEtapa = new ReportePruebaDetalleDTO();
                        var etapaCalificada = listaEtapasOptimizacion.Where(x => x.IdPostulante == item.IdPostulante && x.IdProcesoSeleccionEtapa == item2.IdEtapa).FirstOrDefault();

                        itemEtapa.IdEtapa = item2.IdEtapa;
                        itemEtapa.Etapa = item2.Etapa;
                        itemEtapa.IdProcesoSeleccion = item2.IdProcesoSeleccion;
                        itemEtapa.ProcesoSeleccion = item2.ProcesoSeleccion;
                        itemEtapa.EstadoEtapa = item2.EstadoEtapa;
                        itemEtapa.IdEstadoEtapaProceso = item2.IdEstadoEtapaProceso;
                        itemEtapa.NroOrden = item2.NroOrden;
                        itemEtapa.EtapaContactado = item2.EtapaContactado;
                        itemEtapa.EsCalificadoPorPostulante = item2.EsCalificadoPorPostulante;

                        if (etapaCalificada != null) // Si tiene una calificacion en la tabla gp.T_EtapaPRocesoSeleccionCalificado reemplaza lo datos, en caso no exista coloca los datos por defecto
                        {
                            itemEtapa.EstadoEtapa = etapaCalificada.EsEtapaAprobada == true ? 1 : 0;
                            itemEtapa.IdEstadoEtapaProceso = etapaCalificada.IdEstadoEtapaProcesoSeleccion;
                            itemEtapa.EtapaContactado = etapaCalificada.EsContactado == true ? true : false;
                            itemEtapa.EsCalificadoPorPostulante = etapaCalificada.EsCalificadoPorPostulante == true ? true : false;
                        }
                        obj.Etapas.Add(itemEtapa);
                    }
                    listaEtapasFinal.Add(obj);
                }
                if (listaEtapasFinal.Count > 0)
                {
                    listaEtapasFinal = listaEtapasFinal.OrderBy(x => x.Postulante).ToList();
                }
                var resultadoFinal = new ResultadoReporteEvaluacionPostulante()
                {
                    DatosEvaluacionAgrupado = datosAgrupado,
                    Postulantes = postulantes,
                    EtapaAprobada = listaEtapasFinal,
                    CantidadEtapaAprobada = listaEtapasFinal.Count(),
                    ClasificacionNEO = clasificacionNEO,
                    MatriculaPostulantes = matriculaPostulantes
                };
                return resultadoFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edgar S .
        /// Fecha: 12/05/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene puntaje de examen para su calificación segpun configuración de evaluaciones, grupos y componentes
        /// </summary>
        /// <param name="filtro"> Valores de búsqueda para recolección de información </param>
        /// <returns></returns>
        public List<ProcesoSelecionExamenesCompletosDTO> ObtenerReporteExamenesNuevaVersion(EvaluacionPostulanteFiltroReporteDTO filtro)
        {
            try
            {

                List<DatosExamenPostulanteDTO> reporte = _unitOfWork.ExamenRepository.ObtenerReporteExamenPostulante(filtro);
                if (reporte == null || reporte.Count == 0)
                {
                    return new List<ProcesoSelecionExamenesCompletosDTO>();
                }
                //Obtenemos la configuración de fórmulas de calificación de componentes, grupos y evaluaciones
                List<ConfiguracionExamenTestDTO> configuracionExamen = _unitOfWork.ExamenRepository.ObtenerConfiguracionPuntaje();

                //Asignamos el puntaje real al puntaje de cada componente unitario                
                foreach (var componente in reporte)
                {
                    //Cambiamos el puntaje de Curso a decimal redondeado
                    if (componente.PuntajeCurso != null) componente.PuntajeCurso = FuncionRedondeo(componente.PuntajeCurso.GetValueOrDefault(), _redondeoGeneral);
                    //Obtenemos la cantidad de preguntas para realizar el promedio en caso fuera necesario
                    var cantidadPreguntaComponenteConfigurado = configuracionExamen.Where(x => x.IdExamen == componente.IdExamen).FirstOrDefault();
                    var formulaComponente = componente.IdFormulaComponente.GetValueOrDefault();
                    if (formulaComponente > 0 && cantidadPreguntaComponenteConfigurado != null && cantidadPreguntaComponenteConfigurado.CantidadPreguntas > 0)
                    {
                        // 3 = PROMEDIAR(Puntaje preguntas)
                        if (formulaComponente == 3) componente.Puntaje = (componente.Puntaje * componente.FactorComponente) / cantidadPreguntaComponenteConfigurado.CantidadPreguntas;
                        else componente.Puntaje = componente.Puntaje * componente.FactorComponente;
                    }
                    else componente.Puntaje = componente.Puntaje * componente.FactorComponente;
                }

                //Se agrupan las notas o calificaciones por postulante para que de esta manera podamos calcular su calificacion.
                var listaPostulante = reporte.GroupBy(u => new { u.Postulante, u.NombreProceso, u.IdPostulante })
                    .Select(group =>
                    new DatosNotaPorPostulanteDTO
                    {
                        IdPostulante = group.Key.IdPostulante,
                        Postulante = group.Key.Postulante,
                        NombreProceso = group.Key.NombreProceso,
                        NotasPostulante = group.Select(x => new NotaPostulanteDTO
                        {
                            IdProceso = x.IdProceso,
                            ProcesoSeleccion = x.NombreProceso,
                            IdSexo = x.IdSexo,
                            IdEvaluacion = x.IdEvaluacion,
                            IdGrupo = x.IdGrupo,
                            IdExamen = x.IdExamen,
                            VersionCentil = x.VersionCentil,
                            NombreEvaluacion = x.NombreEvaluacion,
                            NombreExamen = x.NombreExamen,
                            NombreGrupo = x.NombreGrupo,
                            Puntaje = x.Puntaje,
                            IdCategoria = x.IdCategoria,
                            IdEtapa = x.IdEtapa,
                            NombreCategoria = x.NombreCategoria,
                            NombreEtapa = x.NombreEtapa,
                            FactorComponente = x.FactorComponente,
                            FactorEvaluacion = x.FactorEvaluacion,
                            FactorGrupo = x.FactorGrupo,
                            IdFormulaComponente = x.IdFormulaComponente,
                            IdFormulaEvaluacion = x.IdFormulaEvaluacion,
                            IdFormulaGrupo = x.IdFormulaGrupo,
                            EstadoAcceso = x.EstadoAcceso,
                            CantidadConfigurado = x.CantidadConfigurado,
                            CantidadResuelto = x.CantidadResuelto,
                            PuntajeCurso = x.PuntajeCurso
                        }).ToList()
                    }).ToList();

                List<ProcesoSelecionExamenesCompletosDTO> listaNotasProcesoSeleccion = new List<ProcesoSelecionExamenesCompletosDTO>();
                Decimal count = 0.00M;

                foreach (var postulante in listaPostulante)
                {
                    count = 0.00M;
                    //Se obtiene en una lista todas las evaluaciones de un postulante
                    List<int?> ListaEvaluacion = postulante.NotasPostulante.Where(x => x.IdEvaluacion != null).Select(x => x.IdEvaluacion).Distinct().ToList();

                    //Se agrupan las evaluaciones segun sus notas obtenidas por componentes, recordar que una evaluacion puede tener mas de un componente
                    var grupoEvaluacion = postulante.NotasPostulante.GroupBy(u => new { u.IdEvaluacion, u.NombreEvaluacion })
                        .Select(group => new EvaluacionPostulanteDTO
                        {
                            IdEvaluacion = group.Key.IdEvaluacion,
                            ListaComponentesEvaluacion = group.Select(x => new NotaPostulanteDTO
                            {
                                IdProceso = x.IdProceso,
                                ProcesoSeleccion = x.ProcesoSeleccion,
                                IdSexo = x.IdSexo,
                                IdEvaluacion = x.IdEvaluacion,
                                IdGrupo = x.IdGrupo,
                                IdExamen = x.IdExamen,
                                VersionCentil = x.VersionCentil,
                                NombreEvaluacion = x.NombreEvaluacion,
                                NombreExamen = x.NombreExamen,
                                NombreGrupo = x.NombreGrupo,
                                Puntaje = x.Puntaje,
                                IdCategoria = x.IdCategoria,
                                IdEtapa = x.IdEtapa,
                                NombreCategoria = x.NombreCategoria,
                                NombreEtapa = x.NombreEtapa,
                                FactorComponente = x.FactorComponente,
                                FactorEvaluacion = x.FactorEvaluacion,
                                FactorGrupo = x.FactorGrupo,
                                IdFormulaComponente = x.IdFormulaComponente,
                                IdFormulaEvaluacion = x.IdFormulaEvaluacion,
                                IdFormulaGrupo = x.IdFormulaGrupo,
                                EstadoAcceso = x.EstadoAcceso,
                                CantidadConfigurado = x.CantidadConfigurado,
                                CantidadResuelto = x.CantidadResuelto,
                                PuntajeCurso = x.PuntajeCurso,

                            }).ToList()
                        }
                        ).ToList();

                    //Se recorre las evaluaciones de un postulante
                    foreach (var evaluacion in grupoEvaluacion)
                    {
                        var calificacionTotal = _unitOfWork.ExamenTestRepository.ObtenerPorId(evaluacion.IdEvaluacion!.Value);// se obtiene informacion de cada evaluacion del postulante
                        bool esAgrupada = evaluacion.ListaComponentesEvaluacion.Where(x => x.IdGrupo != null).Count() >= 1; // Verifica si las evaluaciones tienen al menos un grupo de Evaluacion para considerar la calificacion por Grupos
                        ProcesoSelecionExamenesCompletosDTO eval = new ProcesoSelecionExamenesCompletosDTO();

                        //Inserta la Nota por Evaluacion, su nota total y calculada
                        // el campo de CalificarEvaluacion indica si se califica por Evaluacion en caso sea true 
                        if (calificacionTotal != null && calificacionTotal.CalificarEvaluacion == true)
                        {
                            var formulaPuntajeEvaluacion = _unitOfWork.FormulaPuntajeRepository.ObtenerPorId(calificacionTotal.IdFormulaPuntaje!.Value)!; // extrae el valor de como se va a calificar la evaluacion
                            if (formulaPuntajeEvaluacion.Nombre.Contains("(Puntaje componentes)")) // Si se califica por Puntaje de Componentes, de todos los componentes de una evaluacion suma o promedia sus puntajes y esa seria la calificacion de la Evaluacion
                            {
                                var cantidadComponentes = configuracionExamen.Where(x => x.IdExamenTest == evaluacion.IdEvaluacion).Count();

                                eval = new ProcesoSelecionExamenesCompletosDTO
                                {
                                    IdProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].IdProceso,
                                    ProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].ProcesoSeleccion,
                                    IdPostulante = postulante.IdPostulante,
                                    Postulante = postulante.Postulante,
                                    Edad = 24,
                                    Examen = null,
                                    IdCategoria = evaluacion.ListaComponentesEvaluacion[0].IdCategoria,
                                    Categoria = evaluacion.ListaComponentesEvaluacion[0].NombreCategoria,
                                    IdExamen = 0,
                                    VersionCentil = evaluacion.ListaComponentesEvaluacion[0].VersionCentil,
                                    IdGrupo = 0,
                                    IdEvaluacion = evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion,
                                    Evaluacion = evaluacion.ListaComponentesEvaluacion[0].NombreEvaluacion,
                                    Grupo = null,
                                    IdEtapa = evaluacion.ListaComponentesEvaluacion[0].IdEtapa,
                                    Etapa = evaluacion.ListaComponentesEvaluacion[0].NombreEtapa,
                                    Orden = Convert.ToInt32(String.Concat(evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion.ToString(), "0", "0")),
                                    Registro = formulaPuntajeEvaluacion.Nombre.ToUpper().Contains("SUMAR") ? evaluacion.ListaComponentesEvaluacion.Sum(x => x.Puntaje).ToString() : PromediarListaPuntaje(evaluacion.ListaComponentesEvaluacion.Select(x => x.Puntaje).ToList(), cantidadComponentes, 1.00M).ToString(),
                                    EsAprobado = false,
                                    CalificaPorCentil = false,
                                    IdSexo = evaluacion.ListaComponentesEvaluacion[0].IdSexo,

                                };
                                count = count + 1M;
                                eval.OrdenReal = count; //Esta parte se utilizara para darle un orden a cada componente.
                                listaNotasProcesoSeleccion.Add(eval); // añade la Nota por Evaluacion a la listafinal
                            }
                            else // de lo contrario la calificacion de la Evaluacion se calcula mediante la suma o promedio de GRUPO DE COMPONENTES
                            {
                                List<ProcesoSelecionExamenesCompletosDTO> listaGrupo = new List<ProcesoSelecionExamenesCompletosDTO>();
                                foreach (var componente in evaluacion.ListaComponentesEvaluacion)
                                {
                                    // Verifica si se insertado el componente al Grupo de componentes listaGrupo
                                    var eval1 = listaGrupo.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault();
                                    if (eval1 == null) // aun no existe el grupo de componentes en  listaGrupo por eso se inserta
                                    {
                                        eval = new ProcesoSelecionExamenesCompletosDTO
                                        {
                                            IdProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].IdProceso,
                                            ProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].ProcesoSeleccion,
                                            IdPostulante = postulante.IdPostulante,
                                            Postulante = postulante.Postulante,
                                            Edad = 24,
                                            Examen = null,
                                            IdExamen = 0,
                                            VersionCentil = evaluacion.ListaComponentesEvaluacion[0].VersionCentil,
                                            IdGrupo = componente.IdGrupo,
                                            IdFormulaGrupo = componente.IdFormulaGrupo,
                                            IdEvaluacion = componente.IdEvaluacion,
                                            Evaluacion = componente.NombreEvaluacion,
                                            Grupo = componente.FactorComponente.ToString(),
                                            Orden = Convert.ToInt32(String.Concat(componente.IdEvaluacion, componente.IdGrupo == null ? "0" : componente.IdGrupo.ToString(), "0")),
                                            Registro = componente.Puntaje.ToString(),
                                            EsAprobado = false,
                                            CalificaPorCentil = false,
                                            EstadoAcceso = componente.EstadoAcceso,
                                            CantidadConfigurado = componente.CantidadConfigurado,
                                            CantidadResuelto = componente.CantidadResuelto,
                                            PuntajeCurso = componente.PuntajeCurso,


                                        };
                                        listaGrupo.Add(eval);
                                    }
                                    else
                                    {
                                        //si el grupo ya existe, se suma el puntaje anterior obtenido de la consulta eval1
                                        listaGrupo.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault().Registro = (Convert.ToDecimal(eval1.Registro, null) + componente.Puntaje).ToString();
                                    }
                                }

                                // Ahora se obtiene los puntajes obtenidos en el grupo y se aplica la fórmula configurada para el grupo
                                foreach (var grupo in listaGrupo)
                                {
                                    //Obtenemos la cantidad de grupos para realizar el promedio en caso fuera necesario
                                    var cantidadGrupoConfigurado = configuracionExamen.Where(x => x.IdExamenTest == evaluacion.IdEvaluacion && x.IdGrupo == grupo.IdGrupo).ToList();
                                    var formulaGrupo = grupo.IdFormulaGrupo.GetValueOrDefault();
                                    if (formulaGrupo > 0 && cantidadGrupoConfigurado.Count > 0)
                                    {
                                        if (formulaGrupo == 6) // 6 = PROMEDIAR(Puntaje componentes)
                                        {
                                            grupo.Registro = (PromediarPuntaje(Convert.ToDecimal(grupo.Registro), cantidadGrupoConfigurado.Count, 1.00M)).ToString();
                                        }
                                    }
                                }

                                // se obtiene el puntaje de la evaluacion de acuerdo a los grupos de componentes que se encuentran almacenadoes en listaGrupo
                                eval = new ProcesoSelecionExamenesCompletosDTO
                                {
                                    IdProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].IdProceso,
                                    ProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].ProcesoSeleccion,
                                    IdPostulante = postulante.IdPostulante,
                                    Postulante = postulante.Postulante,
                                    Edad = 24,
                                    Examen = null,
                                    Categoria = evaluacion.ListaComponentesEvaluacion[0].NombreCategoria,
                                    IdCategoria = evaluacion.ListaComponentesEvaluacion[0].IdCategoria,
                                    IdExamen = 0,
                                    VersionCentil = evaluacion.ListaComponentesEvaluacion[0].VersionCentil,
                                    IdGrupo = 0,
                                    IdEvaluacion = evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion,
                                    Evaluacion = evaluacion.ListaComponentesEvaluacion[0].NombreEvaluacion,
                                    Grupo = null,
                                    Etapa = evaluacion.ListaComponentesEvaluacion[0].NombreEtapa,
                                    IdEtapa = evaluacion.ListaComponentesEvaluacion[0].IdEtapa,
                                    Orden = Convert.ToInt32(String.Concat(evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion.ToString(), "0", "0")),


                                    //Verifica si la formula de la evaluacion es Suma de Grupo de Componentes o Promedio de los mismos
                                    Registro = formulaPuntajeEvaluacion.Nombre.ToUpper().Contains("SUMAR") ? listaGrupo.Sum(x => Convert.ToDecimal(x.Registro, null) * Convert.ToDecimal(x.Grupo, null)).ToString() : (listaGrupo.Sum(x => Convert.ToDecimal(x.Registro, null) * Convert.ToDecimal(x.Grupo, null)) / Convert.ToDecimal(listaGrupo.Count(), null)).ToString(),
                                    EsAprobado = false,
                                    CalificaPorCentil = false,
                                    IdSexo = evaluacion.ListaComponentesEvaluacion[0].IdSexo,
                                };
                                count = count + 1M;
                                eval.OrdenReal = count;
                                listaNotasProcesoSeleccion.Add(eval);
                            }
                        }
                        else
                        {
                            //listaNotasProcesoSeleccion
                            List<ProcesoSelecionExamenesCompletosDTO> listaAuxiliarGrupo = new List<ProcesoSelecionExamenesCompletosDTO>();

                            //Si la calificacion no es por Evaluacion entonces puede ser por Grupo de Componentes o componentes.
                            foreach (var componente in evaluacion.ListaComponentesEvaluacion)
                            {
                                //Inserta los Grupos de Componentes con su Calificacion
                                if (esAgrupada == true)
                                {
                                    var eval1 = listaAuxiliarGrupo.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault();

                                    if (eval1 == null)
                                    {
                                        eval = new ProcesoSelecionExamenesCompletosDTO
                                        {
                                            IdProcesoSeleccion = componente.IdProceso,
                                            ProcesoSeleccion = componente.ProcesoSeleccion,
                                            IdPostulante = postulante.IdPostulante,
                                            Postulante = postulante.Postulante,
                                            Edad = 24,
                                            Examen = null,
                                            IdCategoria = componente.IdCategoria,
                                            Categoria = componente.NombreCategoria,
                                            IdExamen = 0,
                                            VersionCentil = componente.VersionCentil,
                                            IdGrupo = componente.IdGrupo,
                                            IdEvaluacion = componente.IdEvaluacion,
                                            Evaluacion = componente.NombreEvaluacion,
                                            IdFormulaGrupo = componente.IdFormulaGrupo,
                                            Grupo = componente.NombreGrupo,
                                            Etapa = componente.NombreEtapa,
                                            IdEtapa = componente.IdEtapa,
                                            Orden = Convert.ToInt32(String.Concat(componente.IdEvaluacion, componente.IdGrupo == null ? "0" : componente.IdGrupo.ToString(), "0")),
                                            Registro = componente.Puntaje.ToString(),
                                            EsAprobado = false,
                                            CalificaPorCentil = false,
                                            IdSexo = componente.IdSexo,
                                            OrdenReal = count++,
                                            EstadoAcceso = componente.EstadoAcceso,
                                            CantidadConfigurado = componente.CantidadConfigurado,
                                            CantidadResuelto = componente.CantidadResuelto,
                                            PuntajeCurso = componente.PuntajeCurso,

                                        };
                                        count = count + 1M;
                                        eval.OrdenReal = count;
                                        listaAuxiliarGrupo.Add(eval);
                                    }
                                    else
                                    {
                                        listaAuxiliarGrupo.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault().Registro = (Convert.ToDecimal(eval1.Registro, null) + (componente.Puntaje)).ToString();
                                    }
                                }
                                //Inserta los Componentes con su Calificacion respectiva, es decir no realiza ni un calculo solo inserta lo componentes que se obtuvieron en la vista principal
                                else
                                {
                                    eval = new ProcesoSelecionExamenesCompletosDTO
                                    {
                                        IdProcesoSeleccion = componente.IdProceso,
                                        ProcesoSeleccion = componente.ProcesoSeleccion,
                                        IdPostulante = postulante.IdPostulante,
                                        Postulante = postulante.Postulante,
                                        Edad = 24,
                                        Examen = componente.NombreExamen,
                                        IdCategoria = componente.IdCategoria,
                                        Categoria = componente.NombreCategoria,
                                        IdExamen = componente.IdExamen,
                                        VersionCentil = componente.VersionCentil,
                                        IdGrupo = componente.IdGrupo == null ? 0 : componente.IdGrupo,
                                        IdEvaluacion = componente.IdEvaluacion,
                                        Evaluacion = componente.NombreEvaluacion,
                                        Grupo = componente.NombreGrupo,
                                        IdFormulaGrupo = componente.IdFormulaGrupo,
                                        Etapa = componente.NombreEtapa,
                                        IdEtapa = componente.IdEtapa,
                                        Orden = Convert.ToInt32(String.Concat(componente.IdEvaluacion, componente.IdGrupo == null ? "0" : componente.IdGrupo.ToString(), componente.IdExamen == null ? "0" : componente.IdExamen.ToString())),
                                        Registro = componente.Puntaje.ToString(),
                                        EsAprobado = false,
                                        CalificaPorCentil = false,
                                        IdSexo = componente.IdSexo,
                                        EstadoAcceso = componente.EstadoAcceso,
                                        CantidadConfigurado = componente.CantidadConfigurado,
                                        CantidadResuelto = componente.CantidadResuelto,
                                        PuntajeCurso = componente.PuntajeCurso,

                                    };
                                    count = count + 1M;
                                    eval.OrdenReal = count;
                                    listaAuxiliarGrupo.Add(eval); // inserta la calificacion de los grupos de componentes 
                                }
                            }

                            // Ahora se obtiene los puntajes obtenidos en el grupo y se aplica la fórmula configurada para el grupo
                            foreach (var grupo in listaAuxiliarGrupo)
                            {
                                //Obtenemos la cantidad de grupos para realizar el promedio en caso fuera necesario
                                var cantidadGrupoConfigurado = configuracionExamen.Where(x => x.IdExamenTest == evaluacion.IdEvaluacion && x.IdGrupo == grupo.IdGrupo).ToList();
                                var formulaGrupo = grupo.IdFormulaGrupo.GetValueOrDefault();
                                if (formulaGrupo > 0 && cantidadGrupoConfigurado.Count > 0)
                                {
                                    if (formulaGrupo == 6) // 6 = PROMEDIAR(Puntaje componentes)
                                    {
                                        grupo.Registro = (PromediarPuntaje(Convert.ToDecimal(grupo.Registro), cantidadGrupoConfigurado.Count, 1.00M)).ToString();
                                    }
                                }
                            }
                            listaNotasProcesoSeleccion.AddRange(listaAuxiliarGrupo);
                        }
                    }
                }

                var informacionCentilCalificacion = new List<ObtenerCalificacionCentilDTO>();
                if (filtro.IdProcesoSeleccion != null)
                {
                    var idsProcesoSeleccion = new List<int>{
                           filtro.IdProcesoSeleccion.Value
                       };
                    informacionCentilCalificacion = _unitOfWork.ExamenRepository.ObtenerInformacionCentilPorProcesoSeleccion(idsProcesoSeleccion);
                }
                else
                {
                    var idsProcesoSeleccion = reporte.Select(x => x.IdProceso).Distinct().ToList();
                    if (idsProcesoSeleccion != null && idsProcesoSeleccion.Count > 0)
                    {
                        informacionCentilCalificacion = _unitOfWork.ExamenRepository.ObtenerInformacionCentilPorProcesoSeleccion(idsProcesoSeleccion);
                    }
                }

                List<CentilDTO> centilesCompletos = new List<CentilDTO>();
                var informacionCentilCalificacionAuxiliar = _unitOfWork.CentilRepository.ObtenerCentilesSinExamenTest();
                centilesCompletos.AddRange(informacionCentilCalificacionAuxiliar);
                //lista centiles
                List<CentilDTO> centilesAsociados = new List<CentilDTO>();
                if (informacionCentilCalificacion.Count > 0)
                {
                    var idsExamen = informacionCentilCalificacion.Where(x => x.IdExamen != null).Select(x => x.IdExamen!.Value).Distinct().ToList();
                    if (idsExamen.Count > 0)
                    {
                        centilesAsociados = _unitOfWork.ExamenRepository.ObtenerCentilesAsociados(idsExamen);
                        centilesCompletos.AddRange(centilesAsociados);
                    }
                }

                //filtro puntaje minimo centil idexamen test
                var notasAsociadas = informacionCentilCalificacion.Where(x => x.PuntajeMinimo != null).GroupBy(s => s.IdExamen ?? s.IdExamenTest).Select(n =>
                   new
                   {
                       Id = n.Key,

                       Data = n.Select(r =>
                       new
                       {
                           PuntajeMinimo = r.PuntajeMinimo.GetValueOrDefault(),
                           IdCentil = r.IdCentil.GetValueOrDefault()

                       }).Distinct().ToList()
                   }).Distinct().ToList();

                //lista de examenes
                var VersionCentilesExamen = centilesCompletos.Where(x => x.IdExamen != null).GroupBy(s => s.IdExamen).Select(n =>
                new
                {
                    IdExamen = n.Key,
                    VersionCentil = n.Select(r =>
                    new
                    {
                        Version = r.Version.GetValueOrDefault(),
                        EsVigente = r.EsVigente.GetValueOrDefault()
                    }).Distinct().ToList()
                }).Distinct().ToList();

                var VersionCentilesExamenTest = centilesCompletos.Where(x => x.IdExamenTest != null).GroupBy(s => s.IdExamenTest).Select(n =>
                new
                {
                    IdExamenTest = n.Key,
                    VersionCentil = n.Select(r =>
                    new
                    {
                        Version = r.Version.GetValueOrDefault(),
                        EsVigente = r.EsVigente.GetValueOrDefault()
                    }).Distinct().ToList()
                }).Distinct().ToList();

                var VersionCentilesSinExamen = centilesCompletos.Where(x => x.IdExamenTest == null && x.IdExamen == null).Select(n => new
                {
                    Version = n.Version.GetValueOrDefault(),
                    EsVigente = n.EsVigente.GetValueOrDefault()
                }).Distinct().ToList();

                //lista centiles grupo
                var VersionCentilesGrupo = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion != null).GroupBy(s => s.IdGrupoComponenteEvaluacion).Select(n =>
                new
                {
                    IdGrupoComponenteEvaluacion = n.Key,
                    VersionCentil = n.Select(r =>
                    new
                    {
                        Version = r.Version.GetValueOrDefault(),
                        EsVigente = r.EsVigente.GetValueOrDefault()
                    }).Distinct().ToList()
                }).Distinct().ToList();
                var VersionCentilesGrupoExamenTest = centilesCompletos.Where(x => x.IdExamenTest != null && x.IdGrupoComponenteEvaluacion != null).GroupBy(s => new { s.IdExamenTest, s.IdGrupoComponenteEvaluacion }).Select(n =>
                new
                {
                    IdExamenTest = n.Key.IdExamenTest,
                    IdGrupoComponenteEvaluacion = n.Key.IdGrupoComponenteEvaluacion,
                    VersionCentil = n.Select(r =>
                    new
                    {
                        Version = r.Version.GetValueOrDefault(),
                        EsVigente = r.EsVigente.GetValueOrDefault()
                    }).Distinct().ToList()
                }).Distinct().ToList();

                // En esta parte se usa la configuracion del proceso para saber si se califica por centil o en forma directa, en caso se califica por centil se va a la tabla de centiles y se busca su calificacion
                //tambien segun lo parametros configurados, se calcula si un postulante aprueba o no

                

                foreach (var item in listaNotasProcesoSeleccion)
                {
                    item.OrdenReal = item.Orden;

                    var puntaje = Convert.ToDecimal(item.Registro, null);
                    if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                    {
                        var cantidad = _unitOfWork.AsignacionPreguntaExamenRepository.ObtenerCantidadPreguntaExamen(item.IdExamen);
                        var countPregunta = Convert.ToDecimal(cantidad, null);
                        if (cantidad == 0)
                        {
                            throw new BadRequestException("No se encontro la cantidad de preguntas por examen");
                        }
                        item.Registro = FuncionRedondeo((puntaje * 100.0M / countPregunta), _redondeoGeneral).ToString();
                    }

                    // Cambia los puntajes de las Evaluaciones
                    if (item.IdEvaluacion != 0 && item.IdGrupo == 0 && item.IdExamen == 0)
                    {
                        //var calificacionOriginal = repProcesoSeleccionPuntajeCalificacionRep.FirstBy(x => x.Estado == true && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == null && x.IdExamen == null && x.IdProcesoSeleccion == item.IdProcesoSeleccion);
                        var calificacion = informacionCentilCalificacion.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == null && x.IdExamen == null && x.IdProcesoSeleccion == item.IdProcesoSeleccion).FirstOrDefault();

                        if (calificacion == null)
                        {
                            item.EsAprobado = false;
                            item.NotaAprobatoria = "N.A";
                        }
                        else
                        {
                            // La Calificacion debe de ser Igual a Valor minimo del componente o evaluacion
                            item.NotaAprobatoria = calificacion.PuntajeMinimo.ToString();


                            if (calificacion.IdProcesoSeleccionRango == 1)
                            {
                                item.Simbolo = "=";

                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesExamenTest.Where(x => x.IdExamenTest == item.IdEvaluacion).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (calificacion.PuntajeMinimo == Convert.ToDecimal(examenCentilVersion.Registro, null))
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (calificacion.PuntajeMinimo == Convert.ToDecimal(item.Registro, null))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (calificacion.PuntajeMinimo == Convert.ToDecimal(item.Registro, null))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 2)
                            {
                                item.Simbolo = ">";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesExamenTest.Where(x => x.IdExamenTest == item.IdEvaluacion).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();

                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) > calificacion.PuntajeMinimo)
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 9)
                            {
                                item.Simbolo = "<";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesExamenTest.Where(x => x.IdExamenTest == item.IdEvaluacion).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) < calificacion.PuntajeMinimo)
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 12)
                            {
                                item.Simbolo = ">=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesExamenTest.Where(x => x.IdExamenTest == item.IdEvaluacion).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) >= calificacion.PuntajeMinimo)
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 13)
                            {
                                item.Simbolo = "<=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesExamenTest.Where(x => x.IdExamenTest == item.IdEvaluacion).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) <= calificacion.PuntajeMinimo)
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            if (calificacion.IdProcesoSeleccionRango == null || calificacion.IdProcesoSeleccionRango == 0)
                            {
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesExamenTest.Where(x => x.IdExamenTest == item.IdEvaluacion).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                        }
                    }


                    // Cambia los puntajes de las Grupos
                    if (item.IdEvaluacion != 0 && item.IdGrupo != 0 && item.IdExamen == 0)
                    {
                        var calificacion = informacionCentilCalificacion.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdExamen == null && x.IdProcesoSeleccion == item.IdProcesoSeleccion).FirstOrDefault();

                        if (calificacion == null)
                        {
                            item.EsAprobado = false;
                            item.NotaAprobatoria = "N.A";
                        }
                        else
                        {

                            // La Calificacion debe de ser Igual a Valor minimo del componente o evaluacion
                            item.NotaAprobatoria = calificacion.PuntajeMinimo.ToString();
                            if (calificacion.IdProcesoSeleccionRango == 1)
                            {
                                item.Simbolo = "=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesGrupo.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) == calificacion.PuntajeMinimo)
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 2)
                            {
                                item.Simbolo = ">";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesGrupo.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) > calificacion.PuntajeMinimo)
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 9)
                            {
                                item.Simbolo = "<";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesGrupo.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) < calificacion.PuntajeMinimo)
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 12)
                            {
                                item.Simbolo = ">=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesGrupo.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) >= calificacion.PuntajeMinimo)
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 13)
                            {
                                item.Simbolo = "<=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesGrupo.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) <= calificacion.PuntajeMinimo)
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            if (calificacion.IdProcesoSeleccionRango == null || calificacion.IdProcesoSeleccionRango == 0)
                            {
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;

                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesGrupo.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }
                        }
                    }


                    // Cambia los puntajes de los componentes
                    if (item.IdEvaluacion != 0 && item.IdGrupo == 0 && item.IdExamen != 0)
                    {
                        //var calificacionOriginal = repProcesoSeleccionPuntajeCalificacionRep.FirstBy(x => x.Estado == true && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == null && x.IdExamen == item.IdExamen && x.IdProcesoSeleccion == item.IdProcesoSeleccion);
                        var calificacion = informacionCentilCalificacion.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == null && x.IdExamen == item.IdExamen && x.IdProcesoSeleccion == item.IdProcesoSeleccion).FirstOrDefault();

                        if (calificacion == null)
                        {
                            item.EsAprobado = false;
                            item.NotaAprobatoria = "N.A";
                        }
                        else
                        {
                            // La Calificacion debe de ser Igual a Valor minimo del componente o evaluacion
                            item.NotaAprobatoria = calificacion.PuntajeMinimo.ToString();
                            if (calificacion.IdProcesoSeleccionRango == 1)
                            {
                                item.Simbolo = "=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesExamen.Where(x => x.IdExamen == item.IdExamen).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) == calificacion.PuntajeMinimo)
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 2)
                            {
                                item.Simbolo = ">";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesExamen.Where(x => x.IdExamen == item.IdExamen).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) > calificacion.PuntajeMinimo)
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 9)
                            {
                                item.Simbolo = "<";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesExamen.Where(x => x.IdExamen == item.IdExamen).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) < calificacion.PuntajeMinimo)
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 12)
                            {
                                item.Simbolo = ">=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesExamen.Where(x => x.IdExamen == item.IdExamen).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) >= calificacion.PuntajeMinimo)
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 13)
                            {
                                item.Simbolo = "<=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesExamen.Where(x => x.IdExamen == item.IdExamen).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) <= calificacion.PuntajeMinimo)
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            if (calificacion.IdProcesoSeleccionRango == null || calificacion.IdProcesoSeleccionRango == 0)
                            {
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == item.VersionCentil).FirstOrDefault();

                                    var versiones = VersionCentilesExamen.Where(x => x.IdExamen == item.IdExamen).FirstOrDefault();
                                    if (versiones != null)
                                    {
                                        item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                                        versiones.VersionCentil.ForEach(s =>
                                        {
                                            var subcentil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                            var examenCentilVersion = new ExamenCentilVersionDTO();
                                            examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                            examenCentilVersion.Simbolo = item.Simbolo;
                                            if (subcentil == null)
                                            {
                                                examenCentilVersion.Registro = "SIN CENTIL";
                                                examenCentilVersion.EsAprobado = false;
                                            }
                                            else
                                            {
                                                examenCentilVersion.IdCentil = subcentil.Id;
                                                examenCentilVersion.Registro = subcentil.Centil.ToString();
                                                if (Convert.ToDecimal(examenCentilVersion.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                                {
                                                    examenCentilVersion.EsAprobado = true;
                                                }
                                                if (calificacion.EsCalificable == false)
                                                {
                                                    examenCentilVersion.EsAprobado = false;
                                                    examenCentilVersion.NotaAprobatoria = "N.A";
                                                    examenCentilVersion.Simbolo = "";
                                                }
                                            }
                                            examenCentilVersion.Version = s.Version;
                                            examenCentilVersion.EsVigente = s.EsVigente;
                                            examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;

                                            if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                            {
                                                examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro), _redondeoGeneral).ToString();
                                                if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                                                {
                                                    examenCentilVersion.Registro = examenCentilVersion.Registro + "%";
                                                }
                                            }
                                            item.ExamenCentilVersion.Add(examenCentilVersion);
                                        });
                                    }

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                        }
                    }

                    if (!item.Registro.Equals("SIN CENTIL"))
                    {

                        item.Registro = FuncionRedondeo(Convert.ToDecimal(item.Registro), _redondeoGeneral).ToString();
                        if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.IdEvaluacion == 97 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                        {
                            item.Registro = item.Registro + "%";
                        }

                    }

                    item.OrdenReal = item.Orden;
                    // Esta parte se agrego para poder ordenar la evaluacion de Aptitudes a como el señor juan carlos lo requeria. el codigo de orden real esta compuesto por IdEvaluacion IdGrupo IdComponente, lo que forma el codigo de OrdenReal
                    switch (item.OrdenReal)
                    {
                        case 53052:
                            item.OrdenReal = 53051;
                            break;
                        case 53053:
                            item.OrdenReal = 53052;
                            break;
                        case 53051:
                            item.OrdenReal = 53053;
                            break;
                        case 53054:
                            item.OrdenReal = 53054;
                            break;
                    }
                }


                List<ProcesoSelecionExamenesCompletosDTO> listaNotasProcesoSeleccionComplemento = new List<ProcesoSelecionExamenesCompletosDTO>();

                // Esta parte es para que siempre muestre las calificaciones de los componentes asi sea por CalificacionTotal o CalificacionAgrupada.

                //Obtiene solo las evaluaciones con calificacion Total
                var Evaluaciones2 = listaNotasProcesoSeleccion.Where(x => x.IdExamen == 0 && x.IdGrupo == 0).Select(x => new ProcesoSelecionExamenesCompletosComplementoDTO { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal }).ToList();
                var Evaluaciones = Evaluaciones2
                    .GroupBy(x => new { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal })
                    .Select(group =>
                    new ProcesoSelecionExamenesCompletosComplementoDTO
                    {
                        IdProcesoSeleccion = group.Key.IdProcesoSeleccion
                        ,
                        IdEtapa = group.Key.IdEtapa
                        ,
                        IdEvaluacion = group.Key.IdEvaluacion
                        ,
                        IdGrupo = group.Key.IdGrupo
                        ,
                        IdExamen = group.Key.IdExamen
                        ,
                        EsAprobado = group.Key.EsAprobado
                        ,
                        CalificaPorCentil = group.Key.CalificaPorCentil
                        ,
                        Orden = group.Key.Orden
                        ,
                        OrdenReal = group.Key.OrdenReal
                    })
                    .ToList();
                // Obtiene solo las evaluaciones con calificacion agrupada
                var GrupoComponente2 = listaNotasProcesoSeleccion.Where(x => x.IdExamen == 0 && x.IdGrupo != 0).Select(x => new ProcesoSelecionExamenesCompletosComplementoDTO { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal }).Distinct().ToList();
                var GrupoComponente = GrupoComponente2
                    .GroupBy(x => new { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal })
                    .Select(group =>
                    new ProcesoSelecionExamenesCompletosComplementoDTO
                    {
                        IdProcesoSeleccion = group.Key.IdProcesoSeleccion
                        ,
                        IdEtapa = group.Key.IdEtapa
                        ,
                        IdEvaluacion = group.Key.IdEvaluacion
                        ,
                        IdGrupo = group.Key.IdGrupo
                        ,
                        IdExamen = group.Key.IdExamen
                        ,
                        EsAprobado = group.Key.EsAprobado
                        ,
                        CalificaPorCentil = group.Key.CalificaPorCentil
                        ,
                        Orden = group.Key.Orden
                        ,
                        OrdenReal = group.Key.OrdenReal
                    })
                    .ToList();
                foreach (var item in Evaluaciones)
                {
                    count = 0.00M;
                    var EvaluacionesGrupo = reporte.Where(x => x.IdGrupo == null && x.IdEvaluacion == item.IdEvaluacion).ToList();
                    foreach (var componente in EvaluacionesGrupo)
                    {
                        if (componente.Titulo.Equals("N1: Ansiedad"))
                        {
                            var c = 323;
                        }
                        var existe = listaNotasProcesoSeleccionComplemento.Where(x => x.IdEvaluacion == componente.IdEvaluacion && x.IdExamen == componente.IdExamen /*&&x.IdProcesoSeleccion==componente.IdProceso && x.IdEtapa==componente.IdEtapa*/).FirstOrDefault();
                        var eval = new ProcesoSelecionExamenesCompletosDTO
                        {
                            IdProcesoSeleccion = componente.IdProceso,
                            ProcesoSeleccion = componente.NombreProceso,
                            IdPostulante = componente.IdPostulante,
                            Postulante = componente.Postulante,
                            Edad = 24,
                            Examen = componente.NombreExamen,
                            IdCategoria = componente.IdCategoria,
                            Categoria = componente.NombreCategoria,
                            IdExamen = componente.IdExamen,
                            VersionCentil = componente.VersionCentil,
                            IdGrupo = componente.IdGrupo == null ? 0 : componente.IdGrupo,
                            IdEvaluacion = componente.IdEvaluacion,
                            Evaluacion = componente.NombreEvaluacion,
                            Grupo = componente.NombreGrupo,
                            Etapa = componente.NombreEtapa,
                            IdEtapa = componente.IdEtapa,
                            Orden = item.Orden,
                            Registro = componente.Puntaje.ToString(),
                            EsAprobado = item.EsAprobado,
                            CalificaPorCentil = item.CalificaPorCentil,
                            IdSexo = componente.IdSexo,
                            NotaAprobatoria = "N.A",
                            Simbolo = "",
                            EstadoAcceso = componente.EstadoAcceso,
                            CantidadConfigurado = componente.CantidadConfigurado,
                            CantidadResuelto = componente.CantidadResuelto,
                            PuntajeCurso = componente.PuntajeCurso
                        };
                        if (existe != null)
                        {
                            eval.OrdenReal = existe.OrdenReal;
                        }
                        else
                        {
                            count = count + 0.01M; // esta parte interviene en el orden se le pone como decimal para que los componentes siempre esten dentro de la evaluacion o Grupo de Componentes.
                            eval.OrdenReal = item.OrdenReal + count;
                        }
                        listaNotasProcesoSeleccionComplemento.Add(eval);
                    }

                }



                foreach (var item in GrupoComponente)
                {
                    count = 0.00M;
                    var grupos = reporte.Where(x => x.IdGrupo == item.IdGrupo && x.IdEvaluacion == item.IdEvaluacion).ToList();
                    foreach (var componente in grupos)
                    {
                        if (componente.Titulo.Equals("N1: Ansiedad"))
                        {
                            var c = 323;
                        }
                        var existe = listaNotasProcesoSeleccionComplemento.Where(x => x.IdEvaluacion == componente.IdEvaluacion && x.IdExamen == componente.IdExamen /*&& x.IdProcesoSeleccion == componente.IdProceso && x.IdEtapa == componente.IdEtapa*/).FirstOrDefault();
                        var eval = new ProcesoSelecionExamenesCompletosDTO
                        {
                            IdProcesoSeleccion = componente.IdProceso,
                            ProcesoSeleccion = componente.NombreProceso,
                            IdPostulante = componente.IdPostulante,
                            Postulante = componente.Postulante,
                            Edad = 24,
                            Examen = componente.NombreExamen,
                            IdCategoria = componente.IdCategoria,
                            Categoria = componente.NombreCategoria,
                            IdExamen = componente.IdExamen,
                            VersionCentil = componente.VersionCentil,
                            IdGrupo = componente.IdGrupo == null ? 0 : componente.IdGrupo,
                            IdEvaluacion = componente.IdEvaluacion,
                            Evaluacion = componente.NombreEvaluacion,
                            Grupo = componente.NombreGrupo,
                            Etapa = componente.NombreEtapa,
                            IdEtapa = componente.IdEtapa,
                            Orden = item.Orden,
                            Registro = componente.Puntaje.ToString(),
                            EsAprobado = item.EsAprobado,
                            CalificaPorCentil = item.CalificaPorCentil,
                            IdSexo = componente.IdSexo,
                            NotaAprobatoria = "N.A",
                            Simbolo = "",
                            EstadoAcceso = componente.EstadoAcceso,
                            CantidadConfigurado = componente.CantidadConfigurado,
                            CantidadResuelto = componente.CantidadResuelto,
                            PuntajeCurso = componente.PuntajeCurso
                        };
                        if (existe != null)
                        {
                            eval.OrdenReal = existe.OrdenReal;
                        }
                        else
                        {

                            count = count + 0.01M;
                            eval.OrdenReal = item.OrdenReal + count;
                        }
                        listaNotasProcesoSeleccionComplemento.Add(eval);
                    }

                }


                // Aqui a las calificaciones de componentes de las Evaluaciones y Grupo de Componentes se le calcula su puntaje por centil y no se evalua si esta aprobado
                //o no ya que en la configuracion el que deberia de tener la calificacion es la Evaluacion o el Grupo de Componentes que faltaban obtener 
                foreach (var item in listaNotasProcesoSeleccionComplemento)
                {

                    var puntaje = Convert.ToDecimal(item.Registro, null);   

                    if (item.CalificaPorCentil)
                    {
                        item.CalificaPorCentil = true;
                        var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version == item.VersionCentil).FirstOrDefault();

                        var versiones = VersionCentilesExamen.Where(x => x.IdExamen == item.IdExamen).FirstOrDefault();
                        if (versiones != null)
                        {
                            item.ExamenCentilVersion = new List<ExamenCentilVersionDTO>();
                            versiones.VersionCentil.ForEach(s =>
                            {
                                var subcentil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo) && x.Version.GetValueOrDefault() == s.Version).FirstOrDefault();

                                var examenCentilVersion = new ExamenCentilVersionDTO();
                                if (subcentil == null)
                                {
                                    examenCentilVersion.Registro = "SIN CENTIL";
                                    examenCentilVersion.EsAprobado = false;
                                }
                                else
                                {
                                    examenCentilVersion.IdCentil = subcentil.Id;
                                    examenCentilVersion.Registro = subcentil.Centil.ToString();
                                }
                                examenCentilVersion.NotaAprobatoria = item.NotaAprobatoria;
                                examenCentilVersion.Simbolo = item.Simbolo;
                                examenCentilVersion.Version = s.Version;
                                examenCentilVersion.EsVigente = s.EsVigente;
                                examenCentilVersion.EsVersionExamen = s.Version == item.VersionCentil;
                                if (!examenCentilVersion.Registro.Equals("SIN CENTIL"))
                                {
                                    examenCentilVersion.Registro = FuncionRedondeo(Convert.ToDecimal(examenCentilVersion.Registro, null), _redondeoGeneral).ToString();
                                }
                                item.ExamenCentilVersion.Add(examenCentilVersion);
                            });
                        }
                        if (centil == null)
                        {
                            item.Registro = "SIN CENTIL";
                            item.EsAprobado = false;
                        }
                        else
                        {
                            item.Registro = centil.Centil.ToString();
                        }
                    }
                    if (!item.Registro.Equals("SIN CENTIL") && item.IdEvaluacion != 53)
                    {
                        var puntaje2 = Convert.ToDecimal(item.Registro, null);
                        item.Registro = FuncionRedondeo(puntaje2, _redondeoGeneral).ToString();
                    }
                }

              //finalmente se concatenan las dos listas para obtener el resultado final
                listaNotasProcesoSeleccion = listaNotasProcesoSeleccion.Concat(listaNotasProcesoSeleccionComplemento).ToList();


                foreach (var item in listaNotasProcesoSeleccionComplemento)
                {

                    // Cambia los puntajes de los componentes
                    if (item.IdEvaluacion != 0 && item.IdGrupo != 0 && item.IdExamen != 0)
                    {
                        var calificacion = informacionCentilCalificacion
                            .Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdExamen == item.IdExamen && x.IdProcesoSeleccion == item.IdProcesoSeleccion)
                            .FirstOrDefault();

                        if (calificacion == null)
                        {
                            item.EsAprobado = false;
                            item.NotaAprobatoria = "N.A";
                        }
                        else
                        {
                            // La Calificacion debe de ser Igual a Valor minimo del componente o evaluacion
                            item.NotaAprobatoria = calificacion.PuntajeMinimo.ToString();
                            if (calificacion.IdProcesoSeleccionRango == 1)
                            {
                                item.Simbolo = "=";
                            }

                            // La Calificacion debe de ser Mayor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 2)
                            {
                                item.Simbolo = ">";
                            }

                            // La Calificacion debe de ser Menor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 9)
                            {
                                item.Simbolo = "<";

                            }

                            // La Calificacion debe de ser Mayor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 12)
                            {
                                item.Simbolo = ">=";

                            }

                            // La Calificacion debe de ser Menor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 13)
                            {
                                item.Simbolo = "<=";


                            }
                        }
                    }

                 
                }

                foreach (var notas in notasAsociadas)
                {
                    var conincidencias = listaNotasProcesoSeleccionComplemento
                        .Where(x => x.IdExamen == notas.Id)
                        .ToList();

                    foreach (var co in conincidencias)
                    {
                        co.NotaAprobatoria = notas.Data[0].PuntajeMinimo.ToString();
                        co.Simbolo = co.Simbolo;
             
                        foreach (var i in co.ExamenCentilVersion)
                        {
                            i.NotaAprobatoria = notas.Data[0].PuntajeMinimo.ToString();
                            i.Simbolo = co.Simbolo;
                        }

                    }

                    
                }

                return listaNotasProcesoSeleccion.OrderBy(x => x.OrdenReal.Value).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private decimal FuncionRedondeo(decimal numero, int cantidadDecimales)
        {
            return Math.Round(numero, cantidadDecimales, MidpointRounding.AwayFromZero);
        }
        private decimal PromediarListaPuntaje(List<decimal?> listaPuntaje, int denominador, decimal factor)
        {
            decimal resultado = 0;
            if (denominador > 0) resultado = (decimal)(listaPuntaje.Sum() / Convert.ToDecimal(denominador));
            return resultado * factor;
        }
        private decimal PromediarPuntaje(decimal Puntaje, int denominador, decimal factor)
        {
            decimal resultado = 0;
            if (denominador > 0) resultado = (decimal)(Puntaje / Convert.ToDecimal(denominador));
            return resultado * factor;
        }
        public TipoEvaluacionDTO ObtenerTipoExamen(FiltroTipoExamenDTO filtro)
        {
            TipoEvaluacionDTO resultado = new TipoEvaluacionDTO();
            var configuracion = _unitOfWork.ConfiguracionAsignacionEvaluacionRepository.GetBy(x => x.IdProcesoSeleccion == filtro.IdProcesoSeleccion && x.IdProcesoSeleccionEtapa == filtro.IdEtapa).FirstOrDefault();
            if (configuracion == null)
            {
                var casoFiltro = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.GetBy(x => x.IdPostulante == filtro.IdPostulante && x.IdProcesoSeleccionEtapa == filtro.IdEtapa).OrderByDescending(x => x.Id).FirstOrDefault();
                if (casoFiltro != null)
                {
                    resultado.TipoEvaluacion = 1;
                    resultado.IdEvaluacion = null;
                    return resultado;
                }
                else
                {
                    resultado.TipoEvaluacion = 3;
                    resultado.IdEvaluacion = null;
                    return resultado;
                }
            }
            else
            {
                var examenTest = _unitOfWork.ExamenTestRepository.GetBy(x => x.Id == configuracion.IdEvaluacion).FirstOrDefault();
                if (examenTest != null)
                {
                    if (examenTest.EsCalificadoPorPostulante == true)
                    {
                        resultado.TipoEvaluacion = 1;
                        resultado.IdEvaluacion = examenTest.Id;
                        return resultado;
                    }
                    else
                    {
                        resultado.TipoEvaluacion = 2;
                        resultado.IdEvaluacion = examenTest.Id;
                        return resultado;
                    }
                }
                else
                {
                    resultado.TipoEvaluacion = 3;
                    resultado.IdEvaluacion = null;
                    return resultado;
                }
            }
        }
        public List<EvaluacionesAsignadasEvaluadorDTO> ObtenerEvaluacionesAsignadasEvaluador(int idPostulante, int idProcesoSeleccion)
        {
            try
            {
                return _unitOfWork.ExamenAsignadoEvaluadorRepository.ObtenerListaEvaluacionEvaluador(idPostulante, idProcesoSeleccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 03/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Preguntas y Respuestas Realizadas de Test Evaluador
        /// </summary>
        /// <returns> Lista Agrupada de ObjetoDTO : List<PreguntaTestDTO> </returns>
        public PreguntaTestAgrupadoDTO ObtenerPreguntasRespuestasRealizadasTestEvaluador(TestInformacionDTO testInformacion)
        {
            try
            {
                var listaTest = _unitOfWork.ExamenAsignadoEvaluadorRepository.ObtenerPreguntasTest(testInformacion);
                var agrupado = listaTest.GroupBy(x => new { x.IdEvaluacion, x.IdProcesoSeleccion, x.IdPostulante }).Select(g => new PreguntaTestAgrupadoDTO
                {
                    IdEvaluacion = g.Key.IdEvaluacion,
                    IdProcesoSeleccion = g.Key.IdProcesoSeleccion,
                    IdPostulante = g.Key.IdPostulante,
                    ListaPreguntas = g.GroupBy(y => new { y.IdExamenAsignado, y.IdExamen, y.IdPregunta, y.EnunciadoPregunta, y.NroOrdenPregunta, y.IdPreguntaTipo, y.PreguntaTipo, y.IdTipoRespuesta, y.TipoRespuesta }).Select(h => new PreguntaTestAgrupadoDetalleDTO
                    {
                        IdExamenAsignado = h.Key.IdExamenAsignado,
                        IdExamen = h.Key.IdExamen,
                        IdPregunta = h.Key.IdPregunta,
                        EnunciadoPregunta = h.Key.EnunciadoPregunta,
                        NroOrdenPregunta = h.Key.NroOrdenPregunta,
                        IdPreguntaTipo = h.Key.IdPreguntaTipo,
                        PreguntaTipo = h.Key.PreguntaTipo,
                        IdTipoRespuesta = h.Key.IdTipoRespuesta,
                        TipoRespuesta = h.Key.TipoRespuesta
                    }).OrderBy(x => x.NroOrdenPregunta).ToList()
                }).FirstOrDefault();
                foreach (var item in agrupado!.ListaPreguntas)
                {
                    item.ListaRespuestas = _unitOfWork.ExamenAsignadoEvaluadorRepository.ObtenerListaPreguntasRespuestaTest(item.IdExamen, item.IdPregunta);
                    item.ListaRespuestasRealizada = _unitOfWork.ExamenRealizadoRespuestaEvaluadorRepository.GetBy(x => x.IdExamenAsignadoEvaluador == item.IdExamenAsignado && x.IdPregunta == item.IdPregunta).Select(x => new RespuestaRealizadaDTO
                    {
                        Id = x.Id,
                        IdExamenAsignadoEvaluador = x.IdExamenAsignadoEvaluador,
                        IdPregunta = x.IdPregunta,
                        IdRespuestaPregunta = x.IdRespuestaPregunta,
                        TextoRespuesta = x.TextoRespuesta
                    }).ToList();
                }
                return agrupado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PreguntaTestAgrupadoDTO ObtenerPreguntasRespuestasTestEvaluador(TestInformacionDTO testInformacion)
        {
            try
            {
                var listaTest = _unitOfWork.ExamenAsignadoEvaluadorRepository.ObtenerPreguntasTest(testInformacion);
                var agrupado = listaTest.GroupBy(x => new { x.IdEvaluacion, x.IdProcesoSeleccion, x.IdPostulante }).Select(g => new PreguntaTestAgrupadoDTO
                {
                    IdEvaluacion = g.Key.IdEvaluacion,
                    IdProcesoSeleccion = g.Key.IdProcesoSeleccion,
                    IdPostulante = g.Key.IdPostulante,
                    ListaPreguntas = g.GroupBy(y => new { y.IdExamenAsignado, y.IdExamen, y.IdPregunta, y.EnunciadoPregunta, y.NroOrdenPregunta, y.IdPreguntaTipo, y.PreguntaTipo, y.IdTipoRespuesta, y.TipoRespuesta }).Select(h => new PreguntaTestAgrupadoDetalleDTO
                    {
                        IdExamenAsignado = h.Key.IdExamenAsignado,
                        IdExamen = h.Key.IdExamen,
                        IdPregunta = h.Key.IdPregunta,
                        EnunciadoPregunta = h.Key.EnunciadoPregunta,
                        NroOrdenPregunta = h.Key.NroOrdenPregunta,
                        IdPreguntaTipo = h.Key.IdPreguntaTipo,
                        PreguntaTipo = h.Key.PreguntaTipo,
                        IdTipoRespuesta = h.Key.IdTipoRespuesta,
                        TipoRespuesta = h.Key.TipoRespuesta
                    }).OrderBy(x => x.NroOrdenPregunta).ToList()
                }).FirstOrDefault();
                foreach (var item in agrupado.ListaPreguntas)
                {
                    item.ListaRespuestas = _unitOfWork.ExamenAsignadoEvaluadorRepository.ObtenerListaPreguntasRespuestaTest(item.IdExamen, item.IdPregunta);
                }
                return agrupado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EvaluacionPortalPostulante> ObtenerEvaluacionesPortalPostulante(EvaluacionPostulanteFiltroReporteDTO filtro)
        {
            try
            {
                var resultado = _unitOfWork.ExamenAsignadoEvaluadorRepository.ObtenerEvaluacionesPortalPostulante(filtro);
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 19/06/2024
        /// Versión: 2.0
        /// <summary>
        /// Guarda respuestas realizadas por evaluador y califica etapa de evaluación
        /// </summary>
        /// <returns> Confirmación de inserción : Bool </returns>
        public bool ActualizacionManualEtapaExamenAsignado(CalificacionManualDTO dto, string usuario)
        {
            try
            {
                var etapaAnterior = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerEtapaActualPorIdPostulante(dto.IdPostulanteEA);
                if (etapaAnterior != null)
                {
                    etapaAnterior.EsEtapaActual = false;
                    etapaAnterior.UsuarioModificacion = usuario;
                    etapaAnterior.FechaModificacion = DateTime.Now;
                    _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.Update(etapaAnterior);
                    _unitOfWork.Commit();
                }

                var idEtapaCalificadaActual = 0;
                var etapaCalificada = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerPorIdPostulanteIdProcesoSeleccionEtapa(dto.IdPostulanteEA, dto.IdProcesoSeleccionEtapaEA);
                if (etapaCalificada != null)
                {
                    idEtapaCalificadaActual = etapaCalificada.Id;
                    if (dto.IdEstadoEA == 2 || dto.IdEstadoEA == 3 || dto.IdEstadoEA == 4 || dto.IdEstadoEA == 9)
                    {
                        etapaCalificada.EsEtapaAprobada = false;
                    }
                    else
                    {
                        etapaCalificada.EsEtapaAprobada = true;
                    }

                    if (dto.IdEstadoEA == 9) /// Estado de Etapa Sin rendir
                    {
                        etapaCalificada.EsEtapaActual = false;
                        etapaCalificada.EsContactado = false;
                    }
                    else
                    {
                        etapaCalificada.EsEtapaActual = true;
                        etapaCalificada.EsContactado = true;
                    }

                    etapaCalificada.IdEstadoEtapaProcesoSeleccion = dto.IdEstadoEA;
                    etapaCalificada.UsuarioModificacion = usuario;
                    etapaCalificada.FechaModificacion = DateTime.Now;

                    EtapaProcesoSeleccionCalificadoActualizarDTO etapaCalificadaV1 = new()
                    {
                        Id = etapaCalificada.Id,
                        IdProcesoSeleccionEtapa = etapaCalificada.IdProcesoSeleccionEtapa,
                        IdPostulante = etapaCalificada.IdPostulante,
                        EsEtapaAprobada = etapaCalificada.EsEtapaAprobada,
                        NotaCalculada = etapaCalificada.NotaCalculada,
                        IdEstadoEtapaProcesoSeleccion = etapaCalificada.IdEstadoEtapaProcesoSeleccion,
                        EsEtapaActual = etapaCalificada.EsEtapaActual,
                        EsContactado = etapaCalificada.EsContactado,
                        UsuarioModificacion = usuario,

                    };
                    //_unitOfWork.EtapaProcesoSeleccionCalificadoRepository.Update(etapaCalificada);
                    _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ActualizarEtapaCalificada(etapaCalificadaV1);
                    //_unitOfWork.Commit();

                    if (dto.IdEstadoEA == 7)// Aprobado con Observaciones
                    {
                        //Obtenemos los examenes rendidos por el proceso y el postulante y luego obtenemos solos los que rinde el postulante
                        var validacionPaseAEvaluador = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerListaEtapaExamenesPorPostulante(dto.IdProcesoSeleccionEA, dto.IdPostulanteEA);
                        var validacionExamenesPostulante = validacionPaseAEvaluador.Where(x => x.EsCalificadoPorPostulante == true).ToList();

                        //Validamos que el resto de examenes sean aprobados
                        bool banderaTodoPostulanteAprobado = true;
                        foreach (var item in validacionExamenesPostulante)
                        {
                            if (!item.EsEtapaAprobada) banderaTodoPostulanteAprobado = false;
                        }

                        //Si todas las etapas estan aprobadas, se coloca el contactado como si
                        if (banderaTodoPostulanteAprobado)
                        {
                            //Actualizamos el campo ES CONTACTADO del resto de examenes de postulante
                            foreach (var item in validacionExamenesPostulante)
                            {
                                var actualizar = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerPorId(item.Id)!;
                                actualizar.EsContactado = true;
                                actualizar.UsuarioModificacion = usuario;
                                actualizar.FechaModificacion = DateTime.Now;
                                EtapaProcesoSeleccionCalificadoActualizarDTO etapaCalificadaV2 = new()
                                {
                                    Id = etapaCalificada.Id,
                                    IdProcesoSeleccionEtapa = etapaCalificada.IdProcesoSeleccionEtapa,
                                    IdPostulante = etapaCalificada.IdPostulante,
                                    EsEtapaAprobada = etapaCalificada.EsEtapaAprobada,
                                    NotaCalculada = etapaCalificada.NotaCalculada,
                                    IdEstadoEtapaProcesoSeleccion = etapaCalificada.IdEstadoEtapaProcesoSeleccion,
                                    EsEtapaActual = etapaCalificada.EsEtapaActual,
                                    EsContactado = etapaCalificada.EsContactado,
                                    UsuarioModificacion = usuario,

                                };
                                //_unitOfWork.EtapaProcesoSeleccionCalificadoRepository.Update(actualizar);
                                _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ActualizarEtapaCalificada(etapaCalificadaV1);
                                //_unitOfWork.Commit();
                            }

                            //Colocamos "En proceso" la evaluación de evaluador que prosigue
                            var nroEtapaMaximoDePostulante = validacionExamenesPostulante.OrderByDescending(x => x.NroOrden).FirstOrDefault();
                            var siguienteEvaluacionEvaluador = validacionPaseAEvaluador.Where(x => x.NroOrden == nroEtapaMaximoDePostulante.NroOrden + 1).FirstOrDefault();
                            var actualizarEvaluador = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerPorId(siguienteEvaluacionEvaluador.Id);
                            if (actualizarEvaluador != null)
                            {
                                actualizarEvaluador.EsEtapaActual = true;
                                actualizarEvaluador.EsContactado = true;
                                actualizarEvaluador.IdEstadoEtapaProcesoSeleccion = 3; // En Proceso
                                actualizarEvaluador.UsuarioModificacion = usuario;
                                actualizarEvaluador.FechaModificacion = DateTime.Now;

                                //_unitOfWork.EtapaProcesoSeleccionCalificadoRepository.Update(actualizarEvaluador);
                                EtapaProcesoSeleccionCalificadoActualizarDTO etapaCalificadaV3 = new()
                                {
                                    Id = etapaCalificada.Id,
                                    IdProcesoSeleccionEtapa = etapaCalificada.IdProcesoSeleccionEtapa,
                                    IdPostulante = etapaCalificada.IdPostulante,
                                    EsEtapaAprobada = etapaCalificada.EsEtapaAprobada,
                                    NotaCalculada = etapaCalificada.NotaCalculada,
                                    IdEstadoEtapaProcesoSeleccion = etapaCalificada.IdEstadoEtapaProcesoSeleccion,
                                    EsEtapaActual = etapaCalificada.EsEtapaActual,
                                    EsContactado = etapaCalificada.EsContactado,
                                    UsuarioModificacion = usuario,

                                };
                                _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ActualizarEtapaCalificada(etapaCalificadaV3);
                                //_unitOfWork.Commit();
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// TipoFuncion: POST
		/// Autor: Flavio R.M.F.
		/// Fecha: 19/06/2024
		/// Versión: 2.0
		/// <summary>
		/// Guarda respuestas realizadas por evaluador y califica etapa de evaluación
		/// </summary>
		/// <returns> Confirmación de inserción : Bool </returns>
		public bool EnviarRespuestasTest(RespuestaEvaluacionEvaluadorDTO RespuestaTest, string usuario)
        {
            try
            {
                var examen = _unitOfWork.ExamenRepository.ObtenerPorId(RespuestaTest.IdExamenEvaluacionEvaluador);
                if (examen == null)
                {
                    throw new BadRequestException("No se encontro el examen");
                }

                var configuracion = _unitOfWork.ConfiguracionAsignacionEvaluacionRepository.ObtenerPorIdProcesoSeleccionIdEvaluacion(RespuestaTest.IdProcesoSeleccionEvaluacionEvaluador, examen.IdExamenTest!.Value)!;

                var etapaAnterior = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerEtapaActualPorIdPostulante(RespuestaTest.IdPostulanteEvaluacionEvaluador);

                if (etapaAnterior != null)
                {
                    etapaAnterior.EsEtapaActual = false;
                    etapaAnterior.UsuarioModificacion = usuario;
                    etapaAnterior.FechaModificacion = DateTime.Now;

                    EtapaProcesoSeleccionCalificadoActualizarDTO etapaCalificadoUpdateV1 = new()
                    {
                        Id = etapaAnterior.Id,
                        IdProcesoSeleccionEtapa = etapaAnterior.IdProcesoSeleccionEtapa,
                        IdPostulante = etapaAnterior.IdPostulante,
                        EsEtapaAprobada = etapaAnterior.EsEtapaAprobada,
                        NotaCalculada = etapaAnterior.NotaCalculada,
                        IdEstadoEtapaProcesoSeleccion = etapaAnterior.IdEstadoEtapaProcesoSeleccion,
                        EsEtapaActual = etapaAnterior.EsEtapaActual,
                        EsContactado = etapaAnterior.EsContactado,
                        UsuarioModificacion = usuario,

                    };

                    _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ActualizarEtapaCalificada(etapaCalificadoUpdateV1);

                }
                var etapaCalificada = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerPorIdPostulanteIdProcesoSeleccionEtapa(RespuestaTest.IdPostulanteEvaluacionEvaluador, configuracion.IdProcesoSeleccionEtapa!.Value);
                if (etapaCalificada != null)
                {
                    var idEtapaCalificadaActual = etapaCalificada.Id;

                    if (RespuestaTest.IdEstadoEvaluacionEvaluador == 2
                        || RespuestaTest.IdEstadoEvaluacionEvaluador == 3
                        || RespuestaTest.IdEstadoEvaluacionEvaluador == 4
                        || RespuestaTest.IdEstadoEvaluacionEvaluador == 9
                    )
                    {
                        etapaCalificada.EsEtapaAprobada = false;
                    }
                    else
                    {
                        etapaCalificada.EsEtapaAprobada = true;
                    }
                    if (RespuestaTest.IdEstadoEvaluacionEvaluador == 9) // ESTADO Sin Rendir
                    {
                        etapaCalificada.EsEtapaActual = false;
                        etapaCalificada.EsContactado = false;
                    }
                    else
                    {
                        etapaCalificada.EsEtapaActual = true;
                        etapaCalificada.EsContactado = true;
                    }
                    etapaCalificada.IdEstadoEtapaProcesoSeleccion = RespuestaTest.IdEstadoEvaluacionEvaluador;
                    etapaCalificada.UsuarioModificacion = usuario;
                    etapaCalificada.FechaModificacion = DateTime.Now;
                    EtapaProcesoSeleccionCalificadoActualizarDTO etapaCalificadoUpdate = new()
                    {
                        Id = etapaCalificada.Id,
                        IdProcesoSeleccionEtapa = etapaCalificada.IdProcesoSeleccionEtapa,
                        IdPostulante = etapaCalificada.IdPostulante,
                        EsEtapaAprobada = etapaCalificada.EsEtapaAprobada,
                        NotaCalculada = etapaCalificada.NotaCalculada,
                        IdEstadoEtapaProcesoSeleccion = etapaCalificada.IdEstadoEtapaProcesoSeleccion,
                        EsEtapaActual = etapaCalificada.EsEtapaActual,
                        EsContactado = etapaCalificada.EsContactado,
                        UsuarioModificacion = usuario,

                    };
                    //_unitOfWork.EtapaProcesoSeleccionCalificadoRepository.Update(etapaCalificada);
                    _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ActualizarEtapaCalificada(etapaCalificadoUpdate);

                    if (etapaCalificada.EsEtapaAprobada)
                    {
                        //Configurar la siguiente evaluación en estado "En Proceso" 
                        var etapaOrden = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerListaEtapaExamenesPorPostulante(RespuestaTest.IdProcesoSeleccionEvaluacionEvaluador, RespuestaTest.IdPostulanteEvaluacionEvaluador);
                        var ordenActual = etapaOrden.Where(x => x.Id == idEtapaCalificadaActual).OrderByDescending(x => x.NroOrden).FirstOrDefault();
                        var ordenPosterior = etapaOrden.Where(x => x.NroOrden == ordenActual.NroOrden + 1).FirstOrDefault();
                        //if (ordenPosterior != null)
                        //{
                        //    var actualizarEtapaPosterior = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerPorId(ordenPosterior.Id);
                        //    if (actualizarEtapaPosterior != null)
                        //    {
                        //        actualizarEtapaPosterior.EsEtapaActual = true;
                        //        actualizarEtapaPosterior.EsContactado = true;
                        //        actualizarEtapaPosterior.IdEstadoEtapaProcesoSeleccion = 3; // En Proceso
                        //        actualizarEtapaPosterior.UsuarioModificacion = usuario;
                        //        actualizarEtapaPosterior.FechaModificacion = DateTime.Now;
                        //        EtapaProcesoSeleccionCalificadoActualizarDTO etapaCalificadoUpdateV2 = new()
                        //        {
                        //            Id = actualizarEtapaPosterior.Id,
                        //            IdProcesoSeleccionEtapa = actualizarEtapaPosterior.IdProcesoSeleccionEtapa,
                        //            IdPostulante = actualizarEtapaPosterior.IdPostulante,
                        //            EsEtapaAprobada = actualizarEtapaPosterior.EsEtapaAprobada,
                        //            NotaCalculada = actualizarEtapaPosterior.NotaCalculada,
                        //            IdEstadoEtapaProcesoSeleccion = actualizarEtapaPosterior.IdEstadoEtapaProcesoSeleccion,
                        //            EsEtapaActual = actualizarEtapaPosterior.EsEtapaActual,
                        //            EsContactado = actualizarEtapaPosterior.EsContactado,
                        //            UsuarioModificacion = usuario,

                        //        };
                        //        //_unitOfWork.EtapaProcesoSeleccionCalificadoRepository.Update(actualizarEtapaPosterior);
                        //        _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ActualizarEtapaCalificada(etapaCalificadoUpdateV2);

                               
                        //    }
                        //}
                    }
                }

                if (RespuestaTest.IdEstadoEvaluacionEvaluador != 3 && RespuestaTest.IdEstadoEvaluacionEvaluador != 4 && RespuestaTest.IdEstadoEvaluacionEvaluador != 9) //En caso de abandono o manualmente colocar la opción sin Rendir o en proceso no se registra las respuestas
                {
                    var examenRealizado = _unitOfWork.ExamenAsignadoEvaluadorRepository.ObtenerPorIdPostulanteIdExamen(RespuestaTest.IdPostulanteEvaluacionEvaluador, examen.Id)!;
                    if (examenRealizado.EstadoExamen == false)
                    {
                        var agrupado = RespuestaTest.ListaRespuestasEvaluador.GroupBy(x => x.IdExamenAsignado).Select(x => new RespuestaTestDTO
                        {
                            IdExamenAsignado = x.Key,
                            ListaRespuestas = x.GroupBy(y => new { y.IdExamen, y.IdPregunta, y.IdRespuesta, y.TextoRespuesta, y.Flag }).Select(y => new RespuestaTestAgrupadaDTO
                            {
                                IdExamen = y.Key.IdExamen,
                                IdPregunta = y.Key.IdPregunta,
                                IdRespuesta = y.Key.IdRespuesta,
                                TextoRespuesta = y.Key.TextoRespuesta,
                                Flag = y.Key.Flag
                            }).ToList()
                        }).ToList();

                        foreach (var item in agrupado)
                        {
                            var registro = _unitOfWork.ExamenAsignadoEvaluadorRepository.ObtenerPorId(item.IdExamenAsignado)!;
                            foreach (var respuesta in item.ListaRespuestas)
                            {
                                if (respuesta.Flag)
                                {
                                    ExamenRealizadoRespuestaEvaluador examenRealizadoRespuesta = new()
                                    {
                                        IdExamenAsignadoEvaluador = item.IdExamenAsignado,
                                        IdPregunta = respuesta.IdPregunta,
                                        IdRespuestaPregunta = respuesta.IdRespuesta,
                                        TextoRespuesta = respuesta.TextoRespuesta,
                                        Estado = true,
                                        UsuarioCreacion = usuario,
                                        UsuarioModificacion = usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now
                                    };
                                    _unitOfWork.ExamenRealizadoRespuestaEvaluadorRepository.Add(examenRealizadoRespuesta);
                                    _unitOfWork.Commit();
                                }
                            }
                            registro.EstadoExamen = true;
                            registro.UsuarioModificacion = usuario;
                            registro.FechaModificacion = DateTime.Now;
                            _unitOfWork.ExamenAsignadoEvaluadorRepository.Update(registro);
                            _unitOfWork.Commit();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ReportePostulanteMatriculaDTO> ObtenerNotasMatriculaReporte(List<int> idsPostulantes)
        {
            try
            {
                return _unitOfWork.PostulanteRepository.ObtenerNotasMatriculaReporte(idsPostulantes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool RestablecerNotas(EnvioDatosReestablecerDTO dto, string usuario)
        {
            try
            {
                var resultado = _unitOfWork.PostulanteRepository.RestablecerNotas(dto, usuario);
                var idPostulanteProcesoSeleccion = _unitOfWork.PostulanteRepository.ObtenerIdPostulanteProcesoSeleccionPorIdPostulante(dto.IdPostulante);
                if (idPostulanteProcesoSeleccion == null || idPostulanteProcesoSeleccion == 0)
                {
                    throw new BadRequestException("No se encontro el proceso de seleccion del postulante");
                }

                var personalUsuario = _unitOfWork.IntegraAspNetUserRepository.ObtenerIdentidadUsusarioV2(usuario);
                var personal = _unitOfWork.PersonalRepository.ObtenerPorId(personalUsuario.Id);
                if (personal == null)
                {
                    throw new BadRequestException("No se encontro el personal");
                }

                var ultimoRegistro = _unitOfWork.PostulanteProcesoSeleccionRepository.VerificacionTokenPresenteInactivo(dto.IdPostulante);

                var postulante = _unitOfWork.PostulanteRepository.ObtenerPorId(dto.IdPostulante);
                var datosPostulanteLogin = _unitOfWork.PostulanteRepository.ObtenerDatosMatriculaIdPostulante(dto.IdPostulante);

                IReemplazoEtiquetaPlantillaService _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                var emailCalculado = _reemplazoEtiquetaPlantilla.ReemplazarEtiquetasPostulanteCursoAsesorCapacitacion(1818, idPostulanteProcesoSeleccion!.Value);
                List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        personal.Email
                    };

                List<string> correosPersonalizados = new List<string>{
                            datosPostulanteLogin.Email.Trim()
                    };
                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = personal.Email,
                    Recipient = string.Join(",", correosPersonalizados.Distinct()),
                    Subject = emailCalculado.Asunto,
                    Message = emailCalculado.CuerpoHTML,
                    Cc = "",
                    Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                    AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                };
                var mailServie = new TMK_MailService();
                mailServie.SetData(mailDataPersonalizado);
                mailServie.SendMessageTask();

                var gmailCorreo = new GmailCorreo
                {
                    IdEtiqueta = 1,//sent:1 , inbox:2
                    Asunto = emailCalculado.Asunto,
                    Fecha = DateTime.Now,
                    EmailBody = emailCalculado.CuerpoHTML,
                    Seen = false,
                    Remitente = personal.Email,
                    Cc = "",
                    Bcc = "",
                    Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
                    IdPersonal = personal.Id,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "SYSTEM",
                    UsuarioModificacion = "SYSTEM",
                    IdClasificacionPersona = 5
                };
                _unitOfWork.GmailCorreoRepository.Add(gmailCorreo);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 21/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Estados por etapa de Postulante
        /// </summary>
        /// <returns> Reporte de Estados, etapas y puntaje de postulantes </returns>
        /// <returns>   Objeto Agrupado  </returns>
        public List<ReportePruebaDTO> GenerarReporteIntegra(EvaluacionPostulanteFiltroReporteDTO filtroReporte)
        {
            try
            {
                var postulanteProceso = _unitOfWork.PostulanteRepository.ObtenerPostulantesUltimoProcesoSeleccion(filtroReporte);
                if (postulanteProceso == null || postulanteProceso.Count() == 0)
                {
                    return new List<ReportePruebaDTO>();
                }

                if (filtroReporte.FiltroPorPostulante == false)
                {
                    if (filtroReporte.IdsEstadoEtapa.Count() == 0 && filtroReporte.IdsProcesoEtapa.Count() == 0)
                    {
                        if (filtroReporte.IdGrupoComparacion != null && filtroReporte.IdGrupoComparacion > 0)
                        {
                            if (filtroReporte.IdProcesoSeleccion != null)
                            {
                                var idsPostulantes = _unitOfWork.ExamenAsignadoRepository.ObtenerIdsPostulantesPorIdProcesoSeleccion(filtroReporte.IdProcesoSeleccion.Value);
                                filtroReporte.IdProcesoSeleccion = null;
                                filtroReporte.IdsPostulantes = idsPostulantes;
                            }
                        }
                    }
                    else
                    {
                        var resultado = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerIdsPostulanteEtapaProcesoSeleccionActual(filtroReporte);
                        if (resultado.Count() == 0)
                        {
                            throw new ConflictException("No se encontraron postulantes");
                        }
                        filtroReporte.IdsPostulantes = resultado;
                        postulanteProceso = postulanteProceso.Where(x => filtroReporte.IdsPostulantes.Contains(x.IdPostulante)).ToList();
                    }

                    if (filtroReporte.FechaInicio == null)
                    {
                        filtroReporte.FechaInicio = new DateTime(1900, 12, 31);
                    }
                    if (filtroReporte.FechaFin == null)
                    {
                        filtroReporte.FechaFin = DateTime.Now;
                    }
                }
                else
                {
                    filtroReporte.FechaFin = DateTime.Now;
                    filtroReporte.FechaInicio = new DateTime(1900, 12, 31);
                }
                filtroReporte.IdsPostulantes = postulanteProceso.Select(x => x.IdPostulante).ToList();

                if (filtroReporte.IdGrupoComparacion != null && filtroReporte.IdGrupoComparacion != 0)
                {
                    filtroReporte.idsPostulanteGrupoComparacion = _unitOfWork.PostulanteComparacionRepository.ObtenerIdsPostulantesPorIdGrupoComparacion(filtroReporte.IdGrupoComparacion.Value);
                    filtroReporte.IdsPostulantes.AddRange(filtroReporte.idsPostulanteGrupoComparacion);
                    filtroReporte.IdsPostulantes = filtroReporte.IdsPostulantes.Distinct().ToList();
                }

                // de la lita de postulantes que hayan pasado el filtro se agrupan por proceso de seleccion los postulantes.
                var listaProcesoSeleccionAgrupado = (from p in postulanteProceso
                                                     group p by new { p.IdProcesoSeleccion, p.ProcesoSeleccion } into grupo
                                                     select new { grupo.Key.IdProcesoSeleccion, grupo.Key.ProcesoSeleccion }).ToList();

                List<ReportePruebaDetalleDTO> listaEtapas = new List<ReportePruebaDetalleDTO>();
                List<ReportePruebaDTO> listaEtapasFinal = new List<ReportePruebaDTO>();

                //Se busca todas las etapas segun el proceso de seleccion y se coloca por defecto el estado SIN RENDIR ID 9
                foreach (var item in listaProcesoSeleccionAgrupado)
                {
                    var etapasProcesoSeleccion = _unitOfWork.ProcesoSeleccionEtapaRepository.ObtenerEtapaPorIdProcesoSeleccion(item.IdProcesoSeleccion);

                    listaEtapas = listaEtapas.Concat(etapasProcesoSeleccion.Select(x => new ReportePruebaDetalleDTO
                    {
                        IdProcesoSeleccion = item.IdProcesoSeleccion,
                        ProcesoSeleccion = item.ProcesoSeleccion,
                        IdEtapa = x.Id,
                        Etapa = x.Nombre,
                        EstadoEtapa = 0,
                        IdEstadoEtapaProceso = 9,
                        NroOrden = x.NroOrden,
                        EtapaContactado = false
                    }
                    ).ToList()).ToList();
                }

                List<EtapaCalificadaPostulanteProcesoSeleccionDTO> listaEtapasOptimizacion = new List<EtapaCalificadaPostulanteProcesoSeleccionDTO>();
                if (filtroReporte.IdProcesoSeleccion != null)
                {
                    if (postulanteProceso.Count() == 0)
                    {
                        return new List<ReportePruebaDTO>();
                    }
                    else
                    {
                        List<int> idsProcesoSeleccion = new List<int>
                        {
                            filtroReporte.IdProcesoSeleccion!.Value
                        };
                        listaEtapasOptimizacion = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerPorIdsPostulanteIdsProcesoSeleccion(postulanteProceso.Select(x => x.IdPostulante).ToList(), idsProcesoSeleccion);
                    }
                }
                else
                {
                    List<int> idsProcesoSeleccion = listaProcesoSeleccionAgrupado.Select(x => x.IdProcesoSeleccion).Distinct().ToList();
                    if (idsProcesoSeleccion.Count() == 0)
                    {
                        idsProcesoSeleccion = listaProcesoSeleccionAgrupado.Select(x => x.IdProcesoSeleccion).Distinct().ToList();
                    }
                    listaEtapasOptimizacion = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerPorIdsPostulanteIdsProcesoSeleccion(filtroReporte.IdsPostulantes, idsProcesoSeleccion);
                }

                List<ReportePruebaDetalleDTO> etapasList;
                ReportePruebaDetalleDTO itemEtapa;
                //Se recorre la lista de los postulantes que cumplen el filtro y cada postulante se le asigna sus etapas correspondientes segun el proceso de seleccion que se encuentre
                foreach (var item in postulanteProceso)
                {
                    etapasList = new List<ReportePruebaDetalleDTO>();
                    etapasList = listaEtapas.Where(x => x.IdProcesoSeleccion == item.IdProcesoSeleccion).OrderBy(x => x.NroOrden).ToList();
                    ReportePruebaDTO obj = new ReportePruebaDTO();
                    obj.IdPostulante = item.IdPostulante;
                    obj.Postulante = item.Postulante;
                    obj.Etapas = new List<ReportePruebaDetalleDTO>();

                    foreach (var item2 in etapasList)
                    {
                        itemEtapa = new ReportePruebaDetalleDTO();
                        var etapaCalificada = listaEtapasOptimizacion.Where(x => x.IdPostulante == item.IdPostulante && x.IdProcesoSeleccionEtapa == item2.IdEtapa).FirstOrDefault();

                        itemEtapa.IdEtapa = item2.IdEtapa;
                        itemEtapa.Etapa = item2.Etapa;
                        itemEtapa.IdProcesoSeleccion = item2.IdProcesoSeleccion;
                        itemEtapa.ProcesoSeleccion = item2.ProcesoSeleccion;
                        itemEtapa.EstadoEtapa = item2.EstadoEtapa;
                        itemEtapa.IdEstadoEtapaProceso = item2.IdEstadoEtapaProceso;
                        itemEtapa.NroOrden = item2.NroOrden;
                        itemEtapa.EtapaContactado = item2.EtapaContactado;
                        itemEtapa.EsCalificadoPorPostulante = item2.EsCalificadoPorPostulante;

                        if (etapaCalificada != null) // Si tiene una calificacion en la tabla gp.T_EtapaPRocesoSeleccionCalificado reemplaza lo datos, en caso no exista coloca los datos por defecto
                        {
                            itemEtapa.EstadoEtapa = etapaCalificada.EsEtapaAprobada == true ? 1 : 0;
                            itemEtapa.IdEstadoEtapaProceso = etapaCalificada.IdEstadoEtapaProcesoSeleccion;
                            itemEtapa.EtapaContactado = etapaCalificada.EsContactado == true ? true : false;
                            itemEtapa.EsCalificadoPorPostulante = etapaCalificada.EsCalificadoPorPostulante == true ? true : false;
                        }
                        obj.Etapas.Add(itemEtapa);
                    }
                    listaEtapasFinal.Add(obj);
                }
                if (listaEtapasFinal.Count > 0)
                {
                    listaEtapasFinal = listaEtapasFinal.OrderBy(x => x.Postulante).ToList();
                }
                return listaEtapasFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto.
        /// Fecha: 21/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Registra Accesos, envia  correo a postulante para inserción en aula virtual
        /// </summary>
        /// <param name="dto"> Id de Postulante, Id de Examen y Usuario </param>
        /// <returns> Retorna StatusCodes, 200 si se eliminó existasamente con Bool de confirmación y Mensaje a Interfaz </returns>
        public (bool Respuesta, string Mensaje) EnviarAccesoAulaVirtualPostulante(EnviarAccesoPostulanteDTO dto, string usuario)
        {
            try
            {
                string mensaje = "";

                var examenAsignado = _unitOfWork.ExamenAsignadoRepository.ObtenerPorIdPostulanteIdExamen(dto.IdPostulante, dto.IdExamen);
                if (examenAsignado == null)
                {
                    throw new BadRequestException("No se encontro el examen asignado");
                }
                if (examenAsignado.EstadoAcceso == true)
                {
                    mensaje = "El postulante ya cuenta o contó con accesos para este componente";
                    return (false, mensaje);
                }
                if (dto.IdPostulante > 0 && dto.IdExamen > 0 && (usuario).Trim().Length > 0 && dto.IdPlantilla > 0)
                {
                    IPostulanteAccesoTemporalAulaVirtualService postulanteAccesoTemporalAulaVirtualService = new PostulanteAccesoTemporalAulaVirtualService(_unitOfWork);
                    var validacionAcceso = postulanteAccesoTemporalAulaVirtualService.CrearAccesosTemporalesPostulante(dto, usuario);
                    if (!validacionAcceso.ValidacionRespuesta)
                    {
                        throw new BadRequestException("Hubo un fallo en la actualizacion de los accesos temporales");
                    }
                    else
                    {
                        var examen = _unitOfWork.ExamenRepository.ObtenerPorId(dto.IdExamen)!;
                        var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(examen.IdCentroCosto!.Value);
                        var accesoTemporal = _unitOfWork.PostulanteAccesoTemporalAulaVirtualRepository.ObtenerPorIdPostulantePespecificoHijo(dto.IdPostulante, pEspecifico.Id);
                        var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(validacionAcceso.IdAlumno.GetValueOrDefault());
                        if (pEspecifico != null && examen != null && accesoTemporal != null && alumno != null)
                        {
                            IReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                            //{
                            //    IdPlantilla = dto.IdPlantilla,
                            //};
                            var emailPersonal = _unitOfWork.IntegraAspNetUserRepository.ObtenerEmailPorNombreUsuario(usuario);

                            var datosMensaje = reemplazoEtiquetaPlantilla.ReemplazarEtiquetasAccesosTemporalesPostulante(dto.IdPlantilla, validacionAcceso, pEspecifico.Id, accesoTemporal.FechaInicio, accesoTemporal.FechaFin, emailPersonal);
                            /*Prepara datos para envío de correo*/
                            List<string> correosPersonalizadosCopiaOculta = new List<string>
                            {
                                emailPersonal,
                            };
                            List<string> correosPersonalizados = new List<string>
                            {
                                alumno.Email1,
                            };
                            TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                            {
                                Sender = emailPersonal,
                                Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                Subject = datosMensaje.Asunto,
                                Message = datosMensaje.CuerpoHTML,
                                Cc = "",
                                Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                            };
                            var mailServie = new TMK_MailService();
                            mailServie.SetData(mailDataPersonalizado);
                            mailServie.SendMessageTask();
                            /*Se actualiza la información de Postulante*/
                            var examenAsignadoActualizar = _unitOfWork.ExamenAsignadoRepository.ObtenerPorIdPostulanteIdExamen(dto.IdPostulante, dto.IdExamen);
                            if (examenAsignadoActualizar != null)
                            {
                                examenAsignadoActualizar.EstadoAcceso = true;
                                examenAsignadoActualizar.UsuarioModificacion = usuario;
                                examenAsignadoActualizar.FechaModificacion = DateTime.Now;
                                try
                                {
                                    _unitOfWork.ExamenAsignadoRepository.Update(examenAsignadoActualizar);
                                    _unitOfWork.Commit();
                                    mensaje = "Se enviaron accesos y correo a postulante";
                                    return (true, mensaje);
                                }
                                catch (Exception ex)
                                {
                                    mensaje = "No se pudo actualizar la información de Acceso de Postulante";
                                    return (false, mensaje);
                                }
                            }
                            else
                            {
                                mensaje = "No se pudo actualizar la información de Acceso de Postulante";
                                return (false, mensaje);
                            }
                        }
                        else
                        {
                            mensaje = "Falta configuración de curso o accesos de postulante";
                            return (false, mensaje);
                        }
                    }
                }
                else
                {
                    mensaje = "Falta Información para continuar con la función";
                    return (false, mensaje);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
