using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BBSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.TSendingblueContactosDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp.CampaniaMailingWhatsAppFiltradoDTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.CampaniaMailingWhatsapp
{
    public class CampaniaMailingFiltradoRepositorio : GenericRepository<TFiltradoDeDatosPorPrioridadMailing>, ICampaniaMailingFiltradoRepositorio
    {
        public CampaniaMailingFiltradoRepositorio(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
        }

        public bool FiltradoDeDatosParaMailing(CampaniaMailingFiltrado datosFiltro)
        {
            try
            {
                var query = "[mkt].[SP_FiltradoFinalDeDataMailing]";
                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    datosFiltro.IdcampaniaGeneral,
                    datosFiltro.usuario,
                    datosFiltro.IdFiltroSegmento
                });
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<TFiltradoDeDatosPorPrioridadMailing> FiltradoDeDatosParaMailingObtenerData(int IdcampaniaGeneral, int Prioridad)
        {
            var datos = base.GetBy(x=>x.IdcampaniaGeneral==IdcampaniaGeneral && x.Prioridad==Prioridad && x.Estado==true).ToList();
            return datos;
        }

        public List<CampaniaMailingSendingBlueFiltroMailing> FiltradoDeDatosParaMailingObtenerDataMailing(int IdcampaniaGeneral, int Prioridad)
        {
            List<CampaniaMailingSendingBlueFiltroMailing> datos = new List<CampaniaMailingSendingBlueFiltroMailing>();
            var sql = "select a.id,a.Nombre1 as nombre,fm.Email,a.Celular as Telefono from mkt.T_FiltradoDeDatosPorPrioridadMailing  as fm INNER JOIN MKT.T_Alumno as a ON  fm.idAlumno = a.Id where IdcampaniaGeneral=@IdcampaniaGeneral AND Prioridad=@Prioridad AND fm.estado=1";
            var resultado = _dapperRepository.QueryDapper(sql, new
            {
                IdcampaniaGeneral,
                Prioridad
            });
            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
            {
                datos = JsonConvert.DeserializeObject<List<CampaniaMailingSendingBlueFiltroMailing>>(resultado);
            }

            return datos;
        }
        public bool EliminarFiltradoPasado(int IdcampaniaGeneral,string usuario)
        {
            try
            {
                List<CampaniaMailingSendingBlueFiltroMailing> datos = new List<CampaniaMailingSendingBlueFiltroMailing>();
                var sql = "UPDATE mkt.T_FiltradoDeDatosPorPrioridadMailing  set Estado=0, usuarioModificacion=@usuario where IdcampaniaGeneral=@IdcampaniaGeneral AND estado=1";
                var resultado = _dapperRepository.QueryDapper(sql, new
                {
                    usuario,
                    IdcampaniaGeneral,
                });
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
        public List<TFiltradoDeDatosPorPrioridadMailing> FiltradoDeDatosParaMailingObtenerData(int IdcampaniaGeneral)
        {
            var datos = base.GetBy(x => x.IdcampaniaGeneral == IdcampaniaGeneral && x.Estado == true).ToList();
            return datos;
        }
    }
}
