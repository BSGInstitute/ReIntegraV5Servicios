using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendingblueListaDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.CampaniaMailingWhatsapp
{
    public class AreaCampaniaGeneralDetalleRepository : GenericRepository<TCampaniaGeneralDetalleArea>, IAreaCampaniaGeneralDetalleRepository
    {
        private Mapper _mapper;
        public AreaCampaniaGeneralDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCampaniaGeneralDetalleArea, CampaniaGeneralDetalleAreaDTO>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCampaniaGeneralDetalleArea MapeoEntidad(CampaniaGeneralDetalleAreaDTO entidad)
        {
            try
            {
                TCampaniaGeneralDetalleArea modelo = _mapper.Map<TCampaniaGeneralDetalleArea>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCampaniaGeneralDetalleArea Add(CampaniaGeneralDetalleAreaDTO entidad)
        {
            try
            {
                var LandingPage = MapeoEntidad(entidad);
                base.Insert(LandingPage);
                return LandingPage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCampaniaGeneralDetalleArea AddByEntity(TCampaniaGeneralDetalleArea entidad)
        {
            try
            {
                base.Insert(entidad);
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCampaniaGeneralDetalleArea Update(CampaniaGeneralDetalleAreaDTO entidad)
        {
            try
            {
                var LandingPage = MapeoEntidad(entidad);
                int idenntidad = Convert.ToInt32(LandingPage.Id);
                var entidadExistente = base.FirstBy(w => w.Id == idenntidad, s => new { s.RowVersion });
                LandingPage.RowVersion = entidadExistente.RowVersion;

                base.Update(LandingPage);
                return LandingPage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCampaniaGeneralDetalleArea Update(TCampaniaGeneralDetalleArea entidad)
        {
            try
            {
                int idenntidad = Convert.ToInt32(entidad.Id);
                var entidadExistente = base.FirstBy(w => w.Id == idenntidad, s => new { s.RowVersion });
                entidad.RowVersion = entidadExistente.RowVersion;

                base.Update(entidad);
                return entidad;
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


        public IEnumerable<TCampaniaGeneralDetalleArea> Add(IEnumerable<CampaniaGeneralDetalleAreaDTO> listadoEntidad)
        {
            try
            {
                List<TCampaniaGeneralDetalleArea> listado = new List<TCampaniaGeneralDetalleArea>();
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
        public IEnumerable<TCampaniaGeneralDetalleArea> AddByEntity(IEnumerable<TCampaniaGeneralDetalleArea> listado)
        {
            try
            {
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TCampaniaGeneralDetalleArea> Update(IEnumerable<CampaniaGeneralDetalleAreaDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCampaniaGeneralDetalleArea> listado = new List<TCampaniaGeneralDetalleArea>();
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
        /// <summary>
        /// Se obtiene la lista de los filtros segmentos de valor Tipo por la campania general detalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objetos de clase FiltroSegmentoValorTipoDTO</returns>
        public List<FiltroSegmentoValorTipoDTO> ObtenerPorIdCampaniaGeneralDetalle(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<FiltroSegmentoValorTipoDTO> resultadoFinal = new List<FiltroSegmentoValorTipoDTO>();

                string queryDapper = "SELECT Valor, IdCategoriaObjetoFiltro FROM mkt.V_TCampaniaGeneralDetalleArea_CampaniaGeneral WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle";

                var listaRegistros = _dapperRepository.QueryDapper(queryDapper, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });
                if (!string.IsNullOrEmpty(listaRegistros) && !listaRegistros.Contains("[]"))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<FiltroSegmentoValorTipoDTO>>(listaRegistros);
                }

                return resultadoFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Eliminado logico por id de la campania general detalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <param name="usuario">Usuario responsable del cambio</param>
        /// <param name="nuevos">Lista de ids nuevos</param>
        public void EliminacionLogicoPorCampaniaGeneral(int idCampaniaGeneralDetalle, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdCampaniaGeneralDetalle == idCampaniaGeneralDetalle && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdAreaCapacitacion));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ExistFunction(int data, int id)
        {

             return base.Exist(x => x.IdAreaCapacitacion == data && x.IdCampaniaGeneralDetalle == id);
        }

        public CampaniaGeneralDetalleArea FirstById(int id)
        {
            try
            {
                TCampaniaGeneralDetalleArea entidad = base.FirstById(id);

                return _mapper.Map<CampaniaGeneralDetalleArea>(entidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public TCampaniaGeneralDetalleArea FirstBy(int item,int Id)
        {
            try
            {
                TCampaniaGeneralDetalleArea entidad = base.FirstBy(x => x.IdAreaCapacitacion == item && x.IdCampaniaGeneralDetalle == Id);

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
