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
            Assert.True(result.Count() >= 0, $"Total de itens na lista { result.Count() }");
            Assert.All<Message>(result, x => Assert.False(x.Viewed));
        }

        [Fact]
        public void AtualizarSomenteAsUltimasMensagensQueNaoForamVistas()
        {
            //Arrange
            var messages = _service.FindPendingReading(new Infra.PageParameters());
            var last = messages.Last();

            //Act
            var result = _service.UpdateViewed(last);
            var pending = _service.FindPendingReading(new Infra.PageParameters());

            //Assert
            Assert.IsType<int>(result);
            Assert.All<Message>(pending, x => Assert.True(x.Id > last.Id, $"Valor da lista { x.Id } valor do ultimo { last.Id }"));

        }
    
        [Fact]
        public void VerificarSeAsMensagensEstaoSendoAlteradas()
        {
            // Arrange
            var messages = _service.FindAll(new Infra.PageParameters());
            var message = messages.ElementAt(new Random().Next(messages.Count()));
            var text = message.Text;
            message.Text = $"Mensagem atualizada em { DateTime.Now }";

            // Act
            bool result = _service.Update(message);
            var messageAfterUpdate = _service.FindOne(message.Id);

            // Assert
            Assert.True(result, "Registro atualizado");
            Assert.True(messageAfterUpdate.Text != text, $"Mensagem anterior: '{ text }', mensagem atualizada: '{ messageAfterUpdate.Text }'");
        }
    
        [Fact]
        public void VerificarSeAsMensagensEstaoSendoExcluidas()
        {
            // Arrange
            var messages = _service.FindAll(new Infra.PageParameters());
            var message = messages.ElementAt(new Random().Next(messages.Count()));

            //Act
            var result = _service.Delete(message.Id);
            var messagesAfterDelete =  _service.FindAll(new Infra.PageParameters());

            //Assert
            Assert.True(result > 0, $"O registro de Id: '{ message.Id }' foi excluido");
            Assert.All<Message>(messagesAfterDelete, x => Assert.False(x.Id == message.Id, $"Lista: '{ x.Id }' mensagem: '{ message.Id }'"));
        }
    }
}
