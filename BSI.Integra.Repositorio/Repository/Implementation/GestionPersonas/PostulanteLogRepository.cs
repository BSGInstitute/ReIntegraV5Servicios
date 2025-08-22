using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;
using System.Transactions;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PostulanteLogRepository : GenericRepository<TPostulanteLog>, IPostulanteLogRepository
    {
        private Mapper _mapper;

        private readonly IntegraDBContext _context;

        public PostulanteLogRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPostulanteLog, PostulanteLog>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteLog, PostulanteLogDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteLog, TPostulanteLog>(MemberList.None).ReverseMap();

                //PostulanteLogv2 alterno
                //cfg.CreateMap<TPostulanteLog, PostulanteLogv2>(MemberList.None).ReverseMap();
                //cfg.CreateMap<PostulanteLogv2, PostulanteLogDTO>(MemberList.None).ReverseMap();
                //cfg.CreateMap<PostulanteLogv2, TPostulanteLog>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
            _context = context;
        }

        #region Metodos Base
        private TPostulanteLog MapeoEntidad(PostulanteLog entidad)
        {
            try
            {
                TPostulanteLog modelo = _mapper.Map<TPostulanteLog>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //private TPostulanteLog MapeoEntidadV2(PostulanteLogv2 entidad)
        //{
        //    try
        //    {
        //        TPostulanteLog modelo = _mapper.Map<TPostulanteLog>(entidad);
        //        return modelo;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //para PostulanteLogV2
        //public TPostulanteLog Add(PostulanteLogv2 entidad)
        //{
        //    try
        //    {
        //        var PostulanteLog = MapeoEntidadV2(entidad);
        //        base.Insert(PostulanteLog);
        //        return PostulanteLog;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public TPostulanteLog Add(PostulanteLog entidad)
        {
            try
            {
                var PostulanteLog = MapeoEntidad(entidad);
                base.Insert(PostulanteLog);
                return PostulanteLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteLog Update(PostulanteLog entidad)
        {
            try
            {
                var PostulanteLog = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PostulanteLog.RowVersion = entidadExistente.RowVersion;

                base.Update(PostulanteLog);
                return PostulanteLog;
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
        public IEnumerable<TPostulanteLog> Add(IEnumerable<PostulanteLog> listadoEntidad)
        {
            try
            {
                List<TPostulanteLog> listado = new List<TPostulanteLog>();
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
        public IEnumerable<TPostulanteLog> Update(IEnumerable<PostulanteLog> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPostulanteLog> listado = new List<TPostulanteLog>();
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

        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene un registro de T_PostulanteLog por el Primary Key
        /// </summary>
        /// <returns>PostulanteLog o Nulo</returns>
        public PostulanteLog? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
	                    IdPersonal,
	                    IdPostulante,
	                    IdExamen,
	                    IdProcesoSeleccion,
	                    EstadoExamen,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_PostulanteLog
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PostulanteLog>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 07/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene un registro de postulante si existe a travez de su email
        /// </summary>
        /// <param name="IdPostulante"> email, para validar la existencia</param>
        /// <param name="Clave">clave, para traer la lista de cambios</param>
        /// <returns>Lista de Postulante log, con los cambios a un campo</returns>
        public List<PostulanteLogHistorialDTO> ObtenerHistorialPostulante(int IdPostulante, string Clave)
        {
            try
            {
                List<PostulanteLogHistorialDTO> lista = new List<PostulanteLogHistorialDTO>();
                string query = "gp.SP_ObtenerHistorialPostulante";
                var res = _dapperRepository.QuerySPDapper(query, new { IdPostulante = IdPostulante, Clave = Clave });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PostulanteLogHistorialDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
