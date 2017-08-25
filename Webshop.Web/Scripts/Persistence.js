
function SetPersistence(item)
{
    var storage = $(item).attr("id");
    var persistOnDataBase;

    if (storage == 'memory')
    {
        $("#memory").attr("disabled", "disabled");
        $("#database").removeAttr("disabled");
        persistOnDataBase = false;
    }
    else
    {
        $("#database").attr("disabled", "disabled");
        $("#memory").removeAttr("disabled");
        persistOnDataBase = true;
    }

    $.ajax({
       
        type: 'POST', // use Get for [HttpGet] action or POST for [HttpPost]
        //url: '@Url.Action("SetPersistence", "Product")', // Controller/View  
        url: '/../../Product/SetPersistence',
        //contentType: 'application/json', not needed
        //dataType: 'jsonp', jsonp is for sending to a site other than the current one.. 
        data: { 'persistOnDataBase': persistOnDataBase},  // no need to stringify
        success: function (result) {
            if (result == true) {
                
            } else {
                
            }
        }
    });
}
