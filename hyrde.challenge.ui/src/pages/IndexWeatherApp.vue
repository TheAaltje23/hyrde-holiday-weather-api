<template>
  <div class="q-pa-md page-container">
    <div id="input-btn-container">
      <div class="q-gutter-y-md column input-container" style="max-width: 300px">
        <q-input outlined rounded v-model="text" bg-color="white" label="Enter a location" @keyup.enter="fetchWeather">
          <template v-slot:prepend>
            <q-icon name="place" />
          </template>
          <template v-slot:append>
            <q-icon name="close" @click="clearText()" class="cursor-pointer" />
          </template>
        </q-input>
      </div>
      <q-btn round id="search-btn" color="primary" icon="search" @click="fetchWeather" />
    </div>
    <div id="toggle-container" v-if="weatherToday || weatherForecast">
      <q-btn-toggle
        v-model="model"
        class="today-forecast-toggle"
        no-caps
        rounded
        unelevated
        toggle-color="positive"
        color="primary"
        text-color="white"
        :options="[
          {label: 'Today', value: 'today'},
          {label: 'Forecast', value: 'forecast'}
        ]"
      />
    </div>
    <div id="output-wrapper-today" v-if="weatherToday && model === 'today'">
      <div id="location-wrapper">
        <h2>{{weatherToday.data["city"]}}, {{weatherToday.data["region"]}}, {{weatherToday.data["country"]}}</h2>
      </div>
      <div id="condition-wrapper">
        <h3>{{weatherToday.data["conditionText"]}}</h3>
      </div>
      <div id="icon-temp-wrapper">
        <div class="weather-icon"><img :src="weatherToday.data['conditionIcon']" alt="Weather Icon"></div>
        <div>{{weatherToday.data["tempCelcius"]}}°C</div>
      </div>
      <div id="max-min-temp-wrapper" v-if="weatherForecast && weatherForecast.data.length > 0">
        <div id="today-temp-max">Max: {{weatherForecast.data[0]["maxTempCelcius"]}}°C</div>
        <div id="today-temp-min">Min: {{weatherForecast.data[0]["minTempCelcius"]}}°C</div>
      </div>
      <div id="info-wrapper">
        <div id="windspeed" class="infos">
          <div class="info-row">
            <div><q-icon class="info-icon" color="primary" name="air" /></div>
            <div>
              <div class="info-title">Wind</div>
              <div class="info-data">{{weatherToday.data["windKph"]}}km/h</div>
            </div>
          </div>
        </div>
        <div id="winddegree" class="infos">
          <div class="info-row">
            <div><q-icon class="info-icon" color="primary" name="explore" /></div>
            <div>
              <div class="info-title">Direction</div>
              <div class="info-data">{{weatherToday.data["windDir"]}}</div>
            </div>
          </div>
        </div>
        <div id= "precipitation" class="infos">
          <div class="info-row">
            <div><q-icon class="info-icon" color="primary" name="water_drop" /></div>
            <div>
              <div class="info-title">Precipitation</div>
              <div class="info-data">{{weatherToday.data["precipitationMm"]}}mm</div>
            </div>
          </div>
        </div>
        <div id ="humidity" class="infos">
          <div class="info-row">
            <div><q-icon class="info-icon" color="primary" name="water" /></div>
            <div>
              <div class="info-title">Humidity</div>
              <div class="info-data">{{weatherToday.data["humidity"]}}%</div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div id="output-wrapper-forecast" v-if="weatherForecast && model === 'forecast'">
      <div id="location-wrapper">
        <h2>{{weatherToday.data["city"]}}, {{weatherToday.data["region"]}}, {{weatherToday.data["country"]}}</h2>
      </div>
      <div id="forecast-wrapper">
        <div id="forecast-items" v-for="(forecastItem, index) in weatherForecast.data" :key="index">
          <div class="forecast-day">{{index === 0 ? 'Today' : index === 1 ? 'Tomorrow' : forecastItem["forecastDate"]}}</div>
          <div class="weather-icon"><img :src="forecastItem['conditionIcon']" alt="Weather Icon"></div>
          <div class="forecast-temp">
            <div class="forecast-temp-max">{{forecastItem["maxTempCelcius"]}}°C</div>
            <div class="forecast-temp-min">{{forecastItem["minTempCelcius"]}}°C</div>
          </div>
        </div>
      </div>
    </div>

  </div>
</template>

<script>
import { ref } from 'vue'
import axios from 'axios'

export default {
  setup () {
    const text = ref('')
    const weatherToday = ref(null)
    const weatherForecast = ref(null)
    const model = ref('today')

    const fetchWeather = async () => {
      try {
        const responseToday = await axios.get(`http://localhost:5114/WeatherForecast/GetToday?query=${text.value}`)
        const responseForecast = await axios.get(`http://localhost:5114/WeatherForecast/GetForecast?query=${text.value}`)
        weatherToday.value = responseToday.data
        weatherForecast.value = responseForecast.data
        console.log(responseToday.data)
        console.log(responseForecast.data)
      } catch (error) {
        console.error('Error fetching data:', error)
      }
    }

    const clearText = () => {
      text.value = ''
      weatherToday.value = null
      weatherForecast.value = null
    }

    return {
      text,
      weatherToday,
      weatherForecast,
      model,
      fetchWeather,
      clearText
    }
  }
}
</script>
