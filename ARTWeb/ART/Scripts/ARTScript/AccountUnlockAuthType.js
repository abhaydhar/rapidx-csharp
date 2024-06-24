var accountUnlockAuthType = {
    defaults: {
        defaultLang: 'en-US',
        otpDisableMsg: '',
        isOtpEnabled: 'N',
        IsOtpAttemptExceed: 0,
        OTPMaxAttemptMsg: '',
        isEventSourceLink: '',
        enabledEventSource: ['CCH1WPADFS03', 'CCH1WPTMG2']
    },
    init: function () {
        debugger;
        if (accountUnlockAuthType.defaults.isEventSourceLink == 'True') {           
            $("#dvUserLockingSource").show();
        }
        else
        {
            $("#dvUserLockingSource").hide();
        }

        $('#dvUnlockAccAuthType').fadeIn(layout1.defaults.pageloadType);
        $('#dvUnlockAccAuthType').fadeIn(layout1.defaults.pageEffectTime);

        if (accountUnlockAuthType.defaults.isOtpEnabled == "N") {
            $("#otp-form *").attr("disabled", "disabled").off('click');
            $("#radiobtn-OTP").addClass("radio-disabled");
            $("label[for='radiobtn-OTP']").css("opacity", "0.5");
            $('#radiobtn-security').trigger('click');
            $('#lblMessage').text(decodeHtml(accountUnlockAuthType.defaults.otpDisableMsg))
        }
        if (accountUnlockAuthType.defaults.IsOtpAttemptExceed == "1") {
            $("#otp-form *").attr("disabled", "disabled").off('click');
            $("#radiobtn-OTP").addClass("radio-disabled");
            $("label[for='radiobtn-OTP']").css("opacity", "0.5");
            $('#radiobtn-security').trigger('click');
            $('#dvOtpMain').addClass('disabled-sec');
            $('#lblMessage').text(decodeHtml(accountUnlockAuthType.defaults.OTPMaxAttemptMsg))
        }
        layout1.defaults.defaultLang = accountUnlockAuthType.defaults.defaultLang;
        layout1.init();
    },
    lockSourceclick: function () {
        accountUnlockAuthType.getUserLockSource();
    },
    getUserLockSource: function () {
        var context = ['Home', 'GetUserEventsDetails'];
        CoreREST.get(context, null, function (data) {
            accountUnlockAuthType.bindLockSource(data);
        }, function (e) { });
    },

    bindLockSource: function (data) {
        debugger;

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
                            BrowserName =' Browser:'+ arr[1];
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
        $("#modal-locksource").modal({
            show: true, backdrop: 'static',
            keyboard: false,
            backdrop: 'static',
            keyboard: false
        });
    },

}