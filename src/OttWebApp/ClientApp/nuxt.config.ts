export default defineNuxtConfig({
    ssr: false,
    devServer: {host: 'localhost'},
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
        '@unocss/nuxt',
        '@sidebase/nuxt-auth'
    ],
    css: ['~/assets/css/main.css', 'video.js/dist/video-js.css'],
    experimental: {
        inlineSSRStyles: false,
        viewTransition: true,
        renderJsonPayloads: true,
    },
    auth: {
        provider: {
            type: 'local',
            endpoints: {
                getSession: {path: '/info', method: 'get'}
            },
            token: {
                signInResponseTokenPointer: '/accessToken',
                sameSiteAttribute: false,
                maxAgeInSeconds: 3600
            }
        }
    }
})
