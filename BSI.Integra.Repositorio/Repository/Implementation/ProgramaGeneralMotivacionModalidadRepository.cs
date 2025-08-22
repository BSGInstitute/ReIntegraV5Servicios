using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralMotivacionModalidadRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 22/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralMotivacionModalidad
    /// </summary>
    public class ProgramaGeneralMotivacionModalidadRepository : GenericRepository<TProgramaGeneralMotivacionModalidad>, IProgramaGeneralMotivacionModalidadRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralMotivacionModalidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralMotivacionModalidad, ProgramaGeneralMotivacionModalidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TProgramaGeneralMotivacionModalidad MapeoEntidad(ProgramaGeneralMotivacionModalidad entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralMotivacionModalidad modelo = _mapper.Map<TProgramaGeneralMotivacionModalidad>(entidad);

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

        public TProgramaGeneralMotivacionModalidad Add(ProgramaGeneralMotivacionModalidad entidad)
        {
            try
            {
                var ProgramaGeneralMotivacionModalidad = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralMotivacionModalidad);
                return ProgramaGeneralMotivacionModalidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralMotivacionModalidad Update(ProgramaGeneralMotivacionModalidad entidad)
        {
            try
            {
                var ProgramaGeneralMotivacionModalidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralMotivacionModalidad.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralMotivacionModalidad);
                return ProgramaGeneralMotivacionModalidad;
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


        public IEnumerable<TProgramaGeneralMotivacionModalidad> Add(IEnumerable<ProgramaGeneralMotivacionModalidad> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralMotivacionModalidad> listado = new List<TProgramaGeneralMotivacionModalidad>();
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

        public IEnumerable<TProgramaGeneralMotivacionModalidad> Update(IEnumerable<ProgramaGeneralMotivacionModalidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralMotivacionModalidad> listado = new List<TProgramaGeneralMotivacionModalidad>();
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
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ProgramaGeneralMotivacionModalidad? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                   SELECT Id	,
		                IdProgramaGeneralMotivacion,
		                IdModalidadCurso,
		                Nombre,
		                IdPGeneral AS IdPgeneral,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
	                FROM pla.T_ProgramaGeneralMotivacionModalidad
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralMotivacionModalidad>(resultado)!;
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
