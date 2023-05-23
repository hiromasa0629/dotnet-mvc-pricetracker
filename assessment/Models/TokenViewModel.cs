using assessment.Models;

public class TokenViewModel : Token
{
	private double _total_supply_perc;

	public required double total_supply_perc 
	{ 
		get { return _total_supply_perc; } 
		set { _total_supply_perc = Math.Round(value, 5); } 
	}
}