var dateFormatDatePicker = "", docGen;

var listInductionCalender = [];
$(document).ready(function () {


    //var filePath = "/Html/General.html";
    //docGen = GetDocToDOMParser(filePath);
    //$(".divProfile").html(htmlProfileEnd);
    //$("#divReopen").remove();


    //$('#modlNotify').modal('show');

    CalenderLevelGetByLevelAndInduction();

    dateFormatDatePicker = 'dd/mm/yyyy';
    $(document).on('keyup', '.event-keyup', function () {
        CallFunctionCrl(ConvertToString($(this).attr("fn")), this);
    });

    $(document).on('change', '.ddl-event', function () {
        CallFunctionCrl(ConvertToString($(this).attr("fn")), this);
    });

    $(document).on('click', '.event-click', function () {
        CallFunctionCrl(ConvertToString($(this).attr("fn")), this);
    });

    $(document).on('keyup', '.csFilterDiv', function () {
        var did = $(this).attr("did");
        var value = $(this).val().toLowerCase().trim();
        $("#" + did ).filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });

    $(document).on("input", ".int", function () {
        var result = this.value.replace(/\D/g, '');
        try {
            var len = ConvertToInt($(this).attr("len"));
            if (result.length > len)
                result = result.substring(0, len);
        } catch (e) {
        } finally {
            this.value = result;
        }
    });

    $(document).on('click', '.next-prev', function () {
        var statusId = ConvertToInt($(this).attr("statusId"));
        var typeId = ConvertToInt($(this).attr("typeId")); //  for Pre typeId= 1, Next=6

        if (typeId > 1) {
            var resp = ApplicationStatusAddUpdate(53, statusId);
            if (resp.status == true) {
                if (statusId == 8)
                    window.location = "/application-completed";
                else
                    $("#ankPL" + statusId).click();
            }
            else {
                alert(resp.msg);
            }
        }
        else
            $("#ankPL" + statusId).click();
    });

    SetMeritPanels();
});

//#region Ajax Method
function CallAction(type, url, data) {
    var result;
    $.ajax({
        type: type,
        url: url,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resp) {
            result = resp;
        },
        error: function (resp) {
            alert(resp);
        }
    });
    return result;
}

function CallActionGet(url) {
    var result;
    $.ajax({
        type: "GET",
        url: url,
        data: {},
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resp) {
            result = resp;
        },
        error: function (resp) {
            alert(resp);
        }
    });
    return result;
}

function CallActionPost(url, obj) {
    var result;
    $.ajax({
        type: "POST",
        url: url,
        data: obj,
        dataType: "json",
        async: false,
        success: function (resp) {
            result = resp;
        },
        error: function (resp) {
            alert(resp);
        }
    });
    return result;
}
//#endregion

function GetDateDifferenceLable(dateValue, sec) {

    var obj = {};
    var toDate = new Date(dateValue).getTime();
    var dateTime = $("#hfdDateTime").val();
    var fromDate = new Date(dateTime).getTime();
    var distance = toDate - fromDate;

    distance = distance - sec;
    var result = "";

    obj.distance = distance;

    if (distance < 0) {
        result = "Time Expired"; //EXPIRED
    }
    else {

        var days = 0;
        try {
            days = Math.floor(distance / (1000 * 60 * 60 * 24));
            days = ConvertToInt(days);
        } catch (e) {
            days = 0;
        }

        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        if (days > 0) {
            result = days + " d";
        }
        result = result + hours + "h " + minutes + "m " + seconds + "s ";
    }

    obj.lable = result;
    return obj;
}

function GetTimeDifferenceMinSec(start, diff) {

    var obj = {};
    var distance = start - fromDate;

    distance = distance - diff;
    var result = "";
    obj.distance = distance;
    if (distance < 0) {
        result = "Time Expired"; //EXPIRED
    }
    else {
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);
        result =  minutes + " m : " + seconds + " s ";
    }
    obj.lable = result;
    return obj;
}

function CalenderLevelGetByLevelAndInduction() {

    var obj = {};
    obj.levelId = 3
    var resp = CallActionPost("/Common/CalenderLevelGetByLevelAndInduction?applicantId=", obj);
    try {
        listInductionCalender = resp.Table;
    } catch (e) {
    }
}

function SetMeritPanels() {

    var roundId = 0;
    if (listInductionCalender != null && listInductionCalender.length > 0) {
        try {
            roundId = ConvertToInt($.grep(listInductionCalender, function (n, i) {
                return n.calenderId == 61;
            })[0].statusId);
        } catch (e) {
            roundId = 0;
        }
    }

    if (roundId != 11) {
        try {
            roundId = ConvertToInt($.grep(listInductionCalender, function (n, i) {
                return n.calenderId == 69;
            })[0].statusId);
        } catch (e) {
            roundId = 0;
        }
    }

    if (roundId != 11) {
        try {
            roundId = ConvertToInt($.grep(listInductionCalender, function (n, i) {
                return n.calenderId == 71;
            })[0].statusId);
        } catch (e) {
            roundId = 0;
        }
    }

    if (roundId != 11) {
        try {
            roundId = ConvertToInt($.grep(listInductionCalender, function (n, i) {
                return n.calenderId == 76;
            })[0].statusId);
        } catch (e) {
            roundId = 0;
        }
    }

    if (roundId != 11) {
        try {
            roundId = ConvertToInt($.grep(listInductionCalender, function (n, i) {
                return n.calenderId == 81;
            })[0].statusId);
        } catch (e) {
            roundId = 0;
        }
    }

    if (roundId != 11) {
        $(".pnlMeritList").remove();
    }
    else {
        $("#linkMeritList").show();
    }

}

function WebUrlToLowerCase() {
    var pageUrl = ConvertToString(window.location.href.toLowerCase());
    window.history.pushState(null, "", pageUrl);
}

function ApplicationStatusAddUpdate(statusTypeId, statusId) {
    var obj = {};
    obj.statusTypeId = statusTypeId;
    obj.statusId = statusId;
    var resp = CallActionPost("/ApplicantProfile/ApplicantStatusUpdate", obj);
    return resp;
}

//#region Profile/Application 
var profileTotalStep = 0, profileStatus = 1, profileCurrentStatus = 1;
function ProfileProcessGetInfoByParam(stepId) {
    var obj = {};
    obj.stepId = stepId;
    profileCurrentStatus = stepId;
    var resp = CallActionPost("/ApplicantProfile/ProfileProcessGetInfoByParam", obj);
    try {
        if (resp != null) {
            //#region Check is Profile Process Status
            // 1 = pending, 11 = Open/start, 21 = closed/completed
            var objStatus = resp.Table[0];
            if (objStatus.id != 11) {
                window.location = "/ind-step-status?clid=21";
            }
            //#endregion

            var listLink = resp.Table1;
            var objProfileStatus = resp.Table2[0];
            CreateHtmlBreadCrumb(listLink, objProfileStatus);
        }
    } catch (e) {
    }
    return resp;
}
function CreateHtmlBreadCrumb(list, obj) {
    var html = "";

    try {
        profileTotalStep = list.length;
        profileStatus = obj.statusId;
        var cls = "";
        var objItem = {};
        for (var i = 0; i < list.length; i++) {

            objItem = list[i];
            cls = "disable";
            if (obj.statusId >= objItem.id) {
                cls = "complete";
                if (obj.statusId == objItem.id)
                    cls = "active";
            }
            html = html + "<a id='ankPL" + objItem.id + "' href='#' class='" + cls + " event-click'  statusId='" + objItem.id + "' counter='" + (i + 1) + "'"
                + " link='" + objItem.shortDesc + "'"
                + " fn='ClickEventLinkProfile' heading-info='" + objItem.detail + "' >"
                + objItem.nameDisplay + "</a>";

        }
    } catch (e) {
    }
    $("#profileLinks").html(html);
    $("#ankPL" + profileCurrentStatus).click();

    //#region Set Next Previous Button status
    var statusIdPrev = 0;
    var statusIdNext = 0;
    try {
        // Get Index of list by Current page statusId
        var indx = -1
        try {
            indx = list.findIndex((obj => obj.id == profileCurrentStatus));
        } catch (e) {
            indx = -1
        }
        var lastIndex = list.length - 1;

        if (indx > -1 && indx < lastIndex) {
            statusIdNext = list[indx + 1].id;
        }
        else if (indx == lastIndex) {
            statusIdNext = list[lastIndex].id + 1;
        }

        if (indx > 0) {
            statusIdPrev = list[indx - 1].id;
        }

    } catch (e) {
    }
    $("#ankContinue").attr("statusId", statusIdNext);
    $("#ankPrev").attr("statusId", statusIdPrev);
    //#endregion

}
function ClickEventLinkProfile(ctrl) {

    var statusId = ConvertToInt($(ctrl).attr("statusId"));
    var counter = ConvertToInt($(ctrl).attr("counter"));
    var headInfo = ConvertToString($(ctrl).attr("heading-info"));
    if (statusId <= profileStatus) {

        if (statusId != profileCurrentStatus) {
            var link = "/" + ConvertToString($(ctrl).attr("link"));
            profileCurrentStatus = statusId;

            if (statusId > profileStatus) {
                var obj = {};
                obj.stepId = stepId;
                // Check Current Status of application/profile
                var resp = CallActionPost("/AgpplicantProfile/ApplicationStatusGet");
                if (resp.statusId >= statusId) {
                    window.location = link;
                }
            }
            else window.location = link;

        }
        else {
            var heading = headInfo + " : Step " + counter + " of " + profileTotalStep;
            $("#spnProfileHeading").html(heading);
        }

    }

}

//#endregion

function GetDDLResponse(obj) {
    var resp = [];
    try {
        resp = CallActionPost("/Common/GetAllDDL", obj);
    } catch (e) {
        resp = [];
    }
    return resp;
}

function DDLGetAndBind(obj, ddlId = "", DefaultText = "-- Select One --", DefaultValue = 0, SelectedValue = 0) {
    try {
        var resp = CallActionPost("/Common/GetAllDDL", obj);
        if (resp != null && resp.length > 0) {
            DDLBindList(ddlId, resp, DefaultText, DefaultValue, SelectedValue)
        }
    } catch (e) {
    }
}

function CallFunctionCrl(fn, ctrl) {
    if (fn != "") {
        if (fn in window) {
            window[fn](ctrl);
        }
    }
}

function RemoveMultiSpace(txt) {
    try {
        txt = txt.replace(/\s\s+/g, ' ');
    } catch (e) {
    }
    return txt;
}

function GetCurrentDate() {
    return JsonToDate(CallActionGet("/Common/GetCurrentDateTime"));
}

function CreateGuid() {


    var dtStr = GetCurrentDate();

    var dt = new Date(dtStr);

    var dd = dt.getDate();
    var mm = Date.locale.month_names_short[dt.getMonth()]; // dt.getMonth() + 1; //January is 0!
    var yyyy = dt.getFullYear();

    var seconds = dt.getSeconds();
    var minutes = dt.getMinutes();
    var hour = dt.getHours();

    var guid = dd + "" + mm + "" + yyyy + "" + hour + "" + minutes + "" + seconds;

    return guid;

}

function GetHtmlFromFile(filePath) {
    var html = "";
    try {
        var resp = CallActionGet("/AjaxMain/HtmlFileRead?filePath=" + filePath);
        html = resp.html;
    } catch (e) {
        html = "";
    }
    return html;
}

function GetDocToDOMParser(filePath) {

    var doc;
    try {
        var html = GetHtmlFromFile(filePath);
        doc = new DOMParser().parseFromString(html, "text/html");
    } catch (e) {
    }
    return doc;
}



function GetHtmlFromDoc(doc, id) {
    var html = "";
    try {
        var ele = doc.getElementById(id);
        html = ele.innerHTML;
    } catch (e) {
        html = "";
    }
    return html
}

function FormatNumberToLength(num, length) {
    var r = "" + num;
    while (r.length < length) {
        r = "0" + r;
    }
    return r;
}

function DDLSetToDefault(ddlId, defaultText, defaultValue) {

    if (defaultText == "") defaultText = "Loading...";
    $("#" + ddlId).empty().append("<option selected='selected' value='" + defaultValue + "'>" + defaultText + "</option>");

}

function DDLSetItem(ddlId, defaultText, defaultValue) {

    if (defaultText == "") defaultText = "Loading...";
    if (defaultValue == "0") defaultValue = "-1";

    $("#" + ddlId).empty().append("<option selected='selected' value='" + defaultValue + "'>" + defaultText + "</option>");

}

function DDLBindList(ddlId, list, DefaultText = "-- Select One --", DefaultValue = 0, SelectedValue = 0) {
    $(ddlId).empty();
    try {
        $(ddlId).attr("disabled", false);

        if (list != null && list.length > 0) {

            if (DefaultValue != -9) {
                $(ddlId).append("<option selected='selected' value='" + DefaultValue + "'>" + DefaultText + "</option>");
            }

            $(list).each(function (x, index) {
                $(ddlId).append("<option value='" + index.value + "'>" + index.key + "</option>");
            });

            if (SelectedValue == -1) {
                $(ddlId).val(list[0].value);
            }
            else if (SelectedValue == -2) {
                if (list.length == 1)
                    $(ddlId).val(list[0].value);
            }
            else if (SelectedValue == -3) {
                if (list.length == 1) {
                    $(ddlId).val(list[0].value);
                    $(ddlId).attr("disabled", true);
                }
                else $(ddlId).val(list[0].value);
            }
            else if (SelectedValue > 0) {
                $(ddlId).val(SelectedValue);
            }

        }
        else {
            $(ddlId).append("<option selected='selected' value='0'>Loading...</option>");
        }
    } catch (e) {
        $(ddlId).append("<option selected='selected' value='0'>Loading...</option>");
    }

}

function ToOnlyNumber(inputVal) {
    var result;
    try {
        result = inputVal.replace(/\D/g, '');
    } catch (e) {
        result = inputVal;
    }
    return result;

}

function ConvertToInt(inputVal) {

    var outputVal = 0;
    inputVal = ToOnlyNumber(inputVal);
    try {
        if (isNaN(inputVal)) {
            outputVal = 0;
        }
        else if (inputVal === undefined) {
            outputVal = 0;
        }
        else if (inputVal == "") {
            outputVal = 0;
        }
        else if (inputVal == "NaN") {
            outputVal = 0;
        }
        else if (inputVal == "undefined") {
            outputVal = 0;
        }
        else if (inputVal == null) {
            outputVal = 0;
        } else {

            outputVal = parseInt(inputVal);
        }
    } catch (e) {
        outputVal = 0;
    }
    return outputVal;
}

function ConvertToDecimal(inputVal) {

    var outputVal = 0;
    try {
        if (isNaN(inputVal)) {
            outputVal = 0;
        }
        else if (inputVal === undefined) {
            outputVal = 0;
        }
        else if (inputVal == "") {
            outputVal = 0;
        }
        else if (inputVal == "NaN") {
            outputVal = 0;
        }
        else if (inputVal == "undefined") {
            outputVal = 0;
        }
        else if (inputVal == null) {
            outputVal = 0;
        } else {

            outputVal = inputVal.replace(/[^0-9\.]/g, '');

            if (outputVal.indexOf('.') > 0)
                outputVal = parseFloat(outputVal).toFixed(2);

        }
    } catch (e) {
        outputVal = 0;
    }
    return outputVal;
}

function ConvertToString(inputVal) {

    var outputVal = "";
    try {
        //if (isNaN(inputVal)) {
        //    outputVal = "";
        //}
        //else
        if (inputVal === undefined) {
            outputVal = "";
        }

        else if (inputVal == "NaN") {
            outputVal = "";
        }
        else if (inputVal == "undefined") {
            outputVal = "";
        }
        else if (inputVal == null) {
            outputVal = "";
        } else {

            outputVal = inputVal;
        }
    } catch (e) {
        outputVal = "";
    }


    return outputVal;
}

String.prototype.TooOnlyNumbers = function () {

    var txt = this.toString();

    var numberPattern = /\d+/g;

    txt = txt.match(numberPattern)

    return txt;

};

function ExtractOnlyNumbers(value) {
    var numberPattern = /\d+/g;
    value = value.match(numberPattern)
    return value;
}

//#region


var dateFormat = function () {
    var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
        timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
        timezoneClip = /[^-+\dA-Z]/g,
        pad = function (val, len) {
            val = String(val);
            len = len || 2;
            while (val.length < len) val = "0" + val;
            return val;
        };

    // Regexes and supporting functions are cached through closure
    return function (date, mask, utc) {
        var dF = dateFormat;

        // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
        if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
            mask = date;
            date = undefined;
        }

        // Passing date through Date applies Date.parse, if necessary
        date = date ? new Date(date) : new Date;
        if (isNaN(date)) throw SyntaxError("invalid date");

        mask = String(dF.masks[mask] || mask || dF.masks["default"]);

        // Allow setting the utc argument via the mask
        if (mask.slice(0, 4) == "UTC:") {
            mask = mask.slice(4);
            utc = true;
        }

        var _ = utc ? "getUTC" : "get",
            d = date[_ + "Date"](),
            D = date[_ + "Day"](),
            m = date[_ + "Month"](),
            y = date[_ + "FullYear"](),
            H = date[_ + "Hours"](),
            M = date[_ + "Minutes"](),
            s = date[_ + "Seconds"](),
            L = date[_ + "Milliseconds"](),
            o = utc ? 0 : date.getTimezoneOffset(),
            flags = {
                d: d,
                dd: pad(d),
                ddd: dF.i18n.dayNames[D],
                dddd: dF.i18n.dayNames[D + 7],
                m: m + 1,
                mm: pad(m + 1),
                mmm: dF.i18n.monthNames[m],
                mmmm: dF.i18n.monthNames[m + 12],
                yy: String(y).slice(2),
                yyyy: y,
                h: H % 12 || 12,
                hh: pad(H % 12 || 12),
                H: H,
                HH: pad(H),
                M: M,
                MM: pad(M),
                s: s,
                ss: pad(s),
                l: pad(L, 3),
                L: pad(L > 99 ? Math.round(L / 10) : L),
                t: H < 12 ? "a" : "p",
                tt: H < 12 ? "am" : "pm",
                T: H < 12 ? "A" : "P",
                TT: H < 12 ? "AM" : "PM",
                Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
                o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
                S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
            };

        return mask.replace(token, function ($0) {
            return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
        });
    };
}();

// Some common format strings
dateFormat.masks = {
    "default": "ddd mmm dd yyyy HH:MM:ss",
    shortDate: "m/d/yy",
    mediumDate: "mmm d, yyyy",
    longDate: "mmmm d, yyyy",
    fullDate: "dddd, mmmm d, yyyy",
    shortTime: "h:MM TT",
    mediumTime: "h:MM:ss TT",
    longTime: "h:MM:ss TT Z",
    isoDate: "yyyy-mm-dd",
    isoTime: "HH:MM:ss",
    isoDateTime: "yyyy-mm-dd'T'HH:MM:ss",
    isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
};

// Internationalization strings
dateFormat.i18n = {
    dayNames: [
        "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
        "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
    ],
    monthNames: [
        "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
        "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
    ]
};

// For convenience...
Date.prototype.format = function (mask, utc) {
    return dateFormat(this, mask, utc);
};


function ToDateFormatDDMMYYYWithSlash(dtStr) {
    var dt = new Date(dtStr);
    var dd = dt.getDate();
    var mm = dt.getMonth() + 1;
    var yyyy = dt.getFullYear();
    return dd + "/" + mm + "/" + yyyy;
}

function ConvertToDate(dtStr, delimetet = "/") {
    try {
        delimetet = ConvertToString(delimetet);
        if (delimetet = "")
            delimetet = "/";
    } catch (e) {
        delimetet = "/";
    }
    var arr = dtStr.split('/');

    var day = ConvertToInt(arr[0]);
    var month = (ConvertToInt(arr[1]) - 1);
    var year = ConvertToInt(arr[2]);

    var dt = new Date(year, month, day);
    return dt
}

function SetDatePicker(ctrl, min = "", max = "", start = "") {

    var minDays = 0, maxDays = 0;
    var _minDate = new Date();
    var _maxDate = new Date();

    if (min != "") {
        minDays = ConvertToInt(min);
        if (min.indexOf("m") > -1)
            minDays = minDays * 30;
        if (min.indexOf("y") > -1)
            minDays = minDays * 365;
    }

    _minDate.setDate(_minDate.getDate() - minDays);

    if (max != "") {
        maxDays = ConvertToInt(max);
        if (max.indexOf("m") > -1)
            maxDays = maxDays * 30;
        if (max.indexOf("y") > -1)
            maxDays = maxDays * 365;
    }

    if (max.indexOf("-") > -1)
        _maxDate.setDate(_maxDate.getDate() - maxDays);
    else
        _maxDate.setDate(_maxDate.getDate() + maxDays);

    $(ctrl).datepicker({
        format: dateFormatDatePicker,
        autoclose: true,
        clearBtn: true,
        disableTouchKeyboard: true,
        todayHighlight: true,
        showOnFocus: true,
        changeYear: true,
        startDate: _minDate,
        endDate: _maxDate
        //startDate: '-90y',
        //endDate: '-20y'
    });

    var _dateStart = new Date();
    var startDate = "";

    if (start == "-2")
        startDate = ToDateFormatDDMMYYYWithSlash(_minDate);
    else if (start == "-1")
        startDate = ToDateFormatDDMMYYYWithSlash(_maxDate);
    else
        startDate = ToDateFormatDDMMYYYWithSlash(_dateStart);

    $(ctrl).val(startDate);

}

function JsonToDate(dateStr) {
    ///Date(
    var dateTicks = parseInt(dateStr.TooOnlyNumbers());
    var dt = new Date(dateTicks);
    return dt;
}

Date.locale = {
    month_names: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
    month_names_short: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
};

function TooDateFormatDDMMYYYHHMMSS(dtStr) {

    var dt = new Date(dtStr);
    var dd = dt.getDate();
    var mm = Date.locale.month_names_short[dt.getMonth()]; // dt.getMonth() + 1; //January is 0!
    var yyyy = dt.getFullYear();

    var seconds = dt.getSeconds();
    var minutes = dt.getMinutes();
    var hour = dt.getHours();

    return dd + " " + mm + " " + yyyy + " " + hour + ":" + minutes + ":" + seconds;
};
function ToDateFormatDDMMYYY(dtStr) {

    var dt = new Date(dtStr);
    var dd = dt.getDate();
    var mm = Date.locale.month_names_short[dt.getMonth()]; // dt.getMonth() + 1; //January is 0!
    var yyyy = dt.getFullYear();

    return dd + " " + mm + " " + yyyy;
}

function ToDateFormatDDMMYYYWithSlash(dtStr) {

    var dt = new Date(dtStr);
    var dd = dt.getDate();
    var mm = dt.getMonth() + 1;
    var yyyy = dt.getFullYear();

    return dd + "/" + mm + "/" + yyyy;
}

//#endregion


function ToMaxLength(value, length) {

    var result = "";
    try {
        var len = ConvertToInt(value.length);
        if (len > length)
            value = value.substring(0, length);
    } catch (e) {

    } finally {
        result = value;
    }
    return result;
}

$(document).on("keydown", ".dated", function (e) {

    var result = this.value;

    try {
        result = $.trim(result);
        result = result.replace(/[^0-9/]/g, '');
    } catch (e) {

    } finally {
        this.value = result;
    }
});

$(document).on("input", ".reqfd", function () {

    var result = this.value;

    try {

        result = $.trim(result);
        if (result.length == 0) {
            $(this).addClass("req-fld");
        }
        else $(this).removeClass("req-fld");

    } catch (e) {

    } finally {

    }

});

$(document).on("change", ".reqfd", function () {

    var result = ConvertToInt($(this).val());

    try {
        result = $.trim(result);
        if (result == 0) {
            $(this).addClass("req-fld");
        }
        else $(this).removeClass("req-fld");

    } catch (e) {

    } finally {

    }

});

function TrimLength(id) {
    var result = $("#" + id).val();
    result = RemoveMultiSpace(result);
    var len = 0
    try {
        len = ConvertToInt($("#" + id).attr("len"));
        if (result.length > len)
            result = result.substring(0, len);
    } catch (e) {
    } finally {
        $("#" + id).val(result);
        var count = ConvertToString(result.length) + "/" + len;
        $("#" + id + "Count").html(count);
    }
}

function TrimLengthCtrl() {
    $(".lenth").each(function (index) {
        try {
            TrimLength($(this).attr("id"));
        } catch (e) {
        }
    });
}

$(document).on("input", ".lenth", function () {
    TrimLength($(this).attr("id"));
});



$(document).on("textarea", ".lenth", function () {
    var result = this.value;
    try {
        result = result;
        var len = ConvertToInt($(this).attr("len"));
        if (result.length > len)
            result = result.substring(0, len);
    } catch (e) {

    } finally {
        this.value = result;

        $("#" + id).val(result);
        var count = ConvertToString(result.length) + "/" + len;
        $("#" + id + "Count").html(count);
    }

});

$(document).on("input", ".alpha", function () {

    var result = this.value;

    try {
        //result = $.trim(result);
        result = result.replace(/[^a-zA-Z\s ]/g, '');
    } catch (e) {

    } finally {
        this.value = result;
    }

});

$(document).on("input", ".numeric", function () {

    var result = this.value.replace(/\D/g, '');

    try {
        result = $.trim(result);
        var len = ConvertToInt($(this).attr("len"));
        if (result.length > len)
            result = result.substring(0, len);
    } catch (e) {

    } finally {
        this.value = result;
    }

});

$(document).on("focusout", ".decimal", function () {

    var result = 0;

    try {
        result = $.trim(result);
        var len = ConvertToInt($(this).attr("len"));
        result = DecimalConverision(this.value, len);

    } catch (e) {

    } finally {
        this.value = result;
    }

});

function ValidateEmail(emailId) {

    var isValid = false;

    try {

        var format = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
        if (format.test(emailId))
            isValid = true;
        else
            isValid = false;

    } catch (e) {
        isValid = false;
    }

    return isValid
}

function ValidateDate(attrId, year = 0) {

    year = ConvertToInt(year);
    if (year == 0) year = 1947;

    var spnAttr = attrId.replace("txt", "spn");

    $("#" + spnAttr).hide();

    var result = true;
    try {

        var value = $("#" + attrId).val();
        var arr = value.split('/');

        var dd = ConvertToInt(arr[0]);
        var mm = ConvertToInt(arr[1]);
        var yy = ConvertToInt(arr[2]);

        if (dd <= 0 && dd > 31)
            result = false;
        if (mm <= 0 && mm > 13)
            result = false;
        if (yy < year)
            result = false;

    } catch (e) {
        result = false;
    }

    if (result == false)
        $("#" + spnAttr).show();

    return result;
}

function DecimalConverision(input, len) {

    var result = 0;
    try {

        result = input.replace(/[^0-9\.]/g, '');

        if (len > 0 && result.length > len)
            result = result.substring(0, len);

        if (result.indexOf('.') > 0)
            result = parseFloat(result).toFixed(2);

    } catch (e) {
        result = 0;
    }

    return result;
}

function GetQuerystringValues(param) {

    var result = "";
    try {

        var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < url.length; i++) {
            var urlparam = url[i].split('=');
            if (urlparam[0] == param) {
                result = urlparam[1];
            }
        }

    } catch (e) {
        result = "";
    }
    if (result == "undefine")
        result = "";

    return result;

}

function DownloadToExcel(ctrl) {
    try {
        var fileName = ConvertToString($(ctrl).attr("fileName"));
        var sheetName = ConvertToString($(ctrl).attr("sheetName"));

        if (sheetName == "")
            sheetName = "Sheet0";

        var tblId = ConvertToString($(ctrl).attr("tblId"));

        let table = document.getElementById(tblId);
        TableToExcel.convert(table, {
            name: fileName,
            sheet: {
                name: sheetName
            }
        });
    } catch (e) {
    }
}

function CheckPmdcNo(id) {
    var resp = {};
    resp.isOk = true;
    resp.respMsg = "";
    try {
        var obj = {};
        obj.RegistrationNo = id;
        obj.Name = "";
        obj.FatherName = "";
        $.ajax({
            type: "POST",
            url: "https://pmc.gov.pk/api/DRC/GetData",
            data: obj,
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            dataType: "json",
            async: false,
            success: function (response) {
                if (response.data.length > 0) {
                    resp = response.data[0];
                    resp.isOk = true;
                    resp.respMsg = "";
                }
                else {
                    resp.isOk = false;
                    resp.respMsg = "Data not exist";
                }
            },
            error: function () {
                resp = {};
                resp.isOk = false;
                resp.respMsg = "Data not exist";
            }
        });
    } catch (e) {
        resp = {};
        resp.isOk = false;
        resp.respMsg = "Some thing went worng";
    }
    return resp;
}

function CheckAndVerifyPMDC(pmdcNo, inductionId, applicantId) {

    var vfcheckId = 30, isActive = false, comments = "";

    var resp = CheckPmdcNo(pmdcNo);

    if (resp.isOk) {
        var d1 = ConvertToDate("19/12/2023");
        var d2 = ConvertToDate(resp.ValidUpto);
        if (d1 > d2) {
            isActive = true;
            vfcheckId = 32;
            comments = resp.Status;
        }
        else {
            vfcheckId = 32;
            isActive = false;
            comments = resp.Status;
        }
    }
    else {
        isActive = true;
        vfcheckId = 30; // PMDC not Valid
        comments = resp.respMsg;
    }

    try {

        var objSp = {};
        objSp.spName = "[dbo].[spVfCheckApplicantAddUpdate]";
        objSp.listParam = [];

        var objParam = {};
        objParam.value = 1;
        objParam.key = "projId";
        objParam.dataType = "int";
        objSp.listParam.push(objParam);

        objParam = {};
        objParam.value = inductionId;
        objParam.key = "inductionId";
        objParam.dataType = "int";
        objSp.listParam.push(objParam);

        objParam = {};
        objParam.value = applicantId;
        objParam.key = "applicantId";
        objParam.dataType = "int";
        objSp.listParam.push(objParam);

        objParam = {};
        objParam.value = vfcheckId;
        objParam.key = "vfcheckId";
        objParam.dataType = "int";
        objSp.listParam.push(objParam);

        objParam = {};
        objParam.value = isActive;
        objParam.key = "isActive";
        objParam.dataType = "bool";
        objSp.listParam.push(objParam);

        objParam = {};
        objParam.value = true;
        objParam.key = "isSelect";
        objParam.dataType = "bool";
        objSp.listParam.push(objParam);

        objParam = {};
        objParam.value = comments;
        objParam.key = "comments";
        objParam.dataType = "string";
        objSp.listParam.push(objParam);

        var respSp = CallActionPost("/Common/CallSpGenericToMessage", objSp);

    } catch (e) {
    }

    return resp;

}

function UploadImage(uploaderId, applicantId, imageType) {
    var imageName = "";
    try {
        // Checking whether FormData is available in browser
        if (window.FormData !== undefined) {

            var fileUpload = $("#" + uploaderId).get(0);
            var files = fileUpload.files;

            // Create FormData object
            var fileData = new FormData();

            // Looping over all files and add it to FormData object
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            // Adding one more key to FormData object
            fileData.append('applicantId', applicantId);
            fileData.append('imageType', imageType);
            fileData.append('inductionId', 9);

            $.ajax({
                url: '/Common/UploadImage',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                async: false,
                data: fileData,
                success: function (result) {

                    imageName = "";
                    if (result.id > 0) {
                        imageName = result.msg;
                    }
                    else {
                        alert(result.msg);
                    }
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        } else {

            imageName = "";
            alert("FormData is not supported.");

        }
    } catch (e) {
        imageName = "";
    }
    return imageName;
}
function UploadImageAdmin(uploaderId, applicantId, imageType, inductionId, folderPath) {
    var imageName = "";
    try {
        // Checking whether FormData is available in browser
        if (window.FormData !== undefined) {

            var fileUpload = $("#" + uploaderId).get(0);
            var files = fileUpload.files;

            // Create FormData object
            var fileData = new FormData();

            // Looping over all files and add it to FormData object
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            // Adding one more key to FormData object
            fileData.append('applicantId', applicantId);
            fileData.append('imageType', imageType);
            fileData.append('folderPath', folderPath);
            fileData.append('inductionId', inductionId);

            $.ajax({
                url: '/CommonFunctionsAdmin/UploadImage',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                async: false,
                data: fileData,
                success: function (result) {

                    imageName = "";
                    if (result.id > 0) {
                        imageName = result.msg;
                    }
                    else {
                        alert(result.msg);
                    }
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        } else {

            imageName = "";
            alert("FormData is not supported.");

        }
    } catch (e) {
        imageName = "";
    }

    return imageName;
}


//#region Validation

function ValidateControl(Control) {
    var isValid = true;
    try {
        if (Control[0].localName.toLowerCase() == "input") {
            var current = ConvertToString(Control.val());
            if (current.length == 0)
                isValid = false;
        }
        else if (Control[0].localName.toLowerCase() == "select") {
            var current = ConvertToInt(Control.val());
            if (current == 0 || current == -1) {
                isValid = false;
            }
        }
        if (isValid == false) {
            Control.addClass("req-fld");
        }
        else Control.removeClass("req-fld");
    } catch (e) {
    } finally {
    }
    return isValid;
}

function ValidateCheck(formId) {
    var SubmitForm = true;
    //#region Required Feild Check
    $("#" + formId).find(".required").each(function (event) {
        var Control = $(this);
        var result = ValidateControl(Control);
        if (SubmitForm == true)
            SubmitForm = result;
    });
    //#endregion
    return SubmitForm;
}

//#endregion

