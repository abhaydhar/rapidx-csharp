using System;
using System.Net.Http;
using System.Threading.Tasks;
using YourApp.ServiceReference; // This namespace is generated based on your SOAP service reference.

class Program
{
    static async Task Main(string[] args)
    {
        // Call SOAP service
        var soapClient = new SoapServiceClient();
        try
        {
            string soapResult = await soapClient.GetSoapDataAsync("test input");
            Console.WriteLine("SOAP Response: " + soapResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error calling SOAP service: " + ex.Message);
        }

        // Call REST service
        var httpClient = new HttpClient();
        try
        {
            string restUrl = "https://api.example.com/data";
            HttpResponseMessage response = await httpClient.GetAsync(restUrl);
            if (response.IsSuccessStatusCode)
            {
                string restResult = await response.Content.ReadAsStringAsync();
                Console.WriteLine("REST Response: " + restResult);
            }
            else
            {
                Console.WriteLine("REST call failed with status code: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error calling REST service: " + ex.Message);
        }
    }
}