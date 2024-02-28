<script setup lang="ts">
import type { Movie, Show, MediaType } from '~/types'

defineProps<{
  type: MediaType
  items: Movie[] | Show[]
}>()
</script>

<template>
  <CarouselBase>
    <MediaCard
      v-for="i of items"
      :key="i.id"
      :item="i"
      :type="type"
      flex-1 w-40 md:w-60
    />
  </CarouselBase>
</template>
