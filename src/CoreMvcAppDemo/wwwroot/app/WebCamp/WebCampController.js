(function () {
    'use strict';

    angular.module('webCampApp')
        .controller('webCampController', webCampController)
        .controller('webCampListController', webCampListController)
        .controller('webCampDetailscontroller', webCampDetailsController);

    webCampController.$inject = ['$scope'];
    webCampListController.$inject = ['$scope', '$http'];
    webCampDetailsController.$inject = ['$scope', '$routeParams', '$http'];

    function webCampController($scope) {
        $scope.title = 'WebCamp 2016';

        activate();
        function activate(){}
    }

    function webCampListController($scope, $http) {
        $scope.title = 'Speaker List';

        $http.get('/api/WebCampService').then(function (data) {
            $scope.speakers = data.data;
        });

        activate();

        function activate() {}
    }

    function webCampDetailsController($scope, $routeParams, $http) {
        $scope.title = 'Speaker Details';

        $http.get('/api/WebCampService').then(function (data) {
            $scope.speakers = data.data;

            $scope.id = $routeParams.id;

            if ($routeParams.id > 0) {
                $scope.prev = Number($routeParams.id) - 1;
            }
            else {
                $scope.prev = $scope.speakers.length - 1;
            }

            if ($routeParams.id < $scope.speakers.length - 1) {
                $scope.next = Number($routeParams.id) + 1;
            }
            else {
                $scope.next = 0;
            }
        });

        activate();
        function activate(){}
    }
})();