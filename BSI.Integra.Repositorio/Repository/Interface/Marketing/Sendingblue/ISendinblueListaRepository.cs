using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingBlueCampaniasDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueContactosDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendingblueListaDTO;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue
{
    public interface ISendinblueListaRepository
    {
        #region Metodos Base
        TSendinblueListum Add(CrearSendingblueListaDTO entidad);
        TSendinblueListum Update(CrearSendingblueListaDTO entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSendinblueListum> Add(IEnumerable<CrearSendingblueListaDTO> listadoEntidad);
        IEnumerable<TSendinblueListum> Update(IEnumerable<CrearSendingblueListaDTO> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        TSendinblueListum ObtenerListaPorId(int id);
        TSendinblueListum ObtenerListaPorIdSendinBlue(int id);
        IEnumerable<TSendinblueListum> ObtenerTodaslasLista();
        TSendinblueListum UpdateObjetoTabla(TSendinblueListum entidad);
        List<AlumnoDTO> ObtenerAlumnosParaSubirALista(ListaIdsDtos lista);
        public bool agregarListaAlumnos(agregarListaContactosDTO lista);


    }
}
