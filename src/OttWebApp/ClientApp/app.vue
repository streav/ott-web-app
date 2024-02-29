<script setup>
import '@unocss/reset/tailwind.css'

useHead({
  htmlAttrs: {
    lang: 'en',
  },
  charset: 'utf-8',
  title: 'Streav OTT',
  titleTemplate: title => title !== 'Streav OTT' ? `${title} · Streav OTT` : title,
  meta: [
    {name: 'description', content: 'An OTT web app built with Streav to show the platform\'s capabilities.'}
  ]
})
</script>

<template>
  <NuxtLoadingIndicator/>
  <NuxtLayout>
    <NuxtPage/>
  </NuxtLayout>
</template>

<style>
html, body, #__nuxt {
  height: 100vh;
  margin: 0;
  padding: 0;
  background: #111;
  color: white;
  color-scheme: dark;
}

::-webkit-scrollbar {
  width: 14px;
  height: 18px;
}

::-webkit-scrollbar-thumb {
  height: 6px;
  border: 4px solid rgba(0, 0, 0, 0);
  background-clip: padding-box;
  background-color: rgba(215, 215, 215, 0.2);
  -webkit-border-radius: 7px;
  -webkit-box-shadow: inset -1px -1px 0 rgba(0, 0, 0, 0.05),
  inset 1px 1px 0 rgba(0, 0, 0, 0.05);
}

::-webkit-scrollbar-button {
  display: none;
  width: 0;
  height: 0;
}

::-webkit-scrollbar-corner {
  background-color: transparent;
}
</style>
