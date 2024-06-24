var resetPasswordOtp = {
    defaults: {
        defaultLang: 'en-US',
        PlsEnterOtp: '',
        InvalidOTP: '',
        AttemptCount: 0,
        IsOtpAttemptExceed: 0,
        OTPResendMsg: '',
        OTPMinMsg: '',
    },
    init: function () {
        $('#ResetPasswordOTP-form').fadeIn(layout1.defaults.pageloadType);
        $('#ResetPasswordOTP-form').fadeIn(layout1.defaults.pageEffectTime);

        var validate = $("#ResetPasswordOTP-form").validate({
            debug: false,
            onkeyup: false,
            onsubmit: false,
            onblur: true,
            rules: {
                txtOtp: {
                    required: true,
                    maxlength: 6,
                    minlength: 6,
                },
            },
            messages: {
                txtOtp: {
                    required: resetPasswordOtp.defaults.PlsEnterOtp,
                    maxlength: resetPasswordOtp.defaults.OTPMinMsg,
                    minlength: resetPasswordOtp.defaults.OTPMinMsg,
                },
            }
        });

        $("#txtOtp").keypress(function (e) {
            //if the letter is not digit then display error and don't type anything
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                return false;
            }
        });

        $("#btnResetOtp").on("click", function (e) {
            var isValid = $("#ResetPasswordOTP-form").valid();
            if (!isValid) {
                return;
                e.preventDefault();
            }

            document.forms["ResetPasswordOTP-form"].submit();
        });

        $("#btnResetOtpPrevious").on("click", function (e) {
            window.location.href = '/ForgotPassword/AuthenticationType'
        });


        $("#aResetPassResendOTP").on("click", function (e) {
            document.forms["form-resendOTP"].submit();
        });

        if (resetPasswordOtp.defaults.IsOtpAttemptExceed == 1) {
            $("#pOtpWait").hide();
            $("#pOtpSecQuestion").show();
        }

        $("#aSecQueResetPassword").on("click", function (e) {
            window.location.href = '/ForgotPassword/SecurityQuestions'
        });

        if ($.trim($("#lblMessage").html()) != "") {
            $("#pOtpWait").hide();
        }

        layout1.defaults.defaultLang = resetPasswordOtp.defaults.defaultLang;
        layout1.init();
    },
    timeisUp: function () {
        //Code to be executed when timer expires.
        $("#aResetPassResendOTP").show();
        $("#spnOtpReceiveMsg").html(resetPasswordOtp.defaults.OTPResendMsg)
        $("#seconds_timer").hide();
    }
}