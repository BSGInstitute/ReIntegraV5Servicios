using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Service: MatriculaFormularioProgresivoRepository
    /// Autor: Max Mantilla.
    /// Fecha: 26/03/2025
    /// <summary>
    /// Gestión general de T_MatriculaFormularioProgresivo
    /// </summary>
    public class MatriculaFormularioProgresivoRepository : GenericRepository<TMatriculaFormularioProgresivo>, IMatriculaFormularioProgresivoRepository
    {
        private Mapper _mapper;

        public MatriculaFormularioProgresivoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMatriculaFormularioProgresivo, MatriculaFormularioProgresivo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMatriculaFormularioProgresivo MapeoEntidad(MatriculaFormularioProgresivo entidad)
        {
            try
            {
                TMatriculaFormularioProgresivo modelo = _mapper.Map<TMatriculaFormularioProgresivo>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMatriculaFormularioProgresivo Add(MatriculaFormularioProgresivo entidad)
        {
            try
            {
                var MatriculaFormularioProgresivo = MapeoEntidad(entidad);
                base.Insert(MatriculaFormularioProgresivo);
                return MatriculaFormularioProgresivo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMatriculaFormularioProgresivo Update(MatriculaFormularioProgresivo entidad)
        {
            try
            {
                var MatriculaFormularioProgresivo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MatriculaFormularioProgresivo.RowVersion = entidadExistente.RowVersion;

                base.Update(MatriculaFormularioProgresivo);
                return MatriculaFormularioProgresivo;
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


        public IEnumerable<TMatriculaFormularioProgresivo> Add(IEnumerable<MatriculaFormularioProgresivo> listadoEntidad)
        {
            try
            {
                List<TMatriculaFormularioProgresivo> listado = new List<TMatriculaFormularioProgresivo>();
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

        public IEnumerable<TMatriculaFormularioProgresivo> Update(IEnumerable<MatriculaFormularioProgresivo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMatriculaFormularioProgresivo> listado = new List<TMatriculaFormularioProgresivo>();
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

        /// Autor: Max Mantilla
        /// Fecha: 26/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene información del cupón de descuento por el correo del usuario
        /// </summary> 
        /// <returns> InformacionCupoDescuento </returns>
        public InformacionCupoDescuentoDTO ObtenerDescuentoProfiling(string Correo)
        {
            try
            {
                var rpta = new InformacionCupoDescuentoDTO();
                var query = @"SELECT Top 1 Id, PorcentajeDescuento, CodigoDescuento,FechaRegistro, DATEADD(DAY, 7, FechaRegistro) AS FechaVencimiento FROM mkt.V_TProgressiveProfilingCodigoDescuentoCorreo_DatosCupon  
                    WHERE Correo = @Correo AND Utilizado=0 AND DATEDIFF(DAY, FechaRegistro, GETDATE()) <= 7  ORDER BY FechaRegistro DESC";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Correo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]") && resultado != "null" && resultado != null)
                {
                    rpta = JsonConvert.DeserializeObject<InformacionCupoDescuentoDTO>(resultado);
                    return rpta;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
