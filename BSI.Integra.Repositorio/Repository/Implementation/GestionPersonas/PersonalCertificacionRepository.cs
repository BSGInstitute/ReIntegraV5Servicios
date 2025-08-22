using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
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
    public class PersonalCertificacionRepository : GenericRepository<TPersonalCertificacion>, IPersonalCertificacionRepository
    {
        private Mapper _mapper;

        public PersonalCertificacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalCertificacion, PersonalCertificacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalCertificacion, PersonalCertificacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalCertificacion, TPersonalCertificacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalCertificacion MapeoEntidad(PersonalCertificacion entidad)
        {
            try
            {
                //crea la entidad padre
                TPersonalCertificacion modelo = _mapper.Map<TPersonalCertificacion>(entidad);

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

        public TPersonalCertificacion Add(PersonalCertificacion entidad)
        {
            try
            {
                var PersonalCertificacion = MapeoEntidad(entidad);
                base.Insert(PersonalCertificacion);
                return PersonalCertificacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalCertificacion Update(PersonalCertificacion entidad)
        {
            try
            {
                var PersonalCertificacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalCertificacion.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalCertificacion);
                return PersonalCertificacion;
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


        public IEnumerable<TPersonalCertificacion> Add(IEnumerable<PersonalCertificacion> listadoEntidad)
        {
            try
            {
                List<TPersonalCertificacion> listado = new List<TPersonalCertificacion>();
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

        public IEnumerable<TPersonalCertificacion> Update(IEnumerable<PersonalCertificacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalCertificacion> listado = new List<TPersonalCertificacion>();
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
        public List<PersonalCertificacionDTO> ObtenerPersonalCertificacion(int idPersonal)
        {
            try
            {
                List<PersonalCertificacionDTO> rpta = new List<PersonalCertificacionDTO>();
                var query = @"
                    SELECT Id,IdPersonal,IdPersonalArchivo,FechaCertificacion,Programa,IdCentroEstudio,Institucion  FROM gp.T_PersonalCertificacion WHERE Estado = 1 and IdPersonal=@idPersonal ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalCertificacionDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PersonalCertificacion? ObtenerPorId(int Id)
        {
                try
                {
                    var query = @"SELECT Id,
                                   IdPersonal,
                                   Programa,
                                   Institucion,
                                   FechaCertificacion,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdPersonalArchivo,
                                   IdCentroEstudio FROM gp.T_PersonalCertificacion
                            WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PersonalCertificacion>(resultado)!;
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
