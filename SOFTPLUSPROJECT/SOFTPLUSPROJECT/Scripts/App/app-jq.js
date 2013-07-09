
// this function is used for check internet connection
function CheckInternetConnection() {

    if (navigator.onLine) {
        return true;
    } else {
        return false;
    }

}
//end CheckInternetConnection()

//this function in used for check LocalStorage support
function CheckLocalStorageSupport() {

    if (localStorage) {
        return true;
    } else {
        return false;
    }

}
//end CheckLocalStorageSupport()

// this function is used to reset details entity field
function ResetDetails() {

    $("#ItemSizeId").val("0");
    $('#Quantity').val('');

}
//end ResetDetails()

// this function is used to add new DetailInfo
function AddNewDetailInfo() {

    var tableDetails = document.getElementById("tblDetails");

    var rowCount = tableDetails.rows.length;
    var row = tableDetails.insertRow(rowCount);

    var itemSizeId = $("#ItemSizeId option:selected").val();
    var itemSizeName = $("#ItemSizeId option:selected").text();
    var quantity = $('#Quantity').val();

    var cellItemId = row.insertCell(0);
    cellItemId.innerHTML = itemSizeId;

    var cellItemName = row.insertCell(1);
    cellItemName.innerHTML = itemSizeName;

    var cellQuantity = row.insertCell(2);
    cellQuantity.innerHTML = quantity;

}
//end AddNewDetailInfo()

//this variable is used global

var localObjlList = [];

// end global variable

//this function is used Set Local Storage Object
function SetLocalStorageObject(objects) {

    //Push localObj to global localObjList
    localObjlList.push(objects);

    //Clear local storage
    localStorage.clear();

    //Adding the object list to local storage array
    localStorage["localObjLst"] = JSON.stringify(localObjlList);

    return true;

}
// end SetLocalStorageObject()

//this function is used Get Local Storage Object
function InitialLoadLocalStorageObject() {

    //Check localStorage Object Exiting
    if (localStorage.getItem("localObjLst") !== null) {

        //// Retrieve the object from local storage
        //var retrievedlocalObject = localStorage.getItem('localObjLst');

        // Retrieve the object from local storage array
        var retrievedlocalObjects = localStorage['localObjLst'];

        var localStorageObjectListArray = JSON.parse(retrievedlocalObjects);

        for (var i = 0; i < localStorageObjectListArray.length; i++) {

            var localStorageObj = localStorageObjectListArray[i];

            //Push localStorageObj to global localObjList
            localObjlList.push(localStorageObj);

        } //end for

    } //end if

}
// end InitialLoadLocalStorageObject()

// this function is used to save master details
function MasterDetailsFormSave(postUrl, model, modelList) {

    var paramValue = JSON.stringify({ model: model, modelList: modelList });

    //Check Internet Connection
    if (CheckInternetConnection()) {

        $.ajax({
            url: postUrl,
            type: 'POST',
            dataType: 'json',
            data: paramValue,
            contentType: 'application/json; charset=utf-8',
            success: function(data) {

                //JQDialogAlert mass, status, ok click
                JQDialogAlert(data.d.msg, data.d.status, function() {
                    if (data.d.status == 'success')
                        window.location = "/Default.aspx";
                }); //end if

            },
            error: function(objAjaxRequest, strError) {

                var respText = objAjaxRequest.responseText;
                //JQDialogAlert mass, status
                JQDialogAlert(respText, 'info');

            }

        });

    } //end if 
    else {

        //Check Local Storage Support in Browser
        if (CheckLocalStorageSupport()) {

            var localObj = {};
            localObj.Master = model;
            localObj.Details = modelList;

            //Set local storage
            if (SetLocalStorageObject(localObj)) {

                //JQDialogAlert mass, status
                JQDialogAlert('Opp! You are disconnected from internet. So data saved successfully to local.', 'success');

            } //end if


        } //end if

    } //end else

}
// end MasterDetailsFormSave()

//this function  is used to Check Internet Connection After 10 Seconds later
function CheckingInternetConnectionBySetInterval() {

    //Use setInterval for Checking Internet Connection
    setInterval(function() {

        if (!CheckInternetConnection()) {

            ShowCommonMessage("Opp! You are disconnected from internet.");

        } //end if

    }, 10000); //10 seconds

}
// end CheckingInternetConnectionBySetInterval()

//this function  is used to Check Internet Connection After 10 minutes later and Save by Ajax Post
function OfflineDataAjaxPost() {


    //Use setInterval for Checking Internet Connection
    setInterval(function() {

        //Check Internet Connection if true
        if (CheckInternetConnection()) {

            //Check localStorage Object Exiting
            if (localStorage.getItem("localObjLst") !== null) {

                var offlineModelList = [];

                //// Retrieve the object from local storage
                //var retrievedlocalObject = localStorage.getItem('localObjLst');

                // Retrieve the object from local storage array
                var retrievedlocalObjects = localStorage['localObjLst'];

                var localStorageObjectListArray = JSON.parse(retrievedlocalObjects);

                for (var i = 0; i < localStorageObjectListArray.length; i++) {

                    var localStorageObj = localStorageObjectListArray[i];

                    var offlineMasterDetailViewModel = {};
                    offlineMasterDetailViewModel.Master = localStorageObj.Master;
                    offlineMasterDetailViewModel.Details = localStorageObj.Details;


                    //Push offlineMasterDetailViewModel
                    offlineModelList.push(offlineMasterDetailViewModel);

                } //end for

                var paramValue = JSON.stringify({ offlineModelList: offlineModelList });

                $.ajax({
                    url: '/Default.aspx/OfflineAjaxSave',
                    type: 'POST',
                    dataType: 'json',
                    data: paramValue,
                    contentType: 'application/json; charset=utf-8',
                    success: function(data) {

                        if (data.d.status == 'success') {

                            ShowCommonMessage("Oh! Some local data saved successfully to server.");

                            //Clear local Storage
                            localStorage.clear();

                        } //end if

                    },
                    error: function() {

                        return;

                    }

                });

            } //end if


        } //end if

    }, 600000);        //10 minutes = 600000



}
//end OfflineDataAjaxPost()

//this function  is used to show message
function ShowCommonMessage(mess) {

    $('#commonMessage').html('');
    $('#commonMessage').html(mess);
    $('#commonMessage').delay(400).slideDown(400).delay(3000).slideUp(400);

}
//end ShowCommonMessage()

$(document).ready(function() {

    //Ajax Post Offline Data After 10 minutes
    OfflineDataAjaxPost();

    //Checking Internet Connection After 10 Seconds
    CheckingInternetConnectionBySetInterval();

    //Initial Load Pre Local Storage Object To Browser
    InitialLoadLocalStorageObject();

    //start Add DetailInfo

    $('#btnDetailsAdd').click(function() {

        var customerId = $('#CustomerId').val();
        var customerName = $('#CustomerName').val();
        var quantity = $('#Quantity').val();
        var itemSizeId = $("#ItemSizeId option:selected").val();
        var itemSizeName = $("#ItemSizeId option:selected").text();

        var detailInfoViewModel = {};
        detailInfoViewModel.CustomerId = customerId;
        detailInfoViewModel.CustomerName = customerName;
        detailInfoViewModel.Quantity = quantity;
        detailInfoViewModel.ItemSizeId = itemSizeId;
        detailInfoViewModel.ItemSizeName = itemSizeName;

        var paramValue = JSON.stringify({ detailInfoViewModel: detailInfoViewModel });

        $.ajax({
            url: 'Default.aspx/Add',
            type: 'POST',
            dataType: 'json',
            data: paramValue,
            contentType: 'application/json; charset=utf-8',
            success: function(data) {

                if (data.d.msg == "True") {

                    //Add new DetailInfo
                    AddNewDetailInfo();

                    //Reset DetailInfo
                    ResetDetails();

                } //end if
                else {

                    //JQDialogAlert mass, status
                    JQDialogAlert(data.d.msg, data.d.status);

                } //end else

            }, //end success
            error: function(objAjaxRequest, strError) {

                var respText = objAjaxRequest.responseText;
                //JQDialogAlert mass, status
                JQDialogAlert(respText, 'info');

            } //end error

        }); //end ajax post

        return false;

    });

    //end Add DetailInfo

    //start Reset DetailInfo

    $('#btnDetailsReset').click(function() {

        ResetDetails();

        return false;

    });

    //end reset DetailInfo

    //start Form Save

    $('#btnSave').click(function() {

        var postUrl = '/Default.aspx/AjaxSave';

        //Master Model
        var customerId = $('#CustomerId').val();
        var customerName = $('#CustomerName').val();
        var address = $('#Address').val();

        var masterDetailInfoViewModel = {};
        masterDetailInfoViewModel.CustomerId = customerId;
        masterDetailInfoViewModel.CustomerName = customerName;
        masterDetailInfoViewModel.Address = address;

        //Details Model List
        var detailInfoViewModelList = [];

        //Get Details Table
        var tableDetails = document.getElementById("tblDetails");
        var rowCount = tableDetails.rows.length;

        for (var i = 0; i < rowCount; i++) {

            var row = tableDetails.rows[i];

            if (row.rowIndex != 0) {

                var itemSizeId = row.cells[0].innerHTML;
                var itemSizeName = row.cells[1].innerHTML;
                var quantity = row.cells[2].innerHTML;

                var detailInfoViewModel = {};
                detailInfoViewModel.CustomerId = customerId;
                detailInfoViewModel.CustomerName = customerName;
                detailInfoViewModel.Quantity = quantity;
                detailInfoViewModel.ItemSizeId = itemSizeId;
                detailInfoViewModel.ItemSizeName = itemSizeName;

                // Push Data to productList Array
                detailInfoViewModelList.push(detailInfoViewModel);

            } //end if
        } //end for

        MasterDetailsFormSave(postUrl, masterDetailInfoViewModel, detailInfoViewModelList);

        return false;

    });

    //end Form Save

});