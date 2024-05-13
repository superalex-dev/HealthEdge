import React from 'react';
import { useNavigate } from 'react-router-dom';
import { PencilAltIcon, TrashIcon, EyeIcon } from '@heroicons/react/solid';
import './PatientCard.css'; 

const PatientCard = ({ patient, onEdit, onDelete }) => {
  const navigate = useNavigate();

  const handleViewDetails = () => {
    navigate(`/patient-details/${patient.id}`);
  };

  return (
    <div className="patient-card-container">
      <div className="flex justify-between items-center">
        <div>
          <h3>{`${patient.firstName} ${patient.lastName}`}</h3>
          <p>Email: {patient.email}</p>
          <p>DOB: {new Date(patient.dateOfBirth).toLocaleDateString()}</p>
          <p>Phone: {patient.contactNumber}</p>
        </div>
        <div className="flex items-center patient-card-buttons">
          <button onClick={handleViewDetails} aria-label="View patient">
            View
          </button>
          <button onClick={() => onEdit(patient.id)} aria-label="Edit patient">
            Edit
          </button>
          <button onClick={() => onDelete(patient.id)} aria-label="Delete patient">
            Delete
          </button>
        </div>
      </div>
    </div>
  );
};

export default PatientCard;