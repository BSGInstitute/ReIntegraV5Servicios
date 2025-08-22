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
    public interface IFormularioProgresivoAccionBotonService
    {
        #region Metodos Base
        List<FormularioProgresivoAccionBoton> Add(FormularioProgresivoAccionBoton entidad);
        List<FormularioProgresivoAccionBoton> Update(FormularioProgresivoAccionBoton entidad);
        bool Delete(int id, string usuario);
        #endregion
        FormularioProgresivoAccionBoton ObtenerPorId(int id);
        IEnumerable<FormularioProgresivoAccionBoton> ObtenerRegistros();
    }
}
