using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestAPI_RabbitMQ.Models;
using RestAPI_RabbitMQ.Service;

namespace RestAPI_RabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Post(Message message)
        {
            if (message is null) return BadRequest("Objeto null.");
            if (string.IsNullOrEmpty(message.Title)) return BadRequest("Informe um Titulo.");
            if (string.IsNullOrEmpty(message.Title)) return BadRequest("Informe uma Mensagem.");

            using (var serviceRabbitMq = new RabbitMqService())
            {
                if(!serviceRabbitMq.SendMessage(message))
                {
                    return BadRequest("Ocorreu algum problema ao enviar a mensagem.");
                }
            }

            return Ok("Mensagem enviada com sucesso.");
        }

        [HttpGet]
        public async Task<ActionResult<List<Message>>> Get()
        {
            var listMessage = new List<Message>();

            using (var serviceRabbitMq = new RabbitMqService())
            {
                listMessage = serviceRabbitMq.GetMessages();
            }
                
            return Ok(listMessage);
        }
    }
}
