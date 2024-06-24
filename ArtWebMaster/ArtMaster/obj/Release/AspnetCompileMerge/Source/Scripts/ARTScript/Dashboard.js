var dashboard = {
    defaults: {
        defaultLang: 'en-US',
        userId: '',
        resetPasswordCnt: 0,
        changePasswordCnt: 0,
        unLockAccountCnt: 0,
        week: "WEEK",
        month: "MONTH",
        hours: "1_HOUR",  
        currentDt: null,
        newMonth: null,
        prevsYr: null,
       
    },
    init: function () {
        
       dashboard.defaults.currentDt = new Date();
       dashboard.defaults.newMonth = dashboard.defaults.currentDt.getMonth() - 2;
       if (dashboard.defaults.newMonth < 0) {
           dashboard.defaults.newMonth += 12;
           dashboard.defaults.currentDt.setYear(dashboard.defaults.currentDt.getFullYear() - 1);
        }
       dashboard.defaults.prevsYr = new Date(dashboard.defaults.currentDt.setMonth(dashboard.defaults.newMonth));

        dashboard.bindUserActivityInfo();
        dashboard.bindUserRegistrationInfo();
        dashboard.getUserRegistrationByMonth();
        dashboard.bindGSDUserActivityInfo();
        //    dashboard.getFrequentUserLockouts();
        dashboard.getFrequentUserLockouts();
        dashboard.bindUserInCompleteActivityInfo();
        dashboard.getDeviceLockouts();
    },

    bindUserActivityInfo: function () {  // Bind the user activity information.         
        $("#dvUserActivitySection").loading();      
        var mode = $("#drpActivityModes").val(); //Get user activities defalut mode
        if (mode != "DTRANGE") {
            var startDt = new Date();
            var endDt = new Date(startDt);
            var endDtSelect = new Date(startDt);          
            endDt.setHours(startDt.getHours() - 24);
            endDtSelect.setHours(endDt.getHours());   
            $("#activitystartDate").datetimepicker({
                value: new Date(endDt),
                minDate: dashboard.defaults.prevsYr.getFullYear() + "/" + dashboard.defaults.prevsYr.getMonth() + "/" + dashboard.defaults.prevsYr.getDate(),
                maxDate: new Date(),               
                onSelectTime: function (dp, $input) {                   
                    var result =dashboard.datePickerSelection($input);                    
                    if (result == "false") { $("#activitystartDate").val(""); }
                }
            });          
          
            $("#activityendDate").datetimepicker({
                value: new Date(endDtSelect),
                minDate: dashboard.defaults.prevsYr.getFullYear() + "/" + dashboard.defaults.prevsYr.getMonth() + "/" + dashboard.defaults.prevsYr.getDate(),
                maxDate: new Date(),
                onSelectTime: function (dp, $input) {
                    var result = dashboard.datePickerSelection($input);
                    if (result == "false") { $("#activityendDate").val(""); }
                }
                
            });
        }
        var startDate = $("#activitystartDate").val();//.split('/');
        var endDate = $("#activityendDate").val();//.split('/');  
       
        if (startDate != "" && endDate != "") {      
            startDate = encodeURIComponent(startDate.replace('/', '-').replace(':', '|').replace('/', '-'));
            endDate = encodeURIComponent(endDate.replace('/', '-').replace(':', '|').replace('/', '-'));
        }
        
        var context = ['GetUserActivity', dashboard.defaults.userId, mode, startDate, endDate]; //post the userid for useractivity details
        CoreREST.get(context, null, function (response) {
            //console.log(response);
            if (response != null && response != undefined) {
                var jsonobject = JSON.parse(response);
                // Binding Reset Password
                var json = dashboard.find_in_object(jsonobject.Table, { Useractivity: 'Forgot_Password' }) //get the forget password details from table0

                //set the reset count from table0
                if (json != undefined && json != null && json.length > 0)
                    $("#spnResetPassword").html(json[0].Count);
                else
                    $("#spnResetPassword").html("0")

                // Binding Mobile / Desktop
                var json1 = dashboard.find_in_object(jsonobject.Table1, { Useractivity: 'Forgot_Password' })

                if (json1 != undefined && json1 != null && json1.length > 0) {
                    var json2 = dashboard.find_in_object(json1, { ismobiledevice: 'No' })
                    if (json2 != null && json2 != undefined && json2.length > 0)
                        $("#pResetDesktop").html(json2[0].Count);
                    else
                        $("#pResetDesktop").html("0");

                    var json2 = dashboard.find_in_object(json1, { ismobiledevice: 'Yes' })
                    if (json2 != null && json2 != undefined && json2.length > 0)
                        $("#pResetMobile").html(json2[0].Count);
                    else
                        $("#pResetMobile").html("0");
                }
                else {
                    $("#pResetMobile").html("0");
                    $("#pResetDesktop").html("0");
                }                
              

                // Binding Internet / Intranet
                var json1 = dashboard.find_in_object(jsonobject.Table2, { Useractivity: 'Forgot_Password' })

                if (json1 != undefined && json1 != null && json1.length > 0) {
                    var json2 = dashboard.find_in_object(json1, { isinternet: 'No' })
                    if (json2 != null && json2 != undefined && json2.length > 0)
                        $("#pResetIntra").html(json2[0].Count);
                    else
                        $("#pResetIntra").html("0");

                    var json2 = dashboard.find_in_object(json1, { isinternet: 'Yes' })
                    if (json2 != null && json2 != undefined && json2.length > 0)
                        $("#pResetInter").html(json2[0].Count);
                    else
                        $("#pResetInter").html("0");
                }
                else {
                    $("#pResetIntra").html("0");
                    $("#pResetInter").html("0");
                }

                var json = dashboard.find_in_object(jsonobject.Table4, { Useractivity: 'Forgot_Password' }) //get the forget password details from table0

                //set the reset count from table0
                if (json != undefined && json != null && json.length > 0)
                    $("#pResetBot").html(json[0].Count);
                else
                    $("#pResetBot").html("0")

                //Binding Account Unlock
                var json = dashboard.find_in_object(jsonobject.Table, { Useractivity: 'Unlock_Account' })

                if (json != undefined && json != null && json.length > 0)
                    $("#spnAccountUnlock").html(json[0].Count);
                else
                    $("#spnAccountUnlock").html("0");

                // Binding Mobile / Desktop
                var json1 = dashboard.find_in_object(jsonobject.Table1, { Useractivity: 'Unlock_Account' })

                if (json1 != undefined && json1 != null && json1.length > 0) {
                    var json2 = dashboard.find_in_object(json1, { ismobiledevice: 'No' })
                    if (json2 != null && json2 != undefined && json2.length > 0)
                        $("#paccountUnlockdesktop").html(json2[0].Count);
                    else
                        $("#paccountUnlockdesktop").html("0");

                    var json2 = dashboard.find_in_object(json1, { ismobiledevice: 'Yes' })
                    if (json2 != null && json2 != undefined && json2.length > 0)
                        $("#paccountUnlockmobile").html(json2[0].Count);
                    else
                        $("#paccountUnlockmobile").html("0");
                }
                else {
                    $("#paccountUnlockdesktop").html("0");
                    $("#paccountUnlockmobile").html("0");
                }

                // Binding Internet / Intranet
                var json1 = dashboard.find_in_object(jsonobject.Table2, { Useractivity: 'Unlock_Account' })

                if (json1 != undefined && json1 != null && json1.length > 0) {
                    var json2 = dashboard.find_in_object(json1, { isinternet: 'No' })
                    if (json2 != null && json2 != undefined && json2.length > 0)
                        $("#paccountUnlockIntra").html(json2[0].Count);
                    else
                        $("#paccountUnlockIntra").html("0");

                    var json2 = dashboard.find_in_object(json1, { isinternet: 'Yes' })
                    if (json2 != null && json2 != undefined && json2.length > 0)
                        $("#paccountUnlockInter").html(json2[0].Count);
                    else
                        $("#paccountUnlockInter").html("0");
                }
                else {
                    $("#paccountUnlockIntra").html("0");
                    $("#paccountUnlockInter").html("0");
                }

                var json = dashboard.find_in_object(jsonobject.Table4, { Useractivity: 'Unlock_Account' }) //get the forget password details from table0

                //set the reset count from table0
                if (json != undefined && json != null && json.length > 0)
                    $("#paccountUnlockBot").html(json[0].Count);
                else
                    $("#paccountUnlockBot").html("0")

                //Binding Change Password
                var json = dashboard.find_in_object(jsonobject.Table, { Useractivity: 'CHANGE_PASSWORD' })

                if (json != undefined && json != null && json.length > 0)
                    $("#spnChangePassword").html(json[0].Count);
                else
                    $("#spnChangePassword").html("0");

                // Binding Change Password Mobile / Desktop
                var json1 = dashboard.find_in_object(jsonobject.Table1, { Useractivity: 'CHANGE_PASSWORD' })

                if (json1 != undefined && json1 != null && json1.length > 0) {
                    var json2 = dashboard.find_in_object(json1, { ismobiledevice: 'No' })
                    if (json2 != null && json2 != undefined && json2.length > 0)
                        $("#pChangePasswordDesktop").html(json2[0].Count);
                    else
                        $("#pChangePasswordDesktop").html("0");

                    var json2 = dashboard.find_in_object(json1, { ismobiledevice: 'Yes' })
                    if (json2 != null && json2 != undefined && json2.length > 0)
                        $("#pChangePasswordMobile").html(json2[0].Count);
                    else
                        $("#pChangePasswordMobile").html("0");
                }
                else {
                    $("#pChangePasswordDesktop").html("0");
                    $("#pChangePasswordMobile").html("0");
                }

                // Binding Internet / Intranet
                var json1 = dashboard.find_in_object(jsonobject.Table2, { Useractivity: 'CHANGE_PASSWORD' })

                if (json1 != undefined && json1 != null && json1.length > 0) {
                    var json2 = dashboard.find_in_object(json1, { isinternet: 'No' })
                    if (json2 != null && json2 != undefined && json2.length > 0)
                        $("#pChangePasswordIntra").html(json2[0].Count);
                    else
                        $("#pChangePasswordIntra").html("0");

                    var json2 = dashboard.find_in_object(json1, { isinternet: 'Yes' })
                    if (json2 != null && json2 != undefined && json2.length > 0)
                        $("#pChangePasswordInter").html(json2[0].Count);
                    else
                        $("#pChangePasswordInter").html("0");
                }
                else {
                    $("#pChangePasswordIntra").html("0");
                    $("#pChangePasswordInter").html("0");
                }

                var json = dashboard.find_in_object(jsonobject.Table4, { Useractivity: 'CHANGE_PASSWORD' }) //get the forget password details from table0

                //set the reset count from table0
                if (json != undefined && json != null && json.length > 0)
                    $("#pChangePasswordBot").html(json[0].Count);
                else
                    $("#pChangePasswordBot").html("0")

                if (jsonobject.Table3 != undefined && jsonobject.Table3 != null && jsonobject.Table3.length > 0) {
                    if (jsonobject.Table3[0].UserCount != null) { $("#pgUserRegister").html(jsonobject.Table3[0].UserCount); }
                    else { $("#pgUserRegister").html("0"); }
                }


                // Binding Changepassword
                //if (jsonobject.Table1[0] != null && jsonobject.Table1[0] != undefined)
                //    $("#spnChangePassword").html(jsonobject.Table1[0].ChangePasswordCount);
                //else
                //    $("#spnChangePassword").html("0");

                //if (jsonobject.Table3 != null && jsonobject.Table3 != undefined && jsonobject.Table3.length > 0) {
                //    var json2 = dashboard.find_in_object(jsonobject.Table3, { ismobiledevice: 'No' })
                //    if (json2 != null && json2 != undefined && json2.length > 0)
                //        $("#pChangePasswordDesktop").html(json2[0].ChangePasswordCount);
                //    else
                //        $("#pChangePasswordDesktop").html("0");

                //    var json2 = dashboard.find_in_object(jsonobject.Table3, { ismobiledevice: 'Yes' })
                //    if (json2 != null && json2 != undefined && json2.length > 0)
                //        $("#pChangePasswordMobile").html(json2[0].ChangePasswordCount);
                //    else
                //        $("#pChangePasswordMobile").html("0");
                //}
                //else {
                //    $("#pChangePasswordDesktop").html("0");
                //    $("#pChangePasswordMobile").html("0");
                //}

                $("#dvUserActivitySection").loading('stop');
                //$("#dvChangePassword").loading('stop');
                //$("#dvResetPassword").loading('stop');
                //$("#dvAccountUnlock").loading('stop');
            }
        }, function (error) {
            $("#dvUserActivitySection").loading('stop');
            //$("#dvChangePassword").loading('stop');
            //$("#dvResetPassword").loading('stop');
            //$("#dvAccountUnlock").loading('stop');
        });
    },
    find_in_object: function (my_object, my_criteria) {
        return my_object.filter(function (obj) {
            return Object.keys(my_criteria).every(function (c) {
                return obj[c] == my_criteria[c];
            });
        });
    },
    //Bind the user registration information
    bindUserRegistrationInfo: function () {
        $("#dvuserregistration").loading();

        var monthNames = ["January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December"];
        var d = new Date();
        //document.write("The current month is " + monthNames[d.getMonth()]);
        $("#pCurMonthName").html("In " + monthNames[d.getMonth()]);

        var context = ['GetUserRegistrationGSDDashboard', dashboard.defaults.userId];
        CoreREST.get(context, null, function (response) {
            var data = JSON.parse(response);
            if (data != null && data != undefined) {
                //console.log(data);
                // Bind the User Registration information
                if (data.Table[0] != null && data.Table[0] != undefined) {
                    $("#pUserRegToday").html(data.Table[0].UserRegistrationCount);
                }
                if (data.Table1[0] != null && data.Table1[0] != undefined) {
                    $("#pUserRegWeekly").html(data.Table1[0].UserRegistrationCount);
                }
                if (data.Table2[0] != null && data.Table2[0] != undefined) {
                    $("#pUserRegMonth").html(data.Table2[0].UserRegistrationCount);
                }
                if (data.Table3[0] != null && data.Table3[0] != undefined) {
                    $("#pUserRegTotal").html(data.Table3[0].UserRegistrationCount);
                }
                if (data.Table4[0] != null && data.Table4[0] != undefined) {
                    $("#pUserOtpReg").html(data.Table4[0].OtpRegisteredCount);
                }
                if (data.Table5[0] != null && data.Table5[0] != undefined) {
                    $("#pprivateMobNo").html(data.Table5[0].IsPrivateMobileNum);
                }

            }
            $("#dvuserregistration").loading('stop');
        }, function (error) {
            $("#dvuserregistration").loading('stop');
        });
    },
    drpActivityModeChange: function () {
        //Start- Date Wise user Activity filter       
      
        var mode = $("#drpActivityModes").val();
        if (mode == "DTRANGE") {
            $('#ActivitydatePick').slideToggle();
        }
        else {
            //End- Date Wise user Activity filter
            $('#ActivitydatePick').hide();
            dashboard.bindUserActivityInfo();
        }       
    },  
    getUserRegistrationByMonth: function () {
        var context = ['GetUserRegistrationByMonth', dashboard.defaults.userId];
        CoreREST.get(context, null, function (response) {
            var data = JSON.parse(response);
            dashboard.bindChart(data);
        });
    },
    bindChart: function (data) {
        //console.log(data);        
        var arMonth = new Array();
        var arValue = new Array();
        //Bind the month name and count in chart
        for (var i = 0; i < data.Table.length; i++) {
            var monthName = data.Table[i].MonthName;
            var count = data.Table[i].UserRegistrationCount;

            arMonth.push(monthName);
            arValue.push(count);
        }

        //var chart = document.getElementById('compareChart'),
        //myChart = echarts.init(chart);

        //var option = {
        //    title: {
        //        text: 'Month Over Month Comparison',
        //        textStyle: {
        //            fontSize: 15,
        //            fontStyle: 'normal',
        //            fontWeight: 'normal',
        //        }
        //    },
        //    tooltip: {
        //        trigger: 'axis'
        //    },
        //    //legend: {
        //    //    data: ['意向', '预购', '成交']
        //    //},
        //    toolbox: {
        //        show: true,
        //        feature: {
        //            mark: { title: 'mark', show: false },
        //            dataView: { title: 'dataView', show: false, readOnly: false },
        //            magicType: { title: 'magicType', show: false, type: ['line', 'bar'] },
        //            restore: { title: 'restore', show: true },
        //            saveAsImage: { title: 'Save as Image', show: true, lang: ['Save'], }
        //        }
        //    },
        //    calculable: true,
        //    xAxis: [
        //        {
        //            type: 'category',
        //            boundaryGap: false,
        //            data: arMonth,
        //            axisLabel: {
        //                show: true,
        //                interval: 0,
        //                rotate: 20,
        //            },
        //        }
        //    ],
        //    yAxis: [
        //        {
        //            type: 'value',
        //        }
        //    ],
        //    series: [
        //        {
        //            name: 'User Registration',
        //            type: 'bar',
        //            smooth: true,
        //            itemStyle: { normal: { areaStyle: { type: 'default' } } },
        //            data: arValue
        //        },
        //    ]
        //};

        //myChart.clear();
        //// Load data into the ECharts instance
        //myChart.setOption(option);

        //myChart.refresh();



        var dom = document.getElementById("compareChart");
        var myChart = echarts.init(dom);
        var app = {};
        option = null;

        option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'shadow'
                },
                formatter: '{a1} : {c1}'
            },
            //legend: {
            //    data: ['User Registration']
            //},
            toolbox: {
                show: true,
                showTitle: true,
                itemGap: 5,
                right: 15,
                feature: {
                    dataView: { show: false, readOnly: false },
                    magicType: { show: true, type: ['line', 'bar'], title: ['line chart', 'bar chart'] },
                    restore: { show: true, title: 'restore' },
                    saveAsImage: { show: true, title: 'download' }
                }
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '3%',
                containLabel: true
            },
            xAxis: {
                type: 'category',
                data: arMonth, // bind the month name in chart
                axisLabel: {
                    show: true,
                    interval: 0,
                    //rotate: 20,
                },
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: 'User Registration',
                    type: 'bar',
                    barWidth: '50px',
                    label: {
                        normal: {
                            show: true,
                            position: 'insideTop'
                        }
                    },
                    itemStyle: {
                        normal: {
                            color: '#59b2ee',
                        }
                    },
                    data: arValue //bind the month wise count details in chart
                },
                {
                    name: 'User Registration',
                    type: 'line',
                    itemStyle: {
                        normal: {
                            color: '#E37278',
                        }
                    },
                    data: arValue
                },
            ]
        };;
        if (option && typeof option === "object") {
            myChart.setOption(option, true);
        }

    },
    //Bind the User Activity info
    bindGSDUserActivityInfo: function () {
        $("#deskActivity").loading();
        var mode = $("#drpGSDActivityModes").val();
        if (mode != "DTRANGE") {
            var startDt = new Date();
            var endDt = new Date(startDt);
            var endDtSelect = new Date(startDt);            
            endDt.setHours(startDt.getHours() - 24);
            endDtSelect.setHours(endDt.getHours());             
            $("#startDate").datetimepicker({
                value: new Date(endDt),
                minDate: dashboard.defaults.prevsYr.getFullYear() + "/" + dashboard.defaults.prevsYr.getMonth() + "/" + dashboard.defaults.prevsYr.getDate(),
                maxDate: new Date(),
                onSelectTime: function (dp, $input) {
                    var result =dashboard.datePickerSelection($input);
                    if (result == "false") { $("#startDate").val(""); }
                }
            });
            $("#endDate").datetimepicker({
                value: new Date(endDtSelect),
                minDate: dashboard.defaults.prevsYr.getFullYear() + "/" + dashboard.defaults.prevsYr.getMonth() + "/" + dashboard.defaults.prevsYr.getDate(),
                maxDate: new Date(),
                onSelectTime: function (dp, $input) {
                    var result =dashboard.datePickerSelection($input);
                    if (result == "false") { $("#endDate").val(""); }
                }
            });
        }
        var startDate = $("#startDate").val();//.split('/');
        var endDate = $("#endDate").val();//.split('/');
        if (startDate != "" && endDate != "") {          
            startDate = encodeURIComponent(startDate.replace('/', '-').replace(':', '|').replace('/', '-'));
            endDate = encodeURIComponent(endDate.replace('/', '-').replace(':', '|').replace('/', '-'));
        }

        var context = ['GetGSDActivity', dashboard.defaults.userId, mode, startDate, endDate];
        CoreREST.get(context, null, function (response) {
            //console.log(response);
            if (response != null && response != undefined) {
                var jsonobject = JSON.parse(response);

                // Binding GSD Reset Password
                var json = dashboard.find_in_object(jsonobject.Table, { activity: 'Reset_Password' })

                if (json != undefined && json != null && json.length > 0)
                    $("#pgsdResetPass").html(json[0].Count);
                else
                    $("#pgsdResetPass").html("0")

                // Binding GSD Reset Password
                var json = dashboard.find_in_object(jsonobject.Table, { activity: 'Unlock_Account' })

                if (json != undefined && json != null && json.length > 0)
                    $("#pgsdAccUnlock").html(json[0].Count);
                else
                    $("#pgsdAccUnlock").html("0") //If Account unlock count is zero,set the account unlock value is zero.

                $("#deskActivity").loading('stop');

            }
        }, function (error) {
            $("#deskActivity").loading('stop');
        });
    },
    //Activity chnage function
    drpGSDActivityModeChange: function () {
        //Start- Date Wise user Activity filter    
        var mode = $("#drpGSDActivityModes").val();
        if (mode == "DTRANGE") {
            $('#datePick').slideToggle();
        }
        else {
            //End- Date Wise user Activity filter
            $('#datePick').hide();
            dashboard.bindGSDUserActivityInfo();
        }
    },
    selectFrequentUserLockouts: function () {
        var startDt = new Date();
        var endDt = new Date(startDt);
        var endDtSelect = new Date(startDt);
       
        endDt.setHours(startDt.getHours() - 24);
        endDtSelect.setHours(endDt.getHours());
           
        $("#FrqAccLockstartDate").datetimepicker({
            value: new Date(endDt),
            minDate: dashboard.defaults.prevsYr.getFullYear() + "/" + dashboard.defaults.prevsYr.getMonth() + "/" + dashboard.defaults.prevsYr.getDate(),
            maxDate: new Date(),
            onSelectTime: function (dp, $input) {
                var result =dashboard.datePickerSelection($input);
                if (result == "false") { $("#FrqAccLockstartDate").val(""); }
            }
        });
        $("#FrqAccLockendDate").datetimepicker({
            value: new Date(endDtSelect),
            minDate: dashboard.defaults.prevsYr.getFullYear() + "/" + dashboard.defaults.prevsYr.getMonth() + "/" + dashboard.defaults.prevsYr.getDate(),
            maxDate: new Date(),
            onSelectTime: function (dp, $input) {
                var result =dashboard.datePickerSelection($input);
                if (result == "false") { $("#FrqAccLockendDate").val(""); }
            }
        });
        var mode = $("#drpIncompletedActivityModes").val();
        if (mode == "DTRANGE") {
            $('#FrqAccLockdatePick').slideToggle();
        }
        else {
            //End- Date Wise user Activity filter
            $('#FrqAccLockdatePick').hide();
            dashboard.getFrequentUserLockouts();
        }
    },
    //get the account lock out details using Frequent account lock modes
     getFrequentUserLockouts: function () {
        var mode = $("#drpFrqAccLockModes").val();
        if (mode == "DTRANGE") {
            $('#FrqAccLockdatePick').slideToggle();
        }
        else {           
            $('#FrqAccLockdatePick').hide();
            var startDt = new Date();
            var endDt = new Date(startDt);
            var endDtSelect = new Date(startDt);            
            endDt.setHours(startDt.getHours() - 24);
            endDtSelect.setHours(endDt.getHours());
            
            $("#FrqAccLockstartDate").datetimepicker({
                value: new Date(endDt),
                minDate: dashboard.defaults.prevsYr.getFullYear() + "/" + dashboard.defaults.prevsYr.getMonth() + "/" + dashboard.defaults.prevsYr.getDate(),
                maxDate: new Date(),
                onSelectTime: function (dp, $input) {
                    var result =dashboard.datePickerSelection($input);
                    if (result == "false") { $("#FrqAccLockstartDate").val(""); }
                }
            });
            $("#FrqAccLockendDate").datetimepicker({
                value: new Date(endDtSelect),
                minDate: dashboard.defaults.prevsYr.getFullYear() + "/" + dashboard.defaults.prevsYr.getMonth() + "/" + dashboard.defaults.prevsYr.getDate(),
                maxDate: new Date(),
                onSelectTime: function (dp, $input) {
                    var result =dashboard.datePickerSelection($input);
                    if (result == "false") { $("#FrqAccLockendDate").val(""); }
                }
            });

            $("#unlockActivity").loading();
            var startDate = $("#FrqAccLockstartDate").val();//.split('/');
            var endDate = $("#FrqAccLockendDate").val();//.split('/');
            if (startDate != "" && endDate != "") {
                startDate = encodeURIComponent(startDate.replace('/', '-').replace(':', '|').replace('/', '-'));
                endDate = encodeURIComponent(endDate.replace('/', '-').replace(':', '|').replace('/', '-'));
            }
            var context = ['GetFrequentUserLock', dashboard.defaults.userId, mode, startDate, endDate];
            CoreREST.get(context, null, function (response) {
                var data = JSON.parse(response);
                dashboard.bindFrequentUser(data);
            });
        }      
    },
    bindFrequentUser: function (data) {      
        var dataAr = new Array();
        var startDate = $("#FrqAccLockstartDate").val();//.split('/');
        var endDate = $("#FrqAccLockendDate").val();//.split('/');
        var mode = $("#drpFrqAccLockModes").val();
        var currdt = new Date();
        var curr_date = currdt.getDate();
        var curr_month = currdt.getMonth()+1;
        var curr_year = currdt.getFullYear();
        var curr_hours = currdt.getHours();
        var curr_mins = currdt.getMinutes();
        var hours = currdt.getHours()-1;
        if (mode == dashboard.defaults.hours) {
            startDate = curr_year + "/" + (curr_month) + "/" + curr_date + "/" + hours + "/" + curr_mins;
            endDate = curr_year + "/" + (curr_month) + "/" + curr_date + "/" + curr_hours + "/" + curr_mins;
        }
        if (mode == dashboard.defaults.week)
        {
            var priorDate = new Date();
            priorDate.setDate(priorDate.getDate() - 6)
            var stDt = priorDate.getFullYear() + "/" + (priorDate.getMonth() + 1) + "/" + priorDate.getDate();
            startDate = stDt;      
            endDate = curr_year + "/" + (curr_month) + "/" + curr_date + "/" + curr_hours + "/" + curr_mins;
        }
        if (mode == dashboard.defaults.month) {          
            var stDt = currdt.getFullYear() + '/' + (currdt.getMonth() +1) + '/' + (currdt.getDate(), 1);
            startDate = stDt;           
            endDate = curr_year + "/" + (curr_month) + "/" + curr_date + "/" + curr_hours + "/" + curr_mins;
        }
        //get the frequent locked user details
        for (var i = 0; i < data.length; i++) {
            var value = {
                "No": i + 1,
                "User ID": data[i].UserId,
                "User Name": data[i].UserName,
                "Counts": data[i].Count              
            }
            dataAr.push(value);
        }
        //Bind the account locked deatils using jsGrid
        $("#unlockGrid").jsGrid({
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
                {
                    name: "User ID", type: "text", width: 20, itemTemplate: function (value) {                     
                        var hky = $("#hdky").val();
                        var page = "Dashboard";
                        var key = CryptoJS.enc.Utf8.parse(hky);
                        var iv = CryptoJS.enc.Utf8.parse(hky);
                        var encryptedValue = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(page+"|"+value + "|" + startDate + "|" + endDate + "|" + mode), key,
                            {
                                keySize: 128 / 8,
                                iv: iv,
                                mode: CryptoJS.mode.CBC,
                                padding: CryptoJS.pad.Pkcs7
                            });

                        //var encryptedValue = dashboard.encryptValue(value);
                        return $("<a>").attr("href", "../Reports/EmployeeSearch?value=" + encodeURIComponent(encryptedValue) + "").text(value);
                    }, filtering: true
                },
                { name: "User Name", type: "text", width: 40 },
                { name: "Counts", type: "text", width: 30 }
              
            ]
        });

        $("#unlockActivity").loading('stop');
    },
    downloadGSDUserActivity: function () {
        var mode = $("#drpGSDActivityModes").val();
        $("#Mode").val(mode);
        $("#StartDate").val($("#startDate").val());
        $("#EndDate").val($("#endDate").val());
        $("#csvform").submit();
    },
    encryptValue: function (value) {
        var xorKey = 100; // this key is very important using this key only we are decrypt the value in server side ;)
        var result = "";
        for (i = 0; i < value.length; ++i) {
            result += String.fromCharCode(xorKey ^ value.charCodeAt(i));
        }
        return result;
    },
    bindUserInCompleteActivityInfo: function () {
        $("#dvUserIncompActivitySection").loading();
        var mode = $("#drpIncompletedActivityModes").val();
        if (mode != "DTRANGE") {
            var startDt = new Date();
            var endDt = new Date(startDt);
            var endDtSelect = new Date(startDt);
           
            endDt.setHours(startDt.getHours() - 24);
            endDtSelect.setHours(endDt.getHours());
           
            $("#IncompletedActivitystartDate").datetimepicker({
                value: new Date(endDt),
                minDate: dashboard.defaults.prevsYr.getFullYear() + "/" + dashboard.defaults.prevsYr.getMonth() + "/" + dashboard.defaults.prevsYr.getDate(),
                maxDate: new Date(),
                onSelectTime: function (dp, $input) {
                    var result =dashboard.datePickerSelection($input);
                    if (result == "false") { $("#IncompletedActivitystartDate").val(""); }
                }
            });
            $("#IncompletedActivityendDate").datetimepicker({
                value: new Date(endDtSelect),
                minDate: dashboard.defaults.prevsYr.getFullYear() + "/" + dashboard.defaults.prevsYr.getMonth() + "/" + dashboard.defaults.prevsYr.getDate(),
                maxDate: new Date(),
                onSelectTime: function (dp, $input) {
                    var result =dashboard.datePickerSelection($input);
                    if (result == "false") { $("#IncompletedActivityendDate").val(""); }
                }
            });

        }
        var startDate = $("#IncompletedActivitystartDate").val();//.split('/');
        var endDate = $("#IncompletedActivityendDate").val();//.split('/');
        if (startDate != "" && endDate != "") {
            startDate = encodeURIComponent(startDate.replace('/', '-').replace(':', '|').replace('/', '-'));
            endDate = encodeURIComponent(endDate.replace('/', '-').replace(':', '|').replace('/', '-'));
        }

        var context = ['GetUserInCompleteActivity', dashboard.defaults.userId, mode, startDate, endDate];
        CoreREST.get(context, null, function (response) {
            //console.log(response);
            if (response != null && response != undefined) {
                var jsonobject = JSON.parse(response);

                // Binding Reset Password
                var json = dashboard.find_in_object(jsonobject.Table, { Useractivity: 'Forgot_Password' })

                if (json != undefined && json != null && json.length > 0)
                    $("#spnIncompResetPassword").html(json[0].Count);
                else
                    $("#spnIncompResetPassword").html("0")

                //Binding Account Unlock
                var json = dashboard.find_in_object(jsonobject.Table, { Useractivity: 'Unlock_Account' })

                if (json != undefined && json != null && json.length > 0)
                    $("#spnIncompAccountUnlock").html(json[0].Count);
                else
                    $("#spnIncompAccountUnlock").html("0");

                //Binding User Register
                var json = dashboard.find_in_object(jsonobject.Table, { Useractivity: 'User_Register' })

                if (json != undefined && json != null && json.length > 0)
                    $("#spnIncompUserRegister").html(json[0].Count);
                else
                    $("#spnIncompUserRegister").html("0");

                //Binding Change Password
                if (jsonobject.Table1 != undefined && jsonobject.Table1 != null && jsonobject.Table1.length > 0)
                    $("#spnIncompChangePassword").html(jsonobject.Table1[0].ChangePasswordCount);
                else
                    $("#spnIncompChangePassword").html("0");


                $("#dvUserIncompActivitySection").loading('stop');

            }
        }, function (error) {
            $("#dvUserIncompActivitySection").loading('stop');

        });
    },
    getUserInCompleteActivityInfo: function () {
        //Start- Date Wise user Activity filter  
        var mode = $("#drpIncompletedActivityModes").val();
        if (mode == "DTRANGE") {
            $('#IncompletedActivitydatePick').slideToggle();
        }
        else {
            //End- Date Wise user Activity filter
            $('#IncompletedActivitydatePick').hide();
            dashboard.bindUserInCompleteActivityInfo();
        }
    },
    btnCheck: function () {
        var startDate = $("#startDate").val();//.split('/');
        var endDate = $("#endDate").val();//.split('/');       
        if (startDate == "") {
            alert("Start date should not be empty");
            $("#startDate").focus();
            return false;
        }
        else
        {
            var result =dashboard.datePickerSelection($("#startDate"));
            if (result == "false") { $("#startDate").val(""); return false; }
        }
        if (endDate == "") {
            alert("End date should not be empty");
            $("#endDate").focus();
            return false;
        }
        else
        {
            var result = dashboard.datePickerSelection($("#endDate"));
            if (result == "false") { $("#endDate").val(""); return false; }
        }
        var currentYear = (new Date).getFullYear().toString();
        var startDtYr = startDate.substring(0, 4);
        var endDateYr = endDate.substring(0, 4);
      

        if (new Date(startDate) > new Date(endDate)) {
            alert("End date should be greater than Start date");
            $("#endDate").focus();
            return false;
        }

        dashboard.bindGSDUserActivityInfo();
    },
    activitybtnCheck: function () {
        var startDate = $("#activitystartDate").val();//.split('/');
        var endDate = $("#activityendDate").val();//.split('/');
      
        if (startDate == "") {
            alert("Start date should not be empty");
            $("#activitystartDate").focus();
            return false;
        }
        else
        {
            var result = dashboard.datePickerSelection($("#activitystartDate"));
            if (result == "false") { $("#activitystartDate").val(""); return false; }
        }
        if (endDate == "") {
            alert("End date should not be empty");
            $("#activityendDate").focus();
            return false;
        }
        else
        {
            var result = dashboard.datePickerSelection($("#activityendDate"));
            if (result == "false") { $("#activityendDate").val(""); return false; }
        }
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

        dashboard.bindUserActivityInfo();
    },
    FrqAccLockbtnCheck: function () {
        var startDate = $("#FrqAccLockstartDate").val();//.split('/');
        var endDate = $("#FrqAccLockendDate").val();//.split('/');
        if (startDate == "") {
            alert("Start date should not be empty");
            $("#FrqAccLockstartDate").focus();
            return false;
        }
        else
        {
            var result =dashboard.datePickerSelection($("#FrqAccLockstartDate"));
            if (result == "false") { $("#FrqAccLockstartDate").val(""); return false; }
        }
         if (endDate == "") {
            alert("End date should not be empty");
            $("#FrqAccLockendDate").focus();
            return false;
        }
         else
         {
             var result = dashboard.datePickerSelection($("#FrqAccLockendDate"));
             if (result == "false") { $("#FrqAccLockendDate").val(""); return false; }
         }
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

        dashboard.BindtFrequentUserLockouts();
    },
    BindtFrequentUserLockouts: function () {
        $("#unlockActivity").loading();       
        var mode = $("#drpFrqAccLockModes").val();      
        var startDate = $("#FrqAccLockstartDate").val();//.split('/');
        var endDate = $("#FrqAccLockendDate").val();//.split('/');
        if (startDate != "" && endDate != "") {
            startDate = encodeURIComponent(startDate.replace('/', '-').replace(':', '|').replace('/', '-'));
            endDate = encodeURIComponent(endDate.replace('/', '-').replace(':', '|').replace('/', '-'));
        }
        var context = ['GetFrequentUserLock', dashboard.defaults.userId, mode, startDate, endDate];
        CoreREST.get(context, null, function (response) {
            var data = JSON.parse(response);
            dashboard.bindFrequentUser(data);
        });
    },
    IncompletedActivitybtnCheck: function () {
        var startDate = $("#IncompletedActivitystartDate").val();//.split('/');
        var endDate = $("#IncompletedActivityendDate").val();//.split('/');
        if (startDate == "") {
            alert("Start date should not be empty");
            $("#IncompletedActivitystartDate").focus();
            return false;
        }
        else
        {
            var result =dashboard.datePickerSelection($("#IncompletedActivitystartDate"));
            if (result == "false") { $("#IncompletedActivitystartDate").val(""); return false; }
        }
         if (endDate == "") {
            alert("End date should not be empty");
            $("#IncompletedActivityendDate").focus();
            return false;
        }
         else
         {
             var result =dashboard.datePickerSelection($("#IncompletedActivityendDate"));
             if (result == "false") { $("#IncompletedActivityendDate").val(""); return false; }
         }
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

        dashboard.bindUserInCompleteActivityInfo();
    },
    deviceLockCustomeDateBtnClick: function () {
        debugger;
        var startDate = $("#txtDevicelockstartDate").val();//.split('/');
        var endDate = $("#txtDevicelockendDate").val();//.split('/');
        if (startDate == "") {
            alert("Start date should not be empty");
            $("#txtDevicelockstartDate").focus();
            return false;
        }
        else {
            var result =dashboard.datePickerSelection($("#txtDevicelockstartDate"));
            if (result == "false") { $("#txtDevicelockstartDate").val(""); return false; }
        }
        if (endDate == "") {
            alert("End date should not be empty");
            $("#txtDevicelockendDate").focus();
            return false;
        }
        else {
            var result =dashboard.datePickerSelection($("#txtDevicelockendDate"));
            if (result == "false") { $("#txtDevicelockendDate").val(""); return false; }
        }
        var currentYear = (new Date).getFullYear().toString();
        var startDtYr = startDate.substring(0, 4);
        var endDateYr = endDate.substring(0, 4);

        //if (endDateYr != currentYear || startDtYr != currentYear) {
        //    alert("Start and End date should be in current year");
        //    return false;
        //}
        if (new Date(startDate) > new Date(endDate)) {
            alert("End date should be greater than Start date");
            $("#txtDevicelockendDate").focus();
            return false;
        }

        dashboard.customdaterangeBindDeviceLock();
    },
    customdaterangeBindDeviceLock: function () {
        $("#dvDeviceLockContainer").loading();
        var mode = $("#drpDeviceLockModes").val();
        var startDate = $("#txtDevicelockstartDate").val();//.split('/');
        var endDate = $("#txtDevicelockendDate").val();//.split('/');
        if (startDate != "" && endDate != "") {
            startDate = encodeURIComponent(startDate.replace('/', '-').replace(':', '|').replace('/', '-'));
            endDate = encodeURIComponent(endDate.replace('/', '-').replace(':', '|').replace('/', '-'));
        }
        var context = ['GetDeviceLock', dashboard.defaults.userId, mode, startDate, endDate];
        CoreREST.get(context, null, function (response) {
            var data = JSON.parse(response);
            dashboard.bindDeviceLockGrid(data);
        });
    },
    getDeviceLockouts: function () {
        var mode = $("#drpDeviceLockModes").val();
        if (mode == "DTRANGE") {
            $('#dvDevicelockdatepicker').slideToggle();
        }
        else {
            $('#dvDevicelockdatepicker').hide();
            var startDt = new Date();
            var endDt = new Date(startDt);
            var endDtSelect = new Date(startDt);
           
            endDt.setHours(startDt.getHours() - 24);
            endDtSelect.setHours(endDt.getHours());
            
            $("#txtDevicelockstartDate").datetimepicker({
                value: new Date(endDt),
                minDate: dashboard.defaults.prevsYr.getFullYear() + "/" + dashboard.defaults.prevsYr.getMonth() + "/" + dashboard.defaults.prevsYr.getDate(),
                maxDate: new Date(),
                onSelectTime: function (dp, $input) {
                    var result =dashboard.datePickerSelection($input);
                    if (result == "false") { $("#txtDevicelockstartDate").val(""); }
                }
            });
            $("#txtDevicelockendDate").datetimepicker({
                value: new Date(endDtSelect),
                minDate: dashboard.defaults.prevsYr.getFullYear() + "/" + dashboard.defaults.prevsYr.getMonth() + "/" + dashboard.defaults.prevsYr.getDate(),
                maxDate: new Date(),
                onSelectTime: function (dp, $input) {
                    var result =dashboard.datePickerSelection($input);
                    if (result == "false") { $("#txtDevicelockendDate").val(""); }
                }
            });

            $("#dvDeviceLockContainer").loading();
            var startDate = $("#txtDevicelockstartDate").val();//.split('/');
            var endDate = $("#txtDevicelockendDate").val();//.split('/');
            if (startDate != "" && endDate != "") {
                startDate = encodeURIComponent(startDate.replace('/', '-').replace(':', '|').replace('/', '-'));
                endDate = encodeURIComponent(endDate.replace('/', '-').replace(':', '|').replace('/', '-'));
            }
            var context = ['GetDeviceLock', dashboard.defaults.userId, mode, startDate, endDate];
            CoreREST.get(context, null, function (response) {
                var data = JSON.parse(response);
                dashboard.bindDeviceLockGrid(data);
            });
        }
    },
    bindDeviceLockGrid: function (data) {
        var dataAr = new Array();
        var startDate = $("#txtDevicelockstartDate").val();//.split('/');
        var endDate = $("#txtDevicelockendDate").val();//.split('/');
        var mode = $("#drpDeviceLockModes").val();
        var currdt = new Date();
        var curr_date = currdt.getDate();
        var curr_month = currdt.getMonth() + 1;
        var curr_year = currdt.getFullYear();
        var curr_hours = currdt.getHours();
        var curr_mins = currdt.getMinutes();
        var hours = currdt.getHours() - 1;
        if (mode == dashboard.defaults.hours) {
            startDate = curr_year + "/" + (curr_month) + "/" + curr_date + "/" + hours + "/" + curr_mins;
            endDate = curr_year + "/" + (curr_month) + "/" + curr_date + "/" + curr_hours + "/" + curr_mins;
        }
        if (mode == dashboard.defaults.week) {
            var priorDate = new Date();
            priorDate.setDate(priorDate.getDate() - 6)
            var stDt = priorDate.getFullYear() + "/" + (priorDate.getMonth() + 1) + "/" + priorDate.getDate();
            startDate = stDt;
            endDate = curr_year + "/" + (curr_month) + "/" + curr_date + "/" + curr_hours + "/" + curr_mins;
        }
        if (mode == dashboard.defaults.month) {
            var stDt = currdt.getFullYear() + '/' + (currdt.getMonth() + 1) + '/' + (currdt.getDate(), 1);
            startDate = stDt;
            endDate = curr_year + "/" + (curr_month) + "/" + curr_date + "/" + curr_hours + "/" + curr_mins;
        }
        //get the frequent locked user details
        for (var i = 0; i < data.length; i++) {
            var value = {
                "No": i + 1,
                "Source": data[i].EventSource,
                "Counts": data[i].Count
            }
            dataAr.push(value);
        }
        //Bind the account locked deatils using jsGrid
        $("#dvDevicelockGrid").jsGrid({
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
                { name: "Source", type: "text", width: 60 },
                { name: "Counts", type: "text", width: 30 }

            ]
        });

        $("#dvDeviceLockContainer").loading('stop');
    },
    datePickerSelection: function ($input) {
        var startDt = new Date();
        var endDt = new Date(startDt);       

        var minDt = dashboard.defaults.prevsYr.getFullYear() + "/" + (dashboard.defaults.prevsYr.getMonth()) + "/" + dashboard.defaults.prevsYr.getDate();
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



