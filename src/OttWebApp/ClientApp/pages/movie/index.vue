<template>
  <Hero/>
  <CarouselMedia query="popular" />
  <CarouselMedia query="latest" />
  <TheFooter/>
</template>

<script setup lang="ts">
useHead({
  title: 'Movies'
})
</script>