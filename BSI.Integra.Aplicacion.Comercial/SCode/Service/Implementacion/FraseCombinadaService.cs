using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion
{
    /// Service: FraseCombinadaService
    /// Autor: Joseph Llanque
    /// Fecha: 08/03/2025
    /// <summary>
    /// Gestión general de T_FraseCombinadaService
    /// </summary>
    public class FraseCombinadaService : IFraseCombinadaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public FraseCombinadaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFraseCombinadum, FraseCombinada>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public FraseCombinada Add(FraseCombinada entidad)
        {
            try
            {
                var modelo = _unitOfWork.FraseCombinadaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FraseCombinada>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FraseCombinada Update(FraseCombinada entidad)
        {
            try
            {
                var modelo = _unitOfWork.FraseCombinadaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FraseCombinada>(modelo);
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
                _unitOfWork.FraseCombinadaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FraseCombinada> Add(List<FraseCombinada> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FraseCombinadaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FraseCombinada>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FraseCombinada> Update(List<FraseCombinada> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FraseCombinadaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FraseCombinada>>(modelo);
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
                _unitOfWork.FraseCombinadaRepository.Delete(listadoIds, usuario);
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
