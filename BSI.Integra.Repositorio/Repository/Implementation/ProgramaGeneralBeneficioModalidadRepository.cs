using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralBeneficioModalidadRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 25/07/2023
    /// <summary>
    /// Gestión general de T_ProgramaGeneralBeneficioModalidad
    /// </summary>
    public class ProgramaGeneralBeneficioModalidadRepository : GenericRepository<TProgramaGeneralBeneficioModalidad>, IProgramaGeneralBeneficioModalidadRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralBeneficioModalidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralBeneficioModalidad, ProgramaGeneralBeneficioModalidad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TProgramaGeneralBeneficioModalidad MapeoEntidad(ProgramaGeneralBeneficioModalidad entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralBeneficioModalidad modelo = _mapper.Map<TProgramaGeneralBeneficioModalidad>(entidad);

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

        public TProgramaGeneralBeneficioModalidad Add(ProgramaGeneralBeneficioModalidad entidad)
        {
            try
            {
                var ProgramaGeneralBeneficioModalidad = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralBeneficioModalidad);
                return ProgramaGeneralBeneficioModalidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralBeneficioModalidad Update(ProgramaGeneralBeneficioModalidad entidad)
        {
            try
            {
                var ProgramaGeneralBeneficioModalidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralBeneficioModalidad.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralBeneficioModalidad);
                return ProgramaGeneralBeneficioModalidad;
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


        public IEnumerable<TProgramaGeneralBeneficioModalidad> Add(IEnumerable<ProgramaGeneralBeneficioModalidad> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralBeneficioModalidad> listado = new List<TProgramaGeneralBeneficioModalidad>();
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

        public IEnumerable<TProgramaGeneralBeneficioModalidad> Update(IEnumerable<ProgramaGeneralBeneficioModalidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralBeneficioModalidad> listado = new List<TProgramaGeneralBeneficioModalidad>();
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
        /// Obtiene todos los registros de T_ProgramaGeneralBeneficioModalidad por id
        /// </summary>
        /// <returns> ProgramaGeneralBeneficioModalidad </returns>
        public ProgramaGeneralBeneficioModalidad? ObtenerPorId(int id)
        {
            try
            {
                var query = @"      
                    SELECT 
                        Id,
		                IdProgramaGeneralBeneficio,
		                Nombre,
		                IdPGeneral,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
		            FROM com.T_ProgramaGeneralBeneficioArgumento
                        WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralBeneficioModalidad>(resultado)!;
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
