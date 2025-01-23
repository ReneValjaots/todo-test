<template>
  <div class="details-container">
    <h2 class="page-title">Details Page</h2>

    <div class="table-container">
      <h3>Parent Todo Data</h3>
      <table class="todo-table">
        <thead>
          <tr>
            <th>Description</th>
            <th>Creation Date</th>
            <th>Due Date</th>
            <th>Completed</th>
            <th>Parent Todo ID</th>
          </tr>
        </thead>
        <tbody>
          <tr v-if="todo">
            <td>{{ todo.description }}</td>
            <td>{{ todo.creationDate }}</td>
            <td>{{ todo.dueDate }}</td>
            <td>{{ todo.isCompleted }}</td>
            <td>{{ todo.parentTodoId || '-' }}</td>
          </tr>
        </tbody>
      </table>
    </div>

    <h3>Sub todos</h3>
    <UTable :rows="subtodos" :columns="columns">
      <template #actions-data="{ row }">
        <UButton @click="navigateToDetailsPage(row.id)" color="sky">Details</UButton>
      </template>
    </UTable>

    <div class="button-group">
      <UButton @click="navigateBack" color="red" size="md">Back</UButton>
    </div>
  </div>
</template>

<script setup lang="ts">
import { UTable } from '#components';
import { useRoute, useRouter } from 'vue-router';
const { customFetch } = useApi();
const route = useRoute();
const router = useRouter();

const todo = ref<any>();
const subtodos = ref<any[]>([]);

const columns = [
  { key: 'description', label: 'Description' },
  { key: 'creationDate', label: 'Creation Date' },
  { key: 'dueDate', label: 'Due Date' },
  { key: 'isCompleted', label: 'Completed' },
  { key: 'parentTodoId', label: 'Parent Todo' },
  { key: "actions", label: "Actions" }
];

const fetchTodo = async () => {
  try {
    const response = await customFetch<any>(`todo/${route.params.id}`, { method: 'GET' });
    todo.value = response;
    console.log(response);
  } catch (err) {
    console.log('Failed to fetch todo:', err);
  }
};

const fetchSubtodos = async () => {
  try {
    const todoResponse = await customFetch<any>(`todo/${route.params.id}/subtodos`, { method: 'GET' });
    subtodos.value = todoResponse.subTodos;
    console.log(todoResponse.subtodos);
  } catch (err) {
    console.log('Failed to fetch subtodos:', err);
  }
};

const navigateToDetailsPage = (id: number) => {
  router.push(`/details/${id}`);
}

const navigateBack = () => {
  router.push('/todos');
};

onMounted(() => {
  fetchTodo();
  fetchSubtodos();
});
</script>

<style scoped>
.details-container {
  margin: 40px auto;
  padding: 30px;
  border-radius: 10px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.page-title {
  text-align: center;
  font-size: 1.8rem;
  font-weight: bold;
  margin-bottom: 30px;
}

.table-container {
  margin-bottom: 30px;
}

.todo-table {
  width: 100%;
  border-collapse: collapse;
}

.todo-table th,
.todo-table td {
  padding: 10px;
  border: 1px solid #ddd;
  text-align: left;
}

.button-group {
  display: flex;
  justify-content: center;
  margin-top: 20px;
}
</style>
