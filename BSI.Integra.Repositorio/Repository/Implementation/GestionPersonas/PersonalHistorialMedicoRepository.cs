using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
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
    public class PersonalHistorialMedicoRepository : GenericRepository<TPersonalHistorialMedico>, IPersonalHistorialMedicoRepository
    {
        private Mapper _mapper;

        public PersonalHistorialMedicoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalHistorialMedico, PersonalHistorialMedico>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalHistorialMedico, PersonalHistorialMedicoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalHistorialMedico, TPersonalHistorialMedico>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalHistorialMedico MapeoEntidad(PersonalHistorialMedico entidad)
        {
            try
            {
                //crea la entidad padre
                TPersonalHistorialMedico modelo = _mapper.Map<TPersonalHistorialMedico>(entidad);

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

        public TPersonalHistorialMedico Add(PersonalHistorialMedico entidad)
        {
            try
            {
                var PersonalHistorialMedico = MapeoEntidad(entidad);
                base.Insert(PersonalHistorialMedico);
                return PersonalHistorialMedico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalHistorialMedico Update(PersonalHistorialMedico entidad)
        {
            try
            {
                var PersonalHistorialMedico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalHistorialMedico.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalHistorialMedico);
                return PersonalHistorialMedico;
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


        public IEnumerable<TPersonalHistorialMedico> Add(IEnumerable<PersonalHistorialMedico> listadoEntidad)
        {
            try
            {
                List<TPersonalHistorialMedico> listado = new List<TPersonalHistorialMedico>();
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

        public IEnumerable<TPersonalHistorialMedico> Update(IEnumerable<PersonalHistorialMedico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalHistorialMedico> listado = new List<TPersonalHistorialMedico>();
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


        public List<PersonalHistorialMedicoDTO> ObtenerPersonalHistorialMedico(int idPersonal)
        {
            try
            {
                List<PersonalHistorialMedicoDTO> rpta = new List<PersonalHistorialMedicoDTO>();
                var query = @"
                    SELECT Id ,IdPersonal,Enfermedad,DetalleEnfermedad,Periodo FROM gp.T_PersonalHistorialMedico
                    WHERE Estado = 1 and IdPersonal=@idPersonal ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalHistorialMedicoDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PersonalHistorialMedico? ObtenerPorId(int Id)
        {
            try
            {
                var query = @"SELECT Id,
                               IdPersonal,
                               Enfermedad,
                               DetalleEnfermedad,
                               Periodo,
                               Estado,
                               UsuarioCreacion,
                               UsuarioModificacion,
                               FechaCreacion,
                               FechaModificacion,
                               RowVersion
                               FROM gp.T_PersonalHistorialMedico
                            WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PersonalHistorialMedico>(resultado)!;
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
