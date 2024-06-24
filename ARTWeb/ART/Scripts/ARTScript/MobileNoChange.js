var mobileNoChange = {
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
        $('#mobileNoChange-form').fadeIn(layout1.defaults.pageloadType);
        $('#mobileNoChange-form').fadeIn(layout1.defaults.pageEffectTime);

        var validate = $("#mobileNoChange-form").validate({
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
                    required: mobileNoChange.defaults.PlsEnterOtp,
                    maxlength: mobileNoChange.defaults.OTPMinMsg,
                    minlength: mobileNoChange.defaults.OTPMinMsg,
                },
            }
        });

        if (mobileNoChange.defaults.IsOtpAttemptExceed == "1" && (mobileNoChange.defaults.IsOTPSent == "0" || mobileNoChange.defaults.IsOTPSent == "")) {
            $("#lblOTPExceedMsg").text(mobileNoChange.defaults.OTPMaxAttemptMsg);           

        }

        if (mobileNoChange.defaults.IsOtpAttemptExceed == "1" && mobileNoChange.defaults.IsOTPSent == "1") {
            $("#pOtpWait").hide();
        }

        if (mobileNoChange.defaults.IsOTPSent == "1") {
          
            $("#btnSendOTP").css("display", "none");
            $("#txtMobileNumber").attr("disabled", "disabled");
            $('#drpCountryCode').attr("disabled", "disabled");           
        }

        if (mobileNoChange.defaults.IsValidOTP == "1") {
            $("#dv-OTP-section").hide();
            $("#pOtpWait").hide();           
        }

       
        $("#btnSubmitUserInfo").on("click", function (e) {
            if (mobileNoChange.fnCheckCompletion()) {
                $("#txtMobileNum").val($("#txtMobileNumber").val());
                $("#btnAction").val("Submit");
                var result = mobileNoChange.fnValidate()
                if (result)
                    window.location.href = '/UserSettings'
            }
        });

        $("#btnSendOTP").on("click", function (e) {
            if (mobileNoChange.fnValidateMobileNumber()) {
                this.style.display = "none";
                $("#txtMobileNum").val($("#txtMobileNumber").val());
                $("#btnAction").val("SendOTP");
                $("#mobileNoChange-form").submit();
            }
        });
        $("#btnValidateOTP").on("click", function (e) {
            var isValid = $("#mobileNoChange-form").valid();
            if (!isValid) {
                return;
                e.preventDefault();
            }

            $("#txtMobileNum").val($("#txtMobileNumber").val());
            $("#btnAction").val("ValidateOTP");
            $("#mobileNoChange-form").submit();
        });
        $("#aResetPassResendOTP").on("click", function (e) {
            $("#txtMobileNum").val($("#txtMobileNumber").val());
            $("#countryCode").val($('#drpCountryCode').val());
            $("#btnAction").val("ResendOTP");
            $("#mobileNoChange-form").submit();
        });


        $("#btnUserRegisterCancel").on("click", function (e) {
            window.location.href = "/UserSettings";
        });

        $('#drpCountryCode').on('change', function () {
            var countryValue = this.value;
            var countryCode = countryValue.split('_')[0];
            var telephoneCode = countryValue.split('_')[1];
            var mobileNumLength = countryValue.split('_')[2];

            $("#txtMobileNumber").val('');
            $("#txtCountryCode").val(telephoneCode);
            mobileNoChange.defaults.mobileNumberMaxLength = mobileNumLength;
            $("#countryCode").val($('#drpCountryCode').val());

            $("#txtMobileNumber").attr("maxlength", mobileNoChange.defaults.mobileNumberMaxLength);

            //if (mobileNoChange.defaults.DefaultCountryCode == telephoneCode)
            //{
            //    $("#txtMobileNumber").val(mobileNoChange.defaults.DefaultMobileNumber);
            //}

        })

        if (mobileNoChange.defaults.Is_OTP_Enabled == "Y") {
            //$('#drpCountryCode').trigger("change");

            $("#countryCode").val($('#drpCountryCode').val());

            mobileNoChange.defaults.mobileNumberMaxLength = $("#drpCountryCode").val().split('_')[2];
            //mobileNoChange.defaults.DefaultMobileNumber = $("#txtMobileNumber").val();

            $("#txtCountryCode").val($("#drpCountryCode").val().split('_')[1]);
        }
        //mobileNoChange.defaults.DefaultCountryCode = $("#drpCountryCode").val().split('_')[1];

        $("#txtMobileNumber").on("blur", function (e) {
            $("#txtMobileNum").val($("#txtMobileNumber").val());
            if ($.trim($("#txtMobileNumber").val()) == "") {
                $("#lblMessageMobNum").html(mobileNoChange.defaults.EnterMobileNumber);
                return false;
            }
            else {
                $("#lblMessageMobNum").html('');
            }
            if (isNaN($("#txtMobileNumber").val()) || $("#txtMobileNumber").val().length != mobileNoChange.defaults.mobileNumberMaxLength) {
                $("#lblMessageMobNum").html(mobileNoChange.defaults.InvalidMobileNumber);
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
        $("#txtMobileNumber").attr("maxlength", mobileNoChange.defaults.mobileNumberMaxLength);

        if (mobileNoChange.defaults.OpenRegisterForOTP == "1") {
          
        }

        // Bind the last 10(depends on the country code) digits to the mobile number textbox
        var currentMobileNum = $("#txtMobileNumber").val();
        $("#txtMobileNumber").val(currentMobileNum.slice(mobileNoChange.defaults.mobileNumberMaxLength * -1));

        layout1.defaults.defaultLang = mobileNoChange.defaults.defaultLang;
        layout1.init();

    },
    fnValidate: function () {

        return true;
    },
    timeisUp: function () {
        //Code to be executed when timer expires.
        $("#aResetPassResendOTP").show();
        $("#spnOtpReceiveMsg").html(mobileNoChange.defaults.OTPResendMsg)
        $("#seconds_timer").hide();
    },
    fnValidateMobileNumber: function () {
        if ($.trim($("#txtMobileNumber").val()) == "") {
            $("#lblMessageMobNum").html(mobileNoChange.defaults.EnterMobileNumber);
            return false;
        }
        else {
            $("#lblMessageMobNum").html('');
        }
        if (isNaN($("#txtMobileNumber").val()) || $("#txtMobileNumber").val().length != mobileNoChange.defaults.mobileNumberMaxLength) {
            $("#lblMessageMobNum").html(mobileNoChange.defaults.InvalidMobileNumber);
            return false;
        }
        else {
            $("#lblMessageMobNum").html('');
        }

        return true;
    },
    fnCheckCompletion: function () {
      
            if (mobileNoChange.defaults.IsValidOTP == "1") {
                return true;
            }
            else {
                var r = confirm(decodeHtml(mobileNoChange.defaults.MobilNumberRegisterValidation));
                if (r == true) {
                    return false;
                }
                else
                {
                    window.location.href = '/UserSettings'
                }

                return false;
            }
               
    },
    validDate: function (text) {

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