import React from 'react';
import { useNavigate } from 'react-router-dom';
import { PencilAltIcon, TrashIcon, EyeIcon } from '@heroicons/react/solid';

const PatientCard = ({ patient, onEdit, onDelete }) => {
  const navigate = useNavigate();

  const handleViewDetails = () => {
    navigate(`/patient-details/${patient.id}`);
  };

  return (
    <div className="border border-gray-300 shadow rounded-md p-4 max-w-sm w-full mx-auto mb-4">
      <div className="flex justify-between items-center">
        <div>
          <h3 className="text-lg font-semibold">{`${patient.firstName} ${patient.lastName}`}</h3>
          <p className="text-gray-600">Email: {patient.email}</p>
          <p className="text-gray-500">DOB: {new Date(patient.dateOfBirth).toLocaleDateString()}</p>
        </div>
        <div className="flex items-center">
          <button
            onClick={handleViewDetails}
            className="inline-flex items-center justify-center p-2 rounded-md text-green-500 hover:text-green-700 mr-2"
            aria-label="View patient">
            <EyeIcon className="h-5 w-5" aria-hidden="true" />
          </button>
          <button
            onClick={() => onEdit(patient.id)}
            className="inline-flex items-center justify-center p-2 rounded-md text-blue-500 hover:text-blue-700 mr-2"
            aria-label="Edit patient">
            <PencilAltIcon className="h-5 w-5" aria-hidden="true" />
          </button>
          <button
            onClick={() => onDelete(patient.id)}
            className="inline-flex items-center justify-center p-2 rounded-md text-red-500 hover:text-red-700"
            aria-label="Delete patient">
            <TrashIcon className="h-5 w-5" aria-hidden="true" />
          </button>
        </div>
      </div>
    </div>
  );
};

export default PatientCard;