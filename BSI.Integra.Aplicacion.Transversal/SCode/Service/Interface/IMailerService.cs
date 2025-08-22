using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using Mandrill.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IMailerService
    {
        void SetData(TMKMailDataDTO data);
        List<TMKMensajeIdDTO> SendMessageTask();
        List<EmailAttachment> GetAttachmentList(string urlBrochure);
        bool SetFiles(IFormFile files);
        byte[] ConvertToByte(IFormFile file);
        Task<UserInfo> Info();
        TMKMailDataDTO VerifyData();
    }
}
