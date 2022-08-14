namespace Program
{
    using System.Net.Http;
    using Newtonsoft.Json.Linq;

    class StockQuoteAlert
    {
        private HttpClient httpClient;
        private EmailServices emailServices;

        private string[] stocks;
        private float[] sellPrices;
        private float[] buyPrices;

        public StockQuoteAlert(string[] stocks, float[] sellPrices, float[] buyPrices)
        {
            if (sellPrices.Length != buyPrices.Length || sellPrices.Length != stocks.Length)
                throw new System.Exception("Quantidade de ativos, preços de venda e preços de compra diferentes");

            this.stocks = stocks;
            this.sellPrices = sellPrices;
            this.buyPrices = buyPrices;

            httpClient = new HttpClient();
            emailServices = new EmailServices();
        }

        // get the stock quote from the web service
        private float GetStockPrice(string ativo)
        {
            var apiKey = ConfigReader.ReadConfig()["AlphaVantageAPIKEY"];
            var apiUrl = "https://www.alphavantage.co/query" +
                        "?function=TIME_SERIES_DAILY" +
                        $"&symbol={ativo}" +
                        $"&apikey={apiKey}";

            float currentPrice = -1;

            try
            {
                //Get the response from the api
                var response = httpClient.GetAsync(apiUrl).Result;
                //Get the response body
                JObject body = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                //Get the last price
                var FirstEntry = body["Time Series (Daily)"].First;
                // Get the last price                    
                currentPrice = float.Parse(FirstEntry.First["4. close"].ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (currentPrice == -1)
                throw new Exception("Não foi possível obter a cotação do ativo");

            return currentPrice;
        }

        // Watch the a set of stocks and send an email if the price is below the threshold or above the threshold
        public void StockWatch()
        {
            int checkInterval = int.Parse(ConfigReader.ReadConfig()["CheckInterval"]);
            while (true)
            {
                for (int i = 0; i < stocks.Length; i++)
                {
                    var ativo = stocks[i];
                    var precoVenda = sellPrices[i];
                    var precoCompra = buyPrices[i];
                    float cotacao;
                    try
                    {
                        cotacao = GetStockPrice(ativo);
                        Alert(ativo, precoVenda, precoCompra, cotacao);
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e.Message); // GetStockPrice erro or Email erro
                        continue;
                    }

                    // 30 second delay between email and
                    System.Threading.Thread.Sleep(30 * 1000);
                }
                // Minutes between checks
                System.Threading.Thread.Sleep(checkInterval * 1000 * 60);
            }
        }

        //Send an email if the stock price is below the buy price or above the sell price
        private void Alert(string ativo, float sellPrice, float buyPrice, float currentPrice)
        {
            String message;
            if (currentPrice < buyPrice)
            {
                message = $"Alerta de compra \nO ativo {ativo} está abaixo do preço de compra ({buyPrice}) \nPreço atual: {currentPrice}";
                Console.WriteLine(message);
                emailServices.SendEmail(ConfigReader.ReadConfig()["DestinationEmail"],
                "Alerta de compra",
                message);
            }
            else if (currentPrice > sellPrice)
            {
                message = $"Alerta de venda \nO ativo {ativo} está acima do preço de venda ({sellPrice}) \nPreço atual: {currentPrice}";
                Console.WriteLine(message);
                emailServices.SendEmail(ConfigReader.ReadConfig()["DestinationEmail"],
                "Alerta de venda",
                message);
            }
        }

    }
}
