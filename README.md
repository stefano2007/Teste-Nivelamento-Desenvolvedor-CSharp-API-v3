# Teste-Nivelamento-Desenvolvedor-CSharp-API-v3

Realizado teste com 5 questões, segue breve resumo

### Questão 1

Foi necessário criar classes adicionais para ficar mais fácil o entendimento do código

### Questão 2

Realizado consumo de API para obter as paginações das partidas de Futebol com "do while"

### Questão 3

Resposta no arquivo "Questao3/Questão 3.docx"

### Questão 4

Resposta no arquivo "Questao3/Questão 4.docx".
OBS. Não tive tempo de instalar o Oracle na máquina, realizado teste no SQL Serve acredito não muda a sintaxe.

### Questão 5

Tentei fechar todos requisito do arquivo "Questao5/Questão 5.docx"

* Criado Filtro de Exception (CustomExceptionFilter) para customizar a resposta da API.
* Utilizado Dapper para consultas e inserção no banco de dados SQLite 
 OBS. Já estudei Dapper porém e a primeira vez que uso, reutilize a logica para criar a connection da classe DatabaseBootstrap.
* Criado padrão CQRS com Commands e Querys
* Uso do Mediator para implementar o padrão CQRS 
* Criado documentação do Swagger usando uma classe de estensão
* Criado um projeto de Test "Questao5.Test" utilizando o NSubstitute para os mocks


Adicionais:

Criado classes (DateTimeHandler, GuidHandler e TipoMovimentoTypeHandler) para customização do Tipos não suportados no SQLite

Na solução de Saldo da conta corrente, criei um Endpoint Adicional usando uma View SQL (vwSaldoContaCorrente) para demostras uma segunda forma de fazer.

Idempotencia, usei a tabela criada no banco de dados para implementar no Endpoint de "Movimentação de uma conta corrente" , 
acredito que essa seja proposta, solicito o IdRequisicao como identifificador único para recuperar o valores da Requisicao e Resultado salvas no BD,
tento comparar se o corpo da requisão atual e salva caso esteja diferente retorno uma exceção.

Obrigado :)