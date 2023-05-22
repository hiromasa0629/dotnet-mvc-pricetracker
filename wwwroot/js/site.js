// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/// <reference path="../lib/jquery/dist/jquery.min.js" />

// Write your JavaScript code.

var g_tokens;
var g_page;

function editFillForm(id) {
	getDetail(null, id, function(instance, r) {
		if (r.status !== "error") {
			$('form input#name').val(r.name);
			$('form input#symbol').val(r.symbol);
			$('form input#contract_address').val(r.contract_address);
			$('form input#total_supply').val(r.total_supply);
			$('form input#total_holders').val(r.total_holders);
		} else {
			console.error(r.error);
		}
	});
}


class Divs {
	constructor() {
		this.tokenDetailDiv = $("#token-detail");
		this.dashboardDiv = $("#dashboard");
		this.missingIdDiv = $("#error-div");
		this.defaultTableBody = $("#table-body");
		this.defaultRow = this.defaultTableBody.find("#table-row-default").hide(); // Hide table row template
		
		const pathname = window.location.pathname;
		if (pathname !== "/detail.aspx") {
			this.showDashboard();
		} else {
			this.showTokenDetail();
		}
	}
	
	showTokenDetail(data) {
		const urlParams = new URLSearchParams(window.location.search);
		const urlId = urlParams.get("id");
		if (!urlId) {
			this.showError("Missing Id");
			return ;
		}
		
		getDetail(this, urlId, function(instance, r) {
			if (r.status !== "error") {
				
				$("#detail-title").text(`${r.contract_address}`);
				$("#detail-price").text(`$ ${r.price}`);
				$("#detail-total-supply").text(`${r.total_supply}`);
				$("#detail-total-holders").text(`${r.total_holders}`);
				$("#detail-name").text(`${r.name}`);
				
				instance.tokenDetailDiv.show();
				instance.dashboardDiv.hide();
				instance.missingIdDiv.hide();
			} else {
				instance.showError(r.error);
			}
		});
	}
	
	showDashboard() {
		const urlParams = new URLSearchParams(window.location.search);
		const urlPage = urlParams.get("page") ?? "1";
		
		try {
			g_page = parseInt(urlPage);
			if (g_page <= 0) throw "Page must be positive";
			renderPage(this);
		} catch (msg) {
			console.log(msg);
			this.showError(msg);
			return ;
		}
		this.dashboardDiv.show();
		this.tokenDetailDiv.hide();
		this.missingIdDiv.hide();
	}
	
	showError(text) {
		this.dashboardDiv.hide();
		this.tokenDetailDiv.hide();
		this.missingIdDiv.show();
		this.missingIdDiv.text(text);
	}
}

class TokenTable {
	constructor() {
		this.tableBody = $("#table-body");
		this.tableBodyRow = $("#table-row-default");
	}
}

function getDetail(instance, id, callback) {
	$.ajax({
		url: `${apiUrl}/token/${id}`,
		method: "GET",
		success: function(res) {
			callback(instance, res);
		},
		error: function(xhr, status, error) {
			callback(instance, { xhr, status, error });
		}
	})
}

function getTokens(callback) {
	$.ajax({
		url: `${apiUrl}/token`,
		method: "GET",
		data: { page: g_page },
		success: function(res) {
			callback(res);
		},
		error: function(xhr, status, error) {
			callback({ xhr, status, error });
		}
	})
}

/**
 * Handle Form submit
 * @param {FormData} formData 
 * @param {Divs} divs 
 */
function submitTokenForm(formData, divs) {
	$.ajax({
		url: `${apiUrl}/token`,
		method: "POST",
		data:  formData,
		contentType: false,
		processData: false,
		success: function (res) {
			divs.showDashboard();
		},
		error: function (xhr, status, error) {
			console.log(xhr.responseJSON.Message);
		}
	})
}


/**
 * Render page by getting tokens and updating doughnut and table
 * @param {Divs} divs
 */
function renderPage(divs) {
	
	getTokens(function(res) {
		if (res.status === "error") {
			divs.showError("Error");
			console.error(res);
			return;
		}
		// Set token to global state
		g_tokens = res;
		
		console.log(g_tokens);
		
		renderDoughnut();
		renderTable(divs);
		registerEvent(divs);
	})
}

/**
 * Render table by cloning table row template
 * @param {Divs} divs
 */
function renderTable(divs) {
		var rank = 1 + ((g_page - 1) * 10);
		const tableBody = divs.defaultTableBody.clone(true, true);
		for (const t of g_tokens) {
			const row = divs.defaultRow.clone(true, true);
			
			row.show();
			row.attr("id", `${row.attr("id")}-${t.id}`);
			row.find(".table__row__rank").text(rank++);
			row.find(".table__row__symbol").text(t.symbol).attr("token-id", t.id);
			row.find(".table__row__name").text(t.name);
			row.find(".table__row__contract_address").text(t.contract_address);
			row.find(".table__row__total_holders").text(t.total_holders);
			row.find(".table__row__total_supply").text(t.total_supply);
			row.find(".table__row__total_supply_perc").text(`${t.total_supply_perc}%`);
			row.find(".table__row__edit").attr("token-id", t.id);
			tableBody.append(row);
		}
		$('#table-body').replaceWith(tableBody);
}

/**
 * 
 * @param {Divs} divs 
 */
function registerEvent(divs) {
	// Fill edit form
	$(".a__edit").on("click", function(e) {
		e.preventDefault();
		editFillForm($(this).attr("token-id"));
		$("form").get(0).scrollIntoView({behavior: "smooth"});
	});
	
	// Show detail page
	$(".a__symbol").on("click", function(e) {
		e.preventDefault();
		window.history.pushState({}, "", `/detail.aspx?id=${$(this).attr("token-id")}`);
		divs.showTokenDetail();
	});
	
	$("#token-form").on("submit", function(e) {
		e.preventDefault();
		submitTokenForm(new FormData(this), divs);
	})
}


// Docuemnt ready
$(function() {
	g_page = 1; // Default page
	
	// Handle all HTML Element
	const divs = new Divs();
	
	window.addEventListener("popstate", function(e) {
		if (this.window.location.pathname === "/detail.aspx") {
			divs.showTokenDetail();
		} else {
			divs.showDashboard();
		}
	})
});