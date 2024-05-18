import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';
import { confirmAlert } from 'react-confirm-alert';
import './EditDoctor.css'; 


const EditDoctor = () => {
  const [doctor, setDoctor] = useState({
    firstName: '',
    lastName: '',
    userName: '',
    isPedriatritian: true,
    contactNumber: '',
    email: '',
    dateOfBirth: '',
    NZOK: true,
    imageURL: '',
  });
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPatient = async () => {
      try {
        const { data } = await axios.get(`http://localhost:5239/doctors/get/${id}`);
        setDoctor(data);
      } catch (error) {
        console.error('Failed to fetch doctor:', error);
        //handling error
      }
    };

    if (id) {
      fetchPatient();
    }
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;

    if(name == "NZOK" || name == "pedriatritian"){
      if(value === "Yes"){
        value = true;
      }else{
        value = false;
      }
    }
    setDoctor(prev => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    confirmAlert({
      title: 'Confirm to update',
      message: 'Are you sure you want to update this doctor?',
      buttons: [
        {
          label: 'Yes',
          onClick: async () => {
            try {
              await axios.put(`http://localhost:5239/doctors/edit/${id}`, doctor);
              navigate('/doctors');
            } catch (error) {
              console.error('Failed to update doctor:', error);
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
      <h2 className="edit-patient-heading">Edit Doctor</h2>
      <form onSubmit={handleSubmit} className="edit-patient-form">
        <div>
          <label className="edit-patient-label">First Name:</label>
          <input
            type="text"
            name="firstName"
            value={doctor.firstName}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <div>
          <label className="edit-patient-label">Last Name:</label>
          <input
            type="text"
            name="lastName"
            value={doctor.lastName}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <div>
          <label className="edit-patient-label">Email:</label>
          <input
            type="email"
            name="email"
            value={doctor.email}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <div>
          <label className="edit-patient-label">Date of Birth:</label>
          <input
            type="date"
            name="dateOfBirth"
            value={doctor.dateOfBirth.slice(0, 10)}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <div>
          <label className="edit-patient-label">Contact Number:</label>
          <input
            type="text"
            name="contactNumber"
            value={doctor.contactNumber}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <div>
          <label className="edit-patient-label">Has NZOK:</label>
          <input
            type="text"
            name="NZOK"
            value={doctor.NZOK ? "Yes" : "No"}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <div>
          <label className="edit-patient-label">Is pedriatritian:</label>
          <input
            type="text"
            name="pedriatritian"
            value={doctor.isPedriatritian ? "Yes" : "No"}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <div>
          <label className="edit-patient-label">Image URL:</label>
          <input
            type="text"
            name="bloodType"
            value={doctor.imageURL}
            onChange={handleChange}
            className="edit-patient-input"
          />
        </div>
        <button type="submit" className="edit-patient-button">
          Edit Doctor
        </button>
      </form>
    </div>
  );
};

export default EditDoctor;