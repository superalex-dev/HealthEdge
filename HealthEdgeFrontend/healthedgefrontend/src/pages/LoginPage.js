import React, { useState } from 'react';
import axios from 'axios';
import { jwtDecode } from 'jwt-decode';
import './LoginPage.css';

function LoginPage() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = (e) => {
    e.preventDefault();
    try{
      axios.post('http://localhost:5239/login', {
        username: username,
        password: password
      }).then((response) => {
        const token = response.data.accessToken;
        localStorage.setItem('token', token);
        const decoded = jwtDecode(token);
        localStorage.setItem('username', decoded.sub);
        window.location.href = '/dashboard';
      }).catch((error) => {
        if (error.response) {
          console.log(error.response.data);
          console.log(error.response.status);
          console.log(error.response.headers);
        } else if (error.request) {
          console.log(error.request);
        } else {
          console.log('Error', error.message);
        }
      });
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div className="login-wrapper">
      <div className="login-form">
        <h1>HealthEdge</h1>
        <h2>The better hospital management</h2>
        <p>Please log in using your admin credentials</p>
        <form onSubmit={handleLogin}>
          <label htmlFor="username">Username</label>
          <input
            id="username"
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
          <label htmlFor="password">Password</label>
          <input
            id="password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <button type="submit">Log in</button>
        </form>
      </div>
    </div>
  );
}

export default LoginPage;