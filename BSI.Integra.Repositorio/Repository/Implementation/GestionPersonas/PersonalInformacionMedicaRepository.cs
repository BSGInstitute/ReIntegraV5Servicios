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
    public class PersonalInformacionMedicaRepository : GenericRepository<TPersonalInformacionMedica>, IPersonalInformacionMedicaRepository
    {
        private Mapper _mapper;

        public PersonalInformacionMedicaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalInformacionMedica, PersonalInformacionMedica>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalInformacionMedica, PersonalInformacionMedicaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalInformacionMedica, TPersonalInformacionMedica>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalInformacionMedica MapeoEntidad(PersonalInformacionMedica entidad)
        {
            try
            {
                //crea la entidad padre
                TPersonalInformacionMedica modelo = _mapper.Map<TPersonalInformacionMedica>(entidad);

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

        public TPersonalInformacionMedica Add(PersonalInformacionMedica entidad)
        {
            try
            {
                var PersonalInformacionMedica = MapeoEntidad(entidad);
                base.Insert(PersonalInformacionMedica);
                return PersonalInformacionMedica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalInformacionMedica Update(PersonalInformacionMedica entidad)
        {
            try
            {
                var PersonalInformacionMedica = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalInformacionMedica.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalInformacionMedica);
                return PersonalInformacionMedica;
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


        public IEnumerable<TPersonalInformacionMedica> Add(IEnumerable<PersonalInformacionMedica> listadoEntidad)
        {
            try
            {
                List<TPersonalInformacionMedica> listado = new List<TPersonalInformacionMedica>();
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

        public IEnumerable<TPersonalInformacionMedica> Update(IEnumerable<PersonalInformacionMedica> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalInformacionMedica> listado = new List<TPersonalInformacionMedica>();
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

        public List<PersonalInformacionMedicaDTO> ObtenerPersonalInformacionMedica(int idPersonal)
        {
            try
            {
                List<PersonalInformacionMedicaDTO> rpta = new List<PersonalInformacionMedicaDTO>();
                var query = @"
                    SELECT Id,IdPersonal,Alergia,Precaucion FROM gp.T_PersonalInformacionMedica
                    WHERE Estado = 1 and IdPersonal=@idPersonal ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalInformacionMedicaDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public PersonalInformacionMedica? ObtenerPorId(int Id)
        {
            try
            {
                var query = @"SELECT  Id,
                                IdPersonal,
                                IdTipoSangre,
                                Alergia,
                                Precaucion,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion
                               FROM gp.T_PersonalInformacionMedica
                    WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PersonalInformacionMedica>(resultado)!;
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
