using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IDatoContratoPersonalRepository : IGenericRepository<TDatoContratoPersonal>
    {
        #region Metodos Base
        TDatoContratoPersonal Add(DatoContratoPersonal entidad);
        TDatoContratoPersonal Update(DatoContratoPersonal entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDatoContratoPersonal> Add(IEnumerable<DatoContratoPersonal> listadoEntidad);
        IEnumerable<TDatoContratoPersonal> Update(IEnumerable<DatoContratoPersonal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<DatoContratoPersonalFiltroDTO> ObtenerContratosPorFiltro(ContratoFiltroDTO filtro);
        List<DatoContratoPersonalFiltroDTO> ObtenerContratosPorFiltrov2(ContratoFiltroDTO filtro);
        DatosFormularioPersonalDTO ObtenerDatosPersonalesFormulario(int idPersonal);
        List<DatosRemuneracionVariableDTO> ObtenerComboDatosRemuneracionVariable();
        List<ComboDTO> ObtenerComboRemuneracionTipo();

        DatoContratoPersonalDTO ObtenerByIdPersonal(int idPersonal);
        
    }
}
