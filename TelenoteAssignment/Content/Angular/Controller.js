app.controller("myCtrl", ['$scope', '$http', function ($scope, $http) {
    $scope.myThing = "Hello";


    $scope.states = {
        showForm: false
    };

    $scope.new = {
        Point: {}
    };

    $http.get("Distance/getCoordinate").success(function (vd) {
        $scope.model = vd;
        $scope.initialModel = angular.copy($scope.model);
    });

    $scope.showForm = function (show) {
        $scope.states.showForm = show;
    };

    $scope.addPoint = function (validX, validY) {
        if (validX && validY) {
            $http.post('/Distance/Add', $scope.new.Point).success(function (data) {
                $scope.model.cList.push(data);

                $scope.new.Point = {};
            })
        };
    };

    $scope.calculateDist = function () {
        $http.post('/Distance/Calculate', $scope.model.cList).success(function (distanceList) {
            $scope.distanceList = distanceList;
            $scope.states.showDistance = true;
        });
    };

    $scope.clear = function () {
        $scope.model = angular.copy($scope.initialModel);
        $scope.distanceList = {};
        $scope.states.showDistance = false;
    };

    $scope.deletePoint = function (index) {
        $scope.model.cList.push(index);
        $http.post('/Distance/Delete', $scope.model.cList).success(function (list) {
            $scope.model.cList = list;
            $scope.calculateDist();
        });
    };

}]);