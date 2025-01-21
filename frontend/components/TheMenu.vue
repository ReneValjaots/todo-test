<template>
<UHorizontalNavigation :links="links" class="border-b border-gray-200 dark:border-gray-800" />
</template>

<script setup lang="ts">
import { UHorizontalNavigation } from '#components';

const links = [
  { label: "Home", to: "/"},
  { label: "My Todos", to:"/todos"},
  { label: "Create new Todo", to:"/create-todo"}
]


</script>


