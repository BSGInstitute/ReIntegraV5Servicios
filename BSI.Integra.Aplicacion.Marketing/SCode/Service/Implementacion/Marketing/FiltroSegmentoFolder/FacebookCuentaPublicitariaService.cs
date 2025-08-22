using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB; 
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.FacebookCuentaPublicitaria;
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


namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.FacebookCuentaPublicitaria
{
    public class FacebookCuentaPublicitariaService : IFacebookCuentaPublicitariaService
    {
        private readonly IUnitOfWork unitOfWork;

        public FacebookCuentaPublicitariaService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public List<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return unitOfWork.FacebookCuentaPublicitariaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

             public List<FacebookCuentaPublicitariaDTO> ObtenerComboFacebookCuentaPublicitaria()
        {
            try
            {
                return unitOfWork.FacebookCuentaPublicitariaRepository.ObtenerComboFacebookCuentaPublicitaria();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }







    }
}
