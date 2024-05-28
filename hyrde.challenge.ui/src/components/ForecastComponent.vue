<template>
    <div id="forecast-wrapper">
        <div id="forecast-items" v-for="(forecastItem, index) in forecastData.data" :key="index">
          <div class="forecast-day">{{ index === 0 ? 'Today' : index === 1 ? 'Tomorrow' : forecastItem["forecastDate"] }}
          </div>
          <div class="weather-icon"><img :src="forecastItem['conditionIcon']" alt="Weather Icon"></div>
          <div class="forecast-row">
            <div class="forecast-temp-max">{{ forecastItem["maxTemperature"] }}{{ unit === 'c' ? '°C' : '°F' }}</div>
            <div class="forecast-temp-min">{{ forecastItem["minTemperature"] }}{{ unit === 'c' ? '°C' : '°F' }}</div>
          </div>
          <div class="forecast-row forecast-chance">
            <div><q-icon class="info-icon" color="primary" name="water_drop" /></div>
            <div class="forecast-rain">{{ forecastItem["chanceOfRain"] }}%</div>
          </div>
        </div>
      </div>
</template>

<script>
export default {
  name: 'ForecastComponent',
  props: {
    forecastData: {
      type: Object,
      required: true
    },
    unit: {
      type: String,
      required: true
    }
  }
}
</script>
