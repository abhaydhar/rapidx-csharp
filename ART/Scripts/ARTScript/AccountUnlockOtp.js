var accountUnlockOtp = {
    defaults: {
        defaultLang: 'en-US',
        Unlocked: '',
        message: '',
        PlsEnterOtp: '',
        InvalidOTP: '',
        AttemptCount: 0,
        IsOtpAttemptExceed: 0,
        OTPResendMsg: '',
        OTPMinMsg: '',
    },
    init: function () {
        $('#AccountUnlockOTP-form').fadeIn(layout1.defaults.pageloadType);
        $('#AccountUnlockOTP-form').fadeIn(layout1.defaults.pageEffectTime);

        var Unlocked = accountUnlockOtp.defaults.Unlocked;
        var message = accountUnlockOtp.defaults.message;

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

        var validate = $("#AccountUnlockOTP-form").validate({
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
                    required: accountUnlockOtp.defaults.PlsEnterOtp,
                    maxlength: accountUnlockOtp.defaults.OTPMinMsg,
                    minlength: accountUnlockOtp.defaults.OTPMinMsg,
                },
            }
        });
        //$("#btnAccUnlockOtpPrevious").on("click", function (e) {
        //    window.location.href = '/UnlockAccount/AuthenticationType'
        //});
        $("#txtOtp").keypress(function (e) {
            //if the letter is not digit then display error and don't type anything
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                return false;
            }
        });

        $("#btnAccUnlockOtp").on("click", function (e) {
            var isValid = $("#AccountUnlockOTP-form").valid();
            if (!isValid) {
                return;
                e.preventDefault();
            }

            document.forms["AccountUnlockOTP-form"].submit();
        });


        $("#aUnlockAccResendOTP").on("click", function (e) {
            document.forms["form-accUnlockresendOTP"].submit();
        });

        if (accountUnlockOtp.defaults.IsOtpAttemptExceed == 1) {
            $("#pOtpWait").hide();
            $("#pOtpSecQuestion").show();
        }

        $("#aSecQueUnlockAcc").on("click", function (e) {
            window.location.href = '/UnlockAccount/SecurityQuestions'
        });

        if ($.trim($("#lblMessage").html()) != "") {
            $("#pOtpWait").hide();
        }

        layout1.defaults.defaultLang = accountUnlockOtp.defaults.defaultLang;
        layout1.init();
    },
    timeisUp: function () {
        //Code to be executed when timer expires.
        $("#aUnlockAccResendOTP").show();
        $("#spnOtpReceiveMsg").html(accountUnlockOtp.defaults.OTPResendMsg)
        $("#seconds_timer").hide();
    }
}