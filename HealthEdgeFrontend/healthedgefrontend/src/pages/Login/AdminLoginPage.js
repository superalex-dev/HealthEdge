import React, { useState } from 'react';
import './AdminLoginPage.css';

function AdminLoginPage() {
  const [loginData, setLoginData] = useState({ username: '', password: '' });

  const handleChange = (e) => {
    setLoginData({ ...loginData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(loginData);
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