<script setup lang="ts">
import type { Movie, Show, MediaType } from '~/types'

defineProps<{
  type: MediaType
  item: Movie | Show
}>()
</script>

<template>
  <NuxtLink
    :to="`/${type}/${item.id}`" pb2
  >
    <div
      block bg-gray4:10 p1
      class="aspect-10/16"
      transition duration-400
      hover="scale-105 z10"
    >
      <NuxtImg
        v-if="item.posterUrl"
        width="400"
        height="600"
        format="webp"
        :src="`/tmdb${item.posterUrl}`"
        :alt="item.title"
        w-full h-full object-cover
        :style="{ 'view-transition-name': `item-${item.id}` }"
      />
      <div v-else h-full op10 flex>
        <div i-ph:question ma text-4xl />
      </div>
    </div>
    <div mt-2>
      {{ item.title }}
    </div>
    <div flex text-sm gap-2 items-center>
      <StarsRate w-20 :value="item.rating" />
      <div op60>
        {{ formatVote(item.rating) }}
      </div>
    </div>
  </NuxtLink>
</template>
