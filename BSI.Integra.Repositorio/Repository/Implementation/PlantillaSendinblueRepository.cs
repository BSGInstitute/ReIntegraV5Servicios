using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Google.Api.Ads.AdWords.v201809;
using Mandrill.Utilities;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PlantillaSendinblueRepository
    /// Autor: Adriana Chipana.
    /// Fecha: 18/05/202
    /// <summary>
    /// Gestión general de T_PlantillaSendinblue
    /// </summary>
    public class PlantillaSendinblueRepository : GenericRepository<TPlantillaSendinblue>, IPlantillaSendinblueRepository
    {
        private Mapper _mapper;


        public PlantillaSendinblueRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap< TPlantillaSendinblue, PlantillaSendinblue>(MemberList.None).ReverseMap();
         

            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TPlantillaSendinblue MapeoEntidad(PlantillaSendinblue entidad)
        {
            try
            {
                // Crea la entidad padre
                TPlantillaSendinblue modelo = _mapper.Map<TPlantillaSendinblue>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TPlantillaSendinblue Add(PlantillaSendinblue entidad)
        {
            try
            {
                var PlantillaSendinblue = MapeoEntidad(entidad);
                base.Insert(PlantillaSendinblue);
                return PlantillaSendinblue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPlantillaSendinblue Update(PlantillaSendinblue entidad)
        {
            try
            {
                var PlantillaSendinblue = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PlantillaSendinblue.RowVersion = entidadExistente.RowVersion;

                base.Update(PlantillaSendinblue);
                return PlantillaSendinblue;
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


        public IEnumerable<TPlantillaSendinblue> Add(IEnumerable<PlantillaSendinblue> listadoEntidad)
        {
            try
            {
                List<TPlantillaSendinblue> listado = new List<TPlantillaSendinblue>();
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

        public IEnumerable<TPlantillaSendinblue> Update(IEnumerable<PlantillaSendinblue> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPlantillaSendinblue> listado = new List<TPlantillaSendinblue>();
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


        public PlantillaSendinblueDTO ObtenerPlantilllaPorId(int id)
        {
            try
            {
                PlantillaSendinblueDTO conjuntoLista = new PlantillaSendinblueDTO();

                var _query = @"
                            SELECT id, Nombre, Htmlcontenido, Htmleditado, IdPlantillaSendinblueBase FROM mkt.T_PlantillaSendinblue where id = @id and estado = 1
                            ";
                var query = _dapperRepository.FirstOrDefault(_query, new { id });

                if (!string.IsNullOrEmpty(query))
                {
                    conjuntoLista = JsonConvert.DeserializeObject<PlantillaSendinblueDTO>(query);
                }
                return conjuntoLista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PlantillaSendinblueDTO> ObtenerTodo()
        {
            try
            {
                List<PlantillaSendinblueDTO> conjuntoLista = new List<PlantillaSendinblueDTO>();

                var _query = @"
                            SELECT id, nombre, htmlcontenido, htmleditado, idplantillasendinbluebase FROM mkt.T_PlantillaSendinblue where estado = 1
                            ";
                var query = _dapperRepository.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(query))
                {
                    conjuntoLista = JsonConvert.DeserializeObject<List<PlantillaSendinblueDTO>>(query);
                }
                return conjuntoLista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



    }
}
