'use strict'

angular
    .module('Dashboard', ['ngRoute','ui.bootstrap'])

    .controller('dashboardCtrl',['$scope','dashboardService',function ($scope,dashboardService) {
        dashboardService.getDepartment()
            .then(function(response){
                $scope.departments =response.data;
            } ,function(error){
                $scope.error=error;
            });
        dashboardService.getTask()
            .then(function(response){
                $scope.tasks =response.data;
            },function(error){
                $scope.error=error;
            });
        dashboardService.getEmployee()
            .then(function(response){
                $scope.employees =response.data;
            },function(error){
                $scope.error=error;
            });
    }])
    .service('dashboardService',['$http',function ($http) {
        this.getDepartment = function(){
            return $http.get('http://i874156.iris.fhict.nl/WEB2/departments');
        };
        this.getTask = function(){
            return $http.get('http://i874156.iris.fhict.nl/WEB2/tasks');
        };

            this.getEmployee = function(){
                return $http.get('http://i874156.iris.fhict.nl/WEB2/employees');
            };

    }])

    .config(['$routeProvider',function($routeProvider) {
        $routeProvider
            .when('/Dashboard', {
                templateUrl : 'Dashboard/Dashboard.html',
                controller: 'dashboardCtrl'
            })
            .when('/Employees', {
                templateUrl : 'Employees/Employees.html',
                controller: 'dashboardCtrl'
            })
            .when('/Tasks', {
                templateUrl :'Tasks/Tasks.html',
                controller: 'dashboardCtrl'

            })
            .when('/Departments', {
                templateUrl : 'Departments/Departments.html',
                controller: 'dashboardCtrl'

            });
    }]);