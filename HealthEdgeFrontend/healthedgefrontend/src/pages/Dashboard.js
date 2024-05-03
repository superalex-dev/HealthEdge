import React from 'react';
import { Link } from 'react-router-dom';
import { getCurrentUser, signOut } from '../utils/authUtils';
import './Dashboard.css'; 

const Dashboard = () => {
  const user = getCurrentUser();

  const handleSignOut = () => {
    console.log('Signing out...');
    signOut();
    window.location.replace('/login');
  };

  return (
    <div className="dashboard">
      <h1>Welcome to the Dashboard</h1>
      <p>Logged in as: {user.userName}</p>
      <button onClick={handleSignOut}>Sign Out</button>
      <nav>
        <ul>
          <li><Link to="/book-appointment">Book Appointment</Link></li>
          <li><Link to="/login">Login</Link></li>
          <li><Link to="/patients">Patients</Link></li>
          <li><Link to="/lekari">Lekari</Link></li>
          <li><Link to="/home">Home</Link></li>
        </ul>
      </nav>
    </div>
  );
};

export default Dashboard;