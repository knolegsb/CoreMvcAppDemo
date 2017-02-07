(function () {
    'use strict';

    // [1] Angular 모듈 선언
    var app = angular.module('DotNetNoteApp', []);

    // [2][1] Angular 컨트롤러 선언
    app.controller('RecentlyNoteListWithAngular', ['$scope', '$http', noteController]);

    // [2][2] 컨트롤러 구현부
    function noteController($scope, $http) {
        // Web API 주소: ASP.NET Web API
        var API_URL = "/api/NoteService/";

        // 출력: get
        $http.get(API_URL).then(function (data) {
            $scope.notes = data.data;
        })
        .error(function () {
            $scope.error = "데이터를 가져오는 동안 에러가 발생했습니다.";
        });
    }

    // [3][1] Angular 컨트롤러 선언
    app.controller('RecentlyCommentListWithAngular', ['$scope', '$http', commentController]);

    // [3][2] 컨트롤러 구현부
    function commentController($scope, $http) {
        // Web API 주소: ASP.NET Web API로 구현
        var API_URL = "/api/NoteCommentService/";

        // 출력: get
        $http.get(API_URL).then(function (data) {
            $scope.comments = data.data;
        })
        .error(function () {
            $scope.error = "데이터를 가져오는 동안 에러가 발생했습니다.";
        });
    }
})();