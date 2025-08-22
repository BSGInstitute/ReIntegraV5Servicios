using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: BeneficioLaboralPorPeriodoRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_BeneficioLaboralPorPeriodo
    /// </summary>
    public class BeneficioLaboralPorPeriodoRepository : GenericRepository<TBeneficioLaboralPorPeriodo>, IBeneficioLaboralPorPeriodoRepository
    {
        private Mapper _mapper;

        public BeneficioLaboralPorPeriodoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TBeneficioLaboralPorPeriodo, BeneficioLaboralPorPeriodo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TBeneficioLaboralPorPeriodo MapeoEntidad(BeneficioLaboralPorPeriodo entidad)
        {
            try
            {
                //crea la entidad padre
                TBeneficioLaboralPorPeriodo modelo = _mapper.Map<TBeneficioLaboralPorPeriodo>(entidad);

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

        public TBeneficioLaboralPorPeriodo Add(BeneficioLaboralPorPeriodo entidad)
        {
            try
            {
                var BeneficioLaboralPorPeriodo = MapeoEntidad(entidad);
                base.Insert(BeneficioLaboralPorPeriodo);
                return BeneficioLaboralPorPeriodo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TBeneficioLaboralPorPeriodo Update(BeneficioLaboralPorPeriodo entidad)
        {
            try
            {
                var BeneficioLaboralPorPeriodo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                BeneficioLaboralPorPeriodo.RowVersion = entidadExistente.RowVersion;

                base.Update(BeneficioLaboralPorPeriodo);
                return BeneficioLaboralPorPeriodo;
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


        public IEnumerable<TBeneficioLaboralPorPeriodo> Add(IEnumerable<BeneficioLaboralPorPeriodo> listadoEntidad)
        {
            try
            {
                List<TBeneficioLaboralPorPeriodo> listado = new List<TBeneficioLaboralPorPeriodo>();
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

        public IEnumerable<TBeneficioLaboralPorPeriodo> Update(IEnumerable<BeneficioLaboralPorPeriodo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TBeneficioLaboralPorPeriodo> listado = new List<TBeneficioLaboralPorPeriodo>();
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
                var respuesta = base.Delete(listadoIds, usuario);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public IEnumerable<BeneficioLaboralVentasDTO> ObtenerBeneficioLaboralVentasPorPeriodo(int IdPeriodo)
        {
            try
            {
                var _query = "";
                var camposTabla = "IdAgendaTipoUsuario,TipoPersona,Sueldo,Comisiones,SistemaPensionario,RentaQuintaCategoria,EsSalud,CTS,Gratificacion,ParticipacionesUtilidades,Publicidad ";

                List<BeneficioLaboralVentasDTO> listaBeneficioLaboralPeriodo = new List<BeneficioLaboralVentasDTO>();

                _query = "SELECT " + camposTabla + " FROM FIN.V_BeneficioLaboralVentas where IdPeriodo=@IdPeriodo";

                var BeneficioLaboralDB = _dapperRepository.QueryDapper(_query, new { IdPeriodo });
                if (!BeneficioLaboralDB.Contains("[]") && !string.IsNullOrEmpty(BeneficioLaboralDB))
                {
                    listaBeneficioLaboralPeriodo = JsonConvert.DeserializeObject<List<BeneficioLaboralVentasDTO>>(BeneficioLaboralDB);
                }
                return listaBeneficioLaboralPeriodo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
