<template>
  <div flex justify-center items-center w-full h-full p2>
    <div flex flex-col gap6 block bg-gray4:10 p8 lg:w-100 w-full rounded-2>
      <h1 text-center text-2xl>
        Login
      </h1>

      <div class="flex flex-col gap-3 w-full">
        <label for="email">Email</label>
        <input v-model="email" type="email" id="email" placeholder="Email"
               class="p-2 border rounded-md focus:outline-none focus:ring focus:border-primary">
      </div>


      <div class="flex flex-col gap-3 w-full">
        <label for="password">Password</label>
        <input v-model="password" type="password" id="password" placeholder="Password"
               class="p-2 border rounded-md focus:outline-none focus:ring focus:border-primary">
      </div>

      <button
          :disabled="loading"
          @click="submit"
          flex justify-center p="x6 y3" w-full mt-4
          bg="primary hover:primary/70 disabled:gray/50" transition
          title="Login"
      >
        <i v-if="loading" class="i-svg-spinners:180-ring"></i>
        <span v-else>Login</span>
      </button>

      <p class="text-red text-center py-2">
        {{ error }}
      </p>

      <p>New to here?
        <NuxtLink underline to="/sign-up">Sign up now.</NuxtLink>
      </p>

    </div>
  </div>
</template>

<script setup lang="ts">
const {signIn} = useAuth()

const email = ref('')
const password = ref('')
const loading = ref(false)

const error = ref('')

async function submit() {
  error.value = ''
  loading.value = true

  try {
    await signIn({email: email.value, password: password.value}, {callbackUrl: '/'})
  } catch (e) {
    console.dir(e)
    error.value = 'Wrong email or password.'
  }

  loading.value = false
}

definePageMeta({
  middleware: 'auth',
  auth: {
    unauthenticatedOnly: true,
    navigateAuthenticatedTo: '/',
  }
})

useHead({
  title: 'Login'
})
</script>