using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PespecificoCronogramaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PespecificoCronograma
    /// </summary>
    public class PespecificoCronogramaRepository : GenericRepository<TPespecificoCronograma>, IPespecificoCronogramaRepository
    {
        private Mapper _mapper;

        public PespecificoCronogramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoCronograma, PespecificoCronograma>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPespecificoCronograma MapeoEntidad(PespecificoCronograma entidad)
        {
            try
            {
                TPespecificoCronograma modelo = _mapper.Map<TPespecificoCronograma>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoCronograma Add(PespecificoCronograma entidad)
        {
            try
            {
                var PespecificoCronograma = MapeoEntidad(entidad);
                base.Insert(PespecificoCronograma);
                return PespecificoCronograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoCronograma Update(PespecificoCronograma entidad)
        {
            try
            {
                var PespecificoCronograma = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PespecificoCronograma.RowVersion = entidadExistente.RowVersion;

                base.Update(PespecificoCronograma);
                return PespecificoCronograma;
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


        public IEnumerable<TPespecificoCronograma> Add(IEnumerable<PespecificoCronograma> listadoEntidad)
        {
            try
            {
                List<TPespecificoCronograma> listado = new List<TPespecificoCronograma>();
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

        public IEnumerable<TPespecificoCronograma> Update(IEnumerable<PespecificoCronograma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecificoCronograma> listado = new List<TPespecificoCronograma>();
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
        /// Fecha: 23/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PespecificoCronograma.
        /// </summary>
        /// <returns> List<PespecificoCronogramaDTO> </returns>
        public PespecificoCronograma? ObtenerPorIdPespecificoPorIdPais(int idPespecifico, int idPais)
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
		                IdPEspecifico AS IdPespecifico,
		                IdPais,
		                UrlDocumentoCronograma,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM pla.T_PEspecificoCronograma
                    WHERE Estado = 1 
                        AND IdPEspecifico=@idPespecifico 
                        AND IdPais=@idPais";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPespecifico, idPais });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<PespecificoCronograma>(resultado)!;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorIdPespecificoPorIdPais(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 23/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PespecificoCronograma.
        /// </summary>
        /// <returns> List<PespecificoCronogramaDTO> </returns>
        public IEnumerable<PEspecificoCronogramaGrupalDTO> ObtenerPEspecificoCronogramaGrupalPorIdPEspecifico(int idPespecifico)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdPEspecifico,
	                    FechaHoraInicio,
	                    Duracion,
	                    DuracionTotal,
	                    Curso,
	                    Tipo,
	                    Grupo
                    FROM pla.V_ObtenerPEspecifico_CronogramaGrupal
                    WHERE Id=@idPEspecifico;";
                var resultado = _dapperRepository.QueryDapper(query, new { idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    return JsonConvert.DeserializeObject<IEnumerable<PEspecificoCronogramaGrupalDTO>>(resultado)!;
                return new List<PEspecificoCronogramaGrupalDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPEspecificoCronogramaGrupalPorIdPEspecifico(): {ex.Message}", ex);
            }
        }
    }
}



