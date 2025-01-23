<template>
    <div class="todo-container">
      <h2 class="page-title">Todo List</h2>
      <div class="controls-container">
      <UButton label="Create new" size="md" color="emerald" @click="navigateToCreatePage" />
    </div>
    <div class="search-container">
      <UTextarea
        v-model="searchQuery"
        color="emerald"
        size="xs"
        variant="outline"
        placeholder="Search by description..."
        :rows="1"
        style="width: 400px; min-width: 400px;" 
      />
    </div>
  
      <UTable :rows="paginatedTodos" :columns="columns">
        <template #actions-data="{ row }">
          <UButton @click="navigateToUpdatePage(row.id)" color="emerald" class="mr-2">Edit</UButton>
          <UButton @click="deleteTodo(row.id)" color="red">Delete</UButton>
        </template>
      </UTable>
      <UPagination
        v-model="page"
        :total="todos.length"
        :page-count="itemsPerPage"
        :max="10"
        class="pagination"
      />
    </div>
  </template>

<script setup lang="ts">
const { customFetch } = useApi();
const router = useRouter();

const todos = ref<any[]>([]);
const error = ref<string | null>(null);

const searchQuery = ref('');
const page = ref(1);
const itemsPerPage = ref(10);

const navigateToCreatePage = () => {
    router.push('/create-todo');
}

const navigateToUpdatePage = (id: number) => {
    router.push({path: '/update-todo', query: { id }});
}

const columns = [
    { key: 'description', label: 'Description'},
    { key: 'creationDate', label: 'Creation Date'},
    { key: 'dueDate', label: 'Due Date'},
    { key: 'isCompleted', label: 'Completed'},
    { key: 'parentTodoId', label: 'Parent Todo'},
    { key: "actions", label: "Actions" }
]

const fetchTodos = async () => {
    try {
        const response = await customFetch<any[]>('todo', {
            method: 'GET',
        });
        todos.value = response;
    }
    catch (err) {
        error.value = 'Failed to fetch todos: ' + (err as Error).message;
    }
}

const deleteTodo = async (id: number) => {
    try {
        await customFetch(`todo/${id}`, {
            method: 'DELETE'
        });
        todos.value = todos.value.filter(todo => todo.id != id); 
    } catch (error) {
        console.log('Failed to remove Todo: ', error);
    } 
}

const filteredTodos = computed(() => {
  if (!searchQuery.value) return todos.value;
  return todos.value.filter(todo => todo.description.toLowerCase().includes(searchQuery.value.toLowerCase()));
});

const paginatedTodos = computed(() => {
  const start = (page.value - 1) * itemsPerPage.value;
  return filteredTodos.value.slice(start, start + itemsPerPage.value);
});

onMounted(() => {
    fetchTodos();
})
</script>

<style scoped>
.todo-container {
  margin: 20px auto;
  padding: 15px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.page-title {
  text-align: center;
  font-size: 1.8rem;
  font-weight: bold;
  margin-bottom: 30px;
}

.controls-container {
  display: flex;
  justify-content: center;
  margin-bottom: 15px;
}

.search-container {
  display: flex;
  justify-content: center;
  margin-bottom: 20px;
}

.pagination {
  margin-top: 20px;
  display: flex;
  justify-content: center;
}
</style>