using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IMatriculaFormularioProgresivoService
    {
        #region Metodos Base
        List<MatriculaFormularioProgresivo> Add(MatriculaFormularioProgresivo entidad);
        List<MatriculaFormularioProgresivo> Update(MatriculaFormularioProgresivo entidad);
        bool Delete(int id, string usuario);
        #endregion
        InformacionCupoDescuentoDTO ObtenerDescuentoProfiling(string emailUsuario);
    }
}
