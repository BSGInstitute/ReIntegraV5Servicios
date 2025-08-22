using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Operacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Operacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Web.Mvc;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Servicio : CertificadoPartnerComplementoService
    /// Autor: Marco Jose Villanueva Torres
    /// Fecha: 15/09/2023 
    /// Servicio : CertificadoPartnerComplementoService
    /// </summary>
    public class CertificadoPartnerComplementoService : ICertificadoPartnerComplementoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CertificadoPartnerComplementoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCertificadoPartnerComplemento, CertificadoPartnerComplemento>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCertificadoPartnerComplemento, CertificadoPartnerComplementoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CertificadoPartnerComplemento, CertificadoPartnerComplementoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CentroCostoCertificadoDTO, CentroCostoCertificado>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCentroCostoCertificado, CentroCostoCertificadoDTO>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);

        }

        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/09/2023 
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos para el grid
        /// </summary>
        public IEnumerable<CertificadoPartnerComplementoDTO> ObtenerTodo()
        {
            return _unitOfWork.CertificadoPartnerComplementoRepository.ObtenerTodo();
        }
        /// Metodo Insertar
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/09/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="CertificadoPartnerComplementoDTO"> parametros de la nueva Plantilla_PW y sus detalles </param>
        /// <returns> CertificadoPartnerComplementoDTO </returns>
        public CertificadoPartnerComplementoDTO Insertar(CertificadoPartnerComplementoDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    CertificadoPartnerComplemento entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Codigo = dto.Codigo,
                        Descripcion = dto.Descripcion,
                        FrontalCentral = dto.FrontalCentral,
                        FrontalInferiorIzquierda = dto.FrontalInferiorIzquierda,
                        PosteriorCentral = dto.PosteriorCentral,
                        PosteriorInferiorIzquierda = dto.PosteriorInferiorIzquierda,
                        MencionEnCertificado = dto.MencionEnCertificado,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.CertificadoPartnerComplementoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<CertificadoPartnerComplementoDTO>(respuesta);

                    return resultado;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Metodo Actualizar
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/09/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="certificadoPartnerComplementoDTO"> parametros de la nueva Plantilla_PW y sus detalles </param>

        public CertificadoPartnerComplementoDTO Actualizar(CertificadoPartnerComplementoDTO dto, string usuario)
        {
            try
            {
                CertificadoPartnerComplemento? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.CertificadoPartnerComplementoRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {

                            entidad.Nombre = dto.Nombre;
                            entidad.Codigo = dto.Codigo;
                            entidad.Descripcion = dto.Descripcion;
                            entidad.FrontalCentral = dto.FrontalCentral;
                            entidad.FrontalInferiorIzquierda = dto.FrontalInferiorIzquierda;
                            entidad.PosteriorCentral = dto.PosteriorCentral;
                            entidad.PosteriorInferiorIzquierda = dto.PosteriorInferiorIzquierda;
                            entidad.MencionEnCertificado = dto.MencionEnCertificado;
                            entidad.Estado = dto.Estado;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.CertificadoPartnerComplementoRepository.Update(entidad);
                            _unitOfWork.Commit();


                            return dto;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Metodo Eliminar.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/09/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica por el Primary Key
        /// </summary>   
        /// <param name="id"> (PK) </param>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.CertificadoPartnerComplementoRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.CertificadoPartnerComplementoRepository.Delete(id, usuario);

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

        // Metodo Asignar
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/09/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una asignacion por id al consultar si el idcentro de costo existe o no 
        /// </summary>   
        /// <param name="idCertificadoPartnerComplemento,idCentroCosto"> (PK) </param>
        /// return CentroCostoCertificadoDTO

        public CentroCostoCertificadoDTO Asignar(int idCertificadoPartnerComplemento, int idCentroCosto, string usuario)
        {
            try
            {
                CentroCostoCertificado? entidad = new();


                entidad = _unitOfWork.CentroCostoCertificadoRepository.ObtenerPorCentroCosto(idCentroCosto);
                if (entidad != null && entidad.Id != 0)
                {
                    entidad.IdCertificadoPartnerComplemento = idCertificadoPartnerComplemento;
                    entidad.FechaModificacion = DateTime.Now;
                    entidad.UsuarioModificacion = usuario;

                    var respuesta = _unitOfWork.CentroCostoCertificadoRepository.Update(entidad);
                    _unitOfWork.Commit();


                    return _mapper.Map<CentroCostoCertificadoDTO>(respuesta);
                }
                else
                {
                    entidad = new();
                    entidad.IdCertificadoPartnerComplemento = idCertificadoPartnerComplemento;
                    entidad.IdCentroCosto = idCentroCosto;
                    entidad.FechaCreacion = DateTime.Now;
                    entidad.FechaModificacion = DateTime.Now;
                    entidad.UsuarioCreacion = usuario;
                    entidad.UsuarioModificacion = usuario;
                    entidad.Estado = true;
                    var respuesta = _unitOfWork.CentroCostoCertificadoRepository.Add(entidad);
                    _unitOfWork.Commit();

                    return _mapper.Map<CentroCostoCertificadoDTO>(respuesta);
                }




            }
            catch (Exception e)
            {
                throw;
            }
        }


        /// Metodo HTTP: Get.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene un Centro de costo Asignado por un Id
        /// </summary>   
        /// <param name="id"> (PK) </param>

        [Route("[Action]/{Id}")]
        public List<CentroCostoAsignadoCertificadoPartnerComplementoDTO> ObtenerCentroCostoAsociadoPorId(int id)
        {
            return _unitOfWork.CertificadoPartnerComplementoRepository.ObtenerCentroCostoAsociadoPorId(id);

        }

    }
}
