using AutoMapper;
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
    /// Service: CompromisoAlumnoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 12/12/2022
    /// <summary>
    /// Gestión general de T_CompromisoAlumno
    /// </summary>
    public class CompromisoAlumnoService : ICompromisoAlumnoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CompromisoAlumnoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCompromisoAlumno, CompromisoAlumno>(MemberList.None).ReverseMap(); 
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public CompromisoAlumno Add(CompromisoAlumno entidad)
        {
            try
            {
                var modelo = _unitOfWork.CompromisoAlumnoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CompromisoAlumno>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CompromisoAlumno Update(CompromisoAlumno entidad)
        {
            try
            {
                var modelo = _unitOfWork.CompromisoAlumnoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CompromisoAlumno>(modelo);
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
                _unitOfWork.CompromisoAlumnoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CompromisoAlumno> Add(List<CompromisoAlumno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CompromisoAlumnoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CompromisoAlumno>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CompromisoAlumno> Update(List<CompromisoAlumno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CompromisoAlumnoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CompromisoAlumno>>(modelo);
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
                _unitOfWork.CompromisoAlumnoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion 
    }
}
