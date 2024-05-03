import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from '@mui/joy';
import { useNavigate } from 'react-router-dom';
import './AdminHomePage.css';

function AdminHomePage() {
  const navigate = useNavigate();
  const handlePatientsNavigate = () => {
    navigate('/patients');
  }

  const handleDoctorsNavigate = () => {
    navigate('/doctors');
  }

  const handleUsersNavigate = () => {
    navigate('/users');
  }
  return (
    <div>
      <h1>Admin Home Page</h1>
      <Button sx={{ margin: 1 }} color="primary" type="button" onClick={handlePatientsNavigate}>Patients</Button>
      <Button sx={{ margin: 1 }} color="danger" type="button" onClick={handleDoctorsNavigate}>Doctors</Button>
      <Button sx={{ margin: 1 }} color="success" type="button" onClick={handleUsersNavigate}>Users</Button>
    </div>
  );
}

export default AdminHomePage;