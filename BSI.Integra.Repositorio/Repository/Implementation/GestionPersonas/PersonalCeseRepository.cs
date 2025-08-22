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
    public class PersonalCeseRepository : GenericRepository<TPersonalCese>, IPersonalCeseRepository
    {
        private Mapper _mapper;

        public PersonalCeseRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalCese, PersonalCese>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalCese, PersonalCeseDTO>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalCese MapeoEntidad(PersonalCese entidad)
        {
            try
            {
                //crea la entidad padre
                TPersonalCese modelo = _mapper.Map<TPersonalCese>(entidad);

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

        public TPersonalCese Add(PersonalCese entidad)
        {
            try
            {
                var PersonalCese = MapeoEntidad(entidad);
                base.Insert(PersonalCese);
                return PersonalCese;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalCese Update(PersonalCese entidad)
        {
            try
            {
                var PersonalCese = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalCese.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalCese);
                return PersonalCese;
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


        public IEnumerable<TPersonalCese> Add(IEnumerable<PersonalCese> listadoEntidad)
        {
            try
            {
                List<TPersonalCese> listado = new List<TPersonalCese>();
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

        public IEnumerable<TPersonalCese> Update(IEnumerable<PersonalCese> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalCese> listado = new List<TPersonalCese>();
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
        /// Fecha: 15/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PersonalCese.
        /// </summary>
        /// <returns> List<PersonalCeseDTO> </returns>
        public PersonalCeseDTO ObtenerMotivoFechaUltimo(int idPersonal)
        {
            try
            {
                PersonalCeseDTO rpta = new PersonalCeseDTO();
                var query = @"
                    SELECT
	                    Id,IdMotivoCese,FechaCese
                    FROM gp.T_PersonalCese
                    WHERE Estado = 1 and IdPersonal=@idPersonal ORDER BY FechaCese DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<PersonalCeseDTO>(resultado);

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
