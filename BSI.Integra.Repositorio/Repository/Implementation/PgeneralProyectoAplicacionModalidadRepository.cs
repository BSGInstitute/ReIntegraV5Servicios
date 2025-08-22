using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PgeneralProyectoAplicacionModalidadRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PgeneralProyectoAplicacionModalidad
    /// </summary>
    public class PgeneralProyectoAplicacionModalidadRepository : GenericRepository<TPgeneralProyectoAplicacionModalidad>, IPgeneralProyectoAplicacionModalidadRepository
    {
        private Mapper _mapper;

        public PgeneralProyectoAplicacionModalidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralProyectoAplicacionModalidad, PgeneralProyectoAplicacionModalidad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPgeneralProyectoAplicacionModalidad MapeoEntidad(PgeneralProyectoAplicacionModalidad entidad)
        {
            try
            {
                //crea la entidad padre
                TPgeneralProyectoAplicacionModalidad modelo = _mapper.Map<TPgeneralProyectoAplicacionModalidad>(entidad);

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

        public TPgeneralProyectoAplicacionModalidad Add(PgeneralProyectoAplicacionModalidad entidad)
        {
            try
            {
                var PgeneralProyectoAplicacionModalidad = MapeoEntidad(entidad);
                base.Insert(PgeneralProyectoAplicacionModalidad);
                return PgeneralProyectoAplicacionModalidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralProyectoAplicacionModalidad Update(PgeneralProyectoAplicacionModalidad entidad)
        {
            try
            {
                var PgeneralProyectoAplicacionModalidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralProyectoAplicacionModalidad.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralProyectoAplicacionModalidad);
                return PgeneralProyectoAplicacionModalidad;
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


        public IEnumerable<TPgeneralProyectoAplicacionModalidad> Add(IEnumerable<PgeneralProyectoAplicacionModalidad> listadoEntidad)
        {
            try
            {
                List<TPgeneralProyectoAplicacionModalidad> listado = new List<TPgeneralProyectoAplicacionModalidad>();
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

        public IEnumerable<TPgeneralProyectoAplicacionModalidad> Update(IEnumerable<PgeneralProyectoAplicacionModalidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralProyectoAplicacionModalidad> listado = new List<TPgeneralProyectoAplicacionModalidad>();
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/08/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns> PgeneralProyectoAplicacionModalidad </returns>
        public PgeneralProyectoAplicacionModalidad? ObtenerPorIdModalidadCursoIdPgeneralProyectoAplicacion(int idModalidadCurso, int idPgeneralProyectoAplicacion)
        {
            try
            {
                var query = @"
                    SELECT 
	                    Id,
	                    IdPgeneralProyectoAplicacion,
	                    IdModalidadCurso,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_PgeneralProyectoAplicacionModalidad
                    WHERE Estado=1 AND IdModalidadCurso=@idModalidadCurso AND IdPgeneralProyectoAplicacion=@idPgeneralProyectoAplicacion";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idModalidadCurso, idPgeneralProyectoAplicacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PgeneralProyectoAplicacionModalidad>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
