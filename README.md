# API REST com ASP.NET Core e RabbitMQ

Este é um projeto de exemplo de uma API REST desenvolvida com **ASP.NET Core**, que utiliza o **RabbitMQ** para envio e consumo de mensagens. A API possui dois endpoints:

- `POST /mensagens` – Envia uma mensagem com título e corpo para uma fila do RabbitMQ.
- `GET /mensagens` – Lê e retorna as mensagens presentes na fila do RabbitMQ.

---

## 🛠 Tecnologias Utilizadas

- ASP.NET Core
- RabbitMQ
- Docker
- Docker Compose
- C#

---
