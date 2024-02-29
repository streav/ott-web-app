<script setup lang="ts">
import type {Movie, Show, MediaType} from '~/types'

defineProps<{
  item: Movie | Show
  type: MediaType
}>()

const tab = ref<'overview' | 'casts'>('overview')
</script>

<template>
  <div flex items-center justify-center gap8 py6>
    <button n-tab :class="{ 'n-tab-active': tab === 'overview' }" @click="tab = 'overview'">
      Overview
    </button>
    <button v-if="item.casts?.length" n-tab :class="{ 'n-tab-active': tab === 'casts' }" @click="tab = 'casts'">
      Casts
    </button>
  </div>
  <MediaInfo v-if="tab === 'overview'" :item="item" :type="type"/>
  <CarouselBase v-if="tab === 'casts' && item.casts?.length">
    <PersonCard
        v-for="i of item.casts"
        :key="i.id"
        :item="i"
        flex-1 w-50
    />
  </CarouselBase>
</template>
