<script setup lang="ts">
import type { Cast, Person } from '~/types'

defineProps<{
  item: Person | Cast
}>()
</script>

<template>
  <NuxtLink :to="`/person/${item.id}`">
    <div
      block bg-gray4:10 p1
      class="aspect-10/16"
      transition duration-400
      hover="scale-105 z10"
    >
      <img
        v-if="item.profilePictureUrl"
        width="500"
        height="800"
        :src="getTmdbImageUrl(item.profilePictureUrl)"
        :alt="item.name"
        w-full h-full object-cover
      />
      <div v-else h-full op10>
        <div i-ph:user ma text-4xl />
      </div>
    </div>
    <div mt-2>
      {{ item.name }}
    </div>
    <div op50>
      {{ item.character || item.knownFor }}
    </div>
  </NuxtLink>
</template>
