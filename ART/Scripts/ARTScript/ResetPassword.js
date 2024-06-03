var resetPassword = {
    defaults: {
        userQuestionCount: 3,
        defaultLang: 'en-Us',
        emptyErrorMsg: '',
        isOtpEnabled: 'N'
    },
    init: function () {
        $('#reset-form').fadeIn(layout1.defaults.pageloadType);
        $('#reset-form').fadeIn(layout1.defaults.pageEffectTime);

        resetPassword.getUserQuestionsToAnswer();

        $("#btnResetPassword").click(function () {
            var isQuesionAnswered = resetPassword.fnvalidateAnswer();

            if (isQuesionAnswered) {
                document.forms["reset-form"].submit();
            }
        });

        $("#btnCancelAnswer").on("click", function (e) {
            if (resetPassword.defaults.isOtpEnabled == "Y")
                window.location.href = '/ForgotPassword/AuthenticationType'
            else
                window.location.href = '/ForgotPassword'
        });

        layout1.defaults.defaultLang = resetPassword.defaults.defaultLang;
        layout1.init();

    },
    getUserQuestionsToAnswer: function () {
        var context = ['User', 'GetQuestionsForUserToAnswer'];
        CoreREST.get(context, null, function (data) {
            resetPassword.bindUserQuestions(data);
        }, function (e) { });
    },
    bindUserQuestions: function (questions) {
        for (var i = 1; i <= resetPassword.defaults.userQuestionCount; i++) {
            //$("#sQue_" + i).append('<option value="' + questions[i - 1].question_id + '">' + questions[i - 1].question_text + '</option>');
            $("#sQue_" + i).text(questions[i - 1].question_text)
            $("#Que_" + i).val(questions[i - 1].question_id);
        }
    },
    fnvalidateAnswer: function () {
        var result = true;
        for (var i = 1; i <= resetPassword.defaults.userQuestionCount; i++) {
            if ($.trim($("#txtQueAns_" + i).val()) == "") {
                $("#lblErrMessage_" + i).html(resetPassword.defaults.emptyErrorMsg);
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
    }
}