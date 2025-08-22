using AutoMapper;

using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: TipoDocumentoAlumnoService
    /// Autor: Christian Quispe Mamani.
    /// Fecha: 16/05/2023
    /// <summary>
    /// Gestión general de T_TipoDocumentoAlumno
    /// </summary>
    public class TipoDocumentoAlumnoService : ITipoDocumentoAlumnoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoDocumentoAlumnoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TTipoDocumentoAlumno, TipoDocumentoAlumno>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TipoDocumentoAlumno, TipoDocumentoAlumnoDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Inserta un nuevo registro a la tabla TipoDocumentoAlumno y hijos
        /// </summary>
        /// <returns> Lista TipoDocumentoAlumnoDTO </returns>
        public TipoDocumentoAlumnoDTO InsertarTipoDocumentoAlumno(TipoDocumentoAlumnoEntidadDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    List<TipoDocumentoAlumnoEstadoMatricula> listaEstadosMatriculas = new List<TipoDocumentoAlumnoEstadoMatricula>();
                    List<TipoDocumentoAlumnoSubEstadoMatricula> listaSubEstadosMatriculas = new List<TipoDocumentoAlumnoSubEstadoMatricula>();
                    List<TipoDocumentoAlumnoModalidadCurso> listaModalidadesCursos = new List<TipoDocumentoAlumnoModalidadCurso>();
                    List<TipoDocumentoAlumnoPgeneral> listaProgramasGenerales = new List<TipoDocumentoAlumnoPgeneral>();

                    TipoDocumentoAlumno tipoDocumento = new TipoDocumentoAlumno()
                    {
                        Nombre = dto.Nombre,
                        IdPlantillaFrontal = dto.IdPlantillaFrontal,
                        IdPlantillaPosterior = dto.IdPlantillaPosterior,
                        IdOperadorComparacion = dto.IdCriterioNota,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true,
                        TieneDeuda = dto.TieneDeuda
                    };
                    var respuesta = _unitOfWork.TipoDocumentoAlumnoRepository.Add(tipoDocumento);
                    _unitOfWork.Commit();

                    foreach (int idModalidad in dto.IdsModalidad)
                    {
                        TipoDocumentoAlumnoModalidadCurso modalidadCurso = new();
                        modalidadCurso.IdTipoDocumentoAlumno = respuesta.Id;
                        modalidadCurso.IdModalidad = idModalidad;
                        modalidadCurso.UsuarioCreacion = usuario;
                        modalidadCurso.UsuarioModificacion = usuario;
                        modalidadCurso.FechaCreacion = DateTime.Now;
                        modalidadCurso.FechaModificacion = DateTime.Now;
                        modalidadCurso.Estado = true;
                        listaModalidadesCursos.Add(modalidadCurso);
                    }

                    foreach (int idEstadoMatricula in dto.IdsEstadoMatricula)
                    {
                        TipoDocumentoAlumnoEstadoMatricula estadoMatricula = new();
                        estadoMatricula.IdTipoDocumentoAlumno = respuesta.Id;
                        estadoMatricula.IdEstadoMatricula = idEstadoMatricula;
                        estadoMatricula.UsuarioCreacion = usuario;
                        estadoMatricula.UsuarioModificacion = usuario;
                        estadoMatricula.FechaCreacion = DateTime.Now;
                        estadoMatricula.FechaModificacion = DateTime.Now;
                        estadoMatricula.Estado = true;
                        listaEstadosMatriculas.Add(estadoMatricula);
                    }

                    if (dto.IdsSubEstadoMatricula == null)
                    {
                        TipoDocumentoAlumnoSubEstadoMatricula subEstadoMatricula = new();
                        subEstadoMatricula.IdTipoDocumentoAlumno = respuesta.Id;
                        subEstadoMatricula.IdSubEstadoMatricula = 0;
                        subEstadoMatricula.UsuarioCreacion = usuario;
                        subEstadoMatricula.UsuarioModificacion = usuario;
                        subEstadoMatricula.FechaCreacion = DateTime.Now;
                        subEstadoMatricula.FechaModificacion = DateTime.Now;
                        subEstadoMatricula.Estado = true;
                        listaSubEstadosMatriculas.Add(subEstadoMatricula);
                    }
                    else
                    {
                        foreach (int idSubEstado in dto.IdsSubEstadoMatricula)
                        {
                            TipoDocumentoAlumnoSubEstadoMatricula subEstadoMatricula = new();
                            subEstadoMatricula.IdTipoDocumentoAlumno = respuesta.Id;
                            subEstadoMatricula.IdSubEstadoMatricula = idSubEstado;
                            subEstadoMatricula.UsuarioCreacion = usuario;
                            subEstadoMatricula.UsuarioModificacion = usuario;
                            subEstadoMatricula.FechaCreacion = DateTime.Now;
                            subEstadoMatricula.FechaModificacion = DateTime.Now;
                            subEstadoMatricula.Estado = true;
                            listaSubEstadosMatriculas.Add(subEstadoMatricula);
                        }
                    }

                    if (dto.IdsPGenerales == null)
                    {
                        TipoDocumentoAlumnoPgeneral programaGeneral = new();
                        programaGeneral.IdTipoDocumentoAlumno = respuesta.Id;
                        programaGeneral.IdPgeneral = 0;
                        programaGeneral.UsuarioCreacion = usuario;
                        programaGeneral.UsuarioModificacion = usuario;
                        programaGeneral.FechaCreacion = DateTime.Now;
                        programaGeneral.FechaModificacion = DateTime.Now;
                        programaGeneral.Estado = true;
                        listaProgramasGenerales.Add(programaGeneral);
                    }
                    else
                    {
                        foreach (int idPGeneral in dto.IdsPGenerales)
                        {
                            TipoDocumentoAlumnoPgeneral programaGeneral = new();
                            programaGeneral.IdTipoDocumentoAlumno = respuesta.Id;
                            programaGeneral.IdPgeneral = idPGeneral;
                            programaGeneral.UsuarioCreacion = usuario;
                            programaGeneral.UsuarioModificacion = usuario;
                            programaGeneral.FechaCreacion = DateTime.Now;
                            programaGeneral.FechaModificacion = DateTime.Now;
                            programaGeneral.Estado = true;
                            listaProgramasGenerales.Add(programaGeneral);
                        }
                    }

                    _unitOfWork.TipoDocumentoAlumnoModalidadCursoRepository.Add(listaModalidadesCursos);
                    _unitOfWork.TipoDocumentoAlumnoSubEstadoMatriculaRepository.Add(listaSubEstadosMatriculas);
                    _unitOfWork.TipoDocumentoAlumnoEstadoMatriculaRepository.Add(listaEstadosMatriculas);
                    _unitOfWork.TipoDocumentoAlumnoPgeneralRepository.Add(listaProgramasGenerales);
                    _unitOfWork.Commit();

                    var respuestaRetorno = _unitOfWork.TipoDocumentoAlumnoRepository.ObtenerNombrePlantillaPorId(respuesta.Id);
                    return _mapper.Map<TipoDocumentoAlumnoDTO>(respuestaRetorno);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TipoDocumentoAlumnoDTO ActualizarTipoDocumentoAlumno(TipoDocumentoAlumnoEntidadDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    TipoDocumentoAlumno tipoDocumento = _unitOfWork.TipoDocumentoAlumnoRepository.ObtenerPorId(dto.id);
                    if (tipoDocumento != null && tipoDocumento.Id != 0)
                    {
                        tipoDocumento.Nombre = dto.Nombre;
                        tipoDocumento.IdPlantillaFrontal = dto.IdPlantillaFrontal;
                        tipoDocumento.IdPlantillaPosterior = dto.IdPlantillaPosterior;
                        tipoDocumento.IdOperadorComparacion = dto.IdCriterioNota;
                        tipoDocumento.UsuarioModificacion = usuario;
                        tipoDocumento.FechaModificacion = DateTime.Now;
                        tipoDocumento.Estado = true;
                        tipoDocumento.TieneDeuda = dto.TieneDeuda;
                        var uni = _unitOfWork.TipoDocumentoAlumnoRepository.Update(tipoDocumento);
                        _unitOfWork.Commit();
                    }

                    if (dto.IdsModalidad.Count() > 0) EliminarInsertarModalidadCurso(dto, usuario);
                    if (dto.IdsEstadoMatricula.Count() > 0) EliminarInsertarEstadoMatricula(dto, usuario);
                    EliminarInsertarSubEstadoMatricula(dto, usuario);
                    EliminarInsertarProgramaGeneral(dto, usuario);

                    var respuesta = _unitOfWork.TipoDocumentoAlumnoRepository.ObtenerNombrePlantillaPorId(dto.id);
                    return _mapper.Map<TipoDocumentoAlumnoDTO>(respuesta);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Identifica y separa si se va a eliminar o crear T_TipoDocumentoAlumnoModalidadCurso
        /// </summary>
        private void EliminarInsertarModalidadCurso(TipoDocumentoAlumnoEntidadDTO detalleTipoDocumentoAlumno, string usuario)
        {
            var listaBorrarModalidades = _unitOfWork.TipoDocumentoAlumnoModalidadCursoRepository.ObtenerIdsPorIdTipoDocumentoAlumno(detalleTipoDocumentoAlumno.id).ToList();
            var listaCrearModalidades = listaBorrarModalidades.ToList();

            listaBorrarModalidades.RemoveAll(x => detalleTipoDocumentoAlumno.IdsModalidad.Any(y => y.Equals(x.Valor)));
            detalleTipoDocumentoAlumno.IdsModalidad.RemoveAll(x => listaCrearModalidades.Any(y => y.Valor.Equals(x)));

            if (listaBorrarModalidades.Count() > 0)
            {
                _unitOfWork.TipoDocumentoAlumnoModalidadCursoRepository.Delete(listaBorrarModalidades.Select(x => x.Id).ToList(), usuario);
            }
            List<TipoDocumentoAlumnoModalidadCurso> listaModalidades = new List<TipoDocumentoAlumnoModalidadCurso>();
            if (detalleTipoDocumentoAlumno.IdsModalidad.Count() > 0)
            {
                foreach (int idModalidad in detalleTipoDocumentoAlumno.IdsModalidad)
                {
                    TipoDocumentoAlumnoModalidadCurso? modalidadCurso = new TipoDocumentoAlumnoModalidadCurso();
                    modalidadCurso.IdTipoDocumentoAlumno = detalleTipoDocumentoAlumno.id;
                    modalidadCurso.IdModalidad = idModalidad;
                    modalidadCurso.UsuarioCreacion = usuario;
                    modalidadCurso.UsuarioModificacion = usuario;
                    modalidadCurso.FechaCreacion = DateTime.Now;
                    modalidadCurso.FechaModificacion = DateTime.Now;
                    modalidadCurso.Estado = true;
                    listaModalidades.Add(modalidadCurso);
                }
            }
            if (listaModalidades.Count() > 0 || listaBorrarModalidades.Count() > 0)
            {
                if (listaModalidades.Count() > 0) _unitOfWork.TipoDocumentoAlumnoModalidadCursoRepository.Add(listaModalidades);
                _unitOfWork.Commit();
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Identifica y separa si se va a eliminar o crear T_TipoDocumentoAlumnoEstadoMatricula
        /// </summary>
        private void EliminarInsertarEstadoMatricula(TipoDocumentoAlumnoEntidadDTO detalleTipoDocumentoAlumno, string usuario)
        {
            var listaBorrarEstadoMatricula = _unitOfWork.TipoDocumentoAlumnoEstadoMatriculaRepository.ObtenerIdsPorIdTipoDocumentoAlumno(detalleTipoDocumentoAlumno.id).ToList();
            var listaCrearEstadoMatricula = listaBorrarEstadoMatricula.ToList();

            listaBorrarEstadoMatricula.RemoveAll(x => detalleTipoDocumentoAlumno.IdsEstadoMatricula.Any(y => y.Equals(x.Valor)));
            detalleTipoDocumentoAlumno.IdsEstadoMatricula.RemoveAll(x => listaCrearEstadoMatricula.Any(y => y.Valor.Equals(x)));

            if (listaBorrarEstadoMatricula.Count() > 0)
            {
                _unitOfWork.TipoDocumentoAlumnoEstadoMatriculaRepository.Delete(listaBorrarEstadoMatricula.Select(x => x.Id).ToList(), usuario);
            }
            List<TipoDocumentoAlumnoEstadoMatricula> listaEstados = new List<TipoDocumentoAlumnoEstadoMatricula>();
            if (detalleTipoDocumentoAlumno.IdsEstadoMatricula.Count() > 0)
            {
                foreach (int idEstadoMatricula in detalleTipoDocumentoAlumno.IdsEstadoMatricula)
                {
                    TipoDocumentoAlumnoEstadoMatricula? estadoMatricula = new TipoDocumentoAlumnoEstadoMatricula();
                    estadoMatricula.IdTipoDocumentoAlumno = detalleTipoDocumentoAlumno.id;
                    estadoMatricula.IdEstadoMatricula = idEstadoMatricula;
                    estadoMatricula.UsuarioCreacion = usuario;
                    estadoMatricula.UsuarioModificacion = usuario;
                    estadoMatricula.FechaCreacion = DateTime.Now;
                    estadoMatricula.FechaModificacion = DateTime.Now;
                    estadoMatricula.Estado = true;
                    listaEstados.Add(estadoMatricula);
                }
            }
            if (listaEstados.Count() > 0 || listaBorrarEstadoMatricula.Count() > 0)
            {
                if (listaEstados.Count() > 0) _unitOfWork.TipoDocumentoAlumnoEstadoMatriculaRepository.Add(listaEstados);
                _unitOfWork.Commit();
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Identifica y separa si se va a eliminar o crear T_TipoDocumentoAlumnoSubEstadoMatricula
        /// </summary>
        private void EliminarInsertarSubEstadoMatricula(TipoDocumentoAlumnoEntidadDTO detalleTipoDocumentoAlumno, string usuario)
        {
            var listaOriginalSubEstadoMatricula = detalleTipoDocumentoAlumno.IdsSubEstadoMatricula.ToList();
            var listaBorrarSubEstadoMatricula = _unitOfWork.TipoDocumentoAlumnoSubEstadoMatriculaRepository.ObtenerIdsPorIdTipoDocumentoAlumno(detalleTipoDocumentoAlumno.id).ToList();
            var listaCrearSubEstadoMatricula = listaBorrarSubEstadoMatricula.ToList();

            listaBorrarSubEstadoMatricula.RemoveAll(x => detalleTipoDocumentoAlumno.IdsSubEstadoMatricula.Any(y => y.Equals(x.Valor)));
            detalleTipoDocumentoAlumno.IdsSubEstadoMatricula.RemoveAll(x => listaCrearSubEstadoMatricula.Any(y => y.Valor.Equals(x)));

            if (listaBorrarSubEstadoMatricula.Count() > 0)
            {
                _unitOfWork.TipoDocumentoAlumnoSubEstadoMatriculaRepository.Delete(listaBorrarSubEstadoMatricula.Select(x => x.Id).ToList(), usuario);
            }
            List<TipoDocumentoAlumnoSubEstadoMatricula> listaSubEstados = new List<TipoDocumentoAlumnoSubEstadoMatricula>();
            if (listaOriginalSubEstadoMatricula.Count() > 0)
            {
                if (detalleTipoDocumentoAlumno.IdsSubEstadoMatricula.Count() > 0)
                {
                    foreach (int idSubEstadoMatricula in detalleTipoDocumentoAlumno.IdsSubEstadoMatricula)
                    {

                        TipoDocumentoAlumnoSubEstadoMatricula? subEstadoMatricula = new TipoDocumentoAlumnoSubEstadoMatricula();
                        subEstadoMatricula.IdTipoDocumentoAlumno = detalleTipoDocumentoAlumno.id;
                        subEstadoMatricula.IdSubEstadoMatricula = idSubEstadoMatricula;
                        subEstadoMatricula.UsuarioCreacion = usuario;
                        subEstadoMatricula.UsuarioModificacion = usuario;
                        subEstadoMatricula.FechaCreacion = DateTime.Now;
                        subEstadoMatricula.FechaModificacion = DateTime.Now;
                        subEstadoMatricula.Estado = true;
                        listaSubEstados.Add(subEstadoMatricula);
                    }
                }
            }
            else
            {
                TipoDocumentoAlumnoSubEstadoMatricula subEstadoMatricula = new TipoDocumentoAlumnoSubEstadoMatricula();
                subEstadoMatricula.IdTipoDocumentoAlumno = detalleTipoDocumentoAlumno.id;
                subEstadoMatricula.IdSubEstadoMatricula = 0;
                subEstadoMatricula.UsuarioCreacion = usuario;
                subEstadoMatricula.UsuarioModificacion = usuario;
                subEstadoMatricula.FechaCreacion = DateTime.Now;
                subEstadoMatricula.FechaModificacion = DateTime.Now;
                subEstadoMatricula.Estado = true;
                listaSubEstados.Add(subEstadoMatricula);
            }
            if (listaSubEstados.Count() > 0 || listaBorrarSubEstadoMatricula.Count() > 0)
            {
                if (listaSubEstados.Count() > 0) _unitOfWork.TipoDocumentoAlumnoSubEstadoMatriculaRepository.Add(listaSubEstados);
                _unitOfWork.Commit();
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Identifica y separa si se va a eliminar o crear T_TipoDocumentoAlumnoPgeneral
        /// </summary>
        private void EliminarInsertarProgramaGeneral(TipoDocumentoAlumnoEntidadDTO detalleTipoDocumentoAlumno, string usuario)
        {
            var listaOriginalPgeneral = detalleTipoDocumentoAlumno.IdsPGenerales.ToList();
            var listaBorrarPgeneral = _unitOfWork.TipoDocumentoAlumnoPgeneralRepository.ObtenerIdsPorIdTipoDocumentoAlumno(detalleTipoDocumentoAlumno.id).ToList();
            var listaCrearPgeneral = listaBorrarPgeneral.ToList();

            listaBorrarPgeneral.RemoveAll(x => detalleTipoDocumentoAlumno.IdsPGenerales.Any(y => y.Equals(x.Valor)));
            detalleTipoDocumentoAlumno.IdsPGenerales.RemoveAll(x => listaCrearPgeneral.Any(y => y.Valor.Equals(x)));

            if (listaBorrarPgeneral.Count() > 0)
            {
                _unitOfWork.TipoDocumentoAlumnoPgeneralRepository.Delete(listaBorrarPgeneral.Select(x => x.Id), usuario);
            }
            List<TipoDocumentoAlumnoPgeneral> listaPgeneral = new List<TipoDocumentoAlumnoPgeneral>();
            if (listaOriginalPgeneral.Count() > 0)
            {
                if (detalleTipoDocumentoAlumno.IdsPGenerales.Count() > 0)
                {
                    foreach (int idPgeneral in detalleTipoDocumentoAlumno.IdsPGenerales)
                    {
                        TipoDocumentoAlumnoPgeneral? programaGeneral = new TipoDocumentoAlumnoPgeneral();
                        programaGeneral.IdTipoDocumentoAlumno = detalleTipoDocumentoAlumno.id;
                        programaGeneral.IdPgeneral = idPgeneral;
                        programaGeneral.UsuarioCreacion = usuario;
                        programaGeneral.UsuarioModificacion = usuario;
                        programaGeneral.FechaCreacion = DateTime.Now;
                        programaGeneral.FechaModificacion = DateTime.Now;
                        programaGeneral.Estado = true;
                        listaPgeneral.Add(programaGeneral);
                    }
                }
            }
            else
            {
                TipoDocumentoAlumnoPgeneral programaGeneral = new TipoDocumentoAlumnoPgeneral();
                programaGeneral.IdTipoDocumentoAlumno = detalleTipoDocumentoAlumno.id;
                programaGeneral.IdPgeneral = 0;
                programaGeneral.UsuarioCreacion = usuario;
                programaGeneral.UsuarioModificacion = usuario;
                programaGeneral.FechaCreacion = DateTime.Now;
                programaGeneral.FechaModificacion = DateTime.Now;
                programaGeneral.Estado = true;
                listaPgeneral.Add(programaGeneral);
            }
            if (listaPgeneral.Count() > 0 || listaBorrarPgeneral.Count() > 0)
            {
                if (listaPgeneral.Count() > 0) _unitOfWork.TipoDocumentoAlumnoPgeneralRepository.Add(listaPgeneral);
                _unitOfWork.Commit();
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los TipoDocumentoAlumnoService
        /// </summary>
        /// <returns> Lista MaterialAccionDTO </returns>
        public bool EliminarTipoDocumentoAlumno(int id, string usuario)
        {
            try
            {
                var tipoDocumentoAlumno = _unitOfWork.TipoDocumentoAlumnoRepository.ObtenerPorId(id);
                if (tipoDocumentoAlumno != null && tipoDocumentoAlumno.Id != 0)
                {
                    var respuesta = _unitOfWork.TipoDocumentoAlumnoRepository.Delete(id, usuario);
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
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los V_TPlantilla_CertificadoConstancia
        /// </summary>
        /// <returns> Lista PlantilaCertificadoConstanciaDTO </returns>
        public IEnumerable<TipoDocumentoAlumnoDTO> Obtener()
        {
            try
            {
                var respuesta = _unitOfWork.TipoDocumentoAlumnoRepository.Obtener();
                return _mapper.Map<List<TipoDocumentoAlumnoDTO>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los V_TPlantilla_CertificadoConstancia
        /// </summary>
        /// <returns> Lista PlantilaCertificadoConstanciaDTO </returns>
        public IEnumerable<PlantilaCertificadoConstanciaDTO> ObtenerPlantillaCertificadoConstancia()
        {
            try
            {
                var respuesta = _unitOfWork.TipoDocumentoAlumnoRepository.ObtenerPlantillaCertificadoConstancia();
                return _mapper.Map<List<PlantilaCertificadoConstanciaDTO>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los ids de las siguientes tablas:
        /// T_TipoDocumentoAlumnoPGeneral,
        /// T_TipoDocumentoAlumnoSubEstadoMatricula,
        /// T_TipoDocumentoAlumnoEstadoMatricula,
        /// T_TipoDocumentoAlumnoModalidadCurso
        /// </summary>
        /// <returns> Lista ComboPGDTO </returns>
        public TipoDocumentoAlumnoDetalleDTO ObtenerIdsDetalleTipoDocumento(int idTipoDocumentoAlumno)
        {
            try
            {
                TipoDocumentoAlumnoDetalleDTO idsDetalle = new TipoDocumentoAlumnoDetalleDTO()
                {
                    IdsModalidad = _unitOfWork.TipoDocumentoAlumnoModalidadCursoRepository.ObtenerIdsModalidadPorIdTipoDocumentoAlumno(idTipoDocumentoAlumno),
                    IdsEstadoMatricula = _unitOfWork.TipoDocumentoAlumnoEstadoMatriculaRepository.ObtenerIdsEstadoMatriculaPorIdTipoDocumentoAlumno(idTipoDocumentoAlumno),
                    IdsSubEstadoMatricula = _unitOfWork.TipoDocumentoAlumnoSubEstadoMatriculaRepository.ObtenerIdsSubEstadoMatriculaPorIdTipoDocumentoAlumno(idTipoDocumentoAlumno),
                    IdsPGeneral = _unitOfWork.TipoDocumentoAlumnoPgeneralRepository.ObtenerIdsProgramaGeneralPorIdTipoDocumentoAlumno(idTipoDocumentoAlumno),
                };
                return _mapper.Map<TipoDocumentoAlumnoDetalleDTO>(idsDetalle);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los ids de las siguientes tablas:
        /// T_TipoDocumentoAlumnoPGeneral,
        /// T_TipoDocumentoAlumnoSubEstadoMatricula,
        /// T_TipoDocumentoAlumnoEstadoMatricula,
        /// T_TipoDocumentoAlumnoModalidadCurso
        /// </summary>
        /// <returns> Lista ComboPGDTO </returns>
        public TipoDocumentoAlumnoCombosDTO ObtenerCombosTipoDocumento()
        {
            try
            {
                TipoDocumentoAlumnoCombosDTO idsDetalle = new TipoDocumentoAlumnoCombosDTO()
                {
                    filtroModalidadCurso = _unitOfWork.ModalidadCursoRepository.ObtenerCombo(),
                    filtroEstadoMatricula = _unitOfWork.EstadoMatriculaRepository.ObtenerCombo(),
                    filtroOperadorComparacion = _unitOfWork.OperadorComparacionRepository.ObtenerCombo(),
                    filtroSubEstadoMatricula = _unitOfWork.MatriculaCabeceraRepository.ObtenerSubEstadoMatricula()
                };
                return _mapper.Map<TipoDocumentoAlumnoCombosDTO>(idsDetalle);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los ids de las siguientes tablas
        /// </summary>
        /// <returns> Lista ComboPGDTO </returns>
        public TipoDocumentoAlumnoListaDetalleConfiguracionDTO ObtenerDetalleConfiguracionCerficicado(int idTipoDocumentoAlumno)
        {
            try
            {
                var idsProgramasGenerales = _unitOfWork.TipoDocumentoAlumnoPgeneralRepository.ObtenerIdsProgramaGeneralPorIdTipoDocumentoAlumno(idTipoDocumentoAlumno).ToList();
                var configuracion = _unitOfWork.TipoDocumentoAlumnoRepository.ObtenerDetalleConfiguracionCerficicado(idTipoDocumentoAlumno);
                var resultado = new TipoDocumentoAlumnoListaDetalleConfiguracionDTO();
                if (configuracion != null && configuracion.Count() > 0)
                {
                    resultado = configuracion
                    .GroupBy(x => new { x.Id, x.IdOperadorComparacion, x.TieneDeuda })
                    .Select(g => new TipoDocumentoAlumnoListaDetalleConfiguracionDTO
                    {
                        Id = g.Key.Id,
                        IdOperadorComparacion = g.Key.IdOperadorComparacion,
                        TieneDeuda = g.Key.TieneDeuda,
                        IdsModalidad = g.GroupBy(y => y.IdModalidadCurso)
                                        .Select(y => y.Key).ToList(),

                        IdsEstadoMatricula = g.GroupBy(y => y.IdEstadoMatricula)
                                               .Select(y => y.Key).ToList(),

                        IdsSubEstadoMatricula = g.GroupBy(y => y.IdSubEstadoMatricula)
                                                 .Select(y => y.Key).ToList()
                    }).FirstOrDefault();
                } 
                if(idsProgramasGenerales != null && idsProgramasGenerales.Count > 0) resultado.IdsProgramaGeneral = idsProgramasGenerales;
                return _mapper.Map<TipoDocumentoAlumnoListaDetalleConfiguracionDTO>(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
