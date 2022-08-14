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

![Desafio Broker](https://user-images.githubusercontent.com/15125899/184268742-d34f7b66-373d-4535-a1f0-32d61bc44cd1.JPG)

Toda vez que o preço for maior que linha-azul, um e-mail deve ser disparado aconselhando a venda.

Toda vez que o preço for menor que linha-vermelha, um e-mail deve ser disparado aconselhando a compra.

# Como rodar o projeto;

primeiramente e necessario possuit os pacotes do dotnet instalados no mue caso estou no Arch linux, então a instalação do dotnet core foi feita com o comando `sudo pacman -S dotnet-host dotnet-runtime dotnet-sdk dotnet-targeting-pack`.

Apos isso, é necessário criar um arquivo de configuração `config.json`, no diretório raiz do projeto ou na no mesmo diretório do executável, com as seguintes informações:

```json
{
    "AlphaVantageAPIKEY":"************",
    "CheckInterval": "1",
    "SMTPserver":"smtp.yandex.com",
    "SMTPusername":"**************@yandex.ru",
    "SMTPpassword":"**************",
    "SMTPportTLS": "587",
    "SMTPportSSL": "465",
    "DestinationEmail": "destino@gmail.com"
}
```

Voce pode criar o arquivo de configuração copiado do arquivo de configuração de exemplo com o comando `cp config.example.json config.json`.

No meu caso usei o Yandex que ainda permite esse tipo de autenticação, mas se quiser usar outro servidor, basta alterar o SMTPserver, SMTPusername, SMTPpassword, SMTPportTLS, SMTPportSSL e DestinationEmail.

O campo `DestinationEmail` pode receber vários e-mails separados por vírgula.

A API de cotação usada foi a Alpha Vantage.
