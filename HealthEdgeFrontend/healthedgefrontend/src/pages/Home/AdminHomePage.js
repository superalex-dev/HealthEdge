import React from 'react';
import { Link } from 'react-router-dom';
import Box from '@mui/joy/Box';
import Button from '@mui/joy/Button';
import IconButton from '@mui/joy/IconButton';
import OpenInNew from '@mui/icons-material/OpenInNew';
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
      <br></br>
      <br></br>
      <Box sx={{ display: 'flex', gap: 2, alignItems: 'center' }}>
      <Button component="a" href="/create-doctor" startDecorator={<OpenInNew />}>
        Create New Doctor Or Admin Account
      </Button>
      <IconButton aria-label="Create New Doctor Or Admin Account" component="a" href="/create-doctor">
        <OpenInNew />
      </IconButton>
    </Box>
    </div>
  );
}

export default AdminHomePage;