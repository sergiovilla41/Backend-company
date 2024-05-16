using EnviromentConfig;
using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.Models.Columnas;
using SimemNetAdmin.Domain.ViewModel.Colums;
using SimemNetAdmin.Infra.Data.Context;
using SimemNetAdmin.Infra.Data.Repository.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ColumnaDestino
{
    [TestClass]
    public class ColumnaDestinoRepositoryTest
    {
        private ConfiguracionColumnasDestinoRepository _columnaDestinoRepository;
        private readonly SimemNetAdminDbContext _dbContext;


        public ColumnaDestinoRepositoryTest()
        {
            _dbContext = new SimemNetAdminDbContext();
            _columnaDestinoRepository = new ConfiguracionColumnasDestinoRepository();
            Connection.ConfigureConnections();
        }

        [TestMethod]
        public async Task ListColumnaDestino_ShouldReturnAllColumns()
        {

            var result = await _columnaDestinoRepository.ListColumnaDestino();


            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }
        [TestMethod]
        public async Task DataShouldMapToDTO()
        {

            await SeedTestData();
            var result = await _columnaDestinoRepository.ListColumnaDestino();
            Assert.IsNotNull(result);

            foreach (var item in result)
            {
                Assert.IsNotNull(item.IdColumnaDestino);
                Assert.IsNotNull(item.NombreColumnaDestino);
                Assert.IsNotNull(item.TipoDato);
                Assert.IsNotNull(item.Estado);
            }
        }

        private async Task SeedTestData()
        {

            await _dbContext.ColumnasDestino.AddRangeAsync(new[]
            {
                new ConfiguracionColumnasDestino
                {
                    IdColumnaDestino = Guid.NewGuid(),
                    NombreColumnaDestino = "Nombre1",
                    TipoDato = "Tipo1",
                    AtributoVariable = "Atributo1",
                    Estado = true,
                    Descripcion = "Descripción1",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                },
                new ConfiguracionColumnasDestino
                {
                    IdColumnaDestino = Guid.NewGuid(),
                    NombreColumnaDestino = "Nombre2",
                    TipoDato = "Tipo2",
                    AtributoVariable = "Atributo2",
                    Estado = false,
                    Descripcion = "Descripción2",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                }
            });

            await _dbContext.SaveChangesAsync();
        }
        [TestMethod]
        public async Task ListColumnaDestino_ShouldReturnSameNumberOfRecordsAsInTable()
        {

            await SeedTestData();
            var result = await _columnaDestinoRepository.ListColumnaDestino();
            var expectedCount = await _dbContext.ColumnasDestino.CountAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCount, result.Count);
        }
        [TestMethod]
        public async Task UpdateColumnaDestino_ShouldUpdateSuccessfully()
        {
          
            var idColumnaDestino = Guid.NewGuid();
            var fechaCreacion = DateTime.Now;

            var columnaDestinoDTO = new ConfiguracionColumnasDestinoDTO
            {
                IdColumnaDestino = idColumnaDestino,
                NombreColumnaDestino = "NuevoNombre",
                TipoDato = "NuevoTipo",
                AtributoVariable = null,
                VariableId = null,
                Estado = true,
                Descripcion = "NuevaDescripción",
                FechaCreacion = fechaCreacion,
                FechaActualizacion = DateTime.Now
            };

            
            var initialData = new ConfiguracionColumnasDestino
            {
                IdColumnaDestino = idColumnaDestino,
                NombreColumnaDestino = "NombreAntiguo",
                TipoDato = "TipoAntiguo",
                AtributoVariable = null,
                VariableId = null,
                Estado = false,
                Descripcion = "DescripciónAntigua",
                FechaCreacion = fechaCreacion,
                FechaActualizacion = DateTime.Now
            };
            await _dbContext.ColumnasDestino.AddAsync(initialData);
            await _dbContext.SaveChangesAsync();

            
            await _columnaDestinoRepository.UpdateColumnaDestinoAsync(columnaDestinoDTO);

            
            var updatedColumnaDestino = await _dbContext.ColumnasDestino
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdColumnaDestino == columnaDestinoDTO.IdColumnaDestino);

            Assert.IsNotNull(updatedColumnaDestino);
            Assert.AreEqual(columnaDestinoDTO.NombreColumnaDestino, updatedColumnaDestino.NombreColumnaDestino);
            Assert.AreEqual(columnaDestinoDTO.TipoDato, updatedColumnaDestino.TipoDato);
            Assert.AreEqual(columnaDestinoDTO.AtributoVariable, updatedColumnaDestino.AtributoVariable);
            Assert.AreEqual(columnaDestinoDTO.VariableId, updatedColumnaDestino.VariableId);
            Assert.AreEqual(columnaDestinoDTO.Estado, updatedColumnaDestino.Estado);
            Assert.AreEqual(columnaDestinoDTO.Descripcion, updatedColumnaDestino.Descripcion);

            
            Assert.AreEqual(columnaDestinoDTO.FechaCreacion?.ToString("yyyy-MM-dd HH:mm:ss"), updatedColumnaDestino?.FechaCreacion?.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.AreEqual(columnaDestinoDTO.FechaActualizacion?.ToString("yyyy-MM-dd HH:mm:ss"), updatedColumnaDestino?.FechaActualizacion?.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] 
        public async Task UpdateColumnaDestinoAsync_ShouldThrowExceptionIfNotFound()
        {
            
            var nonExistentId = Guid.NewGuid();
            var columnaDestinoDTO = new ConfiguracionColumnasDestinoDTO
            {
                IdColumnaDestino = nonExistentId,
              
            };

            await _columnaDestinoRepository.UpdateColumnaDestinoAsync(columnaDestinoDTO);

           
        }
        [TestMethod]
        public async Task CreateColumnaDestino_ShouldCreateSuccessfully()
        {
            
            var columnaDestinoDTO = new ConfiguracionColumnasDestinoDTO
            {
                
                NombreColumnaDestino = "NuevoNombre",
                TipoDato = "NuevoTipo",
                AtributoVariable = null,
                VariableId = null,
                Estado = true,
                Descripcion = "NuevaDescripción",
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now
            };

            
            var newColumnaDestinoId = await _columnaDestinoRepository.CreateColumnaDestino(columnaDestinoDTO);

            
            var createdColumnaDestino = await _dbContext.ColumnasDestino.FindAsync(newColumnaDestinoId);
            Assert.IsNotNull(createdColumnaDestino);
            Assert.AreEqual(columnaDestinoDTO.NombreColumnaDestino, createdColumnaDestino.NombreColumnaDestino);
            Assert.AreEqual(columnaDestinoDTO.TipoDato, createdColumnaDestino.TipoDato);
        
        }


       



    }
}

