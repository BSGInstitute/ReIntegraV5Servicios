using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    public class SedeService : ISedeService
    {
        private readonly IUnitOfWork unitOfWork;
        private Mapper _mapper;

        public SedeService(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSedeDTO, SedeDTO>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// <summary>
        /// Obtiene la lista de nombres de las sedes segun lo registrado en los FURS
        /// </summary>
        /// <returns></returns>
        public List<SedeComboDTO> ObtenerComboListaSedes()
        {
            try
            {
                return unitOfWork.SedeRepository.ObtenerComboListaSedes();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de nombres de las sedes segun lo registrado en los FURS
        /// </summary>
        /// <returns></returns>
        public List<SedeComboDTO> ObtenerComboPorNombreSede(string Sede)
        {
            try
            {
                return unitOfWork.SedeRepository.ObtenerComboPorNombreSede(Sede);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de nombres de las sedes segun lo registrado en los FURS
        /// </summary>
        /// <returns></returns>
        public List<SedeFiltroCiudadDTO> ObtenerListaSedesSegunFur()
        {
            try
            {
                return unitOfWork.SedeRepository.ObtenerListaSedesSegunFur();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
}
        }

        public List<FiltroDTO> ObtenerListaSedesConComprobanteDetraccion()
        {
            try
            {
                return unitOfWork.SedeRepository.ObtenerListaSedesConComprobanteDetraccion();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
