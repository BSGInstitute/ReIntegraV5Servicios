using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PersonalExperienciaRepository : GenericRepository<TPersonalExperiencium>, IPersonalExperienciaRepository
    {
        private Mapper _mapper;

        public PersonalExperienciaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalExperiencium, PersonalExperiencia>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalExperiencia, PersonalExperienciaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalExperiencia, TPersonalExperiencium>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalExperiencium MapeoEntidad(PersonalExperiencia entidad)
        {
            try
            {
                //crea la entidad padre
                TPersonalExperiencium modelo = _mapper.Map<TPersonalExperiencium>(entidad);

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

        public TPersonalExperiencium Add(PersonalExperiencia entidad)
        {
            try
            {
                var PersonalExperiencia = MapeoEntidad(entidad);
                base.Insert(PersonalExperiencia);
                return PersonalExperiencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalExperiencium Update(PersonalExperiencia entidad)
        {
            try
            {
                var PersonalExperiencia = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalExperiencia.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalExperiencia);
                return PersonalExperiencia;
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


        public IEnumerable<TPersonalExperiencium> Add(IEnumerable<PersonalExperiencia> listadoEntidad)
        {
            try
            {
                List<TPersonalExperiencium> listado = new List<TPersonalExperiencium>();
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

        public IEnumerable<TPersonalExperiencium> Update(IEnumerable<PersonalExperiencia> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalExperiencium> listado = new List<TPersonalExperiencium>();
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


        public List<PersonalExperienciaDTO> ObtenerPersonalExperiencia(int idPersonal)
        {
            try
            {
                List<PersonalExperienciaDTO> rpta = new List<PersonalExperienciaDTO>();
                var query = @"
                    SELECT Id, IdPersonal, FechaIngreso, FechaRetiro, IdAreaTrabajo, IdCargo, IdEmpresa, MotivoRetiro, NombreJefeInmediato, TelefonoJefeInmediato, IdPersonalArchivo FROM gp.T_PersonalExperiencia
                    WHERE Estado = 1 and IdPersonal=@idPersonal ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalExperienciaDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PersonalExperiencia? ObtenerPorId(int Id)
        {
            try
            {
                var query = @"SELECT Id,
                               IdPersonal,
                               IdEmpresa,
                               IdAreaTrabajo,
                               IdCargo,
                               FechaIngreso,
                               FechaRetiro,
                               MotivoRetiro,
                               NombreJefeInmediato,
                               TelefonoJefeInmediato,
                               Estado,
                               UsuarioCreacion,
                               UsuarioModificacion,
                               FechaCreacion,
                               FechaModificacion,
                               RowVersion,
                               IdIndustria,
                               IdPersonalArchivo FROM gp.T_PersonalExperiencia
                            WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PersonalExperiencia>(resultado)!;
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
