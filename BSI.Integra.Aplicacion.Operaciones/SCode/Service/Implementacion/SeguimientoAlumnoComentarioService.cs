using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: SeguimientoAlumnoComentarioService
    /// Autor: Daniel Huaita
    /// Fecha: 02/22/2023
    /// <summary>
    /// Gestión general de T_SeguimientoAlumnoComentario
    /// </summary>
    public class SeguimientoAlumnoComentarioService : ISeguimientoAlumnoComentarioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SeguimientoAlumnoComentarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSeguimientoAlumnoComentario, SeguimientoAlumnoComentario>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);

        }
        public SeguimientoAlumnoComentario Add(SeguimientoAlumnoComentarioDTO entidad)
        {
            try
            {
                object miVariable;
                if (entidad.IdSeguimientoAlumnoCategoriaPago.Value != 0)
                {
                    SeguimientoAlumnoComentario seguimientoAlumnoComentarioPago = new SeguimientoAlumnoComentario
                    {
                        IdMatriculaCabecera = entidad.IdMatriculaCabecera,
                        NroCuota = entidad.NroCuota,
                        NroSubCuota = entidad.NroSubCuota,
                        IdSeguimientoAlumnoCategoria = entidad.IdSeguimientoAlumnoCategoriaPago.Value,
                        IdPersonal = entidad.IdPersonal,
                        IdOportunidad = entidad.IdOportunidad,
                        Comentario = entidad.ComentarioPago,
                        FechaCompromiso = null,//Objeto.FechaCompromiso,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = entidad.Usuario,
                        UsuarioModificacion = entidad.Usuario
                    };
                    var modelo = _unitOfWork.SeguimientoAlumnoComentarioRepository.Add(seguimientoAlumnoComentarioPago);
                    _unitOfWork.Commit();
                    miVariable = _mapper.Map<SeguimientoAlumnoComentario>(modelo);
                }
                if (entidad.IdSeguimientoAlumnoCategoriaAcademico.Value != 0)
                {
                    SeguimientoAlumnoComentario seguimientoAlumnoComentarioAcadmico = new SeguimientoAlumnoComentario
                    {
                        IdMatriculaCabecera = entidad.IdMatriculaCabecera,
                        NroCuota = entidad.NroCuota,
                        NroSubCuota = entidad.NroSubCuota,
                        IdSeguimientoAlumnoCategoria = entidad.IdSeguimientoAlumnoCategoriaAcademico.Value,
                        IdPersonal = entidad.IdPersonal,
                        IdOportunidad = entidad.IdOportunidad,
                        Comentario = entidad.ComentarioAcademico,
                        FechaCompromiso = null,//Objeto.FechaCompromiso,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = entidad.Usuario,
                        UsuarioModificacion = entidad.Usuario
                    };
                    var modelo = _unitOfWork.SeguimientoAlumnoComentarioRepository.Add(seguimientoAlumnoComentarioAcadmico);
                    _unitOfWork.Commit();
                    miVariable = _mapper.Map<SeguimientoAlumnoComentario>(modelo);
                }
                SeguimientoAlumnoComentario a = new SeguimientoAlumnoComentario();
                return a;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Joseph Llanque
        /// Fecha: 24/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public List<TipoSeguimientoDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SeguimientoAlumnoComentarioRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoSeguimientoDTO ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.SeguimientoAlumnoComentarioRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public TipoSeguimientoDTO Update(TipoSeguimientoDTO tipoSeguimientoEntrada)
        {
            try
            {
                return _unitOfWork.SeguimientoAlumnoComentarioRepository.Update(tipoSeguimientoEntrada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertarTipoSeguimiento(TipoSeguimientoEntradaDTO tipoSeguimientoEntrada)
        {
            try
            {
                return _unitOfWork.SeguimientoAlumnoComentarioRepository.InsertarTipoSeguimiento(tipoSeguimientoEntrada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarTipoSeguimiento(int id, string usuario)
        {
            try
            {
                return _unitOfWork.SeguimientoAlumnoComentarioRepository.EliminarTipoSeguimiento(id, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
