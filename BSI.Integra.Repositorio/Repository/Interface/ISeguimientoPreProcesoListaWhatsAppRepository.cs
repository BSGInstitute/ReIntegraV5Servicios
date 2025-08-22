using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISeguimientoPreProcesoListaWhatsAppRepository
    {
        #region Metodos Base
        TSeguimientoPreProcesoListaWhatsApp Add(SeguimientoPreProcesoListaWhatsApp entidad);
        SeguimientoPreProcesoListaWhatsApp FirstById(int id);
        TSeguimientoPreProcesoListaWhatsApp Update(SeguimientoPreProcesoListaWhatsApp entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSeguimientoPreProcesoListaWhatsApp> Add(IEnumerable<SeguimientoPreProcesoListaWhatsApp> listadoEntidad);
        IEnumerable<TSeguimientoPreProcesoListaWhatsApp> Update(IEnumerable<SeguimientoPreProcesoListaWhatsApp> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        public bool Insert(SeguimientoPreProcesoListaWhatsApp objetoBO);


    }
}
