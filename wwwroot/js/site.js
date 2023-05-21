// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
	const data = [
		{ year: 2011, count: 10 },
		{ year: 2012, count: 10 },
		{ year: 2013, count: 10 },
		{ year: 2014, count: 10 },
		{ year: 2015, count: 10 },
	]
	
	var ctx = $("#doughnut");
	var myChart = new Chart(ctx, {
		type: 'doughnut',
		data: {
			labels: data.map(row => row.year),
			datasets: [
				{
					label: 'PIEEEEE',
					data: data.map(row => row.count)
				}
			]
		},
		options: {
			responsive: true,
			maintainAspectRatio: false,
			plugins: {
				labels: {
					render: (a) => `${a.label}`,
					position: 'outside'
				}
			}
		}
	})
});