import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';

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
              // Optionally handle the error
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
    <div className="max-w-md mx-auto mt-10">
      <h2 className="text-2xl font-bold mb-5 text-center">Edit Patient</h2>
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block text-sm font-medium text-gray-700">First Name:</label>
          <input
            type="text"
            name="firstName"
            value={patient.firstName}
            onChange={handleChange}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700">Last Name:</label>
          <input
            type="text"
            name="lastName"
            value={patient.lastName}
            onChange={handleChange}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700">Email:</label>
          <input
            type="email"
            name="email"
            value={patient.email}
            onChange={handleChange}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700">Date of Birth:</label>
          <input
            type="date"
            name="dateOfBirth"
            value={patient.dateOfBirth.slice(0, 10)}
            onChange={handleChange}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700">Gender:</label>
          <input
            type="text"
            name= "gender"
            value = {patient.gender}
            onChange = {handleChange}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700">Blood type:</label>
          <input
            type="text"
            name="bloodType"
            value={patient.bloodType}
            onChange={handleChange}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
          />
          </div>
          <div>
          <label className="block text-sm font-medium text-gray-700"> Contact Number:</label>
          <input
            type="text"
            name="contactNumber"
            value={patient.contactNumber}
            onChange={handleChange}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
          />
          </div>
          <div>
          <label className="block text-sm font-medium text-gray-700"> Address:</label>
          <input
            type="text"
            name="address"
            value={patient.address}
            onChange={handleChange}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
          />
          </div>
          <div>
        </div>
        <button type="submit" className="inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
          Update Patient
        </button>
      </form>
    </div>
  );
};

export default EditPatient;