// See https://aka.ms/new-console-template for more information

using MySqlConnector;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

class ResponseObject {
	public decimal USD { get; set; }
}

class Program
{
	private const string URL = "https://min-api.cryptocompare.com/data/price";
	
	static void Main()
	{
		HttpClient client = new HttpClient();
		client.BaseAddress = new Uri(URL);
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		
		IConfigurationRoot configuration = new ConfigurationBuilder()
			.SetBasePath(AppContext.BaseDirectory)
			.AddUserSecrets<Program>()
			.Build();
		
		// Construct Mysql ConnectionString
		var mysqlConnectionBuilder = new MySqlConnectionStringBuilder();
		mysqlConnectionBuilder.Server = "localhost";
		mysqlConnectionBuilder.Port = 3306;
		mysqlConnectionBuilder.Pooling = true;
		mysqlConnectionBuilder.UserID = Environment.GetEnvironmentVariable("DbUser");
		mysqlConnectionBuilder.Password = Environment.GetEnvironmentVariable("DbPassword");
		mysqlConnectionBuilder.Database = Environment.GetEnvironmentVariable("DbName");
		
		List<string> allSymbols = new List<string>();
		
		// Get all token symbol and add them to a List
		using (var connection = new MySqlConnection(mysqlConnectionBuilder.ToString()))
		{
			connection.Open();
			
			string query = "SELECT * FROM assessment.Token";
			using (var command = new MySqlCommand(query, connection))
			{
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						string symbol = reader.GetString("symbol");
						allSymbols.Add(symbol);
					}
				}
			}
			
			connection.Close();
		}
		
		// Construct a query for updating all tokens
		string updateQuery = "";
		foreach (string symbol in allSymbols)
		{
			string urlParams = $"?fsym={symbol}&tsyms=USD";
						
			HttpResponseMessage response = client.GetAsync(urlParams).Result;
			if (response.IsSuccessStatusCode)
			{
				var responseString = response.Content.ReadAsStringAsync().Result;
				try 
				{
					var jsonSettings = new JsonSerializerSettings
					{
						MissingMemberHandling = MissingMemberHandling.Error
					};
					ResponseObject? responseObject = JsonConvert.DeserializeObject<ResponseObject>(responseString, jsonSettings);
					Console.WriteLine($"{symbol} {responseString}");
					updateQuery += $"UPDATE assessment.Token SET price={responseObject!.USD} WHERE symbol='{symbol}';";

				} 
				catch (Exception e) 
				{
					Console.WriteLine($"{symbol} Not Found");
					updateQuery += $"UPDATE assessment.Token SET price={0.00} WHERE symbol='{symbol}';";
				}
			}
			else
			{
				Console.WriteLine($"{(int)response.StatusCode} {response.ReasonPhrase}");
			}
		}
		
		// Update all tokens
		using (var updateConnection = new MySqlConnection(mysqlConnectionBuilder.ToString()))
		{
			updateConnection.Open();
			using (var updateCommand = new MySqlCommand(updateQuery, updateConnection))
			{
				int updated = updateCommand.ExecuteNonQuery();
				if (updated == allSymbols.Count())
				{
					Console.WriteLine($"updated: {updated}");
				}
				else
				{
					Console.WriteLine($"Expected: {allSymbols.Count()}, Updated: {updated}");
				}
				updateConnection.Close();
			};
		}
	}
}

