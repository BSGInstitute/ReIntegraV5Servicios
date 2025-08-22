using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: MandrilEnvioCorreoRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 24/08/2022
    /// <summary>
    /// Gestión general de T_MandrilEnvioCorreo
    /// </summary>
    public class MandrilEnvioCorreoRepository : GenericRepository<TMandrilEnvioCorreo>, IMandrilEnvioCorreoRepository
    {
        private Mapper _mapper;

        public MandrilEnvioCorreoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMandrilEnvioCorreo, MandrilEnvioCorreo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        private TMandrilEnvioCorreo MapeoEntidad(MandrilEnvioCorreo entidad)
        {
            try
            {
                //crea la entidad padre
                TMandrilEnvioCorreo modelo = _mapper.Map<TMandrilEnvioCorreo>(entidad);

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

        public TMandrilEnvioCorreo Add(MandrilEnvioCorreo entidad)
        {
            try
            {
                var MandrilEnvioCorreo = MapeoEntidad(entidad);
                base.Insert(MandrilEnvioCorreo);
                return MandrilEnvioCorreo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TMandrilEnvioCorreo> Add(IEnumerable<MandrilEnvioCorreo> listadoEntidad)
        {
            try
            {
                List<TMandrilEnvioCorreo> listado = new List<TMandrilEnvioCorreo>();
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
        public IEnumerable<TMandrilEnvioCorreo> AddSync(IEnumerable<MandrilEnvioCorreo> listadoEntidad)
        {
            try
            {
                List<TMandrilEnvioCorreo> listado = new List<TMandrilEnvioCorreo>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.InsertAsync(listado);
                return listado;
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

        public TMandrilEnvioCorreo Update(MandrilEnvioCorreo entidad)
        {
            try
            {
                var MandrilEnvioCorreo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MandrilEnvioCorreo.RowVersion = entidadExistente.RowVersion;

                base.Update(MandrilEnvioCorreo);
                return MandrilEnvioCorreo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TMandrilEnvioCorreo> Update(IEnumerable<MandrilEnvioCorreo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMandrilEnvioCorreo> listado = new List<TMandrilEnvioCorreo>();
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
        public bool ValidarEnvioCorreo(int idOportunidad, string usuario)
        {
            try
            {
                string query = @"
                        SELECT Id FROM mkt.T_MandrilEnvioCorreo WITH(NOLOCK)
                        WHERE IdOportunidad = @IdOportunidad
                            AND Estado = 1
                            AND UsuarioCreacion = @Usuario";
                string resultado = _dapperRepository.FirstOrDefault(query, new { IdOportunidad = idOportunidad, Usuario = usuario });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
