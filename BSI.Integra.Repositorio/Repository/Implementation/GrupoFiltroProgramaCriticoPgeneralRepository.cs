using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: GrupoFiltroProgramaCriticoPgeneralRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_GrupoFiltroProgramaCriticoPgeneral
    /// </summary>
    public class GrupoFiltroProgramaCriticoPgeneralRepository : GenericRepository<TGrupoFiltroProgramaCriticoPgeneral>, IGrupoFiltroProgramaCriticoPgeneralRepository
    {
        private Mapper _mapper;

        public GrupoFiltroProgramaCriticoPgeneralRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGrupoFiltroProgramaCriticoPgeneral, GrupoFiltroProgramaCriticoPgeneral>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TGrupoFiltroProgramaCriticoPgeneral MapeoEntidad(GrupoFiltroProgramaCriticoPgeneral entidad)
        {
            try
            {
                //crea la entidad padre
                TGrupoFiltroProgramaCriticoPgeneral modelo = _mapper.Map<TGrupoFiltroProgramaCriticoPgeneral>(entidad);

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

        public TGrupoFiltroProgramaCriticoPgeneral Add(GrupoFiltroProgramaCriticoPgeneral entidad)
        {
            try
            {
                var GrupoFiltroProgramaCriticoPgeneral = MapeoEntidad(entidad);
                base.Insert(GrupoFiltroProgramaCriticoPgeneral);
                return GrupoFiltroProgramaCriticoPgeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGrupoFiltroProgramaCriticoPgeneral Update(GrupoFiltroProgramaCriticoPgeneral entidad)
        {
            try
            {
                var GrupoFiltroProgramaCriticoPgeneral = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                GrupoFiltroProgramaCriticoPgeneral.RowVersion = entidadExistente.RowVersion;

                base.Update(GrupoFiltroProgramaCriticoPgeneral);
                return GrupoFiltroProgramaCriticoPgeneral;
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


        public IEnumerable<TGrupoFiltroProgramaCriticoPgeneral> Add(IEnumerable<GrupoFiltroProgramaCriticoPgeneral> listadoEntidad)
        {
            try
            {
                List<TGrupoFiltroProgramaCriticoPgeneral> listado = new List<TGrupoFiltroProgramaCriticoPgeneral>();
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

        public IEnumerable<TGrupoFiltroProgramaCriticoPgeneral> Update(IEnumerable<GrupoFiltroProgramaCriticoPgeneral> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TGrupoFiltroProgramaCriticoPgeneral> listado = new List<TGrupoFiltroProgramaCriticoPgeneral>();
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

    }
}
