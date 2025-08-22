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
    /// Repositorio: PlantillaSendinblueEtiquetaPlantillaRepository
    /// Autor: Adriana Chipana.
    /// Fecha: 18/05/202
    /// <summary>
    /// Gestión general de T_PlantillaSendinblueEtiquetaPlantilla
    /// </summary>
    public class PlantillaSendinblueEtiquetaPlantillaRepository : GenericRepository<TPlantillaSendinblueEtiquetaPlantilla>, IPlantillaSendinblueEtiquetaPlantillaRepository
    {
        private Mapper _mapper;


        public PlantillaSendinblueEtiquetaPlantillaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPlantillaSendinblueEtiquetaPlantilla, PlantillaSendinblueEtiquetaPlantilla>(MemberList.None).ReverseMap();
         

            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TPlantillaSendinblueEtiquetaPlantilla MapeoEntidad(PlantillaSendinblueEtiquetaPlantilla entidad)
        {
            try
            {
                // Crea la entidad padre
                TPlantillaSendinblueEtiquetaPlantilla modelo = _mapper.Map<TPlantillaSendinblueEtiquetaPlantilla>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TPlantillaSendinblueEtiquetaPlantilla Add(PlantillaSendinblueEtiquetaPlantilla entidad)
        {
            try
            {
                var PlantillaSendinblueEtiquetaPlantilla = MapeoEntidad(entidad);
                base.Insert(PlantillaSendinblueEtiquetaPlantilla);
                return PlantillaSendinblueEtiquetaPlantilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPlantillaSendinblueEtiquetaPlantilla Update(PlantillaSendinblueEtiquetaPlantilla entidad)
        {
            try
            {
                var PlantillaSendinblueEtiquetaPlantilla = MapeoEntidad(entidad);


                base.Update(PlantillaSendinblueEtiquetaPlantilla);
                return PlantillaSendinblueEtiquetaPlantilla;
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


        public IEnumerable<TPlantillaSendinblueEtiquetaPlantilla> Add(IEnumerable<PlantillaSendinblueEtiquetaPlantilla> listadoEntidad)
        {
            try
            {
                List<TPlantillaSendinblueEtiquetaPlantilla> listado = new List<TPlantillaSendinblueEtiquetaPlantilla>();
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

        public IEnumerable<TPlantillaSendinblueEtiquetaPlantilla> Update(IEnumerable<PlantillaSendinblueEtiquetaPlantilla> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPlantillaSendinblueEtiquetaPlantilla> listado = new List<TPlantillaSendinblueEtiquetaPlantilla>();
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
        public List<EtiquetaPlantillaSendinblueDTO> ObtenerEtiquetasPorPlantilla(int id)
        {
            try
            {
                List<EtiquetaPlantillaSendinblueDTO> PlantillaSendinblueEtiquetaPlantilla = new List<EtiquetaPlantillaSendinblueDTO>();

                var _query = @"SELECT eps.nombre, eps.Etiqueta, teps.nombre AS NombreTipo, teps.id AS IdPlantillaSendinblueTipoEtiqueta FROM mkt.T_PlantillaSendinblueEtiquetaPlantilla epp INNER JOIN mkt.T_PlantillaSendinblueEtiqueta eps ON epp.IdPlantillaSendinblueEtiqueta = eps.id INNER JOIN mkt.T_PlantillaSendinblueTipoEtiqueta teps ON eps.IdPlantillaSendinblueTipoEtiqueta = teps.id where epp.estado = 1 AND epp.idPlantillaSendinblueBase = @id ";
                var query = _dapperRepository.QueryDapper(_query, new { id });
                if (!string.IsNullOrEmpty(query))
                {
                    PlantillaSendinblueEtiquetaPlantilla = JsonConvert.DeserializeObject<List<EtiquetaPlantillaSendinblueDTO>>(query);
                }
                return PlantillaSendinblueEtiquetaPlantilla;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
