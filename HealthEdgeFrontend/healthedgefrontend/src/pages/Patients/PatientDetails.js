import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import './PatientDetails.css';

const PatientDetails = () => {
  const [patient, setPatient] = useState({});
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPatientDetails = async () => {
      try {
        const response = await axios.get(`http://localhost:5239/patients/get/${id}`);
        setPatient(response.data);
      } catch (error) {
        console.error('Failed to fetch patient details:', error);
        //error handling
      }
    };

    fetchPatientDetails();
  }, [id]);

  return (
    <div className="patient-details-container">
      <h1 className="patient-details-heading">Patient Details</h1>
      <div className="patient-details-card">
        <div className="patient-details-section">
          <h3>{patient.firstName} {patient.lastName}</h3>
          <p>Personal details and more.</p>
        </div>
        <div className="patient-details-section">
          <dl>
            <div>
              <b>Full name: </b>  {patient.firstName} {patient.lastName}
            </div>
            <div>
              <b>Email address:</b>  {patient.email}
            </div>
            <div>
              <b>Date of birth:</b>  {new Date(patient.dateOfBirth).toLocaleDateString()}
            </div>
            <div>
              <b>Phone number:</b>  {patient.contactNumber}
            </div>
            <div>
              <b>Gender:</b>  {patient.gender}
            </div>
            <div>
              <b>Blood type:</b>  {patient.bloodType}
            </div>
            <div>
              <b>Address:</b>  {patient.address}
            </div>
            <div>
              <b>UserId:</b>  {patient.Id}
            </div>
          </dl>
        </div>
      </div>
      <button
        onClick={() => navigate('/patients')}
        className="back-button"
      >
        Back to Patients List
      </button>
    </div>
  );
};

export default PatientDetails;