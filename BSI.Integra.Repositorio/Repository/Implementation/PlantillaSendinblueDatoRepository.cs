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
    /// Repositorio: PlantillaSendinblueDatoRepository
    /// Autor: Adriana Chipana.
    /// Fecha: 18/05/202
    /// <summary>
    /// Gestión general de T_PlantillaSendinblueDato
    /// </summary>
    public class PlantillaSendinblueDatoRepository : GenericRepository<TPlantillaSendinblueDato>, IPlantillaSendinblueDatoRepository
    {
        private Mapper _mapper;


        public PlantillaSendinblueDatoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap< TPlantillaSendinblueDato, PlantillaSendinblueDato>(MemberList.None).ReverseMap();
         

            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TPlantillaSendinblueDato MapeoEntidad(PlantillaSendinblueDato entidad)
        {
            try
            {
                // Crea la entidad padre
                TPlantillaSendinblueDato modelo = _mapper.Map<TPlantillaSendinblueDato>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TPlantillaSendinblueDato Add(PlantillaSendinblueDato entidad)
        {
            try
            {
                var PlantillaSendinblueDato = MapeoEntidad(entidad);
                base.Insert(PlantillaSendinblueDato);
                return PlantillaSendinblueDato;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPlantillaSendinblueDato Update(PlantillaSendinblueDato entidad)
        {
            try
            {
                var PlantillaSendinblueDato = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PlantillaSendinblueDato.RowVersion = entidadExistente.RowVersion;

                base.Update(PlantillaSendinblueDato);
                return PlantillaSendinblueDato;
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


        public IEnumerable<TPlantillaSendinblueDato> Add(IEnumerable<PlantillaSendinblueDato> listadoEntidad)
        {
            try
            {
                List<TPlantillaSendinblueDato> listado = new List<TPlantillaSendinblueDato>();
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

        public IEnumerable<TPlantillaSendinblueDato> Update(IEnumerable<PlantillaSendinblueDato> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPlantillaSendinblueDato> listado = new List<TPlantillaSendinblueDato>();
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


        public List<PlantillaSendinblueDatoDTO> ObtenerDatosPlantilllaPorId(int IdPlantillaSendinblue)
        {
            try
            {
                List<PlantillaSendinblueDatoDTO> datosPlantilla = new List<PlantillaSendinblueDatoDTO>();

                var _query = @"
   SELECT psd.id, psd.nombre, psd.valor, psd.etiqueta, psd.idplantillasendinblue, psd.IdPlantillaSendinblueTipoEtiqueta, pste.Nombre as NombreTipo FROM [mkt].[T_PlantillaSendinblueDato] psd inner JOIN mkt.T_PlantillaSendinblueTipoEtiqueta pste ON psd.IdPlantillaSendinblueTipoEtiqueta = pste.id WHERE IdPlantillaSendinblue = @IdPlantillaSendinblue
                            ";
                var query = _dapperRepository.QueryDapper(_query, new { IdPlantillaSendinblue });

                if (!string.IsNullOrEmpty(query))
                {
                    datosPlantilla = JsonConvert.DeserializeObject<List<PlantillaSendinblueDatoDTO>>(query);
                }
                return datosPlantilla;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
