import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';
import { confirmAlert } from 'react-confirm-alert';
import './EditPatient.css'; 


const EditPatient = () => {
  const [patient, setPatient] = useState({
    firstName: '',
    lastName: '',
    email: '',
    dateOfBirth: '',
    gender: '',
    bloodType: '',
    contactNumber: '',
    address: '',
    userId: '',
  });
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPatient = async () => {
      try {
        const { data } = await axios.get(`http://localhost:5239/patients/get/${id}`);
        setPatient(data);
      } catch (error) {
        console.error('Failed to fetch patient:', error);
        //handling error
      }
    };

    if (id) {
      fetchPatient();
    }
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setPatient(prev => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    confirmAlert({
      title: 'Confirm to update',
      message: 'Are you sure you want to update this patient?',
      buttons: [
        {
          label: 'Yes',
          onClick: async () => {
            try {
              await axios.put(`http://localhost:5239/patients/edit/${id}`, patient);
              navigate('/patients');
            } catch (error) {
              console.error('Failed to update patient:', error);
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

  return (
    <div className="edit-patient-container">
      <h2 className="edit-patient-heading">Edit Patient</h2>
      <form onSubmit={handleSubmit} className="edit-patient-form">
        <div>
          <label className="edit-patient-label">First Name:</label>
          <input
            type="text"
            name="firstName"
            value={patient.firstName}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <div>
          <label className="edit-patient-label">Last Name:</label>
          <input
            type="text"
            name="lastName"
            value={patient.lastName}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <div>
          <label className="edit-patient-label">Email:</label>
          <input
            type="email"
            name="email"
            value={patient.email}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <div>
          <label className="edit-patient-label">Date of Birth:</label>
          <input
            type="date"
            name="dateOfBirth"
            value={patient.dateOfBirth.slice(0, 10)}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <div>
          <label className="edit-patient-label">Gender:</label>
          <input
            type="text"
            name="gender"
            value={patient.gender}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <div>
          <label className="edit-patient-label">Blood type:</label>
          <input
            type="text"
            name="bloodType"
            value={patient.bloodType}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <div>
          <label className="edit-patient-label">Contact Number:</label>
          <input
            type="text"
            name="contactNumber"
            value={patient.contactNumber}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <div>
          <label className="edit-patient-label">Address:</label>
          <input
            type="text"
            name="address"
            value={patient.address}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <button type="submit" className="edit-patient-button">
          Edit Patient
        </button>
      </form>
    </div>
  );
};

export default EditPatient;