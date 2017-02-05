(function () {
    'use strict';

    var webCampApp = angular.module('webCampApp', ['ngAnimate', 'ngRoute']);

    webCampApp.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.
            when('/SpeakerList', {
                templateUrl: '/app/WebCamp/Templates/SpeakerList.html',
                controller: 'webCampListController'
            }).
            when('/SpeakerDetails/:id', {
                templateUrl: '/app/WebCamp/Templates/SpeakerDetails.html',
                controller: 'webCampDetailsController'
            }).
            otherwise({
                redirectTo: '/SpeakerList'
            });
    }]);
})();