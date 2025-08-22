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
    /// Service: PlantillaSendinblueEtiquetaService
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 18/05/2022
    /// <summary>
    /// Gestión general de T_PlantillaSendinblueEtiqueta
    /// </summary>
    public class PlantillaSendinblueEtiquetaService : IPlantillaSendinblueEtiquetaService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PlantillaSendinblueEtiquetaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPlantillaSendinblueEtiquetum, PlantillaSendinblueEtiqueta>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }


        public List<EtiquetaPlantillaSendinblueDTO> ObtenerEtiquetasPlantilla()
        {
            try
            {
                var modelo = _unitOfWork.PlantillaSendinblueEtiquetaRepository.ObtenerEtiquetasPlantilla();
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
