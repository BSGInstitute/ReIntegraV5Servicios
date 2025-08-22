using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: EstadoMatriculaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 21/06/2022
    /// <summary>
    /// Gestión general de T_EstadoMatricula
    /// </summary>
    public class EstadoMatriculaService : IEstadoMatriculaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EstadoMatriculaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TMonedum, EstadoMatricula>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public EstadoMatricula Add(EstadoMatricula entidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoMatriculaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EstadoMatricula>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EstadoMatricula Update(EstadoMatricula entidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoMatriculaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EstadoMatricula>(modelo);
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
                _unitOfWork.EstadoMatriculaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoMatricula> Add(List<EstadoMatricula> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoMatriculaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EstadoMatricula>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoMatricula> Update(List<EstadoMatricula> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoMatriculaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EstadoMatricula>>(modelo);
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
                _unitOfWork.EstadoMatriculaRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 21/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EstadoMatricula
        /// </summary>
        /// <returns> List<EstadoMatriculaDTO> </returns>
        public IEnumerable<EstadoMatriculaDTO> ObtenerEstadoMatricula()
        {
            try
            {
                return _unitOfWork.EstadoMatriculaRepository.ObtenerEstadoMatricula();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_EstadoMatricula para mostrarse en combo.
        /// </summary>
        /// <returns> List<EstadoMatriculaComboDTO> </returns>
        public IEnumerable<EstadoMatriculaComboDTO> ObtenerEstadoMatriculaCombo()
        {
            try
            {
                return _unitOfWork.EstadoMatriculaRepository.ObtenerEstadoMatriculaCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_EstadoMatricula asociados a una Matricula Activa para mostrarse en combo.
        /// </summary>
        /// <returns> List<EstadoMatriculaComboDTO> </returns>
        public IEnumerable<EstadoMatriculaComboDTO> ObtenerEstadoMatriculaParaMatriculados()
        {
            try
            {
                return _unitOfWork.EstadoMatriculaRepository.ObtenerEstadoMatriculaParaMatriculados();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Estado Matriculado de Alumno por Id
        /// </summary>
        /// <param name="idAlumno">Id del alumno</param>
        /// <returns> Estado Matriculado de Alumno </returns>
        /// <returns> Lista de Objeto DTO : List<EstadoMatriculadoDTO> </returns>
        public List<EstadoMatriculadoDTO> ObtenerEstadoMatriculado(int idAlumno)
        {
            try
            {
                var servicioCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalService(_unitOfWork);
                var servicioPespecifico = new PEspecificoService(_unitOfWork);
                var servicioMatriculaCabecera = new MatriculaCabeceraService(_unitOfWork);
                var estados = new List<EstadoMatriculadoDTO>();
                var estadoIndividual = new EstadoMatriculadoDTO();

                estados = servicioMatriculaCabecera.EstadoMatriculadoPorAlumno(idAlumno);
                if (estados.Count() == 0)
                {
                    var matriculas = servicioMatriculaCabecera.MatriculaPorAlumno(idAlumno);
                    estados = new List<EstadoMatriculadoDTO>();
                    foreach (var matricula in matriculas)
                    {
                        estadoIndividual = servicioMatriculaCabecera.EstadoEvaluacionPorCodMatricula(matricula.CodigoMatricula);
                        estadoIndividual.IdMatriculaCabecera = matricula.Id;
                        estadoIndividual.CodigoMatricula = matricula.CodigoMatricula;
                        estadoIndividual.NroCuota = 0;
                        estadoIndividual.NroSubCuota = 0;
                        estadoIndividual.VersionPrograma = matricula.VersionPrograma;
                        estadoIndividual.IdCentroCosto = matricula.IdCentroCosto ?? default(int);
                        estadoIndividual.Documentos = matricula.Documentos;
                        estadoIndividual.NombreProgramaGeneral = matricula.NombreProgramaGeneral;
                        estadoIndividual.EstadoFinanciero = $@" <strong> Pago Completo </strong>";

                        var resultado = _unitOfWork.MoodleCronogramaEvaluacionRepository.ObtenerCronogramaAutoEvaluacionUltimaVersion(estadoIndividual.IdMatriculaCabecera);
                        if (resultado.Count > 0)
                        {
                            string estadoEvaluacion = "";
                            var ultimaEvaluacionRealizadas = resultado.Where(w => w.Nota != null && w.FechaRendicion != null).OrderByDescending(w => w.FechaCronograma).FirstOrDefault();
                            var CantidadEvaluaciones = resultado.Where(w => w.Nota == null && w.FechaRendicion == null).OrderBy(w => w.FechaCronograma).ToList();
                            if (CantidadEvaluaciones.Count > 0)
                            {
                                if (CantidadEvaluaciones.FirstOrDefault().FechaCronograma >= DateTime.Now)
                                {
                                    if (ultimaEvaluacionRealizadas != null)
                                    {
                                        if (ultimaEvaluacionRealizadas.FechaCronograma > DateTime.Now)
                                        {
                                            estadoEvaluacion = $@" <strong>Evaluacion Adelantado </strong>
                                                <ul>                                                     
                                                ";
                                        }
                                        else
                                        {
                                            estadoEvaluacion = $@" <strong>Evaluacion Al Dia </strong>
                                                <ul>                                                     
                                                ";
                                        }
                                    }
                                    else
                                    {
                                        estadoEvaluacion = $@" <strong>Evaluacion Al Dia </strong>
                                                <ul>                                                     
                                                ";
                                    }
                                }
                                else
                                {
                                    estadoEvaluacion = $@" <strong>Evaluacion Atrasado </strong>
                                                <ul>                                                     
                                                ";
                                }
                                foreach (var Evaluacion in CantidadEvaluaciones)
                                {
                                    var fecha = Evaluacion.FechaCronograma == null ? null : Evaluacion.FechaCronograma.Value.ToString("dd/M/yyyy");
                                    estadoEvaluacion = estadoEvaluacion + $@"<li> {Evaluacion.NombreEvaluacion} : {fecha} </li>";
                                }
                                estadoEvaluacion = estadoEvaluacion + "</ul>";
                                estadoIndividual.EstadoEvaluacion = estadoEvaluacion;
                            }
                            else
                            {
                                estadoIndividual.EstadoEvaluacion = $@" <strong>Evaluacion Finalizada </strong>";
                            }
                        }
                        else
                        {
                            estadoIndividual.EstadoEvaluacion = $@" <strong>Evaluacion Sin definir </strong>";
                        }
                        estados.Add(estadoIndividual);
                    }
                }
                else
                {
                    var matriculas = servicioMatriculaCabecera.MatriculaPorAlumno(idAlumno);
                    foreach (var matriculaAlumno in matriculas)
                    {
                        var culminado = estados.Where(w => w.CodigoMatricula == matriculaAlumno.CodigoMatricula);
                        if (culminado.Count() == 0)
                        {
                            estadoIndividual = servicioMatriculaCabecera.EstadoEvaluacionPorCodMatricula(matriculaAlumno.CodigoMatricula);
                            estadoIndividual.IdMatriculaCabecera = matriculaAlumno.Id;
                            estadoIndividual.CodigoMatricula = matriculaAlumno.CodigoMatricula;
                            estadoIndividual.NroCuota = 0;
                            estadoIndividual.NroSubCuota = 0;
                            estadoIndividual.VersionPrograma = matriculaAlumno.VersionPrograma;
                            estadoIndividual.IdCentroCosto = matriculaAlumno.IdCentroCosto ?? default(int);
                            estadoIndividual.Documentos = matriculaAlumno.Documentos;
                            estadoIndividual.NombreProgramaGeneral = matriculaAlumno.NombreProgramaGeneral;
                            estadoIndividual.EstadoFinanciero = $@" <strong> Pago Completo </strong>";

                            var resultado = _unitOfWork.MoodleCronogramaEvaluacionRepository.ObtenerCronogramaAutoEvaluacionUltimaVersion(estadoIndividual.IdMatriculaCabecera);
                            if (resultado.Count > 0)
                            {
                                string estadoEvaluacion = "";
                                var ultimaEvaluacionRealizadas = resultado.Where(w => w.Nota != null && w.FechaRendicion != null).OrderByDescending(w => w.FechaCronograma).FirstOrDefault();
                                var CantidadEvaluaciones = resultado.Where(w => w.Nota == null && w.FechaRendicion == null).OrderBy(w => w.FechaCronograma).ToList();
                                if (CantidadEvaluaciones.Count > 0)
                                {
                                    if (CantidadEvaluaciones.FirstOrDefault().FechaCronograma >= DateTime.Now)
                                    {
                                        if (ultimaEvaluacionRealizadas != null)
                                        {
                                            if (ultimaEvaluacionRealizadas.FechaCronograma > DateTime.Now)
                                            {
                                                estadoEvaluacion = $@" <strong>Evaluacion Adelantado </strong>
                                                <ul>                                                     
                                                ";
                                            }
                                            else
                                            {
                                                estadoEvaluacion = $@" <strong>Evaluacion Al Dia </strong>
                                                <ul>                                                     
                                                ";
                                            }
                                        }
                                        else
                                        {
                                            estadoEvaluacion = $@" <strong>Evaluacion Al Dia </strong>
                                                <ul>                                                     
                                                ";
                                        }
                                    }
                                    else
                                    {
                                        estadoEvaluacion = $@" <strong>Evaluacion Atrasado </strong>
                                                <ul>                                                     
                                                ";
                                    }
                                    foreach (var Evaluacion in CantidadEvaluaciones)
                                    {
                                        var fecha = Evaluacion.FechaCronograma == null ? null : Evaluacion.FechaCronograma.Value.ToString("dd/M/yyyy");
                                        estadoEvaluacion = estadoEvaluacion + $@"<li> {Evaluacion.NombreEvaluacion} : {fecha} </li>";
                                    }
                                    estadoEvaluacion = estadoEvaluacion + "</ul>";
                                    estadoIndividual.EstadoEvaluacion = estadoEvaluacion;
                                }
                                else
                                {
                                    estadoIndividual.EstadoEvaluacion = $@" <strong>Evaluacion Finalizada </strong>";
                                }
                            }
                            else
                            {
                                estadoIndividual.EstadoEvaluacion = $@" <strong>Evaluacion Sin definir </strong>";
                            }
                            estados.Add(estadoIndividual);
                        }
                        else
                        {
                            foreach (var item in estados)
                            {
                                if (item.EstadoFinanciero == "Cuota Vencida")
                                {
                                    string estadoFinanciero = "";
                                    var cuotasRestantes = servicioCronogramaPagoDetalleFinal.ObtenerCronogramaFinanzasPorVersionYMCabecera(item.Version, item.IdMatriculaCabecera);
                                    cuotasRestantes = cuotasRestantes.Where(w => w.IdMatriculaCabecera == item.IdMatriculaCabecera && w.NroCuota >= item.NroCuota && w.Version == item.Version).OrderBy(w => w.NroCuota).ToList();
                                    //var cuotasRestantes = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == item.IdMatriculaCabecera && w.NroCuota >= item.NroCuota && w.Cancelado==false && w.Version == item.Version).OrderBy(w => w.NroCuota).ToList();
                                    if (cuotasRestantes.Count() == 1)
                                    {
                                        estadoFinanciero = $@" <strong>Cuota Vencida :</strong> 
                                                <ul> 
                                                    <li> {item.TipoCuota} {item.NroCuota} : {item.FechaVencimiento.ToString("dd/M/yyyy")} </li>
                                                </ul>
                                                ";
                                        item.EstadoFinanciero = estadoFinanciero;
                                    }
                                    else
                                    {
                                        estadoFinanciero = $@" <strong>Cuotas Vencida :</strong> 
                                                <ul>                                                     
                                                ";
                                        foreach (var cuota in cuotasRestantes)
                                        {
                                            estadoFinanciero = estadoFinanciero + $@"<li> {cuota.TipoCuota} {cuota.NroCuota} : {cuota.FechaVencimiento.Value.ToString("dd/M/yyyy")} </li>";
                                        }
                                        estadoFinanciero = estadoFinanciero + "</ul>";
                                        item.EstadoFinanciero = estadoFinanciero;
                                    }
                                }
                                else if (item.EstadoFinanciero == "Cuota Adelantada")
                                {
                                    string estadoFinanciero = "";
                                    var cuotasRestantes = servicioCronogramaPagoDetalleFinal.ObtenerCronogramaFinanzasPorVersionYMCabecera(item.Version, item.IdMatriculaCabecera);
                                    cuotasRestantes = cuotasRestantes.Where(w => w.IdMatriculaCabecera == item.IdMatriculaCabecera && w.NroCuota >= item.NroCuota && w.Version == item.Version).OrderBy(w => w.NroCuota).ToList();
                                    //var cuotasRestantes = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == item.IdMatriculaCabecera && w.NroCuota >= item.NroCuota && w.Cancelado == false && w.Version == item.Version).OrderBy(w => w.NroCuota);
                                    if (cuotasRestantes.Count() == 1)
                                    {
                                        estadoFinanciero = $@" <strong>Cuota Adelantada :</strong> 
                                                <ul> 
                                                    <li> {item.TipoCuota} {item.NroCuota} : {item.FechaVencimiento.ToString("dd/M/yyyy")} </li>
                                                </ul>
                                                ";
                                        item.EstadoFinanciero = estadoFinanciero;
                                    }
                                    else
                                    {
                                        estadoFinanciero = $@" <strong>Cuotas Adelantada :</strong> 
                                                <ul>                                                     
                                                ";
                                        foreach (var cuota in cuotasRestantes)
                                        {
                                            estadoFinanciero = estadoFinanciero + $@"<li> {cuota.TipoCuota} {cuota.NroCuota} : {cuota.FechaVencimiento.Value.ToString("dd/M/yyyy")} </li>";
                                        }
                                        estadoFinanciero = estadoFinanciero + "</ul>";
                                        item.EstadoFinanciero = estadoFinanciero;
                                    }
                                }
                                else if ((item.EstadoFinanciero == "Cuota Al Dia"))
                                {
                                    string estadoFinanciero = "";
                                    var cuotasRestantes = servicioCronogramaPagoDetalleFinal.ObtenerCronogramaFinanzasPorVersionYMCabecera(item.Version, item.IdMatriculaCabecera);
                                    cuotasRestantes = cuotasRestantes.Where(w => w.IdMatriculaCabecera == item.IdMatriculaCabecera && w.NroCuota >= item.NroCuota && w.Version == item.Version).OrderBy(w => w.NroCuota).ToList();
                                    //var cuotasRestantes = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == item.IdMatriculaCabecera && w.NroCuota >= item.NroCuota && w.Version == item.Version).OrderBy(w => w.NroCuota).ToList();
                                    if (cuotasRestantes.Count() == 1)
                                    {
                                        estadoFinanciero = $@" <strong>Cuota Al Dia :</strong> 
                                                <ul> 
                                                    <li> {item.TipoCuota} {item.NroCuota} : {item.FechaVencimiento.ToString("dd/M/yyyy")} </li>
                                                </ul>
                                                ";
                                        item.EstadoFinanciero = estadoFinanciero;
                                    }
                                    else
                                    {
                                        estadoFinanciero = $@" <strong>Cuota Al Dia :</strong> 
                                                <ul>                                                     
                                                ";
                                        foreach (var cuota in cuotasRestantes)
                                        {
                                            estadoFinanciero = estadoFinanciero + $@"<li> {cuota.TipoCuota} {cuota.NroCuota} : {cuota.FechaVencimiento.Value.ToString("dd/M/yyyy")} </li>";
                                        }
                                        estadoFinanciero = estadoFinanciero + "</ul>";
                                        item.EstadoFinanciero = estadoFinanciero;
                                    }
                                }
                                else
                                {
                                    item.EstadoFinanciero = $@" <strong>{item.EstadoFinanciero}</strong>";

                                }
                                var resultado = _unitOfWork.MoodleCronogramaEvaluacionRepository.ObtenerCronogramaAutoEvaluacionUltimaVersion(item.IdMatriculaCabecera);
                                if (resultado.Count > 0)
                                {
                                    string estadoEvaluacion = "";
                                    var ultimaEvaluacionRealizadas = resultado.Where(w => w.Nota != null && w.FechaRendicion != null).OrderByDescending(w => w.FechaCronograma).FirstOrDefault();
                                    var CantidadEvaluaciones = resultado.Where(w => w.Nota == null && w.FechaRendicion == null).OrderBy(w => w.FechaCronograma).ToList();
                                    if (CantidadEvaluaciones.Count > 0)
                                    {
                                        if (CantidadEvaluaciones.FirstOrDefault().FechaCronograma >= DateTime.Now)
                                        {
                                            if (ultimaEvaluacionRealizadas != null)
                                            {
                                                if (ultimaEvaluacionRealizadas.FechaCronograma > DateTime.Now)
                                                {
                                                    estadoEvaluacion = $@" <strong>Evaluacion Adelantado </strong>
                                                <ul>                                                     
                                                ";
                                                }
                                                else
                                                {
                                                    estadoEvaluacion = $@" <strong>Evaluacion Al Dia </strong>
                                                <ul>                                                     
                                                ";
                                                }
                                            }
                                            else
                                            {
                                                estadoEvaluacion = $@" <strong>Evaluacion Al Dia </strong>
                                                <ul>                                                     
                                                ";
                                            }
                                        }
                                        else
                                        {
                                            estadoEvaluacion = $@" <strong>Evaluacion Atrasado </strong>
                                                <ul>                                                     
                                                ";
                                        }
                                        foreach (var Evaluacion in CantidadEvaluaciones)
                                        {
                                            var fecha = Evaluacion.FechaCronograma == null ? null : Evaluacion.FechaCronograma.Value.ToString("dd/M/yyyy");
                                            estadoEvaluacion = estadoEvaluacion + $@"<li> {Evaluacion.NombreEvaluacion} : {fecha} </li>";
                                        }
                                        estadoEvaluacion = estadoEvaluacion + "</ul>";
                                        item.EstadoEvaluacion = estadoEvaluacion;
                                    }
                                    else
                                    {
                                        item.EstadoEvaluacion = $@" <strong>Evaluacion Finalizada </strong>";
                                    }
                                }
                                else
                                {
                                    item.EstadoEvaluacion = $@" <strong>Evaluacion Sin definir </strong>";
                                }
                            }
                        }
                    }
                }
                return (estados);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        ///// Autor: Joseph Llanque
        ///// Fecha: 27/02/2024
        ///// Versión: 1.0
        ///// <summary>
        ///// Obtiene Estado Matriculado de Alumno por Id
        ///// </summary>
        ///// <param name="idAlumno">Id del alumno</param>
        ///// <returns> Estado Matriculado de Alumno </returns>
        ///// <returns> Lista de Objeto DTO : List<EstadoMatriculadoDTO> </returns>
        public List<MatriculaAlumnoDTO> ObtenerMatriculaAlumno(int idAlumno)
        {
            try
            {
                var estados = new List<MatriculaAlumnoDTO>();
                var servicioMatriculaCabecera = new MatriculaCabeceraService(_unitOfWork);
                var estadoIndividual = new MatriculaAlumnoDTO();

                estados = servicioMatriculaCabecera.ObtenerMatriculaAlumno(idAlumno);
                if (estados.Count() == 0)
                {
                    var matriculas = servicioMatriculaCabecera.MatriculaPorAlumno(idAlumno);
                }
                return (estados);
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
        /// Obtiene registros de la tabla T_EstadoMatricula para mostrarse en combo.
        /// </summary>
        /// <returns> List<FiltroConfiguracionCoordinadoraEstadoMatriculaDTO> </returns>
        public List<FiltroConfiguracionCoordinadoraEstadoMatriculaDTO> ObtenerTodoFiltroConfiguracionCoordinadora()
        {
            try
            {
                return _unitOfWork.EstadoMatriculaRepository.ObtenerTodoFiltroConfiguracionCoordinadora();
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
        /// Obtiene registros de la vista V_ObtenerSubEstadoMatricula para mostrarse en combo.
        /// </summary>
        /// <returns> List<SubEstadoMatriculaFiltroDTO> </returns>
        public List<SubEstadoMatriculaFiltroDTO> ObtenerComboOficialSubEstadoMatricula()
        {
            try
            {
                return _unitOfWork.EstadoMatriculaRepository.ObtenerComboOficialSubEstadoMatricula();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los SubEstados de Subestado,a traves de un proceso alamacenado
        /// </summary>
        /// <returns> List<EstadoMatriculaComboDTO> </returns>
        public SubEstadoMatriculaDTO ObtenerSubEstadoIndividual(int IdEstadoMatricula)
        {
            try
            {
                return _unitOfWork.EstadoMatriculaRepository.ObtenerSubEstadoIndividual(IdEstadoMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor Modificacion: Griselberto Huaman
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta en T_EstadoMatricula , actualiza en T_SubEstado
        /// </summary>
        /// <returns> List<TCRM_EstadoMatriculaInsertarDTO> </returns>
        public EstadoMatriculaListDTO InsertarEstadoSubestado(CRUDEstadoMatriculaDTO data)
        {
            try
            {
                return _unitOfWork.EstadoMatriculaRepository.InsertarEstadoSubestado(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor Modificacion: Griselberto Huaman
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Eliminar en T_EstadoMatricula , actualiza en T_SubEstado
        /// </summary>
        /// <returns> List<TCRM_EstadoMatriculaInsertarDTO> </returns>
        public bool EliminarEstadoSubEstado(int id, string usuario)
        {
            try
            {
                return _unitOfWork.EstadoMatriculaRepository.EliminarEstadoSubEstado(id, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor Modificacion: Griselberto Huaman
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza en T_EstadoMatricula , actualiza en T_SubEstado
        /// </summary>
        /// <returns> List<TCRM_SubEstadoMatriculaDTO> </returns>
        public EstadoMatriculaListDTO EditarEstado(CRUDEstadoMatriculaDTO data)
        {
            try
            {
                return _unitOfWork.EstadoMatriculaRepository.EditarEstado(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de estados de matricula para ser usados en filtros
        /// </summary>
        /// <param></param>
        /// <returns>Lista</returns>
        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.EstadoMatriculaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene Estado Matricula
        /// </summary>
        /// <returns></returns>
        public List<ObtenerEstadoMatriculaDTO> ObtenerEstadosMatricula()
        {
            try
            {
                return _unitOfWork.EstadoMatriculaRepository.ObtenerEstadosMatricula();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/01/2023
        /// Version: 1.0
        /// <summary>
        /// Funcion referenciada en EstadoMatriculaController
        /// Devuelve los subestados segun el Id del EstadoMatricula
        /// </summary>
        /// <param name="idEstadoMatricula"></param>
        /// <returns> Lista: List<TCRM_SubEstadoMatriculaDTO> </returns>
        public List<TCRM_SubEstadoMatriculaDTO> ObtenerFiltroSubEstadoMatricula(int idEstadoMatricula)
        {
            try
            {
                return _unitOfWork.EstadoMatriculaRepository.ObtenerFiltroSubEstadoMatricula(idEstadoMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEstadoMatricula"></param>
        /// <returns> Lista: List<TCRM_SubEstadoMatriculaDTO> </returns>
        public List<TCRM_SubEstadoMatriculaDTO> FiltroObtenerSubEstadosMatricula(List<int> idEstadoMatricula)
        {
            try
            {
                List<TCRM_SubEstadoMatriculaDTO> listEstadosMatricula = new List<TCRM_SubEstadoMatriculaDTO>();
                foreach (var item in idEstadoMatricula)
                {
                    EstadoMatriculaService estadoMatriculaService = new EstadoMatriculaService(_unitOfWork);
                    var lista = estadoMatriculaService.ObtenerFiltroSubEstadoMatricula(item);
                    listEstadosMatricula.AddRange(lista);
                }
                if (idEstadoMatricula.Contains(1))
                {
                    TCRM_SubEstadoMatriculaDTO pagoaldia = new TCRM_SubEstadoMatriculaDTO()
                    {
                        Id = 13,
                        IdAgendaTab = 16,
                        Nombre = "Pago al dia"
                    };
                    TCRM_SubEstadoMatriculaDTO pagoatrasado = new TCRM_SubEstadoMatriculaDTO()
                    {
                        Id = 14,
                        IdAgendaTab = 15,
                        Nombre = "Pago atrasado"
                    };
                    TCRM_SubEstadoMatriculaDTO seguimientoacademico = new TCRM_SubEstadoMatriculaDTO()
                    {
                        Id = 15,
                        IdAgendaTab = 17,
                        Nombre = "Seguimiento academico"
                    };
                    listEstadosMatricula.Add(pagoaldia);
                    listEstadosMatricula.Add(pagoatrasado);
                    listEstadosMatricula.Add(seguimientoacademico);
                }
                return listEstadosMatricula;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
