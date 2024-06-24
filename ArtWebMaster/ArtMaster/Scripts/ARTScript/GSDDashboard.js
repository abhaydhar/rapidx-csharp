var gsddashboard = {
    defaults: {
        userId: '',
        noDataFound: '<div style="text-align: center;margin-top: 14%;">- No Data found -</div>',
        mode: "Last30Days",
        pagename: "GSDDashboard"
    },
    init: function () {        
        // initialize methods
        gsddashboard.getAccountlockOutByMonth();
        gsddashboard.getAccountlockOutByDay();
        gsddashboard.getGsdActivitytByDay();
        gsddashboard.getFrequentCallers();

        // Date prototypes
        Date.prototype.monthNames = [
            "January", "February", "March",
            "April", "May", "June",
            "July", "August", "September",
            "October", "November", "December"
        ];

        Date.prototype.getMonthName = function () {
            return this.monthNames[this.getMonth()];
        };
        Date.prototype.getShortMonthName = function () {
            return this.getMonthName().substr(0, 3);
        };
    },
    //Bind AccountLockOut by month wise
    getAccountlockOutByMonth: function () {
        $("#dvcontainer1").loading();

        var context = ['GetAccountlockByMonth', gsddashboard.defaults.userId];
        CoreREST.get(context, null, function (response) {
            var data = JSON.parse(response);
            gsddashboard.bindAccountlockOutByMonth(data);
        }, function (error) { $("#dvcontainer1").loading('stop'); });
    },
    bindAccountlockOutByMonth: function (data) {
        if (data == null || data == "" || data.length == 0) {
            $("#dvChartaccountlockmonth").html(gsddashboard.defaults.noDataFound);
            $("#dvcontainer1").loading('stop');
            return false;
        }
        //data preparation
        var arMonth = new Array();
        var arValue = new Array();
        var arDisValue = new Array();

        for (var i = 0; i < data.Table.length; i++) {
            var monthName = data.Table[i].MonthName;
            var count = data.Table[i].Count;
            var discount = data.Table[i].DistinctCount;

            arMonth.push(monthName);
            arValue.push(count);
            arDisValue.push(discount)
        }

        var dom = document.getElementById("dvChartaccountlockmonth");
        var myChart = echarts.init(dom);
        var app = {};
        option = null;
        option = {
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['Repeat', 'Unique']
            },
            toolbox: {
                show: true,
                itemGap: 5,
                right: 15,
                feature: {
                    dataView: { show: false, readOnly: false },
                    magicType: { show: true, type: ['line', 'bar'], title: ['line chart', 'bar chart'] },
                    restore: { show: true, title: 'restore' },
                    saveAsImage: { show: true, title: 'download' }
                }
            },
            calculable: true,
            xAxis: [
                {
                    type: 'category',
                    data: arMonth, //get the Month name details 
                    axisLabel: {
                        show: true,
                        interval: 0,
                        //rotate: 20,
                    },
                }
            ],
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: [
                {
                    name: 'Repeat',
                    type: 'bar',
                   // barWidth: '30px',                   
                    data: arValue, //Get the month value
                    label: {
                        normal: {
                            show: true,
                            position: 'top'    
                        }
                    },
                    itemStyle: {
                        normal: {
                            color: '#6CE97C',
                        }
                    }

                },
                {
                    name: 'Unique',
                    type: 'bar',
                  //  barWidth: '30px',
                    data: arDisValue,
                    label: {
                        normal: {
                            show: true,
                            position: 'top'
                        }
                    },
                    itemStyle: {
                        normal: {
                            color: '#89926A',
                        }
                    }

                }
            ]
        };
        ;
        if (option && typeof option === "object") {
            myChart.setOption(option, true);
        }

        $("#dvcontainer1").loading('stop');
    },
    getAccountlockOutByDay: function () {
        $("#dvcontainer2").loading();
        var context = ['GetAccountlockByDay', gsddashboard.defaults.userId];
        CoreREST.get(context, null, function (response) {
            var data = JSON.parse(response);
            gsddashboard.bindaccountlockbyDay(data);
        }, function (error) { $("#dvcontainer2").loading('stop'); });
    },
    bindaccountlockbyDay: function (data) {
        if (data == null || data == "" || data.Table.length == 0) {
            $("#dvChartaccountlockDay").html(gsddashboard.defaults.noDataFound);
            $("#dvcontainer2").loading('stop');
            return false;
        }

        //data preparation
        var arDate = new Array();
        var arValue = new Array();
        var arDisValue = new Array();

        for (var i = 0; i < data.Table.length; i++) {
            var day = data.Table[i].Day;
            var count = data.Table[i].Count;
            var discount = data.Table[i].DistinctCount;

            // usage:
            var d = new Date(day);
            //alert(d.getShortMonthName()); // "Oct"

            arDate.push(d.getDate() + " " + d.getShortMonthName());
            arValue.push(count);
            arDisValue.push(discount)
        }
        //arDate = arDate.reverse();
        //arValue = arValue.reverse();
        //arDisValue = arDisValue.reverse();

        var dom = document.getElementById("dvChartaccountlockDay");
        var myChart = echarts.init(dom);
        var app = {};
        option = null;

        option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'shadow'
                },
                formatter: '{a1} : {c1}<br />{a2} : {c2}'
            },
            legend: {
                data: ['Repeat', 'Unique']
            },
            toolbox: {
                show: true,
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
                data: arDate,
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
                    name: 'Unique',
                    type: 'bar',
                    stack: 'Account lock',
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
                    data: arDisValue
                },
                {
                    name: 'Repeat',
                    type: 'bar',
                    stack: 'Account lock',
                    label: {
                        normal: {
                            show: true,
                            position: 'insideTop'
                        }
                    },
                    itemStyle: {
                        normal: {
                            color: '#7e90c0',
                        }
                    },
                    barWidth: '50px',
                    data: arValue
                },
                {
                    name: 'Unique',
                    type: 'line',
                    itemStyle: {
                        normal: {
                            color: '#7e90c0',
                        }
                    },
                    data: arDisValue
                },

            ]
        };;
        if (option && typeof option === "object") {
            myChart.setOption(option, true);
        }

        $("#dvcontainer2").loading('stop');
    },
    getGsdActivitytByDay: function () {
        $("#dvcontainer4").loading();
        var context = ['GetGsdActivityByDay', gsddashboard.defaults.userId];
        CoreREST.get(context, null, function (response) {
            var data = JSON.parse(response);
            gsddashboard.bindGsdActivitytByDay(data);
        }, function (error) { $("#dvcontainer4").loading('stop'); });
    },
    bindGsdActivitytByDay: function (data) {
        if (data == null || data == "" || data.Table.length == 0) {
            $("#dvChartGsdActivityByDay").html(gsddashboard.defaults.noDataFound);
            $("#dvcontainer4").loading('stop');
            return false;
        }

        //data preparation
        var arMonth = new Array();
        var arValue = new Array();
        var arDisValue = new Array();

        var uniqueDate = [];
        for (i = 0; i < data.Table.length; i++) {
            if (uniqueDate.indexOf(data.Table[i].logdatetime) === -1) {
                uniqueDate.push(data.Table[i].logdatetime);
            }
        }

        for (i = 0; i < uniqueDate.length; i++) {
            var resetcount = 0;
            var unlockcount = 0;

            var filterjson = gsddashboard.find_in_object(data.Table, { logdatetime: uniqueDate[i] })
            if (filterjson != null && filterjson != undefined && filterjson.length > 0) {
                var resetFilter = gsddashboard.find_in_object(filterjson, { activity: "Reset_Password" })

                if (resetFilter != null && resetFilter != undefined && resetFilter.length > 0) {
                    resetcount = resetFilter[0].Count;
                }
                var unlockFilter = gsddashboard.find_in_object(filterjson, { activity: "Unlock_Account" })

                if (unlockFilter != null && unlockFilter != undefined && unlockFilter.length > 0) {
                    unlockcount = unlockFilter[0].Count;
                }

            }
            var d = new Date(uniqueDate[i]);

            arMonth.push(d.getDate() + " " + d.getShortMonthName());
            arValue.push(resetcount);
            arDisValue.push(unlockcount)

        }

        var dom = document.getElementById("dvChartGsdActivityByDay");
        var myChart = echarts.init(dom);
        var app = {};
        option = null;
        option = {
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['Reset', 'Unlock']
            },
            toolbox: {
                show: true,
                itemGap: 5,
                right: 15,
                feature: {
                    dataView: { show: false, readOnly: false },
                    magicType: { show: true, type: ['line', 'bar'], title: ['line chart', 'bar chart'] },
                    restore: { show: true, title: 'restore' },
                    saveAsImage: { show: true, title: 'download' }
                }
            },
            calculable: true,
            xAxis: [
                {
                    type: 'category',
                    data: arMonth,
                    axisLabel: {
                        show: true,
                        interval: 0,
                        //rotate: 20,
                        margin:20,
                    },
                }
            ],
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: [
                {
                    name: 'Reset',
                    type: 'bar',
                    data: arValue,
                    label: {
                        normal: {
                            show: true,
                            position: 'insideTop'
                        }
                    },
                    itemStyle: {
                        normal: {
                            color: '#9A5E7E',
                        }
                    },
                   
                },
                {
                    name: 'Unlock',
                    type: 'bar',
                    data: arDisValue,
                    label: {
                        normal: {
                            show: true,
                            position: 'insideTop'
                        }
                    },
                    itemStyle: {
                        normal: {
                            color: '#BBACB6',
                        }
                    },
                }
            ]
        };
        ;
        if (option && typeof option === "object") {
            myChart.setOption(option, true);
        }

        $("#dvcontainer4").loading('stop');
    },
    getFrequentCallers: function () {
        $("#dvcontainer3").loading();
        var context = ['GetFrequentCallers', gsddashboard.defaults.userId];
        CoreREST.get(context, null, function (response) {
            var data = JSON.parse(response);
            gsddashboard.bindFrequentCallers(data);
        }, function (error) { $("#dvcontainer3").loading('stop'); });
    },
    bindFrequentCallers: function (data) {
        if (data == null || data == "" || data.Table.length == 0) {
            $("#dvChartFrequentCallers").html(gsddashboard.defaults.noDataFound);
            $("#dvcontainer3").loading('stop');
            return false;
        }

        //data preparation

        var arUser = new Array();
        var arReset = new Array();
        var arUnlock = new Array();
        for (var i = 0; i < data.Table.length; i++) {
            var userid = data.Table[i].userid;
            var resetcount = 0;
            var unlockcount = 0;

            var filterjson = gsddashboard.find_in_object(data.Table1, { userid: userid })
            if (filterjson != null && filterjson != undefined && filterjson.length > 0) {
                var resetFilter = gsddashboard.find_in_object(filterjson, { activity: "Reset_Password" })

                if (resetFilter != null && resetFilter != undefined && resetFilter.length > 0) {
                    resetcount = resetFilter[0].Count;
                }
                var unlockFilter = gsddashboard.find_in_object(filterjson, { activity: "Unlock_Account" })

                if (unlockFilter != null && unlockFilter != undefined && unlockFilter.length > 0) {
                    unlockcount = unlockFilter[0].Count;
                }

            }

            arUser.push(userid);
            arReset.push(resetcount);
            arUnlock.push(unlockcount)
        }

        var dom = document.getElementById("dvChartFrequentCallers");
        var myChart = echarts.init(dom);
        var app = {};
        option = null;
        option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'shadow'        //'line' | 'shadow'
                }
            },
            legend: {
                data: ['Reset', 'Unlock']
            },
            toolbox: {
                show: true,
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
                type: 'value',
                
            },
            yAxis: {
                type: 'category',
                
                data: arUser,
                silent: false,
                triggerEvent: {
                    // Component type: xAxis, yAxis, radiusAxis, angleAxis
                    // Each of which has an attribute for index, e.g., xAxisIndex for xAxis
                    componentType: 'yAxis',
                    // Value on axis before being formatted.
                    // Click on value label to trigger event.
                    value: '',
                    // Name of axis.
                    // Click on laben name to trigger event.
                    name: ''
                },
                axisLabel: {
                    textStyle: {
                        color: function (value, index) {
                            var isregisterfilter = gsddashboard.find_in_object(data.Table, { userid: value })
                            if (isregisterfilter == null || isregisterfilter[0].isregistered == "0") {
                                return 'gray';
                            }
                            else {
                                return 'green';
                            }
                            //return value >= 0 ? 'green' : 'red';
                        }
                    }
                }
            },
            series: [
                {
                    name: 'Reset',
                    type: 'bar',
                    stack: 'FreqCaller',
                    label: {
                        normal: {
                            show: true,
                            position: 'inside'
                        }
                    },
                    itemStyle: {
                        normal: {
                            color: '#EA9F82',
                        }
                    },
                    barWidth: '23px',
                    data: arReset
                },
                {
                    name: 'Unlock',
                    type: 'bar',
                    stack: 'FreqCaller',
                    label: {
                        normal: {
                            show: true,
                            position: 'inside'
                        }
                    },
                    itemStyle: {
                        normal: {
                            color: '#DA5C53',
                        }
                    },
                    barWidth: '23px',
                    data: arUnlock
                },


            ]
        };;

        if (option && typeof option === "object") {
            myChart.setOption(option, true);
        }

        $("#dvcontainer3").loading('stop');

        myChart.on('click', function (params) {
            // Make sure event from target axis
            if (params.componentType === 'yAxis') {
                // params.value is the axis label before formatted
                var page = gsddashboard.defaults.pagename;
                var today = new Date();
                var curr_hours = today.getHours();
                var curr_mins = today.getMinutes();              
                var priorDate = new Date();
                priorDate.setDate(priorDate.getDate() - 30)
                var endDate = today.getFullYear() + "/" + (today.getMonth() + 1) + "/" + today.getDate() + "/" + curr_hours + "/" + curr_mins;
                var startDate = priorDate.getFullYear() + "/" + (priorDate.getMonth() + 1) + "/" + priorDate.getDate();
                var mode = gsddashboard.defaults.mode;

                var hky = $("#hdky").val();
                var key = CryptoJS.enc.Utf8.parse(hky);
                var iv = CryptoJS.enc.Utf8.parse(hky);
                var encryptedValue = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(page + "|" + params.value + "|" + startDate + "|" + endDate + "|" + mode), key,
                    {
                        keySize: 128 / 8,
                        iv: iv,
                        mode: CryptoJS.mode.CBC,
                        padding: CryptoJS.pad.Pkcs7
                    });
                window.location.href = "../Reports/EmployeeSearch?value=" + encodeURIComponent(encryptedValue) + "";
                //alert(params.value);
            }
        });

    },
    find_in_object: function (my_object, my_criteria) {
        return my_object.filter(function (obj) {
            return Object.keys(my_criteria).every(function (c) {
                return obj[c] == my_criteria[c];
            });
        });
    },
    downloadaccountlockbymnthCsv: function () {
        $("#UserId").val(gsddashboard.defaults.userId);
        $("#csvform").submit();
    },
}