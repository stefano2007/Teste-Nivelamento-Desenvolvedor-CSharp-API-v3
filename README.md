# Teste-Nivelamento-Desenvolvedor-CSharp-API-v3

Realizado teste com 5 quest�es, segue breve resumo

### Quest�o 1

Foi necess�rio criar classes adicionais para ficar mais f�cil o entendimento do c�digo

### Quest�o 2

Realizado consumo de API para obter as pagina��es das partidas de Futebol com "do while"

### Quest�o 3

Resposta no arquivo "Questao3/Quest�o 3.docx"

### Quest�o 4

Resposta no arquivo "Questao3/Quest�o 4.docx".
OBS. N�o tive tempo de instalar o Oracle na m�quina, realizado teste no SQL Serve acredito n�o muda a sintaxe.

### Quest�o 5

Tentei fechar todos requisito do arquivo "Questao5/Quest�o 5.docx"

* Criado Filtro de Exception (CustomExceptionFilter) para customizar a resposta da API.
* Utilizado Dapper para consultas e inser��o no banco de dados SQLite 
 OBS. J� estudei Dapper por�m e a primeira vez que uso, reutilize a logica para criar a connection da classe DatabaseBootstrap.
* Criado padr�o CQRS com Commands e Querys
* Uso do Mediator para implementar o padr�o CQRS 
* Criado documenta��o do Swagger usando uma classe de estens�o
* Criado um projeto de Test "Questao5.Test" utilizando o NSubstitute para os mocks


Adicionais:

Criado classes (DateTimeHandler, GuidHandler e TipoMovimentoTypeHandler) para customiza��o do Tipos n�o suportados no SQLite

Na solu��o de Saldo da conta corrente, criei um Endpoint Adicional usando uma View SQL (vwSaldoContaCorrente) para demostras uma segunda forma de fazer.

Idempotencia, usei a tabela criada no banco de dados para implementar no Endpoint de "Movimenta��o de uma conta corrente" , 
acredito que essa seja proposta, solicito o IdRequisicao como identifificador �nico para recuperar o valores da Requisicao e Resultado salvas no BD,
tento comparar se o corpo da requis�o atual e salva caso esteja diferente retorno uma exce��o.

Obrigado :)