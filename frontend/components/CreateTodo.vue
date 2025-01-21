<template>
    <div class="create-todo-container">
      <h2>Create New Todo</h2>
      <form @submit.prevent="submitTodo">
        <div>Description</div>
        <UInput v-model="newTodo.description" label="Description" placeholder="Enter description" required />
        
        <div>Due Date</div>
        <UInput v-model="newTodo.dueDate" label="Due Date" type="date" required />
        
        <div>Parent Todo ID (optional)</div>
        <UInput v-model="newTodo.parentTodoId" label="Parent Todo ID (optional)" type="number" placeholder="Enter parent todo ID if any" />
        
        <UButton type="submit" color="emerald">Create Todo</UButton>
      </form>
    </div>
  </template>
  
  <script setup lang="ts">
  import { ref } from 'vue';
  import { useRouter } from 'vue-router';
  const { customFetch } = useApi();
  
  const router = useRouter();
  
  const newTodo = ref({
    description: '',
    creationDate: new Date(), 
    dueDate: '',  
    isCompleted: false,
    parentTodoId: undefined as number | undefined ,
  });
  
  const submitTodo = async () => {
    const payload = {
      description: newTodo.value.description,    
      creationDate: newTodo.value.creationDate.toISOString(),  
      dueDate: newTodo.value.dueDate,    
      isCompleted: newTodo.value.isCompleted,  
      parentTodoId: newTodo.value.parentTodoId,  
    };
  
    try {
      await customFetch('todo', {
        method: 'POST',
        body: JSON.stringify(payload),
      });
      console.log('New Todo created:', payload);
      router.push('/todos');  // Redirect to the todos page after successful creation
    } catch (error) {
      console.error('Failed to create new todo:', error);
    }
  
    // Reset the form after submission
    newTodo.value = {
      description: '',
      creationDate: new Date(),
      dueDate: '',
      isCompleted: false,
      parentTodoId: undefined,
    };
  };
  </script>
  
  <style scoped>
  .create-todo-container {
    max-width: 600px;
    margin: 0 auto;
    padding: 20px;
    background-color: #fff;
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  }
  
  h2 {
    text-align: center;
    margin-bottom: 20px;
  }
  
  form {
    display: flex;
    flex-direction: column;
    gap: 15px;
  }
  </style>
  