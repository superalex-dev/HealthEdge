import React from 'react';
import { Link } from 'react-router-dom';
import Calendar from 'react-calendar'
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
        </ul>
      </nav>
      <Calendar />
    </div>
  );
};

export default Dashboard;