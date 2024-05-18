import React from 'react';
import { useNavigate } from 'react-router-dom';
import './DoctorListCard.css'; 

const DoctorListCard = ({ doctor, onEdit, onDelete }) => {
  const navigate = useNavigate();

  const handleViewDetails = () => {
    navigate(`/doctor-details/${doctor.id}`);
  };

  return (
    <div className="doctor-card-container">
      <div className="flex justify-between items-center">
        <div>
          <h3>{`${doctor.firstName} ${doctor.lastName}`}</h3>
          <p><b>Email: </b>{doctor.email}</p>
          <p><b>DOB: </b>{new Date(doctor.dateOfBirth).toLocaleDateString()}</p>
          <p><b>Phone number: </b>{doctor.contactNumber}</p>
        </div>
        <div className="flex items-center doctor-card-buttons">
          <button onClick={handleViewDetails} aria-label="View doctor" style={{backgroundColor: "#4CAF50", color: "white"}}>
            View
          </button>
          <button onClick={() => onEdit(doctor.id)} aria-label="Edit doctor" style={{backgroundColor: "#4CAF50", color: "white"}}>
            Edit
          </button>
          <button onClick={() => onDelete(doctor.id)} aria-label="Delete doctor" style={{backgroundColor: "#4CAF50", color: "white"}}>
            Delete
          </button>
        </div>
      </div>
    </div>
  );
};

export default DoctorListCard;