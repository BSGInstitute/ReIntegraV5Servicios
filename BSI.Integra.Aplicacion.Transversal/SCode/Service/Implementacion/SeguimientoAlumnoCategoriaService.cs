using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: SeguimientoAlumnoCategoriaService
    /// Autor: Jonathan Caipo
    /// Fecha: 11/11/2022
    /// <summary>
    /// Gestión general de T_SeguimientoAlumnoCategoria
    /// </summary>
    public class SeguimientoAlumnoCategoriaService : ISeguimientoAlumnoCategoriaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SeguimientoAlumnoCategoriaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSeguimientoAlumnoCategorium, SeguimientoAlumnoCategoria>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 11/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene combo de Seguiemiento Alumno Categoría
        /// </summary>
        /// <returns></returns>
        public List<FiltroSeguimientoAlumnoCategoriaDTO> ObtenerSeguimientoAlumnoCategoria()
        {
            try
            {
                return _unitOfWork.SeguimientoAlumnoCategoriaRepository.ObtenerSeguimientoAlumnoCategoria();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph llanque
        /// Fecha: 26/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos configuraion comentarios
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public List<ComentarioConfiguracionAgrupadoDTO> ObtenerConfiguracion()
        {
            try
            {
                return _unitOfWork.SeguimientoAlumnoCategoriaRepository.ObtenerConfiguracion().GroupBy(x => new { x.IdTipoSeguimiento, x.NombreTipoSeguimiento, x.IdTipoSeguimientoCategoria, x.NombreSeguimientoCategoria, x.IdEstadoMatricula, x.EstadoMatricula })
                    .Select(y => new ComentarioConfiguracionAgrupadoDTO
                    {
                        IdTipoSeguimiento = y.Key.IdTipoSeguimiento,
                        NombreTipoSeguimiento = y.Key.NombreTipoSeguimiento,
                        IdTipoSeguimientoCategoria = y.Key.IdTipoSeguimientoCategoria,
                        NombreSeguimientoCategoria = y.Key.NombreSeguimientoCategoria,
                        IdEstadoMatricula = y.Key.IdEstadoMatricula,
                        EstadoMatricula = y.Key.EstadoMatricula,
                        IdSubEstadoMatricula = y.Select(z => z.IdSubEstadoMatricula.Value).ToList(),
                        SubEstadoMatricula = y.Select(z => z.SubEstadoMatricula).ToList()

                    }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph llanque
        /// Fecha: 26/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos configuraion comentarios
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool Insertar(SeguimientoAlumnoCategoriaEntradaDTO seguimientoAlumnoCategoriaEntradaDTO)
        {
            try
            {

                var seguimientoAlumnoCategoria = new SeguimientoAlumnoCategoria();
                seguimientoAlumnoCategoria.Nombre = seguimientoAlumnoCategoriaEntradaDTO.Nombre;
                seguimientoAlumnoCategoria.IdTipoSeguimientoAlumnoCategoria = seguimientoAlumnoCategoriaEntradaDTO.IdTipoSeguimientoAlumnoCategoria;
                seguimientoAlumnoCategoria.AplicaModalidadOnline = true;
                seguimientoAlumnoCategoria.AplicaModalidadAonline = true;
                seguimientoAlumnoCategoria.AplicaModalidadPresencial = true;
                seguimientoAlumnoCategoria.UsuarioCreacion = seguimientoAlumnoCategoriaEntradaDTO.Usuario;
                seguimientoAlumnoCategoria.UsuarioModificacion = seguimientoAlumnoCategoriaEntradaDTO.Usuario;
                seguimientoAlumnoCategoria.FechaCreacion = DateTime.Now;
                seguimientoAlumnoCategoria.FechaModificacion = DateTime.Now;
                seguimientoAlumnoCategoria.Estado = true;
                var band = (_unitOfWork.SeguimientoAlumnoCategoriaRepository.Add(seguimientoAlumnoCategoria));
                _unitOfWork.Commit();
                var res1 = band.Id;/*_mapper.Map<SeguimientoAlumnoCategoria>;*/
                //_unitOfWork.DetachAll();
                foreach (var e in seguimientoAlumnoCategoriaEntradaDTO.idSubEstadoMatricula)
                {
                    var seguimientoAlumnoDetalle = new SeguimientoAlumnoDetalle();
                    seguimientoAlumnoDetalle.IdEstadoMatricula = seguimientoAlumnoCategoriaEntradaDTO.idEstadoMatricula;
                    seguimientoAlumnoDetalle.IdSubEstadoMatricula = e;
                    seguimientoAlumnoDetalle.IdSeguimientoAlumnoCategoria = res1;
                    seguimientoAlumnoDetalle.UsuarioCreacion = seguimientoAlumnoCategoriaEntradaDTO.Usuario;
                    seguimientoAlumnoDetalle.UsuarioModificacion = seguimientoAlumnoCategoriaEntradaDTO.Usuario;
                    seguimientoAlumnoDetalle.FechaCreacion = DateTime.Now;
                    seguimientoAlumnoDetalle.FechaModificacion = DateTime.Now;
                    seguimientoAlumnoDetalle.Estado = true;
                    _unitOfWork.SeguimientoAlumnoDetalleRepository.Add(seguimientoAlumnoDetalle);
                    _unitOfWork.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph llanque
        /// Fecha: 26/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos configuraion comentarios
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool Actualizar(SeguimientoAlumnoCategoriaEntradaDTO seguimientoAlumnoCategoriaEntradaDTO)
        {
            try
            {


                //var solicitudTipoReporteService = new SolicitudTipoReporteService(unitOfWork);
                //var solicitudTipoReporte = new SolicitudTipoReporte();
                //solicitudTipoReporte = solicitudTipoReporteService.ObtenerPorId(solicitudTipoReporteEntradaDTO.Id.Value);
                //solicitudTipoReporte.Nombre = solicitudTipoReporteEntradaDTO.Nombre;
                //solicitudTipoReporte.UsuarioModificacion = solicitudTipoReporteEntradaDTO.Usuario;
                //solicitudTipoReporte.FechaModificacion = DateTime.Now;
                //var resultado = solicitudTipoReporteService.Update(solicitudTipoReporte);
                //return Ok(resultado);

                var seguimientoAlumnoCategoria = _unitOfWork.SeguimientoAlumnoCategoriaRepository.ObtenerPorId(seguimientoAlumnoCategoriaEntradaDTO.Id.Value);
                seguimientoAlumnoCategoria.Nombre = seguimientoAlumnoCategoriaEntradaDTO.Nombre;
                seguimientoAlumnoCategoria.IdTipoSeguimientoAlumnoCategoria = seguimientoAlumnoCategoriaEntradaDTO.IdTipoSeguimientoAlumnoCategoria;
                seguimientoAlumnoCategoria.AplicaModalidadOnline = true;
                seguimientoAlumnoCategoria.AplicaModalidadAonline = true;
                seguimientoAlumnoCategoria.AplicaModalidadPresencial = true;
                seguimientoAlumnoCategoria.UsuarioModificacion = seguimientoAlumnoCategoriaEntradaDTO.Usuario;
                seguimientoAlumnoCategoria.FechaModificacion = DateTime.Now;
                var band = (_unitOfWork.SeguimientoAlumnoCategoriaRepository.Update(seguimientoAlumnoCategoria));
                _unitOfWork.Commit();
                //var res1 = band.Id;/*_mapper.Map<SeguimientoAlumnoCategoria>;*/
                //_unitOfWork.DetachAll();
                var seguimientoAlumnodetalle = _unitOfWork.SeguimientoAlumnoDetalleRepository.ObtenerPorIdSeguimientoAlumnoCategoria(seguimientoAlumnoCategoriaEntradaDTO.Id.Value);
                if(seguimientoAlumnodetalle.Count() > 0)
                {
                    if(seguimientoAlumnodetalle.FirstOrDefault()!.IdEstadoMatricula != seguimientoAlumnoCategoriaEntradaDTO.idEstadoMatricula)
                    {
                        _unitOfWork.SeguimientoAlumnoDetalleRepository.Delete(seguimientoAlumnodetalle.Select(x => x.Id), seguimientoAlumnoCategoriaEntradaDTO.Usuario);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        seguimientoAlumnodetalle.RemoveAll(x => seguimientoAlumnoCategoriaEntradaDTO.idSubEstadoMatricula.Any(z => z == x.IdSubEstadoMatricula));
                        _unitOfWork.SeguimientoAlumnoDetalleRepository.Delete(seguimientoAlumnodetalle.Select(x => x.Id), seguimientoAlumnoCategoriaEntradaDTO.Usuario);
                        _unitOfWork.Commit();
                    }
                }
                seguimientoAlumnodetalle = _unitOfWork.SeguimientoAlumnoDetalleRepository.ObtenerPorIdSeguimientoAlumnoCategoria(seguimientoAlumnoCategoriaEntradaDTO.Id.Value);
                
                foreach (var item in seguimientoAlumnoCategoriaEntradaDTO.idSubEstadoMatricula)
                {
                    var seguimientoAlumnoDetalle = seguimientoAlumnodetalle.FirstOrDefault(x => x.IdSubEstadoMatricula == item);
                    if(seguimientoAlumnoDetalle == null) {
                        seguimientoAlumnoDetalle = new();
                        seguimientoAlumnoDetalle.IdEstadoMatricula = seguimientoAlumnoCategoriaEntradaDTO.idEstadoMatricula;
                        seguimientoAlumnoDetalle.IdSubEstadoMatricula = item;
                        seguimientoAlumnoDetalle.IdSeguimientoAlumnoCategoria = seguimientoAlumnoCategoriaEntradaDTO.Id.Value;
                        seguimientoAlumnoDetalle.UsuarioCreacion = seguimientoAlumnoCategoriaEntradaDTO.Usuario;
                        seguimientoAlumnoDetalle.UsuarioModificacion = seguimientoAlumnoCategoriaEntradaDTO.Usuario;
                        seguimientoAlumnoDetalle.FechaCreacion = DateTime.Now;
                        seguimientoAlumnoDetalle.FechaModificacion = DateTime.Now;
                        seguimientoAlumnoDetalle.Estado = true;
                        _unitOfWork.SeguimientoAlumnoDetalleRepository.Add(seguimientoAlumnoDetalle);
                        _unitOfWork.Commit();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Joseph llanque
        /// Fecha: 26/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos configuraion comentarios
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                _unitOfWork.SeguimientoAlumnoCategoriaRepository.Delete(id, usuario);
                var seguimientoAlumnodetalle = _unitOfWork.SeguimientoAlumnoDetalleRepository.ObtenerPorIdSeguimientoAlumnoCategoria(id);
                _unitOfWork.SeguimientoAlumnoDetalleRepository.Delete(seguimientoAlumnodetalle.Select(x => x.Id), usuario);


                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
