using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IFormularioProgresivoTipoService
    {
        #region Metodos Base
        List<FormularioProgresivoTipo> Add(FormularioProgresivoTipo entidad);
        List<FormularioProgresivoTipo> Update(FormularioProgresivoTipo entidad);
        bool Delete(int id, string usuario);
        #endregion
        FormularioProgresivoTipo ObtenerPorId(int id);
        IEnumerable<FormularioProgresivoTipo> ObtenerRegistros();
    }
}
