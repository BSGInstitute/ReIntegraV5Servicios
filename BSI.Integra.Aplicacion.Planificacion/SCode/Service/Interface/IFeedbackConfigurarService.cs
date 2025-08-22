using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IFeedbackConfigurarService
    {
        FeedbackConfigurarDTO Insertar(FeedbackConfigurarDTO dto, string usuario);
        IEnumerable<FeedbackConfigurarFiltroDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerComboSexo();
        
        FeedbackConfigurarDTO Actualizar(FeedbackConfigurarDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
    