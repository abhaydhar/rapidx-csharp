var forgetPassword = {
    defaults: {
        userQuestionCount: 3,
    },
    init: function () {
        forgetPassword.getUserQuestionsToAnswer();
    },
    getUserQuestionsToAnswer: function () {
        var context = ['User', 'GetQuestionsForUserToAnswer'];
        CoreREST.get(context, null, function (data) {
            forgetPassword.bindUserQuestions(data);
        }, function (e) { });
    },
    bindUserQuestions: function (questions) {
        for (var i = 1; i <= forgetPassword.defaults.userQuestionCount; i++) {
            $("#Que" + i).append('<option value="' + questions[i - 1].question_id + '">' + questions[i - 1].question_text + '</option>');
        }
    }
}