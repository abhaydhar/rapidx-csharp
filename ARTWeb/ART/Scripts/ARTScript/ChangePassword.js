var changePassword = {
    defaults: {
        IsPasswordChanged: '',
        IsAccountLocked: '',
        DefaultLang: 'en-US',
        Message: '',
        MissingUserID: '',
        Min6Char: '',
        EnterNewPassword: '',
        Max13Char: '',
        Min9Char: '',
        PasswordPolicy: '',
        ReenterPassword: '',
        PasswordMismatch: '',
        EnterCaptchaImageText: '',
        InvalidCaptcha: '',
        InvalidUserId: '',
        PasswordSpaceMsg: '',
        SamePasswordMsg: '',
        passwordpharsecheck: ''
    },
    init: function () {
        $('#pwdreset-form').fadeIn(layout1.defaults.pageloadType);
        $('#pwdreset-form').fadeIn(layout1.defaults.pageEffectTime);

        var message = changePassword.defaults.Message;

        // Page Load
        if (message != "") {
            var isPasswordChanged = changePassword.defaults.IsPasswordChanged;
            var isChanges = Boolean(isPasswordChanged.toLowerCase());
            var isAccountLocked = changePassword.defaults.IsAccountLocked;

            if (message != "" && isPasswordChanged.toLowerCase() == "true") {
                $("#modal-success").modal({
                    show: true, backdrop: 'static',
                    keyboard: false
                });
            }
            else if (message != "" && isPasswordChanged.toLowerCase() == "false") {
                if (isAccountLocked == "Y")
                    $("#aUnlockAcc").show();

                $("#modal-fail").modal({
                    show: true, backdrop: 'static',
                    keyboard: false
                });
            }
        }

        ///
        var defaults = {
            // Form input element that the user will type into. Required.
            input: null,
            // Element which the CAPTCHA will be appended to. Required.
            display: null,
            // Function to call when the CAPTCHA passes.
            pass: function () { return true; },
            // Function to call when the CAPTCHA fails to pass.
            fail: function () { return false; },
            // Length of CAPTCHA text.
            captchaLength: 5,
            // Characters to use when generating text
            chars: '0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz'
            //chars: 'अबलळऌनणपक़र'

        };
        function generateCaptcha(options) {
            var text = '';
            for (var i = 0; i < options.captchaLength; i++) {
                var rand = Math.floor(Math.random() * options.chars.length);
                text += options.chars.charAt(rand);
            }
            return text;
        }
        function init(form, options) {
            var $this = $(form);
            var captchaText = generateCaptcha(options);
            $("#captchavalue").val(captchaText)
            $this.data('captchaText', captchaText);
            // Create CAPTCHA ui
            var table = $('<table id="captchatable"></table>').css({
                'font-family': 'Arial',
                'background-color': 'transparent',
                'text-decoration': 'none',
                'line-height': '38px',
                'background': 'url("~/Images/captchawhitline.png") repeat-x repeat-y'
            });
            var row = $('<tr></tr>').appendTo(table);
            for (var i = 0; i < captchaText.length; i++) {
                $('<td>' + captchaText.charAt(i) + '</td>').css({
                    'border-collapse': 'collapse',
                    'margin': '0'
                }).appendTo(row);
            }
            table.appendTo(options.display);

            // Hook events
            $this.unbind('submit');

            $this.bind('submit', function (event) {
                var value = $(options.input).val();
                if (value === captchaText) {
                    return options.pass.apply(this);
                } else {
                    return options.fail.apply(this);
                }
            });

        }
        $.fn.clientSideCaptcha = function (options) {
            var opts = $.extend({}, defaults, options);
            return this.each(function () {
                if (this.tagName.toLowerCase() === 'form' && $(opts.input).length && $(opts.display).length) {
                    init(this, opts);
                }
            });
        }
        ///
        $("#pwdreset-form").clientSideCaptcha({
            input: "#captchaText",
            display: "#captcha",
            pass: function () {
                //$("#msgalert").text("proceed");
                return true;
            },
            fail: function () {
                //$("#msgalert").text("Invalid Captcha");
                $("input[type=password]").each(function () { $(this).val(""); });
                return false;
            }
        });

        $("#recaptcha").on("click", function (e) {
            $("#msgalert").text("");
            $("#captchaText").val("");
            $("#captcha table").remove();
            $("form#pwdreset-form").removeData('captchaText');
            $("#pwdreset-form").clientSideCaptcha({
                input: "#captchaText",
                display: "#captcha",
                pass: function () {
                    //$("#msgalert").text("proceed");
                    return true;
                },
                fail: function () {
                    $("#msgalert").text("Invalid Captcha");
                    $("input[type=password]").each(function () { $(this).val(""); });
                    return false;
                }
            });
        });

        $("#btnChgPasswordCancel").on("click", function (e) {
            $('#pwdreset-form').get(0).reset();
        });

        $("#btnChgPassword").on("click", function (e) {
            var isValid = $("#pwdreset-form").valid();
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
                if (!result) {
                    $('#status').attr('class', 'error');
                    $('#status').text('Invalid Captcha');
                    return false;
                }
                else
                {
                    var hky = $("#hdky").val();
                    var key = CryptoJS.enc.Utf8.parse(hky);
                    var iv = CryptoJS.enc.Utf8.parse(hky);
                    var encryptedOtp = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse($("#inputOtpPassword").val()), key,
                        {
                            keySize: 128 / 8,
                            iv: iv,
                            mode: CryptoJS.mode.CBC,
                            padding: CryptoJS.pad.Pkcs7
                        });
                    var encryptedPassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse($("#inputPassword").val()), key,
                        {
                            keySize: 128 / 8,
                            iv: iv,
                            mode: CryptoJS.mode.CBC,
                            padding: CryptoJS.pad.Pkcs7
                        });
                    var encryptedPassword1 = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse($("#inputConfPassword").val()), key,
                       {
                           keySize: 128 / 8,
                           iv: iv,
                           mode: CryptoJS.mode.CBC,
                           padding: CryptoJS.pad.Pkcs7
                       });

                    $("#inputOtpPassword").val(encryptedOtp);
                    $("#inputPassword").val(encryptedPassword);
                    $("#inputConfPassword").val(encryptedPassword1);
                    $("#pwdreset-form").submit();
                }
            }, function (error) {
                $('#status').attr('class', 'error');
                $('#status').text('Invalid Captcha');
                return false;
            })

            //var hky = $("#hdky").val();
            //var key = CryptoJS.enc.Utf8.parse(hky);
            //var iv = CryptoJS.enc.Utf8.parse(hky);
            //var encryptedOtp = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse($("#inputOtpPassword").val()), key,
            //    {
            //        keySize: 128 / 8,
            //        iv: iv,
            //        mode: CryptoJS.mode.CBC,
            //        padding: CryptoJS.pad.Pkcs7
            //    });
            //var encryptedPassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse($("#inputPassword").val()), key,
            //    {
            //        keySize: 128 / 8,
            //        iv: iv,
            //        mode: CryptoJS.mode.CBC,
            //        padding: CryptoJS.pad.Pkcs7
            //    });
            //var encryptedPassword1 = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse($("#inputConfPassword").val()), key,
            //   {
            //       keySize: 128 / 8,
            //       iv: iv,
            //       mode: CryptoJS.mode.CBC,
            //       padding: CryptoJS.pad.Pkcs7
            //   });

            //$("#inputOtpPassword").val(encryptedOtp);
            //$("#inputPassword").val(encryptedPassword);
            //$("#inputConfPassword").val(encryptedPassword1);
            //$("#pwdreset-form").submit();
        });
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
        $.validator.addMethod("spaceCheck", function (value, element, regexp) {
            var regexp = /^\S*$/;
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
        $.validator.addMethod("notEqualTo",
            function (value, element, param) {
                var notEqual = true;
                value = $.trim(value);
                for (i = 0; i < param.length; i++) {
                    if (value == $.trim($(param[i]).val())) { notEqual = false; }
                }
                return this.optional(element) || notEqual;
            },
            "Please enter a diferent value."
        );
        $.validator.addMethod("passwordpharsecheck", function (value, element, regexp) {
            var passarray = ["password", "pass", "hexaware", "hex"];
            var userId = $("#inputEmail").val();
            if ($.trim(userId) != "")
                passarray.push(userId);

            for (var i = 0; i < passarray.length; i++) {
                if (value.indexOf(passarray[i]) != -1) {
                    return false;
                }
            }
            return true;
        }, "Please check your input.");
        var validate = $("#pwdreset-form").validate({
            debug: true,
            //onfocusout: function (element) { $(element).valid(); },
            onkeyup: false,
            onsubmit: false,
            onblur: true,
            rules: {
                inputEmail: {
                    required: true,
                    regex: "^[a-zA-Z0-9@_-]*$",
                    regexHTMLCheck: "/<(.|\n)*?>/g",
                },
                OTPPassword: {
                    required: true,
                    maxlength: 13,
                    minlength: 9,
                    regexHTMLCheck: "/<(.|\n)*?>/g",
                },
                NewPassword: {
                    required: true,
                    maxlength: 13,
                    minlength: 9,
                    //regex: "(?=^.{9,13}$)(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[^A-Za-z0-9]).*",
                    regexNewPassword: "(?=^.{9,13}$)(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?!.*[\-\`\:\"\[\]\'])(?=.*[^A-Za-z0-9]).*",
                    spaceCheck: "/^\S*$/",
                    regexHTMLCheck: "/<(.|\n)*?>/g",
                    notEqualTo: ['#inputOtpPassword'],
                    passwordpharsecheck: "#inputPassword"
                },
                ConfNewPassword: {
                    required: true,
                    equalTo: "#inputPassword"
                },
                captchaText: {
                    required: true,
                    equalTo: "#captchavalue"
                },
                CaptchaCode: {
                    required: true,
                }
            },
            messages: {
                inputEmail: {
                    required: changePassword.defaults.MissingUserID,
                    regex: changePassword.defaults.InvalidUserId,
                    regexHTMLCheck: "Please enter valid User ID",
                },
                OTPPassword: {
                    required: $("#hdnMessage1").val(),
                    maxlength: changePassword.defaults.Max13Char,
                    minlength: changePassword.defaults.Min9Char,
                    regexHTMLCheck: "Please enter valid current password",
                },
                NewPassword: {
                    required: changePassword.defaults.EnterNewPassword,
                    maxlength: changePassword.defaults.Max13Char,
                    minlength: changePassword.defaults.Min9Char,
                    regexNewPassword: changePassword.defaults.PasswordPolicy,
                    spaceCheck: changePassword.defaults.PasswordSpaceMsg,
                    regexHTMLCheck: "Please enter valid new password",
                    notEqualTo: changePassword.defaults.SamePasswordMsg,
                    passwordpharsecheck: changePassword.defaults.passwordpharsecheck,
                },
                ConfNewPassword: {
                    required: changePassword.defaults.ReenterPassword,
                    equalTo: changePassword.defaults.PasswordMismatch
                },
                captchaText: {
                    required: changePassword.defaults.EnterCaptchaImageText,
                    equalTo: changePassword.defaults.InvalidCaptcha
                },
                CaptchaCode: {
                    required: "Please enter the Captcha",
                }
            }
        });

        $(document).keypress(function (e) {
            if (e.which == 13) {
                $('#btnChgPassword').trigger('click');
            }
        });
        $("#captchaText").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#btnChgPassword").click();
            }
        });

        layout1.defaults.defaultLang = changePassword.defaults.DefaultLang;
        layout1.init();
    },

}