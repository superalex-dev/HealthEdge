import axios from 'axios';
import { jwtDecode } from 'jwt-decode';

export const login = async (email, password, navigate, setError) => {
  try {
    const response = await axios.post('http://localhost:5239/login', {
      email,
      password,
    });
    const { token } = response.data;
    localStorage.setItem('token', token);
    const decoded = jwtDecode(token);
    localStorage.setItem('user', JSON.stringify(decoded));
    navigate('/dashboard');
  } catch (error) {
    setError('Failed to login. Please check your credentials and try again.');
    console.error(error);
  }
};
