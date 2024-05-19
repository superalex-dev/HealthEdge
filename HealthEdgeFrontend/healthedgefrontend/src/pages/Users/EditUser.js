import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';
import { confirmAlert } from 'react-confirm-alert';
import './EditUser.css'; 
import { Button } from '@mui/joy';


const EditUser = () => {
  const [user, setUser] = useState({
    firstName: '',
    lastName: '',
    userName: '',
    email: '',
    dateOfBirth: '',
    contactNumber: '',
  });
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const { data } = await axios.get(`http://localhost:5239/users/get/${id}`);
        setUser(data);
      } catch (error) {
        console.error('Failed to fetch user:', error);
        //handling error
      }
    };

    if (id) {
      fetchUser();
    }
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUser(prev => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    confirmAlert({
      title: 'Confirm to update',
      message: 'Are you sure you want to update this user?',
      buttons: [
        {
          label: 'Yes',
          onClick: async () => {
            try {
              await axios.put(`http://localhost:5239/users/edit/${id}`, user);
              navigate('/users');
            } catch (error) {
              console.error('Failed to update user:', error);
              //handle the error
            }
          }
        },
        {
          label: 'No',
          onClick: () => {}
        }
      ]
    });
  };

  const handleUsersRedirect = () => {
    navigate('/users');
  }

  return (
    <div className="edit-user-container">
      <h2 className="edit-user-heading">Edit User</h2>
      <form onSubmit={handleSubmit} className="edit-user-form">
        <div>
          <label className="edit-user-label">First Name:</label>
          <input
            type="text"
            name="firstName"
            value={user.firstName}
            onChange={handleChange}
            className="edit-user-input"
          />
        </div>
        <div>
          <label className="edit-user-label">Last Name:</label>
          <input
            type="text"
            name="lastName"
            value={user.lastName}
            onChange={handleChange}
            className="edit-user-input"
          />
        </div>
        <div>
          <label className="edit-user-label">Username:</label>
          <input
            type="text"
            name="userName"
            value={user.userName}
            onChange={handleChange}
            className="edit-user-input"
          />
        </div>
        <div>
          <label className="edit-user-label">Email:</label>
          <input
            type="email"
            name="email"
            value={user.email}
            onChange={handleChange}
            className="edit-user-input"
          />
        </div>
        <div>
          <label className="edit-user-label">Date of Birth:</label>
          <input
            type="date"
            name="dateOfBirth"
            value={user.dateOfBirth.slice(0, 10)}
            onChange={handleChange}
            className="edit-user-input"
          />
        </div>
        <div>
          <label className="edit-user-label">Contact Number:</label>
          <input
            type="text"
            name="contactNumber"
            value={user.contactNumber}
            onChange={handleChange}
            className="edit-user-input"
          />
        </div>
        <button type="submit" className="edit-user-button">
          Edit User
        </button>
        <Button color="primary" type="button" onClick={handleUsersRedirect}>Back to users list</Button>
      </form>
    </div>
  );
};

export default EditUser;