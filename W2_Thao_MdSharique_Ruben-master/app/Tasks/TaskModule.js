'use strict';

angular
    .module('taskGrid', [])
    .config(['$routeProvider', function($routeProvider) {
        $routeProvider.when('/Tasks', {
            templateUrl: 'Tasks/Tasks.html',
            controller: 'taskController'
        });
    }])

    .service('taskService',['$http',function ($http) {
        this.getTask = function(){
            return $http.get('http://i874156.iris.fhict.nl/WEB2/tasks');
        };
        this.getTaskInfo = function(id){
            return $http.get('http://i874156.iris.fhict.nl/WEB2/tasks/' + id);
        };
        this.getEmpById = function(id){
            return $http.get('http://i874156.iris.fhict.nl/WEB2/employees/'+id);
        };
    }])
    .controller('taskController', ['$scope','taskService',   function($scope,taskService) {
        $scope.filter = '';
        /*Week6*/
        taskService.getTask()
            .then(function(response){
                $scope.tasks =response.data;
                for (var i = 0; i < $scope.tasks.length; i++) {
                    $scope.tasks[i].creatioDate = new Date($scope.tasks[i].creatioDate);
                    $scope.tasks[i].modificationDate = new Date($scope.tasks[i].modificationDate);
                    $scope.tasks[i].finishedDate = new Date($scope.tasks[i].finishedDate);
                }
            },function(error){
                $scope.error=error;
            });

        $scope.ShowEmp = function(id)
        {
            console.log("Eeeeeeeeeee");
            for (var i = 0; i < $scope.tasks.length; i++) {
                if($scope.tasks[i].no == id)
                {
                    taskService.getTaskInfo($scope.tasks[i].no)
                        .then(function(response)
                        {
                            $scope.taskInfo = response.data;
                            for(var i = 0; i < $scope.taskInfo.employees.length; i++)
                            {
                                taskService.getEmpById($scope.taskInfo.employees[i].no)
                                    .then(function(response)
                                    {
                                    })
                            }

                        }), function(error) {
                        $scope.error = error;
                    };
                }
            }
        };

        $scope.newTask = {};
        $scope.addTask = function(title,des,depNo){
            if(!title||!des||!depNo){
                alert("Enter the all details");
            }else{
            $scope.newTask.no = $scope.tasks.length + 1;

            $scope.tasks.push($scope.newTask);}
        };


        //Update

        $scope.edit = function(tId, titleData, status, desc) {
            // var thao = taskFactory.data[0].tId;
            for (var i = 0; i < $scope.tasks.length; i++) {
                if ($scope.tasks[i].no == tId) {
                    $scope.tasks[i].title = titleData;
                    $scope.tasks[i].status = status;
                    $scope.tasks[i].description = desc;
                }
            }
        };
        //Test
        $scope.updateTask = function(){
            for(var i = 0; i<$scope.tasks.length; i++)
            {
                if($scope.tasks[i].no == $scope.curentTask.no)
                {
                    $scope.tasks[i] = $scope.curentTask;
                }
            }
            $scope.curentTask={};
        };

        //week3 assignment remove task
        $scope.remove = function(task) {
            $scope.tasks.splice($scope.tasks.indexOf(task), 1);
        };

        //Week3 - Selected Task
        /*******For clicking on View Task *********/

        $scope.selectedTask = function(tId) {
            for (var i = 0; i < $scope.tasks.length; i++) {
                if ($scope.tasks[i].no == tId) {
                    $scope.name = angular.copy($scope.tasks[i]);
                }
            }
        };

         /*Working on 29/3*/
        //Edit
        $scope.curentTask={};
        $scope.editEmployee = function(id){
            for(var i = 0; i<$scope.tasks.length; i++)
            {
                if($scope.tasks[i].no == id)
                {
                    $scope.curentTask = angular.copy($scope.tasks[i]);
                }
            }
        };

        //close edit
        $scope.read = function(tId) {
            for (var i = 0; i < $scope.tasks.length; i++) {
                if ($scope.tasks[i].tId == tId) {

                }
            }

        };


    }]);
