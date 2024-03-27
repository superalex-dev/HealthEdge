import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './RegisterPage.css';

function RegisterPage() {
    const [user, setUser] = useState({
        firstName: '',
        lastName: '',
        userName: '',
        email: '',
        password: '',
    });
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleChange = (e) => {
        const { name, value } = e.target;
        setUser((prevState) => ({
            ...prevState,
            [name]: value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        try {
            const response = await axios.post('http://localhost:5239/users/create', user);
            if (response.status === 200) {
                navigate('/login');
            }
        } catch (error) {
            setError('Registration failed. Please try again.');
            console.error(error);
        }
    };

    return (
        <div className="register-page">
            <div className="form-container">
                <div className="logo-container">
                    <img src="/healthedgelogo.png" alt="Logo" className="logo" />
                </div>
                <form onSubmit={handleSubmit} className="register-form">
                    <h2>Sign up</h2>
                    {error && <div className="error">{error}</div>}
                    <div className="input-row">
                        <input
                            type="text"
                            name="firstName"
                            placeholder="First name"
                            value={user.firstName}
                            onChange={handleChange}
                            required
                        />
                        <input
                            type="text"
                            name="lastName"
                            placeholder="Last name"
                            value={user.lastName}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <input
                        type="text"
                        name="userName"
                        placeholder="Username"
                        value={user.userName}
                        onChange={handleChange}
                        required
                    />
                    <input
                        type="email"
                        name="email"
                        placeholder="Email"
                        value={user.email}
                        onChange={handleChange}
                        required
                    />
                    <input
                        type="password"
                        name="password"
                        placeholder="Password"
                        value={user.password}
                        onChange={handleChange}
                        required
                    />
                    <button type="submit" className="submit-btn">Create account</button>
                </form>
            </div>
            <div className="inspiration-container">
                <div className="testimonial">
                    <blockquote>
                        ”lorem" ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi.
                        “I’ve never felt so good about my health!”
                    </blockquote>
                    <cite>— Alexander Boev</cite>
                </div>
                <div className="user-count">
                    Join professionals who trust HealthEdge with their needs!
                </div>
            </div>
        </div>
    );
}

export default RegisterPage;