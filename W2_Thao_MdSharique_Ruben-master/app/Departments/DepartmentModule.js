'use strict';
angular
    .module('grid', ['ui.bootstrap'])
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/Departments', {
            templateUrl: 'Departments/Departments.html',
            controller: 'Controller'
        });
    }])
    .service('departmentService',['$http',function ($http) {
this.getDepartment= function () {
    return $http.get('http://i874156.iris.fhict.nl/WEB2/departments');
}
        this.getDeptInfo = function(id){
            return $http.get('http://i874156.iris.fhict.nl/WEB2/departments/' + id);
        };
        this.getEmpById = function(id){
            return $http.get('http://i874156.iris.fhict.nl/WEB2/employees/'+id);
        }
    }])

    /*********Week 4 - use Factory to create an array for Departments*******/

    // .factory('departmentFactory',[ function () {
    //     var obj = {};
    //     obj.data =[
    //         {id: 1,tId:1, name: 'Marketing', employees: 7, manager: 'Thao', managerContact: 'thuthaovinh@gmail.com'},
    //         {id: 2,tId: 2, name: 'Finance', employees: 5, manager: 'Sharique', managerContact: 'mdSharique@gmail.com'},
    //         {id: 3,tId:3, name: 'Human Resources', employees: 3, manager: 'Ruben', managerContact: 'Rubenbrule@gmail.com'}
    //     ];
    //     return obj;
    // }])

    /*********Week2 - Declare an array with department objects*************/
    .controller('Controller', ['$scope','departmentService', function ($scope,departmentService) {


        departmentService.getDepartment()
           .then(function(response){
               $scope.departments =response.data;

           },function(error){
               $scope.error=error;
           });

        //Test 27/3
        /*Get List of Employee*/
        $scope.ShowEmployee = function(id)
        {
            console.log("hhhhhhh");
            for (var i = 0; i < $scope.departments.length; i++) {
                console.log("one");
                if($scope.departments[i].no == id) {
                    console.log("true");
                    departmentService.getDeptInfo($scope.departments[i].no)
                        .then(function (response) {
                            console.log("r: " + response.data);
                            $scope.deptInfo = response.data;

                            // $scope.test = $scope.deptInfo.employees.birthDate;

                        }), function (error) {
                        console.log("e: " + error);
                        $scope.error = error;
                    }
                }
                else { console.log("id: " + id + "    " + "no: " + $scope.departments[i].no);}
            }

        }
        $scope.filter = '';

        $scope.newDept = {};
        $scope.addDept = function(){
           if(!$scope.newDept.code||!$scope.newDept.name){
               alert("Invalid input")
           }else {
               $scope.newDept.no = $scope.departments.length + 1;

               $scope.departments.push($scope.newDept);
           }
        };
        //Week3 - Create New Department
        $scope.newDepartment = function (deName,emp) {
            document.getElementById("idDept").value="";
            var bool = false;
            var x =document.getElementById("nameDept").value;
            if(x===""){
                alert("Invalid input");
            }else {
                for(var i=0;i<$scope.departments.length;i++){
                    if($scope.departments[i].no==x){
                        bool=true;
                        alert("Duplicate Department name");
                    }
                }
                if(bool==false){
                    $scope.count = $scope.departments.length + 1;
                    $scope.departments.push({no: $scope.count, code: deName, name: emp});
                }

            }
        };


        //Week3 - Remove Department
        $scope.remove = function (department) {
            $scope.departments.splice($scope.departments.indexOf(department), 1);
        };
        //Week3 - Selected Department
        /*******For clicking on View Department *********/
        $scope.selectedDepartment = function (Deptname) {
            for (var i = 0; i < $scope.departments.length; i++) {
                if ($scope.departments[i].name == Deptname) {
                    $scope.dept = angular.copy($scope.departments[i]);
                }
            }
        };

        /*Click on Edit
          input textbox will be set value by get department information.
          Then clicking on Edit will not affect to detail information part
        */
        $scope.selectedDepartmentByName = function (dept) {
            document.getElementById('idDept').value=dept.no+'';
            document.getElementById('nameDept').value=dept.code+'';
            document.getElementById('empDept').value=dept.name+'';
        };
       //Edit new
        //Edit
        $scope.curentDept={};
        $scope.editDept = function(id){
            for(var i = 0; i<$scope.departments.length; i++)
            {
                if($scope.departments[i].no == id)
                {
                    $scope.curentDept = angular.copy($scope.departments[i]);
                }
            }
        };

        //Close Edit new
        //Week3 - Update
        //Week6
        $scope.SaveUpdate = function (idDept, nameData, empData) {
            var x = document.getElementById("idDept").value;
            var y = document.getElementById("nameDept").value;
            var z = document.getElementById("empDept").value;
            // if(!isNaN(y)&&y!==""&&isNaN(z)) {
            for (var i = 0; i < $scope.departments.length; i++) {
                if ($scope.departments[i].no == x) {
                    $scope.departments[i].code = y;
                    $scope.departments[i].name = z;
                }
            }
            // }
        };
        $scope.read = function(id){
            for(var i=0;i<$scope.departments.length;i++){
                if($scope.departments[i].id==id){

                }
            }

        }
    }

    ]);





