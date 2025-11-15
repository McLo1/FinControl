# FinControl
O FinControl é um projeto desenvolvido com C# 9.0 e .NET, MySQL e React, atualmente tendo apenas o backend pronto sendo possível cadastrar um novo usuario, uma nova categoria e transações que serão feitas, a ideia do APP surgiu com minhas próprias desorganizações financeiras, ao invés de criar planilhas no Excel ou precisar me adaptar a outros apps, decidi criar o meu próprio app para organizar minhas financias

# Ideias para o App
Dashboards interativos sobre as minhas entradas e saidas

Um chat com Ia dando ideias de investimentos e como seu dinheiro pode ser organizado

Separar tudo com categorias, como gastos-fixos, gastos-viagens, entrada-vendas, etc.

# Ideias complexas que me deixaram doido
Conectar os bancos do usuario com o APP para que a IA do APP sempre tenha noção do quanto você tem e isso ajude nas ideias da sua organização, com notificações de valores pagos com débito automatico -- 'Internet foi paga no valor de R$ 50,00'

Inserção de planilhas que o usuario talvez já possua para que a IA tenha aonde se basear 

A ideia é ter um APP que organiza suas financias diariamente e ainda te ajuda a organizar sua vida financeira


# Em desenvolvimento

Frontend a ser desenvolvido...

# Pronto
Atualmente temos o backend 60% pronto, no momento é possível cadastar um novo usuario, fazer login com JWT, cadastrar novas categorias e as transacoes que os dashboards irão exibir na tela


# Como rodar
No momento ainda está em desenvolvimento, mas caso queira testar o backend do projeto, segue abaixo um pequeno tutorial do que fazer:
Backend do projeto FinControl, desenvolvido em ASP.NET Core 9, utilizando:

Entity Framework Core
Identity para autenticação e gerenciamento de usuários
JWT para login e proteção de rotas
MySQL (Pomelo) como banco de dados

#📦 Pré-requisitos

Antes de rodar o backend, certifique-se de ter instalado:

✔ .NET SDK 8

https://dotnet.microsoft.com/download

✔ MySQL Server

Recomendo instalar via MySQL Installer ou rodar via Docker.

✔ MySQL Workbench (opcional)

Para visualizar e manipular o banco.

#🔧 Configurar o Banco de Dados


Crie um banco no MySQL:
```
CREATE DATABASE fincontrol_db;
```
No arquivo appsettings.json, configure a connection string:
```Json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=fincontrol_db;user=root;password=SUA_SENHA;"
}
```
Troque SUA_SENHA pela senha real do MySQL.

🛠️ Aplicar as migrações do Entity Framework

No terminal, dentro do projeto:
```
dotnet ef database update
```

Isso vai criar todas as tabelas, inclusive as do Identity (aspnetusers, aspnetroles, etc).

▶️ Rodar o servidor

Execute no terminal:
```
dotnet run
```

Ou, no VS Code:

Abra o projeto

Pressione F5

A API iniciará normalmente.

🌐 URLs Padrão

A API roda geralmente em:
```
https://localhost:7260
http://localhost:5260
```
📚 Endpoints Principais
👤 Usuários

| Método | Rota | Descrição |
|---------|----------|----------|
| POST | /api/usuario  | Criar usuário  |
| POST | /api/usuario/login  | Fazer login e gerar JWT |
| GET | /api/usuario | Listar usuários |
| GET | /api/usuario/{id} | Buscar usuário por ID |
| PUT | /api/usuario/{id} | Atualizar |
| DELETE | /api/usuario/{id} | Deletar |


