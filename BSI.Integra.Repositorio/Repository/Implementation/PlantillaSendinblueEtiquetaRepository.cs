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
    /// Repositorio: PlantillaSendinblueEtiquetaRepository
    /// Autor: Adriana Chipana.
    /// Fecha: 18/05/202
    /// <summary>
    /// Gestión general de T_PlantillaSendinblueEtiqueta
    /// </summary>
    public class PlantillaSendinblueEtiquetaRepository : GenericRepository<TConjuntoListum>, IPlantillaSendinblueEtiquetaRepository
    {
        private Mapper _mapper;


        public PlantillaSendinblueEtiquetaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPlantillaSendinblueEtiquetum, PlantillaSendinblueEtiqueta>(MemberList.None).ReverseMap();
         

            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TConjuntoListum MapeoEntidad(PlantillaSendinblueEtiqueta entidad)
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


        public TConjuntoListum Add(PlantillaSendinblueEtiqueta entidad)
        {
            try
            {
                var PlantillaSendinblueEtiqueta = MapeoEntidad(entidad);
                base.Insert(PlantillaSendinblueEtiqueta);
                return PlantillaSendinblueEtiqueta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConjuntoListum Update(PlantillaSendinblueEtiqueta entidad)
        {
            try
            {
                var PlantillaSendinblueEtiqueta = MapeoEntidad(entidad);


                base.Update(PlantillaSendinblueEtiqueta);
                return PlantillaSendinblueEtiqueta;
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


        public IEnumerable<TConjuntoListum> Add(IEnumerable<PlantillaSendinblueEtiqueta> listadoEntidad)
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

        public IEnumerable<TConjuntoListum> Update(IEnumerable<PlantillaSendinblueEtiqueta> listadoEntidad)
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



        public List<EtiquetaPlantillaSendinblueDTO> ObtenerEtiquetasPlantilla()
        {
            try
            {
                List<EtiquetaPlantillaSendinblueDTO> conjuntoLista = new List<EtiquetaPlantillaSendinblueDTO>();

                var _query = @"SELECT eps.Id, eps.Nombre, eps.Etiqueta, teps.nombre AS NombreTipo FROM mkt.T_PlantillaSendinblueEtiqueta eps INNER JOIN mkt.T_PlantillaSendinblueTipoEtiqueta teps ON eps.PlantillaSendinblueTipoEtiqueta = teps.id where eps.estado = 1";
                var query = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(query))
                {
                    conjuntoLista = JsonConvert.DeserializeObject<List<EtiquetaPlantillaSendinblueDTO>>(query);
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
