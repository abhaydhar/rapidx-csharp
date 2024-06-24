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
        OpenRegisterForOTP: '',
        Message:''

    },
    init: function () {
        $('#regUserInfo-form').fadeIn(layout1.defaults.pageloadType);
        $('#regUserInfo-form').fadeIn(layout1.defaults.pageEffectTime);


        var message = registerUserInfo.defaults.Message;

        if (message != "" && message != null) {            
            if (message != "") {
                $("#modal-success").modal({
                    show: true,
                    backdrop: 'static',
                    keyboard: false
                });
            }
            else if (message != "" && registerUserInfo.defaults.IsOTPSent == "false") {
                $("#modal-fail").modal({
                    show: true, backdrop: 'static',
                    keyboard: false,
                    backdrop: 'static',
                    keyboard: false
                });
            }
        }

        if (registerUserInfo.defaults.IsOTPSent == "1") {
            $("#txtMobileNumber").attr("disabled", "disabled");
            $('#drpCountryCode').attr("disabled", "disabled");
            //$("#txtgenOTP").attr("disabled", "disabled");
            $("#btnSendOTP").prop("readonly", true);
            $("#btnSendOTP").attr("disabled", "disabled");
        }
            
        $("#btnSendOTP").on("click", function (e) {
            if (registerUserInfo.fnValidateMobileNumber()) {
                $("#txtMobileNum").val($("#txtMobileNumber").val());
                $("#btnAction").val("SendOTP");
                $("#regUserInfo-form").submit();
            }
        });

        $('#register-otp').on('click', function () {
            $("#btnenrollLink").show();
            $("#register-otp").attr("disabled", "disabled");
        })

        $("#btnenrollLink").on("click", function (e) {
            if (registerUserInfo.fnValidateMobileNumber()) {
                $("#txtMobileNum").val($("#txtMobileNumber").val());

                $("#btnAction").val("EnrollmentLink");
                $("#regUserInfo-form").submit();
            }
        });

        $("#btnGoemployee").on("click", function (e) {
                var userId = $("#hdnuserid").val();
                var hky = $("#hdky").val();
                var page = "RegisterUserInfo";
                var key = CryptoJS.enc.Utf8.parse(hky);
                var iv = CryptoJS.enc.Utf8.parse(hky);
                var encryptedValue = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(page + "|" + userId), key,
                    {
                        keySize: 128 / 8,
                        iv: iv,
                        mode: CryptoJS.mode.CBC,
                        padding: CryptoJS.pad.Pkcs7
                    });          
               $(location).attr("href", "../Reports/EmployeeSearch?value=" + encodeURIComponent(encryptedValue) + "");
             //   $(location).attr("href", "../Reports/EmployeeSearch");

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


        })

        if (registerUserInfo.defaults.Is_OTP_Enabled == "Y") {

            $("#countryCode").val($('#drpCountryCode').val());

            registerUserInfo.defaults.mobileNumberMaxLength = $("#drpCountryCode").val().split('_')[2];

            $("#txtCountryCode").val($("#drpCountryCode").val().split('_')[1]);
        }


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
    }
}