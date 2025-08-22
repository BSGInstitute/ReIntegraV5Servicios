using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion
{
    /// Servicio: TipoEstudioService
    /// Autor: Eliot Arias F.
    /// Fecha: 28/10/2024
    /// <summary>
    /// Gestion de Postulante
    /// </summary>
    public class TipoEstudioService : ITipoEstudioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoEstudioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoEstudio, TipoEstudio>(MemberList.None).ReverseMap();
                cfg.CreateMap<TTipoEstudio, TipoEstudioDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoEstudio, TipoEstudioDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        public IEnumerable<TipoEstudioDTO> ObtenerListaTipoEstudioCombo()
        {
            try
            {
                return _unitOfWork.TipoEstudioRepository.ObtenerListaTipoEstudioCombo();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
