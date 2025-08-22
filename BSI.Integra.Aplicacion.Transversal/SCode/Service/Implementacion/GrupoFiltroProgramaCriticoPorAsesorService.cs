using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: GrupoFiltroProgramaCriticoPorAsesorService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_GrupoFiltroProgramaCriticoPorAsesor
    /// </summary>
    public class GrupoFiltroProgramaCriticoPorAsesorService : IGrupoFiltroProgramaCriticoPorAsesorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public GrupoFiltroProgramaCriticoPorAsesorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TGrupoFiltroProgramaCriticoPorAsesor, GrupoFiltroProgramaCriticoPorAsesor>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public GrupoFiltroProgramaCriticoPorAsesor Add(GrupoFiltroProgramaCriticoPorAsesor entidad)
        {
            try
            {
                var modelo = _unitOfWork.GrupoFiltroProgramaCriticoPorAsesorRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GrupoFiltroProgramaCriticoPorAsesor>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GrupoFiltroProgramaCriticoPorAsesor Update(GrupoFiltroProgramaCriticoPorAsesor entidad)
        {
            try
            {
                var modelo = _unitOfWork.GrupoFiltroProgramaCriticoPorAsesorRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GrupoFiltroProgramaCriticoPorAsesor>(modelo);
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
                _unitOfWork.GrupoFiltroProgramaCriticoPorAsesorRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GrupoFiltroProgramaCriticoPorAsesor> Add(List<GrupoFiltroProgramaCriticoPorAsesor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GrupoFiltroProgramaCriticoPorAsesorRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GrupoFiltroProgramaCriticoPorAsesor>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GrupoFiltroProgramaCriticoPorAsesor> Update(List<GrupoFiltroProgramaCriticoPorAsesor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GrupoFiltroProgramaCriticoPorAsesorRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GrupoFiltroProgramaCriticoPorAsesor>>(modelo);
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
                _unitOfWork.GrupoFiltroProgramaCriticoPorAsesorRepository.Delete(listadoIds, usuario);
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
