import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import './UserDetails.css';

const UserDetails = () => {
  const [user, setUser] = useState({});
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUserDetails = async () => {
      try {
        const response = await axios.get(`http://localhost:5239/users/get/${id}`);
        setUser(response.data);
      } catch (error) {
        console.error('Failed to fetch user details:', error);
        //error handling
      }
    };

    fetchUserDetails();
  }, [id]);

  return (
    <div className="user-details-container">
      <h1 className="user-details-heading">User Details</h1>
      <div className="user-details-card">
        <div className="user-details-section">
          <h3>{user.firstName} {user.lastName}</h3>
          <p>Personal details and more.</p>
        </div>
        <div className="user-details-section">
          <dl>
            <div>
              <b>Full name: </b>  {user.firstName} {user.lastName}
            </div>
            <div>
              <b>Username: </b>  {user.userName} {user.userName}
            </div>
            <div>
              <b>Email address:</b>  {user.email}
            </div>
            <div>
              <b>Password:</b>  {user.password}
            </div>
            <div>
              <b>Date of birth:</b>  {new Date(user.dateOfBirth).toLocaleDateString()}
            </div>
            <div>
              <b>Phone number:</b>  {user.contactNumber}
            </div>
            <div>
              <b>Date of creation:</b>  {new Date(user.dateOfCreation).toLocaleDateString()}
            </div>
          </dl>
        </div>
      </div>
      <button
        onClick={() => navigate('/users')}
        className="back-button"
      >
        Back to Users List
      </button>
    </div>
  );
};

export default UserDetails;