<template>
    <div class="update-container">
        <h2 class="page-title">Update Todo</h2>
        <form @submit.prevent="updateTodo">
            <div class="form-group">
                <div>Description</div>
                <UInput v-model="todo.description" label="Description" required />
            </div>
            <div class="form-group">
                <div>Due date</div>
                <UInput v-model="todo.dueDate" label="Due Date" type="datetime-local" required />
            </div>
            <div class="form-group">
                <UCheckbox v-model="todo.isCompleted" label="Completed" />
            </div>
            <div class="form-group">
                <div>Parent Todo ID (optional)</div>
                <UInput v-model="todo.parentTodoId" label="Parent Todo ID" type="number"
                    placeholder="Parent Todo Id (optional)" :min="1" />
            </div>
            <div class="button-group">
                <UButton type="submit" color="emerald" size="md">Save</UButton>
                <UButton @click="navigateBack" color="red" size="md">Cancel</UButton>
            </div>
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
    max-width: 600px;
    margin: 40px auto;
    padding: 30px;
    background-color: #f9f9f9;
    border-radius: 10px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.page-title {
    text-align: center;
    font-size: 1.8rem;
    font-weight: bold;
    margin-bottom: 30px;
}

.form-group {
    margin-bottom: 20px;
}

.button-group {
    display: flex;
    justify-content: space-between;
    margin-top: 20px;
}
</style>
