using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PersonalFormacionRepository : GenericRepository<TPersonalFormacion>, IPersonalFormacionRepository
    {
        private Mapper _mapper;

        public PersonalFormacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalFormacion, PersonalFormacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalFormacion, PersonalFormacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalFormacion, TPersonalFormacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalFormacion MapeoEntidad(PersonalFormacion entidad)
        {
            try
            {
                //crea la entidad padre
                TPersonalFormacion modelo = _mapper.Map<TPersonalFormacion>(entidad);

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

        public TPersonalFormacion Add(PersonalFormacion entidad)
        {
            try
            {
                var PersonalFormacion = MapeoEntidad(entidad);
                base.Insert(PersonalFormacion);
                return PersonalFormacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalFormacion Update(PersonalFormacion entidad)
        {
            try
            {
                var PersonalFormacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalFormacion.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalFormacion);
                return PersonalFormacion;
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


        public IEnumerable<TPersonalFormacion> Add(IEnumerable<PersonalFormacion> listadoEntidad)
        {
            try
            {
                List<TPersonalFormacion> listado = new List<TPersonalFormacion>();
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

        public IEnumerable<TPersonalFormacion> Update(IEnumerable<PersonalFormacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalFormacion> listado = new List<TPersonalFormacion>();
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

        public List<PersonalFormacionDTO> Obtener(int idPersonal)
        {
            try
            {
                List<PersonalFormacionDTO> rpta = new List<PersonalFormacionDTO>();
                var query = @"
                    SELECT  Id,AlaActualidad,FechaFin,FechaInicio,IdAreaFormacion,IdCentroEstudio,IdEstadoEstudio,IdPersonal,IdTipoEstudio,Logro,IdPersonalArchivo FROM gp.T_PersonalFormacion
                    WHERE Estado = 1 and IdPersonal=@idPersonal ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalFormacionDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PersonalFormacion? ObtenerPorId(int Id)
        {
            try
            {
                var query = @"SELECT Id,
                               IdPersonal,
                               IdCentroEstudio,
                               IdTipoEstudio,
                               IdAreaFormacion,
                               FechaInicio,
                               FechaFin,
                               AlaActualidad,
                               IdEstadoEstudio,
                               Logro,
                               Estado,
                               UsuarioCreacion,
                               UsuarioModificacion,
                               FechaCreacion,
                               FechaModificacion,
                               IdPersonalArchivo
                               FROM gp.T_PersonalFormacion
                            WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PersonalFormacion>(resultado)!;
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
