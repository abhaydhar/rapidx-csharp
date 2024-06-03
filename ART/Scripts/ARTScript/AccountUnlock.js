var accountUnlock = {
    defaults: {
        userQuestionCount: 3,
        defaultLang: 'en-US',
        Unlocked: '',
        message: '',
        EmptyAnswerwError: '',
        isOtpEnabled: 'N'
    },
    init: function () {
        $('#unlockAccount-form').fadeIn(layout1.defaults.pageloadType);
        $('#unlockAccount-form').fadeIn(layout1.defaults.pageEffectTime);

        var Unlocked = accountUnlock.defaults.Unlocked;
        var message = accountUnlock.defaults.message;

        if (Unlocked != null && Unlocked != "") {
            if (message != "" && Unlocked.toLowerCase() == "true") {
                $("#modal-success").modal({
                    show: true, backdrop: 'static',
                    keyboard: false
                });
                $("#lblMessage").html('');
            }
            else if (message != "" && Unlocked.toLowerCase() == "false") {
                $("#modal-fail").modal({
                    show: true, backdrop: 'static',
                    keyboard: false
                });
                $("#lblMessage").html('');
            }
        }

        accountUnlock.getUserQuestionsToAnswer();

        //$("#btnAccountUnlockCancel").on("click", function (e) {
        //    if (resetPassword.defaults.isOtpEnabled == "Y")
        //        window.location.href = '/UnlockAccount/AuthenticationType'
        //    else
        //        window.location.href = '/UnlockAccount'
        //});

        $("#btnUnlockAccount").on("click", function (e) {
            var result = accountUnlock.fnvalidateAnswer()
            if (result)
                $('#unlockAccount-form').submit();
        });

        layout1.defaults.defaultLang = accountUnlock.defaults.defaultLang;
        layout1.init();

    },
    getUserQuestionsToAnswer: function () {
        var context = ['User', 'GetQuestionsForUserToAnswer'];
        CoreREST.get(context, null, function (data) {
            accountUnlock.bindUserQuestions(data);
        }, function (e) { });
    },
    bindUserQuestions: function (questions) {
        for (var i = 1; i <= accountUnlock.defaults.userQuestionCount; i++) {
            //$("#sQue_" + i).append('<option value="' + questions[i - 1].question_id + '">' + questions[i - 1].question_text + '</option>');
            $("#sQue_" + i).text(questions[i - 1].question_text)
            $("#Que_" + i).val(questions[i - 1].question_id);
        }
    },
    fnvalidateAnswer: function () {
        var result = true;
        for (var i = 1; i <= accountUnlock.defaults.userQuestionCount; i++) {
            if ($.trim($("#txtQueAns_" + i).val()) == "") {
                $("#lblErrMessage_" + i).html(accountUnlock.defaults.EmptyAnswerwError);
                result = false;
            }
            else {
                var reg = /<(.|\n)*?>/g;
                if (reg.test($("#txtQueAns_" + i).val()) == true) {
                    $("#lblErrMessage_" + i).html("Please enter valid answer");
                    $("#txtQueAns_" + i).val('');
                    result = false;
                    continue;
                }
                else {
                    $("#lblErrMessage_" + i).html("");
                }

                if (fnValidateUserAnswer($("#txtQueAns_" + i).val())) {
                    $("#lblErrMessage_" + i).html("Please enter valid answer");
                    result = false;
                    continue;
                }
                else {
                    $("#lblErrMessage_" + i).html("");
                }
                //$("#lblErrMessage_" + i).html("");
            }
        }
        return result;
    },
    userSettingsclick: function () {
        accountUnlock.getUserSettings();
    },
    getUserSettings: function () {
        var context = ['User', 'CallUserSettings'];
        CoreREST.get(context, null, function () {          
        }, function (e) { });
    },
}