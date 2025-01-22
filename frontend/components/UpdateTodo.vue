<template>
    <div class="update-container">
        <h2 class="page-title">Update Todo</h2>
        <form @submit.prevent="updateTodo">
            <UInput v-model="todo.description" label="Description" required />
            <UInput v-model="todo.dueDate" label="Due Date" type="date" required />
            <UCheckbox v-model="todo.isCompleted" label="Completed" />
            <UInput v-model="todo.parentTodoId" label="Parent Todo ID" type="number" placeholder="Optional" />
            <UButton type="submit" color="emerald" size="md">Save</UButton>
            <UButton @click="navigateBack" color="red" size="md">Cancel</UButton>
        </form>
    </div>
</template>

<script setup lang="ts">
import { useRoute, useRouter } from 'vue-router';
const { customFetch } = useApi();
const route = useRoute();
const router = useRouter();

const todo = ref<any>({});

const fetchTodo = async () => {
    const id = route.query.id as string;
    try {
        const response = await customFetch<any>(`todo/${id}`, { method: 'GET' });
        todo.value = response;
    } catch (err) {
        console.log('Failed to fetch todo:', err);
    }
};

const updateTodo = async () => {
    try {
        await customFetch(`todo/${todo.value.id}`, {
            method: 'PUT',
            body: todo.value,
        });
        router.push('/todos');
    } catch (err) {
        console.log('Failed to update todo:', err);
    }
};

const navigateBack = () => {
    router.push('/todos');
};

onMounted(() => {
    fetchTodo();
});
</script>

<style scoped>
.update-container {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.page-title {
  text-align: center;
  font-size: 1.5rem;
  margin-bottom: 20px;
}
</style>
