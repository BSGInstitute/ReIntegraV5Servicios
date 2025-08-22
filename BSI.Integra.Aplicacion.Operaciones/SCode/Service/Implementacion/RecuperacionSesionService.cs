using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: RecuperacionSesionService
    /// Autor: Daniel Huaita Carpio
    /// Fecha: 13/02/2023
    /// <summary>
    /// Gestión de recuperacion de sesiones
    /// </summary>
    public class RecuperacionSesionService : IRecuperacionSesionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public RecuperacionSesionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TRecuperacionSesion, RecuperacionSesion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        /// <summary>
        /// Se obtiene la lista de sesiones por el idPespecifico
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public List<PEspecificoSesionRecuperacionDTO> ObtenerSesionesPorPEspecifico(int idPespecifico, int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.RecuperacionSesionRepository.ObtenerSesionesPorPEspecifico(idPespecifico, idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       public RecuperacionSesion ObtenerPorId(int Id)
        {
            try
            {
                return _unitOfWork.RecuperacionSesionRepository.ObtenerPorId(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RecuperacionSesion Update(RecuperacionSesion entidad)
        {
            try
            {
                var modelo = _unitOfWork.RecuperacionSesionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RecuperacionSesion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RecuperacionSesion Insert(RecuperacionSesion entidad)
        {
            try
            {
                var modelo = _unitOfWork.RecuperacionSesionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RecuperacionSesion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RecuperacionSesion Delete(int id, string usuario)
        {
            try
            {
                var modelo = _unitOfWork.RecuperacionSesionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return _mapper.Map<RecuperacionSesion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Exist(int id)
        {
            try
            {
                var existeValor = _unitOfWork.RecuperacionSesionRepository.Exist(id);
                return existeValor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<RecuperacionSesionDTO> RegistrarRecuperacion(List<RecuperacionSesionDTO> sesiones)
        {
            try
            {
                RecuperacionSesionService _repRecuperacionSesion = new RecuperacionSesionService(_unitOfWork);
                List<RecuperacionSesion> listado = new List<RecuperacionSesion>();
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var sesion in sesiones)
                    {
                        if (sesion.Recupera)
                        {
                            if (sesion.IdRecuperacionSesion != null && _repRecuperacionSesion.Exist(sesion.IdRecuperacionSesion.Value))
                            {
                                RecuperacionSesion recuperacionSesionBO = _repRecuperacionSesion.ObtenerPorId(sesion.IdRecuperacionSesion.Value);
                                recuperacionSesionBO.IdMatriculaCabecera = sesion.IdMatriculaCabecera;
                                recuperacionSesionBO.IdPespecificoSesion = sesion.IdPespecificoSesion;
                                recuperacionSesionBO.Estado = true;
                                recuperacionSesionBO.FechaModificacion = DateTime.Now;
                                recuperacionSesionBO.UsuarioModificacion = sesion.Usuario;
                                _repRecuperacionSesion.Update(recuperacionSesionBO);
                            }
                            else
                            {
                                RecuperacionSesion recuperacionSesionBO = new RecuperacionSesion();
                                recuperacionSesionBO.IdMatriculaCabecera = sesion.IdMatriculaCabecera;
                                recuperacionSesionBO.IdPespecificoSesion = sesion.IdPespecificoSesion;

                                recuperacionSesionBO.UsuarioCreacion = sesion.Usuario;
                                recuperacionSesionBO.UsuarioModificacion = sesion.Usuario;
                                recuperacionSesionBO.Estado = true;
                                recuperacionSesionBO.FechaCreacion = DateTime.Now;
                                recuperacionSesionBO.FechaModificacion = DateTime.Now;

                                _repRecuperacionSesion.Insert(recuperacionSesionBO);
                            }
                        }
                        else
                        {
                            if (sesion.IdRecuperacionSesion != null && _repRecuperacionSesion.Exist(sesion.IdRecuperacionSesion.Value))
                            {
                                _repRecuperacionSesion.Delete(sesion.IdRecuperacionSesion.Value, sesion.Usuario);
                            }
                        }
                    }
                    scope.Complete();
                }
                return(sesiones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
