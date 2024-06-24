var resetPasswordLogin = {
    defaults: {
        defaultLang: 'en-US',
        enterUserId: '',
        InvalidUserId: '',
    },
    init: function () {
        $('#resetform1').fadeIn(layout1.defaults.pageloadType);
        $('#resetform1').fadeIn(layout1.defaults.pageEffectTime);

        $.validator.addMethod("regex", function (value, element, regexp) {
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        }, "Please check your input.");

        var validate = $("#resetform1").validate({
            debug: true,
            onkeyup: false,
            onsubmit: false,
            onblur: true,
            rules: {
                txtUserId: {
                    required: true,
                    regex: "^[a-zA-Z0-9@_-]*$",
                },
            },
            messages: {
                txtUserId: {
                    required: resetPasswordLogin.defaults.enterUserId,
                    regex: resetPasswordLogin.defaults.InvalidUserId
                },
            }
        });

        $("#btnResetLogin").on("click", function (e) {
            if ($.trim($("#CaptchaCode").val()) == "") {
                $('#status').attr('class', 'error');
                $('#status').text('Please enter the Captcha');
                //return false;
            }
            else {
                $('#status').text('')
            }

            var isValid = $("#resetform1").valid();
            if (!isValid) {
                return;
                e.preventDefault();
            }

            if ($.trim($("#CaptchaCode").val()) == "") {
                $('#status').attr('class', 'error');
                $('#status').text('Please enter the Captcha');
                return false;
            }

            botDetect.validateBotDetect(function (result) {
                if (true === result) {
                    $("#resetform1").submit();
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
        });

        $(document).keypress(function (e) {
            if (e.which == 13) {
                $('#btnResetLogin').trigger('click');
            }
        });
        $("#txtUserId").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#btnResetLogin").click();
            }
        });

        layout1.defaults.defaultLang = resetPasswordLogin.defaults.defaultLang;
        layout1.init();
    },
}