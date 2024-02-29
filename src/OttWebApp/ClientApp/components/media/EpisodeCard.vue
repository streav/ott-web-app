<script setup lang="ts">
import type {ShowEpisode} from '~/types'

defineProps<{
  item: ShowEpisode,
  showId: number,
  seasonNumber: number
}>()
</script>

<template>
  <NuxtLink
      target="_blank"
      :to="`/watch/show/${showId}?season=${seasonNumber}&episode=${item.number}`" pb2
  >
    <div
        block bg-gray4:10 p1
        class="aspect-10/16"
        transition duration-400
        hover="scale-105 z10"
    >
      <img
          v-if="item.backdropUrl"
          width="400"
          height="600"
          :src="getTmdbImageUrl(item.backdropUrl)"
          :alt="item.name"
          w-full h-full object-cover
          :style="{ 'view-transition-name': `item-${item.number}` }"
      />
      <div v-else h-full op10 flex>
        <div i-ph:question ma text-4xl/>
      </div>
    </div>
    <div mt-2>
      S{{ seasonNumber }}E{{ item.number }}: {{ item.name }}
    </div>
  </NuxtLink>
</template>
