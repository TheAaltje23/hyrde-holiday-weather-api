<template>
    <q-layout view="hHh Lpr lff">
        <HeaderComponent />
        <q-page-container>
            <router-view />
        </q-page-container>
    </q-layout>
</template>

<script>
import HeaderComponent from 'src/components/HeaderComponent.vue'
export default {
  components: {
    HeaderComponent
  }
}
</script>
