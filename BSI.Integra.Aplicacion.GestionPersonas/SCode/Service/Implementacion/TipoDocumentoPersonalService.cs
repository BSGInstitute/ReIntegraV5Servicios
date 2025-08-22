using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
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
    /// Autor: Eliot Arias F.
    /// Fecha: 25/10/2024
    /// Version: 1.0
    /// <summary>
    /// Gestión general de la tabla TipoDocumentoPersonal
    /// </summary>
    public class TipoDocumentoPersonalService : ITipoDocumentoPersonalService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public TipoDocumentoPersonalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoDocumentoPersonal, TipoDocumentoPersonal>(MemberList.None).ReverseMap();
                cfg.CreateMap<TTipoDocumentoPersonal, TipoDocumentoPersonalDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoDocumentoPersonal, TipoDocumentoPersonalDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 25/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los tipos de documento de la tabla TipoDocumentoPersonal para un combo
        /// </summary>
        /// <returns> IEnumerable<TipoDocumentoPersonalComboDTO> </returns>
        public IEnumerable<TipoDocumentoPersonalComboDTO> ObtenerComboDocumentos()
        {
            try
            {
                return _unitOfWork.TipoDocumentoPersonalRepository.ObtenerComboDocumentos();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
