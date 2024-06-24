var login = {
    deafults: {
        defaultLang: 'en-US',
        enterUserName: '',
        enterpassword: '',
        InvalidUserId: '',
    },
    init: function () {
        $('#login-form').fadeIn(layout1.defaults.pageloadType);
        $('#login-form').fadeIn(layout1.defaults.pageEffectTime);

        $('#login-form').get(0).reset();

        $.validator.addMethod("regex", function (value, element, regexp) {
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        }, "Please check your input.");

        //var validate = $("#login-form").validate({
        //    debug: true,
        //    onkeyup: false,
        //    onsubmit: false,
        //    onblur: true,
        //    rules: {
        //        txtUserName: {
        //            required: true,
        //            regex: "^[a-zA-Z0-9@]*$",
        //        },
        //        txtPassword: {
        //            required: true,
        //        },

        //    },
        //    messages: {
        //        txtUserName: {
        //            required: login.deafults.enterUserName,
        //            regex: login.deafults.InvalidUserId
        //        },
        //        txtPassword: {
        //            required: login.deafults.enterpassword
        //        },

        //    }
        //});

        $("#btnLogin").on("click", function (e) {
            var isValid = login.fnValidate(); // $("#login-form").valid();
            if (!isValid) {
                return;
                e.preventDefault();
            }

            $("#login-form").submit();
        });
        //$("#txtUserName").on("blur", function (e) {
        //    var userName = $.trim($("#txtUserName").val());

        //    if ($.trim(userName) == "") {
        //        $("#lblMessage").html(login.deafults.enterUserName);
        //        return false;
        //    }
        //    else {
        //        $("#lblMessage").html('');
        //    }
        //    var re = new RegExp("^[a-zA-Z0-9@]*$");
        //    if (!re.test(userName)) {
        //        $("#lblMessage").html(login.deafults.InvalidUserId);
        //        return false;
        //    }
        //    else {
        //        $("#lblMessage").html('');
        //    }
        //});
        $("#txtPassword").on("blur", function (e) {
            var password = $.trim($("#txtPassword").val());
            if ($.trim(password) == "") {
                $("#lblMessagePass").html(login.deafults.enterpassword);
                return false;
            }
            else {
                $("#lblMessagePass").html('');
            }
        });

        layout1.defaults.defaultLang = login.deafults.defaultLang;
        layout1.init();

        $(document).keypress(function (e) {
            if (e.which == 13) {
                $('#btnLogin').trigger('click');
            }
        });
    },
    fnValidate: function () {
        var userName = $.trim($("#txtUserName").val());
        var password = $.trim($("#txtPassword").val());
        var isValid = true;

        if ($.trim(userName) == "") {
            $("#lblMessage").html(login.deafults.enterUserName);
            isValid = false;
        }
           
        var re = new RegExp("^[a-zA-Z0-9@]*$");
        if (!re.test(userName)) {
            $("#lblMessage").html(login.deafults.InvalidUserId);
            isValid = false;
        }

        if ($.trim(password) == "") {
            $("#lblMessagePass").html(login.deafults.enterpassword);
            isValid = false;
        }

        return isValid;
    },
    UserNameOnKeyPress: function () {
        $('#txtUserName').unbind("blur");
        $("#txtUserName").on("blur", function (e) {
            var userName = $.trim($("#txtUserName").val());

            if ($.trim(userName) == "") {
                $("#lblMessage").html(login.deafults.enterUserName);
                return false;
            }
            else {
                $("#lblMessage").html('');
            }
            var re = new RegExp("^[a-zA-Z0-9@]*$");
            if (!re.test(userName)) {
                $("#lblMessage").html(login.deafults.InvalidUserId);
                return false;
            }
            else {
                $("#lblMessage").html('');
            }
        });
    },
}