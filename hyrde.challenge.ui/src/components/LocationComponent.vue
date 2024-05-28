<template>
    <div id="location-wrapper">
        <h2>{{ locationData.data["city"] }}, {{ locationData.data["region"] }}, {{ locationData.data["country"] }}</h2>
    </div>
</template>

<script>
export default {
  name: 'LocationComponent',
  props: {
    locationData: {
      type: Object,
      required: true
    }
  }
}
</script>
