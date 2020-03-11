using System;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace api.Test
{
    
    public class MessageControllerTest
    {
        api.Controllers.MessageController _controller;

        public MessageControllerTest()
        {
            _controller = new Controllers.MessageController();
        }

        [Fact]
        public void A_metodo_get_funciona()
        {
         //Act
         var okResult = _controller.Get();

         //Assert
         Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void Obter_todas_as_mensagens_pendentes_de_leitura()
        {
            //Act
            var result = _controller.Get().Value;

            //Assert
            
        }
    }
}
