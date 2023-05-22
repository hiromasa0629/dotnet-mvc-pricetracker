// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var g_tokens;

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
	constructor(tokenDetailDiv, dashboardDiv) {
		this.tokenDetailDiv = tokenDetailDiv;
		this.dashboardDiv = dashboardDiv;
		this.missingIdDiv = $("#error-div");
		
		const pathname = window.location.pathname;
		if (pathname !== "/detail.aspx") {
			this.showDashboard();
		} else {
			this.showTokenDetail()
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
		success: function(res) {
			callback(res);
		},
		error: function(xhr, status, error) {
			callback({ xhr, status, error });
		}
	})
}

function submitTokenForm(formData) {
	$.ajax({
		url: `${apiUrl}/token`,
		method: "POST",
		data:  formData,
		contentType: false,
		processData: false,
		success: function (res) {
			renderPage();
		},
		error: function (xhr, status, error) {
			console.log(xhr.responseJSON.Message);
		}
	})
}

function renderPage(divs) {
	getTokens(function(res) {
		if (res.error === "error") {
			divs.showError("Error");
			return;
		}
		
		g_tokens = res;
		console.log(g_tokens);
		renderDoughnut();
	})
}

$(function() {
	const divs = new Divs($("#token-detail"), $("#dashboard"));
	
	renderPage(divs);
	
	window.addEventListener("popstate", function(e) {
		if (this.window.location.pathname === "/detail.aspx") {
			divs.showTokenDetail();
		} else {
			divs.showDashboard();
		}
	})
	
	// Fill edit form
	$(".a__edit").on("click", function(e) {
		e.preventDefault();
		editFillForm($(this).attr("token-id"));
	});
	
	// Show detail page
	$(".a__symbol").on("click", function(e) {
		e.preventDefault();
		window.history.pushState({}, "", `/detail.aspx?id=${$(this).attr("token-id")}`);
		divs.showTokenDetail();
	});
	
	$("#token-form").on("submit", function(e) {
		e.preventDefault();
		
		const formData = new FormData(this);
		submitTokenForm(formData);
		
	})
});