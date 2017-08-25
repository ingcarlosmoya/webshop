
function SetPersistence(item) {
    var storage = $(item).attr("id");
    var persistOnDataBase;

    if (storage == 'memory') {
        $("#memory").attr("disabled", "disabled");
        $("#database").removeAttr("disabled");
        persistOnDataBase = false;
    }
    else {
        $("#database").attr("disabled", "disabled");
        $("#memory").removeAttr("disabled");
        persistOnDataBase = true;
    }

    $.ajax({

        type: 'POST', 
        url: '/../../Product/SetPersistence',
     
        data: { 'persistOnDataBase': persistOnDataBase }, 
        success: function (result) {
            if (result == true) {

            } else {

            }
        }
    });
}


$(document).ready(function () {
    GetPersistence();
})

function GetPersistence() {
    $("#database").removeAttr("disabled");
    $("#memory").removeAttr("disabled");

    $.ajax({
        url: '/../../Product/SetPersistence',
        dataType: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            var responseAjax = Boolean(data.responseText);
            if (responseAjax) {
                $("#database").attr("disabled", "disabled");
                $("#memory").removeAttr("disabled");
               
            }
            else {
                $("#memory").attr("disabled", "disabled");
                $("#database").removeAttr("disabled");
            }
        },
        error: function (xhr) {
            alert('error');
        }
    });
}
