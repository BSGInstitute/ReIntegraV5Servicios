using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PanelIngresoDisponible
    /// Autor: Margiory  Ramirez Neyra.
    /// Fecha: 14/12/2022
    /// <summary>
    /// Gestión general de T_PanelIngresoDisponible
    /// </summary>
    public class PanelIngresoDisponibleRepository : GenericRepository<TPanelIngresoDisponible>, IPanelIngresoDisponibleRepository
    {
        private Mapper _mapper;

        public PanelIngresoDisponibleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPanelIngresoDisponible, PanelIngresoDisponible>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TPanelIngresoDisponible MapeoEntidad(PanelIngresoDisponible entidad)
        {
            try
            {
                //crea la entidad padre
                TPanelIngresoDisponible modelo = _mapper.Map<TPanelIngresoDisponible>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPanelIngresoDisponible Add(PanelIngresoDisponible entidad)
        {
            try
            {
                var PanelIngresoDisponible = MapeoEntidad(entidad);
                base.Insert(PanelIngresoDisponible);
                return PanelIngresoDisponible;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPanelIngresoDisponible Update(PanelIngresoDisponible entidad)
        {
            try
            {
                var PanelIngresoDisponible = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PanelIngresoDisponible.RowVersion = entidadExistente.RowVersion;

                base.Update(PanelIngresoDisponible);
                return PanelIngresoDisponible;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TPanelIngresoDisponible> Add(IEnumerable<PanelIngresoDisponible> listadoEntidad)
        {
            try
            {
                List<TPanelIngresoDisponible> listado = new List<TPanelIngresoDisponible>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TPanelIngresoDisponible> Update(IEnumerable<PanelIngresoDisponible> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPanelIngresoDisponible> listado = new List<TPanelIngresoDisponible>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        /// Autor: Margiory  Ramirez Neyra.
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PanelIngresoDisponible para mostrarse la grilla.
        /// </summary>
        /// <returns> List<> </returns>

        public List<PanelDepositoDisponibleDTO> ObtenerPanelDepositoDisponible()
        {
            try
            {
                List<PanelDepositoDisponibleDTO> panelDepositoDisponible = new List<PanelDepositoDisponibleDTO>();
                var _query = "SELECT Id,IdFormaPago,FormaPago,DiasDeposito,DiasDisponible,CuentaFeriados,CuentaFeriadosEstatales,ConsideraVSD,ConsideraDiasHabilesLunesViernes,ConsideraDiasHabilesLunesSabado,ConsideraDiasFijoSemana,IdDiaSemanaFijo,HoraCorte,MinutoCorte,PorcentajeCobro,UsuarioModificacion FROM FIN.V_ObtenerPanelIngresoDisponible  order by Id desc";
                var panelDepositoDB = _dapperRepository.QueryDapper(_query, null);
                if (!panelDepositoDB.Contains("[]") && !string.IsNullOrEmpty(panelDepositoDB))
                {
                    panelDepositoDisponible = JsonConvert.DeserializeObject<List<PanelDepositoDisponibleDTO>>(panelDepositoDB);
                }
                return panelDepositoDisponible;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

       
    }
}
