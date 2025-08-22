using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PEspecificoPadreFrecuenciaSesionRepository
    /// Autor: Giancarlo Romero
    /// Fecha: 30/05/2023
    /// <summary>
    /// Gestión general de
    /// </summary>
    public class PEspecificoPadreFrecuenciaSesionRepository : GenericRepository<TPespecificoPadreFrecuenciaSesion>, IPEspecificoPadreFrecuenciaSesionRepository
    {
        private Mapper _mapper;

        public PEspecificoPadreFrecuenciaSesionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoPadreFrecuenciaSesion, PespecificoPadreFrecuenciaSesion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPespecificoPadreFrecuenciaSesion MapeoEntidad(PespecificoPadreFrecuenciaSesion entidad)
        {
            try
            {
                //crea la entidad padre
                TPespecificoPadreFrecuenciaSesion modelo = _mapper.Map<TPespecificoPadreFrecuenciaSesion>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoPadreFrecuenciaSesion Add(PespecificoPadreFrecuenciaSesion entidad)
        {
            try
            {
                var PespecificoPadreFrecuenciaSesion = MapeoEntidad(entidad);
                base.Insert(PespecificoPadreFrecuenciaSesion);
                return PespecificoPadreFrecuenciaSesion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoPadreFrecuenciaSesion Update(PespecificoPadreFrecuenciaSesion entidad)
        {
            try
            {
                var PespecificoPadreFrecuenciaSesion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PespecificoPadreFrecuenciaSesion.RowVersion = entidadExistente.RowVersion;

                base.Update(PespecificoPadreFrecuenciaSesion);
                return PespecificoPadreFrecuenciaSesion;
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


        public IEnumerable<TPespecificoPadreFrecuenciaSesion> Add(IEnumerable<PespecificoPadreFrecuenciaSesion> listadoEntidad)
        {
            try
            {
                List<TPespecificoPadreFrecuenciaSesion> listado = new List<TPespecificoPadreFrecuenciaSesion>();
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

        public IEnumerable<TPespecificoPadreFrecuenciaSesion> Update(IEnumerable<PespecificoPadreFrecuenciaSesion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecificoPadreFrecuenciaSesion> listado = new List<TPespecificoPadreFrecuenciaSesion>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 05/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los datos de la tabla por medio del Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - PespecificoPadreFrecuenciaSesion </returns>
        public PespecificoPadreFrecuenciaSesion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
                        IdPEspecificoPadreFrecuencia AS IdPespecificoPadreFrecuencia,
                        Sesion,
                        IdDiaSemana,
                        HoraInicio,
                        HoraFin,
                        Duracion,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_PEspecificoPadreFrecuenciaSesion
                    WHERE 
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PespecificoPadreFrecuenciaSesion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PEPPFSR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de V_PespecificoPadreFrecuenciaSesionDia por id
        /// </summary>
        /// <returns> List<PEspecificoPadreFrecuenciaSesionDTO> </returns>
        public IEnumerable<PEspecificoPadreFrecuenciaSesionDTO> ObtenerTodoPorPEspecificoPadreFrecuencia(int id)
        {
            try
            {
                var query = "SELECT * FROM [pla].[V_PespecificoPadreFrecuenciaSesionDia] WHERE IdPEspecificoPadreFrecuencia = @id";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PEspecificoPadreFrecuenciaSesionDTO>>(resultado)!;
                }
                return new List<PEspecificoPadreFrecuenciaSesionDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PePFSR-OTPePF-001@Error en ObtenerTodoPorPEspecificoPadreFrecuencia() {ex.Message}", ex);
            }
        }
    }
}
