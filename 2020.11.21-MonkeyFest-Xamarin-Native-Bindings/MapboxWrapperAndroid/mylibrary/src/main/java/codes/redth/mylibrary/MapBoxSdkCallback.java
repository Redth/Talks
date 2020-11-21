package codes.redth.mylibrary;

import com.mapbox.mapboxsdk.maps.MapboxMap;

public interface MapBoxSdkCallback {

    void mapReady();

    void markerClicked(String title);
}