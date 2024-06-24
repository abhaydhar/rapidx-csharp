var registerUserInfo = {
    defaults: {
        defaultLang: 'en-US',
        EnterDOB: '',
        IncorrectDOBFormat: '',
        EnterMobileNumber: '',
        InvalidMobileNumber: '',
        InvalidDOB: '',
        IsOTPSent: "0",
        OTPResendMsg: '',
        OTPMinMsg: '',
        PlsEnterOtp: '',
        IsOtpAttemptExceed: 0,
        OTPMaxAttemptMsg: '',
        IsValidOTP: '',
        mobileNumberMaxLength: 10,
        MaxAttempt: 3,
        MobilNumberRegisterValidation: '',
        DefaultCountryCode: '',
        DefaultMobileNumber: '',
        Is_OTP_Enabled: '',
        IS_DOB_Validation_Needed: '',
        OpenRegisterForOTP: ''
    },
    init: function () {
        $('#regUserInfo-form').fadeIn(layout1.defaults.pageloadType);
        $('#regUserInfo-form').fadeIn(layout1.defaults.pageEffectTime);

        var validate = $("#regUserInfo-form").validate({
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
                    required: registerUserInfo.defaults.PlsEnterOtp,
                    maxlength: registerUserInfo.defaults.OTPMinMsg,
                    minlength: registerUserInfo.defaults.OTPMinMsg,
                },
            }
        });

        if (registerUserInfo.defaults.IsOtpAttemptExceed == "1" && (registerUserInfo.defaults.IsOTPSent == "0" || registerUserInfo.defaults.IsOTPSent == "")) {
            $("#lblOTPExceedMsg").text(registerUserInfo.defaults.OTPMaxAttemptMsg);
            $("#register-otp").attr("disabled", "disabled");

        }

        if (registerUserInfo.defaults.IsOtpAttemptExceed == "1" && registerUserInfo.defaults.IsOTPSent == "1") {
            $("#pOtpWait").hide();
        }

        if (registerUserInfo.defaults.IsOTPSent == "1") {

            $('#register-otp').trigger("click");
            $("#btnSendOTP").css("display", "none");
            $("#txtMobileNumber").attr("disabled", "disabled");
            $('#drpCountryCode').attr("disabled", "disabled");

            $('html, body').animate({
                scrollTop: $("#btnSubmitUserInfo").offset().top
            }, 1000);
        }

        if (registerUserInfo.defaults.IsValidOTP == "1") {
            $("#dv-OTP-section").hide();
            $("#pOtpWait").hide();
            $('html, body').animate({
                scrollTop: $("#btnSubmitUserInfo").offset().top
            }, 1000);
        }

        $('#txtDOB').mask("00/00/0000", { placeholder: "dd/mm/yyyy" });

        $("#btnSubmitUserInfo").on("click", function (e) {
            if (registerUserInfo.fnCheckCompletion()) {
                $("#txtMobileNum").val($("#txtMobileNumber").val());
                $("#btnAction").val("Submit");
                var result = registerUserInfo.fnValidate()
                if (result)
                    $("#regUserInfo-form").submit();
            }
        });

        $("#btnSendOTP").on("click", function (e) {
            if (registerUserInfo.fnValidateMobileNumber()) {
                this.style.display = "none";
                $("#txtMobileNum").val($("#txtMobileNumber").val());
                $("#btnAction").val("SendOTP");
                $("#regUserInfo-form").submit();
            }
        });
        $("#btnValidateOTP").on("click", function (e) {
            var isValid = $("#regUserInfo-form").valid();
            if (!isValid) {
                return;
                e.preventDefault();
            }

            $("#txtMobileNum").val($("#txtMobileNumber").val());
            $("#btnAction").val("ValidateOTP");
            $("#regUserInfo-form").submit();
        });
        $("#aResetPassResendOTP").on("click", function (e) {
            $("#txtMobileNum").val($("#txtMobileNumber").val());
            $("#countryCode").val($('#drpCountryCode').val());
            $("#btnAction").val("ResendOTP");
            $("#regUserInfo-form").submit();
        });


        $("#btnUserRegisterPrev").on("click", function (e) {
            window.location.href = '/Enroll/Login'
        });

        $('#drpCountryCode').on('change', function () {
            var countryValue = this.value;
            var countryCode = countryValue.split('_')[0];
            var telephoneCode = countryValue.split('_')[1];
            var mobileNumLength = countryValue.split('_')[2];

            $("#txtMobileNumber").val('');
            $("#txtCountryCode").val(telephoneCode);
            registerUserInfo.defaults.mobileNumberMaxLength = mobileNumLength;
            $("#countryCode").val($('#drpCountryCode').val());

            $("#txtMobileNumber").attr("maxlength", registerUserInfo.defaults.mobileNumberMaxLength);

            //if (registerUserInfo.defaults.DefaultCountryCode == telephoneCode)
            //{
            //    $("#txtMobileNumber").val(registerUserInfo.defaults.DefaultMobileNumber);
            //}

        })

        if (registerUserInfo.defaults.Is_OTP_Enabled == "Y") {
            //$('#drpCountryCode').trigger("change");

            $("#countryCode").val($('#drpCountryCode').val());

            registerUserInfo.defaults.mobileNumberMaxLength = $("#drpCountryCode").val().split('_')[2];
            //registerUserInfo.defaults.DefaultMobileNumber = $("#txtMobileNumber").val();

            $("#txtCountryCode").val($("#drpCountryCode").val().split('_')[1]);
        }
        //registerUserInfo.defaults.DefaultCountryCode = $("#drpCountryCode").val().split('_')[1];

        $("#txtDOB").on("blur", function (e) {
            if ($.trim($("#txtDOB").val()) == "") {
                $("#lblMessageDOB").html(registerUserInfo.defaults.EnterDOB);
                return false;
            }
            else {
                $("#lblMessageDOB").html('');
            }
            if ($('#txtDOB').val().split('/').length < 3) {
                $("#lblMessageDOB").html(registerUserInfo.defaults.IncorrectDOBFormat);
                return false;
            }
            else { $("#lblMessageDOB").html(''); }

            var dateArr = $('#txtDOB').val().split('/');

            if (dateArr[0].length < 2 || dateArr[1].length < 2 || dateArr[2].length < 4) {
                $("#lblMessageDOB").html(registerUserInfo.defaults.IncorrectDOBFormat);
                return false;
            }
            else { $("#lblMessageDOB").html(''); }

            //if (Date.parse($("#txtDOB").val().split('/')[2] + "-" + $("#txtDOB").val().split('/')[1] + "-" + $("#txtDOB").val().split('/')[0])) {
            //    //Valid date
            //} else {
            //    $("#lblMessageDOB").html(registerUserInfo.defaults.InvalidDOB);
            //    return false;
            //}

            if (!registerUserInfo.validDate($("#txtDOB").val().split('/')[1] + "/" + $("#txtDOB").val().split('/')[0] + "/" + $("#txtDOB").val().split('/')[2])) {
                $("#lblMessageDOB").html(registerUserInfo.defaults.InvalidDOB);
                return false;
            }
        });
        $("#txtMobileNumber").on("blur", function (e) {
            $("#txtMobileNum").val($("#txtMobileNumber").val());
            if ($.trim($("#txtMobileNumber").val()) == "") {
                $("#lblMessageMobNum").html(registerUserInfo.defaults.EnterMobileNumber);
                return false;
            }
            else {
                $("#lblMessageMobNum").html('');
            }
            if (isNaN($("#txtMobileNumber").val()) || $("#txtMobileNumber").val().length != registerUserInfo.defaults.mobileNumberMaxLength) {
                $("#lblMessageMobNum").html(registerUserInfo.defaults.InvalidMobileNumber);
                return false;
            }
            else {
                $("#lblMessageMobNum").html('');
            }
        });

        $("#txtOtp").keypress(function (e) {
            //if the letter is not digit then display error and don't type anything
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                return false;
            }
        });
        $("#txtMobileNumber").keypress(function (e) {
            //if the letter is not digit then display error and don't type anything
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                return false;
            }
        });

        // to set the maximum numbers in the mobile number field
        $("#txtMobileNumber").attr("maxlength", registerUserInfo.defaults.mobileNumberMaxLength);

        if (registerUserInfo.defaults.OpenRegisterForOTP == "1") {
            $('#register-otp').trigger("click");
        }

        // Bind the last 10(depends on the country code) digits to the mobile number textbox
        var currentMobileNum = $("#txtMobileNumber").val();
        $("#txtMobileNumber").val(currentMobileNum.slice(registerUserInfo.defaults.mobileNumberMaxLength * -1));

        layout1.defaults.defaultLang = registerUserInfo.defaults.defaultLang;
        layout1.init();

    },
    fnValidate: function () {
        if (registerUserInfo.defaults.IS_DOB_Validation_Needed == "Y") {
            if ($.trim($("#txtDOB").val()) == "") {
                $("#lblMessageDOB").html(registerUserInfo.defaults.EnterDOB);
                return false;
            }
            else {
                $("#lblMessageDOB").html('');
            }
            if ($('#txtDOB').val().split('/').length < 3) {
                $("#lblMessageDOB").html(registerUserInfo.defaults.IncorrectDOBFormat);
                return false;
            }
            else { $("#lblMessageDOB").html(''); }

            var dateArr = $('#txtDOB').val().split('/');

            if (dateArr[0].length < 2 || dateArr[1].length < 2 || dateArr[2].length < 4) {
                $("#lblMessageDOB").html(registerUserInfo.defaults.IncorrectDOBFormat);
                return false;
            }
            else { $("#lblMessageDOB").html(''); }

            //if (Date.parse($("#txtDOB").val().split('/')[2] + "-" + $("#txtDOB").val().split('/')[1] + "-" + $("#txtDOB").val().split('/')[0])) {
            //    //Valid date
            //} else {
            //    $("#lblMessageDOB").html(registerUserInfo.defaults.InvalidDOB);
            //    return false;
            //}

            if (!registerUserInfo.validDate($("#txtDOB").val().split('/')[1] + "/" + $("#txtDOB").val().split('/')[0] + "/" + $("#txtDOB").val().split('/')[2])) {
                $("#lblMessageDOB").html(registerUserInfo.defaults.InvalidDOB);
                return false;
            }
        }

        if ($("#register-otp").is(':checked')) {
            if ($.trim($("#txtMobileNumber").val()) == "") {
                $("#lblMessageMobNum").html(registerUserInfo.defaults.EnterMobileNumber);
                return false;
            }
            else {
                $("#lblMessageMobNum").html('');
            }
            if (isNaN($("#txtMobileNumber").val()) || $("#txtMobileNumber").val().length != registerUserInfo.defaults.mobileNumberMaxLength) {
                $("#lblMessageMobNum").html(registerUserInfo.defaults.InvalidMobileNumber);
                return false;
            }
            else {
                $("#lblMessageMobNum").html('');
            }
        }
        return true;
    },
    timeisUp: function () {
        //Code to be executed when timer expires.
        $("#aResetPassResendOTP").show();
        $("#spnOtpReceiveMsg").html(registerUserInfo.defaults.OTPResendMsg)
        $("#seconds_timer").hide();
    },
    fnValidateMobileNumber: function () {
        if ($.trim($("#txtMobileNumber").val()) == "") {
            $("#lblMessageMobNum").html(registerUserInfo.defaults.EnterMobileNumber);
            return false;
        }
        else {
            $("#lblMessageMobNum").html('');
        }
        if (isNaN($("#txtMobileNumber").val()) || $("#txtMobileNumber").val().length != registerUserInfo.defaults.mobileNumberMaxLength) {
            $("#lblMessageMobNum").html(registerUserInfo.defaults.InvalidMobileNumber);
            return false;
        }
        else {
            $("#lblMessageMobNum").html('');
        }

        return true;
    },
    fnCheckCompletion: function () {
        if ($("#register-otp").is(':checked')) {
            if (registerUserInfo.defaults.IsValidOTP == "1") {
                return true;
            }
            else {
                var r = confirm(decodeHtml(registerUserInfo.defaults.MobilNumberRegisterValidation));
                if (r == true) {
                    return false;
                } else {
                    $("#register-otp").attr("checked", false);
                    $('#contact-section').toggle();
                    return true;
                }

                return false;
            }
        }
        else {
            return true;
        }
    },
    validDate: function(text) {

        var date = Date.parse(text);

        if (isNaN(date)) {
            return false;
        }

        var comp = text.split('/');

        if (comp.length !== 3) {
            return false;
        }

        var m = parseInt(comp[0], 10);
        var d = parseInt(comp[1], 10);
        var y = parseInt(comp[2], 10);
        var date = new Date(y, m - 1, d);
        return (date.getFullYear() == y && date.getMonth() + 1 == m && date.getDate() == d);
    }
}