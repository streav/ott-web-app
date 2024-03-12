<script setup>
const error = useError()

const message = computed(() => String(error.value?.message || ''))
const is404 = computed(() => error.value?.statusCode === 404 || message.value?.includes('404'))
const isDev = process.dev

function handleError() {
  return clearError({redirect: '/'})
}
</script>

<template>
  <div flex="~ col" h-screen text-center items-center justify-center gap4>
    <div text-3xl>
      {{ is404 ? 'This page could not be found' : 'An error occurred' }}
    </div>
    <div text-xl op50>
      Looks like you've followed a broken link or entered a URL that doesn't exist on this site.
    </div>
    <pre v-if="isDev">{{ error }}</pre>
    <button border px4 py1 rounded hover:text-gray:50 hover:border-gray:50 @click="handleError">
      Back to Home
    </button>
  </div>
</template>
