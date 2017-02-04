(function () {
    'use strict';

    angular.module('techApp').controller('techController', techController);
    techController.$inject = ['$scope', '$http'];

    function techController($scope, $http) {
        $scope.title = 'Tech List';

        $http.get("/api/apiteches").then(function (result) {
            $scope.classes = result.data;
            //console.log($scope.teches);
        });

        $scope.add = function () {
            $http.post("/api/apiteches", this.NewTech).then(function (data) {
                $scope.teches.push(data);
            });
        };

        activate();
        function activate(){}
    }
})();