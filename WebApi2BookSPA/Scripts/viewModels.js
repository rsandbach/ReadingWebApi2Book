﻿var taskManagementUrl = "http://localhost:49662/api/V1";

var indexViewModel = function() {

    var self = this;

    self.statusMessage = ko.observable("(Click Refresh button to load tasks)");

    self.refreshTasks = function() {
        $.ajax({
            type: 'GET',
            url: taskManagementUrl + '/tasks',
            xhrFields: {
                withCredentials: true
            },
            contentType: 'application/json;charset=utf8',
            success: self.onRefreshSuccess,
            error: self.onRefreshError
        });
    };

    self.onRefreshSuccess = function(data, status) {
        self.statusMessage(JSON.stringify(data, null, 4));
    };

    self.onRefreshError = function(error) {
        self.statusMessage(error.responseText);
    };
};
