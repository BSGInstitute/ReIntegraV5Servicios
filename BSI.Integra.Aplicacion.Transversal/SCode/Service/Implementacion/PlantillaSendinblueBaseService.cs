using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using PdfSharp.Pdf.Filters;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PlantillaSendinblueBaseService
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 18/05/2022
    /// <summary>
    /// Gestión general de T_PlantillaSendinblueBase
    /// </summary>
    public class PlantillaSendinblueBaseService : IPlantillaSendinblueBaseService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PlantillaSendinblueBaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TConjuntoListum, PlantillaSendinblueBase>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public List<ComboDTO> ObtenerComboPlantillas()
        {
            try
            {
                var modelo = _unitOfWork.PlantillaSendinblueBaseRepository.ObtenerComboPlantillas();
                return modelo;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw ex;
            }
        }

        public List<PlantillaSendinblueBaseDTO> ObtenerComboPlantillasTodo()
        {
            try
            {
                var modelo = _unitOfWork.PlantillaSendinblueBaseRepository.ObtenerComboPlantillasTodo();
                return modelo;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw ex;
            }
        }



    }
}
