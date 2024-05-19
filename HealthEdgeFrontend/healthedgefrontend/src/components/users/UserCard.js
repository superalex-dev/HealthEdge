import React from 'react';
import { useNavigate } from 'react-router-dom';
import { PencilAltIcon, TrashIcon, EyeIcon } from '@heroicons/react/solid';
import './UserCard.css'; 

const UserCard = ({ user, onEdit, onDelete }) => {
  const navigate = useNavigate();

  const handleViewDetails = () => {
    navigate(`/user-details/${user.id}`);
  };

  return (
    <div className="user-card-container">
      <div className="flex justify-between items-center">
        <div>
          <h3>{`${user.firstName} ${user.lastName}`}</h3>
          <p><b>Email: </b>{user.email}</p>
          <p><b>Username: </b>{user.userName}</p>
          <p><b>Date Of birth: </b>{new Date(user.dateOfBirth).toLocaleDateString()}</p>
          <p><b>Phone number: </b>{user.contactNumber}</p>
        </div>
        <div className="flex items-center user-card-buttons">
          <button onClick={handleViewDetails} aria-label="View user" style={{backgroundColor: "#4CAF50", color: "white"}}>
            View
          </button>
          <button onClick={() => onEdit(user.id)} aria-label="Edit user" style={{backgroundColor: "#4CAF50", color: "white"}}>
            Edit
          </button>
          <button onClick={() => onDelete(user.id)} aria-label="Delete user" style={{backgroundColor: "#4CAF50", color: "white"}}>
            Delete
          </button>
        </div>
      </div>
    </div>
  );
};

export default UserCard;