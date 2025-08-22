using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CampaniaGeneralDetalleService
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 25/11/2022
    /// <summary>
    /// Gestión general de T_CampaniaGeneralDetalle
    /// </summary>
    public class CampaniaGeneralDetalleService: ICampaniaGeneralDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CampaniaGeneralDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCampaniaGeneralDetalle, CampaniaGeneralDetalle>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public CampaniaGeneralDetalle Add(CampaniaGeneralDetalleEnvioDTO data,string usuario)
        {
            try
            {
                var repCampaniaGeneralDetalle = _unitOfWork.CampaniaGeneralDetalleRepository;
                CampaniaGeneralDetalle entidad = new CampaniaGeneralDetalle();
                entidad.Id = 0;
                entidad.Nombre = data.Nombre;
                entidad.IdCampaniaGeneral = data.IdCampaniaGeneral;
                entidad.Prioridad = data.Prioridad;
                entidad.Asunto = data.Asunto;
                entidad.IdPersonal = data.IdPersonal;
                entidad.IdCentroCosto = data.IdCentroCosto;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = usuario;
                entidad.UsuarioModificacion = usuario;


                var modelo = _unitOfWork.CampaniaGeneralDetalleRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CampaniaGeneralDetalle>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CampaniaGeneralDetalle Update(CampaniaGeneralDetalleEnvioDTO data)
        {
            try
            {
                var repCampaniaGeneralDetalle = _unitOfWork.CampaniaGeneralDetalleRepository;
                CampaniaGeneralDetalle entidad = new CampaniaGeneralDetalle();
                entidad.Id = data.Id;
                entidad.Nombre = data.Nombre;
                entidad.IdCampaniaGeneral = data.IdCampaniaGeneral;
                entidad.Prioridad = data.Prioridad;
                entidad.Asunto = data.Asunto;
                entidad.IdPersonal = data.IdPersonal;
                entidad.IdCentroCosto = data.IdCentroCosto;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;

                var modelo = _unitOfWork.CampaniaGeneralDetalleRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CampaniaGeneralDetalle>(modelo);
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
                _unitOfWork.CampaniaGeneralDetalleRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CampaniaGeneralDetalle> Add(List<CampaniaGeneralDetalle> listadoEntidad,string usuario)
        {
            try
            {
                listadoEntidad = listadoEntidad.Select(x => new CampaniaGeneralDetalle
                {
                    Asunto= x.Asunto,
                    CantidadContactosMailing=x.CantidadContactosMailing,
                    CantidadContactosWhatsapp=x.CantidadContactosWhatsapp,
                    EnEjecucion=x.EnEjecucion,
                    Estado= x.Estado,
                    FechaCreacion=x.FechaCreacion,
                    FechaModificacion=x.FechaModificacion,
                    Id= x.Id,
                    IdCampaniaGeneral= x.IdCampaniaGeneral,
                    IdCentroCosto= x.IdCentroCosto,
                    IdConjuntoAnuncio=x.IdConjuntoAnuncio,
                    IdMigracion= x.IdMigracion,
                    IdPersonal= x.IdPersonal,
                    NoIncluyeWhatsaap = x.NoIncluyeWhatsaap,
                    Nombre= x.Nombre,
                    Prioridad= x.Prioridad,
                    UsuarioCreacion= usuario,
                    UsuarioModificacion=usuario,
                    RowVersion= x.RowVersion,
                }).ToList();
                var modelo = _unitOfWork.CampaniaGeneralDetalleRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CampaniaGeneralDetalle>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CampaniaGeneralDetalle> Update(List<CampaniaGeneralDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CampaniaGeneralDetalleRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CampaniaGeneralDetalle>>(modelo);
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
                _unitOfWork.CampaniaGeneralDetalleRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 25/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CampaniaGeneralDetalle para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>


        public IEnumerable<CampaniaGeneralDetalleDTO> ObtenerCampaniaGeneralDetalle()
        {
            try
            {
                return _unitOfWork.CampaniaGeneralDetalleRepository.ObtenerCampaniaGeneralDetalle();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CampaniaGeneralDetalle BuscarCampaniaGeneralDetallePorId(int idCampaniaGeneralDetalle)
        {
            try
            {
                return _unitOfWork.CampaniaGeneralDetalleRepository.BuscarCampaniaGeneralDetallePorId(idCampaniaGeneralDetalle);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarEstadoEjecucionCampaniaGeneralDetalle(int idCampaniaGeneralDetalle, bool flagEjecucion, string usuario)
        {
            try
            {
                _unitOfWork.CampaniaGeneralDetalleRepository.ActualizarEstadoEjecucionCampaniaGeneralDetalle(idCampaniaGeneralDetalle, flagEjecucion,usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
