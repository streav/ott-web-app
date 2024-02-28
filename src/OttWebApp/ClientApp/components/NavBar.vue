<template>
  <div
    flex="~ row lg:col"
    justify-evenly items-center
    py5 lg:px5
    border="t lg:r base"
    bg-black
  >
    <NuxtLink v-slot="{ isActive }" to="/" title="Home">
      <div
        text-2xl
        :class="isActive ? 'i-ph-house-fill text-primary' : 'i-ph-house'"
      />
    </NuxtLink>
    <NuxtLink v-slot="{ isActive }" to="/movie" title="Movies">
      <div
        text-2xl
        :class="isActive ? 'i-ph-film-strip-fill text-primary' : 'i-ph-film-strip'"
      />
    </NuxtLink>
    <NuxtLink v-slot="{ isActive }" to="/show" title="Shows">
      <div
        text-2xl
        :class="isActive ? 'i-ph-video-fill text-primary' : 'i-ph-video'"
      />
    </NuxtLink>
    <NuxtLink v-slot="{ isActive }" to="/live" title="Live TV">
      <div
          text-2xl
          :class="isActive ? 'i-ph-television-simple-fill text-primary' : 'i-ph-television-simple'"
      />
    </NuxtLink>
  </div>
</template>
<script setup lang="ts">
</script>