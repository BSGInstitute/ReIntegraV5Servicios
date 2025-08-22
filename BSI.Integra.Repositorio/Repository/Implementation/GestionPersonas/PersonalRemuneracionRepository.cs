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
    public class PersonalRemuneracionRepository : GenericRepository<TPersonalRemuneracion>, IPersonalRemuneracionRepository
    {
        private Mapper _mapper;

        public PersonalRemuneracionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalRemuneracion, PersonalRemuneracion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalRemuneracion, PersonalRemuneracionDTO>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalRemuneracion MapeoEntidad(PersonalRemuneracion entidad)
        {
            try
            {
                //crea la entidad padre
                TPersonalRemuneracion modelo = _mapper.Map<TPersonalRemuneracion>(entidad);

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

        public TPersonalRemuneracion Add(PersonalRemuneracion entidad)
        {
            try
            {
                var PersonalRemuneracion = MapeoEntidad(entidad);
                base.Insert(PersonalRemuneracion);
                return PersonalRemuneracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalRemuneracion Update(PersonalRemuneracion entidad)
        {
            try
            {
                var PersonalRemuneracion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalRemuneracion.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalRemuneracion);
                return PersonalRemuneracion;
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


        public IEnumerable<TPersonalRemuneracion> Add(IEnumerable<PersonalRemuneracion> listadoEntidad)
        {
            try
            {
                List<TPersonalRemuneracion> listado = new List<TPersonalRemuneracion>();
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

        public IEnumerable<TPersonalRemuneracion> Update(IEnumerable<PersonalRemuneracion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalRemuneracion> listado = new List<TPersonalRemuneracion>();
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

        /// Autor:  Marco Jose Villanueva Torres.
        /// Fecha: 22/01/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoFormacion.
        /// </summary>
        /// <returns> List<TipoFormacionDTO> </returns>
        public IEnumerable<PersonalRemuneracionDTO> Obtener(int idPersonal)
        {
            try
            {
                List<PersonalRemuneracionDTO> rpta = new List<PersonalRemuneracionDTO>();
                var query = @"
                    SELECT
	                    Id,IdPersonal,IdTipoPagoRemuneracion,IdEntidadFinanciera,NumeroCuenta,Activo,Estado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM gp.T_PersonalRemuneracion
                    WHERE Estado = 1 and IdPersonal=@idPersonal ORDER BY FechaModificacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalRemuneracionDTO>>(resultado);

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
