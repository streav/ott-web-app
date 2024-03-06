<template>
  <Hero type="show"/>
  <CarouselMedia type="show" query="popular" />
  <CarouselMedia type="show" query="latest" />
</template>

<script setup lang="ts">
useHead({
  title: 'Shows'
})
</script>