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
    public class DatoFamiliarPersonalRepository : GenericRepository<TDatoFamiliarPersonal>, IDatoFamiliarPersonalRepository
    {
        private Mapper _mapper;

        public DatoFamiliarPersonalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDatoFamiliarPersonal, DatoFamiliarPersonal>(MemberList.None).ReverseMap();
                cfg.CreateMap<DatoFamiliarPersonal, DatoFamiliarPersonalDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<DatoFamiliarPersonal, TDatoFamiliarPersonal>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TDatoFamiliarPersonal MapeoEntidad(DatoFamiliarPersonal entidad)
        {
            try
            {
                //crea la entidad padre
                TDatoFamiliarPersonal modelo = _mapper.Map<TDatoFamiliarPersonal>(entidad);

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

        public TDatoFamiliarPersonal Add(DatoFamiliarPersonal entidad)
        {
            try
            {
                var DatoFamiliarPersonal = MapeoEntidad(entidad);
                base.Insert(DatoFamiliarPersonal);
                return DatoFamiliarPersonal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDatoFamiliarPersonal Update(DatoFamiliarPersonal entidad)
        {
            try
            {
                var DatoFamiliarPersonal = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DatoFamiliarPersonal.RowVersion = entidadExistente.RowVersion;

                base.Update(DatoFamiliarPersonal);
                return DatoFamiliarPersonal;
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


        public IEnumerable<TDatoFamiliarPersonal> Add(IEnumerable<DatoFamiliarPersonal> listadoEntidad)
        {
            try
            {
                List<TDatoFamiliarPersonal> listado = new List<TDatoFamiliarPersonal>();
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

        public IEnumerable<TDatoFamiliarPersonal> Update(IEnumerable<DatoFamiliarPersonal> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDatoFamiliarPersonal> listado = new List<TDatoFamiliarPersonal>();
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


        public List<DatoFamiliarPersonalDTO> ObtenerListaFamiliarPersonal(int idPersonal)
        {
            try
            {
                List<DatoFamiliarPersonalDTO> rpta = new List<DatoFamiliarPersonalDTO>();
                var query = @"
                   SELECT Id,IdPersonal,Apellidos,DerechoHabiente,EsContactoInmediato,FechaNacimiento,IdParentescoPersonal,IdSexo,IdTipoDocumentoPersonal,Nombres,NumeroDocumento,NumeroReferencia1 as NumeroReferencia FROM gp.T_DatoFamiliarPersonal
                   WHERE Estado = 1 and IdPersonal=@idPersonal ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DatoFamiliarPersonalDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DatoFamiliarPersonal? ObtenerPorId(int Id)
        {
            try
            {
                var query = @"SELECT Id,
                               IdPersonal,
                               IdSexo,
                               IdParentescoPersonal,
                               IdTipoDocumentoPersonal,
                               Nombres,
                               Apellidos,
                               FechaNacimiento,
                               NumeroDocumento,
                               NumeroReferencia1 as NumeroReferencial,
                               NumeroReferencia2,
                               DerechoHabiente,
                               EsContactoInmediato,
                               Estado,
                               UsuarioCreacion,
                               UsuarioModificacion,
                               FechaCreacion,
                               FechaModificacion
                               FROM gp.T_DatoFamiliarPersonal
                            WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<DatoFamiliarPersonal>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
