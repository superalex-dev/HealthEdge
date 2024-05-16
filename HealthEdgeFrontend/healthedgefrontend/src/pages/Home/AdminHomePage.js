import React from 'react';
import { Link } from 'react-router-dom';
import Box from '@mui/joy/Box';
import Button from '@mui/joy/Button';
import IconButton from '@mui/joy/IconButton';
import OpenInNew from '@mui/icons-material/OpenInNew';
import { useNavigate } from 'react-router-dom';
import { adminSignOut } from '../../utils/authUtils';
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

  const handleSignOut = () => {
    adminSignOut();
    navigate('/login');
  }


  return (
    <div className="admin-container">
      <h1>Admin Home Page</h1>
      <Button sx={{ margin: 1 }} className="primary" type="button" onClick={handlePatientsNavigate}>Patients</Button>
      <Button sx={{ margin: 1 }} className="danger" type="button" onClick={handleDoctorsNavigate}>Doctors</Button>
      <Button sx={{ margin: 1 }} className="success" type="button" onClick={handleUsersNavigate}>Users</Button>
      <Button sx={{ margin: 1 }} className="secondary" type="button" onClick={handleSignOut}>Sign Out</Button>
      <br></br>
      <br></br>
      <Box className="box">
        <Button component="a" href="/create-doctor" startDecorator={<OpenInNew />}>
          Create New Doctor Or Admin Account
        </Button>
        <IconButton aria-label="Create New Doctor Or Admin Account" component="a" href="/create-doctor" className="icon-button">
          <OpenInNew />
        </IconButton>
      </Box>
    </div>
  );
}

export default AdminHomePage;