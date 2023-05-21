// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function editFillForm(index) {
	const rowData = tokens[index];
	
	$('form input#name').val(rowData.name);
	$('form input#symbol').val(rowData.symbol);
	$('form input#contract_address').val(rowData.contract_address);
	$('form input#total_supply').val(rowData.total_supply);
	$('form input#total_holders').val(rowData.total_holders);
}