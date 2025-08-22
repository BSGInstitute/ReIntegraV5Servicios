using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.PersonalHorarioDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PersonalHorarioRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/08/2022
    /// <summary>
    /// Gestión general de T_PersonalHorario
    /// </summary>
    public class PersonalHorarioRepository : GenericRepository<TPersonalHorario>, IPersonalHorarioRepository
    {
        private Mapper _mapper;

        public PersonalHorarioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalHorario, PersonalHorario>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalHorario MapeoEntidad(PersonalHorario entidad)
        {
            try
            {
                //crea la entidad padre
                TPersonalHorario modelo = _mapper.Map<TPersonalHorario>(entidad);

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

        public TPersonalHorario Add(PersonalHorario entidad)
        {
            try
            {
                var PersonalHorario = MapeoEntidad(entidad);
                base.Insert(PersonalHorario);
                return PersonalHorario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalHorario Update(PersonalHorario entidad)
        {
            try
            {
                var PersonalHorario = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalHorario.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalHorario);
                return PersonalHorario;
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


        public IEnumerable<TPersonalHorario> Add(IEnumerable<PersonalHorario> listadoEntidad)
        {
            try
            {
                List<TPersonalHorario> listado = new List<TPersonalHorario>();
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

        public IEnumerable<TPersonalHorario> Update(IEnumerable<PersonalHorario> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalHorario> listado = new List<TPersonalHorario>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PersonalHorario.
        /// </summary>
        /// <returns> List<PersonalHorarioDTO> </returns>
        public IEnumerable<PersonalHorarioDTO> ObtenerPersonalHorario()
        {
            try
            {
                List<PersonalHorarioDTO> rpta = new List<PersonalHorarioDTO>();
                var query = @"
                    SELECT
	                    Id,IdPersonal,Lunes1,Lunes2,Lunes3,Lunes4,Martes1,Martes2,Martes3,Martes4,Miercoles1,Miercoles2,Miercoles3,MIercoles4,Jueves1,
	                    Jueves2,Jueves3,Jueves4,Viernes1,Viernes2,Viernes3,Viernes4,Sabado1,Sabado2,Sabado3,Sabado4,Domingo1,Domingo2,Domingo3,Domingo4,
	                    Activo,FechaInicio,FechaFin,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM gp.T_PersonalHorario
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalHorarioDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Horario del Asesor de Lunes a Domingos
        /// </summary>
        /// <param name="idPersonal"> Id del Personal </param>
        /// <returns> Horario del Personal : HorarioAsesorDTO </returns>
        public HorarioAsesorDTO ObtenerHorarioAsesor(int idPersonal)
        {
            try
            {
                HorarioAsesorDTO horarioAsesor = new HorarioAsesorDTO();
                var query = @"
                    SELECT
	                    Id,IdPersonal,Activo,Lunes1,Lunes2,Lunes3,Lunes4,Martes1,Martes2,Martes3,Martes4,Miercoles1,Miercoles2,Miercoles3,Miercoles4,
	                    Jueves1,Jueves2,Jueves3,Jueves4,Viernes1,Viernes2,Viernes3,Viernes4,Sabado1,Sabado2,Sabado3,Sabado4,Domingo1,Domingo2,Domingo3,
	                    Domingo4
                    FROM gp.V_TPersonalHorario_HorarioAsesor
                    WHERE IdPersonal = @idPersonal";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    horarioAsesor = JsonConvert.DeserializeObject<HorarioAsesorDTO>(resultado);
                    return horarioAsesor;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene el Horario del Asesor de Lunes a Domingos
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<HorarioDTO> ObtenerHorario(int idPersonal)
        {
            try
            {
                string _queryhorario = "Select * from gp.V_TPersonalHorario_HorarioAsesor Where IdPersonal=@IdPersonal";
                var queryhorario = _dapperRepository.QueryDapper(_queryhorario, new { IdPersonal = idPersonal });
                List<HorarioDTO> horario = JsonConvert.DeserializeObject<List<HorarioDTO>>(queryhorario);
                return horario;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<PersonalHorario> ObtenerPorIdPersonal(int idPersonal)
        {
            try
            {
                var query = @"
                    SELECT Id,
                       IdPersonal,
                       Lunes1,
                       Lunes2,
                       Lunes3,
                       Lunes4,
                       Martes1,
                       Martes2,
                       Martes3,
                       Martes4,
                       Miercoles1,
                       Miercoles2,
                       Miercoles3,
                       MIercoles4,
                       Jueves1,
                       Jueves2,
                       Jueves3,
                       Jueves4,
                       Viernes1,
                       Viernes2,
                       Viernes3,
                       Viernes4,
                       Sabado1,
                       Sabado2,
                       Sabado3,
                       Sabado4,
                       Domingo1,
                       Domingo2,
                       Domingo3,
                       Domingo4,
                       Activo,
                       FechaInicio,
                       FechaFin,
                       Estado,
                       UsuarioCreacion,
                       UsuarioModificacion,
                       FechaCreacion,
                       FechaModificacion,
                       RowVersion,
                       IdMigracion FROM gp.T_PersonalHorario 
                    WHERE IdPersonal=@idPersonal AND estado=1";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<PersonalHorario>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}
