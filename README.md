# Desafio Broker

O objetivo desse teste é desenvolver um programa em C# (caso seja necessário pode ser desenvolvido em C++, Java, Golang, Haskell. Nessa ordem de preferência, mas todas funcionam bem para a gente!) que seja capaz de atender aos requisitos descritos abaixo.


O prazo de entrega é flexível, mas em geral esperamos que os candidatos retornem em 15 ~ 30 dias. Mas não há nada de errado em entregar antes (já tivemos resultados ótimos em certos casos que entregaram em menos de 24h).

Quando terminar peço que envie um link para um projeto na sua conta github.  O envio deve ser feito em resposta a esse e-mail "respondendo a todos". Em seguida, marcaremos uma sabatina via zoom para discussão do código entregue. Ok?

O fonte deverá ser enviado como um link para um projeto na sua conta github.

Não se preocupe em apagar o histórico de desenvolvimento, faz parte do processo de avaliação entender o que foi priorizado no decorrer do projeto. Caso não consiga terminar, sugerimos que envie mesmo assim o resultado parcial.

Estamos disponíveis para tirar dúvidas (é só enviar por esse mesmo e-mail!).


## Requisitos

O objetivo do sistema é avisar, via e-mail, caso a cotação de um ativo da B3 caia mais do que certo nível, ou suba acima de outro.

O programa deve ser uma aplicação de console (não há necessidade de interface gráfica).

Ele deve ser chamado via linha de comando com 3 parâmetros.

O ativo a ser monitorado
O preço de referência para venda
O preço de referência para compra
Ex.

`stock-quote-alert.exe PETR4 22.67 22.59` 

Ele deve ler de um arquivo de configuração com:

O e-mail de destino dos alertas
As configurações de acesso ao servidor de SMTP que irá enviar o e-mail
A escolha da API de cotação é livre.

O programa deve ficar continuamente monitorando a cotação do ativo enquanto estiver rodando.



Em outras palavras, dada a cotação de PETR4 abaixo.

![desafiobroker]("./doc imgs/desafiobroker.JPG")

Toda vez que o preço for maior que linha-azul, um e-mail deve ser disparado aconselhando a venda.

Toda vez que o preço for menor que linha-vermelha, um e-mail deve ser disparado aconselhando a compra.

