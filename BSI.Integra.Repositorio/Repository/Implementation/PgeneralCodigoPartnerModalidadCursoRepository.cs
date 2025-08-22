using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PgeneralCodigoPartnerModalidadCursoRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PgeneralCodigoPartnerModalidadCurso
    /// </summary>
    public class PgeneralCodigoPartnerModalidadCursoRepository : GenericRepository<TPgeneralCodigoPartnerModalidadCurso>, IPgeneralCodigoPartnerModalidadCursoRepository
    {
        private Mapper _mapper;

        public PgeneralCodigoPartnerModalidadCursoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralCodigoPartnerModalidadCurso, PgeneralCodigoPartnerModalidadCurso>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPgeneralCodigoPartnerModalidadCurso MapeoEntidad(PgeneralCodigoPartnerModalidadCurso entidad)
        {
            try
            {
                //crea la entidad padre
                TPgeneralCodigoPartnerModalidadCurso modelo = _mapper.Map<TPgeneralCodigoPartnerModalidadCurso>(entidad);

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

        public TPgeneralCodigoPartnerModalidadCurso Add(PgeneralCodigoPartnerModalidadCurso entidad)
        {
            try
            {
                var PgeneralCodigoPartnerModalidadCurso = MapeoEntidad(entidad);
                base.Insert(PgeneralCodigoPartnerModalidadCurso);
                return PgeneralCodigoPartnerModalidadCurso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralCodigoPartnerModalidadCurso Update(PgeneralCodigoPartnerModalidadCurso entidad)
        {
            try
            {
                var PgeneralCodigoPartnerModalidadCurso = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralCodigoPartnerModalidadCurso.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralCodigoPartnerModalidadCurso);
                return PgeneralCodigoPartnerModalidadCurso;
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


        public IEnumerable<TPgeneralCodigoPartnerModalidadCurso> Add(IEnumerable<PgeneralCodigoPartnerModalidadCurso> listadoEntidad)
        {
            try
            {
                List<TPgeneralCodigoPartnerModalidadCurso> listado = new List<TPgeneralCodigoPartnerModalidadCurso>();
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

        public IEnumerable<TPgeneralCodigoPartnerModalidadCurso> Update(IEnumerable<PgeneralCodigoPartnerModalidadCurso> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralCodigoPartnerModalidadCurso> listado = new List<TPgeneralCodigoPartnerModalidadCurso>();
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
        /// <returns> PgeneralCodigoPartnerModalidadCurso </returns>
        public PgeneralCodigoPartnerModalidadCurso? ObtenerPorIdModalidadCursoIdPgeneralCodigoPartner(int idModalidadCurso, int idPgeneralCodigoPartner)
        {
            try
            {
                var query = @"
                   SELECT Id,
	                    IdPgeneralCodigoPartner,
	                    IdModalidadCurso,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion 
                    FROM pla.T_PgeneralCodigoPartnerModalidadCurso
                    WHERE Estado=1 AND IdModalidadCurso=@idModalidadCurso AND IdPgeneralCodigoPartner=@idPgeneralCodigoPartner";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idModalidadCurso, idPgeneralCodigoPartner });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PgeneralCodigoPartnerModalidadCurso>(resultado)!;
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
