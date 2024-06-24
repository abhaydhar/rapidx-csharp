var homePage = {
    defaults: {
        defaultLang: "en-US",
    },
    init: function () {
        homePage.checklangSettings();
        homePage.getlanguages();
        //homePage.getOptions();
    },
    checklangSettings: function () {
        var context = ['Home', 'CheckMultiLingualEnabled'];
        CoreREST.get(context, null, function (data) {
            if (data != null && data != undefined) {
                if (data == "Y") {
                    $("#localize").show();
                }
            }
        }, function (e) { });
    },
    getlanguages: function () {
        var context = ['Home', 'GetLanguages'];
        CoreREST.get(context, null, function (data) {
            for (var j = 0; j < data.length; j++) {
                $("#localize").append('<option value="' + data[j].lang_culture_name + '">' + data[j].lang_name + '</option>');
            }
            $("#localize").val(homePage.defaults.defaultLang);
        }, function (e) { });
    },
    getOptions: function () {
        var context = ['Home', 'GetOptions'];
        CoreREST.get(context, null, function (data) {
        }, function (e) { });
    }
}