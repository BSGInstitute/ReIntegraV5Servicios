using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    public interface IPreferenciaComunicacionAcademicaRepository : IGenericRepository<TPreferenciaComunicacionAcademica>
    {
        #region Metodos Base
        TPreferenciaComunicacionAcademica Add(PreferenciaComunicacionAcademica entidad);
        TPreferenciaComunicacionAcademica Update(PreferenciaComunicacionAcademica entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPreferenciaComunicacionAcademica> Add(IEnumerable<PreferenciaComunicacionAcademica> listadoEntidad);
        IEnumerable<TPreferenciaComunicacionAcademica> Update(IEnumerable<PreferenciaComunicacionAcademica> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<VPreferenciaComunicacionAcademicaMedioComunicacionDTO> ObtenerPreferenciaMedioComunicacionByIdAlumno(int IdAlumno);
    }
}
