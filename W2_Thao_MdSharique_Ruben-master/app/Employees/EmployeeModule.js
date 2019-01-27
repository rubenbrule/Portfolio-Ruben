'use strict';
angular
    .module('employeeGrid', ['ui.bootstrap'])
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/Employees', {
            templateUrl: 'Employees/Employees.html',
            controller: 'employeeController'
        });
    }])

    .controller('employeeController', ['$scope','employeeService', function ($scope,employeeService) {
        $scope.filter = '';
        // $scope.employees = empFactory.data;
        // $scope.department = departmentFactory.data;

        employeeService.getEmployee()
            .then(function(response){
                $scope.employees =response.data;
                for (var i = 0; i < $scope.employees.length; i++) {
                    $scope.employees[i].birthDate = new Date($scope.employees[i].birthDate);
                    $scope.employees[i].hireDate = new Date($scope.employees[i].hireDate);
                    if ($scope.employees[i].gender == 'F') {
                        $scope.employees[i].gender = 'Female';
                    } else {
                        $scope.employees[i].gender = 'Male';
                    }

                }
                },function(error){
                $scope.error=error;
            })

        $scope.GetDept = function(id)
        {
            for (var i = 0; i < $scope.employees.length; i++) {
                if($scope.employees[i].no == id) {
                    employeeService.getEmpInfo($scope.employees[i].no)
                        .then(function (response) {
                            $scope.empInfo = response.data;
                        }), function (error) {
                        $scope.error = error;
                    }
                }
            }

        }
        //Test
        $scope.newEmpl = {};
        $scope.addEmp = function(){
            if(!$scope.newEmpl.birthDate||!$scope.newEmpl.firstName||!$scope.newEmpl.lastName||!$scope.newEmpl.gender||!$scope.newEmpl.hireDate){
                alert("Invalid input")
            }else {
                $scope.newEmpl.no = $scope.employees.length + 10001;

                $scope.employees.push($scope.newEmpl);
            }
        };
        $scope.updateEmployee = function(){
            for(var i = 0; i<$scope.employees.length; i++)
            {
                if($scope.employees[i].no == $scope.curentEmp.no)
                {
                    $scope.employees[i] = $scope.curentEmp;
                }
            }
            $scope.curentEmp={};
        };

        $scope.curentEmp={};
        $scope.editEmployee = function(id){
            for(var i = 0; i<$scope.employees.length; i++)
            {
                if($scope.employees[i].no == id)
                {
                    $scope.curentEmp = angular.copy($scope.employees[i]);
                }
            }
        };

        // $scope.ShowEmployee = function(id)
        // {
        //     console.log("Eeeeeeeeeee");
        //     for (var i = 0; i < $scope.employees.length; i++) {
        //         if($scope.employees[i].no == id)
        //         {
        //             employeeService.getEmpInfo($scope.employees[i].no)
        //                 .then(function(response)
        //                 {
        //                     $scope.empInfo = response.data;
        //
        //                 }), function(error) {
        //                 $scope.error = error;
        //             };
        //         }
        //     }
        // };
        $scope.MoreDetail =function(id){
            console.log("THHHHHHHHHH");
            for(var i=0;i<$scope.employees.length;i++){
                if($scope.employees[i].no==id){
                    employeeService.getEmpInfo($scope.employees[i].no)
                        .then(function (response) {
                            $scope.info = response.data;
                        }),function (error) {
                        $scope.error=error;
                    }
                }
            }
        }
        // $scope.addEmployee = function(fname, lname, birthday, gender, hiredate){
        //     var duplicate = false;
        //     var first = document.getElementById("formFname").value;
        //     var last = document.getElementById("formLname").value;
        //     for(var i=0; i<$scope.employees.length; i++){
        //         if($scope.employees[i].firstName==fname && $scope.employees[i].lastName==lname){
        //             duplicate = true;
        //         }
        //     }
        //     if(first!="" && last!="" && duplicate == false){
        //         $scope.count = 10001 + $scope.employees.length + 1;
        //         $scope.employees.push({no: $scope.count, firstName: fname, lastName: lname, birthDate: birthday, gender: gender, hireDate: hiredate});
        //     }
        //     else{alert("Invalid");}
        // }

        $scope.removeEmp = function (employee) {
            $scope.employees.splice($scope.employees.indexOf(employee), 1);
        };

        $scope.getEmployee = function (id) {
            for (var i = 0; i < $scope.employees.length; i++) {
                if ($scope.employees[i].no == id) {
                    $scope.newEmp = angular.copy($scope.employees[i]);
                }
            }
        };

        $scope.edit = function (id) {
            for (var i = 0; i < $scope.employees.length; i++) {
                if ($scope.employees[i].no == id) {
                    $scope.newEmp = angular.copy($scope.employees[i]);
                }
            }
        };

        $scope.update = function(id, fname, lname, birthday, gender, hireDate){
            for (var i=0; i<$scope.employees.length; i++){
                if($scope.employees[i].no == id){
                    $scope.employees[i].firstName = fname;
                    $scope.employees[i].lastName = lname;
                    $scope.employees[i].birthDate = birthday;
                    $scope.employees[i].gender = gender;
                    $scope.employees[i].hireDate = hireDate;
                }
            }
        }
    }])

    .service('employeeService',['$http',function ($http) {
        this.getEmployee = function(){
            return $http.get('http://i874156.iris.fhict.nl/WEB2/employees');
        };
        this.getEmpInfo = function(id){
            return $http.get('http://i874156.iris.fhict.nl/WEB2/employees/' + id);
        };
    }])

