<template>
  <div class="q-pa-md input-container">
    <div class="q-gutter-y-md column" style="max-width: 300px">
      <q-input outlined bottom-slots v-model="text" label="Enter a city name">
        <template v-slot:prepend>
          <q-icon name="place" />
        </template>
        <template v-slot:append>
          <q-icon name="close" @click="text = ''" class="cursor-pointer" />
        </template>
      </q-input>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'

export default {
  setup () {
    return {
      text: ref(''),
      ph: ref('')
    }
  }
}
</script>
