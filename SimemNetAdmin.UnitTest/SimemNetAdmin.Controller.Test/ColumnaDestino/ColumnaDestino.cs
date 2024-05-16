using Microsoft.AspNetCore.Mvc;
using Moq;
using SimemNetAdmin.Application.Interfaces.Columns;
using SimemNetAdmin.Domain.ViewModel.Colums;
using SimemNetAdmin.Web.Api.Controllers.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Controller.Test.ColumnaDestino
{
    [TestClass]
    public class ColumnaDestino
    {
       
        
            private readonly ConfiguracionColumnasDestinoController _columnasDestinoController;
            private readonly Mock<IConfiguracionColumnasDestinoService> _mockColumnasDestinoService;

            public ColumnaDestino()
            {
                _mockColumnasDestinoService = new Mock<IConfiguracionColumnasDestinoService>();
                _columnasDestinoController = new ConfiguracionColumnasDestinoController(_mockColumnasDestinoService.Object);
            }

            #region Test controller

            [TestMethod]
            public async Task TestListColumnaDestino_StatusCodeOK()
            {
                
                var columns = new List<ConfiguracionColumnasDestinoDTO>
            {
                new ConfiguracionColumnasDestinoDTO
                {
                    IdColumnaDestino = Guid.NewGuid(),
                    NombreColumnaDestino = "Nombre de ejemplo",
                    TipoDato = "string",
                    AtributoVariable = "Atributo de ejemplo",
                    VariableId = Guid.NewGuid(),
                    Estado = true,
                    Descripcion = "Descripción de ejemplo",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                }
            };

                _mockColumnasDestinoService.Setup(x => x.ListColumnaDestino()).ReturnsAsync(columns);

                
                IActionResult response = await _columnasDestinoController.ListColumnaDestino();
                var okResult = response as OkObjectResult;
                var resultColumns = okResult!.Value as List<ConfiguracionColumnasDestinoDTO>;

                
                Assert.IsNotNull(resultColumns);
                Assert.IsTrue(resultColumns.Count > 0);
                Assert.IsNotNull(response);
                Assert.IsTrue(response is OkObjectResult);
            }

            [TestMethod]
            public async Task TestListColumnaDestino_StatusCodeNoContent()
            {
                
                _mockColumnasDestinoService.Setup(x => x.ListColumnaDestino()).ReturnsAsync(new List<ConfiguracionColumnasDestinoDTO>());

                
                IActionResult response = await _columnasDestinoController.ListColumnaDestino();

                
                Assert.IsNotNull(response);
                Assert.IsTrue(response is NoContentResult);
            }

            [TestMethod]
            public async Task TestListColumnaDestino_Exception()
            {
                
                _mockColumnasDestinoService.Setup(x => x.ListColumnaDestino()).ThrowsAsync(new Exception("Test exception"));

                
                try
                {
                    IActionResult response = await _columnasDestinoController.ListColumnaDestino();
                }
                catch (Exception ex)
                {
                    Assert.IsNotNull(ex);
                    Assert.AreEqual("Test exception", ex.Message);
                }
            }
      
        [TestMethod]
        public async Task TestCreateColumnaDestino_Success()
        {
           
            var columnaDestinoDTO = new ConfiguracionColumnasDestinoDTO
            {
                IdColumnaDestino = Guid.NewGuid(),
                NombreColumnaDestino = "Nombre de columna",
                TipoDato = "integer",
                AtributoVariable = "Atributo de variable",
                VariableId = null,
                Estado = true,
                Descripcion = "Esta es una descripción de la columna",
                FechaCreacion = DateTime.UtcNow,
                FechaActualizacion = DateTime.UtcNow
            };

            var nuevaColumnaId = Guid.NewGuid(); 

            _mockColumnasDestinoService.Setup(x => x.CreateColumnaDestino(columnaDestinoDTO)).ReturnsAsync(nuevaColumnaId);

            
            var response = await _columnasDestinoController.CreateColumnaDestino(columnaDestinoDTO);

           
            var createdAtActionResult = response as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            if (createdAtActionResult != null)
            {
                Assert.AreEqual("CreateColumnaDestino", createdAtActionResult.ActionName);
                Assert.IsNotNull(createdAtActionResult.Value);
                Assert.AreEqual(nuevaColumnaId, createdAtActionResult.RouteValues?["id"]);
            }
        }



        [TestMethod]
        public async Task TestCreateColumnaDestino_BadRequest()
        {
           
            var columnaDestinoDTO = new ConfiguracionColumnasDestinoDTO
            {
                IdColumnaDestino = Guid.NewGuid(),
                NombreColumnaDestino = "Nombre de columna",
                TipoDato = "integer",
                AtributoVariable = "Atributo de variable",
                VariableId = null,
                Estado = true,
                Descripcion = "Esta es una descripción de la columna",
                FechaCreacion = DateTime.UtcNow,
                FechaActualizacion = DateTime.UtcNow
            };

            _mockColumnasDestinoService.Setup(x => x.CreateColumnaDestino(columnaDestinoDTO))
                .Throws(new ArgumentException("Invalid column data"));

            
            var response = await _columnasDestinoController.CreateColumnaDestino(columnaDestinoDTO);

          
            var badRequestResult = response as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);

           
            Assert.IsTrue(badRequestResult?.Value?.ToString()?.Contains("Invalid column data") ?? false);
        }


        [TestMethod]
        public async Task TestCreateColumnaDestino_Exception()
        {
          
            var columnaDestinoDTO = new ConfiguracionColumnasDestinoDTO
            {
                IdColumnaDestino = Guid.NewGuid(),
                NombreColumnaDestino = "Nombre de columna",
                TipoDato = "integer",
                AtributoVariable = "Atributo de variable",
                VariableId = null,
                Estado = true,
                Descripcion = "Esta es una descripción de la columna",
                FechaCreacion = DateTime.UtcNow,
                FechaActualizacion = DateTime.UtcNow
            };

            _mockColumnasDestinoService.Setup(x => x.CreateColumnaDestino(columnaDestinoDTO))
                .Throws(new Exception("Test exception"));

            
            var response = await _columnasDestinoController.CreateColumnaDestino(columnaDestinoDTO);

            
            var badRequestResult = response as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);

            
            Assert.IsTrue(badRequestResult?.Value?.ToString()?.Contains("Test exception"));
        }


        [TestMethod]
        public async Task TestUpdateColumnaDestino_Success()
        {
            
            var columnaDestinoDTO = new ConfiguracionColumnasDestinoDTO
            {
                IdColumnaDestino = Guid.NewGuid(),
                NombreColumnaDestino = "Nombre de columna",
                TipoDato = "integer",
                AtributoVariable = "Atributo de variable",
                VariableId = null,
                Estado = true,
                Descripcion = "Esta es una descripción de la columna",
                FechaCreacion = DateTime.UtcNow,
                FechaActualizacion = DateTime.UtcNow
            };

            _mockColumnasDestinoService.Setup(x => x.UpdateColumnaDestinoAsync(columnaDestinoDTO));

           
            var response = await _columnasDestinoController.UpdateColumnaDestino(columnaDestinoDTO);

            var okResult = response as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task TestUpdateColumnaDestino_BadRequest()
        {
          
            var columnaDestinoDTO = new ConfiguracionColumnasDestinoDTO
            {
                IdColumnaDestino = Guid.NewGuid(),
                NombreColumnaDestino = "Nombre de columna",
                TipoDato = "integer",
                AtributoVariable = "Atributo de variable",
                VariableId = null,
                Estado = true,
                Descripcion = "Esta es una descripción de la columna",
                FechaCreacion = DateTime.UtcNow,
                FechaActualizacion = DateTime.UtcNow
            };

            _mockColumnasDestinoService.Setup(x => x.UpdateColumnaDestinoAsync(columnaDestinoDTO)).ThrowsAsync(new ArgumentException("Invalid column data"));

            
            var response = await _columnasDestinoController.UpdateColumnaDestino(columnaDestinoDTO);

            var badRequestResult = response as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.IsTrue(badRequestResult?.Value?.ToString()?.Contains("Invalid column data"));
        }

        [TestMethod]
        public async Task TestUpdateColumnaDestino_Exception()
        {
           
            var columnaDestinoDTO = new ConfiguracionColumnasDestinoDTO
            {
                IdColumnaDestino = Guid.NewGuid(),
                NombreColumnaDestino = "Nombre de columna",
                TipoDato = "integer",
                AtributoVariable = "Atributo de variable",
                VariableId = null,
                Estado = true,
                Descripcion = "Esta es una descripción de la columna",
                FechaCreacion = DateTime.UtcNow,
                FechaActualizacion = DateTime.UtcNow
            };

            _mockColumnasDestinoService.Setup(x => x.UpdateColumnaDestinoAsync(columnaDestinoDTO)).ThrowsAsync(new Exception("Test exception"));

            
            var response = await _columnasDestinoController.UpdateColumnaDestino(columnaDestinoDTO);

       
            var badRequestResult = response as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.IsTrue(badRequestResult?.Value?.ToString()?.Contains("Test exception"));
        }

        #endregion

    }
}
