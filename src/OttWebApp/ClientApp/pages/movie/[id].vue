<script setup lang="ts">
import type { Movie } from '~/types'

const route = useRoute()

const {data} = await getMovie(route.params.id as number)

const item = ref(data.value as Movie)

useHead({
  title: item.value.title,
  meta: [
    { name: 'description', content: item.value.overview }
  ],
})
</script>

<template>
  <div>
    <MediaHero :item="item" />
    <MediaDetails :item="item" type="movie" />
  </div>
</template>