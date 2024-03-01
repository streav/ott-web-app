<template>
  <Hero/>
  <CarouselPopularMovies :explore="false"/>
  <CarouselLatestMovies :explore="false"/>
  <TheFooter/>
</template>

<script setup lang="ts">
useHead({
  title: 'Movies'
})
</script>