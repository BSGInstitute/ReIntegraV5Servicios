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
    /// Repositorio: PlantillaSendinblueBaseRepository
    /// Autor: Adriana Chipana.
    /// Fecha: 18/05/202
    /// <summary>
    /// Gestión general de T_PlantillaSendinblueBase
    /// </summary>
    public class PlantillaSendinblueBaseRepository : GenericRepository<TConjuntoListum>, IPlantillaSendinblueBaseRepository
    {
        private Mapper _mapper;


        public PlantillaSendinblueBaseRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPlantillaSendinblueBase, PlantillaSendinblueBase>(MemberList.None).ReverseMap();
         

            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TConjuntoListum MapeoEntidad(PlantillaSendinblueBase entidad)
        {
            try
            {
                // Crea la entidad padre
                TConjuntoListum modelo = _mapper.Map<TConjuntoListum>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TConjuntoListum Add(PlantillaSendinblueBase entidad)
        {
            try
            {
                var PlantillaSendinblueBase = MapeoEntidad(entidad);
                base.Insert(PlantillaSendinblueBase);
                return PlantillaSendinblueBase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConjuntoListum Update(PlantillaSendinblueBase entidad)
        {
            try
            {
                var PlantillaSendinblueBase = MapeoEntidad(entidad);


                base.Update(PlantillaSendinblueBase);
                return PlantillaSendinblueBase;
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


        public IEnumerable<TConjuntoListum> Add(IEnumerable<PlantillaSendinblueBase> listadoEntidad)
        {
            try
            {
                List<TConjuntoListum> listado = new List<TConjuntoListum>();
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

        public IEnumerable<TConjuntoListum> Update(IEnumerable<PlantillaSendinblueBase> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConjuntoListum> listado = new List<TConjuntoListum>();
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

        public List<ComboDTO> ObtenerComboPlantillas()
        {
            try
            {
                List<ComboDTO> conjuntoLista = new List<ComboDTO>();

                var _query = @"SELECT id, nombre FROM [mkt].[T_PlantillaSendinblueBase] where estado = 1";
                var query = _dapperRepository.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(query))
                {
                    conjuntoLista = JsonConvert.DeserializeObject<List<ComboDTO>>(query);
                }
                return conjuntoLista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PlantillaSendinblueBaseDTO> ObtenerComboPlantillasTodo()
        {
            try
            {
                List<PlantillaSendinblueBaseDTO> conjuntoLista = new List<PlantillaSendinblueBaseDTO>();

                var _query = @"SELECT Id, Nombre, HtmlContenido FROM [mkt].[T_PlantillaSendinblueBase] where estado = 1";
                var query = _dapperRepository.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(query))
                {
                    conjuntoLista = JsonConvert.DeserializeObject<List<PlantillaSendinblueBaseDTO>>(query);
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
