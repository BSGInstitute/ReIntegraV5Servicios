using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PersonalEncargadoDeEnvioDeConsultumRepository
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_PersonalEncargadoDeEnvioDeConsultum
    /// </summary>
    public class PersonalEncargadoDeEnvioDeConsultumRepository : GenericRepository<TPersonalEncargadoDeEnvioDeConsultum>, IPersonalEncargadoDeEnvioDeConsultumRepository
    {
        private Mapper _mapper;

        public PersonalEncargadoDeEnvioDeConsultumRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalEncargadoDeEnvioDeConsultum, PersonalEncargadoDeEnvioDeConsultum>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalEncargadoDeEnvioDeConsultum MapeoEntidad(PersonalEncargadoDeEnvioDeConsultum entidad)
        {
            try
            {
     
                TPersonalEncargadoDeEnvioDeConsultum modelo = _mapper.Map<TPersonalEncargadoDeEnvioDeConsultum>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalEncargadoDeEnvioDeConsultum Add(PersonalEncargadoDeEnvioDeConsultum entidad)
        {
            try
            {
                var PersonalEncargadoDeEnvioDeConsultum = MapeoEntidad(entidad);
                base.Insert(PersonalEncargadoDeEnvioDeConsultum);
                return PersonalEncargadoDeEnvioDeConsultum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalEncargadoDeEnvioDeConsultum Update(PersonalEncargadoDeEnvioDeConsultum entidad)
        {
            try
            {
                var PersonalEncargadoDeEnvioDeConsultum = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalEncargadoDeEnvioDeConsultum.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalEncargadoDeEnvioDeConsultum);
                return PersonalEncargadoDeEnvioDeConsultum;
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


        public IEnumerable<TPersonalEncargadoDeEnvioDeConsultum> Add(IEnumerable<PersonalEncargadoDeEnvioDeConsultum> listadoEntidad)
        {
            try
            {
                List<TPersonalEncargadoDeEnvioDeConsultum> listado = new List<TPersonalEncargadoDeEnvioDeConsultum>();
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

        public IEnumerable<TPersonalEncargadoDeEnvioDeConsultum> Update(IEnumerable<PersonalEncargadoDeEnvioDeConsultum> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalEncargadoDeEnvioDeConsultum> listado = new List<TPersonalEncargadoDeEnvioDeConsultum>();
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


        public List<PersonalEncargadoDeEnvioDeConsultumObtenerDTO> ObtenerDias(int IdConfiguracionDeEnvioParaWhatsApp)
        {
            try
            {
                List<PersonalEncargadoDeEnvioDeConsultumObtenerDTO> Personal = new List<PersonalEncargadoDeEnvioDeConsultumObtenerDTO>();

                var resultado = _dapperRepository.QueryDapper("SELECT * FROM whp.V_DiasWhatsapp where IdConfiguracionDeEnvioParaWhatsApp = @IdConfiguracionDeEnvioParaWhatsApp", new { IdConfiguracionDeEnvioParaWhatsApp  });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    Personal = JsonConvert.DeserializeObject<List<PersonalEncargadoDeEnvioDeConsultumObtenerDTO>>(resultado);
                }
                return Personal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
