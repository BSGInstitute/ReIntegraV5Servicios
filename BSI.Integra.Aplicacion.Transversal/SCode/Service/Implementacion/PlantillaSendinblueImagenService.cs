using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using PdfSharp.Pdf.Filters;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PlantillaSendinblueImagenService
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 18/05/2022
    /// <summary>
    /// Gestión general de T_PlantillaSendinblueImagen
    /// </summary>
    public class PlantillaSendinblueImagenService : IPlantillaSendinblueImagenService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PlantillaSendinblueImagenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPlantillaSendinblueImagen, PlantillaSendinblueImagen>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.PlantillaSendinblueImagenRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<PlantillaSendinblueImagenDTO> ObtenerImagenesPlantilla()
        {
            try
            {
                var modelo = _unitOfWork.PlantillaSendinblueImagenRepository.ObtenerImagenesPlantilla();
                return modelo;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw ex;
            }
        }


        public object AdjuntarArchivoMarketing(IFormFile file, string usuario)
        {
            string respuesta = string.Empty;

            try
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    respuesta = _unitOfWork.PlantillaSendinblueImagenRepository.guardarArchivos(fileBytes, file.ContentType, file.FileName);
                }

                PlantillaSendinblueImagen datos = new PlantillaSendinblueImagen();
                datos.NombreArchivo = file.FileName;
                datos.Ruta = respuesta;
                datos.Extension = file.ContentType;
                datos.FechaCreacion = DateTime.Now;
                datos.FechaModificacion = DateTime.Now;
                datos.UsuarioCreacion = usuario;
                datos.UsuarioModificacion = usuario;
                datos.Estado = true;


                _unitOfWork.PlantillaSendinblueImagenRepository.Add(datos);
                _unitOfWork.Commit();

                if (string.IsNullOrEmpty(respuesta))
                {
                    return (new { Resultado = "Error" });
                }
                else
                {
                    return (new { Resultado = "Ok", UrlArchivo = respuesta, NombreArchivo = file.FileName });
                }
            }
            catch (Exception Ex)
            {
                return (new { Resultado = "Error" });
            }
        }


    }
}
