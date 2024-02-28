<template>
  <div style="height: 500px" v-if="loading">
    <div class="i-svg-spinners:180-ring w-2em h-2em"></div>
  </div>

  <MediaHero v-else-if="item" :item="item"/>
</template>

<script setup lang="ts">
const item = ref()

const loading = ref(false)

onMounted(async () => {
  loading.value = true

  const {data, error} = await getMostPopularMovie()

  if (!error.value) {
    item.value = data.value
  }

  loading.value = false
})
</script>