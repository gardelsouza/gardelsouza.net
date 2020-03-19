using System;
using System.Collections.Generic;
using System.Linq;
using api.Controllers;
using api.Database;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace api.Test
{

    public class MessageControllerTest
    {
        private api.Controllers.MessageController _controller;
        private readonly ITestOutputHelper _output;

        public MessageControllerTest(ITestOutputHelper output)
        {
            _output = output;

            var mockLogger =  new Mock<ILogger<MessageController>>();
            ILogger<MessageController> logger = mockLogger.Object;

            IOptions<LiteDbOptions> opt = Options.Create<LiteDbOptions>(new LiteDbOptions() { DatabaseLocation = "database.db"});
            LiteDbContext db = new LiteDbContext(opt);
            IMessageService service = new MessageService(db);

            _controller = new Controllers.MessageController(logger, service);
        }

        [Fact]
        public void OsRegistrosEstaoSendoInseridos()
        {
            //Arrange
            Message msg1 = new Message {
                Date = DateTime.Now,
                Text = $"Mensagem criada em {DateTime.Now} pelo xUnit para teste, essa é apenas uma mensagem",
                Viewed = false
            };
            
            //Act
            var result = _controller.Insert(msg1);
            
            //Assert
            Assert.IsAssignableFrom<Microsoft.AspNetCore.Mvc.CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void VerificarSeOGetRetornaTodasAsMensagens()
        {
            //Arrange - O cenário

            //Act - O método sob teste
            var pag1 = _controller.Get(new Infra.PageParameters{ PageSize = 5, PageNumber = 1 });
            var pag2 = _controller.Get(new Infra.PageParameters{ PageSize = 6, PageNumber = 2 });

            //Assert - O resultado esperado
            Assert.IsAssignableFrom<IEnumerable<Message>>(pag1);
            Assert.True(pag1.Count() >= 0);
            Assert.True(pag1.Count() > 1 && pag1.Count() <= 5);
            Assert.True(pag2.Count() > 1 && pag1.Count() <= 6);
        }

        [Fact]
        public void RetornarSomenteMensagensPendentesDeLeitura()
        {
            //Arrange - O cenário

            //Act - O método sob teste
            var result = _controller.GetPendingReading(new Infra.PageParameters{ PageSize = 5, PageNumber = 1 });


            //Assert - O resultado esperado
            Assert.IsAssignableFrom<IEnumerable<Message>>(result);
            Assert.True(result.Count() >= 0);
            Assert.True(result.Count() > 1 && result.Count() <= 5);
            Assert.All<Message>(result, x => Assert.False(x.Viewed));
        }

        [Fact]
        public void AtualizarSomenteAsUltimasMensagensQueNaoForamVistas()
        {
            //Arrange

            //Act
            //var result = _controller.UpdateViewedMessages();

            //Assert
        }
    }
}
