<template>
    <div class="home-page">
      <h1 class="title">Todo Application</h1>
      <router-link to="/todos">
        <button class="start-button">Go to Todos</button>
      </router-link>
    </div>
  </template>
  
  <script>
  export default {
    name: "HomePage",
  };
  </script>
  
  <style scoped>
  .home-page {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    height: 100vh;
    background-color: #f0f4f8;
    text-align: center;
  }
  
  .title {
    font-size: 3rem;
    color: #333;
    margin-bottom: 2rem;
  }
  
  .start-button {
    background-color: #007bff;
    color: white;
    border: none;
    padding: 1rem 2rem;
    font-size: 1.2rem;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
  }
  
  .start-button:hover {
    background-color: #0056b3;
  }
  </style>
  