using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: SubNivelCcService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_SubNivelCc
    /// </summary>
    public class SubNivelCcService : ISubNivelCcService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SubNivelCcService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSubNivelCc, SubNivelCc>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSubNivelCc, SubNivelCcDTO>(MemberList.None).ReverseMap(); 
            }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Qm
        /// Fecha: 10/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Registra un nuevo SubNivelCc
        /// </summary>
        /// <param name="subNivelCcDTO"> SubNivelCc Jaon</param> 
        /// <returns> SubNivelCcDTO </returns>
        public SubNivelCcDTO Insertar(SubNivelCcDTO subNivelCcDTO, string usuario)
        {
            try
            {
                if (subNivelCcDTO != null)
                {
                    SubNivelCc subNivelCc = new SubNivelCc();
                    subNivelCc.IdAreaCc = subNivelCcDTO.IdAreaCc;
                    subNivelCc.Nombre = subNivelCcDTO.Nombre;
                    subNivelCc.Codigo = subNivelCcDTO.Codigo;
                    subNivelCc.Estado = true;
                    subNivelCc.FechaModificacion = DateTime.Now;
                    subNivelCc.FechaCreacion = DateTime.Now;
                    subNivelCc.UsuarioModificacion = usuario;
                    subNivelCc.UsuarioCreacion = usuario;

                    var tSubNivelCc = _unitOfWork.SubNivelCcRepository.Add(subNivelCc);
                    _unitOfWork.Commit();
                    return _mapper.Map<SubNivelCcDTO>(tSubNivelCc);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 10/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Actuaiza un regitro SubNivelCc
        /// </summary>
        /// <param name="subNivelCcDTO"> SubNivelCc Jaon</param> 
        /// <returns> SubNivelCcDTO </returns>
        public SubNivelCcDTO Actualizar(SubNivelCcDTO subNivelCcDTO, string usuario)
        {
            try
            {
                if (subNivelCcDTO != null)
                {
                    SubNivelCc subNivelCc = _unitOfWork.SubNivelCcRepository.ObtenerPorId(subNivelCcDTO.Id.Value);
                    subNivelCc.IdAreaCc = subNivelCcDTO.IdAreaCc;
                    subNivelCc.Nombre = subNivelCcDTO.Nombre;
                    subNivelCc.Codigo = subNivelCcDTO.Codigo;
                    subNivelCc.FechaModificacion = DateTime.Now;
                    subNivelCc.UsuarioModificacion = usuario;

                    var tSubNivelCc = _unitOfWork.SubNivelCcRepository.Update(subNivelCc);
                    _unitOfWork.Commit();
                    return _mapper.Map<SubNivelCcDTO>(tSubNivelCc);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 10/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina un regitro SubNivelCc
        /// </summary>
        /// <param name="id"> Primary Key del dato </param> 
        /// <param name="usuario"> Usuario del sistema integra </param> 
        /// <returns> bool </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var materialAccion = _unitOfWork.SubNivelCcRepository.ObtenerPorId(id);
                if (materialAccion != null && materialAccion.Id != 0)
                {
                    var respuesta = _unitOfWork.SubNivelCcRepository.Delete(id, usuario);
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
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registros por el filtro
        /// </summary>
        /// <param name="filtro"> filtro para obtener datos </param> 
        /// <returns> List<SubNivelCcListaDTO> </returns>
        public List<SubNivelCcListaDTO> ObtenerPorFiltro(FiltroKendoGridDTO filtro)
        {
            string subNivel = "_";
            if(filtro.Filter != null)
            {
                subNivel = filtro.Filter.Filters.Where(w => w.Field == "nombre").FirstOrDefault() == null ? "_" : filtro.Filter.Filters.Where(w => w.Field == "nombre").FirstOrDefault().Value;
            }
            var resultado = _unitOfWork.SubNivelCcRepository.ObtenerPorFiltro(filtro.Skip, filtro.Take, subNivel);
            var resultadoOrdenado = resultado;
            if (filtro.Sort.Count() > 0 && filtro.Sort[0].Dir != null)
            {
                resultadoOrdenado = (filtro.Sort[0].Dir == "asc") ? resultado.OrderBy(x => x.Nombre).ToList() : resultado.OrderByDescending(x => x.Nombre).ToList();
            }
            return resultadoOrdenado;
        }
    }
}
