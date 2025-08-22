using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB; 
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.FacebookAudiencia;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.FacebookAudiencia
{
    public class FacebookAudienciaService : IFacebookAudienciaService
    {
        private readonly IUnitOfWork unitOfWork;

        public FacebookAudienciaService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public List<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return unitOfWork.FacebookAudienciaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<FacebookAudienciaHistorialDTO> ObtenerHistorialPorIdFiltroSegmento(int idFiltroSegmento)
        {
        try
        {
            return unitOfWork.FacebookAudienciaRepository.ObtenerHistorialPorIdFiltroSegmento(idFiltroSegmento);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        }
      public List<FacebookAudienciaComboDTO> ObtenerComboFacebookAudiencia()
        {
            try
            {
                return unitOfWork.FacebookAudienciaRepository.ObtenerComboFacebookAudiencia();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FacebookAudienciaComboDTO> ObtenerComboListaPublico()
        {
            try
            {
                return unitOfWork.FacebookAudienciaRepository.ObtenerComboListaPublico();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
