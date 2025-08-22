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
    public class PersonalMotivoTiempoInactividadRepository : GenericRepository<TPersonalMotivoTiempoInactividad>, IPersonalMotivoTiempoInactividadRepository
    {
        private Mapper _mapper;

        public PersonalMotivoTiempoInactividadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalMotivoTiempoInactividad, PersonalMotivoTiempoInactividad>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalMotivoTiempoInactividad, TPersonalMotivoTiempoInactividad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalMotivoTiempoInactividad MapeoEntidad(PersonalMotivoTiempoInactividad entidad)
        {
            try
            {
                //crea la entidad padre
                TPersonalMotivoTiempoInactividad modelo = _mapper.Map<TPersonalMotivoTiempoInactividad>(entidad);

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

        public TPersonalMotivoTiempoInactividad Add(PersonalMotivoTiempoInactividad entidad)
        {
            try
            {
                var PersonalMotivoTiempoInactividad = MapeoEntidad(entidad);
                base.Insert(PersonalMotivoTiempoInactividad);
                return PersonalMotivoTiempoInactividad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalMotivoTiempoInactividad Update(PersonalMotivoTiempoInactividad entidad)
        {
            try
            {
                var PersonalMotivoTiempoInactividad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalMotivoTiempoInactividad.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalMotivoTiempoInactividad);
                return PersonalMotivoTiempoInactividad;
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


        public IEnumerable<TPersonalMotivoTiempoInactividad> Add(IEnumerable<PersonalMotivoTiempoInactividad> listadoEntidad)
        {
            try
            {
                List<TPersonalMotivoTiempoInactividad> listado = new List<TPersonalMotivoTiempoInactividad>();
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

        public IEnumerable<TPersonalMotivoTiempoInactividad> Update(IEnumerable<PersonalMotivoTiempoInactividad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalMotivoTiempoInactividad> listado = new List<TPersonalMotivoTiempoInactividad>();
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

        public List<PersonalTiempoInactivoHistoricoDTO> ObtenerPeriodoInactivoHistorico(int idPersonal)
        {
            try
            {
                List<PersonalTiempoInactivoHistoricoDTO> listaTiempoInactivo = new List<PersonalTiempoInactivoHistoricoDTO>();
                string query = "SELECT Id, IdMotivoInactividad,MotivoInactividad,FechaInicio,FechaFin,Estado FROM [gp].[V_TPersonalMotivoTiempoInactividad_ObtenerHistorico] WHERE IdPersonal = @idPersonal";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal });
                if (!string.IsNullOrEmpty(resultado))
                {
                    listaTiempoInactivo = JsonConvert.DeserializeObject<List<PersonalTiempoInactivoHistoricoDTO>>(resultado);
                }
                return listaTiempoInactivo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
