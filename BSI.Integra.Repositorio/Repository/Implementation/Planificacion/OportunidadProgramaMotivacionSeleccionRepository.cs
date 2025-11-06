using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class OportunidadProgramaMotivacionSeleccionRepository :GenericRepository<TOportunidadProgramaMotivacionSeleccion>, IOportunidadProgramaMotivacionSeleccionRepository
    {
        private Mapper _mapper;
        public OportunidadProgramaMotivacionSeleccionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOportunidadProgramaMotivacionSeleccion, OportunidadProgramaMotivacionSeleccion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TOportunidadProgramaMotivacionSeleccion MapeoEntidad(OportunidadProgramaMotivacionSeleccion entidad)
        {
            try
            {
                TOportunidadProgramaMotivacionSeleccion modelo = _mapper.Map<TOportunidadProgramaMotivacionSeleccion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TOportunidadProgramaMotivacionSeleccion Add(OportunidadProgramaMotivacionSeleccion entidad)
        {
            try
            {
                var OportunidadProgramaMotivacionSeleccion = MapeoEntidad(entidad);
                base.Insert(OportunidadProgramaMotivacionSeleccion);
                return OportunidadProgramaMotivacionSeleccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TOportunidadProgramaMotivacionSeleccion Update(OportunidadProgramaMotivacionSeleccion entidad)
        {
            try
            {
                var OportunidadProgramaMotivacionSeleccion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                OportunidadProgramaMotivacionSeleccion.RowVersion = entidadExistente.RowVersion;
                base.Update(OportunidadProgramaMotivacionSeleccion);
                return OportunidadProgramaMotivacionSeleccion;
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
        #endregion 
        public List<OportunidadProgramaMotivacionSeleccion> ObtenerTodoByIdOportunidad(int idOportunidad)
        {
            try
            {
                List<OportunidadProgramaMotivacionSeleccion> rpta = new();
                var query = @"
                   SELECT 
	                    Id,
	                    IdOportunidad,
	                    IdProgramaMotivacion,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_OportunidadProgramaMotivacionSeleccion WHERE Estado=1 AND IdOportunidad=@idOportunidad";
                var resultado = _dapperRepository.QueryDapper(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadProgramaMotivacionSeleccion>>(resultado);
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
