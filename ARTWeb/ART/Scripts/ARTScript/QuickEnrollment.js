var QuickEnrollment = {
    defaults: {
        defaultLang: 'en-US',
        enterUserKey: '',
        userQuestionCount: 5,
        totalQuestions: 6,
        UniqueQuestionValidation: '',
        EmptyAnswerwError: '',
        Message: '',
        IsRegistered: '',
        Question: '',
        Answer: ''
    },
    init: function () {
        $('#register-form').fadeIn(layout1.defaults.pageloadType);
        $('#register-form').fadeIn(layout1.defaults.pageEffectTime);
        validate = $("#register-form").validate({
            debug: true,
            onkeyup: false,
            onsubmit: false,
            onblur: true,
            rules: {
                txtkey: {
                    required: true,
                    regex: "^[a-zA-Z0-9@_-]*$",                   
                    minlength: 7
                }
            },
            messages: {
                txtkey: {
                    required: "Please enter the User Key",
                    regex: "Invalid Key Id",                
                    minlength: "Key should be minimum of 6 characters"
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

        $("#btnLogin").click(function () {
            $("#hbtnLogin").val($("#btnLogin").html())
            $("#txtkey").val();
            var key = $("#txtkey").val();
            var isValid = true;
            if (key == "")
                isValid = $("#register-form").valid();
            else
                $('#lblMessage').text('');

            if ($.trim($("#CaptchaCode").val()) == "") {
                $('#status').attr('class', 'error');
                $('#status').text('Please enter the Captcha');
                return false;
            }
            else { $('#status').text(''); }

            botDetect.validateBotDetect(function (result) {
                if (true === result) {
                    $("#register-form").submit();
                }
                else {
                    $('#status').attr('class', 'error');
                    $('#status').text('Invalid Captcha');
                    return false;
                }
            }, function (error) {
                $('#status').attr('class', 'error');
                $('#status').text('Invalid Captcha');
                return false;
            })

            if (isValid && result) {
                $("#btnAction").val("Login");
                $("#div-EnrollQuestion").show();
                $("#register-form").submit();
            }

        });


        var message = QuickEnrollment.defaults.Message;

        if (message != "" && message != null) {
            var isRegistered = QuickEnrollment.defaults.IsRegistered;
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

        layout1.defaults.defaultLang = QuickEnrollment.defaults.defaultLang;
        layout1.init();

        $("#btnRegister").click(function () {
            var isQuesionAnswered = QuickEnrollment.fnvalidateAnswer();

            if (isQuesionAnswered) {
                var isQuestionsUnique = QuickEnrollment.validatedropdownSelection();

                if (!isQuestionsUnique) {
                    $("#lblErrMessage").html(QuickEnrollment.defaults.UniqueQuestionValidation);
                    return false;
                }

                QuickEnrollment.showConfirmBox();

                //document.forms["register-form"].submit();
            }
        });
    },
    validatedropdownSelection: function () {
        var arr = new Array();
        for (var i = 1; i <= QuickEnrollment.defaults.userQuestionCount; i++) {
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
        for (var i = 0; i < QuickEnrollment.defaults.userQuestionCount; i++) {
            if ($("#txtQueAns_" + i).val() == "") {
                $("#lblErrMessage_" + i).html(QuickEnrollment.defaults.EmptyAnswerwError);
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
        content += '<th>' + QuickEnrollment.defaults.Question + '</th>';
        content += '<th>' + QuickEnrollment.defaults.Answer + '</th>';
        content += '</tr>';
        content += '</thead>';
        content += '<tbody>';

        for (var i = 0; i < QuickEnrollment.defaults.userQuestionCount; i++) {
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