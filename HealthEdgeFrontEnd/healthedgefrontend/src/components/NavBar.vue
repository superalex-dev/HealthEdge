<template>
  <nav class="navbar">
    <div class="container">
      <div class="navbar-brand">
        <router-link to="/" class="logo">HealthEdge</router-link>
      </div>
      <ul class="navbar-menu">
        <li><router-link to="/">Home</router-link></li>
        <li><router-link to="/mppmcden">Mppmcden</router-link></li>
        <li><router-link to="/phioce">Phioce</router-link></li>
      </ul>
    </div>
  </nav>
</template>

<script setup>
import { RouterLink } from "vue-router";
</script>

<style scoped>
.navbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1rem 2rem;
  background-color: #ffffff;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);

.container {
  display: flex;
  justify-content: space-between;
  align-items: center;
  max-width: 1200px;
  margin: 0 auto;
}

.navbar-brand .logo {
  font-size: 1.5rem;
  font-weight: bold;
  color: #333;
  text-decoration: none;
}

.navbar-menu {
  list-style: none;
  display: flex;
  gap: 1rem;
}

.navbar-menu li a {
  color: #333;
  text-decoration: none;
  padding: 0.5rem 1rem;
  transition: color 0.3s;
}

.navbar-menu li a:hover {
  color: #ff5733;
}
}
</style>