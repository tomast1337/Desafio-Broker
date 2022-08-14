namespace Program
{
    using System.Net.Http;
    using Newtonsoft.Json.Linq;

    class StockQuoteAlert
    {
        internal void Alert(string ativo, float sellPrice, float buyPrice)
        {
            if (ativo is null)
                throw new ArgumentNullException($"{nameof(ativo)} não pode ser nulo");



            //Alpha Vantage
            var httpClient = new HttpClient();
            var apiKey = ConfigReader.ReadConfig()["AlphaVantageAPIKEY"];
            var apiUrl = "https://www.alphavantage.co/query" +
                         "?function=TIME_SERIES_DAILY" +
                         $"&symbol={ativo}" +
                         $"&apikey={apiKey}";
            

            int checkInterval = int.Parse(ConfigReader.ReadConfig()["CheckInterval"]) * 1000 * 60;

            var emailServices = new EmailServices();
            string DestinationEmail = ConfigReader.ReadConfig()["DestinationEmail"];

            while (true)
            {
                try
                {
                    //Get the response from the api
                    var response = httpClient.GetAsync(apiUrl).Result;
                    //Get the response body
                    JObject body = JObject.Parse(response.Content.ReadAsStringAsync().Result);                    
                    //Get the last price
                    var FirstEntry = body["Time Series (Daily)"].First;
                    // Get the last price                    
                    var currentPrice = float.Parse(FirstEntry.First["4. close"].ToString());
                    //Check if the last price is lower than the buy price
                    if (currentPrice < buyPrice)
                    {
                        var message = $"Alerta de compra \n O ativo {ativo} está abaixo do preço de compra ({buyPrice}) \n Preço atual: {currentPrice}";
                        Console.WriteLine(message);
                        emailServices.SendEmail(DestinationEmail, $"Alerta de compra - {ativo}", message);
                    }
                    //If the last price is higher than the sell price, send an email
                    if (currentPrice > sellPrice)
                    {
                        var message = $"Alerta de venda \n O ativo {ativo} está acima do preço de venda ({sellPrice}) \n Preço atual: {currentPrice}";
                        Console.WriteLine(message);
                        emailServices.SendEmail(DestinationEmail, $"Alerta de venda - {ativo}", message);
                    }
                    System.Threading.Thread.Sleep(checkInterval);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao obter preço atual");
                    Console.WriteLine(e.Message);
                }
                System.Threading.Thread.Sleep(30000);
            }
        }
    }
}
