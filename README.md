# AuditNow

Aplicação de auditoria para uma empresa que lida com informações financeiras sensíveis. A empresa precisa garantir que as transações financeiras sejam registradas de
forma segura, que os dados sejam consistentes e que seja possível rastrear as alterações feitas nos Registros.

## Requisitos

Para executar este projeto, você precisará das seguintes ferramentas instaladas em sua máquina:

- Node.js (versão 14.20.0 ou superior)
- Angular CLI (versão 15.1.2 ou superior)
- .NET SDK (versão 8.0.202 ou superior)
- MySQL (versão 8.0.0 ou superior)

## Instalação

1. Clone o repositório:
   ```bash
   git clone https://github.com/Vitoria-Emanuely/AuditNow.git
   ```

2. Navegue até o diretório do frontend:
   ```bash
   cd AuditNow/auditnow-app
   ```

3. Instale as dependências:
   ```bash
   npm install
   ```

4. Inicie o servidor de desenvolvimento:
   ```bash
   ng serve
   ```

5. Abra o aplicativo no navegador em [http://localhost:4200](http://localhost:4200).

6. Navegue até o diretório do backend:
   ```bash
   cd ../auditnow-api
   ```

7. Compile o projeto:
   ```bash
   dotnet build
   ```

8. Execute o projeto:
   ```bash
   dotnet run
   ```

## Configuração do Banco de Dados

1. Certifique-se de que o MySQL está em execução em sua máquina.
2. Na interface do software Microsoft Visual Studio, abre o Console Gerenciador de Pacotes e selecione em Projeto padrão, AuditNow.Api.
3. Execute o comando:
   ```bash
   EntityFramework6\Update-Database
   ```
