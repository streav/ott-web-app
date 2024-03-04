<template>
  <div
      flex="~ row lg:col"
      justify-evenly items-center
      py3 lg:px3
      border="t lg:r base"
      bg-black
  >
    <NuxtLink v-for="item in items" v-slot="{ isActive }" :to="item.to" :title="item.title">
      <div flex flex-col items-center>
        <div
            text-2xl
            :class="isActive ? item.iconActive: item.icon"
        />
        <div font-bold :class="isActive ? 'text-primary': ''">
          {{ item.title }}
        </div>
      </div>
    </NuxtLink>
  </div>
</template>
<script setup lang="ts">
const items = [
  {
    icon: 'i-ph-house',
    iconActive: 'i-ph-house-fill text-primary',
    title: 'Home',
    to: '/'
  },
  {
    icon: 'i-ph-film-strip',
    iconActive: 'i-ph-film-strip-fill text-primary',
    title: 'Movies',
    to: '/movie'
  },
  {
    icon: 'i-ph-video',
    iconActive: 'i-ph-video-fill text-primary text-primary',
    title: 'Shows',
    to: '/show'
  },
  {
    icon: 'i-ph-television-simple',
    iconActive: 'i-ph-television-simple-fill text-primary',
    title: 'Live',
    to: '/live'
  }
]
</script>