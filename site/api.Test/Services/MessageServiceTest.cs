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

    public class MessageServiceTest
    {
        private api.Services.IMessageService _service;
        private readonly ITestOutputHelper _output;

        public MessageServiceTest(ITestOutputHelper output)
        {
            _output = output;

            IOptions<LiteDbOptions> opt = Options.Create<LiteDbOptions>(new LiteDbOptions() { DatabaseLocation = "database.db"});
            LiteDbContext db = new LiteDbContext(opt);
            _service = new MessageService(db);
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
            var result = _service.Insert(msg1);
            
            //Assert
            Assert.IsType<int>(result);
            Assert.True(result > 0);
        }

        [Fact]
        public void VerificarSeOGetRetornaTodasAsMensagens()
        {
            //Arrange - O cenário

            //Act - O método sob teste
            var pag1 = _service.FindAll(new Infra.PageParameters{ PageSize = 5, PageNumber = 1 });
            var pag2 = _service.FindAll(new Infra.PageParameters{ PageSize = 6, PageNumber = 2 });

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
            var result = _service.FindPendingReading(new Infra.PageParameters{ PageSize = 5, PageNumber = 1 });

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
