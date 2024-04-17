import React from 'react';
import { Link } from 'react-router-dom';
import './Dashboard.css'; 

const Dashboard = () => {
  return (
    <div className="dashboard">
      <h1>Welcome to the Dashboard</h1>
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