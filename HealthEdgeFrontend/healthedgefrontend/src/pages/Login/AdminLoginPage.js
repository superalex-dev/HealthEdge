import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { confirmAlert } from 'react-confirm-alert';
import './AdminLoginPage.css';

function AdminLoginPage() {
  const [loginData, setLoginData] = useState({ username: '', password: '' });
  const navigate = useNavigate();

  const handleChange = (e) => {
    setLoginData({ ...loginData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const response = await fetch('http://localhost:5239/admin-login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(loginData),
    });

    const data = await response.json();

    if (data) {
      console.log(data);
      localStorage.setItem('role', 'admin');
      localStorage.setItem('doctorId', data.id);
      navigate('/admin-dashboard');
    } else {
      console.log('Login failed');
      confirmAlert({
        title: 'Error',
        message: 'Invalid credentials. Please try again.',
        buttons: [
          {
            label: 'Ok',
          },
        ],
      });
    }
  };

  return (
    <div className="admin-login-page">
      <form onSubmit={handleSubmit} className="admin-login-form">
        <label>
          Username:
          <input type="text" name="username" value={loginData.username} onChange={handleChange} required />
        </label>
        <label>
          Password:
          <input type="password" name="password" value={loginData.password} onChange={handleChange} required />
        </label>
        <button type="submit">Log in</button>
      </form>
    </div>
  );
}

export default AdminLoginPage;