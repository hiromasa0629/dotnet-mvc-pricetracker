namespace assessment.Models;

public class Token
{
	public int id { get; set; }
	public required string symbol { get; set; }
	public required string name { get; set; }
	public required Int64 total_supply { get; set; }
	public required string contract_address { get; set; }
	public required int total_holders { get; set; }
	public double price { get; set; } = 0.0;
}