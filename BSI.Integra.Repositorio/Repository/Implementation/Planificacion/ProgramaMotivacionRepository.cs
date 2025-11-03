using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class ProgramaMotivacionRepository : GenericRepository<TProgramaMotivacion>, IProgramaMotivacionRepository
    {
        private Mapper _mapper;

        public ProgramaMotivacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaMotivacion, ProgramaMotivacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaMotivacion, ProgramaMotivacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaMotivacion, TProgramaMotivacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaMotivacion MapeoEntidad(ProgramaMotivacion entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaMotivacion modelo = _mapper.Map<TProgramaMotivacion>(entidad);

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

        public TProgramaMotivacion Add(ProgramaMotivacion entidad)
        {
            try
            {
                var ProgramaMotivacion = MapeoEntidad(entidad);
                base.Insert(ProgramaMotivacion);
                return ProgramaMotivacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaMotivacion Update(ProgramaMotivacion entidad)
        {
            try
            {
                var ProgramaMotivacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaMotivacion.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaMotivacion);
                return ProgramaMotivacion;
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


        public IEnumerable<TProgramaMotivacion> Add(IEnumerable<ProgramaMotivacion> listadoEntidad)
        {
            try
            {
                List<TProgramaMotivacion> listado = new List<TProgramaMotivacion>();
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

        public IEnumerable<TProgramaMotivacion> Update(IEnumerable<ProgramaMotivacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaMotivacion> listado = new List<TProgramaMotivacion>();
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
        public List<ProgramaMotivacion> ObtenerTodo()
        {
            try
            {
                List<ProgramaMotivacion> rpta = new();
                var query = @"
                   SELECT 
                    Id,
                    Descripcion,
                    Estado,
                    UsuarioCreacion,
                    UsuarioModificacion,
                    FechaCreacion,
                    FechaModificacion
                    FROM pla.T_ProgramaMotivacion WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaMotivacion>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaMotivacion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    	  SELECT Id,
                             Descripcion,
                             Estado,
                             UsuarioCreacion,
                             UsuarioModificacion,
                             FechaCreacion,
                             FechaModificacion,
                             RowVersion,
                             IdMigracion FROM pla.T_ProgramaMotivacion
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaMotivacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        public IEnumerable<ProgramaMotivacionDTO> Obtener()
        {
            try
            {
                List<ProgramaMotivacionDTO> rpta = new List<ProgramaMotivacionDTO>();
                var query = @"
               SELECT Id,
                      Descripcion,
               FROM pla.T_ProgramaMotivacion
               WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaMotivacionDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
