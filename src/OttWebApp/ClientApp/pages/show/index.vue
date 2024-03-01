<template>
  <Hero type="show"/>
  <CarouselMedia type="show" query="popular" />
  <CarouselMedia type="show" query="latest" />
  <TheFooter/>
</template>

<script setup lang="ts">
useHead({
  title: 'Shows'
})
</script>