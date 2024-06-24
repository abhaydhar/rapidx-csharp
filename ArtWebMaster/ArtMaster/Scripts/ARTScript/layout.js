var layout1 = {
    defaults: {
        defaultLang: "en-US",
        pageEffectTime: 1000,
        pageloadType: "fast"
    },
    init: function () {
        layout1.checklangSettings();
        layout1.getlanguages();
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
            $("#localize").val(layout1.defaults.defaultLang);
        }, function (e) { });
    }
}