<script setup lang="ts">
import type {Person} from '~/types'
import {formatDate} from '~/composables/utils'

const props = defineProps<{
  item: Person
}>()
</script>

<template>
  <div p4 grid="~ md:cols-[max-content_1fr]" gap-8 items-center ma max-w-300>
    <img
        v-if="props.item.profilePictureUrl"
        width="400"
        height="600"
        :src="getTmdbImageUrl(props.item.profilePictureUrl)"
        :alt="props.item.name"
        block border="4 gray4/10" w-70 md:90 self-start mt-5 mx-auto
        transition duration-400 object-cover aspect="3/4"
    />
    <div p4 gap8>
      <div>
        <h2 text-3xl mb4>
          {{ props.item.name }}
        </h2>

        <div v-if="props.item.biography" font-sans ws-pre-wrap op80 leading-relaxed v-text="props.item.biography"/>
        <div v-else op50 italic>
          (no biography)
        </div>
      </div>

      <div text-sm op80>
        <ul grid="~ cols-[max-content_1fr] gap3" items-center>
          <template v-if="props.item.knownFor">
            <div op60>
              Known for
            </div>
            <div>
              {{ props.item.knownFor }}
            </div>
          </template>
          <template v-if="props.item.birthPlace">
            <div op60>
              Place of birth
            </div>
            <div>
              {{ props.item.birthPlace }}
            </div>
          </template>

          <template v-if="props.item.birthDate">
            <div op60>
              Birthday
            </div>
            <div>
              {{ formatDate(props.item.birthDate) }}
            </div>
          </template>
        </ul>
      </div>
    </div>
  </div>
</template>
