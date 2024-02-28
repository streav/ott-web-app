<template>
  <CarouselBase>
    <template #title>
      Popular Movies
    </template>
    <template #more>
      <NuxtLink to="/movie" n-link>
        Explore more
      </NuxtLink>
    </template>

    <div style="height: 500px" v-if="loading">
      <div class="i-svg-spinners:180-ring w-2em h-2em"></div>
    </div>

    <MediaCard
        v-else-if="result && result.data && result.data.length > 0"
        v-for="i of result?.data || []"
        :key="i.id"
        :item="i"
        type="movie"
        flex-1 w-40 md:w-60
    />

    <div class="py8" v-else>
      <p>Nothing to show.</p>
    </div>
  </CarouselBase>
</template>

<script setup lang="ts">
const result = ref()

const loading = ref(false)

onMounted(async () => {
  loading.value = true

  const {data, error} = await getPopularMovies()

  if (!error.value) {
    result.value = data.value
  }

  loading.value = false
})

</script>