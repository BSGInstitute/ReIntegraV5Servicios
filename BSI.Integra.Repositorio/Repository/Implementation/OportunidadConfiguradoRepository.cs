using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OportunidadConfiguradoRepository
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OportunidadConfigurado
    /// </summary>
    public class OportunidadConfiguradoRepository : GenericRepository<TOportunidadConfigurado>, IOportunidadConfiguradoRepository
    {
        private Mapper _mapper;

        public OportunidadConfiguradoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOportunidadConfigurado, OportunidadConfigurado>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TOportunidadConfigurado MapeoEntidad(OportunidadConfigurado entidad)
        {
            try
            {
                //crea la entidad padre
                TOportunidadConfigurado modelo = _mapper.Map<TOportunidadConfigurado>(entidad);

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

        public TOportunidadConfigurado Add(OportunidadConfigurado entidad)
        {
            try
            {
                var OportunidadConfigurado = MapeoEntidad(entidad);
                base.Insert(OportunidadConfigurado);
                return OportunidadConfigurado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOportunidadConfigurado Update(OportunidadConfigurado entidad)
        {
            try
            {
                var OportunidadConfigurado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                OportunidadConfigurado.RowVersion = entidadExistente.RowVersion;

                base.Update(OportunidadConfigurado);
                return OportunidadConfigurado;
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


        public IEnumerable<TOportunidadConfigurado> Add(IEnumerable<OportunidadConfigurado> listadoEntidad)
        {
            try
            {
                List<TOportunidadConfigurado> listado = new List<TOportunidadConfigurado>();
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

        public IEnumerable<TOportunidadConfigurado> Update(IEnumerable<OportunidadConfigurado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOportunidadConfigurado> listado = new List<TOportunidadConfigurado>();
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
