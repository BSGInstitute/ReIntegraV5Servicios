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
    /// Service: PlantillaSendinblueService
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 18/05/2022
    /// <summary>
    /// Gestión general de T_PlantillaSendinblue
    /// </summary>
    public class PlantillaSendinblueService : IPlantillaSendinblueService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PlantillaSendinblueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPlantillaSendinblue, PlantillaSendinblue>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public PlantillaSendinblueDTO ObtenerPlantilllaPorId(int id)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaSendinblueRepository.ObtenerPlantilllaPorId(id);
                return modelo;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw ex;
            }
        }

        public List<PlantillaSendinblueDTO> ObtenerTodo()
        {
            try
            {
                var modelo = _unitOfWork.PlantillaSendinblueRepository.ObtenerTodo();
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
