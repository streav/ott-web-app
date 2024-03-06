<template>
  <div class="flex flex-col items-center gap-6">
    <div class="flex flex-col gap-3 lg:w-150 w-full">
      <label for="email">Email</label>
      <input v-model="email" type="email" id="email" placeholder="Email"
             class="p-2 border rounded-md focus:outline-none focus:ring focus:border-primary">
    </div>


    <div class="flex flex-col gap-3 lg:w-150 w-full">
      <label for="password">Password</label>
      <input v-model="password" type="password" id="password" placeholder="Password"
             class="p-2 border rounded-md focus:outline-none focus:ring focus:border-primary">
    </div>

    <button
        :disabled="loading"
        @click="submit"
        flex justify-center p="x6 y3" lg:w-150 w-full
        bg="primary hover:primary/70 disabled:gray/50" transition
        title="Sign up"
    >
      <i v-if="loading" class="i-svg-spinners:180-ring"></i>
      <span v-else>Sign up</span>
    </button>

    <div class="my-8">

      <p v-if="success" text-green>Your account has been created. You can
        <NuxtLink underline to="/login">login now</NuxtLink>
        .
      </p>

      <ul v-else class="text-red">
        <li v-for="error in errors">{{ error }}</li>
      </ul>
    </div>
  </div>
</template>

<script setup lang="ts">
const props = withDefaults(defineProps<{
  plan: string
}>(), {
  plan: 'basic'
})

const email = ref('')
const password = ref('')
const loading = ref(false)

const errors = ref([] as string[])
const success = ref(false)

async function submit() {
  success.value = false
  errors.value = []
  loading.value = true

  const {error} = await signUp(email.value, password.value, props.plan)

  if (error.value) {
    const validationErrors = error.value.data?.errors
    for (const prop in validationErrors) {
      if (validationErrors.hasOwnProperty(prop)) {
        errors.value.push(...validationErrors[prop] as string[])
      }
    }
  } else {
    success.value = true
  }

  loading.value = false
}
</script>