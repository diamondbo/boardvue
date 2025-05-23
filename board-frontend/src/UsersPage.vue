<template>
 <div class = "btn-and-form-wrapper">
  <h2>Users</h2>
  <button class="add-button" @click="showForm = !showForm">➕ Add</button>

  <div class="form-container" v-show="showForm">
    <form @submit.prevent="submitForm">
      <label for="name">Name:</label>
      <input type="text" id="name" v-model="name" required>
      <label for="email">Email:</label>
      <input type="email" id="email" v-model="email" required>
      <button type="submit">Submit</button>
    </form>
  </div>
 </div>

  <div class="drag-container">
    <div
      v-for="user in users"
      :key="user.id"
      class="draggable-box"
      :style="{ top: user.positionY + 'px', left: user.positionX + 'px' }"
      @mousedown="startDrag($event, user)"
    >
      <button class="delete-button" @click="deleteUser(user)">
        <img src="/user-delete.png" class="delete-icon">
      </button>
      <strong>{{ user.name.length > 10 ? user.name.slice(0, 10) + '...' : user.name }}</strong>
    </div>
  </div>
</template>
  
<script>
import axios from 'axios'

export default {
  name: 'UsersPage',
  data() {
    return {
      showForm: false,
      users: [],
      draggingUser: null,
      offsetX: 0,
      offsetY: 0
    }
  },
  mounted() {
    axios.get("http://localhost:5039/api/user")
    .then(response => {
  console.log(response.data);
  this.users = response.data;
    })
  },
  methods: {
    startDrag(e, user) {
      this.draggingUser = user
      this.offsetX = e.clientX - user.positionX
      this.offsetY = e.clientY - user.positionY
      document.addEventListener('mousemove', this.onDrag)
      document.addEventListener('mouseup', this.stopDrag)
    },
    onDrag(e) {
      if (this.draggingUser) {
    const container = document.querySelector('.drag-container');
    const containerRect = container.getBoundingClientRect();

    const boxWidth = 100;
    const boxHeight = 100;

    let newX = e.clientX - this.offsetX;
    let newY = e.clientY - this.offsetY;

    // Clamp within container bounds
    newX = Math.max(0, Math.min(containerRect.width - boxWidth, newX));
    newY = Math.max(0, Math.min(containerRect.height - boxHeight, newY));

    this.draggingUser.positionX = newX;
    this.draggingUser.positionY = newY;
  }
    },
    submitForm() {
      axios.post("http://localhost:5039/api/user", {
        name: this.name,
        showForm: false,
        email: this.email
      })
      .then(response => {
        this.showForm = false
        this.name = ''
        this.email = ''
        console.log(response.data)
        this.users.push(response.data)
      })
      .catch(error => {
        console.error(error)
      })
    },
    stopDrag() {
      const user = this.draggingUser;
      this.draggingUser = null;
      document.removeEventListener('mousemove', this.onDrag)
      document.removeEventListener('mouseup', this.stopDrag)

    if (user) {
      axios.put(`http://localhost:5039/api/user/${user.newId}`, {
        posX: user.positionX,
        posY: user.positionY
      })
      .then(response => {
        console.log(response.data)
      })
      .catch(error => {
        console.error(error)
      })
    }
    }
    ,
    deleteUser(user) {
      axios.delete(`http://localhost:5039/api/user/${user.newId}`)
      .then(response => {
        console.log(response.data)
        this.users = this.users.filter(u => u.newId !== user.newId);
      })
      .catch(error => {
        console.error(error)
      })
    }
  }
}
</script>
<style scoped>
.draggable-box {
  position: absolute;
  width: 100px;
  height: 100px;
  background-color: #b2dfdb;
  border: 1px solid #00796b;
  padding: 10px;
  box-sizing: border-box;
  cursor: move;
  user-select: none;
  padding-top: 24px;
  padding-right: 24px;
}
.drag-container {
  position: relative;
  width: 80%;
  height: 600px;
  border: 2px dashed #aaa;
  overflow: hidden;
  margin: 20px auto;

  background-image:
    repeating-linear-gradient(45deg, #b5e2dd 0, #00796b 1px, transparent 1px, transparent 10px);
} 
.btn-and-form-wrapper {
  position: relative;
  display: inline-block;
}

.add-button {
  padding: 10px 16px;
  background-color: #1d4ed8; /* blue-700 */
  color: white;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-weight: bold;
  transition: background-color 0.2s;
}
.add-button:hover {
  background-color: #2563eb; /* blue-600 */
}

.form-container {
  position: absolute;
  top: 100%;
  left: 0;
  width: 250px;
  margin-top: 8px;
  padding: 16px;
  background-color: white;
  border: 1px solid #d1d5db; /* gray-300 */
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  z-index: 10;
}

form {
  display: flex;
  flex-direction: column;
}

label {
  margin-bottom: 4px;
  font-weight: 600;
  color: #374151; /* gray-700 */
}

input {
  margin-bottom: 12px;
  padding: 8px;
  border: 1px solid #cbd5e1; /* gray-300 */
  border-radius: 4px;
  outline: none;
  transition: border 0.2s;
}
input:focus {
  border-color: #2563eb; /* blue-600 */
}

form button[type="submit"] {
  padding: 8px;
  background-color: #16a34a; /* green-600 */
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.2s;
}
form button[type="submit"]:hover {
  background-color: #15803d; /* green-700 */
}
.delete-button {
  position: absolute;
  top: 4px;
  right: 4px;
  background: transparent;
  border: none;
  color: red;
  font-weight: bold;
  font-size: 14px;
  cursor: pointer;
}
.delete-icon {
  width: 16px;
  height: 16px;
}
</style>