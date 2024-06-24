var employeeSearch = {
    defaults: {
        defaultLang: 'en-US',
        userId: '',
        locked: 'Locked',
        notLoccked: 'Not Locked',
        gsdUserId: '',
        empId: '',
        employeeMobNum: '',
        userPrivilegeReset: '',
        userPrivilegeUnlock: '',
        startDt: '',
        endDt: '',
        mode: '',
        eventdate: '',
        currentDt: null,
        newMonth: null,
        prevsYr: null,
        enabledEventSource: ['CCH1WPADFS03', 'CCH1WPTMG2']
    },
    init: function () {

        employeeSearch.defaults.currentDt = new Date();
        employeeSearch.defaults.newMonth = employeeSearch.defaults.currentDt.getMonth() - 2;
        if (employeeSearch.defaults.newMonth < 0) {
            employeeSearch.defaults.newMonth += 12;
            employeeSearch.defaults.currentDt.setYear(employeeSearch.defaults.currentDt.getFullYear() - 1);
        }
        employeeSearch.defaults.prevsYr = new Date(employeeSearch.defaults.currentDt.setMonth(employeeSearch.defaults.newMonth));

        var startDt = new Date();
        var endDt = new Date(startDt);
        var endDtSelect = new Date(startDt);
        endDt.setHours(startDt.getHours() - 24);
        endDtSelect.setHours(endDt.getHours());
      
        $("#startDate").datetimepicker({
            value: employeeSearch.defaults.startDt == "" ? new Date(endDt) : employeeSearch.defaults.startDt,
            minDate: employeeSearch.defaults.prevsYr.getFullYear() + "/" + employeeSearch.defaults.prevsYr.getMonth() + "/" + employeeSearch.defaults.prevsYr.getDate(),
            maxDate: new Date(),
            //timepicker: false,
            //format:'d/m/Y',
            onSelectTime: function (dp, $input) {
                var result =employeeSearch.datePickerSelection($input);
                if (result == "false") { $("#startDate").val(""); }
            }
        });
        $("#endDate").datetimepicker({
            value: employeeSearch.defaults.endDt == "" ? new Date(endDtSelect) : employeeSearch.defaults.endDt,
            minDate: employeeSearch.defaults.prevsYr.getFullYear() + "/" + employeeSearch.defaults.prevsYr.getMonth() + "/" + employeeSearch.defaults.prevsYr.getDate(),
            maxDate: new Date(),
            //timepicker: false,
            //format: 'd/m/Y',
            onSelectTime: function (dp, $input) {
                var result = employeeSearch.datePickerSelection($input);
                if (result == "false") { $("#endDate").val(""); }
            }

        });
        $.validator.addMethod("regex", function (value, element, regexp) {
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        }, "Please check your input.");
        var validate = $("#employeeSearch-form").validate({
            debug: true,
            onkeyup: false,
            onsubmit: false,
            onblur: true,
            rules: {
                txtUserId: {
                    required: true,
                    regex: "^[a-zA-Z0-9@_-]*$",
                },
                startDate: {
                    required: true,
                },
                endDate: {
                    required: true,
                }
            },
            messages: {
                txtUserId: {
                    required: "Please enter Employee Id",
                    regex: "Invalid Employee Id"
                },
                startDate: {
                    required: "Please enter Start Date",
                },
                endDate: {
                    required: "Please enter End Date",
                }
            },
            onfocusout: false,
            invalidHandler: function (form, validator) {
                var errors = validator.numberOfInvalids();
                if (errors) {
                    validator.errorList[0].element.focus();
                }
            }
        });

        if (employeeSearch.defaults.empId != null && employeeSearch.defaults.empId != "") {
            $("#txtUserId").val(employeeSearch.defaults.empId);
            $("#btnCheck").trigger('click');
        }
        $("#aShowOu").on('click', function () { employeeSearch.showOUPopUp(); });
        $("#aShowBitlocker").on('click', function () { employeeSearch.showBitlockerPopUp(); });

        $('#txtComputerName').on('keypress', function (e) {
            var code = e.keyCode || e.which;
            if (code == 13) {
                employeeSearch.bindBitlockerInfo();
            }
        });

        $('#txtUserId').keypress(
            function () {
                employeeSearch.btnCancelClick();
            });

        layout1.defaults.defaultLang = employeeSearch.defaults.defaultLang;
        layout1.init();
    },

    //Get the Employee information 
    btnCheck: function () {

        var isValid = $("#employeeSearch-form").valid();
        if (!isValid) {
            return;
            e.preventDefault();
        }

        var userId = $("#txtUserId").val();
        var startDate = $("#startDate").val();//.split('/');
        var endDate = $("#endDate").val();//.split('/');
        //if ($.trim(userId) == "") {
        //    alert('Please enter Employee Id');
        //    $("#txtUserId").focus();
        //    return false;
        //}
        if ($.trim(startDate) == "") {
            alert("Start Date should not be empty");
            $("#startDate").focus();
            return false;
        }
        else {
            var result = employeeSearch.datePickerSelection($("#startDate"));
            if (result == "false") { $("#startDate").val(""); return false; }
        }
        if ($.trim(endDate) == "") {
            alert("End Date should not be empty");
            $("#endDate").focus();
            return false;
        }
        else {
            var result = employeeSearch. datePickerSelection($("#endDate"));
            if (result == "false") { $("#endDate").val(""); return false; }
        }


        $('.clsNA').text('- NA -');
        var currentYear = (new Date).getFullYear().toString();
        var startDtYr = startDate.substring(0, 4);
        var endDateYr = endDate.substring(0, 4);

        //if (endDateYr != currentYear || startDtYr != currentYear) {
        //    alert("Start and End date should be in current year");
        //    return false;
        //}
        if (new Date(startDate) > new Date(endDate)) {
            alert("End date should be greater than Start date");
            $("#endDate").focus();
            return false;
        }
        //$(".pre-loader").show();

        employeeSearch.defaults.userId = userId;

        $("#dvUserInfo").show();
        $("#dvUserInfo").loading();
        $("#dvChartBox").show();
        $("#dvChartBox").loading();
        $("#dvuserActivityContainer").show();
        $("#dvuserActivityContainer").loading();

        // To bind the basic User Info
        employeeSearch.getUserInfo(function (response) {
            //console.log(response);   
            if (response.IsInValid == false) {
                $("#spnInvalidUser").hide();
                // $("#dvUserInfo").show();
                //$("#dvUserInfo").loading();
                // $("#dvChartBox").show();
                //$("#dvChartBox").loading();
                //$("#dvuserActivityContainer").show();
                //$("#dvuserActivityContainer").loading();
                employeeSearch.bindUserInfo(response);
                $("#dvUserInfo").loading('stop');
                //Bind Chart
                employeeSearch.getUserEventsByDate(function (response) {
                    // $("#chart").html('');
                    if (response.length > 0) {
                        $("#pChartHeader").show();
                        $("#spnChartNoData").hide();
                        $("#chart").show();
                        $("#jsGrid").show();
                        employeeSearch.bindChart(response);
                    }
                    else {
                        $("#pChartHeader").hide();
                        $("#spnChartNoData").show();
                        $("#chart").hide();
                        $("#jsGrid").hide();

                        employeeSearch.bindChart(response);
                    }
                }, function (error) { });

                employeeSearch.getUserEvents(function (response) {
                    employeeSearch.bindGrid(response);
                    $("#dvChartBox").loading('stop');
                    //$(".pre-loader").hide();
                }, function (error) { $("#dvChartBox").loading('stop'); });

                employeeSearch.getUserActivity();
            }
            else {
                $("#spnInvalidUser").show();
                $("#dvUserInfo").hide();
                $("#dvChartBox").hide();
                $("#dvuserActivityContainer").hide();

                $("#dvUserInfo").loading('stop');
                $("#dvChartBox").loading('stop');
                $("#dvuserActivityContainer").loading('stop');
            }
        }, function (error) { $("#dvUserInfo").loading('stop'); });

        // To bind the User Question and Answer
        //employeeSearch.getUserQuestionAndAnswer(function (response) {
        //    //console.log(response);
        //    employeeSearch.bindUserQuestionAndAnswer(response);

        //}, function (error) { });     
    },
    //To get employee userinfo
    getUserInfo: function (success, failure) {
        var context = ['GetUserInfo', employeeSearch.defaults.userId];
        CoreREST.get(context, null, success, failure);
    },
    //Bind the User Information
    bindUserInfo: function (data) {
        if (data != null) {

            // if the user not opted for OTP then we disable the link , for consecutive user search we need to remove
            if ($("#pwdReset").hasClass("cls-disabled")) {
                $("#pwdReset").removeClass("cls-disabled")
            }

            // Binding User Account Status
            if (data.UserAccountStatus) {
                $("#spnStatus").addClass('red');
                $("#spnStatus").html(employeeSearch.defaults.locked)
                $("#accUnlock").removeClass("cls-disabled");
            }
            else {
                $("#spnStatus").addClass('green');
                $("#spnStatus").html(employeeSearch.defaults.notLoccked);
                $("#accUnlock").addClass("cls-disabled");
            }

            employeeSearch.defaults.employeeMobNum = data.MobileNumber;

 
            if (data.IsRegistered == "No")
                $("#div-EnrollLink").show();
            else
                $("#div-EnrollLink").hide();

            // $("#div-EnrollLink").show();
            $("#empName").html(data.UserName + " " + "<span id='empId'></span>");
            $("#empId").html("&#40;" + data.UserId + "&#41;");
            $("#empDOB").html(employeeSearch.dateTimeFormat(data.UserDOB))
            $("#spnIsPassExpired").html(data.PasswordExpired);

            if (data.PasswordExpired == "Yes" || data.PasswordAge == "-NA-") {
                $("#spnIsPassExpired").css("color", "red");
                $("#empPassAge").html(data.PasswordAge);
            }
            else if (data.PasswordExpired == "No") {
                $("#spnIsPassExpired").css("color", "green");
                $("#empPassAge").html(data.PasswordAge + " day(s)");
            }


            //$("#empOU").html(data.OUName);

            var employeeMobNum = ""

            if (data.MobileNumber != null && data.MobileNumber != "") {
                if (data.IsOTPEnabled) {
                    employeeMobNum = employeeSearch.maskMobileNumber(data.MobileNumber);
                }
                else {
                    // $("#pwdReset").addClass("cls-disabled");
                    employeeMobNum = "not registered for OTP";
                }
            }
            else {
                employeeMobNum = "- NA -";
                $("#pwdReset").addClass("cls-disabled")
            }

            $("#empMobileNum").html(employeeMobNum);

            $("#spnIsRegistered").html(data.IsRegistered);

            //cls-disabled
        }
    },
    getUserQuestionAndAnswer: function (success, failure) {
        var context = ['GetUserQuestionAndAnswer', employeeSearch.defaults.userId];
        CoreREST.get(context, null, success, failure);
    },
    bindUserQuestionAndAnswer: function (data) {
        var content = "";

        for (var i = 0; i < data.length; i++) {
            content += '<div class="col-sm-12 col-md-12 col-lg-12 Padding-Nil">';
            content += '<div class="col-sm-6 col-md-3 col-lg-3 Padding-Nil">';
            content += '<label>security q' + (i + 1) + ':</label>';
            content += '</div>';
            content += '<div class="col-sm-6 col-md-9 col-lg-9 Padding-Nil">';
            content += '<span id="securityQ_' + i + '">' + data[i].Question + '</span>';
            content += '<span class="security-ans" id="securityA_' + i + '">' + data[i].Answer + '</span>';
            content += '</div>';
            content += '</div>';
        }

        $("#dvQuestionAnswer").html(content);
    },
    getUserEventsByDate: function (success, failure) {
        var stDateArr = $("#startDate").val();//.split('/');
        var endDateArr = $("#endDate").val();//.split('/');      

        //var stDate = stDateArr[2] + "-" + stDateArr[0] + "-" + stDateArr[1];
        //var endDate = endDateArr[2] + "-" + endDateArr[0] + "-" + endDateArr[1];

        var arr = {};
        arr.UserId = employeeSearch.defaults.userId;
        arr.StartDate = stDateArr;
        arr.EndDate = endDateArr;
        arr.Mode = employeeSearch.defaults.mode;;

        var context = ['GetUserEventsModelDateWise'];
        CoreREST.postArray(context, arr, function (data) {
            success(data);
        }, function (e) { failure(e) });
    },
    getUserEvents: function (success, failure) {
        var stDateArr = $("#startDate").val();//.split('/');
        var endDateArr = $("#endDate").val();//.split('/');     
        //var stDate = stDateArr[2] + "-" + stDateArr[0] + "-" + stDateArr[1];
        //var endDate = endDateArr[2] + "-" + endDateArr[0] + "-" + endDateArr[1];

        var arr = {};
        arr.UserId = employeeSearch.defaults.userId;
        arr.StartDate = stDateArr;
        arr.Mode = employeeSearch.defaults.mode;
        arr.EndDate = endDateArr;

        var context = ['GetUserEventsDetails'];
        CoreREST.postArray(context, arr, function (data) {
            success(data);
        }, function (e) { failure(e) });
    },
    bindGrid: function (data) {
        var dataAr = new Array();

        for (var i = 0; i < data.length; i++) {
            var value = {
                "No": i + 1,
                "Locked out date": data[i].EventDate,
                "Locked Device": data[i].EventSource,
                "Locked out Trace": data[i].Eventdt,
                "LockedData": data[i].EventSource
            }
            dataAr.push(value);

        }

        $("#jsGrid").jsGrid({
            width: "100%",
            height: "auto",

            inserting: false,
            editing: false,
            sorting: true,
            paging: true,
            autoload: true,

            pageSize: 5,
            pageButtonCount: 5,

            data: dataAr,
            fields: [
                { name: "No", type: "text", width: 10 },
                { name: "Locked out date", type: "text", width: 30 },
                { name: "Locked Device", type: "text", width: 30 },
                {
                    name: "Locked out Trace", type: "text", width: 20, itemTemplate: function (cellvalue, options, rowObject) {
                        if ($.inArray(options.LockedData, employeeSearch.defaults.enabledEventSource) != -1) {
                            var cellval = cellvalue + '/' + options.LockedData;
                            return '<a href="JavaScript:void(0);" class="actionButton" onclick="employeeSearch.showUserAgentInfo(this)" data-value="' + cellval + '">more...</a>';
                        }
                        else {
                            return 'NA';
                        }

                    }, filtering: true
                }
            ]
        });

        //$(".actionButton").bind('click', function () {

        //})
    },
    showIPInfo: function (obj) {
        var clientIP;
        //arr.UserId = employeeSearch.defaults.userId;
        clientIP = $(obj).attr('data-value');
        window.open("https://db-ip.com/" + clientIP + "", '_blank');
    },
    showUserAgentInfo: function (obj) {
        $("#dvUserAgentInfo").html('');
        $("#dvUserAgentInfo").html('Loading...');
        var arr = {};
        arr.UserId = employeeSearch.defaults.userId;
        arr.Eventinput = $(obj).attr('data-value');
        var BrowserName = '';
        var OsName = '';
        var Eventdate = '';
        var Devicename = '';

        var context = ['GetUserAgentDetails'];
        CoreREST.postArray(context, arr, function (data) {

            if (data.length > 0) {
                var content = '<table class="table table-striped" id="tblGroup">';
                content += '<thead>';
                content += '<tr>';
                content += '<th style="width:30px">#</th>';
                content += '<th style="width:100px">Date time</th>';
                content += '<th style="width:80px">Device</th>';
                content += '<th style="width:80px">OS</th>';
                content += '<th style="width:80px">Browser</th>';
                content += '<th style="width:80px">ClientIP</th>';
                content += '<th style="width:80px">User agent</th>';
                content += '</tr>';
                content += '</thead>';
                content += '<tbody>';

                for (var i = 0; i < data.length; i++) {

                    if (data[i].substrings.length > 1) {
                        BrowserName = data[i].substrings[1];
                    }

                    if (data[i].substrings.length > 1) {
                        Devicename = data[i].device[2];
                        //alert(Devicename);
                        if (data[i].device != undefined && data[i].device[0] == 'iPhone')
                            Devicename = "iphone";
                        else if (Devicename != undefined)
                            Devicename = data[i].device[2];
                        else
                            Devicename = data[i].deviceinfo;
                    }
                    if (data[i].device.length > 0) {
                        if (data[i].device[0].indexOf('Windows') >= 0) {
                            OsName = "Windows";
                        }

                        else {
                            OsName = (data[i].device[1] != undefined) ? data[i].device[1] : "";
                        }
                    }
                    Devicename = Devicename != "" ? Devicename : "NA";
                    OsName = OsName != "" ? OsName : "NA";
                    BrowserName = BrowserName != "" ? BrowserName : "NA"
                    var useragent = (data[i].UserAgent != undefined && data[i].UserAgent != "") ? data[i].UserAgent : "NA";

                    Eventdate = data[i].EventDate;
                    var str = data[i].ClientIP != undefined ? data[i].ClientIP : "";
                    var res = str != "" ? str.split(",") : "";
                    content += '<tr>';
                    content += '<th scope="row">' + (i + 1) + '</th>';
                    content += '<td>' + Eventdate + '</td>';
                    content += '<td>' + Devicename + '</td>';
                    content += '<td>' + OsName + '</td>';
                    content += '<td>' + BrowserName + '</td>';
                    content += '<td>'

                    if (res.length > 0) {
                        for (var z = 0; z < res.length; z++) {
                            content += '<a href="JavaScript:void(0);" onclick="employeeSearch.showIPInfo(this)" data-value=' + res[z] + ' class="actionButton" >' + res[z] + '  ' + '</a>';

                        }
                    }
                    else {
                        content += 'NA';
                    }

                    + '</td>';

                    content += '<td>' + useragent + '</td>';
                    content += '</tr>';
                }
            }
            else {
                content += '<tr>';
                content += '<th ></th>';
                content += '<td colspan="3" align="center" >' + "No data found." + '</td>';

                content += '</tr>';
            }


            content += '</tbody>';
            content += '</table>';

            $("#dvUserAgentInfo").html(content);

            return true;

        }, function () { return false });

        $("#modal-lockinfo").modal({
            show: true, backdrop: 'static',
            keyboard: false,
            backdrop: 'static',
            keyboard: false
        });
    },
    showAccountlockerPopUp: function () {

        $("#modal-lockinfo").modal({
            show: true, backdrop: 'static',
            keyboard: false,
            backdrop: 'static',
            keyboard: false
        });
    },
    bindChart: function (data) {
        var arDate = new Array();
        var arValue = new Array();
        var totalLockCnt = 0;

        for (var i = 0; i < data.length; i++) {
            var date = new Date(data[i].Date);
            var dateformat = date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();

            arDate.push(dateformat);

            arValue.push(data[i].Attempts);
            totalLockCnt = totalLockCnt + parseInt(data[i].Attempts);
        }
        $("#totalLock").html(totalLockCnt);
        var chart = document.getElementById('chart'),
            myChart = echarts.init(chart);

        var option = {
            //title: {
            //    text: $("#txtUserId").val() + "'s, locked out count",
            //},
            tooltip: {
                trigger: 'item',
            },
            //legend: {
            //    data: ['User lockedout count']
            //},
            noDataLoadingOption: {
                text: 'No Data found for this',
                x: 'center',
                y: 'center',
                textStyle: {
                    fontSize: 12,
                    fontStyle: 'normal',
                    fontWeight: 'normal',
                },
                effect: 'bubble',
                effectOption: null,
                progress: null,
            },
            width: 100,
            toolbox: {
                show: true,
                feature: {
                    mark: { title: 'mark', show: false },
                    dataView: { title: 'dataView', show: false, readOnly: false },
                    magicType: { title: 'magicType', show: false, type: ['line', 'bar'] },
                    restore: { title: 'restore', show: true },
                    saveAsImage: { title: 'Save as Image', show: true, lang: ['Save'] }
                }
            },
            calculable: true,
            xAxis: [
                {
                    type: 'category',
                    data: arDate,
                    axisLabel: {
                        show: true,
                        interval: 0,
                        rotate: 20,
                    },
                },
            ],
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: [
                {
                    name: 'User locked out count',
                    type: 'bar',
                    data: arValue,
                    itemStyle: {
                        normal: {
                            color: 'tomato',
                            barBorderColor: 'tomato',
                            label: {
                                show: true, position: 'top'
                            },
                        }
                    },
                    barWidth: 30,
                }
            ]
        };
        myChart.clear();
        // Load data into the ECharts instance
        myChart.setOption(option);

        myChart.refresh();
    },
    dateTimeFormat: function (dob) {
        if (dob != null && dob != "") {
            var m_names = new Array("Jan", "Feb", "Mar",
                "Apr", "May", "Jun", "Jul", "Aug", "Sep",
                "Oct", "Nov", "Dec");

            var d = new Date(dob);
            var curr_date = d.getDate();
            var curr_month = d.getMonth();
            var curr_year = d.getFullYear();
            return curr_date + "/" + (curr_month + 1) + "/" + curr_year;
        }
        else
            return dob;
    },
    btnResetClick: function () {
        var r = confirm("Do you really want to reset the password?");
        if (r == true) {
            $("#presetmessage").html("");          
            $("#dvbtn1").show();
            $("#dvbtn2").hide();

            $("#modal-reset").modal({
                show: true, backdrop: 'static',
                keyboard: false,
                backdrop: 'static',
                keyboard: false
            });
        } else {
            //txt = "You pressed Cancel!";
            return false;
        }

        //$("#passwordResetConf").html("Reseting user password, please wait...");
        //$("#modal-confirm").modal({
        //    show: true, backdrop: 'static',
        //    keyboard: false,
        //    backdrop: 'static',
        //    keyboard: false
        //});

        //var userId = $("#txtUserId").val();
        //employeeSearch.defaults.userId = userId;

        //employeeSearch.resetandSendPassword(function (response) {
        //    if (response)
        //        $("#passwordResetConf").html('Successfully reset password and OTP has been sent to registered mobile number "' + employeeSearch.maskMobileNumber(employeeSearch.defaults.employeeMobNum) + '"')
        //    else
        //        $("#passwordResetConf").html('Error while send the message, please try after some time')

        //}, function (error) { });
    },
    btnCangePasswordClick: function () {
        $("#dvbtn1").hide();
        $("#dvbtn2").hide();

        $("#presetmessage").html("Reseting user password, please wait...");

        var userId = $("#txtUserId").val();
        employeeSearch.defaults.userId = userId;

        employeeSearch.resetandSendPassword(function (response) {
            if (response.IsSuccess) {
                if (response.Mode == "C") {
                    //if (employeeSearch.defaults.employeeMobNum != null && employeeSearch.defaults.employeeMobNum != "") {
                    //    $("#presetmessage").html('Successfully reset password to "' + response.Otp + '", and OTP has been sent to registered mobile number "' + employeeSearch.maskMobileNumber(employeeSearch.defaults.employeeMobNum) + '"')
                    //}
                    //else {
                    $("#presetmessage").html('Successfully reset password to "' + response.Otp + '"');
                    //}
                }
                else
                    $("#presetmessage").html('Successfully reset password and OTP has been sent to registered mobile number "' + employeeSearch.maskMobileNumber(employeeSearch.defaults.employeeMobNum) + '"')
            }
            else
                $("#presetmessage").html('Error while send the message, please try after some time')

            $("#dvbtn2").show();

        }, function (error) {
            $("#dvbtn1").show();
            $("#dvbtn2").hide();
        });
    },
    btnUnlockClick: function () {
        var r = confirm("Do you really want to unlock the user account?");
        if (r == true) {

            if ($("#spnStatus").html() == employeeSearch.defaults.notLoccked) {
                $("#fail-msg").html("User Account is not locked!!");
                $("#modal-fail").modal({
                    show: true, backdrop: 'static',
                    keyboard: false,
                    backdrop: 'static',
                    keyboard: false
                });

                return false;
            }

            var userId = $("#txtUserId").val();
            employeeSearch.defaults.userId = userId;

            $("#passwordResetConf").html("Unlocking User Account, please wait...");
            $("#modal-confirm").modal({
                show: true, backdrop: 'static',
                keyboard: false,
                backdrop: 'static',
                keyboard: false
            });

            employeeSearch.unlockAccount(function (response) {
                if (response) {
                    $("#passwordResetConf").html('User Account has been unlocked successfully.');

                    // To bind the basic User Info
                    employeeSearch.getUserInfo(function (response) {
                        employeeSearch.bindUserInfo(response);
                    }, function (error) { });
                }
                else
                    $("#passwordResetConf").html('Error while unlock user account, please try after sometime.');

            }, function (error) { });
        } else {
            return false;
        }
    },
    //Reset and send the password
    resetandSendPassword: function (success, failure) {
        var mode = "D";
       
        var context = ['ResetAndSendPassword', employeeSearch.defaults.userId, employeeSearch.defaults.gsdUserId, mode];
        CoreREST.get(context, null, success, failure);
    },
    unlockAccount: function (success, failure) {
        var context = ['UnlockAccount', employeeSearch.defaults.userId, employeeSearch.defaults.gsdUserId];
        CoreREST.get(context, null, success, failure);
    },
    btnCancelClick: function () {
        $('#datePick').slideUp();
        $("#dvUserInfo").hide();
        $("#dvChartBox").hide();
        $("#dvuserActivityContainer").hide();
    },
    btnenroll: function () {
        var userId = $("#txtUserId").val();

        var hky = $("#hdky").val();
        var key = CryptoJS.enc.Utf8.parse(hky);
        var iv = CryptoJS.enc.Utf8.parse(hky);
        var encryptedValue = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(userId), key,
            {
                keySize: 128 / 8,
                iv: iv,
                mode: CryptoJS.mode.CBC,
                padding: CryptoJS.pad.Pkcs7
            });
        // var url = '../Reports/RegisterUserInfo/'"+userId+"'';
        //  var url ='../Reports/RegisterUserInfo?userId=" + encodeURIComponent(encryptedValue) + "").text(value)';
        //  $(location).attr('href', url);       
        $(location).attr("href", "../Reports/RegisterUserInfo?value=" + encodeURIComponent(encryptedValue) + "");

    },
    downloadAsCsv: function () {
        $("#UserId").val(employeeSearch.defaults.userId);
        $("#StartDate").val($("#startDate").val());
        $("#EndDate").val($("#endDate").val());

        $("#csvform").submit();
        //var stDateArr = $("#startDate").val();//.split('/');
        //var endDateArr = $("#endDate").val();//.split('/');

        ////var stDate = stDateArr[2] + "-" + stDateArr[0] + "-" + stDateArr[1];
        ////var endDate = endDateArr[2] + "-" + endDateArr[0] + "-" + endDateArr[1];

        //var arr = {};
        //arr.UserId = employeeSearch.defaults.userId;
        //arr.StartDate = stDateArr;
        //arr.EndDate = endDateArr;

        //var context = ['Reports', 'DownloadCsv'];
        //CoreREST.postArray(context, arr, function (data) {
        //    success(data);
        //}, function (e) { failure(e) });
    },
    maskMobileNumber: function (mobileNumber) {
        if (mobileNumber != null && mobileNumber != "") {
            var str = mobileNumber;
            var trailingCharsIntactCount = 4;
            str = new Array(str.length - trailingCharsIntactCount + 1).join('*') + str.slice(-trailingCharsIntactCount);
            return str;
        }
        else
            return mobileNumber;
    },
    showOUPopUp: function () {
        $("#modal-ou").modal({
            show: true, backdrop: 'static',
            keyboard: false,
            backdrop: 'static',
            keyboard: false
        });

        employeeSearch.bindUserGroupInfo(function (response) { }, function (errresponse) { });
        employeeSearch.bindUserOUInfo(function (response) { }, function (errresponse) { });
    },
    bindUserGroupInfo: function (success, failure) {
        var context = ['GetUserGroupInfo', employeeSearch.defaults.userId];
        CoreREST.get(context, null, function (data) {
            if (data == null) {
                $("#dvUserGroupInfo").html('- Not able to get user Group information, please try after sometime. -');
                return false;
            }
            // console.log(data);
            var content = '<table class="table table-striped" id="tblGroup">';

            content += '<thead>';
            content += '<tr>';
            content += '<th>#</th>';
            content += '<th>Name</th>';
            content += '<th>Distinguish Name</th>';
            content += '</tr>';
            content += '</thead>';
            content += '<tbody>';
            for (var i = 0; i < data.length; i++) {
                content += '<tr>';
                content += '<th scope="row">' + (i + 1) + '</th>';
                content += '<td>' + data[i].m_Item1 + '</td>';
                content += '<td>' + data[i].m_Item2 + '</td>';
                content += '</tr>';
            }


            content += '</tbody>';
            content += '</table>';

            $("#dvUserGroupInfo").html(content);

            return true;

        }, function () { return false });
    },
    //Display the GetUserOUInfo by PopUp
    bindUserOUInfo: function (success, failure) {
        var context = ['GetUserOUInfo', employeeSearch.defaults.userId];
        CoreREST.get(context, null, function (data) {
            $("#dvUserOUName").html(data);
            return true;
        }, function () { return false; });
    },
    showBitlockerPopUp: function () {
        $("#modal-bitlocker").modal({
            show: true, backdrop: 'static',
            keyboard: false,
            backdrop: 'static',
            keyboard: false
        });
    },
    bindBitlockerInfo: function () {
        var computerName = $("#txtComputerName").val();
        if ($.trim(computerName) == "") {
            alert('Please enter computer name');
            $("#txtComputerName").focus();
            return false;
        }
        $("#dvBitLockerInfo").html("<div class='dvbitlockloadinfo'>loading bit locker info, please wait...<div>");

        var context = ['GetBitLockerInfo', employeeSearch.defaults.userId, computerName];
        CoreREST.get(context, null, function (data) {
            if (data == null || data.length == 0) {
                $("#dvBitLockerInfo").html("<div class='dvbitlockloadinfo'>No Bit locker info found<div>");
                return false;
            }

            var content = '<table class="table table-striped" id="tblBitlocker">';

            content += '<thead>';
            content += '<tr>';
            content += '<th>#</th>';
            content += '<th>Password Id</th>';
            content += '<th>Recovery Password</th>';
            content += '</tr>';
            content += '</thead>';
            content += '<tbody>';
            for (var i = 0; i < data.length; i++) {
                content += '<tr>';
                content += '<th scope="row">' + (i + 1) + '</th>';
                content += '<td>' + data[i].m_Item1 + '</td>';
                content += '<td>' + data[i].m_Item2 + '</td>';
                content += '</tr>';
            }


            content += '</tbody>';
            content += '</table>';

            $("#dvBitLockerInfo").html(content);

        }, function () { $("#dvBitLockerInfo").html("<div class='dvbitlockloadinfo'>Not able to load bit locker info, please try after somtime!!!<div>"); });
    },
    getUserActivity: function () {
        var stDateArr = $("#startDate").val();//.split('/');
        var endDateArr = $("#endDate").val();//.split('/');

        var arr = {};
        arr.UserId = employeeSearch.defaults.userId;
        arr.StartDate = stDateArr;
        arr.EndDate = endDateArr;

        var context = ['GetUserActivity'];
        CoreREST.postArray(context, arr, function (data) {
            employeeSearch.bindUserActivityGrid(data);
        }, function (e) { $("#dvuserActivityContainer").loading('stop'); });
    },
    bindUserActivityGrid: function (data) {
        data = JSON.parse(data);

        if (data.Table.length == 0) {
            $("#dvuserActivityContainer").loading('stop');
            $("#dvUserActivity").html('- No data found -');
            return false;
        }

        var dataAr = new Array();
        for (var i = 0; i < data.Table.length; i++) {
            var value = {
                "No": i + 1,
                "User Activity": data.Table[i].useractivity,
                "Status": data.Table[i].status,
                "Datetime": data.Table[i].startdatetime
            }
            dataAr.push(value);

        }

        $("#dvUserActivity").jsGrid({
            width: "100%",
            height: "auto",

            inserting: false,
            editing: false,
            sorting: true,
            paging: true,
            autoload: true,

            pageSize: 5,
            pageButtonCount: 5,

            data: dataAr,

            fields: [
                { name: "No", type: "text", width: 10 },
                { name: "User Activity", type: "text", width: 30 },
                { name: "Status", type: "text", width: 30 },
                { name: "Datetime", type: "text", width: 30 }
            ]
        });

        $("#dvuserActivityContainer").loading('stop');
    },
    datePickerSelection: function ($input) {
        var startDt = new Date();
        var endDt = new Date(startDt);

        var minDt = employeeSearch.defaults.prevsYr.getFullYear() + "/" + (employeeSearch.defaults.prevsYr.getMonth()) + "/" + employeeSearch.defaults.prevsYr.getDate();
        var currentDt = new Date();
        var maxDt = currentDt.getFullYear() + "/" + (currentDt.getMonth() + 1) + "/" + currentDt.getDate();

        var inputVal = $input.val();
        var a = new Date(minDt);
        var b = new Date(maxDt);

        var c = new Date(inputVal);
        var selectDt = c.getFullYear() + "/" + (c.getMonth() + 1) + "/" + c.getDate();
        c = new Date(selectDt);
        if (selectDt != (currentDt.getFullYear() + "/" + (currentDt.getMonth() + 1) + "/" + currentDt.getDate())) {

            if (!(c <= b)) {
                alert("Please select a date not later than the current date");
                return "false";
            }
            if (!(c >= a)) {
                alert("Allowed date range is 3 months from the current date");
                return "false";
            }
        }
    },
}
