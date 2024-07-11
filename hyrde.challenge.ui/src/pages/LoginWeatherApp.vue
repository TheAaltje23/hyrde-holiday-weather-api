<template>
  <div class="page-container">

    <q-card class="login-card">
      <q-card-section>
        <div class="q-pa-md">
          <q-form @submit="onSubmit" @reset="onReset" class="q-gutter-md">

            <q-input bg-color="white" filled v-model="username" label="Your username" lazy-rules
              :rules="[val => val && val.length > 0 || 'Please type something']" />

            <q-input bg-color="white" filled type="password" v-model="password" label="Your password" lazy-rules
              :rules="[val => val !== null && val !== '' || 'Please type your password']" />

            <div>
              <q-btn label="Login" type="submit" color="primary" />
              <q-btn label="Reset" type="reset" color="white" flat class="q-ml-sm" />
            </div>

          </q-form>
        </div>
      </q-card-section>
    </q-card>
  </div>
</template>

<script>
import { ref } from 'vue'
import axios from 'axios'

export default {
  setup () {
    const username = ref(null)
    const password = ref(null)

    const onSubmit = async () => {
      try {
        const responseLogin = await axios.post('http://localhost:5114/user/login', {
          username: username.value,
          password: password.value
        })
        console.log(responseLogin.data)
      } catch (error) {
        console.error(error.response.data)
      }
    }

    const onReset = () => {
      username.value = null
      password.value = null
    }

    return {
      username,
      password,
      onSubmit,
      onReset
    }
  }
}
</script>
