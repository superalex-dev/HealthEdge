import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button } from '@mui/joy';
import { login } from '../../utils/authUtils'; 
import './LoginPage.css';
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';

  export function LoginPage() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
      e.preventDefault();
      const result = await login(email, password, navigate, setError);
      if (result) {
        navigate('/dashboard');
      } else {
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
          <Button color="primary" type="submit" onClick={handleSubmit}>Log in</Button>
          <Button color="danger" type="button" onClick={handleJoinUsClick}>Join us</Button>
        </form>
      </div>
    </div>
  );
}


export default LoginPage;