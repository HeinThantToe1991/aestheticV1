 $(document).ready(function () {
        var language = localStorage.getItem("Sitelanguage");
        if (language != null) {
            $('#selLanguage').val(language);              
        }
        else {
            var valueSelected = $('#selLanguage').val();
            localStorage.removeItem('Sitelanguage');
            localStorage.setItem('Sitelanguage', valueSelected);
            ChangeLanguage(valueSelected);
        }
    });
    $('#selLanguage').on('change', function (e) {
        var valueSelected = this.value;          
        localStorage.removeItem('Sitelanguage');
        localStorage.setItem('Sitelanguage', valueSelected);
        ChangeLanguage(valueSelected);
    });

    function ChangeLanguage(culture) {
        var cultureURL;
        $.ajax({
            type: "GET",
            url: "/Home/ChangeLanguage",
            data: { culture: culture },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                cultureURL = response;
                location.reload();
            },
            //failure: function (response) {
            //    AlertMessage(1, response.message);
            //},
            error: function (response) {
                console.error("Error:", response);
            }
        });
    }