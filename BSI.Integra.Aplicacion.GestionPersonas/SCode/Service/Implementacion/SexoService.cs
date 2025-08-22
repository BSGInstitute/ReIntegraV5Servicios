using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
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
    public class SexoService : ISexoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SexoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSexo, Sexo>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSexo, SexoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<Sexo, SexoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SexoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                return _unitOfWork.SexoRepository.ObtenerComboAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
