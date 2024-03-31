import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { Button } from '@mui/joy';
import { login } from '../utils/authUtils'; 
import {jwtDecode} from 'jwt-decode';
  import './LoginPage.css';

  export function LoginPage() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleSubmit = (e) => {
      e.preventDefault();
      login(email, password, navigate, setError);
    };

  const handleJoinUsClick = () => {
    navigate('/register');
  };

  return (
    <div className="login-wrapper">
      <div className="login-form">
        <h1>HealthEdge</h1>
        <h2>The better hospital management</h2>
        <p>Please log in using your admin credentials</p>
        <form onSubmit={login}>
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
          {/* <button type="submit">Log in</button> */}
          <Button color="primary" type="submit">Log in</Button>
          <Button color="danger" type="button" onClick={handleJoinUsClick}>Join us</Button>
        </form>
      </div>
    </div>
  );
}


export default LoginPage;