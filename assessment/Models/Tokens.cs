using System.ComponentModel.DataAnnotations;

namespace assessment.Models;

public class Token
{
	public int id { get; set; }
	
	[Required]
	[StringLength(5)]
	public required string symbol { get; set; }
	
	[Required]
	[StringLength(50)]
	public required string name { get; set; }
	
	[Required]
	[Range(0, UInt64.MaxValue)]
	public required UInt64 total_supply { get; set; }
	
	[Required]
	[StringLength(66)]
	public required string contract_address { get; set; }
	
	[Required]
	[Range(0, UInt32.MaxValue)]
	public required uint total_holders { get; set; }
	
	public decimal price { get; set; } = 0.00M;
}