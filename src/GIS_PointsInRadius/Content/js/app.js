var app = angular.module("gisApp", ["google-maps"]);
function gisCtrl($scope, $http) {
    $scope.query = {};
    $scope.submit = function() {
        $http({
            url: '/treasure',
            method:"GET",
            params:$scope.query
                
        }).then(function (response) {
            var markers = [];
            for (var i = 0; i < response.data.results.length; i++) {
                switch (response.data.results[i].t) {
                    case 0:
                        response.data.results[i].t = "Gold";
                        break;
                    case 1:
                        response.data.results[i].t = "Silver";
                        break;
                    case 2:
                        response.data.results[i].t = "Ooze Monster!!!";
                        break;
                    default:
                        response.data.results[i].t = "Empty :(";

                }

                markers.push({ latitude: response.data.results[i].l.coordinates[1], longitude: response.data.results[i].l.coordinates[0] });
            }
            $scope.markers = markers;
            $scope.center.latitude = $scope.query.latitude;
            $scope.center.longitude = $scope.query.longitude;
           
            $scope.treasures = response.data.results;
        });
    };
    angular.extend($scope, {
        center: {
            latitude: 0, // initial map center latitude
            longitude: 0 // initial map center longitude
        },
        markers: [], // an array of markers,
        fit:true,
        zoom: 3, // the zoom level
    });
   
}