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
    public class PersonalSistemaPensionarioRepository : GenericRepository<TPersonalSistemaPensionario>, IPersonalSistemaPensionarioRepository
    {
        private Mapper _mapper;

        public PersonalSistemaPensionarioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalSistemaPensionario, PersonalSistemaPensionario>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalSistemaPensionario, PersonalSistemaPensionarioDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalSistemaPensionario, TPersonalSistemaPensionario>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalSistemaPensionario MapeoEntidad(PersonalSistemaPensionario entidad)
        {
            try
            {
                //crea la entidad padre
                TPersonalSistemaPensionario modelo = _mapper.Map<TPersonalSistemaPensionario>(entidad);

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

        public TPersonalSistemaPensionario Add(PersonalSistemaPensionario entidad)
        {
            try
            {
                var PersonalSistemaPensionario = MapeoEntidad(entidad);
                base.Insert(PersonalSistemaPensionario);
                return PersonalSistemaPensionario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalSistemaPensionario Update(PersonalSistemaPensionario entidad)
        {
            try
            {
                var PersonalSistemaPensionario = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalSistemaPensionario.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalSistemaPensionario);
                return PersonalSistemaPensionario;
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


        public IEnumerable<TPersonalSistemaPensionario> Add(IEnumerable<PersonalSistemaPensionario> listadoEntidad)
        {
            try
            {
                List<TPersonalSistemaPensionario> listado = new List<TPersonalSistemaPensionario>();
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

        public IEnumerable<TPersonalSistemaPensionario> Update(IEnumerable<PersonalSistemaPensionario> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalSistemaPensionario> listado = new List<TPersonalSistemaPensionario>();
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


        public List<PersonalSistemaPensionarioDTO> ObtenerPersonalSistemaPensionario(int idPersonal)
        {
            try
            {
                List<PersonalSistemaPensionarioDTO> rpta = new List<PersonalSistemaPensionarioDTO>();
                var query = @"
                    SELECT Id,IdSistemaPensionario,IdEntidadSistemaPensionario,CodigoAfiliado,Activo,FechaModificacion,UsuarioModificacion  FROM gp.T_PersonalSistemaPensionario
                    WHERE Estado = 1 and IdPersonal=@idPersonal ORDER BY FechaModificacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalSistemaPensionarioDTO>>(resultado);

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


