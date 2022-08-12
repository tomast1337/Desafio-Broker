namespace Program
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    class StockQuoteAlertService
    {

        internal void Alert(string ativo, float precoVenda, float precoCompra)
        {

            //Check interval
            var checkInterval = int.Parse(ConfigReader.ReadConfig()["CheckInterval"]);
            //SMTP server
            var apiKey = ConfigReader.ReadConfig()["AlphaVantageAPIKEY"];
            var apiUrl = "https://www.alphavantage.co/" +
                            "query?function=TIME_SERIES_INTRADAY" +
                            $"&symbol={ativo}" +
                            "&interval=60min" +
                            $"&apikey={apiKey}";

            Console.WriteLine("Api url: {0}", apiUrl);

            //Http client
            var client = new HttpClient();

            int intCheckInterval = int.Parse(ConfigReader.ReadConfig()["CheckInterval"]) * 1000 * 60;

            // Run loop
            while (true)
            {
                try
                {
                    //Get the response from the api
                    var response = client.GetAsync(apiUrl).Result;
                    //Get the response body
                    JObject body = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                    //Get the last price
                    var FirstEntry = body["Time Series (60min)"].First;
                    // Get the last price                    
                    var lastPrice = float.Parse(FirstEntry.First["4. close"].ToString());
                    //Check if the last price is lower than the buy price
                    if (lastPrice < precoCompra)
                    {
                        Console.WriteLine($"Alerta de compra \n O ativo {ativo} está abaixo do preço de compra ({precoCompra})");
                    }
                    //If the last price is higher than the sell price, send an email
                    if (lastPrice > precoVenda)
                    {
                        Console.WriteLine($"Alerta de venda \n O ativo {ativo} está acima do preço de venda ({precoVenda})");
                    }
                    Console.WriteLine($"Preço atual: {lastPrice}");
                    System.Threading.Thread.Sleep(intCheckInterval);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao obter preço atual");
                    Console.WriteLine(e.Message);
                }
                //30 sec
                System.Threading.Thread.Sleep(30000);

            }

        }
    }
}


/*If the last price is lower than the buy price, send an email
if (lastPrice < precoCompra)
{
    Console.WriteLine("Alerta de compra", $"O ativo {ativo} está abaixo do preço de compra ({precoCompra})");
}
//If the last price is higher than the sell price, send an email
if (lastPrice > precoVenda)
{
    Console.WriteLine("Alerta de venda", $"O ativo {ativo} está acima do preço de venda ({precoVenda})");
}
Console.WriteLine($"Preço atual: {lastPrice}");
//Wait 1 minute
System.Threading.Thread.Sleep(intCheckInterval);

*/