namespace Program
{
    class programa
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Uso: \"Desafio Broker.exe\" <ativo> <preço de venda> <preço de compra>");
                System.Environment.Exit(1);
                return;
            }
            string[] ativo = args[0].Contains(',') ? args[0].Split(',') : new[] { args[0] };
            float[] precoVenda = args[1].Contains(',') ? args[1].Split(',').Select(x => float.Parse(x)).ToArray() : new[] { float.Parse(args[1]) };
            float[] precoCompra = args[2].Contains(',') ? args[2].Split(',').Select(x => float.Parse(x)).ToArray() : new[] { float.Parse(args[2]) };

            // Ler configurações do arquivo config.json
            ConfigReader.ReadConfig();
            StockQuoteAlert alertService;
            try
            {
                alertService = new StockQuoteAlert(ativo, precoVenda, precoCompra);
                alertService.StockWatch();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Desafio Broker.exe <ativo1>,<ativo2> <preço de venda1>,<preço de venda2> <preço de compra1>,<preço de compra2>");
            }
        }
    }
}