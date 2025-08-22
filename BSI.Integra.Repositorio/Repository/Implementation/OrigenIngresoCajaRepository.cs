using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OrigenIngresoCajaRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_OrigenIngresoCaja
    /// </summary>
    public class OrigenIngresoCajaRepository : GenericRepository<TOrigenIngresoCaja>, IOrigenIngresoCajaRepository
    {
        private Mapper _mapper;

        public OrigenIngresoCajaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOrigenIngresoCaja, OrigenIngresoCaja>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TOrigenIngresoCaja MapeoEntidad(OrigenIngresoCaja entidad)
        {
            try
            {
                //crea la entidad padre
                TOrigenIngresoCaja modelo = _mapper.Map<TOrigenIngresoCaja>(entidad);

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

        public TOrigenIngresoCaja Add(OrigenIngresoCaja entidad)
        {
            try
            {
                var OrigenIngresoCaja = MapeoEntidad(entidad);
                base.Insert(OrigenIngresoCaja);
                return OrigenIngresoCaja;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOrigenIngresoCaja Update(OrigenIngresoCaja entidad)
        {
            try
            {
                var OrigenIngresoCaja = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                OrigenIngresoCaja.RowVersion = entidadExistente.RowVersion;

                base.Update(OrigenIngresoCaja);
                return OrigenIngresoCaja;
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


        public IEnumerable<TOrigenIngresoCaja> Add(IEnumerable<OrigenIngresoCaja> listadoEntidad)
        {
            try
            {
                List<TOrigenIngresoCaja> listado = new List<TOrigenIngresoCaja>();
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

        public IEnumerable<TOrigenIngresoCaja> Update(IEnumerable<OrigenIngresoCaja> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOrigenIngresoCaja> listado = new List<TOrigenIngresoCaja>();
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
        /// Autor: Griselberto Huaman
        /// Fecha: 14/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_OrigenIngresoCaja para mostrarse en combo.
        /// </summary>
        /// <returns> List<OrigenIngresoCajaComboDTO> </returns>
        public IEnumerable<OrigenIngresoCajaComboDTO> ObtenerCombo()
        {
            try
            {
                List<OrigenIngresoCajaComboDTO> rpt = this.GetBy(x => x.Estado == true, x => new OrigenIngresoCajaComboDTO { Id = x.Id, Nombre = x.Nombre.ToUpper() }).ToList();
                return rpt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
