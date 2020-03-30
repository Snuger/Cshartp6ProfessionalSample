/// <reference path="oidc-client.js" />

function log() {
	document.getElementById('results').innerText = '';

	Array.prototype.forEach.call(arguments, function(msg) {
		if (msg instanceof Error) {
			msg = 'Error: ' + msg.message;
		} else if (typeof msg !== 'string') {
			msg = JSON.stringify(msg, null, 2);
		}
		document.getElementById('results').innerHTML += msg + '\r\n';
	});
}

document.getElementById('login').addEventListener('click', login, false);
document.getElementById('api').addEventListener('click', api, false);
document.getElementById('logout').addEventListener('click', logout, false);

var config = {
	authority: 'http://localhost:5000/identity/',
	//client_id: 'ttow_implicit',
	client_id: 'sampke',	
	redirect_uri: 'http://localhost:5003/callback.html',
	response_type: 'id_token token',
	scope: 'openid 784698765322752000',
	post_logout_redirect_uri: 'http://localhost:5003/index.html'
};
var mgr = new Oidc.UserManager(config);

mgr.getUser().then(function(user) {
	if (user) {
		log('User logged in', user.profile);
	} else {
		log('User not logged in');
	}
});

function login() {
	mgr.signinRedirect();
}

function api() {
	mgr.getUser().then(function(user) {
		var url = 'http://localhost:5088/WeatherForecast';

		var xhr = new XMLHttpRequest();
		xhr.open('GET', url);
		xhr.onload = function() {
			log(xhr.status, JSON.parse(xhr.responseText));
		};
		xhr.setRequestHeader('Authorization', 'Bearer ' + user.access_token);
		xhr.send();
	});
}

function logout() {
	mgr.signoutRedirect();
}
