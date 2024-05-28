<template>
  <div id="hourly-table-wrapper" class="q-pa-md">
    <q-table
      class="hourly-table"
      table-header-class="hourly-header"
      dense
      :rows-per-page-options="[0]"
      hide-bottom
      flat bordered
      :rows="rows"
      :columns="columns"
      row-key="hour"
      style="height: 400px"
    />
  </div>
</template>

<script>
import { h, computed } from 'vue'

export default {
  name: 'HourlyTable',
  props: {
    hourlyData: {
      type: Object,
      required: false
    },
    unit: {
      type: String,
      required: false
    }
  },
  setup (props) {
    const columns = [
      {
        name: 'hour',
        required: true,
        label: 'Hour',
        align: 'left',
        field: row => row.time,
        format: val => `${val}`,
        sortable: true
      },
      { name: 'icon', align: 'center', label: 'Icon', field: 'icon', format: val => h('img', { src: val, alt: 'Weather Icon' }), sortable: true },
      { name: 'temp', align: 'center', label: 'Temperature', field: 'temp', sortable: true },
      { name: 'chanceOfRain', align: 'center', label: 'Chance of Rain', field: 'chanceOfRain', format: val => `${val}%`, sortable: true },
      { name: 'precip', align: 'center', label: 'Precipitation', field: 'precip', format: val => `${val} mm`, sortable: true },
      { name: 'wind', align: 'center', label: 'Wind', field: 'wind', sortable: true },
      { name: 'windDir', align: 'center', label: 'Wind Direction', field: 'windDir', sortable: true }
    ]

    const rows = computed(() => {
      return props.hourlyData.data.map(item => ({
        time: item.forecastHour,
        icon: item.conditionIcon,
        temp: item.temperature + `${props.unit === 'c' ? '°C' : '°F'}`,
        chanceOfRain: item.chanceOfRain,
        precip: item.precipitationMm,
        wind: item.wind + ` ${props.unit === 'c' ? 'km/h' : 'mph'}`,
        windDir: item.windDir
      }))
    })

    return {
      columns,
      rows
    }
  }
}
</script>
