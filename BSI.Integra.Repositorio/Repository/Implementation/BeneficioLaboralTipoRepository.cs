using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: BeneficioLaboralTipoRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_BeneficioLaboralTipo
    /// </summary>
    public class BeneficioLaboralTipoRepository : GenericRepository<TBeneficioLaboralTipo>, IBeneficioLaboralTipoRepository
    {
        private Mapper _mapper;

        public BeneficioLaboralTipoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TBeneficioLaboralTipo, BeneficioLaboralTipo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TBeneficioLaboralTipo MapeoEntidad(BeneficioLaboralTipo entidad)
        {
            try
            {
                //crea la entidad padre
                TBeneficioLaboralTipo modelo = _mapper.Map<TBeneficioLaboralTipo>(entidad);

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

        public TBeneficioLaboralTipo Add(BeneficioLaboralTipo entidad)
        {
            try
            {
                var BeneficioLaboralTipo = MapeoEntidad(entidad);
                base.Insert(BeneficioLaboralTipo);
                return BeneficioLaboralTipo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TBeneficioLaboralTipo Update(BeneficioLaboralTipo entidad)
        {
            try
            {
                var BeneficioLaboralTipo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                BeneficioLaboralTipo.RowVersion = entidadExistente.RowVersion;

                base.Update(BeneficioLaboralTipo);
                return BeneficioLaboralTipo;
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


        public IEnumerable<TBeneficioLaboralTipo> Add(IEnumerable<BeneficioLaboralTipo> listadoEntidad)
        {
            try
            {
                List<TBeneficioLaboralTipo> listado = new List<TBeneficioLaboralTipo>();
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

        public IEnumerable<TBeneficioLaboralTipo> Update(IEnumerable<BeneficioLaboralTipo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TBeneficioLaboralTipo> listado = new List<TBeneficioLaboralTipo>();
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


    }
}
