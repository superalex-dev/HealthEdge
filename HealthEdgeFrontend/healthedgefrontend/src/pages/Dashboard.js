import React from 'react';
import { Link } from 'react-router-dom';

const Dashboard = () => {
  return (
    <div>
      <h1>Welcome to the Dashboard</h1>
      <nav>
        <ul>
          <li><Link to="/book-appointment">Book Appointment</Link></li>
        </ul>
      </nav>
    </div>
  );
};

export default Dashboard;