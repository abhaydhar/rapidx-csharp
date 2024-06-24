var reset = {
    defaults: {
        defaultLang: 'en-US',
        Message: '',
        IsPasswordReset: '',
        EnterNewPassword: '',
        Max13Char: '',
        Min9Char: '',
        PasswordPolicy: '',
        ReenterPassword: '',
        PasswordMismatch: '',
        PasswordSpaceMsg: '',
        passwordpharsecheck: '',
        UserId: '',
    },
    init: function () {
        $('#reset-form').fadeIn(layout1.defaults.pageloadType);
        $('#reset-form').fadeIn(layout1.defaults.pageEffectTime);

        $('#reset-form').get(0).reset();

        var message = reset.defaults.Message;
        var defaultLang = reset.defaults.defaultLang;

        if (message != "") {
            var isPasswordChanged = reset.defaults.IsPasswordReset;

            if (message != "" && isPasswordChanged.toLowerCase() == "true") {
                $("#modal-success").modal({
                    show: true, backdrop: 'static',
                    keyboard: false
                });
            }
            else if (message != "" && isPasswordChanged.toLowerCase() == "false") {
                $("#modal-fail").modal({
                    show: true, backdrop: 'static',
                    keyboard: false
                });
            }
        }

        $('#btnReset').on('click', function () {
            reset.fnSubmit();
        })

        $.validator.addMethod("passwordpharsecheck", function (value, element, regexp) {
            var passarray = ["password", "pass", "hexaware", "hex"];
            if (reset.defaults.UserId != null && $.trim(reset.defaults.UserId) != "")
                passarray.push(reset.defaults.UserId);

            for (var i = 0; i < passarray.length; i++) {
                if (value.indexOf(passarray[i]) != -1) {
                    return false;
                }
            }
            return true;
        }, "Please check your input.");
        $.validator.addMethod("spaceCheck", function (value, element, regexp) {
            var regexp = /^\S*$/;
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        }, "Please check your input.");

        var validate = $("#reset-form").validate({
            debug: false,
            onkeyup: false,
            onsubmit: false,
            onblur: true,
            rules: {
                txtNewPassword: {
                    required: true,
                    maxlength: 13,
                    minlength: 9,
                    regexNewPassword: "(?=^.{9,13}$)(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?!.*[\-\`\:\"\[\]\'\\])(?=.*[^A-Za-z0-9]).*",
                    spaceCheck: "/^\S*$/",
                    passwordpharsecheck: "#txtNewPassword",
                    regexHTMLCheck: "/<(.|\n)*?>/g",
                },
                txtConfNewPassword: {
                    required: true,
                    equalTo: "#txtNewPassword"
                },
            },
            messages: {
                txtNewPassword: {
                    required: reset.defaults.EnterNewPassword,
                    maxlength: reset.defaults.Max13Char,
                    minlength: reset.defaults.Min9Char,
                    regexNewPassword: reset.defaults.PasswordPolicy,
                    spaceCheck: reset.defaults.PasswordSpaceMsg,
                    passwordpharsecheck: reset.defaults.passwordpharsecheck,
                    regexHTMLCheck: "Please enter valid new password",
                },
                txtConfNewPassword: {
                    required: reset.defaults.ReenterPassword,
                    equalTo: reset.defaults.PasswordMismatch
                },
            }
        });

        layout1.defaults.defaultLang = reset.defaults.defaultLang;
        layout1.init();

        $.validator.addMethod("regex", function (value, element, regexp) {
            //var regexp = /(?=^.{9,13}$)(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?!.*[\-\`\:\"\[\]\'])(?=.*[^A-Za-z0-9]).*/;
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        }, "Please check your input.");
        $.validator.addMethod("regexNewPassword", function (value, element, regexp) {
            var regexp = /(?=^.{9,13}$)(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?!.*[\-\`\:\"\[\]\'\\])(?=.*[^A-Za-z0-9]).*/;
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        }, "Please check your input.");
        $.validator.addMethod("regexHTMLCheck", function (value, element, regexp) {
            var regexp = /<(.|\n)*?>/g;
            var re = new RegExp(regexp);
            if (re.test(value))
                return false;//this.optional(element) || re.test(value);
            else
                return true;
        }, "Please check your input.");
        function clearValidation(formElement) {
            //Internal $.validator is exposed through $(form).validate()
            var validator = $(formElement).validate();
            //Iterate through named elements inside of the form, and mark them as error free
            $('[name]', formElement).each(function () {
                validator.successList.push(this);//mark as error free
                validator.showErrors();//remove error messages if present
            });
            validator.resetForm();//remove error class on name elements and clear history
            validator.reset();//remove all error and success data
        }
    },
    fnSubmit: function () {
        var isValid = $("#reset-form").valid();
        if (!isValid) {
            return;
            e.preventDefault();
        }

        var hky = $("#hdky").val();
        var key = CryptoJS.enc.Utf8.parse(hky);
        var iv = CryptoJS.enc.Utf8.parse(hky);
        var encryptedNewPassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse($("#txtNewPassword").val()), key,
            {
                keySize: 128 / 8,
                iv: iv,
                mode: CryptoJS.mode.CBC,
                padding: CryptoJS.pad.Pkcs7
            });

        var encryptedConfirmPassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse($("#txtConfNewPassword").val()), key,
            {
                keySize: 128 / 8,
                iv: iv,
                mode: CryptoJS.mode.CBC,
                padding: CryptoJS.pad.Pkcs7
            });

        $("#txtNewPassword").val(encryptedNewPassword);
        $("#txtConfNewPassword").val(encryptedConfirmPassword);

        document.forms["reset-form"].submit();
    }
}