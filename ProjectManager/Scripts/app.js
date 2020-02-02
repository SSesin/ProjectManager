var ViewModel = function () {
    var self = this;
    self.tasks = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.users = ko.observableArray();
    self.overlaps = ko.observableArray();
    self.displayOverlapReport = ko.observable();
    self.newTask = {
        Employee: ko.observable(),
        TaskName: ko.observable(),
        DateStart: ko.observable(),
        Workload: ko.observable(),
        DateEnd: ko.observable()
    }
    
    var tasksUri = '/api/Tasks/';
    var usersUri = '/api/Users/';
    var overlapUri = '/api/Tasks/Overlap';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllTasks() {
        ajaxHelper(tasksUri, 'GET').done(function (data) {
            self.tasks(data);
        });
    }

    self.displayOverlapReport = function getOverlapReport() {
        ajaxHelper(overlapUri, 'GET').done(function(data) {
            self.overlaps(data);
        });
    }

    self.getTaskDetail = function (item) {
        ajaxHelper(tasksUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    function getUsers() {
        ajaxHelper(usersUri, 'GET').done(function (data) {
            self.users(data);
        });
    }

    self.deleteTask = function (item) {
        ajaxHelper(tasksUri + item.Id, 'Delete').done(
            function () {
                window.location.reload();
            });
    }

    self.addTask = function (formElement) {
        var task = {
            EmployeeId: self.newTask.Employee().Id,
            ProjectManagerId: 1,
            TaskName: self.newTask.TaskName(),
            DateStart: self.newTask.DateStart(),
            Workload: self.newTask.Workload(),
        };

        ajaxHelper(tasksUri, 'POST', task).done(function (item) {
            self.tasks.push(item);
        });
    }

    // Fetch the initial data.
    getAllTasks();
    getUsers();
};

ko.applyBindings(new ViewModel());