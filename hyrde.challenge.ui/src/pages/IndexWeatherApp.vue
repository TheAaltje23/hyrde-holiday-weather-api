<template>
  <div class="q-pa-md page-container">
    <div class="q-gutter-y-md column" style="max-width: 300px">
      <q-input outlined v-model="text" bg-color="white" label="Enter a location">
        <template v-slot:prepend>
          <q-icon name="place" />
        </template>
        <template v-slot:append>
          <q-icon name="close" @click="clearText()" class="cursor-pointer" />
        </template>
      </q-input>
    </div>
    <q-btn id="search-btn" @click="fetchData" color="primary" label="Search" />
    <div id="output-wrapper" v-if="weatherToday">
      <div id="location-wrapper">
        <h2>{{weatherToday.data["city"]}}, {{weatherToday.data["region"]}}, {{weatherToday.data["country"]}}</h2>
      </div>
      <div id="condition-wrapper">
        <h3>{{weatherToday.data["conditionText"]}}</h3>
      </div>
      <div id="icon-temp-wrapper">
        <div><img id="weatherIcon" :src="weatherToday.data['conditionIcon']" alt="Weather Icon"></div>
        <div>{{weatherToday.data["tempCelcius"]}}°C</div>
      </div>
      <div id="info-wrapper">
        <div id="windspeed" class="infos">
          <div class="info-row">
            <div><q-icon color="primary" name="air" /></div>
            <div>
              <div id="info-title">Wind</div>
              <div id="info-data">{{weatherToday.data["windKph"]}}km/h</div>
            </div>
          </div>
        </div>
        <div id="winddegree" class="infos">
          <div class="info-row">
            <div><q-icon color="primary" name="explore" /></div>
            <div>
              <div id="info-title">Direction</div>
              <div id="info-data">{{weatherToday.data["windDir"]}}</div>
            </div>
          </div>
        </div>
        <div id= "precipitation" class="infos">
          <div class="info-row">
            <div><q-icon color="primary" name="water_drop" /></div>
            <div>
              <div id="info-title">Precipitation</div>
              <div id="info-data">{{weatherToday.data["precipitationMm"]}}mm</div>
            </div>
          </div>
        </div>
        <div id ="humidity" class="infos">
          <div class="info-row">
            <div><q-icon color="primary" name="water" /></div>
            <div>
              <div id="info-title">Humidity</div>
              <div id="info-data">{{weatherToday.data["humidity"]}}%</div>
            </div>
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

    const fetchData = async () => {
      try {
        const response = await axios.get(`http://localhost:5114/WeatherForecast/GetToday?query=${text.value}`)
        weatherToday.value = response.data
        console.log(response.data)
      } catch (error) {
        console.error('Error fetching data:', error)
      }
    }

    const clearText = () => {
      text.value = ''
      weatherToday.value = null
    }

    return {
      text,
      weatherToday,
      fetchData,
      clearText
    }
  }
}
</script>
