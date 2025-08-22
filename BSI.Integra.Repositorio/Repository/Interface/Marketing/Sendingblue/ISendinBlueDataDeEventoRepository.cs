using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue
{
    public interface ISendinBlueDataDeEventoRepository
    {
        TSendinBlueDataDeEvento Add(SendinBlueDataDeEvento entidad);
        TSendinBlueDataDeEvento Update(SendinBlueDataDeEvento entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSendinBlueDataDeEvento> Add(IEnumerable<SendinBlueDataDeEvento> listadoEntidad);
        IEnumerable<TSendinBlueDataDeEvento> Update(IEnumerable<SendinBlueDataDeEvento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        List<SendinblueReporteMarketingDTO> ObtenerReproteSendinBlue(SendinblueReporteParametrosDTO parametrosConsulta);
    }
}
