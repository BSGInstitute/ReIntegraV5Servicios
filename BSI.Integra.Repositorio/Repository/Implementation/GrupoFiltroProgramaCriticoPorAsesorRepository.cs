using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: GrupoFiltroProgramaCriticoPorAsesorRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_GrupoFiltroProgramaCriticoPorAsesor
    /// </summary>
    public class GrupoFiltroProgramaCriticoPorAsesorRepository : GenericRepository<TGrupoFiltroProgramaCriticoPorAsesor>, IGrupoFiltroProgramaCriticoPorAsesorRepository
    {
        private Mapper _mapper;

        public GrupoFiltroProgramaCriticoPorAsesorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGrupoFiltroProgramaCriticoPorAsesor, GrupoFiltroProgramaCriticoPorAsesor>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TGrupoFiltroProgramaCriticoPorAsesor MapeoEntidad(GrupoFiltroProgramaCriticoPorAsesor entidad)
        {
            try
            {
                //crea la entidad padre
                TGrupoFiltroProgramaCriticoPorAsesor modelo = _mapper.Map<TGrupoFiltroProgramaCriticoPorAsesor>(entidad);

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

        public TGrupoFiltroProgramaCriticoPorAsesor Add(GrupoFiltroProgramaCriticoPorAsesor entidad)
        {
            try
            {
                var GrupoFiltroProgramaCriticoPorAsesor = MapeoEntidad(entidad);
                base.Insert(GrupoFiltroProgramaCriticoPorAsesor);
                return GrupoFiltroProgramaCriticoPorAsesor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGrupoFiltroProgramaCriticoPorAsesor Update(GrupoFiltroProgramaCriticoPorAsesor entidad)
        {
            try
            {
                var GrupoFiltroProgramaCriticoPorAsesor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                GrupoFiltroProgramaCriticoPorAsesor.RowVersion = entidadExistente.RowVersion;

                base.Update(GrupoFiltroProgramaCriticoPorAsesor);
                return GrupoFiltroProgramaCriticoPorAsesor;
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


        public IEnumerable<TGrupoFiltroProgramaCriticoPorAsesor> Add(IEnumerable<GrupoFiltroProgramaCriticoPorAsesor> listadoEntidad)
        {
            try
            {
                List<TGrupoFiltroProgramaCriticoPorAsesor> listado = new List<TGrupoFiltroProgramaCriticoPorAsesor>();
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

        public IEnumerable<TGrupoFiltroProgramaCriticoPorAsesor> Update(IEnumerable<GrupoFiltroProgramaCriticoPorAsesor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TGrupoFiltroProgramaCriticoPorAsesor> listado = new List<TGrupoFiltroProgramaCriticoPorAsesor>();
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
