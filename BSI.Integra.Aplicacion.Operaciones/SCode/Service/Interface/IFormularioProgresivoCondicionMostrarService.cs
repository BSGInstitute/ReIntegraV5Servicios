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
    public interface IFormularioProgresivoCondicionMostrarService
    {
        #region Metodos Base
        List<FormularioProgresivoCondicionMostrar> Add(FormularioProgresivoCondicionMostrar entidad);
        List<FormularioProgresivoCondicionMostrar> Update(FormularioProgresivoCondicionMostrar entidad);
        bool Delete(int id, string usuario);
        #endregion
        FormularioProgresivoCondicionMostrar ObtenerPorId(int id);
        IEnumerable<FormularioProgresivoCondicionMostrar> ObtenerRegistros();
    }
}
