var userId = 0, userTypeId = 0, inducId = 0, inductionId = 0, doc;
var perPageRecords = 10, pageNo = 1, totalCount = 0, currentPageId = 0, searchFunction = "";

$(document).ready(function () {

    userId = ConvertToInt($("#hfdLoggedInUserId").val());
    userTypeId = ConvertToInt($("#hfdLoggedInUserTypeId").val());
    inductionId = ConvertToInt($("#hfdInductionId").val());
    inducId = ConvertToInt($("#hfdInductionId").val());

    GetContactQuestionGetNoReplied();

    $(document).on('click', '.mdlClose', function () {
        $('.modell').modal('hide');

    });

    $(document).on('change', '.flup', function () {

        var idAttr = $(this).attr("id");

        var ctrlId = idAttr.replace("fl", "");

        var ctrlImg = "img" + ctrlId;
        var ctrlRmv = "rmv" + ctrlId;

        var ext = $(this).val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['png', 'jpg', 'jpeg', 'pdf']) == -1) {

            $(this).val(null);
            $('#' + ctrlImg).attr('src', "");
            $('#' + ctrlImg).hide();
            $('#' + ctrlRmv).hide();
            alert('invalid extension!');
        }
        else {

            var _size = this.files[0].size;
            var iSize = (this.files[0].size / 1024);
            iSize = (Math.round(iSize * 100) / 100)

            //var fSExt = new Array('Bytes', 'KB', 'MB', 'GB'),
            //    i = 0; while (_size > 900) { _size /= 1024; i++; }
            //var fSExt = new Array( 'KB'),
            //    i = 0; while (_size > 900) { _size /= 1024; i++; }
            //var exactSize = (Math.round(_size * 100) / 100);
            if (iSize > 1000) {
                //var exactSizeFull = (Math.round(_size * 100) / 100) + ' ' + fSExt[i];
                alert("File size " + iSize + "KB, greater then 500 KB. Please compress image size.");
                $(this).val("");
                $('#' + ctrlImg).attr('src', "");
                $('#' + ctrlImg).hide();

                $('#' + ctrlRmv).hide();
            }
            else {
                readURL(this, ctrlId);
            }
        }
    });

    $(document).on('click', '.removeImage', function () {

        var idAttr = $(this).attr("id");
        var ctrlId = idAttr.replace("rmv", "");

        var ctrlImg = "img" + ctrlId;
        var flId = "fl" + ctrlId;
        $('#' + ctrlImg).attr('src', "");
        $('#' + ctrlImg).hide();

        $(this).hide();
        $('#' + flId).val(null);

    });

    $(document).on('click', '.liPage', function () {

        $(".page-item").removeClass("active");

        var id = ConvertToInt($(this).attr("id").replace("pgli", ""));

        if (id == pageNo) {

            $("#pgli" + currentPageId).addClass("active");
            return;
        }

        if (id == 0) {
            pageNo = 1;
        }
        else if (id == -1) {
            var lastPage = Math.ceil(totalCount / perPageRecords);
            pageNo = lastPage;
        }
        else {
            pageNo = id;
        }

        var fn = searchFunction;

        if (fn in window) {
            window[fn]();
        }

    });

    //setInterval(GetContactQuestionGetNoReplied, 60000);
});

function GetContactQuestionGetNoReplied() {

    try {
        var resp = CallActionGet("/ContactUsAdmin/ContactQuestionGetNoReplied");
        $("#spnQuestionNoReplied").html(resp.id);
    } catch (e) {
        $("#spnQuestionNoReplied").html("0");
    }

}

function readURL(input, ctrlId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        ctrlImg = "img" + ctrlId;
        reader.onload = function (e) {
            $('#' + ctrlImg).attr('src', e.target.result);
            $('#' + ctrlImg).show();

            var ctrlRmv = "rmv" + ctrlId;
            $('#' + ctrlRmv).show();
        }
        reader.readAsDataURL(input.files[0]);
    }
}

function printDiv() {

    var result = false;
    try {

        var divToPrint = document.getElementById('divprint');
        var newWin = window.open('', 'Print-Window');
        newWin.document.open();
        newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
        newWin.document.close();
        setTimeout(function () { newWin.close(); }, 10);
        result = true;

    } catch (e) {
        result = false;
    }
    return result;
}

function ShowingRowsText(perPageRecords, count, pageNum) {

    var rcordsNow = pageNum * perPageRecords;
    var startRecords = rcordsNow - perPageRecords;
    if (rcordsNow > count)
        rcordsNow = count;

    startRecords = startRecords + 1;
    $("#divShowingResult").html("Showing " + startRecords + " to " + rcordsNow + " of " + count + " entries");


}

function CreatePaginationHtml(perPageRecords, count, pageNum) {

    try {

        ShowingRowsText(perPageRecords, count, pageNum);
        $("#divPagination").show();

        var htmlPagination = $("#templatePagination").html();
        var pageStart = 1, pageEnd = 5;
        var lastPage = Math.ceil(count / perPageRecords);

        if (pageNum > 3) {
            pageStart = pageNum - 2;
            pageEnd = pageNum + 2;
        }

        if (pageEnd > lastPage)
            pageEnd = lastPage;

        var lisHtml = "";
        for (var i = pageStart; i <= pageEnd; i++) {
            lisHtml = lisHtml + "<li class='liPage' id='" + i + "'> " + i + "</li>";
        }
        htmlPagination = htmlPagination.replace(/{#lastPage#}/g, lastPage).replace(/{#Pagination#}/g, lisHtml);
        $(".list_pagination").html(htmlPagination);

        $("#" + pageNum).addClass("current");

    } catch (e) {
        $("#liPagination").hide();
    }
}