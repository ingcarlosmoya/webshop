
function SetPersistence(persistOnDataBase)
{
    alert(persistOnDataBase);
    $.ajax({
       
        type: 'POST', // use Get for [HttpGet] action or POST for [HttpPost]
        //url: '@Url.Action("SetPersistence", "Product")', // Controller/View  
        url: '/../../Product/SetPersistence',
        //contentType: 'application/json', not needed
        //dataType: 'jsonp', jsonp is for sending to a site other than the current one.. 
        data: { 'persistOnDataBase': persistOnDataBase},  // no need to stringify
        success: function (result) {
            if (result == true) {
                alert("cambio")
            } else {
                alert("fallo");
            }
        }
    });
}