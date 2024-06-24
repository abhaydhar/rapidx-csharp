//$(document).ready(function () {
//    debugger;
//    $('#check').click(function checkForm(event) {

//        $('#status').attr('class', 'inProgress');
//        $('#status').text('Checking...');

//        // get client-side Captcha object instance
//        var captchaObj = $("#CaptchaCode").get(0).Captcha;

//        // gather data required for Captcha validation
//        var params = {}
//        params.CaptchaId = captchaObj.Id;
//        params.InstanceId = captchaObj.InstanceId;
//        params.UserInput = $("#CaptchaCode").val();

//        // make asynchronous Captcha validation request
//        $.getJSON('../BotDetect/CheckCaptcha', params, function (result) {
//            if (true === result) {
//                $('#status').attr('class', 'correct');
//                $('#status').text('Check passed');
//            } else {
//                $('#status').attr('class', 'incorrect');
//                $('#status').text('Check failed');
//                // always change Captcha code if validation fails
//                captchaObj.ReloadImage();
//            }
//        });

//        event.preventDefault();
//    })
//});

$(document).ready(function () {
    //$("a:contains('BotDetect CAPTCHA ASP.NET Form Validation')").removeAttr('style')
    //$("a:contains('BotDetect CAPTCHA ASP.NET Form Validation')").removeAttr('title')
    //$("a:contains('BotDetect CAPTCHA ASP.NET Form Validation')").html('')
});

var botDetect = {
    validateBotDetect: function (success, failure) {
        $('#status').text('');
        //$('#status').attr('class', 'error');
        //$('#status').text('Validating...');

        // get client-side Captcha object instance
        var captchaObj = $("#CaptchaCode").get(0).Captcha;

        // gather data required for Captcha validation
        var params = {}
        params.CaptchaId = captchaObj.Id;
        params.InstanceId = captchaObj.InstanceId;
        params.UserInput = $("#CaptchaCode").val();

        // make asynchronous Captcha validation request
        $.getJSON('../BotDetect/CheckCaptcha', params, function (result) {
            //if (true === result) {
            //    $('#status').attr('class', 'correct');
            //    $('#status').text('Invalid Captcha');
            //} else {
            //    $('#status').attr('class', 'incorrect');
            //    $('#status').text('Invalid Captcha');
            //    // always change Captcha code if validation fails
            //    captchaObj.ReloadImage();
            //}
            if (!result) {
                captchaObj.ReloadImage();
            }
            success(result);

        }).fail(function (data) {
            failure(data);
        });
    }
}