<template>
  <div class="q-pa-md input-container">
    <div class="q-gutter-y-md column" style="max-width: 300px">
      <q-input outlined bottom-slots v-model="text" label="Enter a city name">
        <template v-slot:prepend>
          <q-icon name="place" />
        </template>
        <template v-slot:append>
          <q-icon name="close" @click="clearText()" class="cursor-pointer" />
        </template>
      </q-input>
    </div>
    <q-btn id="search-btn" @click="fetchData" color="primary" label="Search" />
    <!--LIST 1-->
    <div id="list-container" v-if="weatherToday">
      <q-list id="list" bordered>
        <q-item clickable v-ripple>
          <q-item-section avatar>
            <q-icon color="primary" name="thermostat"/>
          </q-item-section>
          <q-item-section>{{weatherToday.data["tempCelcius"]}} degrees Celcius</q-item-section>
        </q-item>
      </q-list>
      <q-list id="list" bordered>
        <q-item clickable v-ripple v-if="weatherToday">
          <q-item-section id="iconAndText"><img id="weatherIcon" :src="weatherToday.data['conditionIcon']" alt="Weather Icon">{{weatherToday.data["conditionText"]}}</q-item-section>
        </q-item>
      </q-list>
    </div>
    <!--LIST 2-->
    <div id="list-container" v-if="weatherToday">
      <q-list id="list" bordered>
        <q-item clickable v-ripple>
          <q-item-section avatar>
            <q-icon color="primary" name="air" />
          </q-item-section>
          <q-item-section>Wind: {{weatherToday.data["windKph"]}} km/h</q-item-section>
        </q-item>
      </q-list>
      <q-list id="list" bordered>
        <q-item clickable v-ripple v-if="weatherToday">
          <q-item-section avatar>
            <q-icon color="primary" name="water_drop" />
          </q-item-section>
          <q-item-section>Precipitation: {{weatherToday.data["percipitationMm"]}}mm</q-item-section>
        </q-item>
      </q-list>
      <q-list id="list" bordered>
        <q-item clickable v-ripple v-if="weatherToday">
          <q-item-section avatar>
            <q-icon color="primary" name="water" />
          </q-item-section>
          <q-item-section>Humidity: {{weatherToday.data["humidity"]}}%</q-item-section>
        </q-item>
      </q-list>
    </div>
    <!--LIST 3-->
    <div id="list-container" v-if="weatherToday">
      <q-list id="list" bordered>
        <q-item clickable v-ripple>
          <q-item-section avatar>
            <q-icon color="primary" name="place" />
          </q-item-section>
          <q-item-section> {{weatherToday.data["city"]}}, {{weatherToday.data["country"]}}</q-item-section>
        </q-item>
      </q-list>
      <q-list id="list" bordered>
        <q-item clickable v-ripple v-if="weatherToday">
          <q-item-section avatar>
            <q-icon color="primary" name="calendar_today" />
          </q-item-section>
          <q-item-section>{{weatherToday.data["localTime"]}}</q-item-section>
        </q-item>
      </q-list>
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
        const response = await axios.get(`http://localhost:5114/WeatherForecast/GetToday?cityName=${text.value}`)
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
