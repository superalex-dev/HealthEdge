import axios from 'axios';
import { jwtDecode } from 'jwt-decode';

export const login = async (email, password, navigate, setError) => {
  try {
    const response = await axios.post('http://localhost:5239/login', {
      email,
      password,
    });

    console.log(response);
    const { token } = response.data;
    localStorage.setItem('token', token);
    const decoded = jwtDecode(token);
    localStorage.setItem('user', JSON.stringify(decoded));
    navigate('/home');
    return true;
  } catch (error) {
    setError('Failed to login. Please check your credentials and try again.');
    console.error(error);
    return false;
  }
};

export const getCurrentUser = () => {
  const user = localStorage.getItem('user');
  return user ? JSON.parse(user) : null;
};

export const signOut = () => {
  localStorage.removeItem('token');
  localStorage.removeItem('user');
  window.location.replace('/login');
};

export const adminSignOut = () => {
  localStorage.removeItem('role');
};

export const getToken = () => {
  return localStorage.getItem('token');
}