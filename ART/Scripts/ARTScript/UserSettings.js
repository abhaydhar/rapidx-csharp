var usersettings = {
    defaults: {
        defaultLang: 'en-US',
        MobilNumberRegisterValidation: ''
    },
    init: function () {
        usersettings.getUserLockSource();
        $("#passwordRemainder").on("change", function (e) {
            $("#userSettings-form").submit();           
        });
        $("#MobileNoChange").on("change", function (e) {
            usersettings.mobileNoChange();
        });
        $("#btnUserSettingsPrev").on("click", function (e) {
            parent.history.back();
        });
    },
    mobileNoChange: function () {
        var r = confirm(decodeHtml(usersettings.defaults.MobilNumberRegisterValidation));
        if (r == true) {
            return false;
        }
        else {
            window.location.href = '/MobileNoChange'

        }
    },
    changeUserPasswordExpiry: function () {
        var context = ['User', 'UserSettings'];
        CoreREST.get(context, null, function (data) {           
        }, function (e) { });
    },
    getUserLockSource: function () {
        var context = ['Home', 'GetUserEventsDetails'];
        CoreREST.get(context, null, function (data) {
            usersettings.bindLockSource(data);
        }, function (e) { });
    },
    bindLockSource: function (data) {
        var data4740 = JSON.parse(data);
        var events = data4740.Table1;

        //var dataevent = JSON.parse(data.Table1);
        if (data4740.Table.length > 0) {

            var content = '<div class="table-responsive"><table class="table table-bordered t-12" style="font-size:12px">';
            content += '<thead class="info-cnf-table">';
            content += '<tr>';
            content += '<th>#</th>';
            content += '<th>Date time</th>';
            content += '<th>Source</th>';
            content += '<th>Device</th>';
            content += '<th>Trace</th>';
            content += '</tr>';
            content += '</thead>';
            content += '<tbody>';

            for (var i = 0; i < data4740.Table.length; i++) {

                content += '<tr>';
                content += '<th scope="row">' + (i + 1) + '</th>';
                content += '<td>' + custom.fnFormatDate(data4740.Table[i].EventTMP) + '</td>';
                content += '<td>' + data4740.Table[i].EventSource + '</td>';

                var found_events = events.filter(function (item) {
                    return item.EventID == data4740.Table[i].EventID;
                });

                if (found_events.length > 0 && $.inArray(data4740.Table[i].EventSource, accountUnlockAuthType.defaults.enabledEventSource) != -1) {
                    debugger;
                    for (var j = 0; j < found_events.length; j++) {
                        var OsName = '';
                        var BrowserName = '';
                        var Device = '';
                        if (found_events[j].Browser.length > 1) {
                            var myString = found_events[j].Browser;
                            var arr = myString.split(':');
                            BrowserName = ' Browser:' + arr[1];
                        }

                        if (found_events[j].OS.indexOf('Windows') >= 0) {
                            OsName = "OS : Windows";
                            OsName = OsName != undefined ? OsName : 'NA';
                        }
                        else {
                            OsName = 'OS:' + found_events[j].OS;
                            OsName = OsName != undefined ? OsName : 'NA';
                        }
                        Device = found_events[j].Device != undefined ? found_events[j].Device : 'NA';
                        if (found_events[j].useragent == 'iPhone')
                            Device = "iphone";

                        content += '<td>  ' + Device + ' </td>';
                        content += '<td>' + OsName + ' , ' + BrowserName + '</td>';

                    }
                }
                else {
                    content += '<td>NA </td>';
                    content += '<td>NA</td>';
                }

                content += '</tr>';
            }
            content += '</tbody>';
            content += '</table></div>';

            $("#dvLockingSource").html(content);
        }
        else {
            $("#dvLockingSource").html("- No lock out information found -");
        }

    }   
}