var userRegister = {
    defaults: {
        defaultLang: 'en-US',
        userQuestionCount: 5,
        totalQuestions: 6,
        UniqueQuestionValidation: '',
        EmptyAnswerwError: '',
        Message: '',
        IsRegistered: '',
        Question: '',
        Answer: '',
    },
    init: function () {
        $('#register-form').fadeIn(layout1.defaults.pageloadType);
        $('#register-form').fadeIn(layout1.defaults.pageEffectTime);

        var message = userRegister.defaults.Message;

        if (message != "" && message != null) {
            var isRegistered = userRegister.defaults.IsRegistered;
            if (message != "" && isRegistered != "" && isRegistered.toLowerCase() == "true") {
                $("#modal-success").modal({
                    show: true,
                    backdrop: 'static',
                    keyboard: false
                });
            }
            else if (message != "" && isRegistered != "" && isRegistered.toLowerCase() == "false") {
                $("#modal-fail").modal({
                    show: true, backdrop: 'static',
                    keyboard: false,
                    backdrop: 'static',
                    keyboard: false
                });
            }
        }

        layout1.defaults.defaultLang = userRegister.defaults.defaultLang;
        layout1.init();

        $("#btnRegister").click(function () {
            var isQuesionAnswered = userRegister.fnvalidateAnswer();

            if (isQuesionAnswered) {
                var isQuestionsUnique = userRegister.validatedropdownSelection();

                if (!isQuestionsUnique) {
                    $("#lblErrMessage").html(userRegister.defaults.UniqueQuestionValidation);
                    return false;
                }

                userRegister.showConfirmBox();

                //document.forms["register-form"].submit();
            }
        });
    },
    validatedropdownSelection: function () {
        var arr = new Array();
        for (var i = 1; i <= userRegister.defaults.userQuestionCount; i++) {
            if ($.inArray($("#Que_" + i).val(), arr) == -1) {
                arr.push($("#Que_" + i).val())
                $("#Que_" + i).css("border", "1px solid black");
            }
            else {
                $("#Que_" + i).css("border", "1px solid red");
                return false;
            }
        }
        return true;
    },
    fnvalidateAnswer: function () {
        var result = true;
        for (var i = 0; i < userRegister.defaults.userQuestionCount; i++) {
            if ($("#txtQueAns_" + i).val() == "") {
                $("#lblErrMessage_" + i).html(userRegister.defaults.EmptyAnswerwError);
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

            //var reg = /<(.|\n)*?>/g;
            //if (reg.test($("#txtQueAns_" + i).val()) == true) {
            //    $("#lblErrMessage_" + i).html("Please enter valid answer");
            //    result = false;
            //}
            //else {
            //    $("#lblErrMessage_" + i).html("");
            //}
        }
        return result;
    },
    showConfirmBox: function () {

        var content = '<table class="table table-bordered">';
        content += '<thead class="info-cnf-table">';
        content += '<tr>';
        content += '<th>#</th>';
        content += '<th>' + userRegister.defaults.Question + '</th>';
        content += '<th>' + userRegister.defaults.Answer + '</th>';
        content += '</tr>';
        content += '</thead>';
        content += '<tbody>';

        for (var i = 0; i < userRegister.defaults.userQuestionCount; i++) {
            var question = $("#Que_" + i + " :selected").text()
            var answer = $("#txtQueAns_" + i).val();

            content += '<tr>';
            content += '<th scope="row">' + (i + 1) + '</th>';
            content += '<td>' + question + '</td>';
            content += '<td>' + answer + '</td>';
            content += '</tr>';
        }
        content += '</tbody>';
        content += '</table>';

        $("#dvConfirmContent").html(content);

        $("#modal-confirm").modal({
            show: true, backdrop: 'static',
            keyboard: false,
            backdrop: 'static',
            keyboard: false
        });
    },
    fnSubmit: function () {
        document.forms["register-form"].submit();
    }
}