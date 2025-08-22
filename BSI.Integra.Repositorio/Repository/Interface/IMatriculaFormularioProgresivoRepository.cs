using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMatriculaFormularioProgresivoRepository : IGenericRepository<TMatriculaFormularioProgresivo>
    {
        #region Metodos Base
        TMatriculaFormularioProgresivo Add(MatriculaFormularioProgresivo entidad);
        TMatriculaFormularioProgresivo Update(MatriculaFormularioProgresivo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMatriculaFormularioProgresivo> Add(IEnumerable<MatriculaFormularioProgresivo> listadoEntidad);
        IEnumerable<TMatriculaFormularioProgresivo> Update(IEnumerable<MatriculaFormularioProgresivo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        InformacionCupoDescuentoDTO ObtenerDescuentoProfiling(string Correo);
    }
}
