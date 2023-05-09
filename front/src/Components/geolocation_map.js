import { YMaps, Map, FullscreenControl, Placemark } from '@pbe/react-yandex-maps';
import './../assets/css/map_style.css'

export const GeoMap = ({latitude, longitude}) => {

    return(
        <>
            <YMaps>
                <div>
                    <Map defaultState={{ center: [55.81441, 49.12068], zoom: 9 }} >
                        <FullscreenControl options={{float: 'right'}} />
                        <Placemark geometry={[latitude, longitude]} />
                    </Map>
                </div>
            </YMaps>
        </>
    )
}