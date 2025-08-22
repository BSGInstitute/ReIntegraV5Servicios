using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class FeedbackGrupoPreguntaProgramaEspecificoService : IFeedbackGrupoPreguntaProgramaEspecificoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FeedbackGrupoPreguntaProgramaEspecificoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<TFeedbackGrupoPreguntaProgramaEspecifico, FeedbackGrupoPreguntaProgramaEspecifico>(MemberList.None).ReverseMap();
                cfg.CreateMap<TFeedbackGrupoPreguntaProgramaEspecifico, FeedbackGrupoPreguntaProgramaEspecificoDTO>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Klebert Layme
        /// Fecha: 17/05/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los FeedbackGrupoPreguntaProgramaGeneral asociados a una idGrupoPreguntaPG
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorIdGrupoPreguntaPEspecifico(int idGrupoPreguntaPE, string usuario, IEnumerable<int> nuevos)
        {
            try
            {
                var listaBorrar = _unitOfWork.FeedbackGrupoPreguntaProgramaEspecificoRepository.ObtenerPorIdFeedbackConfigurar(idGrupoPreguntaPE).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Equals(x.IdPespecifico)));
                _unitOfWork.FeedbackGrupoPreguntaProgramaEspecificoRepository.Delete(listaBorrar.Select(x => x.Id).ToList(), usuario);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
