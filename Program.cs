/*
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
*/
namespace Program
{
    class programa
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Uso: stock-quote-alert.exe <ativo> <preço de venda> <preço de compra>");
                return;
            }
            string ativo;
            float precoVenda;
            float precoCompra;
            
            ativo = args[0];
            precoVenda = float.Parse(args[1]);
            precoCompra = float.Parse(args[2]);

            // Ler configurações do arquivo config.json
            ConfigReader.ReadConfig();

            Console.WriteLine("Monitorando {0}", ativo);
            Console.WriteLine("Preço de venda: {0}", precoVenda);
            Console.WriteLine("Preço de compra: {0}", precoCompra);

            var alertService = new StockQuoteAlert();
            alertService.Alert(ativo, precoVenda, precoCompra);
        }
    }
}