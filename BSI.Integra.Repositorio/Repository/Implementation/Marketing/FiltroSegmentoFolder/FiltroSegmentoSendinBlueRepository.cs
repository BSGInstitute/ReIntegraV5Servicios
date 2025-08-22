using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB; 
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroSegmentoFolder;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.FiltroSegmentoTipoContacto;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.FiltroSegmentoFolder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.FiltroSegmentoFolder
{
    public class FiltroSegmentoSendinBlueRepository : GenericRepository<TSendinblueContacto>, IFiltroSegmentoSendinBlueRepository
    {
        public FiltroSegmentoSendinBlueRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository):base(context, connectionFactory, dapperRepository)
        {
        }
        public List<FiltroSegmentoPanelDTO> ObtenerFiltroSegmentoPanel()
        {
            try
            {
                List<FiltroSegmentoPanelDTO> items = new List<FiltroSegmentoPanelDTO>();
                var _query = "select Id, Nombre, Descripcion, IdFiltroSegmentoTipoContacto, NombreFiltroSegmentoTipoContacto, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion, FiltroEjecutado from [mkt].[V_TFiltroSegmento_PanelDataBasica] where Estado = 1 order by FechaCreacion desc";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<FiltroSegmentoPanelDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<FiltroSegmentoCompuestoDTO> ObtenerResultadoFiltroSegmento(int id, int idFiltroSegmentoTipoContacto)
        {
            try
            {
                var listadoFiltroSegmentoCompuestos = new List<FiltroSegmentoCompuestoDTO>();

                var query = "";

                switch (idFiltroSegmentoTipoContacto)
                {
                    case 1:///alumno - exalumno
                        query = "mkt.SP_ObtenerResultadoFiltroTipoAlumno";
                        break;
                    case 2://docente
                        query = "";
                        break;
                    case 6:///prospecto
                        query = "mkt.SP_ObtenerResultadoFiltro";
                        break;
                    default:
                        break;
                }
                var listadoFiltroSegmentoDB = _dapperRepository.QuerySPDapper(query, new { IdFiltroSegmento = id });

                if (!string.IsNullOrEmpty(listadoFiltroSegmentoDB) && !listadoFiltroSegmentoDB.Contains("[]"))
                {
                    listadoFiltroSegmentoCompuestos = JsonConvert.DeserializeObject<List<FiltroSegmentoCompuestoDTO>>(listadoFiltroSegmentoDB);
                }
                return listadoFiltroSegmentoCompuestos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



    }
}
