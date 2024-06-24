CoreREST = {

    _defaultServer: "../",
    accessKey: "dummy",

    _addContext: function (url, context) {
        if (context != null && context.length > 0) {
            for (var key in context) {
                url += context[key] + '/';
            }
        }
        return url;
    },
    _sessionFilter: function (a)
    {
        var auth = a.getResponseHeader("NOAUTH");
        if (a.status === 302 && auth == 0) {
            // assume that our login has expired - reload our current page
            window.location.href = "../"
        }
    },
    _raw: function (url, requestType, context, data, success, failure) {
        //TODO $.mobile.allowCrossDomainPages = true; un - comment code
        $.support.cors = true;
        url = this._addContext(url, context);
        if (data == null) {
            data = {};
        }
        //console.log(url);
        data.accessKey = this.accessKey;
        $.ajax({
            url: url,
            type: requestType,
            data: data,
            contentType: "text/plain",
            dataType: "json",
            async: true,
            crossDomain: true,
            success: function (response) {
                success(response);
            },
            error: function (a, b, c) {
                //console.log(JSON.stringify(a) + " - " + JSON.stringify(b) + " - " + JSON.stringify(c));
                failure(a);
                CoreREST._sessionFilter(a);
            }
        });
    },
    _rawAlt: function (url, requestType, context, data, success, failure) {
        $.support.cors = true;
        url = this._addContext(url, context);
        if (data == null) {
            data = {};
        }
        //console.log(url);
        //console.log(JSON.stringify(data));
        data = JSON.stringify(data);
        $.ajax({
            url: url,
            type: requestType,
            crossDomain: true,
            contentType: 'application/json',
            data: data,
            async: true,
            cache: false,
            success: function (response) {
                success(response);
            },
            error: function (a, b, c) {
                if (failure) failure(a);
            }
        });
    },
    _rawPostFile: function (url, requestType, context, data, success, failure) {
        $.support.cors = true;
        url = this._addContext(url, context);
        if (data == null) {
            data = {};
        }
        $.ajax({
            url: url,
            type: requestType,
            crossDomain: true,
            contentType: false,
            processData: false,
            dataType: 'json',
            data: data,
            async: true,
            cache: false,
            success: function (response) {
                success(response);
            },
            error: function (a, b, c) {
                if (failure) failure(angel);
            }
        });
    },
    post: function (context, data, success, failure) {
        this._raw(this._defaultServer, 'POST', context, data, success, failure);
    },
    postArray: function (context, data, success, failure) {
        this._rawAlt(this._defaultServer, 'POST', context, data, success, failure);
    },
    postFile: function (context, data, success, failure) {
        this._rawPostFile(this._defaultServer, 'POST', context, data, success, failure);
    },
    put: function (context, data, success, failure) {
        this._raw(this._defaultServer, 'POST', context, data, success, failure);
    },

    remove: function (context, data, success, failure) {
        this._raw(this._defaultServer, 'DELETE', context, data, success, failure);
    },

    get: function (context, data, success, failure) {
        this._raw(this._defaultServer, 'GET', context, data, success, failure);
    },
    localGet: function (context, data, success, failure) {
        this._raw('', 'GET', context, data, success, failure);
    },
    getSync: function (context, data, success, failure) {
        this._rawSync(this._defaultServer, 'GET', context, data, success, failure);
    },
    _rawSync: function (url, requestType, context, data, success, failure) {
        //TODO $.mobile.allowCrossDomainPages = true; un - comment code
        $.support.cors = true;
        url = this._addContext(url, context);
        if (data == null) {
            data = {};
        }
        //console.log(url);
        data.accessKey = this.accessKey;
        $.ajax({
            url: url,
            type: requestType,
            data: data,
            contentType: "text/plain",
            dataType: "json",
            async: false,
            crossDomain: true,
            success: function (response) {
                success(response);
            },
            error: function (a, b, c) {
                //console.log(JSON.stringify(a) + " - " + JSON.stringify(b) + " - " + JSON.stringify(c));
                failure(a);
            }
        });
    },
};


var sn = "../../";
var OrchAjax = {
    requestInvoke: function (ctn, actn, parms, method, successcallback, failurecallback) {
        var datapara = "";

        if (parms != null) {
            for (var j = 0; j < parms.length; j++) {
                var value = parms[j].value;
                if (parms[j].type == "JSON") {
                    value = JSON.stringify(value);
                }

                if (j == 0) {
                    datapara = parms[j].name + "=" + value;
                }
                else {
                    datapara += "&" + parms[j].name + "=" + value;
                }
            }
        }
        var aurl = sn + ctn + "/" + actn;
        $.ajax({
            type: method,
            url: aurl,
            data: datapara,
            async: false,
            cache: false,
            success: function (response) {

                successcallback(response);
            },
            error: function (e) {
                failurecallback(e);
            }
        });
    }
}