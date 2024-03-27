// LoginPage.js
import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom'; // For navigation after login
import {jwtDecode} from 'jwt-decode';
import './LoginPage.css'; // Make sure your CSS path is correct

function LoginPage() {
  const [email, setEmail] = useState(''); // Changed from username to email
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    setError(''); // Reset any error message
    try {
      const response = await axios.post('http://localhost:5239/login', {
        email, // Adjusted for email
        password,
      });
      const { token } = response.data;
      localStorage.setItem('token', token);
      const decoded = jwtDecode(token);
      localStorage.setItem('user', decoded.sub); // You might want to store more than just username/email
      navigate('/dashboard'); // Navigate to dashboard or your target route
    } catch (error) {
      // A more detailed error handling can be implemented here
      setError('Failed to login. Please check your credentials and try again.');
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
          <label htmlFor="email">Email</label>
          <input
            id="email"
            type="text"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
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