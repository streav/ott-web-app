export default defineNuxtConfig({
  ssr: false,
  devServer: { host: 'localhost' },
  nitro: {
    devProxy: {
      '/api': {
        target: 'http://localhost:5251/api',
        changeOrigin: true,
        secure: false
      }
    }
  },
  modules: [
    '@vueuse/nuxt',
    '@unocss/nuxt'
  ],
  experimental: {
    inlineSSRStyles: false,
    viewTransition: true,
    renderJsonPayloads: true,
  }
})
