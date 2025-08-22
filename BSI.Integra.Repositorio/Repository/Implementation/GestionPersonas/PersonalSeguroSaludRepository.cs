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
    public class PersonalSeguroSaludRepository : GenericRepository<TPersonalSeguroSalud>, IPersonalSeguroSaludRepository
    {
        private Mapper _mapper;

        public PersonalSeguroSaludRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalSeguroSalud, PersonalSeguroSalud>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalSeguroSalud, PersonalSeguroSaludDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalSeguroSalud, TPersonalSeguroSalud>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalSeguroSalud MapeoEntidad(PersonalSeguroSalud entidad)
        {
            try
            {
                //crea la entidad padre
                TPersonalSeguroSalud modelo = _mapper.Map<TPersonalSeguroSalud>(entidad);

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

        public TPersonalSeguroSalud Add(PersonalSeguroSalud entidad)
        {
            try
            {
                var PersonalSeguroSalud = MapeoEntidad(entidad);
                base.Insert(PersonalSeguroSalud);
                return PersonalSeguroSalud;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalSeguroSalud Update(PersonalSeguroSalud entidad)
        {
            try
            {
                var PersonalSeguroSalud = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalSeguroSalud.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalSeguroSalud);
                return PersonalSeguroSalud;
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


        public IEnumerable<TPersonalSeguroSalud> Add(IEnumerable<PersonalSeguroSalud> listadoEntidad)
        {
            try
            {
                List<TPersonalSeguroSalud> listado = new List<TPersonalSeguroSalud>();
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

        public IEnumerable<TPersonalSeguroSalud> Update(IEnumerable<PersonalSeguroSalud> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalSeguroSalud> listado = new List<TPersonalSeguroSalud>();
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


        public List<PersonalSeguroSaludDTO> ObtenerPersonalSeguroSalud(int idPersonal)
        {
            try
            {
                List<PersonalSeguroSaludDTO> rpta = new List<PersonalSeguroSaludDTO>();
                var query = @"
                    SELECT Id, IdEntidadSeguroSalud,Activo,FechaModificacion,UsuarioModificacion FROM gp.T_PersonalSeguroSalud
                    WHERE Estado = 1 and IdPersonal=@idPersonal ORDER BY FechaModificacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalSeguroSaludDTO>>(resultado);

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
