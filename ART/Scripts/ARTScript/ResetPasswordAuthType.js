var resetPasswordAuthType = {
    defaults: {
        defaultLang: 'en-US',
        otpDisableMsg: '',
        isOtpEnabled: 'N',
        IsOtpAttemptExceed: 0,
        OTPMaxAttemptMsg:'',
    },
    init: function () {
        $('#dvResetPasswordAuthType').fadeIn(layout1.defaults.pageloadType);
        $('#dvResetPasswordAuthType').fadeIn(layout1.defaults.pageEffectTime);

        if (resetPasswordAuthType.defaults.isOtpEnabled == "N") {
            $("#otp-form *").attr("disabled", "disabled").off('click');
            $("#radiobtn-OTP").addClass("radio-disabled");
            $("label[for='radiobtn-OTP']").css("opacity", "0.5");
            $('#radiobtn-security').trigger('click');
            $('#lblMessage').text(decodeHtml(resetPasswordAuthType.defaults.otpDisableMsg))
        }
        if (resetPasswordAuthType.defaults.IsOtpAttemptExceed == "1") {
            $("#otp-form *").attr("disabled", "disabled").off('click');
            $("#radiobtn-OTP").addClass("radio-disabled");
            $("label[for='radiobtn-OTP']").css("opacity", "0.5");
            $('#radiobtn-security').trigger('click');
            $('#dvOtpMain').addClass('disabled-sec');
            $('#lblMessage').text(decodeHtml(resetPasswordAuthType.defaults.OTPMaxAttemptMsg))
        }

        layout1.defaults.defaultLang = resetPasswordAuthType.defaults.defaultLang;
        layout1.init();
    },
}