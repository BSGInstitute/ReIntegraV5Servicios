using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB; 
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.FiltroSegmentoTipoContacto;
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


namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.FiltroSegmentoTipoContacto
{
    public class FiltroSegmentoTipoContactoService : IFiltroSegmentoTipoContactoService
    {
        private readonly IUnitOfWork unitOfWork;

        public FiltroSegmentoTipoContactoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        
             
            
          

    public List<DTO.ComboDTO> ObtenerTodoFiltro()
    {
        try
        {
            return unitOfWork.FiltroSegmentoTipoContactoRepository.ObtenerTodoFiltro();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    }
}
