using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Mandrill.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    /// Repositorio: TipoDocumentoRespository
    /// Autor: Eliot Arias F.
    /// Fecha: 25/10/2024
    /// <summary>
    /// Gestión general de T_TipoDocumentoPersonal
    /// </summary>
    public class TipoDocumentoPersonalRepository : GenericRepository<TTipoDocumentoPersonal>, ITipoDocumentoPersonalRepository
    {
        private Mapper _mapper;

        public TipoDocumentoPersonalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoDocumentoPersonal, TipoDocumentoPersonal>(MemberList.None).ReverseMap();
                cfg.CreateMap<TTipoDocumentoPersonal, TipoDocumentoPersonalDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoDocumentoPersonal, TipoDocumentoPersonalDTO>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TTipoDocumentoPersonal MapeoEntidad(TipoDocumentoPersonal entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoDocumentoPersonal modelo = _mapper.Map<TTipoDocumentoPersonal>(entidad);

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

        public TTipoDocumentoPersonal Add(TipoDocumentoPersonal entidad)
        {
            try
            {
                var TipoDocumentoPersonal = MapeoEntidad(entidad);
                base.Insert(TipoDocumentoPersonal);
                return TipoDocumentoPersonal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoDocumentoPersonal Update(TipoDocumentoPersonal entidad)
        {
            try
            {
                var TipoDocumentoPersonal = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoDocumentoPersonal.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoDocumentoPersonal);
                return TipoDocumentoPersonal;
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
        //IEnumerable<TTipoDocumentoPersonal> Add(IEnumerable<TipoDocumentoPersonal> listadoEntidad)
        //{

        //}

        public IEnumerable<TTipoDocumentoPersonal> Add(IEnumerable<TipoDocumentoPersonal> listadoEntidad)
        {
            try
            {
                List<TTipoDocumentoPersonal> listado = new List<TTipoDocumentoPersonal>();
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

        public IEnumerable<TTipoDocumentoPersonal> Update(IEnumerable<TipoDocumentoPersonal> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoDocumentoPersonal> listado = new List<TTipoDocumentoPersonal>();
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

        public IEnumerable<TipoDocumentoPersonalDTO> Obtener()
        {
            try
            {
                List<TipoDocumentoPersonalDTO> rpta = new List<TipoDocumentoPersonalDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM gp.T_TipoDocumentoPersonal
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoDocumentoPersonalDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TipoDocumentoPersonalComboDTO> ObtenerComboDocumentos()
        {
            try
            {
                List<TipoDocumentoPersonalComboDTO> rpta = new List<TipoDocumentoPersonalComboDTO>();
                string queryDapper = @"SELECT * FROM gp.T_TipoDocumentoPersonal WHERE Estado = 1;";

                var resultado = _dapperRepository.QueryDapper(queryDapper, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoDocumentoPersonalComboDTO>>(resultado)!;
                    return rpta;
                }

                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
