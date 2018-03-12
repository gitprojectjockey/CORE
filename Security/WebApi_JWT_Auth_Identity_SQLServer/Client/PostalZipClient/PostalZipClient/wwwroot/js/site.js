// Write your JavaScript code.
//Remove conflix bootstrap jquery dialog x on close button
//$.fn.bootstrapBtn = $.fn.button.noConflict();
$(document).ready()
{
    function serviceEndPoints(isProduction) {

        var baseDev = 'http://localhost:52549/API/security/';
        var baseProd = 'http://localhost:8081/';
        var login = 'login';
        var register = 'register';
        var base = isProduction == true ? baseProd : baseDev;

        this.loginUrl = function () {
            return base + login;
        }

        this.registerUrl = function () {
            return base + register;
        }
    };


    //if (sessionStorage.getItem('accessToken') === null) {
    //    window.location.href = 'login.html';
    //}

    $('#btnLogin').click(function () {
        var uri = new serviceEndPoints(false).loginUrl();

        var loginModel = {
            Username: 'enordin',
            Password: 'Wr400fg!',
            Email:  'enordin@comcast.com'
        };

        $.ajax({
            method: 'POST',
            url: uri,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            data: JSON.stringify(loginModel),
            success: function (response) {
                // api returns JWT token made up of user claims in response object and we save in session.
                sessionStorage.setItem('accessToken', response.token);
                window.location.href = '../HtmlViews/ProductsByCompanyDisplay.html';
            },
            error: function (jqxhr) {
                // api returns error in jquery xml http request object.
                $('#errorMessage').text(jqxhr.responseText);
                $('#validationError').show('fade');
            }
        });
    });

    $('#btnRegister').click(function () {
        var uri = new serviceEndPoints(false).registerUrl();

        var registerModel = {
            "username": "enordin",
            "password": "Wr400fg!",
            "ConfirmPassword": "Wr400fg!",
            "Email": "enordin@comcast.com",
            "Age": "46",
            "District": "South.West"
        };

        $.ajax({
            method: 'POST',
            url: uri,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            data: JSON.stringify(registerModel),
            success: function (response) {
                // api returns JWT token made up of user claims in response object and we save in session.
                var identity = response.identityResult;
                window.location.href = '../HtmlViews/ProductsByCompanyDisplay.html';
            },
            error: function (jqxhr) {
                // api returns error in jquery xml http request object.
                $('#errorMessage').text(jqxhr.responseText);
                $('#validationError').show('fade');
            }
        });
    });
}