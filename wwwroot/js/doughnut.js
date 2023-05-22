var doughnutChart;

function renderDoughnut() {
	// `g_tokens` from global variable
	const data = g_tokens.map((v) => {
		return {
			name: v.name,
			total_supply: v.total_supply
		}
	});
	
	if (doughnutChart) {
		doughnutChart.data = {
			labels: data.map(row => row.name),
			datasets: [
				{
					data: data.map(row => row.total_supply)
				}
			]
		}
		return ;
	}
	
	// Canvas element
	var ctx = $("#doughnut");
	
	var chartData = {
		labels: data.map(row => row.name),
		datasets: [
			{
				data: data.map(row => row.total_supply)
			}
		]
	}
	
	var chartOptions = {
		responsive: true,
		maintainAspectRatio: false,
		plugins: {
			title: { 
				text: "Total Statistics by Total Supply", 
				display: true, 
				font: { size: 30 } 
			},
			legend: { display: false },
			labels: {
				render: (a) => a.label,
				position: 'border'
			},
		}
	}
	
	doughnutChart = new Chart(ctx, {
		type: 'doughnut',
		data: chartData,
		options: chartOptions
	})
	
}