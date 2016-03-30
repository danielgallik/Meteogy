<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Meteogy._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-9">
            <h1>Map</h1>
            <div id="map"></div>
        </div>
        <div class="col-md-3">
            <h1>Parameters</h1>
            <form>
                <div class="form-group">
                    <label for="UI_Lat">Latitude</label>
                    <input type="text" class="form-control" id="UI_Lat" placeholder="Latitude">
                </div>
                <div class="form-group">
                    <label for="UI_Lng">Longitude</label>
                    <input type="text" class="form-control" id="UI_Lng" placeholder="Longitude">
                </div>
                <div class="form-group">
                    <label for="UI_Width">Width</label>
                    <input type="text" class="form-control" id="UI_Width" placeholder="Width">
                </div>
                <div class="form-group">
                    <label for="UI_Height">Height</label>
                    <input type="text" class="form-control" id="UI_Height" placeholder="Height">
                </div>
                <button type="submit" class="btn btn-default">Submit</button>
            </form>
        </div>
    </div>

    <script>
        var rectangle;
        var UI_Map;
        var centerLatLng = { lat: 48.6688584, lng: 19.6950909 };
        var height = 1.95;
        var width = 5.9;

        function calculateBounds() {
            return {
                south: centerLatLng.lat - height / 2,
                north: centerLatLng.lat + height / 2,
                west: centerLatLng.lng - width / 2,
                east: centerLatLng.lng + width / 2
            };
        }

        function updateForm() {
            document.getElementById('UI_Lat').value = centerLatLng.lat;
            document.getElementById('UI_Lng').value = centerLatLng.lng;
            document.getElementById('UI_Width').value = width;
            document.getElementById('UI_Height').value = height;
        }

        function initMap() {
            map = new google.maps.Map(document.getElementById('map'), {
                center: centerLatLng,
                zoom: 7,
                mapTypeId: google.maps.MapTypeId.TERRAIN
            });

            rectangle = new google.maps.Rectangle({
                bounds: calculateBounds(),
                editable: true,
                draggable: true
            });
            rectangle.setMap(map);
            rectangle.addListener('bounds_changed', function (event) {
                var ne = rectangle.getBounds().getNorthEast();
                var sw = rectangle.getBounds().getSouthWest();
                centerLatLng.lat = sw.lat() + height / 2;
                centerLatLng.lng = sw.lng() + width / 2;
                height = ne.lat() - sw.lat();
                width = ne.lng() - sw.lng(); 
                updateForm();
            });

            map.addListener('click', function (event) {
                centerLatLng.lat = event.latLng.lat();
                centerLatLng.lng = event.latLng.lng();
                var ne = rectangle.getBounds().getNorthEast();
                var sw = rectangle.getBounds().getSouthWest();
                height = ne.lat() - sw.lat();
                width = ne.lng() - sw.lng();
                rectangle.setBounds(calculateBounds());
                updateForm();
            });

            updateForm();
        }
    </script>
    <script src="https://maps.googleapis.com/maps/api/js?callback=initMap" async defer></script>

</asp:Content>
