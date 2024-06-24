/*Custom jQuery file for ART (Account Recovery Tool)
 *Copyright 2016, Hexaware Technologies powered by Raise IT
 *Date Novemeber 2016
 */
var custom = {
    defaults: {
        MinMax: 'Min 9 - Max 13 characters',
        UpperCase: 'One uppercase letter',
        SmallLetter: 'One small letter',
        NumericChar: 'One numeric character',
        SpecialChar: 'One special character',
    }
}
$(document).ready(function () {
    window['adrum-start-time'] = new Date().getTime();
    (function (config) {
        config.appKey = 'AD-AAB-AAJ-KCD';
        config.adrumExtUrlHttp = 'http://cdn.appdynamics.com';
        config.adrumExtUrlHttps = 'https://cdn.appdynamics.com';
        config.beaconUrlHttp = 'http://col.eum-appdynamics.com';
        config.beaconUrlHttps = 'https://col.eum-appdynamics.com';
        config.xd = { enable: false };
    })(window['adrum-config'] || (window['adrum-config'] = {}));

    var script = document.createElement('script');
    script.src = 'https://cdn.appdynamics.com/adrum/adrum-4.3.8.0.js';
    document.getElementsByTagName('head')[0].appendChild(script); 

    $('.info-img').hover(function () {
        $('.info-box').css('display', 'block');
    })
    $('.info-img').mouseleave(function () {
        $('.info-box').hide();
    })

    // all user id limit alphanumeric
    $('.clsaplhanumeric').keypress(function (e) {
        var regex = new RegExp("^[a-zA-Z0-9]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }
        if (e.keyCode == 8 || e.keyCode == 9 || e.keyCode == 46 || e.keyCode == 37 || e.keyCode == 39) {
            return true;
        }

        e.preventDefault();
        return false;
    });

    /*radio span on selection style change*/
    $('#radiobtn-OTP').on('click', function () {
        $(this).addClass('radio-checked');
        $('#section-otp').addClass('auth-grp-open');
        $('#section-security').removeClass('auth-grp-open');
        $('#radiobtn-security').removeClass('radio-checked');

    });
    $('#radiobtn-security').on('click', function () {
        $(this).addClass('radio-checked');
        $('#section-security').addClass('auth-grp-open');
        $('#section-otp').removeClass('auth-grp-open');
        $('#radiobtn-OTP').removeClass('radio-checked');
    });

    /*check box toggle section*/
    $('#register-otp').on('click', function () {
        $('#contact-section').toggle();
    })

    /* tool tip box for form controls on focus*/
    $('input[type=text], input[type=password]').focusin(function () {
        var infoText = $(this).data('text'),
            id = $(this).attr('id');
        if (id == "inputPassword" || id == "txtNewPassword") {
            //$("<ul class='info-list info-box'><li class='fail'>Min 9 - Max 13 characters</li><li class='fail'>One uppercase letter</li><li class='fail'>One small letter</li><li class='fail'>One numeric character</li><li class='fail'>One special character</li></ul>").insertAfter(this);
            //$("<ul class='info-list info-box'><li class='fail'>" + custom.defaults.MinMax + "</li><li class='fail'>" + custom.defaults.UpperCase + "</li><li class='fail'>" + custom.defaults.SmallLetter + "</li><li class='fail'>" + custom.defaults.NumericChar + "</li><li class='fail'>" + custom.defaults.SpecialChar + "</li></ul>").insertAfter(this);
            $("<ul class='info-list info-box'><li class='fail'>" + custom.defaults.UpperCase + "</li><li class='fail'>" + custom.defaults.SmallLetter + "</li><li class='fail'>" + custom.defaults.NumericChar + "</li><li class='fail'>" + custom.defaults.SpecialChar + "</li><li class='fail'>" + custom.defaults.MinMax + "</li></ul>").insertAfter(this);

            pwdValidate($('#' + id));
        }
        else {
            $('<div></div>', {
                class: 'info-box',
                text: infoText
            }).insertAfter(this);
        }

        if ((id == "startDate") || (id == "endDate") || (id == "activitystartDate") || (id == "activityendDate") || (id == "FrqAccLockstartDate") || (id == "FrqAccLockendDate") || (id == "IncompletedActivitystartDate") || (id == "IncompletedActivityendDate"))
           {
            $(this).next('div').remove();
        }
    });

    //remove tool tip on out focus
    $('input[type=text], input[type=password]').focusout(function () {
        $(this).next('div').remove();
        $(this).next('ul').remove();

    });

    //password validate method on key up
    $('#inputPassword, #txtNewPassword').keyup(function () {
        if ($('#inputPassword').is(":visible"))
            pwdValidate($('#inputPassword'));
        if ($('#txtNewPassword').is(":visible"))
            pwdValidate($('#txtNewPassword'));
    });

    function pwdValidate(controlID) {
        var pwd = controlID.val(),
                check = $('.info-list li');

        //validate uppercase
        if (pwd.match(/[A-Z]/)) {
            check.eq(0).removeClass('fail');
            check.eq(0).addClass('success');
        }
        else {
            check.eq(0).removeClass('success');
            check.eq(0).addClass('fail');
        }

        //validate lowercase
        if (pwd.match(/[a-z]/)) {
            check.eq(1).removeClass('fail');
            check.eq(1).addClass('success');
        }
        else {
            check.eq(1).removeClass('success');
            check.eq(1).addClass('fail');
        }

        //validate number
        if (pwd.match(/\d/)) {
            check.eq(2).removeClass('fail');
            check.eq(2).addClass('success');
        }
        else {
            check.eq(2).removeClass('success');
            check.eq(2).addClass('fail');
        }

        //validate special character
        if (pwd.match(/([!,@,#,$,%,^,&,*,_,~])/)) {
            check.eq(3).removeClass('fail');
            check.eq(3).addClass('success');
        }
        else {
            check.eq(3).removeClass('success');
            check.eq(3).addClass('fail');
        }

        //validate length
        if ((pwd.length <= '13') && (pwd.length >= '9')) {
            check.eq(4).removeClass('fail');
            check.eq(4).addClass('success');
        }
        else {
            check.eq(4).removeClass('success');
            check.eq(4).addClass('fail');
        }
    }

    //to avoid body width jump when modal pops up
    $('.modal').on('show.bs.modal', function () {
        // no-scroll
        $('body').addClass("modal-open-noscroll");
    });
    $('.modal').on('hide.bs.modal', function () {
        $('body').removeClass("modal-open-noscroll");
    });

    $('.clsGoBack').on('click', function () {
        fnGoBack();
    })
    $('.clsGoHome').on('click', function () {
        fnGoToHome();
    })

    $('.clsGoEmployee').on('click', function () {
        fnGoToEmployee();
    })

  

});

function fnGoBack() {
    window.history.back();
}
function fnGoToHome() {
    window.location.href = "../";
}
function fnGoToEmployee() {
    window.location.href = "../Reports/EmployeeSearch";
}
function decodeHtml(html) {
    return $('<div>').html(html).text();
}
function fnValidateUserId(userId) {
    return /^[a-zA-Z0-9@]*$/.test(userId);
}

