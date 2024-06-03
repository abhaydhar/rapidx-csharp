var accountUnlockLogin = {
    defaults: {
        defaultLang: 'en-US',
        enterUserId: '',
        InvalidUserId: '',
    },
    init: function () {
        $('#unlockaccform1-form').fadeIn(layout1.defaults.pageloadType);
        $('#unlockaccform1-form').fadeIn(layout1.defaults.pageEffectTime);

        layout1.defaults.defaultLang = accountUnlockLogin.defaults.defaultLang;
        layout1.init();

        $.validator.addMethod("regex", function (value, element, regexp) {
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        }, "Please check your input.");

        var validate = $("#unlockaccform1-form").validate({
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
                    required: accountUnlockLogin.defaults.enterUserId,
                    regex: accountUnlockLogin.defaults.InvalidUserId
                },
            }
        });

        $("#btnUnlocklogin").on("click", function (e) {

            if ($.trim($("#CaptchaCode").val()) == "") {
                $('#status').attr('class', 'error');
                $('#status').text('Please enter the Captcha');
                //return false;
            }
            else {
                $('#status').text('')
            }
            var isValid = $("#unlockaccform1-form").valid();
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
                    $("#unlockaccform1-form").submit();
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
                $('#btnUnlocklogin').trigger('click');
            }
        });
        $("#txtUserId").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#btnUnlocklogin").click();
            }
        });

        //accountUnlockLogin.enableCaptcha();
    },
    //enableCaptcha: function () {
    //    $('#btnUnlocklogin').click(function () {
    //        var isValid = $("#unlockaccform1-form").valid();
    //        if (!isValid) {
    //            return;
    //            e.preventDefault();
    //        }

    //        $captcha = $('.g-recaptcha');
    //        response = grecaptcha.getResponse();

    //        if (response.length === 0) {
    //            $('.msg-error').text("Please validate the captcha");
    //            if (!$captcha.hasClass("error")) {
    //                $captcha.addClass("error");
    //            }
    //        } else {
    //            $('.msg-error').text('');
    //            $captcha.removeClass("error");
    //            // form submit return true
    //            //return true;
    //            $("#unlockaccform1-form").submit();
    //        }
    //    });
    //}
}