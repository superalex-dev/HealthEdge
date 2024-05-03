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
        const encodedEmail = email.replace('@', '%40');
        const url = `http://localhost:5239/patients/searchByUserNameOrEmail?email=${encodedEmail}`;
        await fetch(url).then(response => {
          if (!response.ok) {
            throw new Error('Network response was not ok');
          }
          return response.json();
        })
        .then(data => {
          localStorage.setItem('patientId', data.id);
        })
        .catch(error => {
          console.error('Error:', error);
        });
        navigate('/home');
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

  const handleDoctorOrAdminViewClick = () => {
    navigate('/adminLogin')
  }

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
          <Button color="success" type="button" onClick={handleDoctorOrAdminViewClick}>Doctor or Admin account</Button>
        </form>
      </div>
    </div>
  );
}


export default LoginPage;