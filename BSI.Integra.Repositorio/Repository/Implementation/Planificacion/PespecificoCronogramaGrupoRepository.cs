using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PespecificoCronogramaGrupoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PespecificoCronogramaGrupo
    /// </summary>
    public class PespecificoCronogramaGrupoRepository : GenericRepository<TPespecificoCronograma>, IPespecificoCronogramaGrupoRepository
    {
        private Mapper _mapper;

        public PespecificoCronogramaGrupoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoCronograma, PespecificoCronogramaGrupo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPespecificoCronograma MapeoEntidad(PespecificoCronogramaGrupo entidad)
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

        public TPespecificoCronograma Add(PespecificoCronogramaGrupo entidad)
        {
            try
            {
                var PespecificoCronogramaGrupo = MapeoEntidad(entidad);
                base.Insert(PespecificoCronogramaGrupo);
                return PespecificoCronogramaGrupo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoCronograma Update(PespecificoCronogramaGrupo entidad)
        {
            try
            {
                var PespecificoCronogramaGrupo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PespecificoCronogramaGrupo.RowVersion = entidadExistente.RowVersion;

                base.Update(PespecificoCronogramaGrupo);
                return PespecificoCronogramaGrupo;
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


        public IEnumerable<TPespecificoCronograma> Add(IEnumerable<PespecificoCronogramaGrupo> listadoEntidad)
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

        public IEnumerable<TPespecificoCronograma> Update(IEnumerable<PespecificoCronogramaGrupo> listadoEntidad)
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
        /// Obtiene todos los registros de T_PespecificoCronogramaGrupo.
        /// </summary>
        /// <returns> List<PespecificoCronogramaGrupoDTO> </returns>
        public PespecificoCronogramaGrupo? ObtenerPorIdPespecificoPorIdPais(int idPespecifico, int idPais)
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
		                IdPEspecifico AS IdPespecifico,
		                IdPais,
		                UrlDocumentoCronogramaGrupo,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM pla.T_PEspecificoCronogramaGrupo
                    WHERE Estado = 1 
                        AND IdPEspecifico=@idPespecifico 
                        AND IdPais=idPais";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPespecifico, idPais });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<PespecificoCronogramaGrupo>(resultado)!;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorIdPespecificoPorIdPais(): {ex.Message}", ex);
            }
        }
    }
}



