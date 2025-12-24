using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    public class PreferenciaComunicacionAcademicaRepository : GenericRepository<TPreferenciaComunicacionAcademica>, IPreferenciaComunicacionAcademicaRepository
    {
        private Mapper _mapper;
        public PreferenciaComunicacionAcademicaRepository (IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreferenciaComunicacionAcademica, PreferenciaComunicacionAcademica>(MemberList.None).ReverseMap();
                cfg.CreateMap<PreferenciaComunicacionAcademica, PreferenciaComunicacionAcademicaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PreferenciaComunicacionAcademica, TPreferenciaComunicacionAcademica>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPreferenciaComunicacionAcademica MapeoEntidad(PreferenciaComunicacionAcademica entidad)
        {
            try
            {
                TPreferenciaComunicacionAcademica modelo = _mapper.Map<TPreferenciaComunicacionAcademica>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreferenciaComunicacionAcademica Add(PreferenciaComunicacionAcademica entidad)
        {
            try
            {
                var PreferenciaComunicacionAcademica = MapeoEntidad(entidad);
                base.Insert(PreferenciaComunicacionAcademica);
                return PreferenciaComunicacionAcademica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreferenciaComunicacionAcademica Update(PreferenciaComunicacionAcademica entidad)
        {
            try
            {
                var PreferenciaComunicacionAcademica = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PreferenciaComunicacionAcademica.RowVersion = entidadExistente.RowVersion;

                base.Update(PreferenciaComunicacionAcademica);
                return PreferenciaComunicacionAcademica;
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


        public IEnumerable<TPreferenciaComunicacionAcademica> Add(IEnumerable<PreferenciaComunicacionAcademica> listadoEntidad)
        {
            try
            {
                List<TPreferenciaComunicacionAcademica> listado = new List<TPreferenciaComunicacionAcademica>();
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

        public IEnumerable<TPreferenciaComunicacionAcademica> Update(IEnumerable<PreferenciaComunicacionAcademica> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPreferenciaComunicacionAcademica> listado = new List<TPreferenciaComunicacionAcademica>();
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
        public List<PreferenciaComunicacionAcademicaDTO> ObtenerPreferenciaMedioComunicacionByIdAlumno(int IdAlumno)
        {
            try
            {
                List<PreferenciaComunicacionAcademicaDTO> rpta = new List<PreferenciaComunicacionAcademicaDTO>();
                var query = @"
                    SELECT
	                    Id, IdAlumno, IdMedioComunicacion
                    FROM mkt.T_PreferenciaComunicacionAcademica
                    WHERE Estado = 1 AND IdAlumno = @IdAlumno";
                var resultado = _dapperRepository.QueryDapper(query, new { IdAlumno });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PreferenciaComunicacionAcademicaDTO>>(resultado);
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
