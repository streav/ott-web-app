<template>
  <Hero/>
  <CarouselMedia query="popular" />
  <CarouselMedia query="latest" />
</template>

<script setup lang="ts">
useHead({
  title: 'Movies'
})
</script>