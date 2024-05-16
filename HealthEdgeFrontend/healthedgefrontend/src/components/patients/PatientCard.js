import React from 'react';
import { useNavigate } from 'react-router-dom';
import { PencilAltIcon, TrashIcon, EyeIcon } from '@heroicons/react/solid';
import './PatientCard.css'; 

const PatientCard = ({ patient, onEdit, onDelete }) => {
  const navigate = useNavigate();

  const handleViewDetails = () => {
    navigate(`/patient-details/${patient.id}`);
  };

  const AddMedicalRecord = () => {
    navigate(`/add-medical-record`, { state: { patientId: patient.id } });
  }

  return (
    <div className="patient-card-container">
      <div className="flex justify-between items-center">
        <div>
          <h3>{`${patient.firstName} ${patient.lastName}`}</h3>
          <p><b>Email: </b>{patient.email}</p>
          <p><b>DOB: </b>{new Date(patient.dateOfBirth).toLocaleDateString()}</p>
          <p><b>Phone number: </b>{patient.contactNumber}</p>
        </div>
        <div className="flex items-center patient-card-buttons">
          <button onClick={handleViewDetails} aria-label="View patient" style={{backgroundColor: "#4CAF50", color: "white"}}>
            View
          </button>
          <button onClick={() => onEdit(patient.id)} aria-label="Edit patient" style={{backgroundColor: "#4CAF50", color: "white"}}>
            Edit
          </button>
          <button onClick={() => onDelete(patient.id)} aria-label="Delete patient" style={{backgroundColor: "#4CAF50", color: "white"}}>
            Delete
          </button>
          <button onClick={() => AddMedicalRecord(patient.id)} aria-label="Add medical record" style={{backgroundColor: "#4CAF50", color: "white"}}>
            Add a medical record
          </button>
        </div>
      </div>
    </div>
  );
};

export default PatientCard;