using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class TipoDescuentoAsesorCoordinadorPwService : ITipoDescuentoAsesorCoordinadorPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoDescuentoAsesorCoordinadorPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoDescuento, TipoDescuentoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoDescuento, TipoDescuentoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TTipoDescuentoAsesorCoordinadorPw, TipoDescuentoAsesorCoordinadorPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<TTipoDescuentoAsesorCoordinadorPw, TipoDescuentoAsesorCoordinadorPwDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoDescuentoAsesorCoordinadorPw, TipoDescuentoAsesorCoordinadorPwDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoDescuento, CompuestoTipoDescuentoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TTipoDescuento, CompuestoTipoDescuentoDTO>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Klebert Layme
        /// Fecha: 17/05/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los TipoDescuentoAsesorCoordinadorPw asociados a una TipoDescuento
        /// </summary>
        /// <param name="idTipoDescuento"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorTipoDescuento(int idTipoDescuento, string usuario, List<string> nuevos)
        {
            try
            {
                var listaBorrar = _unitOfWork.TipoDescuentoAsesorCoordinadorPwRepository.ObtenerPorIdTipoDescuento(idTipoDescuento).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Equals(x.Tipo)));
                _unitOfWork.TipoDescuentoAsesorCoordinadorPwRepository.Delete(listaBorrar.Select(x => x.Id).ToList(), usuario);
                _unitOfWork.Commit();
            }
            catch (Exception )
            {
                throw ;
            }
        }



    }
}
