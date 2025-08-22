using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.Data.SqlClient;
using PdfSharp.Pdf.Filters;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ConjuntoListaDetalleService
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 18/05/2022
    /// <summary>
    /// Gestión general de T_ConjuntoListaDetalle
    /// </summary>
    public class ConjuntoListaDetalleService : IConjuntoListaDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConjuntoListaDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TConjuntoListum, ConjuntoListaDetalle>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }


        public List<ConjuntoListaDetalleMailingMasivoDTO> ObtenerListasMailingMasivo(int idConjuntoLista)
        {
            try
            {
                return _unitOfWork.ConjuntoListaDetalleRepository.ObtenerListasMailingMasivo(idConjuntoLista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
